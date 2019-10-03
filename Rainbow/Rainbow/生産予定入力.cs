using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;


namespace Rainbow
{
    public partial class 生産予定入力 : Form
    {
        private static bool SEIFlag = false;
        private static bool BUFlag = false;
        private static bool getFlag = false;
        private static bool updateFlag = false;
        private static AutoCompleteStringCollection ZubanCollection = new AutoCompleteStringCollection();
        private static string currentSeihin = "";
        private static 図番マスター seihinForm;

        public 生産予定入力()
        {
            InitializeComponent();
            dtCYUDT.Value = DateTime.Today;
            dtNOUNYU.Value = DateTime.Today.AddDays(8);
        }

        private void 生産予定入力_Load(object sender, EventArgs e)
        {
            txtKISYU.TabStop = false;
            txtSNAME.TabStop = false;
            txtTANKA.TabStop = false;
            lblzuban.Text = "";

            string OTAConString = "Data Source=" + Properties.Settings.Default.OTAServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
            string FUJIConString = "Data Source=" + Properties.Settings.Default.FUJIServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";

            ///TOK 得意先の情報を取得する
            DataTable dtTOK = new DataTable();
            using (SqlConnection con = new SqlConnection(FUJIConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RIGHT(TOKCD,3) TOKCD,NAME FROM M0200", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtTOK);
                con.Close();
            }
            cbxTOK.DataSource = dtTOK;
            cbxTOK.DisplayMember = "NAME";
            cbxTOK.ValueMember = "NAME";

            cbxTOKCD.DataSource = dtTOK;
            cbxTOKCD.DisplayMember = "TOKCD";
            cbxTOKCD.ValueMember = "TOKCD";

            
            ///小田原サーバーで　部品図番を取得する            
            using (SqlConnection con = new SqlConnection(OTAConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(M0100B.ZUBAN) ZUBAN,M0100B.ZAICD " +
                    "FROM M0100 M0100A " +
                    "LEFT JOIN M0120 M0120 ON M0100A.ZAICD = M0120.ZAICD " +
                    "INNER JOIN M0100 M0100B ON M0120.KABUH = M0100B.ZAICD " +
                    "WHERE M0100B.ZUBAN LIKE '3M10%'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ZubanCollection.Add(reader.GetString(0));
                }

                txtBUHIN.AutoCompleteCustomSource = ZubanCollection;
                con.Close();
            }

            ///富士宮サーバーで製品図番を取得する
            using (SqlConnection con = new SqlConnection(FUJIConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(ZUBAN) ZUBAN FROM M0100 WHERE ZAIKB = 'A'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }

                txtSEIHIN.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        /// <summary>
        /// 得意先コードを選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTOKCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSEIHIN.Text = "";
            if (cbxTOKCD.Text != "001")
            {
                txtBUHIN.AutoCompleteCustomSource = null;
                txtBUHIN.Enabled = false;
            }
            else
            {
                txtBUHIN.AutoCompleteCustomSource = ZubanCollection;
                txtBUHIN.Enabled = true;
            }

            txtBUHIN.Text = "";

            ///納入場所を確認する
            DataTable dtResult = new DataTable();
            string ConString = "Data Source=" + Properties.Settings.Default.FUJIServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOKCD, NOUCD,NAME AS NOU FROM M0220 WHERE RIGHT(TOKCD,3)='" + cbxTOKCD.Text + "'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }

            if (dtResult.Rows.Count == 0)
            {
                cbxNOU.Items.Clear();
                lblNOU.Text = "*納入場所は存在しません。";
                cbxNOU.Text = "";
                btnOK.Enabled = false;
            }
            else
            {
                lblzuban.Text = "";
                cbxNOU.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbxNOU.Items.Add(dtResult.Rows[i][1].ToString() + " - " + dtResult.Rows[i][2].ToString());
                }

                cbxNOU.SelectedIndex = 0;
                lblNOU.Text = "";
                btnOK.Enabled = true;
            }

            checkOK();
        }

