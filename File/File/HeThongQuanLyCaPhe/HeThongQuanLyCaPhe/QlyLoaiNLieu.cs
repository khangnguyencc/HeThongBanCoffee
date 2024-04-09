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
    public partial class QlyLoaiNLieu : Form
    {
        public QlyLoaiNLieu()
        {
            InitializeComponent();
        }

        SqlCommand command;
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);

        private void LoadDataToGrild()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaLoaiNguyenLieu, MaKhoHang, TenLoaiNguyenLieu FROM LoaiNguyenLieu";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuLoaiNLieu.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        // Lấy Mã KHO //
        private void LayMaKhoHang()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaKhoHang FROM KhoHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    cmbKhoHang.DisplayMember = "MaKhoHang";
                    cmbKhoHang.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cc", ex.Message);
                }
            }

        }

        private void QlyLoaiNLieu_Load(object sender, EventArgs e)
        {
            
           
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
        private bool ContainsOnlyNumbers(string input)
        {
            foreach (char character in input)
            {
                if (!char.IsDigit(character))
                {
                    return false;
                }
            }

            return true;
        }
        private void btnThemLoaiNLieu_Click(object sender, EventArgs e)
        {
            string maLoaiNguyenLieu = txtMaLoaiNLieu.Text.Trim();
            string maKhoHang = cmbKhoHang.Text.Trim();
            string tenLoaiNguyenLieu = txtTenLoaiNLieu.Text.Trim();

            if (string.IsNullOrEmpty(maLoaiNguyenLieu) || string.IsNullOrEmpty(maKhoHang) || string.IsNullOrEmpty(tenLoaiNguyenLieu))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (ContainsSpecialCharacters(maLoaiNguyenLieu) || ContainsSpecialCharacters(maKhoHang) || ContainsSpecialCharacters(tenLoaiNguyenLieu))
            {
                MessageBox.Show("Vui lòng không nhập kí tự đặc biệt.");
                return;
            }

            if (ContainsOnlyNumbers(tenLoaiNguyenLieu))
            {
                MessageBox.Show("Vui lòng không nhập kí số.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO LoaiNguyenLieu (MaLoaiNguyenLieu, MaKhoHang, TenLoaiNguyenLieu) VALUES (@MaLoaiNguyenLieu, @MaKhoHang, @TenLoaiNguyenLieu)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaLoaiNguyenLieu", txtMaLoaiNLieu.Text);
                    command.Parameters.AddWithValue("@MaKhoHang", cmbKhoHang.Text);
                    command.Parameters.AddWithValue("@TenLoaiNguyenLieu", txtTenLoaiNLieu.Text);

                    command.ExecuteNonQuery();
                    LoadDataToGrild();

                    MessageBox.Show("Thêm loại nguyên liệu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaLoaiNLieu_Click(object sender, EventArgs e)
        {
            if (DuLieuLoaiNLieu.SelectedRows.Count > 0)
            {


                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại nguyên liệu này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int selectedIndex = DuLieuLoaiNLieu.SelectedRows[0].Index;
                    string MaLoaiNguyenLieu = DuLieuLoaiNLieu.Rows[selectedIndex].Cells[0].Value.ToString();

                    connection.Open();
                    // Tạo câu lệnh SQL để kiểm tra sự tồn tại của nguyên liệu trong loại nguyên liệu
                    string query = "SELECT COUNT(*) FROM NguyenLieu WHERE MaLoaiNguyenLieu = @MaLoaiNguyenLieu";
                    SqlCommand command = new SqlCommand(query, connection);
                    // Thêm tham số vào command
                    command.Parameters.AddWithValue("@MaLoaiNguyenLieu", MaLoaiNguyenLieu);

                    // Thực hiện truy vấn và lấy kết quả
                    int count = (int)command.ExecuteScalar();

                    // Kiểm tra xem nguyên liệu có tồn tại trong loại nguyên liệu không
                    if (count > 0)
                    {
                        MessageBox.Show("Hiện tại không thể xóa Loại Nguyên liệu này", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        try
                        {

                            command = connection.CreateCommand();
                            command.CommandText = "DELETE FROM LoaiNguyenLieu WHERE MaLoaiNguyenLieu = @maLoaiNLieu";
                            command.Parameters.AddWithValue("@maLoaiNLieu", MaLoaiNguyenLieu);
                            command.ExecuteNonQuery();

                            LoadDataToGrild();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xóa loại nguyên liệu: " + ex.Message, "Lỗi");
                        }
                    }
                    connection.Close();

                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một loại nguyên liệu để xóa.", "Thông báo");
            }
        }

        private void btnSuaLoaiNLieu_Click(object sender, EventArgs e)
        {
            if (DuLieuLoaiNLieu.SelectedRows.Count > 0)
            {
                int selectedIndex = DuLieuLoaiNLieu.SelectedRows[0].Index;
                string MaLoaiNguyenLieu = DuLieuLoaiNLieu.Rows[selectedIndex].Cells[0].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa loại nguyên liệu này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Kiểm tra xem người dùng đã thay đổi mã loại nguyên liệu hay không
                    if (MaLoaiNguyenLieu != txtMaLoaiNLieu.Text)
                    {
                        MessageBox.Show("Không được phép sửa mã loại nguyên liệu!", "Thông báo");
                        return; // Dừng xử lý tiếp theo
                    }

                    string maKhoHang = cmbKhoHang.Text.Trim();
                    string tenLoaiNguyenLieu = txtTenLoaiNLieu.Text.Trim();

                    if ( string.IsNullOrEmpty(maKhoHang) || string.IsNullOrEmpty(tenLoaiNguyenLieu))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                        return;
                    }

                    if (ContainsSpecialCharacters(maKhoHang) || ContainsSpecialCharacters(tenLoaiNguyenLieu))
                    {
                        MessageBox.Show("Vui lòng không nhập kí tự đặc biệt.");
                        return;
                    }

                    if (ContainsOnlyNumbers(tenLoaiNguyenLieu))
                    {
                        MessageBox.Show("Vui lòng không nhập kí số.");
                        return;
                    }

                    try
                    {
                        connection.Open();

                        string query = "UPDATE LoaiNguyenLieu SET MaKhoHang = @MaKhoHang, TenLoaiNguyenLieu = @TenLoaiNguyenLieu WHERE MaLoaiNguyenLieu = @MaLoaiNguyenLieu";

                        

                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaKhoHang", maKhoHang);
                        command.Parameters.AddWithValue("@TenLoaiNguyenLieu", tenLoaiNguyenLieu);
                        command.Parameters.AddWithValue("@MaLoaiNguyenLieu", MaLoaiNguyenLieu);
                        command.ExecuteNonQuery();

                        LoadDataToGrild();

                        MessageBox.Show("Sửa loại nguyên liệu thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi sửa loại nguyên liệu: " + ex.Message, "Lỗi");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại mã loại nguyên liệu bạn muốn sửa.", "Thông báo");
            }
        }

        private void btnTimMaLoaiNLieu_Click(object sender, EventArgs e)
        {
            try
            {
                string MaLoaiNguyenLieu = txtTimMaLoaiNLieu.Text.Trim();

                if (string.IsNullOrEmpty(MaLoaiNguyenLieu))
                {
                    MessageBox.Show("Vui lòng nhập mã loại để tìm kiếm.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM LoaiNguyenLieu WHERE MaLoaiNguyenLieu =@MaLoaiNguyenLieu";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaLoaiNguyenLieu", MaLoaiNguyenLieu);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtMaLoaiNLieu.Text = reader["MaLoaiNguyenLieu"].ToString();
                            txtTenLoaiNLieu.Text = reader["TenLoaiNguyenLieu"].ToString();
                            cmbKhoHang.SelectedItem = reader["MaKhoHang"].ToString();
                            btnThemLoaiNLieu.Enabled = false;
                            txtMaLoaiNLieu.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã loại được nhập.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void DuLieuLoaiNLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < DuLieuLoaiNLieu.Rows.Count)
            {
                txtMaLoaiNLieu.Text = DuLieuLoaiNLieu.Rows[rowIndex].Cells[0].Value.ToString();
                cmbKhoHang.Text = DuLieuLoaiNLieu.Rows[rowIndex].Cells[1].Value.ToString();
                txtTenLoaiNLieu.Text = DuLieuLoaiNLieu.Rows[rowIndex].Cells[2].Value.ToString();
                btnThemLoaiNLieu.Enabled = false;
                txtMaLoaiNLieu.Enabled = false;
            }
            else
            {
                txtMaLoaiNLieu.Text = string.Empty;
                cmbKhoHang.SelectedIndex = -1;
                txtTenLoaiNLieu.Text = string.Empty;
            }
        }

        private void btnMoiLoaiNLieu_Click(object sender, EventArgs e)
        {
            txtMaLoaiNLieu.Text = string.Empty;
            txtTenLoaiNLieu.Text = string.Empty;
            txtTimMaLoaiNLieu.Text = string.Empty;

            DuLieuLoaiNLieu.ClearSelection();
            DuLieuLoaiNLieu.CurrentCell = null;
            btnThemLoaiNLieu.Enabled = true;
            txtMaLoaiNLieu.Enabled = true;

            // Gọi phương thức LayMaKhoHang để cập nhật dữ liệu cho cmbKhoHang
            LayMaKhoHang();
        }

        private void QlyLoaiNLieu_Load_1(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            LoadDataToGrild();
            connection.Close();
            LayMaKhoHang();
        }
    }
}
