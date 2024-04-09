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
    public partial class Qlynglieu : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public Qlynglieu()
        {
            InitializeComponent();
        }
        private void LoadDataToGrild()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaNguyenLieu, MaLoaiNguyenLieu, TenNguyenLieu, HanSuDung, DonVi FROM NguyenLieu";
                    adapter = new SqlDataAdapter(query, connection);
                    table = new DataTable();
                    adapter.Fill(table);

                    DuLieuNguyenLieu.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void Qlynglieu_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            LoadDataToGrild();
            connection.Close();

            //Truyền cmb
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Chuỗi truy vấn SQL để lấy danh sách mã nhà cung cấp
                string query = "SELECT MaLoaiNguyenLieu FROM LoaiNguyenLieu";

                // Tạo đối tượng Command
                SqlCommand command = new SqlCommand(query, connection);

                // Thực thi truy vấn và lấy dữ liệu
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Kiểm tra xem có dữ liệu hay không
                    if (reader.HasRows)
                    {
                        // Duyệt qua từng dòng dữ liệu
                        while (reader.Read())
                        {
                            // Lấy giá trị của cột MaNCC
                            string maNCC = reader.GetString(0);

                            // Thêm giá trị vào ComboBox
                            cmbMaLoaiNLieu.Items.Add(maNCC);
                        }
                    }
                }
            }
        }

        private void DuLieuNguyenLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNguyenLieu.Enabled = false;
            if (e.RowIndex >= 0 && e.RowIndex < DuLieuNguyenLieu.Rows.Count - 1)
            {
                DataGridViewRow row = DuLieuNguyenLieu.Rows[e.RowIndex];
                txtMaNguyenLieu.Text = row.Cells["MaNguyenLieu"].Value.ToString();
                txtTenNguyenLieu.Text = row.Cells["TenNguyenLieu"].Value.ToString();
                cmbMaLoaiNLieu.Text = row.Cells["MaLoaiNguyenLieu"].Value.ToString();
                cmbDonVi.Text = row.Cells["DonVi"].Value.ToString();
                dateTimePicker1.Text = row.Cells["HanSuDung"].Value.ToString();
                btnThemNguyenLieu.Enabled = false;
                txtMaNguyenLieu.Enabled = false;
            }
        }

        private bool ContainsSpecialCharacters(string input)
        {
            string specialCharacters = "!@#$%^&*()_+{}[];',./:\"<>?`~\\|";
            foreach (char character in input)
            {
                if (specialCharacters.Contains(character))
                {
                    return true;
                }
            }
            return false;
        }

        private bool KiemTraMaNguyenLieuTonTai(string maNguyenLieu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                    return false;
                }
            }
        }

        private void btnThemNguyenLieu_Click(object sender, EventArgs e)
        {
            string maNguyenLieu = txtMaNguyenLieu.Text.Trim();
            string maLoaiNguyenLieu = cmbMaLoaiNLieu.SelectedItem?.ToString();
            string tenNguyenLieu = txtTenNguyenLieu.Text.Trim();
            DateTime hSD = dateTimePicker1.Value;
            string hanSuDung = hSD.ToString("yyyy-MM-dd");
            string donVi = cmbDonVi.Text.Trim();

            if (string.IsNullOrEmpty(maNguyenLieu) || string.IsNullOrEmpty(maLoaiNguyenLieu) || string.IsNullOrEmpty(tenNguyenLieu) || string.IsNullOrEmpty(hanSuDung) || string.IsNullOrEmpty(donVi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nguyên liệu.");
                return;
            }
            if (ContainsSpecialCharacters(maNguyenLieu) || ContainsSpecialCharacters(maLoaiNguyenLieu) || ContainsSpecialCharacters(tenNguyenLieu) || ContainsSpecialCharacters(hanSuDung) || ContainsSpecialCharacters(donVi))
            {
                MessageBox.Show("Vui lòng không nhập kí tự đặc biệt.");
                return;
            }

            // Kiểm tra xem mã nguyên liệu đã tồn tại hay chưa
            bool maNguyenLieuTonTai = KiemTraMaNguyenLieuTonTai(maNguyenLieu);
            if (maNguyenLieuTonTai)
            {
                MessageBox.Show("Mã nguyên liệu đã tồn tại.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO NguyenLieu (MaNguyenLieu, MaLoaiNguyenLieu, TenNguyenLieu, HanSuDung, DonVi) " +
                                   "VALUES (@MaNguyenLieu, @MaLoaiNguyenLieu, @TenNguyenLieu, @HanSuDung, @DonVi)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                    command.Parameters.AddWithValue("@MaLoaiNguyenLieu", maLoaiNguyenLieu);
                    command.Parameters.AddWithValue("@TenNguyenLieu", tenNguyenLieu);
                    command.Parameters.AddWithValue("@HanSuDung", hanSuDung);
                    command.Parameters.AddWithValue("@DonVi", donVi);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm nguyên liệu thành công.");
                    txtMaNguyenLieu.Enabled = true;
                    txtMaNguyenLieu.Text = string.Empty;
                    cmbMaLoaiNLieu.SelectedIndex = -1;
                    txtTenNguyenLieu.Text = string.Empty;
                    dateTimePicker1.Text = string.Empty;
                    cmbDonVi.SelectedIndex = -1;
                    txtTimMaNguyenLieu.Text = string.Empty;
                    btnThemNguyenLieu.Enabled = true;

                    LoadDataToGrild();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.ToString());
                }
            }
        }

        private void btnSuaNguyenLieu_Click(object sender, EventArgs e)
        {
            string maNguyenLieu = txtMaNguyenLieu.Text.Trim();
            string maLoaiNguyenLieu = cmbMaLoaiNLieu.SelectedItem?.ToString();
            string tenNguyenLieu = txtTenNguyenLieu.Text.Trim();
            DateTime hSD = dateTimePicker1.Value;
            string hanSuDung = hSD.ToString("yyyy-mm-dd");
            string donVi = cmbDonVi.Text.Trim();

            if (string.IsNullOrEmpty(maNguyenLieu) || string.IsNullOrEmpty(maLoaiNguyenLieu) || string.IsNullOrEmpty(tenNguyenLieu) || string.IsNullOrEmpty(hanSuDung) || string.IsNullOrEmpty(donVi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nguyên liệu.");
                return;
            }
            if (ContainsSpecialCharacters(maNguyenLieu) || ContainsSpecialCharacters(maLoaiNguyenLieu) || ContainsSpecialCharacters(tenNguyenLieu) || ContainsSpecialCharacters(hanSuDung) || ContainsSpecialCharacters(donVi))
            {
                MessageBox.Show("Vui lòng không nhập kí tự đặc biệt.");
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "UPDATE NguyenLieu SET MaLoaiNguyenLieu = @MaLoaiNguyenLieu, TenNguyenLieu = @TenNguyenLieu, HanSuDung = @HanSuDung, DonVi = @DonVi " +
                                   "WHERE MaNguyenLieu = @MaNguyenLieu";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                    command.Parameters.AddWithValue("@MaLoaiNguyenLieu", maLoaiNguyenLieu);
                    command.Parameters.AddWithValue("@TenNguyenLieu", tenNguyenLieu);
                    command.Parameters.AddWithValue("@HanSuDung", hSD);
                    command.Parameters.AddWithValue("@DonVi", donVi);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật nguyên liệu thành công.");
                        txtMaNguyenLieu.Enabled = true;
                        txtMaNguyenLieu.Text = string.Empty;
                        txtTenNguyenLieu.Text = string.Empty;
                        dateTimePicker1.Text = string.Empty;
                        txtTimMaNguyenLieu.Text = string.Empty;
                        cmbDonVi.Text = string.Empty;
                        cmbMaLoaiNLieu.Text = string.Empty;
                        btnThemNguyenLieu.Enabled = true;
                        LoadDataToGrild();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nguyên liệu có mã: " + maNguyenLieu);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaNguyenLieu_Click(object sender, EventArgs e)
        {
            string maNguyenLieu = txtMaNguyenLieu.Text.Trim();

            if (string.IsNullOrEmpty(maNguyenLieu))
            {
                MessageBox.Show("Vui lòng nhập mã nguyên liệu cần xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nguyên liệu có mã: " + maNguyenLieu + "?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkHD = $"Select COunt(*) from ChiTietPhieuXuatKho Where MaNguyenLieu = '{maNguyenLieu}'";
                    SqlCommand sqlCommand = new SqlCommand(checkHD, connection);
                    int Count = (int)sqlCommand.ExecuteScalar();
                    if (Count > 0)
                    {
                        MessageBox.Show("Nguyên liệu này hiện tại không được Xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        try
                        {
                            string query = "DELETE FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa nguyên liệu thành công.");
                                txtMaNguyenLieu.Enabled = true;
                                txtMaNguyenLieu.Text = string.Empty;
                                txtTenNguyenLieu.Text = string.Empty;
                                dateTimePicker1.Text = string.Empty;
                                txtTimMaNguyenLieu.Text = string.Empty;
                                cmbDonVi.Text = string.Empty;
                                cmbMaLoaiNLieu.Text = string.Empty;
                                btnThemNguyenLieu.Enabled = true;
                                LoadDataToGrild();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nguyên liệu có mã: " + maNguyenLieu);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                        }
                    }
                    connection.Close();

                }
            }
        }

        private void btnMoiNguyenLieu_Click(object sender, EventArgs e)
        {
            txtMaNguyenLieu.Enabled = true;
            txtMaNguyenLieu.Text = string.Empty;
            cmbMaLoaiNLieu.SelectedIndex = -1;
            txtTenNguyenLieu.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            cmbDonVi.SelectedIndex = -1;
            txtTimMaNguyenLieu.Text = string.Empty;
            btnThemNguyenLieu.Enabled = true;

            LoadDataToGrild();
        }

        private void btnTimNguyenLieu_Click(object sender, EventArgs e)
        {
            string maNguyenLieu = txtTimMaNguyenLieu.Text.Trim();

            if (string.IsNullOrEmpty(maNguyenLieu))
            {
                MessageBox.Show("Vui lòng nhập mã nguyên liệu cần tìm.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtMaNguyenLieu.Text = reader["MaNguyenLieu"].ToString();
                        cmbMaLoaiNLieu.SelectedItem = reader["MaLoaiNguyenLieu"].ToString();
                        txtTenNguyenLieu.Text = reader["TenNguyenLieu"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["HanSuDung"]);
                        cmbDonVi.Text = reader["DonVi"].ToString();
                        btnThemNguyenLieu.Enabled = false;

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nguyên liệu có mã: " + maNguyenLieu);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            DateTime currentDate = DateTime.Now;

            if (selectedDate > currentDate)
            {
                // Ngày được chọn là sau ngày hiện tại
                MessageBox.Show("Ngày được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
