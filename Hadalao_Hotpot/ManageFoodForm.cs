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

namespace Hadalao_Hotpot
{
    public partial class ManageFoodForm : Form
    {

        string connectionSTR = @"Data Source=DESKTOP-6QPUDLE;Initial Catalog=QUANLYLAU;Integrated Security=True";
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
            ae.setTxbId();
            ae.ShowDialog();
            PrintFoodList();
        }

        private void EditFoodBtn_Click(object sender, EventArgs e)
        {
            AddAndEditFoodForm ae = new AddAndEditFoodForm();
            ae.Text = "Chỉnh Sửa";
            ae.ShowDialog();
            PrintFoodList();
        }

        private void DeleteFoodBtn_Click(object sender, EventArgs e)
        {
            if(dtgvFood.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow row in dtgvFood.SelectedRows)
                {
                    try
                    {
                        string food_idSTR = row.Cells[0].Value.ToString();

                        string querydel = @"DELETE FROM FOOD WHERE @food_id = food_id";

                        SqlCommand cm = new SqlCommand(querydel, conn);
                        cm.Parameters.AddWithValue("@food_id", food_idSTR);
                        cm.ExecuteNonQuery();
                        dtgvFood.Rows.Remove(row);
                    }
                    catch(SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            catch(SqlException ex)
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
                if(dtgvFood.SelectedRows.Count < 0) 
                {
                    throw new Exception("Bạn đang chọn hàng rỗng");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
