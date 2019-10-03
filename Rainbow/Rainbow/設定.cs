using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Rainbow
{
    public partial class 設定 : Form
    {
        public 設定()
        {
            InitializeComponent();
        }

        private void 設定_Load(object sender, EventArgs e)
        {
            lblFolder.Text = "";
            lblServer1.Text = "";
            lblServer2.Text = "";

            txtFolder.Text = Properties.Settings.Default.DBFFolder;
            txtServer1.Text = Properties.Settings.Default.FUJIServer;
            txtServer2.Text = Properties.Settings.Default.OTAServer;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.InitialDirectory = txtFolder.Text;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Database Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                txtFolder.Text = folderPath;
                lblFolder.Text = "";
                if (!File.Exists(folderPath + @"\CYUMNDF.DBF") && !File.Exists(folderPath + @"\SEIYODF.DBF"))
                {
                   lblFolder.Text = "*ここに　[CYUMNDF.DBF] & [SEIYODF.DBF]　が存在しません。";
                }
                else
                {
                    if (!File.Exists(folderPath + @"\CYUMNDF.DBF")) lblFolder.Text = "*ここに　[CYUMNDF.DBF]　が存在しません。";
                    if (!File.Exists(folderPath + @"\SEIYODF.DBF")) lblFolder.Text = "*ここに　[SEIYODF.DBF]　が存在しません。";
                }
            }

            checkBtnOK();
        }

        private void txtServer1_Leave(object sender, EventArgs e)
        {
            string SQLCon = "Data Source=" + txtServer1.Text + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;Connection Timeout=1";

            using (SqlConnection con = new SqlConnection(SQLCon))
            {
                try
                {
                    con.Open();
                    con.Close();
                    lblServer1.Text = "";
                }
                catch { lblServer1.Text = "* [" + txtServer1.Text + "] に接続できませんでした。"; }
            }

            checkBtnOK();
        }

        private void txtServer2_Leave(object sender, EventArgs e)
        {
            string SQLCon = "Data Source=" + txtServer2.Text + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;Connection Timeout=1";

            using (SqlConnection con = new SqlConnection(SQLCon))
            {
                try
                {
                    con.Open();
                    con.Close();
                    lblServer2.Text = "";
                }
                catch { lblServer2.Text = "* ["+txtServer2.Text+"] に接続できませんでした。"; }
            }

            checkBtnOK();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DBFFolder = txtFolder.Text;
            Properties.Settings.Default.FUJIServer = txtServer1.Text;
            Properties.Settings.Default.OTAServer = txtServer2.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBtnOK()
        {
            if (lblFolder.Text != "" || lblServer1.Text != "" || lblServer2.Text != "")
            {
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
            }
        }
    }
}
