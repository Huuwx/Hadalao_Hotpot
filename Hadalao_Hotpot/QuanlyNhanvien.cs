using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hadalao_Hotpot
{
    public partial class QuanlyNhanvien1 : Form
    {
        int sua = 0;
        public QuanlyNhanvien1()
        {
            InitializeComponent();
            textMNV.Text = GenerateEmployeeID();
        }
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-0V5FIJG;Initial Catalog=QUANLYLAU;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        
        private bool ktrasohvt()
        {
            string input = textName.Text;
            string pattern = @"\d+";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(input))
            {
                return true;
            }
            else return false;
        }


        private bool checktk()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell cell = row.Cells[2];
                if (cell.Value != null && cell.Value.ToString() == textTK.Text)
                {
                    if (sua == 1)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;

        }

        private bool ktraphone(string phoneNumber)
        {

            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c) && c != ' ' && c != '-' && c != '(' && c != ')')
                {
                    return true;
                }
            }
            return false;
        }


        private bool checkmnv()
        {
            try
            {
                
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();

                    
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM NVQUAN WHERE MANV = @empID";
                    command.Parameters.AddWithValue("@empID", textMNV.Text.Trim());
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException)
            {

                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false; 
        }
        private string GenerateEmployeeID()
        {
            List<string> employeeIDs = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                employeeIDs.Add(row[0].ToString());
            }

            int newID = employeeIDs.Count + 1;
            return "NV"+newID.ToString("0000");
        }
        private void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select MANV as [Mã nhân viên], TENNV as [Họ và tên], username as [Tài khoản], passwordd as [Mật khẩu], NAMSINH as [Ngày sinh] , SDT as [SĐT] , TINHTRANG as [Tình trạng] from NVQUAN ";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }
        private void cleartxt()
        {
            textMNV.Clear();
            textName.Clear();
            textTK.Clear();
            textMK.Clear();
            textPhone.Clear();
            textAdress.Clear();
            textEmail.Clear();
            
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void QuanlyNhanvien1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int i;
                i = dataGridView1.CurrentRow.Index;
                textMNV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                textName.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                textTK.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                textMK.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                textPhone.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                textAdress.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                textEmail.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                textStatus.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {   
                string empID = GenerateEmployeeID();
                textMNV.Text = empID;

                if ((textName.Text.Length == 0 || textStatus.Text.Length == 0 || textMNV.Text.Length == 0 || comboBox1.Text.Length == 0 || textAdress.Text.Length == 0 || textEmail.Text.Length == 0 || textPhone.Text.Length == 0 || textMK.Text.Length == 0 || textTK.Text.Length == 0))
                {
                    MessageBox.Show("Dữ liệu không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (checkmnv())
                {
                    MessageBox.Show("Mã nhân viên không được trùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (checktk())
            {
                MessageBox.Show("Tài khoản không được trùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                else if(ktraphone(textPhone.Text))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {


                    string newEmployeeID = GenerateEmployeeID();
                    textMNV.Text = newEmployeeID;
                    command.CommandText = "insert into NVQUAN values('" + textMNV.Text + "',N'" + textName.Text + "',N'" + textTK.Text + "',N'" + textMK.Text + "',N'" + comboBox1.Text + "',N'" + dateTimePicker1.Value + "',N'" + textPhone.Text + "',N'" + textAdress.Text + "',N'" + textEmail.Text + "',N'" + textStatus.Text + "')";
                    command.ExecuteNonQuery();
                    loaddata();
                   
                    cleartxt();
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException)
           {
                MessageBox.Show("Lỗi dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                catch (FormatException)
               {
                    MessageBox.Show("Nhập không đúng định dạng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sua = 1;
            try
            {

                if ((textName.Text.Length == 0 || textStatus.Text.Length == 0 || textMNV.Text.Length == 0 || comboBox1.Text.Length == 0 || textAdress.Text.Length == 0 || textEmail.Text.Length == 0 || textPhone.Text.Length == 0 || textMK.Text.Length == 0 || textTK.Text.Length == 0))
                {
                    MessageBox.Show("Dữ liệu không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (checktk())
                {
                    MessageBox.Show("Tài khoản không được trùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else if (ktraphone(textPhone.Text))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                 command = connection.CreateCommand();
                command.CommandText = "update NVQUAN set emp_id = N'" +textMNV.Text + "', emp_name = N'" + textName.Text + "', emp_username = N'" + textTK.Text + "', emp_password = N'" + textMK.Text + "',emp_gender = N'" + comboBox1.Text + "',emp_birth = '" + dateTimePicker1.Value + "',emp_phone = N'" + textPhone.Text +  "' , emp_address = '"+textAdress.Text+"', emp_email = '"+textEmail.Text+ "',emp_status = N'" + textStatus.Text + "' where emp_id = '" + textMNV.Text + "'";
                command.ExecuteNonQuery();
                loaddata();
                cleartxt();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Nhập không đúng định dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                MessageBox.Show("Lỗi dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            sua = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if ((textName.Text.Length == 0 || textStatus.Text.Length == 0 || textMNV.Text.Length == 0 || comboBox1.Text.Length == 0 || textAdress.Text.Length == 0 || textEmail.Text.Length == 0 || textPhone.Text.Length == 0 || textMK.Text.Length == 0 || textTK.Text.Length == 0))
                {
                    MessageBox.Show("Dữ liệu không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    command = connection.CreateCommand();
                    command.CommandText = "delete from NVQUAN where MANV = ('" + textMNV.Text + "')";
                    command.ExecuteNonQuery();
                    loaddata();

                    cleartxt();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Lỗi dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Nhập không đúng định dạng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
      
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textSatus_TextChanged(object sender, EventArgs e)
        {

        }
    private void textPhone_KeyPress(object sender, KeyPressEventArgs e)
    {
       
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
