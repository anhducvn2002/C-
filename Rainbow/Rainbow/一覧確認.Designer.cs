namespace Rainbow
{
    partial class 一覧確認
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
            this.cbxJyuchyuu = new System.Windows.Forms.ComboBox();
            this.dgvIchiran = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIchiran)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxJyuchyuu
            // 
            this.cbxJyuchyuu.FormattingEnabled = true;
            this.cbxJyuchyuu.Items.AddRange(new object[] {
            "確定受注",
            "生産予定"});
            this.cbxJyuchyuu.Location = new System.Drawing.Point(50, 57);
            this.cbxJyuchyuu.Name = "cbxJyuchyuu";
            this.cbxJyuchyuu.Size = new System.Drawing.Size(142, 20);
            this.cbxJyuchyuu.TabIndex = 0;
            this.cbxJyuchyuu.SelectedIndexChanged += new System.EventHandler(this.cbxJyuchyuu_SelectedIndexChanged);
            // 
            // dgvIchiran
            // 
            this.dgvIchiran.AllowUserToAddRows = false;
            this.dgvIchiran.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIchiran.Location = new System.Drawing.Point(12, 100);
            this.dgvIchiran.Name = "dgvIchiran";
            this.dgvIchiran.ReadOnly = true;
            this.dgvIchiran.RowTemplate.Height = 21;
            this.dgvIchiran.Size = new System.Drawing.Size(1210, 444);
            this.dgvIchiran.TabIndex = 1;
            this.dgvIchiran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIchiran_CellContentClick);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(70, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1074, 37);
            this.label9.TabIndex = 46;
            this.label9.Text = "一覧確認";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 47;
            this.label1.Text = "受注";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(233, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "リロード";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 一覧確認
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 556);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dgvIchiran);
            this.Controls.Add(this.cbxJyuchyuu);
            this.Location = new System.Drawing.Point(0, 320);
            this.MaximizeBox = false;
            this.Name = "一覧確認";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "一覧確認";
            this.Load += new System.EventHandler(this.一覧確認_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIchiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxJyuchyuu;
        private System.Windows.Forms.DataGridView dgvIchiran;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}