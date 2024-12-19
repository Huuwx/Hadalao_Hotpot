namespace Hadalao_Hotpot
{
    partial class AddAndEditFoodForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbbTT = new System.Windows.Forms.ComboBox();
            this.lbTT = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.nbudPrice = new System.Windows.Forms.NumericUpDown();
            this.lbPrice = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txbFoodName = new System.Windows.Forms.TextBox();
            this.lbFoodName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nbudFoodId = new System.Windows.Forms.NumericUpDown();
            this.lbMTA = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbudPrice)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbudFoodId)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnAccept);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 394);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Tomato;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(347, 283);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(157, 48);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(75, 283);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(157, 48);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Đồng ý";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbbTT);
            this.panel5.Controls.Add(this.lbTT);
            this.panel5.Location = new System.Drawing.Point(18, 220);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(584, 55);
            this.panel5.TabIndex = 4;
            // 
            // cbbTT
            // 
            this.cbbTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbTT.FormattingEnabled = true;
            this.cbbTT.Items.AddRange(new object[] {
            "Available",
            "Unavailable"});
            this.cbbTT.Location = new System.Drawing.Point(177, 15);
            this.cbbTT.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTT.Name = "cbbTT";
            this.cbbTT.Size = new System.Drawing.Size(232, 24);
            this.cbbTT.TabIndex = 1;
            // 
            // lbTT
            // 
            this.lbTT.AutoSize = true;
            this.lbTT.BackColor = System.Drawing.Color.Transparent;
            this.lbTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTT.ForeColor = System.Drawing.Color.White;
            this.lbTT.Location = new System.Drawing.Point(4, 15);
            this.lbTT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTT.Name = "lbTT";
            this.lbTT.Size = new System.Drawing.Size(131, 25);
            this.lbTT.TabIndex = 0;
            this.lbTT.Text = "Tình Trạng :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.nbudPrice);
            this.panel4.Controls.Add(this.lbPrice);
            this.panel4.Location = new System.Drawing.Point(18, 157);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 55);
            this.panel4.TabIndex = 3;
            // 
            // nbudPrice
            // 
            this.nbudPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbudPrice.Location = new System.Drawing.Point(177, 15);
            this.nbudPrice.Margin = new System.Windows.Forms.Padding(4);
            this.nbudPrice.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.nbudPrice.Name = "nbudPrice";
            this.nbudPrice.Size = new System.Drawing.Size(233, 22);
            this.nbudPrice.TabIndex = 1;
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrice.ForeColor = System.Drawing.Color.White;
            this.lbPrice.Location = new System.Drawing.Point(4, 15);
            this.lbPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(58, 25);
            this.lbPrice.TabIndex = 0;
            this.lbPrice.Text = "Giá :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txbFoodName);
            this.panel3.Controls.Add(this.lbFoodName);
            this.panel3.Location = new System.Drawing.Point(18, 94);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 55);
            this.panel3.TabIndex = 2;
            // 
            // txbFoodName
            // 
            this.txbFoodName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbFoodName.Location = new System.Drawing.Point(177, 15);
            this.txbFoodName.Margin = new System.Windows.Forms.Padding(4);
            this.txbFoodName.Name = "txbFoodName";
            this.txbFoodName.Size = new System.Drawing.Size(232, 22);
            this.txbFoodName.TabIndex = 1;
            this.txbFoodName.TextChanged += new System.EventHandler(this.txbFoodName_TextChanged);
            // 
            // lbFoodName
            // 
            this.lbFoodName.AutoSize = true;
            this.lbFoodName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFoodName.ForeColor = System.Drawing.Color.White;
            this.lbFoodName.Location = new System.Drawing.Point(4, 15);
            this.lbFoodName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFoodName.Name = "lbFoodName";
            this.lbFoodName.Size = new System.Drawing.Size(144, 25);
            this.lbFoodName.TabIndex = 0;
            this.lbFoodName.Text = "Tên Món Ăn :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nbudFoodId);
            this.panel2.Controls.Add(this.lbMTA);
            this.panel2.Location = new System.Drawing.Point(18, 31);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 55);
            this.panel2.TabIndex = 1;
            // 
            // nbudFoodId
            // 
            this.nbudFoodId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbudFoodId.Location = new System.Drawing.Point(177, 15);
            this.nbudFoodId.Margin = new System.Windows.Forms.Padding(4);
            this.nbudFoodId.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.nbudFoodId.Name = "nbudFoodId";
            this.nbudFoodId.Size = new System.Drawing.Size(233, 22);
            this.nbudFoodId.TabIndex = 2;
            // 
            // lbMTA
            // 
            this.lbMTA.AutoSize = true;
            this.lbMTA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMTA.ForeColor = System.Drawing.Color.White;
            this.lbMTA.Location = new System.Drawing.Point(4, 15);
            this.lbMTA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMTA.Name = "lbMTA";
            this.lbMTA.Size = new System.Drawing.Size(143, 25);
            this.lbMTA.TabIndex = 0;
            this.lbMTA.Text = "Mã Thức Ăn :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(471, 362);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 32);
            this.label2.TabIndex = 7;
            this.label2.Text = "Chỉnh sửa";
            // 
            // AddAndEditFoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(624, 395);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAndEditFoodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chỉnh sửa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddAndEditFoodForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddAndEditFoodForm_FormClosed);
            this.Load += new System.EventHandler(this.AddAndEditFoodForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbudPrice)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbudFoodId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbMTA;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbTT;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown nbudPrice;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txbFoodName;
        private System.Windows.Forms.Label lbFoodName;
        private System.Windows.Forms.ComboBox cbbTT;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.NumericUpDown nbudFoodId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}