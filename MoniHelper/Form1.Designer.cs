namespace MoniHelper
{
    partial class FormMaster
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaster));
            this.label1 = new System.Windows.Forms.Label();
            this.lDevName = new System.Windows.Forms.Label();
            this.checkBoxChkCard = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBoxWriteBand = new System.Windows.Forms.CheckBox();
            this.checkBoxEmuUID = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteUID = new System.Windows.Forms.CheckBox();
            this.checkBoxGetData = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDo = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名字";
            // 
            // lDevName
            // 
            this.lDevName.AutoSize = true;
            this.lDevName.Location = new System.Drawing.Point(69, 35);
            this.lDevName.Name = "lDevName";
            this.lDevName.Size = new System.Drawing.Size(29, 12);
            this.lDevName.TabIndex = 1;
            this.lDevName.Text = "----";
            // 
            // checkBoxChkCard
            // 
            this.checkBoxChkCard.AutoCheck = false;
            this.checkBoxChkCard.AutoSize = true;
            this.checkBoxChkCard.Location = new System.Drawing.Point(6, 36);
            this.checkBoxChkCard.Name = "checkBoxChkCard";
            this.checkBoxChkCard.Size = new System.Drawing.Size(72, 16);
            this.checkBoxChkCard.TabIndex = 2;
            this.checkBoxChkCard.Text = "检查原卡";
            this.checkBoxChkCard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.checkBoxWriteBand);
            this.groupBox1.Controls.Add(this.checkBoxEmuUID);
            this.groupBox1.Controls.Add(this.checkBoxWriteUID);
            this.groupBox1.Controls.Add(this.checkBoxGetData);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxChkCard);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 181);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 146);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(147, 23);
            this.progressBar1.Step = 20;
            this.progressBar1.TabIndex = 8;
            // 
            // checkBoxWriteBand
            // 
            this.checkBoxWriteBand.AutoCheck = false;
            this.checkBoxWriteBand.AutoSize = true;
            this.checkBoxWriteBand.Location = new System.Drawing.Point(6, 124);
            this.checkBoxWriteBand.Name = "checkBoxWriteBand";
            this.checkBoxWriteBand.Size = new System.Drawing.Size(84, 16);
            this.checkBoxWriteBand.TabIndex = 7;
            this.checkBoxWriteBand.Text = "刷写模拟卡";
            this.checkBoxWriteBand.UseVisualStyleBackColor = true;
            // 
            // checkBoxEmuUID
            // 
            this.checkBoxEmuUID.AutoCheck = false;
            this.checkBoxEmuUID.AutoSize = true;
            this.checkBoxEmuUID.Location = new System.Drawing.Point(6, 102);
            this.checkBoxEmuUID.Name = "checkBoxEmuUID";
            this.checkBoxEmuUID.Size = new System.Drawing.Size(78, 16);
            this.checkBoxEmuUID.TabIndex = 6;
            this.checkBoxEmuUID.Text = "模拟UID卡";
            this.checkBoxEmuUID.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteUID
            // 
            this.checkBoxWriteUID.AutoCheck = false;
            this.checkBoxWriteUID.AutoSize = true;
            this.checkBoxWriteUID.Location = new System.Drawing.Point(6, 80);
            this.checkBoxWriteUID.Name = "checkBoxWriteUID";
            this.checkBoxWriteUID.Size = new System.Drawing.Size(72, 16);
            this.checkBoxWriteUID.TabIndex = 5;
            this.checkBoxWriteUID.Text = "刷写卡号";
            this.checkBoxWriteUID.UseVisualStyleBackColor = true;
            // 
            // checkBoxGetData
            // 
            this.checkBoxGetData.AutoCheck = false;
            this.checkBoxGetData.AutoSize = true;
            this.checkBoxGetData.Location = new System.Drawing.Point(6, 58);
            this.checkBoxGetData.Name = "checkBoxGetData";
            this.checkBoxGetData.Size = new System.Drawing.Size(72, 16);
            this.checkBoxGetData.TabIndex = 4;
            this.checkBoxGetData.Text = "解析数据";
            this.checkBoxGetData.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "过程进度";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.buttonDo);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Location = new System.Drawing.Point(177, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 181);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "操作说明";
            // 
            // buttonDo
            // 
            this.buttonDo.Location = new System.Drawing.Point(252, 12);
            this.buttonDo.Name = "buttonDo";
            this.buttonDo.Size = new System.Drawing.Size(75, 23);
            this.buttonDo.TabIndex = 1;
            this.buttonDo.Text = "下一步";
            this.buttonDo.UseVisualStyleBackColor = true;
            this.buttonDo.Click += new System.EventHandler(this.ButtonDo_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(6, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(321, 139);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.ForeColor = System.Drawing.Color.Yellow;
            this.richTextBox2.Location = new System.Drawing.Point(12, 237);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(498, 127);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "++++++++++++日志输出++++++++++++\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(497, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "本软件仅用于学习研究用途，因不正当使用所造成任何后果与作者无关！由使用人自行负责！";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label5.Location = new System.Drawing.Point(175, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "需要使用：PN532读写器，UID卡，原门禁卡";
            // 
            // FormMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 376);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lDevName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMaster";
            this.Text = "模拟助手！ - BY RINSCR！";
            this.Load += new System.EventHandler(this.FormMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lDevName;
        private System.Windows.Forms.CheckBox checkBoxChkCard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBoxWriteBand;
        private System.Windows.Forms.CheckBox checkBoxEmuUID;
        private System.Windows.Forms.CheckBox checkBoxWriteUID;
        private System.Windows.Forms.CheckBox checkBoxGetData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

