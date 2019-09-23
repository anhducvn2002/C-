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
            this.button1.Location = new System.Drawing.Point(28, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "設定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 126);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSEI);
            this.Controls.Add(this.btnJYU);
            this.Name = "MainF";
            this.Text = "MainF";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnJYU;
        private System.Windows.Forms.Button btnSEI;
        private System.Windows.Forms.Button button1;
    }
}