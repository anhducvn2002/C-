using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Rainbow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();

            string ConString = "Data Source=10.121.21.15;Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand(richTextBox1.Text, con);
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                dtResult = new DataTable();
                DataColumn dc;
                dc = new DataColumn("選択", typeof(bool));
                dtResult.Columns.Add(dc);
                ada.Fill(dtResult);

                //DataColumn dc;
                //dc = new DataColumn("Column1", typeof(bool));
                //DataTableへ追加
                //dtResult.Columns.Add(dc);
                con.Close();
            }

            dataGridView1.DataSource = dtResult;

            dataGridView1.Columns["選択V"].Visible = false;
            dataGridView1.Columns["SEICD_OTA"].Visible = false;
            dataGridView1.Columns["HNAME"].Visible = false;
            //dataGridView1.Columns["ZUBAN_OTA"].Visible = false;
            dataGridView1.Columns["KFLG"].Visible = false;
            dataGridView1.Columns["TOKCD"].Visible = false;
            dataGridView1.Columns["TNAME"].Visible = false;
            dataGridView1.Columns["NOUCD"].Visible = false;
            dataGridView1.Columns["NNAME"].Visible = false;
            dataGridView1.Columns["SEICD"].Visible = false;
            //dataGridView1.Columns["ZUBAN"].Visible = false;
            

            dataGridView1.Columns["選択V"].Visible = false;
            dataGridView1.AllowUserToAddRows = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ZUBAN"].FormattedValue.ToString() != "")
                {
                    row.Cells["選択"].ReadOnly = false;
                    if (row.Cells["RN"].FormattedValue.ToString() == "1")
                        row.Cells["選択"].Value = true;
                }
                else
                {
                    row.Cells["選択"].ReadOnly = true;
                }
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //F1キーが押されたか調べる
            if (e.KeyData == Keys.F5)
            {
                button1_Click_1(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int maxCYU, maxSEI, insertRow;
            string TestConString = "Data Source=" + Properties.Settings.Default.TestServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";

            DataTable dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(TestConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SEICD FROM D1000 WHERE JYUNO='000000'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }
            maxCYU = Int32.Parse(dtResult.Rows[0]["SEICD"].ToString());

            dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(TestConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ZAICD FROM D1100 WHERE SIYNO='000000'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }
            maxSEI = Int32.Parse(dtResult.Rows[0]["ZAICD"].ToString());

            insertRow = dataGridView1.Rows.Count - 1;

            using (SqlConnection connection = new SqlConnection(TestConString))
            {
                SqlCommand command = new SqlCommand("UPDATE D1000 SET SEICD='" + (maxCYU + insertRow) + "' WHERE JYUNO='000000'", connection);
                //SqlCommand command = new SqlCommand("UPDATE D1000 SET SEICD='445119' WHERE JYUNO='000000'", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            using (SqlConnection connection = new SqlConnection(TestConString))
            {
                SqlCommand command = new SqlCommand("UPDATE D1100 SET ZAICD='" + (maxSEI + insertRow) + "' WHERE SIYNO='000000'", connection);
                //SqlCommand command = new SqlCommand("UPDATE D1100 SET ZAICD='462331' WHERE SIYNO='000000'", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            string tokcd, tname, noucd, nname, seicd, zuban, name, kisyu, jyusu, jtanka, nouki, ydate, cyuno;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                maxCYU++; maxSEI++;

                tokcd = row.Cells["TOKCD"].FormattedValue.ToString().Trim();
                tname = row.Cells["TNAME"].FormattedValue.ToString().Trim();
                noucd = row.Cells["NOUCD"].FormattedValue.ToString().Trim();
                nname = row.Cells["NNAME"].FormattedValue.ToString().Trim();
                seicd = row.Cells["SEICD"].FormattedValue.ToString().Trim();
                zuban = row.Cells["ZUBAN"].FormattedValue.ToString().Trim();
                name = row.Cells["NAME"].FormattedValue.ToString().Trim();
                kisyu = row.Cells["KISYU"].FormattedValue.ToString().Trim();
                jyusu = row.Cells["JYUSU"].FormattedValue.ToString().Trim();
                jtanka = row.Cells["JTANKA"].FormattedValue.ToString().Trim();
                nouki = "20191020";// row.Cells["NOUKI"].FormattedValue.ToString().Trim();
                ydate = "20191020";// row.Cells["YDATE"].FormattedValue.ToString().Trim();
                cyuno = row.Cells["CYUNO"].FormattedValue.ToString().Trim();

                if (row.Cells["CYUNO"].FormattedValue.ToString() == "")
                    return;

                //確定受注
                string insertSQL = "INSERT INTO D1000 VALUES " +
                    "('" + maxCYU + "','      ','" + tokcd + "','" + tname + "','" + noucd + "','" + nname + "'," +
                    "'" + seicd + "','" + zuban + "','" + name + "','" + kisyu + "','" + jyusu + "','" + jtanka + "'," +
                    "'2003" + nouki.Substring(4, 4) + "','2003" + ydate.Substring(4, 4) + "'," +
                    "'999','182','" + cyuno + "','  ','                              ','                              ','0'" +
                    ",' ',' ',' ',0,0," +
                    "'2003" + DateTime.Now.ToString("MMdd") + "')";

                using (SqlConnection connection = new SqlConnection(TestConString))
                {
                    SqlCommand command = new SqlCommand(insertSQL, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                //生産予定
                insertSQL = "INSERT INTO D1100 VALUES (" +
                    "'" + maxSEI + "','0','" + tokcd + "','" + tname + "','0'," +
                    "'" + seicd + "','" + zuban + "','" + name + "','" + kisyu + "','" + jyusu + "','" + jtanka + "'," +
                    "'" + nouki + "','                    ','" + jyusu + "','0','1','2003" + DateTime.Now.ToString("MMdd") + "')";

                using (SqlConnection connection = new SqlConnection(TestConString))
                {
                    SqlCommand command = new SqlCommand(insertSQL, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                //D1010
                insertSQL = "INSERT INTO D1010 VALUES ('" + maxCYU + "','" + maxSEI + "','" + nouki + "','" + jyusu + "','0')";
                using (SqlConnection connection = new SqlConnection(TestConString))
                {
                    SqlCommand command = new SqlCommand(insertSQL, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Columns.Contains("選択") == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ZUBAN"].FormattedValue.ToString() != "")
                    {
                        row.Cells["選択"].ReadOnly = false;
                    }
                    else
                    {
                        row.Cells["選択"].ReadOnly = true;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

            if (dataGridView1.Columns[e.ColumnIndex].Name == "選択")
            {
                string hacno = dataGridView1.Rows[e.RowIndex].Cells["HACNO"].Value.ToString();
                bool result;

                if (dataGridView1.Rows[e.RowIndex].Cells["選択"].ReadOnly == true) return;

                result = bool.Parse(dataGridView1.Rows[e.RowIndex].Cells["選択V"].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells["選択V"].Value = !result;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["HACNO"].Value.ToString() == hacno)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            row.Cells["選択"].Value = false;
                            row.Cells["選択V"].Value = "False";
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "選択")
            {
                string hacno = dataGridView1.Rows[e.RowIndex].Cells["HACNO"].Value.ToString();
                bool result;

                if (dataGridView1.Rows[e.RowIndex].Cells["選択"].ReadOnly == true) return;

                result = bool.Parse(dataGridView1.Rows[e.RowIndex].Cells["選択V"].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells["選択V"].Value = !result;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["HACNO"].Value.ToString() == hacno)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            row.Cells["選択"].Value = false;
                            row.Cells["選択V"].Value = "False";
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int today = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            DataTable dt = new DataTable();

            dt = (DataTable)dataGridView1.DataSource;

            DataRow[] rows;
            rows = dt.Select("選択V = 'False'");      

            foreach (DataRow row in rows)
            {
                dt.Rows.Remove(row);
            }
            //DataColumn dc;
            //dc = new DataColumn("Result", typeof(string));
            if (!dt.Columns.Contains("Result"))
                dt.Columns.Add("Result").SetOrdinal(0);


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["選択V"].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int today = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            DataTable dt = new DataTable();  
            dt = (DataTable)dataGridView1.DataSource;

            //Insert result column
            if (!dt.Columns.Contains("Result"))
                dt.Columns.Add("Result").SetOrdinal(1);

            DataRow[] rows = dt.Select("選択V = 'True'");

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            


            int maxCYU, maxSEI, insertRow;
            string TestConString = "Data Source=" + Properties.Settings.Default.TestServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";

            DataTable dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(TestConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SEICD FROM D1000 WHERE JYUNO='000000'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }
            maxCYU = Int32.Parse(dtResult.Rows[0]["SEICD"].ToString());

            dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(TestConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ZAICD FROM D1100 WHERE SIYNO='000000'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }
            maxSEI = Int32.Parse(dtResult.Rows[0]["ZAICD"].ToString());

            insertRow = rows.Count();

            using (SqlConnection connection = new SqlConnection(TestConString))
            {
                SqlCommand command = new SqlCommand("UPDATE D1000 SET SEICD='" + (maxCYU + insertRow) + "' WHERE JYUNO='000000'", connection);
                //SqlCommand command = new SqlCommand("UPDATE D1000 SET SEICD='445119' WHERE JYUNO='000000'", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            using (SqlConnection connection = new SqlConnection(TestConString))
            {
                SqlCommand command = new SqlCommand("UPDATE D1100 SET ZAICD='" + (maxSEI + insertRow) + "' WHERE SIYNO='000000'", connection);
                //SqlCommand command = new SqlCommand("UPDATE D1100 SET ZAICD='462331' WHERE SIYNO='000000'", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            string tokcd, tname, noucd, nname, seicd, zuban, name, kisyu, jyusu, jtanka, nouki, ydate, cyuno;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            { 
                if (row.Cells["選択V"].Value.ToString() == "True")
                {
                    maxCYU++; maxSEI++;
                    tokcd = row.Cells["TOKCD"].FormattedValue.ToString().Trim();
                    tname = row.Cells["TNAME"].FormattedValue.ToString().Trim();
                    noucd = row.Cells["NOUCD"].FormattedValue.ToString().Trim();
                    nname = row.Cells["NNAME"].FormattedValue.ToString().Trim();
                    seicd = row.Cells["SEICD"].FormattedValue.ToString().Trim();
                    zuban = row.Cells["ZUBAN"].FormattedValue.ToString().Trim();
                    name = row.Cells["NAME"].FormattedValue.ToString().Trim();
                    kisyu = row.Cells["KISYU"].FormattedValue.ToString().Trim();
                    jyusu = row.Cells["JYUSU"].FormattedValue.ToString().Trim();
                    jtanka = row.Cells["JTANKA"].FormattedValue.ToString().Trim();
                    nouki = "20191020";// row.Cells["NOUKI"].FormattedValue.ToString().Trim();
                    ydate = "20191020";// row.Cells["YDATE"].FormattedValue.ToString().Trim();
                    cyuno = row.Cells["CYUNO"].FormattedValue.ToString().Trim();


                    //確定受注
                    string insertSQL = "INSERT INTO D1000 VALUES " +
                        "('" + maxCYU + "','      ','" + tokcd + "','" + tname + "','" + noucd + "','" + nname + "'," +
                        "'" + seicd + "','" + zuban + "','" + name + "','" + kisyu + "','" + jyusu + "','" + jtanka + "'," +
                        "'2003" + nouki.Substring(4, 4) + "','2003" + ydate.Substring(4, 4) + "'," +
                        "'999','182','" + cyuno + "','  ','                              ','                              ','0'" +
                        ",' ',' ',' ',0,0," +
                        "'2003" + DateTime.Now.ToString("MMdd") + "')";

                    using (SqlConnection connection = new SqlConnection(TestConString))
                    {
                        SqlCommand command = new SqlCommand(insertSQL, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }

                    //生産予定
                    insertSQL = "INSERT INTO D1100 VALUES (" +
                        "'" + maxSEI + "','0','" + tokcd + "','" + tname + "','0'," +
                        "'" + seicd + "','" + zuban + "','" + name + "','" + kisyu + "','" + jyusu + "','" + jtanka + "'," +
                        "'" + nouki + "','                    ','" + jyusu + "','0','1','2003" + DateTime.Now.ToString("MMdd") + "')";

                    using (SqlConnection connection = new SqlConnection(TestConString))
                    {
                        SqlCommand command = new SqlCommand(insertSQL, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }

                    //D1010
                    insertSQL = "INSERT INTO D1010 VALUES ('" + maxCYU + "','" + maxSEI + "','" + nouki + "','" + jyusu + "','0')";
                    using (SqlConnection connection = new SqlConnection(TestConString))
                    {
                        SqlCommand command = new SqlCommand(insertSQL, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }

                    row.Cells["Result"].Value = "OK";
                }
                else row.Cells["Result"].Value = "";

            }
            dataGridView1.Sort(dataGridView1.Columns["HACNO"], ListSortDirection.Descending);
            dataGridView1.Sort(dataGridView1.Columns["HACNO"], ListSortDirection.Ascending);
            dataGridView1.Columns["選択V"].Visible = false;
            dataGridView1.Columns["SEICD_OTA"].Visible = false;
            dataGridView1.Columns["HNAME"].Visible = false;
            //dataGridView1.Columns["ZUBAN_OTA"].Visible = false;
            dataGridView1.Columns["KFLG"].Visible = false;
            dataGridView1.Columns["TOKCD"].Visible = false;
            dataGridView1.Columns["TNAME"].Visible = false;
            dataGridView1.Columns["NOUCD"].Visible = false;
            dataGridView1.Columns["NNAME"].Visible = false;
            dataGridView1.Columns["SEICD"].Visible = false;
            //dataGridView1.Columns["ZUBAN"].Visible = false;
        }
    }
}