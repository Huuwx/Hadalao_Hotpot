using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        string chuoiketnoi = "Data Source=DESKTOP-B87EC4S;Initial Catalog=QUANLYLAU;TrustServerCertificate=true;Integrated Security=True";
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
                else if (numericUpDown_food.Value <= 0)
                {
                    throw new Exception("Vui lòng thêm số lượng");
                }

                // Bước 1: Lấy food_id từ tên món ăn đã chọn trong comboBox_food
                int foodId = GetFoodId(comboBox_food.Text);
                if (foodId == -1)
                {
                    throw new Exception("Món ăn không tồn tại trong cơ sở dữ liệu");
                }

                // Bước 2: Kiểm tra nếu chưa có hóa đơn cho bàn hiện tại
                int billId = GetCurrentBillId(); // Lấy bill_id hiện tại của bàn

                // Nếu chưa có hóa đơn (billId == -1), tạo hóa đơn mới
                if (billId == -1)
                {
                    billId = CreateNewBill();  // Tạo hóa đơn mới và lấy bill_id
                    if (billId == -1)
                    {
                        throw new Exception("Không thể tạo hóa đơn mới");
                    }
                    MessageBox.Show($"Hóa đơn mới được tạo với bill_id: {billId}"); // Thông báo hóa đơn mới
                }

                // Bước 3: Lấy quantity từ numericUpDown_food
                int quantity = (int)numericUpDown_food.Value;

                // Bước 4: Thêm món ăn vào bảng bill_info
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    connection.Open();

                    // Lệnh SQL để thêm món ăn vào bill_info
                    string insertBillInfoQuery = "INSERT INTO bill_info (bill_id, food_id, quantity) VALUES (@bill_id, @food_id, @quantity)";

                    using (SqlCommand cmd = new SqlCommand(insertBillInfoQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bill_id", billId);  // Đảm bảo bill_id đã tồn tại trong bảng bill
                        cmd.Parameters.AddWithValue("@food_id", foodId);  // ID món ăn
                        cmd.Parameters.AddWithValue("@quantity", quantity); // Số lượng món ăn

                        // Thực thi lệnh SQL để thêm món ăn vào bill_info
                        cmd.ExecuteNonQuery();
                    }

                    // Sau khi thêm thành công, bạn có thể thêm vào DataGridView để hiển thị
                    dgv.Rows.Add(comboBox_food.Text, numericUpDown_food.Value, int.Parse(textBox_price.Text) * numericUpDown_food.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Phương thức lấy food_id từ tên món ăn
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

        // Phương thức lấy bill_id hiện tại của bàn
        private int GetCurrentBillId()
        {
            int billId = -1;

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();

                string query = "SELECT bill_id FROM bill WHERE MABAN = @MABAN";

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
        private int CreateNewBill()
        {
            int billId = -1;
            string maKhach = comboBoxkhach.Text; // Lấy tên khách hàng từ comboBoxkhach

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

                // Nếu MAKH tồn tại, tiếp tục tạo hóa đơn mới
                string insertBillQuery = "INSERT INTO bill (payment_time, MABAN, MAKH, total) " +
                                         "VALUES (GETDATE(), @MABAN, @MAKH, 0); " +  // Trạng thái 'Pending'
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




        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    //if (!row.IsNewRow)
                    //   {
                    // Xóa hàng
                    dgv.Rows.Remove(row);
                    //    }
                    button_delete.Enabled = false;
                    button_edit.Enabled = false;

                }
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
                if (comboBox_food.Text.Length == 0)
                {
                    throw new Exception("Vui lòng chọn đồ ăn để thêm");

                }
                else if (numericUpDown_food.Value <= 0)
                {
                    throw new Exception("Vui lòng thêm số lượng");


                }
                dgv.SelectedRows[0].Cells[0].Value = comboBox_food.Text;
                dgv.SelectedRows[0].Cells[1].Value = numericUpDown_food.Value.ToString();
                dgv.SelectedRows[0].Cells[2].Value = textBox_price.Text;

                comboBox_food.Text = "";
                numericUpDown_food.Value = 0;
                dgv.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void button_datban_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxkhach.SelectedItem != null)
                {
                    if (datban == 0)
                    {
                        button_datban.BackColor = Color.Red;
                        button_datban.Text = "Bàn đang được sử dụng";
                        datban = 1;
                        comboBoxkhach.Enabled = false;
                        panel1.Enabled = true;
                        panel2.Enabled = true;
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
            DialogResult confirmationResult = MessageBox.Show("Bạn chắc chắn muốn thanh toán?", "Thông báo", MessageBoxButtons.YesNo);
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

                // Tạo lệnh SQL để tìm MAKH từ TENKH
                string findCustomerSql = "SELECT MAKH FROM KHACH WHERE TENKH = @TENKH";
                cmd = connection.CreateCommand();
                cmd.CommandText = findCustomerSql;
                cmd.Parameters.AddWithValue("@TENKH", comboBoxkhach.Text);  // Lấy tên khách hàng từ comboBox

                // Thực hiện truy vấn để lấy MAKH từ TENKH
                object customerId = cmd.ExecuteScalar();
                if (customerId == null)
                {
                    // Nếu không tìm thấy mã khách hàng tương ứng với tên, thông báo lỗi và dừng lại
                    MessageBox.Show("Không tìm thấy khách hàng với tên này.");
                    return;
                }

                // Lấy MAKH từ kết quả truy vấn
                string maKhach = customerId.ToString();

                // Kiểm tra xem hóa đơn đã tồn tại chưa
                string checkBillQuery = "SELECT bill_id FROM bill WHERE MABAN = @MABAN";
                cmd.CommandText = checkBillQuery;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@MABAN", tableName);  // Lấy tên bàn hiện tại

                object existingBillId = cmd.ExecuteScalar();
                int billId = -1;

                if (existingBillId != null)
                {
                    billId = Convert.ToInt32(existingBillId);  // Nếu hóa đơn đã tồn tại, lấy bill_id hiện tại
                }
                else
                {
                    // Nếu không có hóa đơn, tạo hóa đơn mới
                    string insertBillSql = "INSERT INTO bill (payment_time, MABAN, MAKH, total) " +
                                            "VALUES (GETDATE(), @TENBAN, @MAKH, 0); " +
                                            "SELECT SCOPE_IDENTITY();";  // Ghi giá trị 0 vào total lúc tạo hóa đơn

                    cmd.CommandText = insertBillSql;
                    cmd.Parameters.Clear();  // Xóa các tham số cũ
                    cmd.Parameters.AddWithValue("@TENBAN", tableName);
                    cmd.Parameters.AddWithValue("@MAKH", maKhach);  // Sử dụng MAKH vừa tìm được

                    // Lấy bill_id vừa được chèn vào bảng bill
                    object billIdObj = cmd.ExecuteScalar();
                    if (billIdObj != null)
                    {
                        billId = Convert.ToInt32(billIdObj);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi tạo hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
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

                // Truyền giá trị total vào textBox_total
                textBox_total.Text = total.ToString("N2");  // Định dạng số thành 2 chữ số thập phân

                // Hiển thị thông báo thanh toán thành công
                MessageBox.Show("Đã thanh toán thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa dữ liệu trong file nếu có
                string filePath = "C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    MessageBox.Show("Đã xóa dữ liệu từ file.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
