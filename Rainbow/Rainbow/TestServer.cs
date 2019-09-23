using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rainbow
{
    public partial class TestServer : Form
    {
        public TestServer()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //rtbSQL.Text = Clipboard.GetText();

            DataTable dtResult = new DataTable();
            string ConString = "Data Source=" + txtServer.Text + ";Initial Catalog=TESC;Persist Security Info=True;User ID=" + txtID.Text + ";Password=" + txtPass.Text;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand(rtbSQL.Text, con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }

            dgvResult.DataSource = dtResult;
        }
    }
}
