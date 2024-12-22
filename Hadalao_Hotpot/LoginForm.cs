using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hadalao_Hotpot
{
    public partial class LoginForm : Form
    {

        string connectionSTR = @"Data Source=DESKTOP-B87EC4S;Initial Catalog=QUANLYLAU;Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
        }

        private bool checkUser()
        {

            string username = textBox1.Text;
            string password = textBox2.Text;

            string query = "SELECT COUNT(*) FROM NVQUAN WHERE emp_username = @Username AND emp_password = @passwordd";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    if(connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@passwordd", password);

                        int userCount = (int)command.ExecuteScalar();

                        return userCount > 0;
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void username_text_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_label_Click(object sender, EventArgs e)
        {

        }

        private void password_label_Click(object sender, EventArgs e)
        {

        }

        private void login_button_Click(object sender, EventArgs e)
        {
            String MaNV = "";
            if (textBox1.Text == "admin" && textBox2.Text == "admin" && ChangeButton.Text == "Admin")
            {
                this.Hide();
                AdminDashboard ad = new AdminDashboard();
                ad.role = "admin";
                ad.FormClosed += (s, args) => this.Show();
                ad.ShowDialog();
            }
            else if (ChangeButton.Text == "User")
            {   
               
                if(checkUser()  == true)
                {
                    string username = textBox1.Text;
                    string password = textBox2.Text;
                    string query = "select * from NVQUAN WHERE emp_username = @Username AND emp_password = @passwordd";
                    using (SqlConnection connection = new SqlConnection(connectionSTR))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@passwordd", password);
                        SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                MaNV = reader["emp_id"].ToString();

                            }

                        }

                    }


                    this.Hide();
                    AdminDashboard mng = new AdminDashboard();
                    mng.role = "user";
                    mng.ID = MaNV;
                    mng.FormClosed += (s, args) => this.Show();
                    
                    mng.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                }
            else
            {
                MessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if(ChangeButton.Text == "Admin")
            {
                ChangeButton.Text = "User";
            }
            else if(ChangeButton.Text == "User")
            {
                ChangeButton.Text = "Admin";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
