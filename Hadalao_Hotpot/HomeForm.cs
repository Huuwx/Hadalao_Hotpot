using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hadalao_Hotpot
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            string chuoiketnoi = "Data Source=DESKTOP-0V5FIJG;Initial Catalog=QUANLYLAU;TrustServerCertificate=true;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                //sqlCommand.CommandText = "SELECT SUM(total) as 'Tổng' FROM bill";
                sqlCommand.CommandText = "SELECT Total as 'Tổng' FROM bill";

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string totalAll = reader["Tổng"].ToString();
                        textBox_totalall.Text = totalAll;
                    }
                    else
                    {
                        textBox_totalall.Text = "0";
                    }
                }

                string sql = " SELECT MONTH(payment_time) AS 'Tháng', SUM(total) AS 'Tổng' FROM bill GROUP BY MONTH(payment_time);";
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                adapter.Fill(dataTable);
                dgv.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HoaDonForm hd = new HoaDonForm();
            hd.ShowDialog();
        }
    }
}
