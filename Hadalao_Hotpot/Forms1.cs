using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Hadalao_Hotpot
{
    public partial class Forms1 : Form
    {
        string chuoiketnoi = @"Data Source=DESKTOP-1ST7HQB\DANGHUONG;Initial Catalog=QUANLYLAU1;Integrated Security=True";
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
            string sqlSelect = "SELECT MAKH, TENKH,SDT, TUOI, SOLAN,dbo.GetCustomerStatus(SOLAN) as TINHTRANG ,DIEM, Discount FROM KHACH";
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
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(txtMAKH.Text) || string.IsNullOrEmpty(txtTENKH.Text)
                    || string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtTUOI.Text))
                {
                    MessageBox.Show("Dữ liệu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra kiểu dữ liệu hợp lệ
                if (!int.TryParse(txtTUOI.Text, out int tuoi) || tuoi <= 0)
                {
                    MessageBox.Show("Tuổi phải là số nguyên dương!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();

                    // Tạo command để gọi thủ tục lưu khách hàng mới
                    using (SqlCommand cmd = new SqlCommand("AddKhachHangWithCondition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm các tham số vào thủ tục
                        cmd.Parameters.AddWithValue("@MAKH", txtMAKH.Text.Trim());
                        cmd.Parameters.AddWithValue("@TENKH", txtTENKH.Text.Trim());
                        cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@TUOI", tuoi);

                        // Thực thi thủ tục
                        cmd.ExecuteNonQuery();
                    }

                    // Gọi stored procedure UpdateCustomerPointsById để cập nhật điểm cho khách hàng mới
                    using (SqlCommand updateCmd = new SqlCommand("UpdateCustomerPointsById", conn))
                    {
                        updateCmd.CommandType = CommandType.StoredProcedure;

                        // Thêm tham số MAKH để cập nhật điểm cho khách hàng vừa thêm
                        updateCmd.Parameters.AddWithValue("@MAKH", txtMAKH.Text.Trim());

                        // Thực thi thủ tục
                        updateCmd.ExecuteNonQuery();
                    }

                    // Cập nhật giao diện
                    HienThi();

                    // Thông báo thành công
                    MessageBox.Show("Thêm khách hàng và cập nhật điểm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi SQL
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                // Xử lý lỗi kiểu dữ liệu
                MessageBox.Show("Lỗi kiểu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi không xác định
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    txtSOLAN.Text = selectedRow.Cells["SOLAN"].Value.ToString();
                    txtTINHTRANG.Text = selectedRow.Cells["TINHTRANG"].Value.ToString();
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
                    string sqlTIMKIEM = "SELECT * FROM View_TimKiemKhachHang WHERE MAKH = @MAKH";
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
            txtMAKH.Text = "";
            txtTENKH.Text = "";
            txtSDT.Text = "";
            txtTUOI.Text = "";
            HienThi();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra kết nối
                if (conn == null || conn.State == ConnectionState.Closed)
                {
                    conn = new SqlConnection(chuoiketnoi);
                    conn.Open();
                }

                // Lấy giá trị được chọn trong ComboBox
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (selectedValue == "Khách ghé qua nhiều nhất")
                {
                    string sqlCursor = @"
            DECLARE SOLANMAX CURSOR SCROLL FOR
            SELECT TENKH, SOLAN FROM KHACH;

            OPEN SOLANMAX;

            DECLARE @tenkh NVARCHAR(200), @solan INT, @max INT;
            SET @max = 0;

            FETCH FIRST FROM SOLANMAX INTO @tenkh, @solan;
            WHILE (@@FETCH_STATUS = 0)
            BEGIN
                IF (@solan > @max)
                    SET @max = @solan;
                FETCH NEXT FROM SOLANMAX INTO @tenkh, @solan;
            END;

            -- Lấy danh sách khách hàng có số lần bằng @max
            FETCH FIRST FROM SOLANMAX INTO @tenkh, @solan;
            DECLARE @result NVARCHAR(MAX) = '';
            WHILE (@@FETCH_STATUS = 0)
            BEGIN
                IF (@solan = @max)
                    SET @result = @result + @tenkh + ', ';
                FETCH NEXT FROM SOLANMAX INTO @tenkh, @solan;
            END;

            CLOSE SOLANMAX;
            DEALLOCATE SOLANMAX;

            SELECT CAST(@max AS NVARCHAR) AS MaxVisits, @result AS TopCustomers;";

                    SqlCommand cmd = new SqlCommand(sqlCursor, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string maxVisits = reader["MaxVisits"].ToString();
                        string topCustomers = reader["TopCustomers"].ToString();

                        MessageBox.Show($"Số lần ghé nhiều nhất: {maxVisits}\nKhách ghé nhiều nhất: {topCustomers}",
                                        "Thông tin khách hàng",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    reader.Close();
                }
                else
                {
                    string query;
                    switch (selectedValue)
                    {
                        case "Trên 18 tuổi":
                            query = "SELECT * FROM View_KhachTren18";
                            break;
                        case "Dưới 18 tuổi":
                            query = "SELECT * FROM View_KhachDuoi18";
                            break;
                        case "All":
                            query = "SELECT * FROM KHACH";
                            break;
                        default:
                            throw new Exception("Mục không hợp lệ.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnDoiDiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra kết nối
                if (conn == null || conn.State == ConnectionState.Closed)
                {
                    conn = new SqlConnection(chuoiketnoi);
                    conn.Open();
                }

                // Lấy mã khách hàng từ giao diện (giả sử có TextBox hoặc ComboBox)
                string maKH = txtMAKH.Text.Trim(); // Thay txtMaKH bằng tên của điều khiển nhập mã khách hàng

                // Câu lệnh SQL để cập nhật cột Discount theo mã khách hàng
                string sqlUpdateDiscount = @"
        UPDATE KHACH
SET 
  Discount = 
    CASE 
      WHEN DIEM >= 1000 THEN 100
      WHEN DIEM >= 400 THEN 40
      WHEN DIEM >= 300 THEN 30
      WHEN DIEM >= 200 THEN 20
      WHEN DIEM >= 100 THEN 10
      ELSE Discount
    END,
  DIEM = 
    CASE 
      WHEN DIEM >= 1000 THEN DIEM - 1000
      WHEN DIEM >= 400 THEN DIEM - 400
      WHEN DIEM >= 300 THEN DIEM - 300
      WHEN DIEM >= 200 THEN DIEM - 200
      WHEN DIEM >= 100 THEN DIEM - 100
      ELSE DIEM
    END
WHERE DIEM >= 100 AND MAKH = @MAKH";


                // Thực hiện câu lệnh SQL
                using (SqlCommand cmd = new SqlCommand(sqlUpdateDiscount, conn))
                {
                    // Thêm tham số @MAKH
                    cmd.Parameters.AddWithValue("@MAKH", maKH);

                    int rowsAffected = cmd.ExecuteNonQuery(); // Số dòng được cập nhật
                    MessageBox.Show($"Cập nhật thành công {rowsAffected} khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Tải lại dữ liệu trên giao diện
                HienThi();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}




