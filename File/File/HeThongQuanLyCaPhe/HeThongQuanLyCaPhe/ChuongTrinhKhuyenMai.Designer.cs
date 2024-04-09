namespace HeThongQuanLyCaPhe
{
    partial class ChuongTrinhKhuyenMai
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DuLieuVoucher = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnDungKM = new System.Windows.Forms.Button();
            this.btnMoiKM = new System.Windows.Forms.Button();
            this.btnThemKM = new System.Windows.Forms.Button();
            this.txtMaGG = new System.Windows.Forms.TextBox();
            this.txtGTGG = new System.Windows.Forms.TextBox();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateUse = new System.Windows.Forms.DateTimePicker();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuVoucher)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DuLieuVoucher);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(325, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(363, 254);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin khuyến mãi";
            // 
            // DuLieuVoucher
            // 
            this.DuLieuVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DuLieuVoucher.Location = new System.Drawing.Point(4, 17);
            this.DuLieuVoucher.Margin = new System.Windows.Forms.Padding(2);
            this.DuLieuVoucher.Name = "DuLieuVoucher";
            this.DuLieuVoucher.RowHeadersWidth = 51;
            this.DuLieuVoucher.RowTemplate.Height = 24;
            this.DuLieuVoucher.Size = new System.Drawing.Size(354, 237);
            this.DuLieuVoucher.TabIndex = 0;
            this.DuLieuVoucher.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DuLieuVoucher_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.btnDungKM);
            this.groupBox1.Controls.Add(this.btnMoiKM);
            this.groupBox1.Controls.Add(this.btnThemKM);
            this.groupBox1.Controls.Add(this.txtMaGG);
            this.groupBox1.Controls.Add(this.txtGTGG);
            this.groupBox1.Controls.Add(this.dateEnd);
            this.groupBox1.Controls.Add(this.dateUse);
            this.groupBox1.Controls.Add(this.numSoLuong);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(314, 254);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chương trình";
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(78, 214);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(66, 29);
            this.btnSua.TabIndex = 13;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnDungKM
            // 
            this.btnDungKM.Location = new System.Drawing.Point(228, 214);
            this.btnDungKM.Margin = new System.Windows.Forms.Padding(2);
            this.btnDungKM.Name = "btnDungKM";
            this.btnDungKM.Size = new System.Drawing.Size(66, 29);
            this.btnDungKM.TabIndex = 12;
            this.btnDungKM.Text = "Dừng";
            this.btnDungKM.UseVisualStyleBackColor = true;
            this.btnDungKM.Click += new System.EventHandler(this.btnDungKM_Click);
            // 
            // btnMoiKM
            // 
            this.btnMoiKM.Location = new System.Drawing.Point(148, 214);
            this.btnMoiKM.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoiKM.Name = "btnMoiKM";
            this.btnMoiKM.Size = new System.Drawing.Size(66, 29);
            this.btnMoiKM.TabIndex = 11;
            this.btnMoiKM.Text = "Mới";
            this.btnMoiKM.UseVisualStyleBackColor = true;
            this.btnMoiKM.Click += new System.EventHandler(this.btnMoiKM_Click);
            // 
            // btnThemKM
            // 
            this.btnThemKM.Location = new System.Drawing.Point(7, 214);
            this.btnThemKM.Margin = new System.Windows.Forms.Padding(2);
            this.btnThemKM.Name = "btnThemKM";
            this.btnThemKM.Size = new System.Drawing.Size(66, 29);
            this.btnThemKM.TabIndex = 10;
            this.btnThemKM.Text = "Thêm";
            this.btnThemKM.UseVisualStyleBackColor = true;
            this.btnThemKM.Click += new System.EventHandler(this.btnThemKM_Click);
            // 
            // txtMaGG
            // 
            this.txtMaGG.Location = new System.Drawing.Point(120, 29);
            this.txtMaGG.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaGG.Name = "txtMaGG";
            this.txtMaGG.Size = new System.Drawing.Size(174, 23);
            this.txtMaGG.TabIndex = 9;
            // 
            // txtGTGG
            // 
            this.txtGTGG.Location = new System.Drawing.Point(120, 65);
            this.txtGTGG.Margin = new System.Windows.Forms.Padding(2);
            this.txtGTGG.Name = "txtGTGG";
            this.txtGTGG.Size = new System.Drawing.Size(174, 23);
            this.txtGTGG.TabIndex = 8;
            // 
            // dateEnd
            // 
            this.dateEnd.Location = new System.Drawing.Point(120, 176);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(174, 23);
            this.dateEnd.TabIndex = 7;
            // 
            // dateUse
            // 
            this.dateUse.Location = new System.Drawing.Point(120, 139);
            this.dateUse.Margin = new System.Windows.Forms.Padding(2);
            this.dateUse.Name = "dateUse";
            this.dateUse.Size = new System.Drawing.Size(174, 23);
            this.dateUse.TabIndex = 6;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(120, 104);
            this.numSoLuong.Margin = new System.Windows.Forms.Padding(2);
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(42, 23);
            this.numSoLuong.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 181);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày hết hạn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ngày áp dụng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số lượng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Giá trị giảm giá";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã giảm giá";
            // 
            // ChuongTrinhKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 267);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChuongTrinhKhuyenMai";
            this.Text = "ChuongTrinhKhuyenMai";
            this.Load += new System.EventHandler(this.ChuongTrinhKhuyenMai_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuVoucher)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DuLieuVoucher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnDungKM;
        private System.Windows.Forms.Button btnMoiKM;
        private System.Windows.Forms.Button btnThemKM;
        private System.Windows.Forms.TextBox txtMaGG;
        private System.Windows.Forms.TextBox txtGTGG;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.DateTimePicker dateUse;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}