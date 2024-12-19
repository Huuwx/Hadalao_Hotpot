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

        string chuoiketnoi = "Data Source=DESKTOP-6QPUDLE;Initial Catalog=QUANLYLAU;TrustServerCertificate=true;Integrated Security=True";
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
                if (comboBox_food.Text.Length == 0)
                {
                    throw new Exception("Vui lòng chọn đồ ăn để thêm");
                }
                else if (numericUpDown_food.Value <= 0)
                {
                    throw new Exception("Vui lòng thêm số lượng");

                }
                dgv.Rows.Add(comboBox_food.Text, numericUpDown_food.Value, textBox_price.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
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
                if (textBox_tenkhachhang.TextLength != 0)
                {
                    if (datban == 0)
                    {
                        button_datban.BackColor = Color.Red;
                        button_datban.Text = "Bàn đang được sử dụng";
                        datban = 1;
                        textBox_tenkhachhang.Enabled = false;
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
            int total = 0;
            foreach(DataGridViewRow row in dgv.Rows)
            {
                int quantity = Convert.ToInt32(row.Cells[2].Value);
                int price = Convert.ToInt32(row.Cells[1].Value);
                total += quantity * price;
            }
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thanh toán, hiện tại tổng đang là: " + total, "Thông báo", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
            datban = 0;
            button_datban.BackColor = Color.White;
            button_datban.Text = "Đặt bàn";
            button_datban.Enabled = false;
            thanhtoan = 1;
            //textBox_tenkhachhang.Enabled = true;
            comboBox_food.Text = "";
            numericUpDown_food.Value = 0;
                //textBox_tenkhachhang.Text = "";
                panel1.Enabled = false;
                panel2.Enabled = false;

            textBox_total.Text = total.ToString();
            button_thanhtoan.Enabled = false;
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO bill (payment_time, table_code, emp_id, customer_name,total) VALUES (GETDATE(), @tableName, @emp_id, @customerName, @total);SELECT SCOPE_IDENTITY() AS LastInsertedID;";
                //SCOPE_IDENTITY() AS LastInsertedID;. Điều này sẽ trả về bill_id của bản ghi vừa chèn vào bảng bill.
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@emp_id", this.userID);
                cmd.Parameters.AddWithValue("@customerName", textBox_tenkhachhang.Text);
                cmd.Parameters.AddWithValue("@total", total);

                //cmd.ExecuteNonQuery();
                int billId = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    cmd.CommandText = "INSERT INTO bill_info (bill_id, food_name, quantity) VALUES(@bill_id,@food_name,@quantity);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@bill_id", billId);
                    cmd.Parameters.AddWithValue("@food_name", row.Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@quantity", row.Cells[2].Value);
                    cmd.ExecuteNonQuery();
                }



                MessageBox.Show("Đã thanh toán thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (File.Exists("C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt"))
            {
                File.Delete("C:\\Users\\XuanHoang\\Desktop\\Project\\Hadalao_Hotpot\\Hadalao_Hotpot\\SavedData\\" + tableName + "data.txt");
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
                        writer.WriteLine(textBox_tenkhachhang.Text);
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
                        textBox_tenkhachhang.Text = reader.ReadLine();
                        datban = int.Parse(reader.ReadLine()); // Đọc trạng thái đặt bàn

                        // Đặt trạng thái đặt bàn cho nút
                        if (datban == 1)
                        {
                            button_datban.BackColor = Color.Red;
                            button_datban.Text = "Bàn đang được sử dụng";
                            textBox_tenkhachhang.Enabled = false;
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
    }
}
