using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hadalao_Hotpot
{
    public partial class AddAndEditFoodForm : Form
    {

        string connectionSTR = @"Data Source=DESKTOP-B87EC4S;Initial Catalog=QUANLYLAU;Integrated Security=True";
        SqlConnection conn = null;
        int close = 0;
        int id;

        public AddAndEditFoodForm()
        {
            InitializeComponent();
            InitializePlaceHolderFortxbFoodName();
        }

        private void InitializePlaceHolderFortxbFoodName()
        {
            // Xóa văn bản khi người dùng nhấn vào
            this.txbFoodName.Enter += (s, e) =>
            {
                if (this.txbFoodName.Text == "Nhập tên món ăn...")
                {
                    this.txbFoodName.Text = "";
                    this.txbFoodName.ForeColor = Color.Black;
                }
            };

            // Hiển thị lại văn bản nếu người dùng không nhập
            this.txbFoodName.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(this.txbFoodName.Text))
                {
                    this.txbFoodName.Text = "Nhập tên món ăn...";
                    this.txbFoodName.ForeColor = Color.Gray;
                }
            };
        }

        public void InitializeItemsForCbTT()
        {
            if (this.Text == "Thêm")
            {
                cbbTT.Items.Add("Available");
            }
            else
            {
                cbbTT.Items.Add("Available");
                cbbTT.Items.Add("Unavailable");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            DialogResult rel = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rel == DialogResult.Yes)
            {
                close = 1;
                this.Close();
            }
        }

        private void AddAndEditFoodForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close == 0)
            {
                DialogResult rel = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rel == DialogResult.No)
                    e.Cancel = true;
            }
        }

        void changeorAddValue(string query)
        {
            try
            {
                if (string.IsNullOrEmpty(txbFoodName.Text) || string.IsNullOrEmpty(cbbTT.Text))
                {
                    MessageBox.Show("Dữ liệu không được để trống !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand command = new SqlCommand(query, conn);
                if (this.Text == "Chỉnh Sửa")
                {
                    command.Parameters.AddWithValue("@food_id", id); // Đảm bảo id được truyền chính xác từ SetFoodDetails
                    command.Parameters.AddWithValue("@food_availability", cbbTT.Text);
                }
                command.Parameters.AddWithValue("@food_name", txbFoodName.Text);
                command.Parameters.AddWithValue("@food_price", nbudPrice.Value);
                command.ExecuteNonQuery();
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi kiểu dữ liệu : " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string query;

            if (this.Text == "Thêm")
            {
                query = "EXEC pr_ThemMonAn @food_name, @food_price";
            }
            else
            {
                query = "UPDATE FOOD SET food_name = @food_name, food_price = @food_price , food_availability = @food_availability WHERE food_id = @food_id";
                string queryCheck = "Select Count(*) From FOOD Where food_id = @food_id";
                SqlCommand cmdc = new SqlCommand(queryCheck, conn);
                cmdc.Parameters.AddWithValue("@food_id", id);

                var existingRecords = (int)cmdc.ExecuteScalar();
                if (existingRecords == 0)
                {
                    MessageBox.Show("Mã thức ăn không hợp lệ !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(query))
                changeorAddValue(query);
        }

        private void AddAndEditFoodForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }

        private void AddAndEditFoodForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionSTR);
            conn.Open();
        }

        private void txbFoodName_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetFoodDetails(int foodId, string foodName, decimal foodPrice, string foodAvailability)
        {
            id = foodId;
            txbFoodName.Text = foodName;
            nbudPrice.Value = foodPrice;
            cbbTT.Text = foodAvailability;
        }
    }
}
