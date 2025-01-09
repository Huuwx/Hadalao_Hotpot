using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hadalao_Hotpot
{
    public partial class EditTableForm : Form
    {
        public string tableName { get; set; }
        public string userID { get; set; }
        public int datban = 0;
        public int thanhtoan = 0;

        string chuoiketnoi = "Data Source=DESKTOP-4UUFE49;Initial Catalog=QUANLYLAU;TrustServerCertificate=true;Integrated Security=True";
        SqlConnection connection = null;
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dataTable = new DataTable();
        public EditTableForm()
        {
            InitializeComponent();
            button_delete.Enabled = false;
            button_edit.Enabled = false;
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                string sql = "SELECT * FROM food where food_availability = 'Available'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string foodName = reader["food_name"].ToString();
                    comboBox_food.Items.Add(foodName);
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                string sql = "SELECT * FROM KHACH ";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string customername = reader["TENKH"].ToString();
                    comboBoxkhach.Items.Add(customername);
                }
                reader.Close();
            }
        }


        private bool check()
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataGridViewCell cell = row.Cells[0];
                if (cell.Value != null && cell.Value.ToString() == comboBox_food.Text)
                {
                    return true;
                }
            }
            return false;
        }
        private void EditTableForm_Load(object sender, EventArgs e)
        {
            this.Text = tableName;
            LoadDataFromFile();
            if (datban == 0)
            {
                panel2.Enabled = false;
                panel1.Enabled = false;
            }
            connection = new SqlConnection(chuoiketnoi);
            connection.Open();
            //LoadData();
        }

        private void TableLabel_Click(object sender, EventArgs e)
        {

        }
        private void EditTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thanhtoan == 0) SaveDataToFile();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu chưa chọn món ăn
                if (comboBox_food.Text.Length == 0)
                {
                    throw new Exception("Vui lòng chọn đồ ăn để thêm");
                }

                // Bước 1: Lấy food_id từ tên món ăn đã chọn trong comboBox_food
                int foodId = GetFoodId(comboBox_food.Text);
                if (foodId == -1)
                {
                    throw new Exception("Món ăn không tồn tại trong cơ sở dữ liệu");
                }

                // Bước 2: Kiểm tra xem đã có hóa đơn chưa thanh toán
                int billId = GetCurrentBillId(); // Lấy bill_id hiện tại của bàn

                if (billId == -1)
                {
                    throw new Exception("Chưa có hóa đơn. Vui lòng tạo hóa đơn trước.");
                }

                // Bước 3: Lấy quantity từ numericUpDown_food
                int quantity = (int)numericUpDown_food.Value;

                // Bước 4: Kiểm tra nếu món ăn đã có trong bill_info, nếu có thì cập nhật số lượng, nếu chưa thì thêm mới
                string checkFoodInBillInfoQuery = "SELECT quantity FROM bill_info WHERE bill_id = @bill_id AND food_id = @food_id";
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(checkFoodInBillInfoQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bill_id", billId);
                        cmd.Parameters.AddWithValue("@food_id", foodId);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Món ăn đã có trong bill_info, cập nhật số lượng
                            int currentQuantity = Convert.ToInt32(result);
                            int newQuantity = currentQuantity + quantity;

                            string updateQuantityQuery = "UPDATE bill_info SET quantity = @newQuantity WHERE bill_id = @bill_id AND food_id = @food_id";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuantityQuery, connection))
                            {
                                updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                                updateCmd.Parameters.AddWithValue("@bill_id", billId);
                                updateCmd.Parameters.AddWithValue("@food_id", foodId);
                                updateCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Cập nhật số lượng món ăn thành công. Số lượng mới: {newQuantity}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Món ăn chưa có trong bill_info, thêm mới
                            string insertBillInfoQuery = "INSERT INTO bill_info (bill_id, food_id, quantity) VALUES (@bill_id, @food_id, @quantity)";

                            using (SqlCommand insertCmd = new SqlCommand(insertBillInfoQuery, connection))
                            {
                                insertCmd.Parameters.AddWithValue("@bill_id", billId);  // Đảm bảo bill_id đã tồn tại trong bảng bill
                                insertCmd.Parameters.AddWithValue("@food_id", foodId);  // ID món ăn
                                insertCmd.Parameters.AddWithValue("@quantity", quantity); // Số lượng món ăn
                                insertCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Thêm món ăn vào hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                // Sau khi thêm thành công, bạn có thể thêm vào DataGridView để hiển thị
                dgv.Rows.Add(comboBox_food.Text, numericUpDown_food.Value, int.Parse(textBox_price.Text) * numericUpDown_food.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Phương thức lấy bill_id hiện tại của bàn
        private int GetCurrentBillId()
        {
            int billId = -1;

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();

                string query = "SELECT bill_id FROM bill WHERE MABAN = @MABAN AND bill_status = N'Chưa thanh toán'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MABAN", tableName); // Lấy tên bàn hiện tại

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        billId = Convert.ToInt32(result);
                    }
                }
            }

            return billId;
        }

        private int CreateNewBill(string maKhach)
        {
            int billId = -1;

            // Kiểm tra xem MAKH có tồn tại trong bảng KHACH không
            string checkCustomerQuery = "SELECT MAKH FROM KHACH WHERE TENKH = @TENKH";

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(checkCustomerQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TENKH", maKhach); // Sử dụng tên khách hàng để tìm MAKH

                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        // Nếu không tìm thấy khách hàng, báo lỗi và không tạo hóa đơn
                        MessageBox.Show("Khách hàng không tồn tại trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                    else
                    {
                        maKhach = result.ToString(); // Lấy MAKH từ kết quả
                    }
                }

                // Tạo hóa đơn mới
                string insertBillQuery = "INSERT INTO bill (create_time, MABAN, MAKH, total, bill_status) " +
                                        "VALUES (GETDATE(), @MABAN, @MAKH, 0, N'Chưa thanh toán'); " +  // Tạo hóa đơn mới với trạng thái 'Chưa thanh toán'
                                        "SELECT SCOPE_IDENTITY();"; // Lấy bill_id vừa tạo

                using (SqlCommand cmd = new SqlCommand(insertBillQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MABAN", tableName);  // Lấy tên bàn hiện tại
                    cmd.Parameters.AddWithValue("@MAKH", maKhach);  // Đảm bảo MAKH đã tồn tại trong bảng KHACH

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        billId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi tạo hóa đơn mới.");
                    }
                }
            }

            return billId;
        }



        // Đảm bảo rằng bạn đã khai báo phương thức GetFoodId trong lớp EditTableForm
        private int GetFoodId(string foodName)
        {
            int foodId = -1;

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();

                string query = "SELECT food_id FROM food WHERE food_name = @foodName";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@foodName", foodName);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        foodId = Convert.ToInt32(result);
                    }
                }
            }

            return foodId;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu chưa chọn món ăn
                if (comboBox_food.Text.Length == 0)
                {
                    throw new Exception("Vui lòng chọn đồ ăn để xóa");
                }

                // Bước 1: Lấy food_id từ tên món ăn đã chọn trong comboBox_food
                int foodId = GetFoodId(comboBox_food.Text);
                if (foodId == -1)
                {
                    throw new Exception("Món ăn không tồn tại trong cơ sở dữ liệu");
                }

                // Bước 2: Kiểm tra xem đã có hóa đơn chưa thanh toán
                int billId = GetCurrentBillId(); // Lấy bill_id hiện tại của bàn

                if (billId == -1)
                {
                    throw new Exception("Chưa có hóa đơn. Vui lòng tạo hóa đơn trước.");
                }

                // Bước 3: Kiểm tra nếu món ăn có trong bill_info
                string checkFoodInBillInfoQuery = "SELECT quantity FROM bill_info WHERE bill_id = @bill_id AND food_id = @food_id";
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(checkFoodInBillInfoQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bill_id", billId);
                        cmd.Parameters.AddWithValue("@food_id", foodId);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Món ăn đã có trong bill_info, tiến hành xóa
                            string deleteFoodQuery = "DELETE FROM bill_info WHERE bill_id = @bill_id AND food_id = @food_id";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteFoodQuery, connection))
                            {
                                deleteCmd.Parameters.AddWithValue("@bill_id", billId);
                                deleteCmd.Parameters.AddWithValue("@food_id", foodId);
                                deleteCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Xóa món ăn khỏi hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại DataGridView (nếu có)
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                if (row.Cells[0].Value.ToString() == comboBox_food.Text)
                                {
                                    dgv.Rows.Remove(row);  // Xóa dòng khỏi DataGridView
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // Món ăn chưa có trong bill_info, thông báo lỗi
                            MessageBox.Show("Món ăn này chưa có trong hóa đơn. Không thể xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    button_delete.Enabled = true;
                    button_edit.Enabled = true;
                    comboBox_food.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
                    numericUpDown_food.Value = decimal.Parse(dgv.SelectedRows[0].Cells[1].Value.ToString());
                }
                else
                {
                    throw new Exception("Bạn đang chọn hàng rỗng");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu chưa chọn món ăn
                if (comboBox_food.Text.Length == 0)
                {
                    throw new Exception("Vui lòng chọn đồ ăn để chỉnh sửa");
                }

                // Bước 1: Lấy food_id từ tên món ăn đã chọn trong comboBox_food
                int foodId = GetFoodId(comboBox_food.Text);
                if (foodId == -1)
                {
                    throw new Exception("Món ăn không tồn tại trong cơ sở dữ liệu");
                }

                // Bước 2: Kiểm tra xem đã có hóa đơn chưa thanh toán
                int billId = GetCurrentBillId(); // Lấy bill_id hiện tại của bàn

                if (billId == -1)
                {
                    throw new Exception("Chưa có hóa đơn. Vui lòng tạo hóa đơn trước.");
                }

                // Bước 3: Lấy quantity từ numericUpDown_food
                int quantity = (int)numericUpDown_food.Value;

                // Bước 4: Kiểm tra nếu món ăn đã có trong bill_info, nếu có thì cập nhật số lượng, nếu chưa thì thông báo lỗi
                string checkFoodInBillInfoQuery = "SELECT quantity FROM bill_info WHERE bill_id = @bill_id AND food_id = @food_id";
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(checkFoodInBillInfoQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bill_id", billId);
                        cmd.Parameters.AddWithValue("@food_id", foodId);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Món ăn đã có trong bill_info, cập nhật số lượng
                            int currentQuantity = Convert.ToInt32(result);
                            int newQuantity = quantity; // Cập nhật trực tiếp số lượng mới

                            string updateQuantityQuery = "UPDATE bill_info SET quantity = @newQuantity WHERE bill_id = @bill_id AND food_id = @food_id";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuantityQuery, connection))
                            {
                                updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                                updateCmd.Parameters.AddWithValue("@bill_id", billId);
                                updateCmd.Parameters.AddWithValue("@food_id", foodId);
                                updateCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Sửa số lượng món ăn thành công. Số lượng mới: {newQuantity}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Món ăn chưa có trong bill_info, thông báo lỗi
                            MessageBox.Show("Món ăn này chưa có trong hóa đơn. Không thể sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Sau khi sửa thành công, bạn có thể cập nhật lại DataGridView (nếu cần thiết)
                // Giả sử DataGridView đã có một dòng tương ứng, bạn sẽ cần tìm nó và cập nhật giá trị tương ứng:
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value.ToString() == comboBox_food.Text)
                    {
                        row.Cells[1].Value = numericUpDown_food.Value;
                        row.Cells[2].Value = int.Parse(textBox_price.Text) * numericUpDown_food.Value;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal GetFoodPrice(string foodName)
        {
            decimal price = -1; // Trả về -1 nếu không tìm thấy giá

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();
                string query = "SELECT price FROM food WHERE food_name = @foodName";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@foodName", foodName);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        price = Convert.ToDecimal(result); // Lấy giá từ cơ sở dữ liệu
                    }
                }
            }

            return price;
        }



        private void button_datban_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxkhach.SelectedItem != null)
                {
                    if (datban == 0)
                    {
                        // Đặt bàn, cập nhật trạng thái bàn
                        button_datban.BackColor = Color.Red;
                        button_datban.Text = "Bàn đang được sử dụng";
                        datban = 1;
                        comboBoxkhach.Enabled = false;
                        panel1.Enabled = true;
                        panel2.Enabled = true;

                        // Bước 1: Lấy tên khách hàng từ comboBox
                        string maKhach = comboBoxkhach.Text;

                        // Bước 2: Kiểm tra xem bàn có hóa đơn chưa thanh toán không
                        int billId = GetCurrentBillId();
                        if (billId == -1)
                        {
                            // Nếu chưa có hóa đơn, tạo hóa đơn mới
                            billId = CreateNewBill(maKhach);
                            if (billId == -1)
                            {
                                throw new Exception("Không thể tạo hóa đơn mới.");
                            }
                            MessageBox.Show($"Hóa đơn mới được tạo với bill_id: {billId}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Bàn này đã có hóa đơn chưa thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    throw new Exception("Vui lòng nhập tên khách hàng");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_thanhtoan_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận thanh toán
            DialogResult confirmationResult = MessageBox.Show("Bạn có chắc muốn tạo hóa đơn?", "Thông báo", MessageBoxButtons.YesNo);
            if (confirmationResult == DialogResult.Yes)
            {
                // Đặt trạng thái ban đầu sau thanh toán
                datban = 0;
                button_datban.BackColor = Color.White;
                button_datban.Text = "Đặt bàn";
                button_datban.Enabled = false;
                thanhtoan = 1;

                // Reset các trường nhập liệu và điều khiển
                comboBox_food.Text = "";
                numericUpDown_food.Value = 0;
                panel1.Enabled = false;
                panel2.Enabled = false;

                // Vô hiệu hóa nút thanh toán
                button_thanhtoan.Enabled = false;

                try
                {
                    // Kiểm tra và mở kết nối cơ sở dữ liệu nếu chưa mở
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Kiểm tra xem có `bill_id` hiện tại không (đã được tạo từ trước khi đặt bàn)
                    string checkBillQuery = "SELECT bill_id FROM bill WHERE MABAN = @MABAN";
                    cmd = connection.CreateCommand();
                    cmd.CommandText = checkBillQuery;
                    cmd.Parameters.AddWithValue("@MABAN", tableName);  // Lấy tên bàn hiện tại

                    object existingBillId = cmd.ExecuteScalar();
                    int billId = -1;

                    if (existingBillId != null)
                    {
                        billId = Convert.ToInt32(existingBillId);  // Nếu hóa đơn đã tồn tại, lấy bill_id hiện tại
                    }
                    else
                    {
                        // Nếu không có hóa đơn, thông báo lỗi
                        MessageBox.Show("Hóa đơn chưa được tạo. Vui lòng tạo hóa đơn trước khi thanh toán.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Gọi stored procedure để cập nhật tổng tiền của hóa đơn
                    cmd.CommandText = "EXEC pr_UpdateBillTotal @bill_id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@bill_id", billId);
                    cmd.ExecuteNonQuery();  // Thực thi stored procedure để tính tổng tiền và cập nhật vào bảng bill

                    // Lấy lại giá trị total từ bảng bill sau khi cập nhật
                    string getTotalSql = "SELECT total FROM bill WHERE bill_id = @bill_id";
                    cmd.CommandText = getTotalSql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@bill_id", billId);

                    object totalObj = cmd.ExecuteScalar();
                    decimal total = 0;
                    if (totalObj != null)
                    {
                        total = Convert.ToDecimal(totalObj);  // Lấy giá trị total từ cơ sở dữ liệu
                    }

                    // Hiển thị tổng tiền trong TextBox
                    textBox_total.Text = total.ToString("N2");  // Định dạng số thành 2 chữ số thập phân

             
                    MessageBox.Show("Đã tạo hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa dữ liệu trong file nếu có
                    string filePath = "C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        MessageBox.Show("Đã xóa dữ liệu từ file.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Đảm bảo đóng kết nối sau khi thực hiện xong
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }




        private void SaveDataToFile()
        {
            try
            {
                if (datban == 1)
                {
                    using (StreamWriter writer = new StreamWriter("C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt"))
                    {
                        writer.WriteLine(tableName); // Ghi tên bàn
                        writer.WriteLine(comboBoxkhach.Text);
                        writer.WriteLine(datban);    // Ghi trạng thái đặt bàn

                        // Ghi dữ liệu từ DataGridView
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            writer.WriteLine(row.Cells[0].Value + "," + row.Cells[1].Value);
                        }
                    }

                    MessageBox.Show("Dữ liệu đã được lưu vào file.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDataFromFile()
        {
            try
            {
                if (File.Exists("C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt"))
                {
                    using (StreamReader reader = new StreamReader("C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt"))
                    {
                        tableName = reader.ReadLine(); // Đọc tên bàn
                        comboBoxkhach.Text = reader.ReadLine();
                        datban = int.Parse(reader.ReadLine()); // Đọc trạng thái đặt bàn

                        // Đặt trạng thái đặt bàn cho nút
                        if (datban == 1)
                        {
                            button_datban.BackColor = Color.Red;
                            button_datban.Text = "Bàn đang được sử dụng";
                            comboBoxkhach.Enabled = false;
                        }

                        // Đọc dữ liệu vào DataGridView
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            dgv.Rows.Add(parts[0], parts[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi đọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_food_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFoodName = comboBox_food.SelectedItem.ToString();
            string sql = "SELECT food_price FROM food WHERE food_name = @foodName";
            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@foodName", selectedFoodName);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    int foodPrice = Convert.ToInt32(result);
                    textBox_price.Text = foodPrice.ToString();
                }
                else
                {
                    textBox_price.Text = "Price not available";
                }
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }


        private void textBox_tenkhachhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào một ô dữ liệu trong DataGridView (không phải tiêu đề cột)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                
            }
        }

        private void PrintCustomerBill()
        {
            // Sử dụng chuỗi kết nối sẵn có
            string connectionString = chuoiketnoi;

            // Tạo câu lệnh SQL để truy vấn dữ liệu từ view vw_CustomerBills cho bàn hiện tại
            string query = @"SELECT * FROM vw_CustomerBills";

            // Tạo một SqlConnection và SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo SqlCommand và thêm tham số cho câu truy vấn
                    SqlCommand command = new SqlCommand(query, connection);
                    /*command.Parameters.AddWithValue("@MABAN", tableName);*/ // Dùng tableName để lọc dữ liệu cho bàn hiện tại

                    // Tạo DataTable để chứa kết quả truy vấn
                    DataTable dataTable = new DataTable();

                    // Tạo SqlDataAdapter để thực hiện truy vấn
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                    // Điền dữ liệu vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Gán DataTable cho DataGridView
                    dgv.DataSource = dataTable;  // dgv là DataGridView của bạn
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có
                    MessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxkhach.SelectedItem != null)
            {
                // Lấy tên khách hàng đã chọn từ ComboBox
                string selectedCustomerName = comboBoxkhach.SelectedItem.ToString();

                // Truy vấn SQL để lấy mã khách hàng (MAKH) từ tên khách hàng (TENKH)
                string sql = "SELECT MAKH FROM KHACH WHERE TENKH = @TENKH";

                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    // Thêm tham số cho truy vấn SQL
                    command.Parameters.AddWithValue("@TENKH", selectedCustomerName);

                    try
                    {
                        connection.Open();
                        // Thực thi truy vấn và lấy kết quả
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            // Chuyển kết quả thành mã khách hàng (MAKH)
                            string customerID = result.ToString();

                            // Hiển thị mã khách hàng vào ComboBox.SelectedItem hoặc một Label/TextBox khác
                            comboBoxkhach.SelectedItem = customerID;  // Cập nhật giá trị đúng

                            // Nếu cần, hiển thị mã khách hàng ở một vị trí khác
                            // labelCustomerID.Text = customerID;  // Ví dụ: hiển thị mã khách hàng trên một Label

                            MessageBox.Show("Customer ID: " + customerID);  // Hiển thị mã khách hàng
                        }
                        else
                        {
                            // Nếu không tìm thấy khách hàng
                            MessageBox.Show("Customer not found.");
                            comboBoxkhach.SelectedItem = null; // Xóa lựa chọn nếu không tìm thấy
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                // Thông báo khi không có khách hàng nào được chọn
                MessageBox.Show("Please select a customer.");
            }


        }
    }
}
