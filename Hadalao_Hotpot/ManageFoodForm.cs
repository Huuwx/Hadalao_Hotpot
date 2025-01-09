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
            InitializePlaceHolderFortxbSearch();
        }

        private void InitializePlaceHolderFortxbSearch()
        {
            // Xóa văn bản khi người dùng nhấn vào
            this.SearchTb.Enter += (s, e) =>
            {
                if (this.SearchTb.Text == "Nhập tên món ăn...")
                {
                    this.SearchTb.Text = "";
                    this.SearchTb.ForeColor = Color.Black;
                }
            };

            // Hiển thị lại văn bản nếu người dùng không nhập
            this.SearchTb.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(this.SearchTb.Text))
                {
                    this.SearchTb.Text = "Nhập tên món ăn...";
                    this.SearchTb.ForeColor = Color.Gray;
                }
            };
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

        public void PrintUnavailableFoodList()
        {
            string query = "SELECT * from fn_UnavailableFood()";

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
            ae.InitializeItemsForCbTT();
            ae.ShowDialog();
            PrintFoodList();
        }

        private void EditFoodBtn_Click(object sender, EventArgs e)
        {
            int i = dtgvFood.CurrentRow.Index;
            AddAndEditFoodForm ae = new AddAndEditFoodForm();
            ae.Text = "Chỉnh Sửa";
            ae.InitializeItemsForCbTT();
            ae.SetFoodDetails(
                    Convert.ToInt32(dtgvFood.Rows[i].Cells[1].Value),
                    dtgvFood.Rows[i].Cells[2].Value.ToString(),
                    Convert.ToDecimal(dtgvFood.Rows[i].Cells[3].Value),
                    dtgvFood.Rows[i].Cells[4].Value.ToString()
                );
            ae.ShowDialog();
            PrintFoodList();
        }

        private void DeleteFoodBtn_Click(object sender, EventArgs e)
        {
            //if (dtgvFood.SelectedRows.Count >= 0)
            //{
            //    int i;
            //    i = dtgvFood.CurrentRow.Index;
            //    string food_idSTR = dtgvFood.Rows[i].Cells[0].Value.ToString();
            //    Console.WriteLine(food_idSTR);
            //    try
            //    {
            //        string querydel = @"EXEC pr_DeleteFoodById @food_id";

            //        SqlCommand cm = new SqlCommand(querydel, conn);
            //        cm.Parameters.AddWithValue("@food_id", food_idSTR);
            //        int rowAffected = cm.ExecuteNonQuery();
            //        if (rowAffected > 0)
            //        {
            //            MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            dtgvFood.Rows.RemoveAt(i);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Xóa món ăn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    catch (SqlException ex)
            //    {
            //        MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


            List<int> selectedFoodIds = new List<int>();
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();

            // Duyệt qua tất cả các hàng trong DataGridView
            foreach (DataGridViewRow row in dtgvFood.Rows)
            {
                // Kiểm tra nếu checkbox được chọn
                if (row.Cells["chkSelect"].Value != null && (bool)row.Cells["chkSelect"].Value)
                {
                    int foodId = Convert.ToInt32(row.Cells["food_id"].Value);
                    selectedFoodIds.Add(foodId);
                    rowsToDelete.Add(row); // Thêm dòng vào danh sách để xóa sau
                }
            }

            // Kiểm tra nếu không có món ăn nào được chọn
            if (selectedFoodIds.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món ăn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị hộp thoại xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa các món ăn đã chọn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            // Xóa các món ăn trong cơ sở dữ liệu
            try
            {
                for(int i = selectedFoodIds.Count - 1; i >= 0; i--)
                {
                    //DialogResult dialogResults = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn ?" + selectedFoodIds[i], "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    string query = @"EXEC pr_DeleteFoodById @food_id";

                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@food_id", selectedFoodIds[i]);
                    command.ExecuteNonQuery();

                    //int rowAffected = command.ExecuteNonQuery();
                    //if (rowAffected > 0)
                    //{
                    //    MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Xóa món ăn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }

                // Xóa các dòng trong DataGridView
                foreach (DataGridViewRow row in rowsToDelete)
                {
                    dtgvFood.Rows.Remove(row);
                }

                MessageBox.Show("Xóa thành công các món ăn đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi xóa món ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManageFoodForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionSTR);
            conn.Open();
            // Thêm cột checkbox vào DataGridView
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Chọn";
            chk.Name = "chkSelect";
            chk.ReadOnly = false; // Đảm bảo checkbox có thể chỉnh sửa
            chk.TrueValue = true; // Giá trị khi được tick
            chk.FalseValue = false; // Giá trị khi không được tick
            dtgvFood.Columns.Insert(0, chk);

            // Cấu hình DataGridView
            dtgvFood.AllowUserToAddRows = false; // Không cho phép thêm hàng mới
            dtgvFood.EditMode = DataGridViewEditMode.EditOnEnter; // Cho phép chỉnh sửa ngay khi nhấn vào ô

            // In danh sách món ăn
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
                string query = "SELECT dbo.fn_TotalFoodByAvailability(@availability) AS Total";
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
            //string query = "SELECT dbo.fn_AverageFoodPrice() AS AveragePrice";
            //SqlCommand command = new SqlCommand(query, conn);

            //var result = command.ExecuteScalar();
            //MessageBox.Show("Giá trung bình tất cả các món ăn: " + result.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PrintUnavailableFoodList();
        }

        private void PrintByCursor_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"EXEC pr_MaxPriceByCursor";
                SqlCommand command = new SqlCommand(query, conn);

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

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
