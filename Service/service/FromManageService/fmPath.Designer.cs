namespace FromManageService
{
    partial class fmPath
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
            this.btOpen = new System.Windows.Forms.Button();
            this.tbPathFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PathFolder = new System.Windows.Forms.OpenFileDialog();
            this.btOkay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHour = new System.Windows.Forms.TextBox();
            this.tbMinute = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(367, 11);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(62, 20);
            this.btOpen.TabIndex = 5;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // tbPathFolder
            // 
            this.tbPathFolder.Cursor = System.Windows.Forms.Cursors.No;
            this.tbPathFolder.Location = new System.Drawing.Point(84, 12);
            this.tbPathFolder.Name = "tbPathFolder";
            this.tbPathFolder.Size = new System.Drawing.Size(260, 20);
            this.tbPathFolder.TabIndex = 4;
            this.tbPathFolder.Text = "D:\\Server";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Folder Monitor";
            // 
            // PathFolder
            // 
            this.PathFolder.FileName = "openFileDialog1";
            // 
            // btOkay
            // 
            this.btOkay.Location = new System.Drawing.Point(179, 99);
            this.btOkay.Name = "btOkay";
            this.btOkay.Size = new System.Drawing.Size(75, 23);
            this.btOkay.TabIndex = 6;
            this.btOkay.Text = "OK";
            this.btOkay.UseVisualStyleBackColor = true;
            this.btOkay.Click += new System.EventHandler(this.btOkay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Set Up Time";

            // 
            // tbHour
            // 
            this.tbHour.Location = new System.Drawing.Point(84, 48);
            this.tbHour.Name = "tbHour";
            this.tbHour.Size = new System.Drawing.Size(46, 20);
            this.tbHour.TabIndex = 8;
            // 
            // tbMinute
            // 
            this.tbMinute.Location = new System.Drawing.Point(152, 48);
            this.tbMinute.Name = "tbMinute";
            this.tbMinute.Size = new System.Drawing.Size(46, 20);
            this.tbMinute.TabIndex = 11;
            
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "HH:MM";
            // 
            // fmPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 144);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMinute);
            this.Controls.Add(this.tbHour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btOkay);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.tbPathFolder);
            this.Controls.Add(this.label1);
            this.Name = "fmPath";
            this.Text = "fmPath";
            this.Load += new System.EventHandler(this.fmPath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.TextBox tbPathFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog PathFolder;
        private System.Windows.Forms.Button btOkay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHour;
        private System.Windows.Forms.TextBox tbMinute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}