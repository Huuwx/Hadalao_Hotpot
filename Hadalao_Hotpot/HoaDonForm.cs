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
    public partial class HoaDonForm : Form
    {
        string chuoiketnoi = "Data Source=DESKTOP-B87EC4S;Initial Catalog=QUANLYLAU;TrustServerCertificate=true;Integrated Security=True";
        public HoaDonForm() { 
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HoaDonForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            using(SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();
                
                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM bill_info WHERE bill_id = @bill_id";
                sqlCommand.Parameters.AddWithValue("@bill_id", textBox1.Text);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }

            }catch (SqlException ex)
            {
                MessageBox.Show("Nhập sai mã hóa đơn, vui lòng nhập lại");

            }

        }
    }
}
