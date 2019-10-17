namespace Rainbow
{
    partial class MainF
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
            this.btnJYU = new System.Windows.Forms.Button();
            this.btnSEI = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnJYU
            // 
            this.btnJYU.Location = new System.Drawing.Point(28, 18);
            this.btnJYU.Name = "btnJYU";
            this.btnJYU.Size = new System.Drawing.Size(201, 23);
            this.btnJYU.TabIndex = 0;
            this.btnJYU.Text = "確定受注入力";
            this.btnJYU.UseVisualStyleBackColor = true;
            this.btnJYU.Click += new System.EventHandler(this.btnJYU_Click);
            // 
            // btnSEI
            // 
            this.btnSEI.Location = new System.Drawing.Point(28, 48);
            this.btnSEI.Name = "btnSEI";
            this.btnSEI.Size = new System.Drawing.Size(201, 23);
            this.btnSEI.TabIndex = 1;
            this.btnSEI.Text = "生産予定入力";
            this.btnSEI.UseVisualStyleBackColor = true;
            this.btnSEI.Click += new System.EventHandler(this.btnSEI_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "設定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(28, 77);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(201, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "一覧確認";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(28, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(201, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "TEST QUERY";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 190);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSEI);
            this.Controls.Add(this.btnJYU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(50, 100);
            this.MaximizeBox = false;
            this.Name = "MainF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainF";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnJYU;
        private System.Windows.Forms.Button btnSEI;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}