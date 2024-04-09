namespace HeThongQuanLyCaPhe
{
    partial class ChinhSuaChiTietDatBan
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
            this.DuLieuChiTietDatBanXem = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.DuLieuChiTietDatBanSua = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuChiTietDatBanXem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuChiTietDatBanSua)).BeginInit();
            this.SuspendLayout();
            // 
            // DuLieuChiTietDatBanXem
            // 
            this.DuLieuChiTietDatBanXem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DuLieuChiTietDatBanXem.Location = new System.Drawing.Point(37, 84);
            this.DuLieuChiTietDatBanXem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DuLieuChiTietDatBanXem.Name = "DuLieuChiTietDatBanXem";
            this.DuLieuChiTietDatBanXem.RowHeadersWidth = 51;
            this.DuLieuChiTietDatBanXem.RowTemplate.Height = 24;
            this.DuLieuChiTietDatBanXem.Size = new System.Drawing.Size(727, 338);
            this.DuLieuChiTietDatBanXem.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(297, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 35);
            this.label1.TabIndex = 4;
            this.label1.Text = "Danh Sách Bàn ";
            // 
            // DuLieuChiTietDatBanSua
            // 
            this.DuLieuChiTietDatBanSua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DuLieuChiTietDatBanSua.Location = new System.Drawing.Point(37, 84);
            this.DuLieuChiTietDatBanSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DuLieuChiTietDatBanSua.Name = "DuLieuChiTietDatBanSua";
            this.DuLieuChiTietDatBanSua.RowHeadersWidth = 51;
            this.DuLieuChiTietDatBanSua.RowTemplate.Height = 24;
            this.DuLieuChiTietDatBanSua.Size = new System.Drawing.Size(727, 338);
            this.DuLieuChiTietDatBanSua.TabIndex = 3;
            this.DuLieuChiTietDatBanSua.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DuLieuChiTietDatBanSua_CellClick);
            this.DuLieuChiTietDatBanSua.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DuLieuChiTietDatBanSua_CellValueChanged);
            this.DuLieuChiTietDatBanSua.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DuLieuChiTietDatBanSua_RowsRemoved);
            // 
            // ChinhSuaChiTietDatBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DuLieuChiTietDatBanXem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DuLieuChiTietDatBanSua);
            this.Name = "ChinhSuaChiTietDatBan";
            this.Text = "ChinhSuaChiTietDatBan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChinhSuaChiTietDatBan_FormClosing);
            this.Load += new System.EventHandler(this.ChinhSuaChiTietDatBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuChiTietDatBanXem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuChiTietDatBanSua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DuLieuChiTietDatBanXem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DuLieuChiTietDatBanSua;
    }
}