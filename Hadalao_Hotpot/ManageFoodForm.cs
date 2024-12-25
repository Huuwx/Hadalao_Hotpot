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

        public void PrintFoodList()
        {
            string query = "SELECT * FROM vw_FoodDetails";

            SqlCommand command = new SqlCommand(query, conn);

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);

            dtgvFood.DataSource = data;
        }

        public void PrintAvailableFoodList()
        {
            string query = "SELECT * FROM vw_AvailableFood";

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
                Console.WriteLine(food_idSTR);
                try
                {
                    string querydel = @"EXEC pr_DeleteFoodById @food_id";

                    SqlCommand cm = new SqlCommand(querydel, conn);
                    cm.Parameters.AddWithValue("@food_id", food_idSTR);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string query = @"EXEC pr_SearchByName @food_name";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@food_name", SearchTb.Text);

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

        private void availableFoodBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT dbo.fn_TotalFoodByAvailability(@availability) AS TotalFood";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@availability", "Available");

                var result = command.ExecuteScalar();
                MessageBox.Show("Số lượng món ăn còn hàng: " + result.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            PrintAvailableFoodList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT dbo.fn_AverageFoodPrice() AS AveragePrice";
            SqlCommand command = new SqlCommand(query, conn);

            var result = command.ExecuteScalar();
            MessageBox.Show("Giá trung bình tất cả các món ăn: " + result.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PrintByCursor_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT dbo.fn_MaxPriceByCursor()";
                SqlCommand command = new SqlCommand(query, conn);

                var result = command.ExecuteScalar();
                MessageBox.Show("Giá của món ăn đắt nhất: " + result.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
