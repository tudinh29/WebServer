namespace FormManageService
{
    partial class fmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenFolderDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.tbLogService = new System.Windows.Forms.TextBox();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.btnBuildDaily = new System.Windows.Forms.Button();
            this.btnMonthly = new System.Windows.Forms.Button();
            this.btnQuarterly = new System.Windows.Forms.Button();
            this.btnYearly = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblServer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenFolderDialog
            // 
            this.OpenFolderDialog.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Status Service";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(134, 45);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(41, 13);
            this.lbStatus.TabIndex = 4;
            this.lbStatus.Text = "Stoped";
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(293, 35);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 5;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // tbLogService
            // 
            this.tbLogService.Location = new System.Drawing.Point(12, 111);
            this.tbLogService.Multiline = true;
            this.tbLogService.Name = "tbLogService";
            this.tbLogService.Size = new System.Drawing.Size(581, 178);
            this.tbLogService.TabIndex = 6;
            // 
            // btnBuildDaily
            // 
            this.btnBuildDaily.Location = new System.Drawing.Point(211, 73);
            this.btnBuildDaily.Name = "btnBuildDaily";
            this.btnBuildDaily.Size = new System.Drawing.Size(75, 23);
            this.btnBuildDaily.TabIndex = 7;
            this.btnBuildDaily.Text = "Daily";
            this.btnBuildDaily.UseVisualStyleBackColor = true;
            this.btnBuildDaily.Click += new System.EventHandler(this.btnBuildDaily_Click);
            // 
            // btnMonthly
            // 
            this.btnMonthly.Location = new System.Drawing.Point(293, 73);
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.Size = new System.Drawing.Size(75, 23);
            this.btnMonthly.TabIndex = 8;
            this.btnMonthly.Text = "Monthly";
            this.btnMonthly.UseVisualStyleBackColor = true;
            this.btnMonthly.Click += new System.EventHandler(this.btnMonthly_Click);
            // 
            // btnQuarterly
            // 
            this.btnQuarterly.Location = new System.Drawing.Point(374, 73);
            this.btnQuarterly.Name = "btnQuarterly";
            this.btnQuarterly.Size = new System.Drawing.Size(75, 23);
            this.btnQuarterly.TabIndex = 9;
            this.btnQuarterly.Text = "Quarterly";
            this.btnQuarterly.UseVisualStyleBackColor = true;
            this.btnQuarterly.Click += new System.EventHandler(this.btnQuarterly_Click);
            // 
            // btnYearly
            // 
            this.btnYearly.Location = new System.Drawing.Point(455, 73);
            this.btnYearly.Name = "btnYearly";
            this.btnYearly.Size = new System.Drawing.Size(75, 23);
            this.btnYearly.TabIndex = 10;
            this.btnYearly.Text = "Yearly";
            this.btnYearly.UseVisualStyleBackColor = true;
            this.btnYearly.Click += new System.EventHandler(this.btnYearly_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(455, 30);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(108, 23);
            this.btnInsert.TabIndex = 11;
            this.btnInsert.Text = "Insert DP to Server";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Time run package:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(134, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(34, 13);
            this.lblTime.TabIndex = 13;
            this.lblTime.Text = "00:00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(549, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(304, 9);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(23, 13);
            this.lblServer.TabIndex = 16;
            this.lblServer.Text = "D:\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Server:";
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 301);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnYearly);
            this.Controls.Add(this.btnQuarterly);
            this.Controls.Add(this.btnMonthly);
            this.Controls.Add(this.btnBuildDaily);
            this.Controls.Add(this.tbLogService);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label2);
            this.Name = "fmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmMain_FormClosing);
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFolderDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.TextBox tbLogService;
        private System.ServiceProcess.ServiceController serviceController1;
        private System.Windows.Forms.Button btnBuildDaily;
        private System.Windows.Forms.Button btnMonthly;
        private System.Windows.Forms.Button btnQuarterly;
        private System.Windows.Forms.Button btnYearly;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label label4;
    }
}

