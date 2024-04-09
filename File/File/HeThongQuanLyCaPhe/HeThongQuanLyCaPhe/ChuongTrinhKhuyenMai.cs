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
    public partial class ChuongTrinhKhuyenMai : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);

        public ChuongTrinhKhuyenMai()
        {
            InitializeComponent();
        }

        private void LoadDataToGridKM()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM KhuyenMai";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuVoucher.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private bool IsValidMaGG(string maGG)
        {
            bool containsSpecialCharacters = maGG.Any(c => !char.IsLetterOrDigit(c));
            return !containsSpecialCharacters;
        }

        private bool IsValidGiaTriGG(string giaTriGG)
        {
            decimal giaTri;
            bool isValid = decimal.TryParse(giaTriGG, out giaTri);

            return isValid && giaTri > 0;
        }

        private bool IsValidSoLuongMa(string soLuongMa)
        {
            int soLuong;
            bool isValid = int.TryParse(soLuongMa, out soLuong);

            return isValid;
        }

        private void ClearForm()
        {
            txtMaGG.Text = "";
            txtGTGG.Text = "";
            btnThemKM.Enabled = true;
            txtMaGG.Enabled = true;
            txtGTGG.Enabled = true;
            LoadDataToGridKM();
        }

        private void DuLieuVoucher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DuLieuVoucher.Rows[e.RowIndex];

                txtMaGG.Text = row.Cells["MaKhuyenMai"].FormattedValue.ToString();
                txtGTGG.Text = row.Cells["GiaTriKhuyenMai"].FormattedValue.ToString();
                numSoLuong.Text = row.Cells["SoLuong"].FormattedValue.ToString();
                dateUse.Text = row.Cells["NgayBatDau"].FormattedValue.ToString();
                dateEnd.Text = row.Cells["NgayKetThuc"].FormattedValue.ToString();
                btnThemKM.Enabled = false;
                txtGTGG.Enabled = false;
                txtMaGG.Enabled = false;
            }
        }

        private void btnThemKM_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string maKM = txtMaGG.Text;
                    string TrangThaiMa = "True";
                    string giaTriKM = txtGTGG.Text;
                    string soLuong = numSoLuong.Value.ToString();
                    DateTime ngayBatDau = dateUse.Value;
                    DateTime ngayKetThuc = dateEnd.Value;


                    if (string.IsNullOrEmpty(maKM) || string.IsNullOrEmpty(giaTriKM) || string.IsNullOrEmpty(soLuong))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                        return;
                    }
                    if (!IsValidMaGG(maKM))
                    {
                        MessageBox.Show("Mã giảm giá không được chứa ký tự đặc biệt!");
                        return;
                    }
                    if (!IsValidGiaTriGG(giaTriKM))
                    {
                        MessageBox.Show("Giá trị giảm giá không hợp lệ!");
                        return;
                    }
                    if (!IsValidSoLuongMa(soLuong))
                    {
                        MessageBox.Show("Số lượng mã không được chứa chữ và ký tự đặc biệt!");
                        return;
                    }
                    if (ngayBatDau > ngayKetThuc)
                    {
                        MessageBox.Show("Thời Gian kh hợp lệ");
                        return;

                    }

                    string query = "INSERT INTO KhuyenMai (MaKhuyenMai, GiaTriKhuyenMai, SoLuong, NgayBatDau, NgayKetThuc) VALUES (@MaKhuyenMai, @GiaTriKhuyenMai, @SoLuong, @NgayBatDau, @NgayKetThuc)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaKhuyenMai", maKM);
                    command.Parameters.AddWithValue("TrangThaiMa", TrangThaiMa);
                    command.Parameters.AddWithValue("@GiaTriKhuyenMai", giaTriKM);
                    command.Parameters.AddWithValue("@SoLuong", soLuong);
                    command.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm mã giảm giá thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
                LoadDataToGridKM();
                ClearForm();
            }
        }

        private void btnDungKM_Click(object sender, EventArgs e)
        {
            if (txtMaGG.Text == "")
            {
                MessageBox.Show("Vui lòng Nhập Mã Giảm giá");
                return;
            }

            string maKhuyenMai = txtMaGG.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE KhuyenMai SET TrangThai = 0 WHERE MaKhuyenMai = @maKhuyenMai";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            ClearForm();
            MessageBox.Show("Dừng mã Khuyễn Mãi '" + maKhuyenMai + "' thành Công");
        }



        private void ChuongTrinhKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadDataToGridKM();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dateUse.Value;
            DateTime ngayKetThuc = dateEnd.Value;


            if (numSoLuong.Value == 0)
            {
                MessageBox.Show("Vui Lòng Nhập Số Lượng");
                return;
            }

            if (ngayBatDau > ngayKetThuc)
            {
                MessageBox.Show("Thời Gian kh hợp lệ");
                return;

            }

            string updateSL = "UPDATE KhuyenMai SET SoLuong = @SoLuong WHERE MaKhuyenMai = @MaKhuyenMai";

            // Tạo kết nối và lệnh truy vấn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateSL, connection))
                {
                    // Thêm tham số và gán giá trị
                    command.Parameters.AddWithValue("@SoLuong", numSoLuong.Value);
                    command.Parameters.AddWithValue("@MaKhuyenMai", txtMaGG.Text);

                    // Thực thi truy vấn
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật số lượng thành công");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật số lượng thất bại");
                    }
                }
            }
            ChuongTrinhKhuyenMai_Load(sender, e);
        }

        private void btnMoiKM_Click(object sender, EventArgs e)
        {
            txtMaGG.Text = "";
            txtGTGG.Text = "";
            btnThemKM.Enabled = true;
            txtMaGG.Enabled = true;
            txtGTGG.Enabled = true;
            LoadDataToGridKM();
        }
    }
}
