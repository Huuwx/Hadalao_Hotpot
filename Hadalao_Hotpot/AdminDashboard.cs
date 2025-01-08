using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hadalao_Hotpot
{
    public partial class AdminDashboard : Form
    {
        bool sidebarExpand = true;
        public String role { get; set; }
        public String ID { get; set; }
        public AdminDashboard()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void openChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();//Nếu có form con khác thì tắt đi
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None; // tắt border
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new HoaDonForm());
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            //if (this.role != "admin")
            //{
            //    button2.Enabled = false;
            //    button5.Enabled = false;
            //}
            //else
            //{
            //    button3.Enabled = false;
            //}
        }


        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)// nếu mở rộng rồi thì thu bé vào
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageFoodForm());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageTableForm(this.ID));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms1());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new QuanlyNhanvien1());
        }
    }
}
