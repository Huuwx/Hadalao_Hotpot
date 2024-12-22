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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
namespace Hadalao_Hotpot
{
    public partial class Forms1 : Form
    {
        string chuoiketnoi = "Data Source=DESKTOP-B87EC4S;Initial Catalog=QUANLYLAU;Integrated Security=True";
        SqlConnection conn = null;
        int close = 0;
        public Forms1()
        {
            InitializeComponent();
        }

        private void QLKH_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            HienThi();
        }
        public void HienThi()
        {
            string sqlSelect = "SELECT *FROM KHACH";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            dtgdskh.DataSource = data;
        }

        private void QLKH_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close == 0)
            {
                DialogResult rel = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rel == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMAKH.Text) || string.IsNullOrEmpty(txtTENKH.Text)
                     || string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtTUOI.Text))
                {
                    MessageBox.Show("Dữ liệu không được để trống !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sqlTHEM = "INSERT INTO KHACH VALUES(@MAKH,@TENKH,@SDT,@TUOI)";
                    SqlCommand cmd = new SqlCommand(sqlTHEM, conn);
                    cmd.Parameters.AddWithValue("MAKH", txtMAKH.Text);
                    cmd.Parameters.AddWithValue("TENKH", txtTENKH.Text);
                    int sdt = int.Parse(txtSDT.Text);
                    cmd.Parameters.AddWithValue("SDT", "0" + sdt);
                    int age = int.Parse(txtTUOI.Text);
                    cmd.Parameters.AddWithValue("TUOI", age);

                    cmd.ExecuteNonQuery();
                    HienThi();
                }

            }

            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi kiểu dữ liệu : " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlSUA = "UPDATE KHACH SET MAKH=@MAKH, TENKH=@TENKH, SDT=@sdt, TUOI=@TUOI WHERE MAKH=@MAKH";
                SqlCommand cmd = new SqlCommand(sqlSUA, conn);
                cmd.Parameters.AddWithValue("MAKH", txtMAKH.Text);
                cmd.Parameters.AddWithValue("TENKH", txtTENKH.Text);
                int sdt = int.Parse(txtSDT.Text);
                cmd.Parameters.AddWithValue("SDT", "0" + sdt);
                int age = int.Parse(txtTUOI.Text);
                cmd.Parameters.AddWithValue("TUOI", age);
                cmd.ExecuteNonQuery();
                HienThi();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi kiểu dữ liệu : " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgdskh.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgdskh.SelectedRows)
                {
                    try
                    {
                        string makh = row.Cells[0].Value.ToString();

                        string querydel = @"DELETE FROM KHACH WHERE @MAKH = MAKH";

                        SqlCommand cmd = new SqlCommand(querydel, conn);
                        cmd.Parameters.AddWithValue("MAKH", makh);
                        cmd.ExecuteNonQuery();
                        dtgdskh.Rows.Remove(row);
                        HienThi();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void dtgdskh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dtgdskh.Rows.Count)
                {
                    DataGridViewRow selectedRow = dtgdskh.Rows[e.RowIndex];
                    txtMAKH.Text = selectedRow.Cells["MAKH"].Value.ToString();
                    txtTENKH.Text = selectedRow.Cells["TENKH"].Value.ToString();
                    txtSDT.Text = selectedRow.Cells["SDT"].Value.ToString();
                    txtTUOI.Text = selectedRow.Cells["TUOI"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnTIMKIEM_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTIMKIEM.Text))
                {
                    MessageBox.Show("Dữ liệu không được để trống !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sqlTIMKIEM = "SELECT *FROM KHACH WHERE MAKH=@MAKH";
                    SqlCommand cmd = new SqlCommand(sqlTIMKIEM, conn);
                    cmd.Parameters.AddWithValue("MAKH", txtTIMKIEM.Text);
                    cmd.Parameters.AddWithValue("TENKH", txtTENKH.Text);
                    cmd.Parameters.AddWithValue("SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("TUOI", txtTUOI.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dtgdskh.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            HienThi();
        }
    }
}




