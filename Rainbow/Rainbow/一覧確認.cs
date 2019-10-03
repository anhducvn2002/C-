using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace Rainbow
{
    public partial class 一覧確認 : Form
    {
        public 一覧確認()
        {
            InitializeComponent();
        }

        private void 一覧確認_Load(object sender, EventArgs e)
        {
            cbxJyuchyuu.SelectedIndex = 1;
        }

        private void cbxJyuchyuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvIchiran.DataSource = null;
            string strTable = "";

            if (cbxJyuchyuu.SelectedItem.ToString().Equals("確定受注"))
            {
                strTable = " CYUMNDF.DBF";
            }
            else
            {
                strTable = " SEIYODF.DBF";
            }
            
            string strcon = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;";
            string sqlINS = "SELECT CYUNO,ZUBAN,SNAME,KISYU,TANKA,CYUSU,CYUDT,NOUKI,TOKCD,NOUCD FROM " + strTable + " WHERE SFLG IS NULL";

            DataTable dtResult = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(strcon))
            {
                OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dtResult);

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    string date = dtResult.Rows[i]["NOUKI"].ToString();
                    string nam = date.Substring(0, 4);
                    string thang = date.Substring(4, 2);
                    string ngay = date.Substring(6, 2);
                    string datenew = nam + "/" + thang + "/" + ngay;
                    dtResult.Rows[i]["NOUKI"] = datenew;

                    string date1 = dtResult.Rows[i]["CYUDT"].ToString();
                    string nam1 = date1.Substring(0, 4);
                    string thang1 = date1.Substring(4, 2);
                    string ngay1 = date1.Substring(6, 2);
                    string datenew1 = nam1 + "/" + thang1 + "/" + ngay1;
                    dtResult.Rows[i]["CYUDT"] = datenew1;

                    string tokcd = dtResult.Rows[i]["TOKCD"].ToString();
                    string tok = "00" + tokcd;
                    string name = getName(tok);
                    dtResult.Rows[i]["TOKCD"] = tokcd + "-" + name;


                    string noucd = dtResult.Rows[i]["NOUCD"].ToString();
                    string fac = "00" +noucd;
                    string namefac = getFac(tok,fac);
                    dtResult.Rows[i]["NOUCD"] = noucd + "-" + namefac;
                }
                
                if (dgvIchiran.Columns.Contains("削除")==false)
                {
                    DataGridViewButtonColumn column = new DataGridViewButtonColumn();                  
                    column.Name = "削除";                 
                    column.UseColumnTextForButtonValue = true;
                    column.Text = "削除";
                    dgvIchiran.Columns.Add(column);
                }
                dgvIchiran.DataSource = dtResult;
              
                cmd.Dispose();
                adapter.Dispose();
                conn.Close();

                dgvIchiran.Columns[1].HeaderText = "受注番号";
                dgvIchiran.Columns[2].HeaderText = "製品図番";
                dgvIchiran.Columns[3].HeaderText = "機種";
                dgvIchiran.Columns[4].HeaderText = "品名";
                dgvIchiran.Columns[6].HeaderText = "数量";
                dgvIchiran.Columns[5].HeaderText = "単価";
                dgvIchiran.Columns[7].HeaderText = "受注日";
                dgvIchiran.Columns[9].HeaderText = "得意先";
                dgvIchiran.Columns[10].HeaderText = "納入場所";
                if (cbxJyuchyuu.SelectedItem.ToString().Equals("確定受注"))
                {
                    dgvIchiran.Columns[8].HeaderText = "受注納期";
                }
                else
                {
                    dgvIchiran.Columns[8].HeaderText = "自社納入日";
                }
               

                this.dgvIchiran.Columns["TOKCD"].Width = 200;
                this.dgvIchiran.Columns["KISYU"].Width = 67;
                this.dgvIchiran.Columns["削除"].Width = 80;
            }
        }

        public string getName(string tok)
        {
            string name = "";
        
            string FUJIConString = "Data Source=" + Properties.Settings.Default.FUJIServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";


            ///TOK 得意先の情報を取得する
            DataTable dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(FUJIConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT NAME FROM M0200 WHERE TOKCD='" + tok + "'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }
            name = dtResult.Rows[0]["NAME"].ToString();
            return name;
        }

        public string getFac(string tok,string nou)
        {
            string fac = "";

            string FUJIConString = "Data Source=" + Properties.Settings.Default.FUJIServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";


            ///TOK 得意先の情報を取得する
            DataTable dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(FUJIConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT NAME FROM M0220 WHERE NOUCD='" + nou + "' and TOKCD='" + tok + "'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }
            fac = dtResult.Rows[0]["NAME"].ToString();
            return fac;
        }
        /// <summary>
        /// 使ってない
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string cyuno = dgvIchiran.Rows[e.RowIndex].Cells[1].Value.ToString();
            string zuban = dgvIchiran.Rows[e.RowIndex].Cells[2].Value.ToString();
            string sname = dgvIchiran.Rows[e.RowIndex].Cells[3].Value.ToString();
            string kisyu = dgvIchiran.Rows[e.RowIndex].Cells[4].Value.ToString();
            string tanka = dgvIchiran.Rows[e.RowIndex].Cells[5].Value.ToString();
            string cyusu = dgvIchiran.Rows[e.RowIndex].Cells[6].Value.ToString();
            string cyudt = dgvIchiran.Rows[e.RowIndex].Cells[7].Value.ToString();
            string nouki = dgvIchiran.Rows[e.RowIndex].Cells[8].Value.ToString();
            string tokcd = dgvIchiran.Rows[e.RowIndex].Cells[9].Value.ToString();
            string noucd = dgvIchiran.Rows[e.RowIndex].Cells[10].Value.ToString();
            string nounyu = "19850726";

            string nam = nouki.Substring(0, 4);
            string thang = nouki.Substring(5, 2);
            string ngay = nouki.Substring(8, 2);
            nouki = nam + thang + ngay;

            string nam1 = cyudt.Substring(0, 4);
            string thang1 = cyudt.Substring(5, 2);
            string ngay1 = cyudt.Substring(8, 2);
            cyudt = nam1 + thang1 + ngay1;

            tokcd = tokcd.Substring(0, 3);

            noucd = noucd.Substring(0, 3);
            if (cbxJyuchyuu.SelectedItem.ToString().Equals("確定受注"))
            {
                if (Application.OpenForms.Cast<Form>().Any(form => form.Name == "確定受注入力"))
                {
                    確定受注入力 f1 = (確定受注入力)Application.OpenForms["確定受注入力"];
                    f1.Show();
                    f1.Activate();
                    f1.updateRecord(cyuno, zuban, sname, kisyu, tanka, cyusu, cyudt, nouki, nounyu, tokcd, noucd);
                }
                else
                {
                    確定受注入力 f1 = new 確定受注入力();
                    f1.Show();
                    f1.updateRecord(cyuno, zuban, sname, kisyu, tanka, cyusu, cyudt, nouki, nounyu, tokcd, noucd);
                }
            }
            else
            {
                if (Application.OpenForms.Cast<Form>().Any(form => form.Name == "生産予定入力"))
                {
                    生産予定入力 f1 = (生産予定入力)Application.OpenForms["生産予定入力"];
                    f1.Show();
                    f1.Activate();
                    f1.updateRecord(zuban, sname, kisyu, tanka, cyusu, cyudt, nounyu, tokcd, noucd);
                }
                else
                {
                    生産予定入力 f1 = new 生産予定入力();
                    f1.Show();
                    f1.updateRecord(zuban, sname, kisyu, tanka, cyusu, cyudt, nounyu, tokcd, noucd);
                }
            }


        }

        private void dgvIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvIchiran.Columns[e.ColumnIndex].Name == "削除")
            {

                string cyuno = dgvIchiran.Rows[e.RowIndex].Cells[1].Value.ToString();
                string zuban = dgvIchiran.Rows[e.RowIndex].Cells[2].Value.ToString();
                string sname = dgvIchiran.Rows[e.RowIndex].Cells[3].Value.ToString();
                string kisyu = dgvIchiran.Rows[e.RowIndex].Cells[4].Value.ToString();
                string tanka = dgvIchiran.Rows[e.RowIndex].Cells[5].Value.ToString();
                string cyusu = dgvIchiran.Rows[e.RowIndex].Cells[6].Value.ToString();
                string cyudt = dgvIchiran.Rows[e.RowIndex].Cells[7].Value.ToString();
                string nouki = dgvIchiran.Rows[e.RowIndex].Cells[8].Value.ToString();
                string tokcd = dgvIchiran.Rows[e.RowIndex].Cells[9].Value.ToString();
                string noucd = dgvIchiran.Rows[e.RowIndex].Cells[10].Value.ToString();

                string nam = nouki.Substring(0, 4);
                string thang = nouki.Substring(5, 2);
                string ngay = nouki.Substring(8, 2);
                nouki = nam + thang + ngay;

                string nam1 = cyudt.Substring(0, 4);
                string thang1 = cyudt.Substring(5, 2);
                string ngay1 = cyudt.Substring(8, 2);
                cyudt = nam1 + thang1 + ngay1;

                tokcd = tokcd.Substring(0, 3);

                noucd = noucd.Substring(0, 3);
                //Delete
                ///確定受注を

                if (cbxJyuchyuu.SelectedItem.ToString().Equals("確定受注"))
                {
                    string sqlINS = "";
                    string sqlINS1 = "";
                    if (tokcd == "001")
                    {
                        if (cyuno == "")
                        {
                            sqlINS = "delete from CYUMNDF.DBF  where  ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                            sqlINS1 = "delete from SEIYODF.DBF  where  ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and ( SFLG IS NULL ) ";
                            OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                            conn.Open();
                            OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                            cmd.ExecuteNonQuery();
                            OleDbCommand cmd1 = new OleDbCommand(sqlINS1, conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            sqlINS = "delete from CYUMNDF.DBF  where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                            sqlINS1 = "delete from SEIYODF.DBF  where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and ( SFLG IS NULL ) ";
                            OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                            conn.Open();
                            OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                            cmd.ExecuteNonQuery();
                            OleDbCommand cmd1 = new OleDbCommand(sqlINS1, conn);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                      
                    }
                    else
                    {
                        if (cyuno == "")
                        {

                            sqlINS = "delete from  CYUMNDF.DBF  where ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "' and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";

                        }
                        else
                        {

                            sqlINS = "delete from  CYUMNDF.DBF  where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                        }
                        OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                else
                {
                    string sqlINS = "";
                    string sqlINS1 = "";
                   
                    if (tokcd == "001")
                    {
                       
                        if (test(cyuno, zuban, tokcd, cyudt) == true)
                        {
                            if (cyuno == "")
                            {
                                sqlINS = "delete from SEIYODF.DBF  where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                                sqlINS1 = "delete from CYUMNDF.DBF  where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and ( SFLG IS NULL ) ";
                                OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                                conn.Open();
                                OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                                cmd.ExecuteNonQuery();
                                OleDbCommand cmd1 = new OleDbCommand(sqlINS1, conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                sqlINS = "delete from SEIYODF.DBF  where  ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                                sqlINS1 = "delete from CYUMNDF.DBF  where ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and ( SFLG IS NULL ) ";
                                OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                                conn.Open();
                                OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                                cmd.ExecuteNonQuery();
                                OleDbCommand cmd1 = new OleDbCommand(sqlINS1, conn);
                                cmd1.ExecuteNonQuery();
                                conn.Close();
                            }
                            
                        }
                        else
                        {
                           
                            if (cyuno == "")
                            {

                                sqlINS = "delete from SEIYODF.DBF  where ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "' and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";

                            }
                            else
                            {

                                sqlINS = "delete from SEIYODF.DBF  where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                            }
                            OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                            conn.Open();
                            OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                      
                    }
                    else
                    {
                        if (cyuno == "")
                        {

                            sqlINS = "delete from SEIYODF.DBF  where ZUBAN='"+zuban+"'and TOKCD='"+tokcd+"' and CYUDT='"+cyudt+"' and NOUKI='"+nouki+"' and ( SFLG IS NULL ) ";
                      
                        }
                        else
                        {
                            
                            sqlINS = "delete from SEIYODF.DBF  where CYUNO='"+cyuno+"' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and NOUKI='" + nouki + "' and ( SFLG IS NULL ) ";
                        }
                        OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                   

                }
                
            }
            cbxJyuchyuu_SelectedIndexChanged(sender, e);
        }

        public Boolean test(string cyuno,string zuban , string tokcd,string cyudt)
        {
            string strcon = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;";
            string sqlINS = "SELECT CYUNO,ZUBAN,SNAME,KISYU,TANKA,CYUSU,CYUDT,NOUKI,TOKCD,NOUCD FROM CYUMNDF.DBF where CYUNO='" + cyuno + "' and ZUBAN='" + zuban + "'and TOKCD='" + tokcd + "'  and CYUDT='" + cyudt + "' and ( SFLG IS NULL ) ";
            DataTable dtResult = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(strcon))
            {
                OleDbCommand cmd = new OleDbCommand(sqlINS, conn);
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dtResult);
               


                if (dtResult.Rows.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbxJyuchyuu_SelectedIndexChanged(sender, e);
        }
    }
}
