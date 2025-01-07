using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Hadalao_Hotpot
{
    public partial class HoaDonForm : Form
    {
        string chuoiketnoi = "Data Source=DESKTOP-4UUFE49;Initial Catalog=QUANLYLAU;TrustServerCertificate=true;Integrated Security=True";

        public HoaDonForm()
        {
            InitializeComponent();
        }


        // Tải dữ liệu từ view khi form được load
        private void HoaDonForm_Load(object sender, EventArgs e)
        {
            // Gọi phương thức để truy vấn và hiển thị dữ liệu trong DataGridView
            LoadBillDetails();
            LoadTongThuNhap();
        }
        private void LoadTongThuNhap()
        {
            try
            {
                // Chuỗi kết nối tới cơ sở dữ liệu
                string connectionString = chuoiketnoi;

                // Tên stored procedure
                string query = "pr_GetTongThuNhap"; // Gọi stored procedure

                // Tạo kết nối và thực thi câu lệnh SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand để thực thi stored procedure
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure; // Xác định là gọi stored procedure

                    // Mở kết nối
                    connection.Open();

                    // Thực thi câu lệnh SQL và lấy giá trị trả về
                    object result = command.ExecuteScalar();

                    // Kiểm tra xem kết quả có null hay không
                    if (result != null)
                    {
                        // Chuyển đổi kết quả trả về thành kiểu decimal
                        decimal totalIncome = Convert.ToDecimal(result);

                        // Hiển thị kết quả trong TextBox (định dạng tiền tệ)
                        textBoxTongThuNhap.Text = totalIncome.ToString("C2"); // Định dạng tiền tệ
                    }
                    else
                    {
                        // Nếu kết quả trả về null, hiển thị thông báo lỗi
                        MessageBox.Show("Không thể tính tổng thu nhập. Vui lòng kiểm tra dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Hiển thị thông báo lỗi nếu có sự cố với cơ sở dữ liệu
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có bất kỳ lỗi nào khác
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức để tải dữ liệu từ view vw_BillDetails
        private void LoadBillDetails()
        {
            // Sử dụng chuỗi kết nối sẵn có
            string connectionString = chuoiketnoi;

            // Tạo câu lệnh SQL để truy vấn dữ liệu từ view vw_BillDetails
            string query = "select *from vw_Billall";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo DataAdapter và DataTable
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // Điền dữ liệu vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Gán DataTable vào DataGridView
                    dataGridView1.DataSource = dataTable;  // Gán dữ liệu mới cho DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);  // Nếu có lỗi trong quá trình lấy dữ liệu
                }
            }
        }

        // Xử lý sự kiện khi người dùng nhấn vào ô trong DataGridView (nếu cần)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào một ô dữ liệu trong DataGridView (không phải tiêu đề cột)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Lấy bill_id từ dòng đã chọn trong DataGridView
                int billId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["bill_id"].Value);

                // Cập nhật hoặc xử lý các thông tin chi tiết hóa đơn (ví dụ: mở thông tin chi tiết, thanh toán)
                //ShowBillDetails(billId);
            }
        }

        private void ShowBillDetails(int billId)
        {
            // Dùng câu lệnh SQL để lấy chi tiết hóa đơn từ bill_info và các bảng liên quan
            string query = "select *from vw_Billall";

            // Kết nối đến cơ sở dữ liệu và lấy chi tiết hóa đơn
            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BillId", billId);

                    // Sử dụng SqlDataReader để lấy dữ liệu chi tiết hóa đơn
                    SqlDataReader reader = command.ExecuteReader();

                    // Hiển thị thông tin chi tiết hóa đơn (ví dụ: hiển thị trong DataGridView hoặc form khác)
                    // Bạn có thể xử lý và hiển thị dữ liệu chi tiết hóa đơn ở đây.
                    while (reader.Read())
                    {
                        string foodName = reader["food_name"].ToString();
                        int quantity = Convert.ToInt32(reader["quantity"]);
                        decimal price = Convert.ToDecimal(reader["price"]);
                        decimal total = quantity * price;

                        // In ra thông tin chi tiết món ăn (chỉ là ví dụ, bạn có thể hiển thị nó ở nơi khác)
                        Console.WriteLine($"Food: {foodName}, Quantity: {quantity}, Price: {price}, Total: {total}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem textBox1 có trống không hoặc có phải là một số hợp lệ
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int billId;
                if (!int.TryParse(textBox1.Text, out billId))  // Kiểm tra nếu mã hóa đơn là một số hợp lệ
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ, vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    connection.Open();

                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = "select *from vw_Billall WHERE bill_id = @bill_id";
                    sqlCommand.Parameters.AddWithValue("@bill_id", billId); // Sử dụng billId đã kiểm tra hợp lệ

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();

                    // Clear DataTable trước khi điền dữ liệu mới
                    dataTable.Clear();
                    adapter.Fill(dataTable);

                    // Gán DataTable cho DataGridView
                    dataGridView1.DataSource = dataTable;

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin hóa đơn với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show("Đã xảy ra lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Phương thức vẽ giao diện, nếu không cần có thể để trống
        }

        private void textBoxTongThuNhap_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Chuỗi kết nối tới cơ sở dữ liệu
                string connectionString = chuoiketnoi;  // Đảm bảo 'chuoiketnoi' là chuỗi kết nối đúng

                // Câu lệnh SQL để gọi hàm fn_TongThuNhap
                string query = "SELECT dbo.fn_TongThuNhap()";

                // Tạo kết nối và thực thi câu lệnh SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand để thực thi truy vấn
                    SqlCommand command = new SqlCommand(query, connection);

                    // Mở kết nối
                    connection.Open();

                    // Thực thi câu lệnh SQL và lấy giá trị trả về
                    object result = command.ExecuteScalar();

                    // Kiểm tra xem kết quả có null hay không
                    if (result != null)
                    {
                        // Chuyển đổi kết quả trả về thành kiểu decimal
                        decimal totalIncome = Convert.ToDecimal(result);

                        // Hiển thị kết quả trong TextBox
                        textBoxTongThuNhap.Text = totalIncome.ToString("C2");  // Định dạng dưới dạng tiền tệ
                    }
                    else
                    {
                        // Nếu kết quả trả về null, hiển thị thông báo lỗi
                        MessageBox.Show("Không thể tính tổng thu nhập. Vui lòng kiểm tra dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Hiển thị thông báo lỗi nếu có sự cố với cơ sở dữ liệu
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có bất kỳ lỗi nào khác
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có chọn dòng nào trong DataGridView hay không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Thực hiện xóa từng dòng đã chọn
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Kiểm tra nếu không phải là dòng mới (dòng trống trong DataGridView)
                    if (!row.IsNewRow)
                    {
                        // Lấy giá trị ID của hóa đơn từ dòng được chọn để xóa trong cơ sở dữ liệu
                        string billId = row.Cells["bill_id"].Value.ToString(); // Giả sử "bill_id" là tên cột chứa ID hóa đơn

                        // Kết nối với cơ sở dữ liệu và thực hiện lệnh xóa trên bảng gốc (bill_info)
                        using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                        {
                            try
                            {
                                connection.Open();
                                // Thực hiện lệnh DELETE từ bảng bill_info thay vì từ VIEW
                                string deleteQuery = "DELETE FROM bill_info WHERE bill_id = @bill_id"; // Câu lệnh SQL xóa
                                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                                deleteCommand.Parameters.AddWithValue("@bill_id", billId);

                                int rowsAffected = deleteCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Xóa hóa đơn thành công!");
                                    // Tải lại dữ liệu sau khi xóa
                                    LoadBillDetails();
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy hóa đơn với ID: " + billId);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Câu lệnh SQL để truy vấn dữ liệu từ view 'vw_Bill_dathanhtoan'
            string query = "SELECT * FROM vw_Bill_dathanhtoan";

            // Chuỗi kết nối cơ sở dữ liệu
            string connectionString = chuoiketnoi;  // Đảm bảo chuoiketnoi là chuỗi kết nối đúng

            // Tạo kết nối và SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo SqlDataAdapter để thực thi câu lệnh SQL
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Tạo DataTable để chứa kết quả truy vấn
                    DataTable dataTable = new DataTable();

                    // Điền dữ liệu vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Gán DataTable cho DataGridView để hiển thị dữ liệu
                    dataGridView1.DataSource = dataTable;
                }
                catch (SqlException ex)
                {
                    // Nếu có lỗi khi truy vấn, hiển thị thông báo lỗi
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi khác, hiển thị thông báo lỗi chung
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có chọn bất kỳ dòng nào trong DataGridView không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Duyệt qua từng dòng đã chọn
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Kiểm tra nếu không phải là dòng trống (dòng không chứa dữ liệu)
                    if (!row.IsNewRow)
                    {
                        // Lấy giá trị ID của hóa đơn từ dòng đã chọn
                        string billId = row.Cells["bill_id"].Value.ToString(); // "bill_id" là tên cột chứa ID hóa đơn

                        // Tạo câu lệnh xóa từ cơ sở dữ liệu
                        string deleteQuery = @"
                    DELETE FROM bill_info WHERE bill_id = @bill_id; 
                    DELETE FROM bill WHERE bill_id = @bill_id;";

                        // Kết nối cơ sở dữ liệu để thực hiện lệnh xóa
                        using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                        {
                            try
                            {
                                connection.Open();
                                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                                deleteCommand.Parameters.AddWithValue("@bill_id", billId);

                                // Thực hiện câu lệnh xóa
                                int rowsAffected = deleteCommand.ExecuteNonQuery();

                                // Kiểm tra kết quả xóa
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Xóa hóa đơn thành công!");

                                    // Xóa dòng khỏi DataGridView (giúp nó không còn xuất hiện)
                                    dataGridView1.Rows.Remove(row);

                                    // Tải lại dữ liệu sau khi xóa (hoặc làm mới DataGridView nếu cần)
                                    LoadBillDetails();  // Nếu bạn muốn tải lại toàn bộ bảng sau khi xóa
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy hóa đơn với ID: " + billId);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                // Thông báo nếu không có dòng nào được chọn để xóa
                MessageBox.Show("Vui lòng chọn dòng để xóa.");
            }
        }



        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có dòng nào được chọn trong DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID của hóa đơn từ dòng đã chọn
                int billId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["bill_id"].Value);  // Giả sử "bill_id" là tên cột chứa ID hóa đơn

                // Câu lệnh SQL để cập nhật trạng thái của hóa đơn
                string query = "UPDATE bill SET bill_status = @status WHERE bill_id = @billId";

                // Chuỗi kết nối cơ sở dữ liệu
                string connectionString = chuoiketnoi;

                // Kết nối với cơ sở dữ liệu và thực thi câu lệnh cập nhật
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Mở kết nối với cơ sở dữ liệu
                        connection.Open();

                        // Tạo SqlCommand để thực thi câu lệnh SQL
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@status", "Đã thanh toán");  // Trạng thái mới là "Đã thanh toán"
                        command.Parameters.AddWithValue("@billId", billId);  // Tham số billId

                        // Thực thi câu lệnh cập nhật
                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra xem có hóa đơn nào bị cập nhật hay không
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Hóa đơn đã được thanh toán!");

                            // Tải lại dữ liệu sau khi cập nhật trạng thái
                            LoadBillDetails();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hóa đơn với ID: " + billId);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi trong quá trình cập nhật
                        MessageBox.Show("Lỗi khi cập nhật trạng thái hóa đơn: " + ex.Message);
                    }
                }
            }
            else
            {
                // Thông báo nếu không có dòng nào được chọn để thanh toán
                MessageBox.Show("Vui lòng chọn một hóa đơn để thanh toán.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadBillDetails();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Câu lệnh SQL để truy vấn dữ liệu từ view 'vw_Bill_dathanhtoan'
            string query = "SELECT * FROM vw_Bill_chuathanhtoan";

            // Chuỗi kết nối cơ sở dữ liệu
            string connectionString = chuoiketnoi;  // Đảm bảo chuoiketnoi là chuỗi kết nối đúng

            // Tạo kết nối và SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo SqlDataAdapter để thực thi câu lệnh SQL
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Tạo DataTable để chứa kết quả truy vấn
                    DataTable dataTable = new DataTable();

                    // Điền dữ liệu vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Gán DataTable cho DataGridView để hiển thị dữ liệu
                    dataGridView1.DataSource = dataTable;
                }
                catch (SqlException ex)
                {
                    // Nếu có lỗi khi truy vấn, hiển thị thông báo lỗi
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi khác, hiển thị thông báo lỗi chung
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
