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
    public partial class 図番マスター : Form
    {
        DataTable dtResult = new DataTable();

        public 図番マスター()
        {
            InitializeComponent();
            DataGridViewButtonColumn column = new DataGridViewButtonColumn();
            //列の名前を設定
            column.Name = "選択";
            //全てのボタンに"詳細閲覧"と表示する
            column.UseColumnTextForButtonValue = true;
            column.Text = "選択";
            //DataGridViewに追加する
            dgvSEI.Columns.Add(column);
        }

        private void 図番マスター_Load(object sender, EventArgs e)
        {
            
        }

        private void DataGridView1_CellContentClick(object sender,DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //"Button"列ならば、ボタンがクリックされた
            
        }


        public void reloadDT()
        {
            dgvSEI.DataSource = dtValue;

            return;

            DataTable dtResult = new DataTable();

            string ConString = "Data Source=" + Properties.Settings.Default.OTAServer + ";Initial Catalog=TESC;Persist Security Info=True;User ID=TESCWIN;";
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(M0100B.ZUBAN) BUHIN, RTRIM(M0100A.ZUBAN) SEIHIN,RTRIM(M0100A.NAME) NAME,M0100A.KISYU,M0100A.TANKA " +
                    "FROM M0100 M0100A " +
                    "LEFT JOIN M0120 M0120 ON M0100A.ZAICD = M0120.ZAICD " +
                    "INNER JOIN M0100 M0100B ON M0120.KABUH = M0100B.ZAICD " +
                    "WHERE M0100B.ZUBAN ='" + zubanCode + "'", con);
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                dtResult = new DataTable();
                ada.Fill(dtResult);
                con.Close();
            }



            if (dtResult.Rows.Count == 0)
            {

            }
            else
            {
                dgvSEI.DataSource = dtResult;
            }
        }

        public String zubanCode { get; set; }

        public DataTable dtValue { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dgvSEI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvSEI.Columns[e.ColumnIndex].Name == "選択")
            {
                if ((Application.OpenForms.Cast<Form>().Any(form => form.Name == "確定受注入力")) && (Application.OpenForms.Cast<Form>().Any(form => form.Name == "生産予定入力")))
                {
                    if (Application.OpenForms["生産予定入力"].Visible)
                    {
                        ((生産予定入力)Application.OpenForms["生産予定入力"]).getValue(dgvSEI.Rows[e.RowIndex].Cells["ZUBAN"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["NAME"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["KISYU"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["TANKA"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["BUHIN"].FormattedValue.ToString());
                        ((生産予定入力)Application.OpenForms["生産予定入力"]).Activate();
                    }
                    else
                    {
                        ((確定受注入力)Application.OpenForms["確定受注入力"]).getValue(dgvSEI.Rows[e.RowIndex].Cells["ZUBAN"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["NAME"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["KISYU"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["TANKA"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["BUHIN"].FormattedValue.ToString());
                        ((確定受注入力)Application.OpenForms["確定受注入力"]).Activate();
                    }
                }
                else if ((Application.OpenForms.Cast<Form>().Any(form => form.Name == "確定受注入力")))
                {
                    ((確定受注入力)Application.OpenForms["確定受注入力"]).getValue(dgvSEI.Rows[e.RowIndex].Cells["ZUBAN"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["NAME"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["KISYU"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["TANKA"].FormattedValue.ToString(),
               dgvSEI.Rows[e.RowIndex].Cells["BUHIN"].FormattedValue.ToString());
                    ((確定受注入力)Application.OpenForms["確定受注入力"]).Activate();
                }
                else
                {
                    ((生産予定入力)Application.OpenForms["生産予定入力"]).getValue(dgvSEI.Rows[e.RowIndex].Cells["ZUBAN"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["NAME"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["KISYU"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["TANKA"].FormattedValue.ToString(),
                dgvSEI.Rows[e.RowIndex].Cells["BUHIN"].FormattedValue.ToString());
                    ((生産予定入力)Application.OpenForms["生産予定入力"]).Activate();
                }
            }
        }
    }
}
