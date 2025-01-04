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
            this.ten_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soluong_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.01156F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.98844F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1063, 614);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(382, 163);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 447);
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
            this.Column_price});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(4);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(677, 447);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
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
            // Column_price
            // 
            this.Column_price.HeaderText = "Giá tiền";
            this.Column_price.MinimumWidth = 6;
            this.Column_price.Name = "Column_price";
            this.Column_price.ReadOnly = true;
            this.Column_price.Width = 125;
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
            this.panel2.Location = new System.Drawing.Point(4, 163);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 447);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // textBox_price
            // 
            this.textBox_price.Location = new System.Drawing.Point(153, 94);
            this.textBox_price.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_price.Name = "textBox_price";
            this.textBox_price.ReadOnly = true;
            this.textBox_price.Size = new System.Drawing.Size(159, 22);
            this.textBox_price.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Giá";
            // 
            // button_thanhtoan
            // 
            this.button_thanhtoan.Location = new System.Drawing.Point(105, 375);
            this.button_thanhtoan.Margin = new System.Windows.Forms.Padding(4);
            this.button_thanhtoan.Name = "button_thanhtoan";
            this.button_thanhtoan.Size = new System.Drawing.Size(144, 49);
            this.button_thanhtoan.TabIndex = 7;
            this.button_thanhtoan.Text = "Xác nhận";
            this.button_thanhtoan.UseVisualStyleBackColor = true;
            this.button_thanhtoan.Click += new System.EventHandler(this.button_thanhtoan_Click);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(260, 252);
            this.button_delete.Margin = new System.Windows.Forms.Padding(4);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(100, 28);
            this.button_delete.TabIndex = 6;
            this.button_delete.Text = "Xóa";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_edit
            // 
            this.button_edit.Location = new System.Drawing.Point(137, 252);
            this.button_edit.Margin = new System.Windows.Forms.Padding(4);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(100, 28);
            this.button_edit.TabIndex = 5;
            this.button_edit.Text = "Sửa";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(16, 252);
            this.button_add.Margin = new System.Windows.Forms.Padding(4);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(100, 28);
            this.button_add.TabIndex = 4;
            this.button_add.Text = "Thêm";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // comboBox_food
            // 
            this.comboBox_food.FormattingEnabled = true;
            this.comboBox_food.Location = new System.Drawing.Point(153, 38);
            this.comboBox_food.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_food.Name = "comboBox_food";
            this.comboBox_food.Size = new System.Drawing.Size(160, 24);
            this.comboBox_food.TabIndex = 3;
            this.comboBox_food.SelectedIndexChanged += new System.EventHandler(this.comboBox_food_SelectedIndexChanged);
            // 
            // numericUpDown_food
            // 
            this.numericUpDown_food.Location = new System.Drawing.Point(153, 145);
            this.numericUpDown_food.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_food.Name = "numericUpDown_food";
            this.numericUpDown_food.Size = new System.Drawing.Size(160, 22);
            this.numericUpDown_food.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Số lượng";
            // 
            // label_fname
            // 
            this.label_fname.AutoSize = true;
            this.label_fname.Location = new System.Drawing.Point(12, 38);
            this.label_fname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_fname.Name = "label_fname";
            this.label_fname.Size = new System.Drawing.Size(31, 16);
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
            this.panel3.Location = new System.Drawing.Point(382, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(677, 151);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // comboBoxkhach
            // 
            this.comboBoxkhach.FormattingEnabled = true;
            this.comboBoxkhach.Location = new System.Drawing.Point(127, 43);
            this.comboBoxkhach.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxkhach.Name = "comboBoxkhach";
            this.comboBoxkhach.Size = new System.Drawing.Size(214, 24);
            this.comboBoxkhach.TabIndex = 5;
            this.comboBoxkhach.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox_total
            // 
            this.textBox_total.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_total.ForeColor = System.Drawing.Color.Lime;
            this.textBox_total.Location = new System.Drawing.Point(127, 95);
            this.textBox_total.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_total.Name = "textBox_total";
            this.textBox_total.ReadOnly = true;
            this.textBox_total.Size = new System.Drawing.Size(208, 42);
            this.textBox_total.TabIndex = 4;
            this.textBox_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_total.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_total
            // 
            this.label_total.AutoSize = true;
            this.label_total.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_total.Location = new System.Drawing.Point(8, 107);
            this.label_total.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(103, 28);
            this.label_total.TabIndex = 3;
            this.label_total.Text = "Tổng tiền";
            // 
            // button_datban
            // 
            this.button_datban.Location = new System.Drawing.Point(477, 33);
            this.button_datban.Margin = new System.Windows.Forms.Padding(4);
            this.button_datban.Name = "button_datban";
            this.button_datban.Size = new System.Drawing.Size(187, 41);
            this.button_datban.TabIndex = 2;
            this.button_datban.Text = "Đặt bàn";
            this.button_datban.UseVisualStyleBackColor = true;
            this.button_datban.Click += new System.EventHandler(this.button_datban_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên khách hàng";
            // 
            // bindingSource1
            // 
            this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // EditTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 614);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ten_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn soluong_col;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_food;
        private System.Windows.Forms.Button button_thanhtoan;
        private System.Windows.Forms.Button button_datban;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox textBox_price;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_price;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.TextBox textBox_total;
        private System.Windows.Forms.ComboBox comboBoxkhach;
    }
}