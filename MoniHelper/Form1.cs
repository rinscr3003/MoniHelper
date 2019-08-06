using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoniHelper
{
    public partial class FormMaster : Form
    {

        /* ***********************************************************
         * 模拟助手部分源码借鉴MifareOneTool，在此向原作者XAS-712致敬！
         * 因MifareOneTool为GPL协议，本软件同样以GPL协议开源！
         * 因不正当使用本软件所造成的后果与作者无关！由使用人自行负责！
         * BY RINSCR3003
         * ***********************************************************/
        public FormMaster()
        {
            InitializeComponent();
        }

        private Process process = new Process();
        private void FormMaster_Load(object sender, EventArgs e)
        {
            string text = MoniHelper.Properties.Resources.String1;
            if (MessageBox.Show("使用本软件前需要阅读以下内容：\n" + text + "\n若同意请点确认，否则点击取消退出软件！", "必读", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                Environment.Exit(-2);
            }
            File.Delete("libnfc.conf");
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(List_dev);
            bgw.WorkerReportsProgress = true;
            bgw.ProgressChanged += Res_list_dev;
            bgw.RunWorkerAsync();
            richTextBox1.Text = "欢迎使用模拟助手！\n模拟助手支持半加密的M1门禁卡片，请等待设备名字显示后，放置您的卡片点下一步进行检测！";
        }

        int stepCount = 1;

        void List_dev(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("nfc-bin/nfc-scan-device.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            BackgroundWorker b = (BackgroundWorker)sender;
            process = Process.Start(psi);
            List<string> myReader = new List<string>();
            process.OutputDataReceived += (s, _e) =>
            {
                b.ReportProgress(0, _e.Data);
                if (!string.IsNullOrEmpty(_e.Data))
                {
                    Match m = Regex.Match(_e.Data, "pn532_uart:COM\\d+:115200");
                    if (m.Success)
                    {
                        myReader.Add(m.Value);
                    }
                }
            };
            process.ErrorDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            //StreamReader stderr = process.StandardError;
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            b.ReportProgress(103, myReader);
        }

        void WriteConfig(string devstr, bool autoscan = true, bool intscan = false)
        {
            string cfg = "allow_autoscan = " + (autoscan ? "true" : "false") + "\n";
            cfg += "allow_intrusive_scan = " + (intscan ? "true" : "false") + "\n";
            cfg += "device.name = \"NFC - Device\"\n";
            cfg += "device.connstring = \"" + devstr + "\"";
            File.WriteAllText("libnfc.conf", cfg);
        }

        string devname = "";

        void Res_list_dev(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                LogAppend((string)e.UserState);
            }
            else if (e.ProgressPercentage == 103)
            {
                List<string> myReaders = (List<string>)(e.UserState);
                if (myReaders.Count > 0)
                {
                    WriteConfig(myReaders.First());
                    devname = myReaders.First();
                    lDevName.Text = devname;
                }
                else
                {
                    MessageBox.Show("没有发现任何有效的NFC设备\n请检查接线是否正确,驱动是否正常安装,设备电源是否已经打开。\n确认无误后请重新运行软件。");
                    System.Environment.Exit(-1);
                }
            }
        }

        void ChkCard()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(W_listcard);
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Reporter;
            worker.RunWorkerAsync();
        }

        void W_listcard(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("nfc-bin/nfc-list.exe")
            {
                Arguments = "-t 1",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            BackgroundWorker b = (BackgroundWorker)sender;
            process = Process.Start(psi);
            string[] thisTag = new string[4];
            process.OutputDataReceived += (s, _e) =>
            {
                b.ReportProgress(0, _e.Data);
                if (!string.IsNullOrEmpty(_e.Data))
                {
                    Match m = Regex.Match(_e.Data, @"UID\s\(NFCID\d\)\: ([0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2})");
                    if (m.Success)
                    {
                        thisTag[0] = m.Captures[0].Value.Replace(" ", "").Split(':')[1];
                    }
                    m = Regex.Match(_e.Data, @"ATQA\s\(SENS_RES\)\: ([0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2})");
                    if (m.Success)
                    {
                        thisTag[1] = m.Captures[0].Value.Replace(" ", "").Replace("ATQA(SENS_RES):", "");
                    }
                    m = Regex.Match(_e.Data, @"SAK\s\(SEL_RES\)\: ([0-9A-Fa-f]{2})");
                    if (m.Success)
                    {
                        thisTag[2] = m.Captures[0].Value.Replace(" ", "").Replace("SAK(SEL_RES):", "");

                        string sak = thisTag[2];
                        switch (sak)
                        {
                            case "08":
                            case "88":
                                thisTag[3] = "M1卡";
                                break;

                            case "20":
                            case "28":
                                thisTag[3] = "CPU卡";
                                break;

                            default:
                                thisTag[3] = "未知类型";
                                break;
                        }
                    }
                }
            };
            process.ErrorDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            b.ReportProgress(3, thisTag);
            b.ReportProgress(1, "##运行完毕##");
        }

        private void Reporter(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    LogAppend((string)e.UserState);
                    break;

                case 1:
                    LogAppend((string)e.UserState);
                    break;

                case 2:
                    GetCardSuccess();
                    break;

                case 20:
                    GetCardFail();
                    break;

                case 3://cardinfo
                    ShowCardInfo((string[])e.UserState);
                    break;

                case 4:
                    WriteUIDCardSuccess();
                    break;

                case 40:
                    WriteUIDCardFail();
                    break;

                case 100:
                    WriteBandSuccess();
                    break;

                case 105:
                    BandUIDErr((string[])e.UserState);
                    break;

                case 200:
                    WriteBandFail();
                    break;

                default:
                    LogAppend((string)e.UserState);
                    break;
            }
        }

        private void BandUIDErr(string[] userState)
        {
            if (userState[0] != cardid)
            {
                richTextBox1.AppendText("警告，模拟卡卡号（" + userState[0] + "）与原卡（"+cardid+"）不同\n");
            }
            if (userState[1] != "可写卡")
            {
                richTextBox1.AppendText("警告，模拟卡可能不能写入\n");
            }
            richTextBox1.ScrollToCaret();
        }

        private void WriteBandFail()
        {
            MessageBox.Show("刷写失败了，建议重试！");
            buttonDo.Enabled = true;
        }

        private void WriteBandSuccess()
        {
            checkBoxWriteBand.Checked = true;
            stepCount++;
            progressBar1.PerformStep();
            richTextBox1.Text = "模拟卡刷写完成，请取下模拟卡，关闭软件。";
        }

        private void WriteUIDCardFail()
        {
            MessageBox.Show("刷写失败了，请重试！");
            buttonDo.Enabled = true;
        }

        private void WriteUIDCardSuccess()
        {
            checkBoxWriteUID.Checked = true;
            stepCount++;
            progressBar1.PerformStep();
            buttonDo.Enabled = true;
            richTextBox1.Text = "刷写完成，请取下UID卡，用模拟卡设备模拟此UID卡，完成后点击下一步。";
        }

        bool Writecheck(string file)
        {
            //if (checkBoxWriteProtect.Checked == false)
            //{ return true; }//如果禁用，直接假装检查成功
            S50 card = new S50();
            try
            {
                card.LoadFromMfd(file);
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "打开出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (card.Verify()[16] == 0x00)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetCardFail()
        {
            if (MessageBox.Show("解析失败了，要使用已有dump数据嘛？", "解析失败", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Filter = "dump文件|*.dump",
                    Title = "使用已有dump数据",
                    Multiselect = false
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!Writecheck(ofd.FileName)) { MessageBox.Show("这个文件不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    File.Delete("work.dump");
                    File.Copy(ofd.FileName,"work.dump");
                    checkBoxGetData.Checked = true;
                    stepCount++;
                    progressBar1.PerformStep();
                    richTextBox1.Text = "解析完了，请取下原卡，在设备上面放一张UID卡(注意CUID卡不能用)，点击下一步程序会擦除这张卡并刷写卡号。";
                }
                else
                {
                    ;
                }
            }
            else
            {
                ;
            }
            buttonDo.Enabled = true;
        }

        private void GetCardSuccess()
        {
            checkBoxGetData.Checked = true;
            stepCount++;
            progressBar1.PerformStep();
            buttonDo.Enabled = true;
            richTextBox1.Text = "解析完了，请取下原卡，在设备上面放一张UID卡(注意CUID卡不能用)，点击下一步程序会擦除这张卡并刷写卡号。";
        }

        private void LogAppend(string userState)
        {
            richTextBox2.AppendText(userState + "\n");
            richTextBox2.ScrollToCaret();
        }

        string cardid = "DEADBEEF";

        private void ShowCardInfo(string[] p)
        {
            string pat = "[0-9A-Fa-f]{8}";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("卡号：" + p[0]);
            cardid = Regex.IsMatch(p[0], pat) ? p[0] : "DEADBEEF";
            sb.AppendLine("类型：" + p[3]);
            if (p[3] == null)
            {
                MessageBox.Show("出错啦，请重试，没有扫描到卡！\n详情参见日志输出窗口！");
            }
            else if (p[3] == "M1卡")
            {
                checkBoxChkCard.Checked = true;
                stepCount++;
                progressBar1.PerformStep();
                richTextBox1.Text = "已经识别了卡片:\n" + sb.ToString() + "\n请保持卡片不动，点击下一步进行解析";
            }
            else
            {
                MessageBox.Show("出错啦，请重试，卡类型不支持！\n详情参见日志输出窗口！");
            }
            buttonDo.Enabled = true;
        }

        private void ButtonDo_Click(object sender, EventArgs e)
        {
            switch (stepCount)
            {
                default:
                    break;
                case 1:
                    buttonDo.Enabled = false;
                    ChkCard();
                    break;
                case 2:
                    buttonDo.Enabled = false;
                    GetCard();
                    break;
                case 3:
                    buttonDo.Enabled = false;
                    WriteUIDCard();
                    break;
                case 4:
                    ;
                    checkBoxEmuUID.Checked = true;
                    stepCount++;
                    progressBar1.PerformStep();
                    richTextBox1.Text = "请将模拟卡放在读卡器设备上，点击下一步程序将刷入卡内数据";
                    break;
                case 5:
                    buttonDo.Enabled = false;
                    WriteBand();
                    break;
            }
        }

        private void WriteBand()
        {
            string rmfd = "work.dump";
            if (!Writecheck(rmfd)) { MessageBox.Show("将要写入的文件存在错误,不能继续", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(Mf_write);
            bgw.WorkerReportsProgress = true;
            bgw.ProgressChanged += Reporter;
            bgw.RunWorkerAsync(rmfd);
        }

        void Mf_write(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("nfc-bin/nfc-mfclassic.exe");
            string rmfd = (string)e.Argument;
            psi.Arguments = "w a u \"" + rmfd + "\"";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            BackgroundWorker b = (BackgroundWorker)sender;
            process = Process.Start(psi);
            string[] card = new string[2];
            b.ReportProgress(0, "开始执行写入M1卡片");
            process.OutputDataReceived += (s, _e) =>
            {
                b.ReportProgress(0, _e.Data); if (!string.IsNullOrEmpty(_e.Data))
                {
                    Match m = Regex.Match(_e.Data, @"UID\s\(NFCID\d\)\: ([0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2}\s\s[0-9A-Fa-f]{2})");
                    if (m.Success)
                    {
                        card[0] = m.Captures[0].Value.Replace(" ", "").Split(':')[1].Trim();
                    }
                    m = Regex.Match(_e.Data, @"SAK\s\(SEL_RES\)\: ([0-9A-Fa-f]{2})");
                    if (m.Success)
                    {
                        string sak = m.Captures[0].Value.Replace(" ", "").Replace("SAK(SEL_RES):", "");
                        switch (sak)
                        {
                            case "08":
                            case "28":
                            case "88":
                                card[1] = "可写卡";
                                break;

                            case "20":
                                card[1] = "不可写CPU卡";
                                break;

                            default:
                                card[1] = "未知类型";
                                break;
                        }
                    }
                }
            };
            process.ErrorDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            //StreamReader stderr = process.StandardError;
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            b.ReportProgress(105, card);
            if (process.ExitCode == 0)
            {
                b.ReportProgress(100);
            }
            else
            {
                b.ReportProgress(200);
            }
        }

        private void WriteUIDCard()
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(Reset_uid);
            bgw.WorkerReportsProgress = true;
            bgw.ProgressChanged += Reporter;
            bgw.RunWorkerAsync();
        }

        void Reset_uid(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("nfc-bin/nfc-mfsetuid.exe")
            {
                Arguments = "-f " + cardid + "2B0804006263646566676869",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            BackgroundWorker b = (BackgroundWorker)sender;
            process = Process.Start(psi);
            process.OutputDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            process.ErrorDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                b.ReportProgress(4);
            }
            else
            {
                b.ReportProgress(40);
            }
        }

        private void GetCard()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(W_getcard);
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Reporter;
            worker.RunWorkerAsync();
        }

        void W_getcard(object sender, DoWorkEventArgs e)
        {
            string workFile = "work.dump";
            ProcessStartInfo psi = new ProcessStartInfo("nfc-bin/mfoc.exe")
            {
                Arguments = "-O " + workFile,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            BackgroundWorker b = (BackgroundWorker)sender;
            process = Process.Start(psi);
            process.OutputDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            process.ErrorDataReceived += (s, _e) => b.ReportProgress(0, _e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                b.ReportProgress(2);
            }
            else
            {
                b.ReportProgress(20);
            }
        }
    }
}