        /// <summary>
        /// 部品図番をに入力する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBUHIN_TextChanged(object sender, EventArgs e)
        {
            if (getFlag) return;
            if (SEIFlag) return;
            
            //if (txtBUHIN.Text.Length < 3) return;

            if (cbxTOKCD.Text != "001") return;

            BUFlag = true;
            if (txtBUHIN.Text == "" || txtBUHIN.Text == null　|| txtBUHIN.Text.Length < 3)
            {
                txtSEIHIN.Text = "";
                txtSNAME.Text = "";
                txtTANKA.Text = "";
                txtKISYU.Text = "";
                lblzuban.Text = "";
                if (Application.OpenForms.Cast<Form>().Any(form => form.Name == "図番マスター"))
                {
                    if (Application.OpenForms["図番マスター"].Visible)
                    {
                        Application.OpenForms["図番マスター"].Hide();
                    }
                }
                BUFlag = false;
                return;
            }

            ///製品図番の確認
            DataTable dtResult = new DataTable();
            string ConString = "Data Source=" + Properties.Settings.Default.FUJIServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(M0100A.ZUBAN) ZUBAN,RTRIM(M0100A.NAME) NAME,M0100A.KISYU,M0100A.TANKA " +
                    "FROM M0100 M0100A " +
                    "LEFT JOIN M0120 M0120 ON M0100A.ZAICD = M0120.ZAICD " +
                    "INNER JOIN M0100 M0100B ON M0120.KABUH = M0100B.ZAICD " +
                    "WHERE M0100B.ZUBAN ='" + txtBUHIN.Text + "'", con);
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                dtResult = new DataTable();
                ada.Fill(dtResult);
                con.Close();
            }

            if (dtResult.Rows.Count == 0)
            {
                txtSNAME.Text = "";
                txtTANKA.Text = "";
                txtKISYU.Text = "";
                txtSEIHIN.Text = "";
                lblzuban.Text = "*図番をマスターに登録されていません。";
            }
            else
            {
                lblzuban.Text = "";
                txtSEIHIN.Text = dtResult.Rows[0][0].ToString();
                currentSeihin = dtResult.Rows[0][0].ToString();
                txtSNAME.Text = dtResult.Rows[0][1].ToString();
                txtKISYU.Text = dtResult.Rows[0][2].ToString();
                txtTANKA.Text = dtResult.Rows[0][3].ToString();
            }
            BUFlag = false;

