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

namespace HeThongQuanLyCaPhe
{
    public partial class ChinhSuaChiTietDatBan : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);
        public string LayMaDatBan { get; set; }
        public int Count { get; set; }

        public DataTable DataSource
        {
            get { return DuLieuChiTietDatBanSua.DataSource as DataTable; }
            set { DuLieuChiTietDatBanSua.DataSource = value; }
        }
        public ChinhSuaChiTietDatBan()
        {
            InitializeComponent();
        }

        private void ChinhSuaChiTietDatBan_Load(object sender, EventArgs e)
        {
            if (Count == 1)
            {
                MessageBox.Show("Danh sách bàn đã chọn của phiếu: " + LayMaDatBan);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = $"SELECT ChiTietPhieuDatBan.*, DatBan.ThoiGianNhanBan FROM ChiTietPhieuDatBan INNER JOIN DatBan ON ChiTietPhieuDatBan.MaDatBan = DatBan.MaDatBan WHERE ChiTietPhieuDatBan.MaDatBan = '{LayMaDatBan}'";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DuLieuChiTietDatBanSua.DataSource = dataTable;
                        DuLieuChiTietDatBanXem.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Danh sách bàn đã chọn của phiếu: " + LayMaDatBan);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = $"SELECT ChiTietPhieuDatBan.*, DatBan.ThoiGianNhanBan FROM ChiTietPhieuDatBan INNER JOIN DatBan ON ChiTietPhieuDatBan.MaDatBan = DatBan.MaDatBan WHERE ChiTietPhieuDatBan.MaDatBan = '{LayMaDatBan}'";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DuLieuChiTietDatBanXem.DataSource = dataTable;
                        DuLieuChiTietDatBanSua.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                    }
                }
            }
        }

        private List<string> maBanList = new List<string>();

        private void DuLieuChiTietDatBanSua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = DuLieuChiTietDatBanSua.Rows[e.RowIndex];
                DataGridViewCell maBanCell = selectedRow.Cells["MaBan"];



                if (maBanCell.Value != null)
                {
                    string maBan = maBanCell.Value.ToString();

                    DialogResult result = MessageBox.Show("Bạn có muốn hủy chọn Mã Bàn " + maBan + " không?",
                        "Xác nhận hủy chọn Mã Bàn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM ChiTietPhieuDatBan WHERE MaBan = @MaBan";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@MaBan", maBan);
                                command.ExecuteNonQuery();
                            }
                            maBanList.Add(maBan);
                            DuLieuChiTietDatBanSua.Rows.Remove(selectedRow);
                        }

                    }
                }
            }
        }

        private void DuLieuChiTietDatBanSua_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TrangChu formTrangChu = (TrangChu)Application.OpenForms["TrangChu"];
            formTrangChu.DemSoBanChiTiet();
        }

        private void DuLieuChiTietDatBanSua_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            TrangChu formTrangChu = (TrangChu)Application.OpenForms["TrangChu"];
            formTrangChu.DemSoBanChiTiet();
        }

        private void ChinhSuaChiTietDatBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Gọi phương thức truy cập dữ liệu từ Form A
            TrangChu formA = Application.OpenForms["TrangChu"] as TrangChu;
            formA?.EnabledBtn(maBanList);
        }
    }
}
