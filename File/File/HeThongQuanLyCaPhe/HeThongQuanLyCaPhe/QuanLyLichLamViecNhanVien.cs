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
    public partial class QuanLyLichLamViecNhanVien : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);
        public QuanLyLichLamViecNhanVien()
        {
            InitializeComponent();
        }

        private void txtTimeStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':')
            {
                e.Handled = true; 
            }
        }

        private void txtTimeStart_TextChanged(object sender, EventArgs e)
        {
            string input = txtTimeStart.Text.Trim();

            if (string.IsNullOrEmpty(input) || input.Length < 8)
            {
                return;
            }

            string[] parts = input.Split(':');
            if (parts.Length == 3)
            {
                int hours, minutes, seconds;
                if (int.TryParse(parts[0], out hours) &&
                    int.TryParse(parts[1], out minutes) &&
                    int.TryParse(parts[2], out seconds))
                {
                    string formattedTime = hours.ToString("00") + ":" +
                                           minutes.ToString("00") + ":" +
                                           seconds.ToString("00");
                    txtTimeStart.Text = formattedTime;
                }
            }
        }
        private void txtTimeOver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':')
            {
                e.Handled = true;
            }
        }

        private void txtTimeOver_TextChanged(object sender, EventArgs e)
        {
            string input = txtTimeStart.Text.Trim();

            if (string.IsNullOrEmpty(input) || input.Length < 8)
            {
                return;
            }

            string[] parts = input.Split(':');
            if (parts.Length == 3)
            {
                int hours, minutes, seconds;
                if (int.TryParse(parts[0], out hours) &&
                    int.TryParse(parts[1], out minutes) &&
                    int.TryParse(parts[2], out seconds))
                {
                    string formattedTime = hours.ToString("00") + ":" +
                                           minutes.ToString("00") + ":" +
                                           seconds.ToString("00");
                    txtTimeStart.Text = formattedTime;
                }
            }
        }
        private void clearForm()
        {
            txtMaCaLam.Clear();
            txtTenCaLam.Clear();
            txtTimeStart.Text = "00:00:00";
            txtTimeOver.Text = "00:00:00";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string maCaLam = txtMaCaLam.Text;
            string tenCaLam = txtTenCaLam.Text;
            string gioBatDau = txtTimeStart.Text;
            string gioKetThuc = txtTimeOver.Text;

            if (string.IsNullOrEmpty(maCaLam) || string.IsNullOrEmpty(tenCaLam) || string.IsNullOrEmpty(gioBatDau) || string.IsNullOrEmpty(gioKetThuc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ContainsSpecialCharacter(maCaLam))
            {
                MessageBox.Show("Mã ca làm không được chứa ký tự đặc biệt.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ContainsSpecialCharacterOrNumber(tenCaLam))
            {
                MessageBox.Show("Tên ca làm không được chứa ký tự đặc biệt và số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkDuplicateQuery = "SELECT COUNT(*) FROM CaLamViec WHERE MaCaLamViec = @MaCaLam";
                    using (SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        checkDuplicateCommand.Parameters.AddWithValue("@MaCaLam", maCaLam);
                        int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();

                        if (duplicateCount > 0)
                        {
                            MessageBox.Show("Mã ca làm đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO CaLamViec (MaCaLamViec, TenCaLamViec, GioBatDau, GioKetThuc) VALUES (@MaCaLam, @TenCaLam, @GioBatDau, @GioKetThuc)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@MaCaLam", maCaLam);
                        insertCommand.Parameters.AddWithValue("@TenCaLam", tenCaLam);
                        insertCommand.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                        insertCommand.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);

                        insertCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearForm();
                    LoadCaLamViec();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ContainsSpecialCharacter(string input)
        {
            string specialCharacters = @"~`!@#$%^&*()-_=+[]{}\|;:'"",<.>/?";
            foreach (char c in specialCharacters)
            {
                if (input.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsSpecialCharacterOrNumber(string input)
        {
            string specialCharactersAndNumbers = @"~`!@#$%^&*()-_=+[]{}\|;:'"",<.>/?1234567890";
            foreach (char c in specialCharactersAndNumbers)
            {
                if (input.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DuLieuCaLam.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để chỉnh sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maCaLam = txtMaCaLam.Text;
            string tenCaLam = txtTenCaLam.Text;
            string gioBatDau = txtTimeStart.Text;
            string gioKetThuc = txtTimeOver.Text;
            if (string.IsNullOrEmpty(maCaLam) || string.IsNullOrEmpty(tenCaLam) || string.IsNullOrEmpty(gioBatDau) || string.IsNullOrEmpty(gioKetThuc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ContainsSpecialCharacter(maCaLam))
            {
                MessageBox.Show("Mã ca làm không được chứa ký tự đặc biệt.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ContainsSpecialCharacterOrNumber(tenCaLam))
            {
                MessageBox.Show("Tên ca làm không được chứa ký tự đặc biệt và số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE CaLamViec SET TenCaLamViec = @TenCaLam, GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc WHERE MaCaLamViec = @MaCaLam";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenCaLam", tenCaLam);
                        command.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                        command.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Cập nhật dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearForm();
                    LoadCaLamViec();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DuLieuCaLam.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            string maCaLam = txtMaCaLam.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkUsageQuery = "SELECT COUNT(*) FROM ChamCong WHERE MaCaLamViec = @MaCaLam";
                    using (SqlCommand checkUsageCommand = new SqlCommand(checkUsageQuery, connection))
                    {
                        checkUsageCommand.Parameters.AddWithValue("@MaCaLam", maCaLam);
                        int usageCount = (int)checkUsageCommand.ExecuteScalar();

                        if (usageCount > 0)
                        {
                            MessageBox.Show("Không thể xóa mã ca làm vì đã được sử dụng trong việc chấm công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string deleteQuery = "DELETE FROM CaLamViec WHERE MaCaLamViec = @MaCaLam";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@MaCaLam", maCaLam);
                        deleteCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearForm();
                    LoadCaLamViec();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCaLamViec()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT MaCaLamViec, TenCaLamViec, GioBatDau, GioKetThuc FROM CaLamViec";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DuLieuCaLam.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyLichLamViecNhanVien_Load(object sender, EventArgs e)
        {
            LoadCaLamViec();
        }

        private void DuLieuCaLam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DuLieuCaLam.Rows[e.RowIndex];
                txtMaCaLam.Text = row.Cells["MaCaLamViec"].Value.ToString();
                txtTenCaLam.Text = row.Cells["TenCaLamViec"].Value.ToString();
                txtTimeStart.Text = row.Cells["GioBatDauVao"].Value.ToString();
                txtTimeOver.Text = row.Cells["GioKetThucCa"].Value.ToString();
            }
        }
    }
}