            checkOK();
        }

        /// <summary>
        /// 図番をマスターに登録されていない場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBUHIN_Leave(object sender, EventArgs e)
        {
            if (getFlag) return;
            if (txtBUHIN.Text == "") return;
            if (txtBUHIN.Text.Length < 3) return;

            ///富士宮サーバーに製品図番を登録されていない場合
            ///小田原サーバーで製品情報を取得する
            if (txtSEIHIN.Text == "" || txtSEIHIN.Text == null)
            {
                getFlag = true;
                DataTable dtResult = new DataTable();
                string ConString2 = "Data Source=" + Properties.Settings.Default.OTAServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
                using (SqlConnection con = new SqlConnection(ConString2))
                {
                    SqlCommand cmd = new SqlCommand("SELECT RTRIM(M0100A.ZUBAN) ZUBAN,RTRIM(M0100A.NAME) NAME,M0100A.KISYU,M0100A.TANKA,RTRIM(M0100B.ZUBAN) BUHIN,M0100A.ZAIKB " +
                        "FROM M0100 M0100A " +
                        "LEFT JOIN M0120 M0120 ON M0100A.ZAICD = M0120.ZAICD " +
                        "INNER JOIN M0100 M0100B ON M0120.KABUH = M0100B.ZAICD " +
                        "WHERE M0100B.ZUBAN ='" + txtBUHIN.Text + "'", con);
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(cmd);
                    dtResult = new DataTable();
                    ada.Fill(dtResult);
                    Clipboard.SetText(cmd.CommandText);
                    con.Close();
                }

                
                if (dtResult.Rows.Count == 0)
                {
                    getFlag = false;
                    return;
                }
                if (dtResult.Rows.Count == 1)
                {
                    txtSEIHIN.Text = dtResult.Rows[0][0].ToString();
                    currentSeihin = dtResult.Rows[0][0].ToString();
                    txtSNAME.Text = dtResult.Rows[0][1].ToString();
                    txtKISYU.Text = dtResult.Rows[0][2].ToString();
                    txtTANKA.Text = dtResult.Rows[0][3].ToString();
                    getFlag = false;
                    return;
                }
                getFlag = false;

                if (Application.OpenForms.Cast<Form>().Any(form => form.Name == "図番マスター"))
                {
                    seihinForm = (図番マスター)Application.OpenForms["図番マスター"];
                }
                else seihinForm = new 図番マスター();

                var UniqueRows = dtResult.AsEnumerable().Distinct(DataRowComparer.Default);
                seihinForm.dtValue = UniqueRows.CopyToDataTable();
                seihinForm.reloadDT();
                seihinForm.Show();
            }

            checkOK();
        }

        /// <summary>
        /// 図番マスターFormからデーターを取得する
        /// </summary>
        /// <param name="seihin"></param>
        /// <param name="name"></param>
        /// <param name="kisyu"></param>
        /// <param name="tanka"></param>
        /// <param name="buhin"></param>
        public void getValue(string seihin, string name, string kisyu, string tanka, string buhin)
        {
            getFlag = true;
            currentSeihin = seihin;
            txtSEIHIN.Text = seihin;
            txtSNAME.Text = name;
            txtKISYU.Text = kisyu;
            txtTANKA.Text = tanka;
            txtBUHIN.Text = buhin;
            getFlag = false;
        }

        /// <summary>
        /// 製品の情報を取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSEIHIN_TextChanged(object sender, EventArgs e)
        {
            if (txtSEIHIN.Text == currentSeihin) return;
            if (getFlag) return;
            if (txtSEIHIN.Text == "")
            {
                txtSNAME.Text = "";
                txtTANKA.Text = "";
                txtKISYU.Text = "";
                return;
            }
            if (BUFlag) return;
            SEIFlag = true;

            txtBUHIN.Text = "";

            ///製品情報を取得する
            DataTable dtResult = new DataTable();
            string ConString = "Data Source=" + Properties.Settings.Default.FUJIServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(P.ZUBAN) ZUBAN,P.NAME,P.KISYU,P.TANKA,C.ZUBAN " +
                    "FROM M0100 P " +
                    "LEFT JOIN M0120 T ON P.ZAICD = T.ZAICD " +
                    "LEFT JOIN M0100 C ON T.KABUH = C.ZAICD " +
                    "WHERE P.ZUBAN='" + txtSEIHIN.Text + "'", con);
                con.Open();

                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dtResult);
                con.Close();
            }

            if (dtResult.Rows.Count == 0)
            {
                txtSNAME.Text = "";
                txtTANKA.Text = "";
                txtKISYU.Text = "";
                lblzuban.Text = "*図番をマスターに登録されていません。";
            }
            else
            {
                lblzuban.Text = "";
                txtSNAME.Text = dtResult.Rows[0][1].ToString();
                txtKISYU.Text = dtResult.Rows[0][2].ToString();
                txtTANKA.Text = dtResult.Rows[0][3].ToString();
            }
            currentSeihin = txtSEIHIN.Text;
            SEIFlag = false;

            checkOK();
        }

        /// <summary>
        /// 要件確認
        /// </summary>
        private void checkOK()
        {
            if (lblzuban.Text + lblNOU.Text == "") btnOK.Enabled = true;
            else btnOK.Enabled = false;
        }

        /// <summary>
        /// 登録場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string SOUHU = "0";
            string CYUDT = dtCYUDT.Value.ToString("yyyyMMdd");
            string BCODE = "";
            string ZUBAN = txtSEIHIN.Text;
            string SNAME = txtSNAME.Text;
            string KISYU = "";
            string TANI = "";
            string TANKA = txtTANKA.Text;
            string CYUSU = numCYUSU.Value.ToString();
            string NOUKI = dtNOUNYU.Value.ToString("yyyyMMdd");
            string HKBN = "";
            string SEIZO = "";
            string HINSYU = "";
            string NKBN = "";
            string TOKCD = cbxTOKCD.Text;
            string NOUCD = cbxNOU.Text.Substring(2, 3);
            string SFLG = " ";

            if (ZUBAN == "")
            {
                MessageBox.Show("*図番を入力してください。");
                return;
            }
            if (lblzuban.Text != "")
            {
                MessageBox.Show("*図番をマスターに登録されていません。");
                return;
            }

            ///生産予定を登録            
            if (lblNOUNYU.Visible && dtNOUNYU.Visible)
            {
                string NOUNYU = dtNOUNYU.Value.ToString("yyyyMMdd");
                string sqlINS2 = "INSERT INTO SEIYODF.DBF VALUES ('','" + SOUHU + "','" + CYUDT + "','" + BCODE + "','" + ZUBAN + "','" + SNAME + "','" + KISYU + "','" + TANI + "','" + TANKA + "','" + CYUSU + "','" + NOUNYU + "','" + HKBN + "','" + SEIZO + "','" + HINSYU + "','" + NKBN + "','" + TOKCD + "','" + NOUCD + "','" + SFLG + "')";
                OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Properties.Settings.Default.DBFFolder + "; Extended Properties = dBASE IV;");
                conn.Open();
                OleDbCommand cmd2 = new OleDbCommand(sqlINS2, conn);
                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("製品図番 [" + txtSEIHIN.Text + "] を登録しました。");
            }

            btnOK.Enabled = false;
        }

        /// <summary>
        /// 中止場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtKISYU.Text = "";
            txtSNAME.Text = "";
            txtTANKA.Text = "";
            txtSEIHIN.Text = "";
            txtBUHIN.Text = "";
            dtCYUDT.Value = DateTime.Today;
            dtNOUNYU.Value = DateTime.Today.AddDays(10);
        }

        public void updateRecord(string zuban, string sname, string kisyu, string tanka, string cyusu, string cyudt, string nounyu, string tokcd, string noucd)
        {
            updateFlag = true;

            cbxTOKCD.SelectedText = tokcd;

            //for (int i = 0; i< cbxNOU.Items.Count; i++)
            //{
            //    if()
            //}

            cbxNOU.SelectedText = noucd;
            //txtCYUNO.Text = cyuno;
            txtBUHIN.Text = "";
            txtSEIHIN.Text = zuban;
            txtKISYU.Text = kisyu;
            txtSNAME.Text = sname;
            numCYUSU.Value = Int32.Parse(cyusu);
            txtTANKA.Text = tanka;


            dtCYUDT.Value = new DateTime(2019, 10, 20);

            dtCYUDT.Value = new DateTime(Int32.Parse(cyudt.Substring(0, 4)), Int32.Parse(cyudt.Substring(4, 2)), Int32.Parse(cyudt.Substring(6, 2)));
            //dtNOUKI.Value = new DateTime(Int32.Parse(nouki.Substring(0, 4)), Int32.Parse(nouki.Substring(4, 2)), Int32.Parse(nouki.Substring(6, 2)));
            dtNOUNYU.Value = new DateTime(Int32.Parse(nounyu.Substring(0, 4)), Int32.Parse(nounyu.Substring(4, 2)), Int32.Parse(nounyu.Substring(6, 2)));

            btnOK.Text = "Update";

            updateFlag = false;
        }

        private void 生産予定入力_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Cast<Form>().Any(form => form.Name == "図番マスター"))
                Application.OpenForms["図番マスター"].Close();
        }

        private void 生産予定入力_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
