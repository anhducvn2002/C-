namespace Rainbow
{
    partial class 設定
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
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txtServer1 = new System.Windows.Forms.TextBox();
            this.txtServer2 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSer1 = new System.Windows.Forms.Label();
            this.lblSer2 = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.lblServer2 = new System.Windows.Forms.Label();
            this.lblServer1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(23, 13);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(106, 23);
            this.btnFolder.TabIndex = 0;
            this.btnFolder.Text = "DBFフォルダー";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(146, 15);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(211, 19);
            this.txtFolder.TabIndex = 3;
            // 
            // txtServer1
            // 
            this.txtServer1.Location = new System.Drawing.Point(146, 64);
            this.txtServer1.Name = "txtServer1";
            this.txtServer1.Size = new System.Drawing.Size(211, 19);
            this.txtServer1.TabIndex = 4;
            this.txtServer1.Leave += new System.EventHandler(this.txtServer1_Leave);
            // 
            // txtServer2
            // 
            this.txtServer2.Location = new System.Drawing.Point(146, 107);
            this.txtServer2.Name = "txtServer2";
            this.txtServer2.Size = new System.Drawing.Size(211, 19);
            this.txtServer2.TabIndex = 6;
            this.txtServer2.Leave += new System.EventHandler(this.txtServer2_Leave);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(159, 147);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSer1
            // 
            this.lblSer1.AutoSize = true;
            this.lblSer1.Location = new System.Drawing.Point(40, 67);
            this.lblSer1.Name = "lblSer1";
            this.lblSer1.Size = new System.Drawing.Size(89, 12);
            this.lblSer1.TabIndex = 9;
            this.lblSer1.Text = "自拠点サーバー１";
            // 
            // lblSer2
            // 
            this.lblSer2.AutoSize = true;
            this.lblSer2.Location = new System.Drawing.Point(28, 110);
            this.lblSer2.Name = "lblSer2";
            this.lblSer2.Size = new System.Drawing.Size(101, 12);
            this.lblSer2.TabIndex = 10;
            this.lblSer2.Text = "依頼拠点サーバー２";
            // 
            // lblFolder
            // 
            this.lblFolder.ForeColor = System.Drawing.Color.Red;
            this.lblFolder.Location = new System.Drawing.Point(144, 36);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(213, 25);
            this.lblFolder.TabIndex = 11;
            this.lblFolder.Text = "Folder";
            // 
            // lblServer2
            // 
            this.lblServer2.ForeColor = System.Drawing.Color.Red;
            this.lblServer2.Location = new System.Drawing.Point(144, 127);
            this.lblServer2.Name = "lblServer2";
            this.lblServer2.Size = new System.Drawing.Size(213, 12);
            this.lblServer2.TabIndex = 12;
            this.lblServer2.Text = "Server2";
            // 
            // lblServer1
            // 
            this.lblServer1.ForeColor = System.Drawing.Color.Red;
            this.lblServer1.Location = new System.Drawing.Point(144, 85);
            this.lblServer1.Name = "lblServer1";
            this.lblServer1.Size = new System.Drawing.Size(213, 12);
            this.lblServer1.TabIndex = 13;
            this.lblServer1.Text = "Server1";
            // 
            // 設定
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 194);
            this.Controls.Add(this.lblServer1);
            this.Controls.Add(this.lblServer2);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.lblSer2);
            this.Controls.Add(this.lblSer1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtServer2);
            this.Controls.Add(this.txtServer1);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnFolder);
            this.Location = new System.Drawing.Point(305, 100);
            this.Name = "設定";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "設定";
            this.Load += new System.EventHandler(this.設定_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtServer1;
        private System.Windows.Forms.TextBox txtServer2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSer1;
        private System.Windows.Forms.Label lblSer2;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Label lblServer2;
        private System.Windows.Forms.Label lblServer1;
    }
}