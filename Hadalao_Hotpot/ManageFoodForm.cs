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
using System.Collections;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hadalao_Hotpot
{
    public partial class ManageFoodForm : Form
    {

        string connectionSTR = @"Data Source=DESKTOP-B87EC4S;Initial Catalog=QUANLYLAU;Integrated Security=True";
        SqlConnection conn = null;

        public ManageFoodForm()
        {
            InitializeComponent();
        }

        public void PrintFoodList(string query = "select food_id as [Mã Thức Ăn], food_name as [Tên Món], food_price as [Giá], food_availability as [Tình Trạng] from FOOD")
        {

            SqlCommand command = new SqlCommand(query, conn);

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);

            dtgvFood.DataSource = data;
        }

        private void AddFoodBtn_Click(object sender, EventArgs e)
        {
            AddAndEditFoodForm ae = new AddAndEditFoodForm();
            ae.Text = "Thêm";
            ae.ShowDialog();
            PrintFoodList();
        }

        private void EditFoodBtn_Click(object sender, EventArgs e)
        {
            int i = dtgvFood.CurrentRow.Index;
            AddAndEditFoodForm ae = new AddAndEditFoodForm();
            ae.Text = "Chỉnh Sửa";
            ae.SetFoodDetails(
                    Convert.ToInt32(dtgvFood.Rows[i].Cells[0].Value),
                    dtgvFood.Rows[i].Cells[1].Value.ToString(),
                    Convert.ToDecimal(dtgvFood.Rows[i].Cells[2].Value),
                    dtgvFood.Rows[i].Cells[3].Value.ToString()
                );
            ae.ShowDialog();
            PrintFoodList();
        }

        private void DeleteFoodBtn_Click(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedRows.Count >= 0)
            {
                int i;
                i = dtgvFood.CurrentRow.Index;
                string food_idSTR = dtgvFood.Rows[i].Cells[0].Value.ToString();
                Console.WriteLine( food_idSTR );
                try
                {
                    string querydel = @"DELETE FROM FOOD WHERE @food_id = food_id";

                    SqlCommand cm = new SqlCommand(querydel, conn);
                    cm.Parameters.AddWithValue("@food_id", food_idSTR);
                    cm.ExecuteNonQuery();
                    dtgvFood.Rows.RemoveAt(i);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ManageFoodForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionSTR);
            conn.Open();
            PrintFoodList();
        }

        private void ManageFoodForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select food_id as [Mã Thức Ăn], food_name as [Tên Món], food_price as [Giá], food_availability as [Tình Trạng] from FOOD WHERE food_id = @food_id OR food_name = @food_name OR food_price = @food_price OR food_availability = @food_availability";
                SqlCommand command = new SqlCommand(query, conn);

                if (int.TryParse(SearchTb.Text, out int resultId))
                {
                    command.Parameters.AddWithValue("@food_id", resultId);
                }
                else
                {
                    command.Parameters.AddWithValue("@food_id", -1000);
                }
                command.Parameters.AddWithValue("@food_name", SearchTb.Text);
                if (double.TryParse(SearchTb.Text, out double resultPrice))
                {
                    command.Parameters.AddWithValue("@food_price", resultPrice);
                }
                else
                {
                    command.Parameters.AddWithValue("@food_price", -1000);
                }
                command.Parameters.AddWithValue("@food_availability", SearchTb.Text);

                DataTable data = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                dtgvFood.DataSource = data;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadbtn_Click(object sender, EventArgs e)
        {
            PrintFoodList();
        }

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedRows.Count < 0)
                {
                    throw new Exception("Bạn đang chọn hàng rỗng");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
