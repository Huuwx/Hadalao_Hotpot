namespace Hadalao_Hotpot
{
    partial class EditTableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_price = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_thanhtoan = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_edit = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.comboBox_food = new System.Windows.Forms.ComboBox();
            this.numericUpDown_food = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label_fname = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxkhach = new System.Windows.Forms.ComboBox();
            this.textBox_total = new System.Windows.Forms.TextBox();
            this.label_total = new System.Windows.Forms.Label();
            this.button_datban = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ten_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soluong_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_food)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.63363F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.36637F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.01156F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.98844F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(797, 499);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(287, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 364);
            this.panel1.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ten_col,
            this.soluong_col,
            this.price_col});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(507, 364);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_price);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button_thanhtoan);
            this.panel2.Controls.Add(this.button_delete);
            this.panel2.Controls.Add(this.button_edit);
            this.panel2.Controls.Add(this.button_add);
            this.panel2.Controls.Add(this.comboBox_food);
            this.panel2.Controls.Add(this.numericUpDown_food);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label_fname);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 364);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // textBox_price
            // 
            this.textBox_price.Location = new System.Drawing.Point(115, 76);
            this.textBox_price.Name = "textBox_price";
            this.textBox_price.ReadOnly = true;
            this.textBox_price.Size = new System.Drawing.Size(120, 20);
            this.textBox_price.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Giá";
            // 
            // button_thanhtoan
            // 
            this.button_thanhtoan.Location = new System.Drawing.Point(79, 305);
            this.button_thanhtoan.Name = "button_thanhtoan";
            this.button_thanhtoan.Size = new System.Drawing.Size(108, 40);
            this.button_thanhtoan.TabIndex = 7;
            this.button_thanhtoan.Text = "Xác nhận";
            this.button_thanhtoan.UseVisualStyleBackColor = true;
            this.button_thanhtoan.Click += new System.EventHandler(this.button_thanhtoan_Click);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(195, 205);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(75, 23);
            this.button_delete.TabIndex = 6;
            this.button_delete.Text = "Xóa";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_edit
            // 
            this.button_edit.Location = new System.Drawing.Point(103, 205);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(75, 23);
            this.button_edit.TabIndex = 5;
            this.button_edit.Text = "Sửa";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(12, 205);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 4;
            this.button_add.Text = "Thêm";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // comboBox_food
            // 
            this.comboBox_food.FormattingEnabled = true;
            this.comboBox_food.Location = new System.Drawing.Point(115, 31);
            this.comboBox_food.Name = "comboBox_food";
            this.comboBox_food.Size = new System.Drawing.Size(121, 21);
            this.comboBox_food.TabIndex = 3;
            this.comboBox_food.SelectedIndexChanged += new System.EventHandler(this.comboBox_food_SelectedIndexChanged);
            // 
            // numericUpDown_food
            // 
            this.numericUpDown_food.Location = new System.Drawing.Point(115, 118);
            this.numericUpDown_food.Name = "numericUpDown_food";
            this.numericUpDown_food.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_food.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Số lượng";
            // 
            // label_fname
            // 
            this.label_fname.AutoSize = true;
            this.label_fname.Location = new System.Drawing.Point(9, 31);
            this.label_fname.Name = "label_fname";
            this.label_fname.Size = new System.Drawing.Size(26, 13);
            this.label_fname.TabIndex = 0;
            this.label_fname.Text = "Tên";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comboBoxkhach);
            this.panel3.Controls.Add(this.textBox_total);
            this.panel3.Controls.Add(this.label_total);
            this.panel3.Controls.Add(this.button_datban);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(287, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 123);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // comboBoxkhach
            // 
            this.comboBoxkhach.FormattingEnabled = true;
            this.comboBoxkhach.Location = new System.Drawing.Point(95, 35);
            this.comboBoxkhach.Name = "comboBoxkhach";
            this.comboBoxkhach.Size = new System.Drawing.Size(162, 21);
            this.comboBoxkhach.TabIndex = 5;
            this.comboBoxkhach.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox_total
            // 
            this.textBox_total.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_total.ForeColor = System.Drawing.Color.Lime;
            this.textBox_total.Location = new System.Drawing.Point(95, 77);
            this.textBox_total.Name = "textBox_total";
            this.textBox_total.ReadOnly = true;
            this.textBox_total.Size = new System.Drawing.Size(157, 35);
            this.textBox_total.TabIndex = 4;
            this.textBox_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_total.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_total
            // 
            this.label_total.AutoSize = true;
            this.label_total.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_total.Location = new System.Drawing.Point(6, 87);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(83, 21);
            this.label_total.TabIndex = 3;
            this.label_total.Text = "Tổng tiền";
            // 
            // button_datban
            // 
            this.button_datban.Location = new System.Drawing.Point(358, 27);
            this.button_datban.Name = "button_datban";
            this.button_datban.Size = new System.Drawing.Size(140, 33);
            this.button_datban.TabIndex = 2;
            this.button_datban.Text = "Đặt bàn";
            this.button_datban.UseVisualStyleBackColor = true;
            this.button_datban.Click += new System.EventHandler(this.button_datban_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên khách hàng";
            // 
            // bindingSource1
            // 
            this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // ten_col
            // 
            this.ten_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ten_col.HeaderText = "Tên";
            this.ten_col.MinimumWidth = 6;
            this.ten_col.Name = "ten_col";
            this.ten_col.ReadOnly = true;
            // 
            // soluong_col
            // 
            this.soluong_col.HeaderText = "Số lượng";
            this.soluong_col.MinimumWidth = 6;
            this.soluong_col.Name = "soluong_col";
            this.soluong_col.ReadOnly = true;
            this.soluong_col.Width = 125;
            // 
            // price_col
            // 
            this.price_col.HeaderText = "Giá tiền";
            this.price_col.MinimumWidth = 6;
            this.price_col.Name = "price_col";
            this.price_col.ReadOnly = true;
            this.price_col.Width = 125;
            // 
            // EditTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 499);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditTableForm";
            this.Text = "EditTableForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditTableForm_FormClosing);
            this.Load += new System.EventHandler(this.EditTableForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_food)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_fname;
        private System.Windows.Forms.NumericUpDown numericUpDown_food;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_food;
        private System.Windows.Forms.Button button_thanhtoan;
        private System.Windows.Forms.Button button_datban;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox textBox_price;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.TextBox textBox_total;
        private System.Windows.Forms.ComboBox comboBoxkhach;
        private System.Windows.Forms.DataGridViewTextBoxColumn ten_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn soluong_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn price_col;
    }
}