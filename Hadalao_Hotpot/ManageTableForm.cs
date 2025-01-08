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
    public partial class ManageTableForm : Form
    {

        public string id { get; set; }

        public ManageTableForm(String ID)
        {
            InitializeComponent();
            id = ID;
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            EditTableForm editTableForm = new EditTableForm();
            editTableForm.userID = this.id;
            editTableForm.tableName = clickedButton.Text;
            editTableForm.ShowDialog();

        }
        private void ManageTableForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
