using System.Drawing;

namespace Hadalao_Hotpot
{
    partial class ManageFoodForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tcAdmin = new System.Windows.Forms.TabControl();
            this.tpFood = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtgvFood = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PrintByCursor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.availableFoodBtn = new System.Windows.Forms.Button();
            this.loadbtn = new System.Windows.Forms.Button();
            this.DeleteFoodBtn = new System.Windows.Forms.Button();
            this.EditFoodBtn = new System.Windows.Forms.Button();
            this.AddFoodBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SearchTb = new System.Windows.Forms.TextBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tcAdmin.SuspendLayout();
            this.tpFood.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvFood)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tcAdmin);
            this.panel1.Location = new System.Drawing.Point(16, 14);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 629);
            this.panel1.TabIndex = 0;
            // 
            // tcAdmin
            // 
            this.tcAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcAdmin.Controls.Add(this.tpFood);
            this.tcAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcAdmin.Location = new System.Drawing.Point(4, 4);
            this.tcAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcAdmin.Name = "tcAdmin";
            this.tcAdmin.SelectedIndex = 0;
            this.tcAdmin.Size = new System.Drawing.Size(1028, 617);
            this.tcAdmin.TabIndex = 0;
            // 
            // tpFood
            // 
            this.tpFood.BackColor = System.Drawing.Color.Firebrick;
            this.tpFood.Controls.Add(this.panel4);
            this.tpFood.Controls.Add(this.panel3);
            this.tpFood.Controls.Add(this.panel2);
            this.tpFood.Location = new System.Drawing.Point(4, 25);
            this.tpFood.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpFood.Name = "tpFood";
            this.tpFood.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpFood.Size = new System.Drawing.Size(1020, 588);
            this.tpFood.TabIndex = 0;
            this.tpFood.Text = "Thức ăn";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dtgvFood);
            this.panel4.Location = new System.Drawing.Point(8, 59);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1001, 418);
            this.panel4.TabIndex = 2;
            // 
            // dtgvFood
            // 
            this.dtgvFood.AllowUserToAddRows = false;
            this.dtgvFood.AllowUserToDeleteRows = false;
            this.dtgvFood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvFood.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvFood.Location = new System.Drawing.Point(4, 4);
            this.dtgvFood.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtgvFood.Name = "dtgvFood";
            this.dtgvFood.ReadOnly = true;
            this.dtgvFood.RowHeadersWidth = 62;
            this.dtgvFood.Size = new System.Drawing.Size(995, 400);
            this.dtgvFood.TabIndex = 0;
            this.dtgvFood.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvFood_CellClick);
            this.dtgvFood.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvFood_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.PrintByCursor);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.availableFoodBtn);
            this.panel3.Controls.Add(this.loadbtn);
            this.panel3.Controls.Add(this.DeleteFoodBtn);
            this.panel3.Controls.Add(this.EditFoodBtn);
            this.panel3.Controls.Add(this.AddFoodBtn);
            this.panel3.Location = new System.Drawing.Point(12, 485);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(997, 96);
            this.panel3.TabIndex = 1;
            // 
            // PrintByCursor
            // 
            this.PrintByCursor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintByCursor.BackColor = System.Drawing.Color.MediumAquamarine;
            this.PrintByCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintByCursor.ForeColor = System.Drawing.Color.White;
            this.PrintByCursor.Location = new System.Drawing.Point(804, 48);
            this.PrintByCursor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PrintByCursor.Name = "PrintByCursor";
            this.PrintByCursor.Size = new System.Drawing.Size(181, 37);
            this.PrintByCursor.TabIndex = 6;
            this.PrintByCursor.Text = "Món Đắt Nhất";
            this.PrintByCursor.UseVisualStyleBackColor = false;
            this.PrintByCursor.Click += new System.EventHandler(this.PrintByCursor_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(604, 48);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 37);
            this.button1.TabIndex = 5;
            this.button1.Text = "Các Món Hết";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // availableFoodBtn
            // 
            this.availableFoodBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.availableFoodBtn.BackColor = System.Drawing.Color.Teal;
            this.availableFoodBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableFoodBtn.ForeColor = System.Drawing.Color.White;
            this.availableFoodBtn.Location = new System.Drawing.Point(604, 4);
            this.availableFoodBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.availableFoodBtn.Name = "availableFoodBtn";
            this.availableFoodBtn.Size = new System.Drawing.Size(181, 37);
            this.availableFoodBtn.TabIndex = 4;
            this.availableFoodBtn.Text = "Các Món Còn";
            this.availableFoodBtn.UseVisualStyleBackColor = false;
            this.availableFoodBtn.Click += new System.EventHandler(this.availableFoodBtn_Click);
            // 
            // loadbtn
            // 
            this.loadbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadbtn.BackColor = System.Drawing.Color.Peru;
            this.loadbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadbtn.ForeColor = System.Drawing.Color.White;
            this.loadbtn.Location = new System.Drawing.Point(804, 4);
            this.loadbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadbtn.Name = "loadbtn";
            this.loadbtn.Size = new System.Drawing.Size(181, 37);
            this.loadbtn.TabIndex = 3;
            this.loadbtn.Text = "Tải lại";
            this.loadbtn.UseVisualStyleBackColor = false;
            this.loadbtn.Click += new System.EventHandler(this.loadbtn_Click);
            // 
            // DeleteFoodBtn
            // 
            this.DeleteFoodBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteFoodBtn.BackColor = System.Drawing.Color.Tomato;
            this.DeleteFoodBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteFoodBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteFoodBtn.Location = new System.Drawing.Point(404, 4);
            this.DeleteFoodBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteFoodBtn.Name = "DeleteFoodBtn";
            this.DeleteFoodBtn.Size = new System.Drawing.Size(181, 37);
            this.DeleteFoodBtn.TabIndex = 2;
            this.DeleteFoodBtn.Text = "Xóa";
            this.DeleteFoodBtn.UseVisualStyleBackColor = false;
            this.DeleteFoodBtn.Click += new System.EventHandler(this.DeleteFoodBtn_Click);
            // 
            // EditFoodBtn
            // 
            this.EditFoodBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditFoodBtn.BackColor = System.Drawing.Color.MediumAquamarine;
            this.EditFoodBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditFoodBtn.ForeColor = System.Drawing.Color.White;
            this.EditFoodBtn.Location = new System.Drawing.Point(204, 4);
            this.EditFoodBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EditFoodBtn.Name = "EditFoodBtn";
            this.EditFoodBtn.Size = new System.Drawing.Size(181, 37);
            this.EditFoodBtn.TabIndex = 1;
            this.EditFoodBtn.Text = "Sửa";
            this.EditFoodBtn.UseVisualStyleBackColor = false;
            this.EditFoodBtn.Click += new System.EventHandler(this.EditFoodBtn_Click);
            // 
            // AddFoodBtn
            // 
            this.AddFoodBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddFoodBtn.BackColor = System.Drawing.Color.LimeGreen;
            this.AddFoodBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddFoodBtn.ForeColor = System.Drawing.Color.White;
            this.AddFoodBtn.Location = new System.Drawing.Point(4, 4);
            this.AddFoodBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddFoodBtn.Name = "AddFoodBtn";
            this.AddFoodBtn.Size = new System.Drawing.Size(181, 37);
            this.AddFoodBtn.TabIndex = 0;
            this.AddFoodBtn.Text = "Thêm";
            this.AddFoodBtn.UseVisualStyleBackColor = false;
            this.AddFoodBtn.Click += new System.EventHandler(this.AddFoodBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.SearchTb);
            this.panel2.Controls.Add(this.SearchBtn);
            this.panel2.Location = new System.Drawing.Point(8, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1001, 48);
            this.panel2.TabIndex = 0;
            // 
            // SearchTb
            // 
            this.SearchTb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchTb.ForeColor = System.Drawing.Color.Gray;
            this.SearchTb.Location = new System.Drawing.Point(4, 11);
            this.SearchTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SearchTb.Name = "SearchTb";
            this.SearchTb.Size = new System.Drawing.Size(827, 22);
            this.SearchTb.TabIndex = 1;
            this.SearchTb.Text = "Nhập tên món ăn...";
            // 
            // SearchBtn
            // 
            this.SearchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBtn.ForeColor = System.Drawing.Color.Firebrick;
            this.SearchBtn.Location = new System.Drawing.Point(840, 4);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(157, 37);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Tìm Kiếm";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // ManageFoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(1057, 651);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ManageFoodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý món";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManageFoodForm_FormClosed);
            this.Load += new System.EventHandler(this.ManageFoodForm_Load);
            this.panel1.ResumeLayout(false);
            this.tcAdmin.ResumeLayout(false);
            this.tpFood.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvFood)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tcAdmin;
        private System.Windows.Forms.TabPage tpFood;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button DeleteFoodBtn;
        private System.Windows.Forms.Button EditFoodBtn;
        private System.Windows.Forms.Button AddFoodBtn;
        private System.Windows.Forms.TextBox SearchTb;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.DataGridView dtgvFood;
        private System.Windows.Forms.Button loadbtn;
        private System.Windows.Forms.Button availableFoodBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button PrintByCursor;
    }
}