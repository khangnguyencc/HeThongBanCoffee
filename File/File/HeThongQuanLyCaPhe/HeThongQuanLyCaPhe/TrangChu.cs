using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Contexts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.IO;
using System.Security.Policy;

namespace HeThongQuanLyCaPhe
{
    public partial class TrangChu : Form
    {
        
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);

        private Timer timer;

        public TrangChu()
        {
            InitializeComponent();
            InitializeTimer();
            /////////////// Phiếu Nhập \\\\\\\\\\\\\\\\
        }


        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MenuHoaDon.Enabled == false)
            {
                DialogResult result = MessageBox.Show("Hóa đơn chưa hoàn thành, bạn có muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string MaThe = "The" + HD_lbSoThe.Text;
                        connection.Open();
                        string Query = $"Update TrangThaiSanPham Set TrangThai = '{0}',ThoiGianNhap = NULL where MaTrangThaiThe = '{MaThe}'";
                        SqlCommand cmd = new SqlCommand(Query, connection);
                        cmd.ExecuteNonQuery();

                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string MaHoaDon = HD_lbMaHD.Text;
                            string deleteChiTietQuery = $"DELETE FROM ChiTietHoaDon WHERE MaHoaDon = '{MaHoaDon}'";
                            SqlCommand chiTietCommand = new SqlCommand(deleteChiTietQuery, connection, transaction);
                            chiTietCommand.ExecuteNonQuery();
                            string deleteHoaDonQuery = $"DELETE FROM HoaDon WHERE MaHoaDon = '{MaHoaDon}'";
                            SqlCommand hoaDonCommand = new SqlCommand(deleteHoaDonQuery, connection, transaction);
                            hoaDonCommand.ExecuteNonQuery();
                            string deleteChiTietQueryTam = $"DELETE FROM ChiTietHoaDonTam WHERE MaHoaDonTam = '{MaHoaDon}'";
                            SqlCommand chiTietCommandTam = new SqlCommand(deleteChiTietQueryTam, connection, transaction);
                            chiTietCommandTam.ExecuteNonQuery();
                            string deleteHoaDonQueryTam = $"DELETE FROM HoaDonTam WHERE MaHoaDonTam = '{MaHoaDon}'";
                            SqlCommand hoaDonCommandTam = new SqlCommand(deleteHoaDonQueryTam, connection, transaction);
                            hoaDonCommandTam.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (MenuPhieuNhap.Enabled == false)
            {
                DialogResult result = MessageBox.Show("Phiếu Nhập chưa hoàn thành, bạn có muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string MaPhieuNhapKho = PN_lbMaPN.Text;
                            string deleteChiTietQuery = $"DELETE FROM ChiTietPhieuNhapKho WHERE MaPhieuNhapKho = '{MaPhieuNhapKho}'";
                            SqlCommand chiTietCommand = new SqlCommand(deleteChiTietQuery, connection, transaction);
                            chiTietCommand.ExecuteNonQuery();
                            string deletePhieuNhapQuery = $"DELETE FROM PhieuNhapKho WHERE MaPhieuNhapKho = '{MaPhieuNhapKho}'";
                            SqlCommand phieuNhapCommand = new SqlCommand(deletePhieuNhapQuery, connection, transaction);
                            phieuNhapCommand.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (MenuPhieuXuat.Enabled == false)
            {
                DialogResult result = MessageBox.Show("Phiếu Xuất chưa hoàn thành, bạn có muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string MaPhieuXuatKho = PX_lbMaPX.Text;
                            string deleteChiTietQuery = $"DELETE FROM ChiTietPhieuXuatKho WHERE MaPhieuXuatKho = '{MaPhieuXuatKho}'";
                            SqlCommand chiTietCommand = new SqlCommand(deleteChiTietQuery, connection, transaction);
                            chiTietCommand.ExecuteNonQuery();
                            string deletePhieuXuatQuery = $"DELETE FROM PhieuXuatKho WHERE MaPhieuXuatKho = '{MaPhieuXuatKho}'";
                            SqlCommand phieuXuatCommand = new SqlCommand(deletePhieuXuatQuery, connection, transaction);
                            phieuXuatCommand.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (bienDemTaoBan == 1)
            {
                DialogResult result = MessageBox.Show("Phiếu đặt bàn chưa hoàn thành, bạn có muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string MADATBAN = lblMaDatBan.Text;
                            string deleteChiTietQuery = $"DELETE FROM ChiTietPhieuDatBan WHERE MaDatBan = '{MADATBAN}'";
                            SqlCommand chiTietCommand = new SqlCommand(deleteChiTietQuery, connection, transaction);
                            chiTietCommand.ExecuteNonQuery();
                            string deleteDatBanQuery = $"DELETE FROM DatBan WHERE MaDatBan = '{MADATBAN}'";
                            SqlCommand datBanCommand = new SqlCommand(deleteDatBanQuery, connection, transaction);
                            datBanCommand.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        private string _DN_txtManvText;

        public string DN_txtManvText
        {
            get { return _DN_txtManvText; }
            set
            {
                _DN_txtManvText = value;
                DN_txtManv.Text = value;
            }
        }
        // Load Form và Hoạt động của form //
        private void TrangChu_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyCongNhanVien);
            tabControl1.TabPages.Remove(TKdoanhthu);
            tabControl1.TabPages.Remove(TKTonKho);
            tabControl1.TabPages.Remove(BaoCao);
            tabControl1.Visible = false;
            HS_LoadMaNhanVien();
            GanDuLieuMaNV();

        }
        private void MenuQlyLoaiNguyenLieu_Click(object sender, EventArgs e)
        {
            QlyLoaiNLieu formLoaiNlieu = new QlyLoaiNLieu();
            formLoaiNlieu.ShowDialog();
        }
        private void MenuQlyNguyenLieu_Click(object sender, EventArgs e)
        {
            Qlynglieu formNguyenLieu = new Qlynglieu();
            formNguyenLieu.ShowDialog();
        }
        private void MenuKhuyenMai_Click(object sender, EventArgs e)
        {
            ChuongTrinhKhuyenMai formKhuyenMai = new ChuongTrinhKhuyenMai();
            formKhuyenMai.ShowDialog();
        }

        private void MenuQlyNCC_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(QlyNhaCC);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuQlyNCC.Enabled = false;
            QLyChamCong.Enabled = true;
            MenuQlyNhanVien.Enabled = true; 
            MenuTinhTrangDon.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;

            LoadDataToGridNCC();
            MaNCC = GetNextMaNhaCC(connection);
            txtMaNCC.Text = MaNCC.ToString();
            txtMaNCC.Enabled = false;

        }
        ///////////////////////////////////////////////////// NHÀ CUNG CẤP \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public string MaNCC = "";
        // Tạo mã nhà cung cấp tự động
        private string GetNextMaNhaCC(SqlConnection connection)
        {
            connection.Open();
            string query = "SELECT MAX(MaNhaCungCap) FROM NhaCungCap";
            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            string maxMaNhaCC = Convert.ToString(result);
            connection.Close();


            if (!string.IsNullOrEmpty(maxMaNhaCC))
            {
                int nextNumber = int.Parse(maxMaNhaCC.Substring(3)) + 1;
                string nextMaNhaCC = "NCC" + nextNumber.ToString("D2");
                return nextMaNhaCC;
            }
            else
            {
                return "NCC01";
            }
        }
        // Load dữ liệu nhà cung cấp 
        private void LoadDataToGridNCC()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM NhaCungCap";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuNhaCC.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        // Chỉnh sửa cho bảng dữ liệu nhà cung cấp
        private void DuLieuNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemNCC.Enabled = false;
            int i = DuLieuNhaCC.CurrentRow.Index;

            txtMaNCC.Text = DuLieuNhaCC.Rows[i].Cells[0].Value.ToString();
            txtTenNCC.Text = DuLieuNhaCC.Rows[i].Cells[1].Value.ToString();
            EmailNhaCC.Text = DuLieuNhaCC.Rows[i].Cells[3].Value.ToString();
            SdtNhaCC.Text = DuLieuNhaCC.Rows[i].Cells[2].Value.ToString();
            DiaChiNhaCC.Text = DuLieuNhaCC.Rows[i].Cells[4].Value.ToString();
        }
        private void clearNCC()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            DiaChiNhaCC.Clear();
            EmailNhaCC.Clear();
            SdtNhaCC.Clear();
        }
        private void btnTimNhaCC_Click(object sender, EventArgs e)
        {
            string maNhaCCFind = txtTimMaNCC.Text;
            string tenNhaCCFind = txtTimTenNCC.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM NhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap OR TenNhaCungCap = @TenNhaCungCap";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhaCungCap", maNhaCCFind);
                        command.Parameters.AddWithValue("@TenNhaCungCap", tenNhaCCFind);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtMaNCC.Text = reader["MaNhaCungCap"].ToString();
                            txtTenNCC.Text = reader["TenNhaCungCap"].ToString();
                            SdtNhaCC.Text = reader["SDT"].ToString();
                            EmailNhaCC.Text = reader["Email"].ToString();
                            DiaChiNhaCC.Text = reader["DiaChi"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu phù hợp.");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void txtTenNCC_TextChanged(object sender, EventArgs e)
        {
            string input = SdtNhaCC.Text;

            string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length == 10)
            {
                string formattedSdt = string.Format("({0}) {1}-{2}",
                    digitsOnly.Substring(0, 3),
                    digitsOnly.Substring(3, 3),
                    digitsOnly.Substring(6));

                SdtNhaCC.Text = formattedSdt;
            }
        }

        // Thêm nhà cung cấp
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            string MaCC = txtMaNCC.Text;
            string TenNCC = txtTenNCC.Text;
            string DiaChi = DiaChiNhaCC.Text;
            string Email = EmailNhaCC.Text;
            string SdtNCC = SdtNhaCC.Text;

            if (MaCC == "" || TenNCC == "" || Email == "" || SdtNCC == "" || DiaChi == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
                return;
            }

            else if (!IsValidNameNCC(TenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không hợp lệ");
                return;
            }
            else if (!IsValidEmailNCC(Email))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }

            else if (!IsValidPhoneNumberNCC(SdtNCC))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }
            else if (!IsValidAddressNCC(DiaChi))
            {
                MessageBox.Show("Địa chỉ không hợp lệ");
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open(); // Mở kết nối
                        string insertQuery = "INSERT INTO NhaCungCap (MaNhaCungCap, TenNhaCungCap, SDT, Email, DiaChi) VALUES (@MaNhaCungCap, @TenNhaCungCap, @SDT, @Email, @DiaChi)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                        insertCmd.Parameters.AddWithValue("@MaNhaCungCap", MaCC);
                        insertCmd.Parameters.AddWithValue("@TenNhaCungCap", TenNCC);
                        insertCmd.Parameters.AddWithValue("@SDT", SdtNCC);
                        insertCmd.Parameters.AddWithValue("@Email", Email);
                        insertCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Đã thêm thông tin");
                        clearNCC();
                        LoadDataToGridNCC();
                        TrangChu_Load(sender, e);
                        txtMaNCC.Enabled = false;
                        MenuQlyNCC.Enabled = true;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                    }
                }
                connection.Close();
            }
        }
        private bool IsValidNameNCC(string name)
        {
            return Regex.IsMatch(name, @"^[\p{L}\s]+$");
        }
        private bool IsValidEmailNCC(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidPhoneNumberNCC(string phoneNumber)
        {
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length == 10)
            {
                string formattedSdt = string.Format("({0}) {1}-{2}",
                    digitsOnly.Substring(0, 3),
                    digitsOnly.Substring(3, 3),
                    digitsOnly.Substring(6));

                SdtNhaCC.Text = formattedSdt;
                return true;
            }

            return false;
        }
        private bool IsValidAddressNCC(string address)
        {
            return Regex.IsMatch(address, @"^[^@#$%^&*()\[\]{}\\|<>.,?:;'""~`+=_\-@#\$%\^&\*\(\)\[\]\{\}\\|<>,\.\?\:\;'""]+$");
        }
        // Sửa thông tin nhà cung cấp
        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (DuLieuNhaCC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn một nhà cung cấp trong danh sách");
                return;
            }

            string MaCC = DuLieuNhaCC.SelectedRows[0].Cells["MaNhaCungCap"].Value.ToString();
            string TenNCC = txtTenNCC.Text;
            string DiaChi = DiaChiNhaCC.Text;
            string Email = EmailNhaCC.Text;
            string SdtNCC = SdtNhaCC.Text;

            if (TenNCC == "" || Email == "" || SdtNCC == "" || DiaChi == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
                return;
            }

            if (!IsValidEmailNCC(Email))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }

            if (!IsValidPhoneNumberNCC(SdtNCC))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }

            if (!IsValidAddressNCC(DiaChi))
            {
                MessageBox.Show("Địa chỉ không hợp lệ");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string updateQuery = "UPDATE NhaCungCap " +
                                         "SET TenNhaCungCap = @TenNhaCungCap, SDT = @SDT, Email = @Email, DiaChi = @DiaChi " +
                                         "WHERE MaNhaCungCap = @MaNhaCungCap";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.CommandType = CommandType.Text;
                    updateCmd.Parameters.AddWithValue("@MaNhaCungCap", MaCC);
                    updateCmd.Parameters.AddWithValue("@TenNhaCungCap", TenNCC);
                    updateCmd.Parameters.AddWithValue("@Email", Email);
                    updateCmd.Parameters.AddWithValue("@SDT", SdtNCC);
                    updateCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                    updateCmd.ExecuteNonQuery();
                    updateCmd.Dispose();
                    MessageBox.Show("Đã cập nhật thông tin");
                    LoadDataToGridNCC();
                    clearNCC();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Bị lỗi: " + ex.Message);
                }
            }
        }
        // Xóa nhà cung cấp
        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string MaCC = txtMaNCC.Text;

                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu?", "Xác nhận xóa", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        // Kiểm tra xem có bản ghi liên quan trong bảng PhieuNhapKho hay không
                        string checkQuery = "SELECT COUNT(*) FROM PhieuNhapKho WHERE MaNhaCungCap = @MaNhaCungCap";
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@MaNhaCungCap", MaCC);
                            int relatedRecords = (int)checkCommand.ExecuteScalar();

                            if (relatedRecords > 0)
                            {
                                MessageBox.Show("Mã nhà cung cấp đã tồn tại nên không thể xóa được");
                            }
                            else
                            {
                                // Xóa bản ghi trong bảng NhaCungCap
                                string deleteQuery = "DELETE FROM NhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap";
                                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                                {
                                    deleteCommand.Parameters.AddWithValue("@MaNhaCungCap", MaCC);
                                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Xóa dữ liệu thành công!");
                                        LoadDataToGridNCC();
                                        clearNCC();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Không có mã nào như vậy!");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void txtTimTenNCC_TextChanged(object sender, EventArgs e)
        {
            txtTimMaNCC.Text = string.Empty;
        }

        private void txtTimMaNCC_TextChanged(object sender, EventArgs e)
        {
            txtTimTenNCC.Text = string.Empty;
        }
        private void btnMoiNCC_Click(object sender, EventArgs e)
        {
            btnThemNCC.Enabled = false;
            clearNCC();

        }
        ///////////////////////////////////////////////////// QUẢN LÝ LOẠI MÓN \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        private void MenuQlyLMon_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(LoaiMon);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuQlyLMon.Enabled = false;
            MenuTinhTrangDon.Enabled = true;
            MenuQlyNhanVien.Enabled = true;
            QLyChamCong.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyMenu.Enabled = true;
            LoadDataToGridLoaiSP();
            LoadDuLieuKhoHang();
        }
        private void LoadDuLieuKhoHang()
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
                    cmbKhoHang.ValueMember = "MaKhoHang";
                    cmbKhoHang.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void LoadDataToGridLoaiSP()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM LoaiSanPham";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuLoaiMon.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        // Tìm loại món
        private void btnTimLoai_Click(object sender, EventArgs e)
        {
            string maNhaCCFind = txtTimMaLoai.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM LoaiSanPham WHERE MaLoaiSanPham = @MaLoaiSanPham";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLoaiSanPham", maNhaCCFind);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtMaLoai.Text = reader["MaLoaiSanPham"].ToString();
                            txtTenLoai.Text = reader["TenLoaiSanPham"].ToString();
                            cmbKhoHang.Text = reader["MaKhoHang"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu phù hợp.");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi" + ex.Message);
                }
            }
        }
        // Thêm loại món
        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            string maLNL = txtMaLoai.Text;
            string tenLL = txtTenLoai.Text;
            string maKH = cmbKhoHang.Text;
            if (maLNL == "" || tenLL == "" || maKH == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
                return; 
            }
            Regex specialCharRegex = new Regex("[~`!@#$%^&*()+=|{}':;',\\[\\].<>/?_]");
            if (specialCharRegex.IsMatch(maLNL))
            {
                MessageBox.Show("Mã loại nguyên liệu không được chứa ký tự đặc biệt.");
                return;
            }
            if (specialCharRegex.IsMatch(tenLL))
            {
                MessageBox.Show("Tên loại nguyên liệu không được chứa ký tự đặc biệt.");
                return;
            }
            Regex numberRegex = new Regex("[0-9]");
            if (numberRegex.IsMatch(tenLL))
            {
                MessageBox.Show("Tên loại nguyên liệu không được chứa số.");
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    string checkMaLNLQuery = "SELECT COUNT(*) FROM LoaiSanPham WHERE MaLoaiSanPham = @MaLoaiSanPham";
                    SqlCommand checkMaLNLCommand = new SqlCommand(checkMaLNLQuery, connection);
                    checkMaLNLCommand.Parameters.AddWithValue("@MaLoaiSanPham", maLNL);
                    int existingMaLNLCount = (int)checkMaLNLCommand.ExecuteScalar();
                    checkMaLNLCommand.Dispose();

                    if (existingMaLNLCount > 0)
                    {
                        MessageBox.Show("Mã loại sản phẩm đã tồn tại!");
                        return;
                    }

                    // Thêm thông tin vào cơ sở dữ liệu
                    string query = "INSERT INTO LoaiSanPham (MaLoaiSanPham, MaKhoHang, TenLoaiSanPham) VALUES (@MaLoaiSanPham, @MaKhoHang, @TenLoaiSanPham)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaLoaiSanPham", maLNL);
                    cmd.Parameters.AddWithValue("@MaKhoHang", maKH);
                    cmd.Parameters.AddWithValue("@TenLoaiSanPham", tenLL);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    MessageBox.Show("Đã thêm thông tin");
                    LoadDataToGridLoaiSP();
                    clear2();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        // Sửa thông tin loại món
        private void btnSuaLoai_Click(object sender, EventArgs e)
        {
            string maLNL = txtMaLoai.Text;
            string tenLL = txtTenLoai.Text;
            string maKH = cmbKhoHang.Text;

            if (maLNL == "" || tenLL == "" || maKH == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
                return;
            }
            Regex specialCharRegex = new Regex("[~`!@#$%^&*()+=|{}':;',\\[\\].<>/?_]");
            if (specialCharRegex.IsMatch(maLNL))
            {
                MessageBox.Show("Mã loại nguyên liệu không được chứa ký tự đặc biệt.");
                return;
            }
            if (specialCharRegex.IsMatch(tenLL))
            {
                MessageBox.Show("Tên loại nguyên liệu không được chứa ký tự đặc biệt.");
                return;
            }
            Regex numberRegex = new Regex("[0-9]");
            if (numberRegex.IsMatch(tenLL))
            {
                MessageBox.Show("Tên loại nguyên liệu không được chứa số.");
                return;
            }
            using (SqlConnection con = new SqlConnection(connectionString)) // tạo kết nối
            {
                try
                {
                    con.Open(); // mở kết nối

                    string checkMaLNLQuery = "SELECT COUNT(*) FROM LoaiSanPham WHERE MaLoaiSanPham = @MaLoaiSanPham";
                    SqlCommand checkMaLNLCommand = new SqlCommand(checkMaLNLQuery, con);
                    checkMaLNLCommand.Parameters.AddWithValue("@MaLoaiSanPham", maLNL);
                    int maLNLCount = (int)checkMaLNLCommand.ExecuteScalar();

                    if (maLNLCount == 0)
                    {
                        MessageBox.Show("Mã loại hàng không tồn tại. Vui lòng chọn mã khác hoặc kiểm tra lại.");
                    }
                    else
                    {
                        // Tiến hành cập nhật thông tin
                        string updateQuery = "UPDATE LoaiSanPham " +
                                                "SET MaKhoHang = @MaKhoHang, TenLoaiSanPham = @TenLoaiSanPham " +
                                                "WHERE MaLoaiSanPham = @MaLoaiSanPham";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                        updateCommand.Parameters.AddWithValue("@MaLoaiSanPham", maLNL);
                        updateCommand.Parameters.AddWithValue("@TenLoaiSanPham", tenLL);
                        updateCommand.Parameters.AddWithValue("@MaKhoHang", maKH);
                        updateCommand.ExecuteNonQuery();
                        updateCommand.Dispose();
                        MessageBox.Show("Đã cập nhật thông tin");
                        LoadDataToGridLoaiSP();
                        clear2();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi" + ex.Message);
                }
            }
        }
        // Xóa loại món
        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string maLNL = txtMaLoai.Text;

                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu?", "Xác nhận xóa", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        string checkRelatedRecordsQuery = "SELECT TOP 1 1 FROM SanPham WHERE MaLoaiSanPham = @MaLoaiSanPham";
                        using (SqlCommand checkRelatedRecordsCommand = new SqlCommand(checkRelatedRecordsQuery, connection))
                        {
                            checkRelatedRecordsCommand.Parameters.AddWithValue("@MaLoaiSanPham", maLNL);
                            object resultObject = checkRelatedRecordsCommand.ExecuteScalar();
                            if (resultObject != null)
                            {
                                MessageBox.Show("Mã loại sản phẩm đã tồn tại trong menu rồi nên không thể xóa được!");
                                return;
                            }
                        }
                        string deleteRecordQuery = "DELETE FROM LoaiSanPham WHERE MaLoaiSanPham = @MaLoaiSanPham";
                        using (SqlCommand deleteRecordCommand = new SqlCommand(deleteRecordQuery, connection))
                        {
                            deleteRecordCommand.Parameters.AddWithValue("@MaLoaiSanPham", maLNL);
                            int rowsAffected = deleteRecordCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa dữ liệu thành công!");
                                LoadDataToGridLoaiSP();
                                clear2();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy mã loại sản phẩm này!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        // Làm mới loại món
        private void btnMoiLoai_Click(object sender, EventArgs e)
        {
            clear2();
            btnThemLoai.Enabled = true;
            txtMaLoai.Enabled = true;
        }
        private void clear2()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
        }
        private void DuLieuLoaiMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemLoai.Enabled = false;
            txtMaLoai.Enabled = false;
            int i = DuLieuLoaiMon.CurrentRow.Index;

            txtMaLoai.Text = DuLieuLoaiMon.Rows[i].Cells[0].Value.ToString();
            txtTenLoai.Text = DuLieuLoaiMon.Rows[i].Cells[2].Value.ToString();
            cmbKhoHang.Text = DuLieuLoaiMon.Rows[i].Cells[1].Value.ToString();
        }

        ///////////////////////////////////////////////////// QUẢN LÝ MÓN \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        private void MenuQlyMenu_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(MenuMon);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuQlyMenu.Enabled = false;
            MenuQlyNhanVien.Enabled = true;
            MenuTinhTrangDon.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            LoadDataToGridMenu();
            LoadDuLieuMaLoaiSP();
        }
        // Load dữ liệu menu
        private void LoadDataToGridMenu()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM SanPham";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuMenuMon.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void LoadDuLieuMaLoaiSP()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaLoaiSanPham FROM LoaiSanPham";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmbMaLoai.DisplayMember = "MaLoaiSanPham";
                    cmbMaLoai.ValueMember = "MaLoaiSanPham";
                    cmbMaLoai.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
                openFileDialog.InitialDirectory = @"""C:\Users\OS\Desktop\PRO231\File\File\Ảnh menu\Ảnh menu""";
                openFileDialog.Title = "Chọn hình ảnh";

                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName; // Lấy tên file
                    picMon.Image = Image.FromFile(fileName); // Hiển thị hình ảnh đã chọn
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName); // Chỉ lấy tên file không có phần mở rộng
                    string fileExtension = Path.GetExtension(fileName); // Lấy phần mở rộng của file
                    string fullPath = System.IO.Path.GetFullPath(fileName); // Đường dẫn đầy đủ

                    HANH.Text = fileName;
                }
            }
        }
        private void picMon_Click_1(object sender, EventArgs e)
        {
        }
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            string MaMOn = txtMaMon.Text;
            string MaLoai = cmbMaLoai.Text;
            string TenMon = txtTenMon.Text;
            string DonGia = txtDonGiaMon.Text;
            string HAc = HANH.Text;
            Regex regex = new Regex(@"^\d+(\.\d+)?$");

            if (MaMOn == "" || MaLoai == "" || TenMon == "" || DonGia == "" || HAc == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
            }
            else if (!regex.IsMatch(DonGia))
            {
                // Đơn giá không phải là số
                MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Kiểm tra trùng mã sản phẩm hoặc mã loại sản phẩm
                        string checkQuery = "SELECT COUNT(*) FROM SanPham WHERE MaSanPham = @MaSanPham";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                        checkCmd.Parameters.AddWithValue("@MaSanPham", MaMOn);

                        int existingCount = (int)checkCmd.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Mã sản phẩm đã tồn tại");
                            return;
                        }
                        else
                        {

                            // Tiếp tục thêm thông tin vào cơ sở dữ liệu
                            string query = "INSERT INTO SanPham (MaSanPham, MaLoaiSanPham, TenSanPham, DonGia, HinhAnh) VALUES (@MaSanPham, @MaLoaiSanPham, @TenSanPham, @DonGia, @HinhAnh)";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@MaSanPham", MaMOn);
                            cmd.Parameters.AddWithValue("@MaLoaiSanPham", MaLoai);
                            cmd.Parameters.AddWithValue("@TenSanPham", TenMon);
                            cmd.Parameters.AddWithValue("@DonGia", DonGia);
                            cmd.Parameters.AddWithValue("@HinhAnh", HAc);



                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            MessageBox.Show("Đã thêm thông tin");
                            LoadDataToGridMenu();
                            clear5();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.ToString());
                    }
                }
            }
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            string MaMon = txtMaMon.Text;
            string MaLoai = cmbMaLoai.Text;
            string TenMon = txtTenMon.Text;
            string DonGia = txtDonGiaMon.Text;
            string HAc = HANH.Text;
            if (MaMon == "" || MaLoai == "" || TenMon == "" || DonGia == "" || HAc == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
            }
            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // Kiểm tra sự tồn tại và trùng mã sản phẩm
                        string checkMaSanPhamQuery = "SELECT COUNT(*) FROM SanPham WHERE MaSanPham = @MaSanPham";
                        SqlCommand checkMaSanPhamCommand = new SqlCommand(checkMaSanPhamQuery, con);
                        checkMaSanPhamCommand.Parameters.AddWithValue("@MaSanPham", MaMon);
                        int maSanPhamCount = (int)checkMaSanPhamCommand.ExecuteScalar();

                        if (maSanPhamCount == 0)
                        {
                            MessageBox.Show("Không tồn tại mã sản phẩm như vậy. Vui lòng kiểm tra lại.");
                        }
                        else
                        {
                            string updateQuery = "UPDATE SanPham " +
                                                 "SET MaLoaiSanPham = @MaLoaiSanPham, TenSanPham = @TenSanPham, DonGia = @DonGia,HinhAnh = @HinhAnh " +
                                                 "WHERE MaSanPham = @MaSanPham";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                            updateCommand.Parameters.AddWithValue("@MaLoaiSanPham", MaLoai);
                            updateCommand.Parameters.AddWithValue("@TenSanPham", TenMon);
                            updateCommand.Parameters.AddWithValue("@DonGia", DonGia);
                            updateCommand.Parameters.AddWithValue("@MaSanPham", MaMon);
                            updateCommand.Parameters.AddWithValue("@HinhAnh", HAc);
                            updateCommand.ExecuteNonQuery();
                            updateCommand.Dispose();

                            MessageBox.Show("Đã cập nhật thông tin");
                            LoadDataToGridMenu();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Còn đang bị lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string maMon = txtMaMon.Text;

                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu?", "Xác nhận xóa", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        // Kiểm tra xem có bản ghi liên quan trong bảng ChiTietHoaDon hay không
                        string checkRelatedRecordsQuery = "SELECT TOP 1 1 FROM ChiTietHoaDon WHERE MaSanPham = @MaSanPham";
                        using (SqlCommand checkRelatedRecordsCommand = new SqlCommand(checkRelatedRecordsQuery, connection))
                        {
                            checkRelatedRecordsCommand.Parameters.AddWithValue("@MaSanPham", maMon);
                            object resultObject = checkRelatedRecordsCommand.ExecuteScalar();
                            if (resultObject != null)
                            {
                                MessageBox.Show("Mã sản phẩm đã tồn tại trong chi tiết hóa đơn, không thể xóa!");
                                return;
                            }
                        }

                        // Xóa bản ghi trong bảng SanPham
                        string deleteRecordQuery = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham";
                        using (SqlCommand deleteRecordCommand = new SqlCommand(deleteRecordQuery, connection))
                        {
                            deleteRecordCommand.Parameters.AddWithValue("@MaSanPham", maMon);
                            int rowsAffected = deleteRecordCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa dữ liệu thành công!");
                                LoadDataToGridMenu();
                                clear5();
                            }
                            else
                            {
                                MessageBox.Show("Không có mã sản phẩm như vậy!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnMoiMon_Click(object sender, EventArgs e)
        {
            clear5();
            btnThemMon.Enabled = true;
            txtMaMon.Enabled = true;
        }
        private void clear5()
        {
            txtMaMon.Clear();
            txtMaLoai.Clear();
            txtTenMon.Clear();
            txtDonGiaMon.Clear();
            picMon.Image = null;
        }

        private void DuLieuMenuMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemMon.Enabled = false;
            txtMaMon.Enabled = false;
            int i = DuLieuMenuMon.CurrentRow.Index;

            txtMaMon.Text = DuLieuMenuMon.Rows[i].Cells[0].Value.ToString();
            cmbMaLoai.Text = DuLieuMenuMon.Rows[i].Cells[1].Value.ToString();
            txtTenMon.Text = DuLieuMenuMon.Rows[i].Cells[2].Value.ToString();
            txtDonGiaMon.Text = DuLieuMenuMon.Rows[i].Cells[3].Value.ToString();
            string imagePath = DuLieuMenuMon.Rows[i].Cells[4].Value.ToString();
            if (File.Exists(imagePath))
            {
                picMon.Image = Image.FromFile(imagePath);
            }
            else
            {
                picMon.Image = null;
            }
        }




        ///////////////////////////////////////////////////// PHIẾU NHẬP \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///Tạo Mã Phiếu Nhập RanDom
        private void MenuPhieuNhap_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(PhieuNhap);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuPhieuNhap.Enabled = false;
            MenuTinhTrangDon.Enabled = true;
            MenuQlyNhanVien.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;

            // Lấy mã phiếu nhập kho tiếp theo
            string MaPhieuNhapKhoRandom = GetNextMaPN(connection);
            MaPhieuNhapKho = MaPhieuNhapKhoRandom;

            PN_lbMaNV.Text = DN_txtManv.Text;
            connection.Open();
            string Query = $"Insert Into PhieuNhapKho(MaPhieuNhapKho) Values ('{MaPhieuNhapKho}')";
            SqlCommand sqlCommand = new SqlCommand(Query, connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            LoadNLieu_PN();
            LoadCN_PN();
            LoadDataToGridPN(MaPhieuNhapKho);
        }

        private string GetNextMaPN(SqlConnection connection)
        {
            connection.Open(); // Mở kết nối với cơ sở dữ liệu

            string query = "SELECT MAX(MaPhieuNhapKho) FROM PhieuNhapKho";

            SqlCommand command = new SqlCommand(query, connection);

            object result = command.ExecuteScalar();

            connection.Close(); // Đóng kết nối với cơ sở dữ liệu

            string maxMaPN = result != DBNull.Value ? result.ToString() : string.Empty;

            if (!string.IsNullOrEmpty(maxMaPN))
            {
                int nextNumber = int.Parse(maxMaPN.Substring(2)) + 1;

                string nextMaPN = "PN" + nextNumber.ToString("D3");

                return nextMaPN;
            }
            else
            {
                return "PN001";
            }
        }

        public string MaPhieuNhapKho { get; set; }
        private void LoadDataToGridPN(string MaPhieuNhap)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM ChiTietPhieuNhapKho Where MaPhieuNhapKho = '{MaPhieuNhap}'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewPN.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void HD_txtDonGiaSP_TextChanged(object sender, EventArgs e)
        {
            string input = HD_txtDonGiaSP.Text;
            double donGia;

            if (!double.TryParse(input, out donGia))
            {
                // Hiển thị thông báo lỗi khi người dùng nhập chữ
                MessageBox.Show("Vui lòng nhập một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HD_txtDonGiaSP.Text = ""; // Xóa nội dung nhập vào
            }
        }
        private void LoadNLieu_PN()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM NguyenLieu";
                string queryTenSP = $"SELECT TenNguyenLieu FROM NguyenLieu";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieu" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "PN_BtnNLieu" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenNguyenLieu"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadCN_PN()
        {

            PN_lbMaPN.Text = MaPhieuNhapKho;
            // Hiển thị ngày tháng
            PN_lbNgayNhap.Text = DateTime.Now.ToString("yyyy/MM/dd");

            // Hiển thị thời gian hiện tịa
            timer1 = new Timer();
            timer1.Interval = 1000; // Cập nhật mỗi giây (1000ms)
            timer1.Tick += timer1_Tick;
            timer1.Start();




            //Truyền dữ liệu vào Cmb
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Chuỗi truy vấn SQL để lấy danh sách mã nhà cung cấp
                string query = "SELECT MaNhaCungCap FROM NhaCungCap";

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
                            PN_cmbMaNCC.Items.Add(maNCC);
                        }
                    }
                }
            }
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
                            string maNLieu = reader.GetString(0);

                            // Thêm giá trị vào ComboBox
                            PN_CmbLNKLieu.Items.Add(maNLieu);
                        }
                        PN_CmbLNKLieu.Items.Add("Tất cả");
                    }
                }
            }






        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PN_lbThoiGianNhap.Text = DateTime.Now.ToString("HH:mm:ss:tt");
        }

        private void PN_cmbMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Open();
            PN_lbMaNCC.Text = PN_cmbMaNCC.Text;
            string query = $"SELECT TenNhaCungCap FROM NhaCungCap WHERE MaNhaCungCap = '{PN_cmbMaNCC.Text}'";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string tenNhaCungCap = reader.GetString(0);

                    PN_lbTenNCC.Text = tenNhaCungCap;
                }
            }
            connection.Close();
        }

        private void EnabledBtn()
        {
            for (int i = 1; i <= 55; i++)
            {
                string buttonName = "PN_BtnNLieu" + i.ToString();
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                if (button != null)
                {
                    button.Visible = false;
                }
            }
        }
        private void PN_CmbLNKLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string LoaiNguyenLieu = PN_CmbLNKLieu.Text;
            EnabledBtn();

            if(LoaiNguyenLieu == "Tất cả")
            {
                LoadNLieu_PN();
                return;
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM NguyenLieu Where MaLoaiNguyenLieu = '{LoaiNguyenLieu}'";
                string queryTenSP = $"SELECT TenNguyenLieu FROM NguyenLieu whERE MaLoaiNguyenLieu = '{LoaiNguyenLieu}' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieu" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "PN_BtnNLieu" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenNguyenLieu"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }

        }

        private void PN_btnFindNLieu_Click(object sender, EventArgs e)
        {
            string TenNguyenLieu = PN_txtFindTenNLieu.Text;
            EnabledBtn();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM NguyenLieu Where TenNguyenLieu Like N'%{TenNguyenLieu}%'";
                string queryTenSP = $"SELECT TenNguyenLieu FROM NguyenLieu whERE TenNguyenLieu Like N'%{TenNguyenLieu}%' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieu" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "PN_BtnNLieu" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenNguyenLieu"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HienthiThongTinNguyenLieu(string TenNguyenLieu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM NguyenLieu WHERE TenNguyenLieu = N'{TenNguyenLieu}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Đọc dữ liệu từ cột và hiển thị lên các text box
                            PN_txtMaNLieu.Text = reader["MaNguyenLieu"].ToString();
                            PN_txtTenNLieu.Text = reader["TenNguyenLieu"].ToString();
                            // Thêm các trường còn lại tương ứng
                        }
                        else
                        {
                            MessageBox.Show("Nguyên Liệu không được tìm thấy");
                        }
                    }
                }
            }
        }

        private void PN_BtnNLieu1_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu1.Text);

        }

        private void PN_BtnNLieu2_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu2.Text);
        }

        private void PN_BtnNLieu3_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu3.Text);
        }

        private void PN_BtnNLieu4_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu4.Text);
        }

        private void PN_BtnNLieu5_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu5.Text);
        }

        private void PN_BtnNLieu6_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu6.Text);
        }

        private void PN_BtnNLieu7_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu7.Text);
        }

        private void PN_BtnNLieu8_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu8.Text);
        }

        private void PN_BtnNLieu9_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu9.Text);
        }

        private void PN_BtnNLieu10_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu10.Text);
        }

        private void PN_BtnNLieu11_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu11.Text);
        }

        private void PN_BtnNLieu12_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu12.Text);
        }

        private void PN_BtnNLieu13_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu13.Text);
        }

        private void PN_BtnNLieu14_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu14.Text);
        }

        private void PN_BtnNLieu15_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu15.Text);
        }

        private void PN_BtnNLieu16_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu16.Text);
        }

        private void PN_BtnNLieu17_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu17.Text);
        }

        private void PN_BtnNLieu18_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu18.Text);
        }

        private void PN_BtnNLieu19_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu19.Text);
        }

        private void PN_BtnNLieu20_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu20.Text);
        }

        private void PN_BtnNLieu21_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu21.Text);
        }

        private void PN_BtnNLieu22_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu22.Text);
        }

        private void PN_BtnNLieu23_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu23.Text);
        }

        private void PN_BtnNLieu24_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu24.Text);
        }

        private void PN_BtnNLieu25_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu25.Text);
        }

        private void PN_BtnNLieu26_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu26.Text);
        }

        private void PN_BtnNLieu27_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu27.Text);
        }

        private void PN_BtnNLieu28_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu28.Text);
        }

        private void PN_BtnNLieu29_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu29.Text);
        }

        private void PN_BtnNLieu30_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu30.Text);
        }

        private void PN_BtnNLieu31_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu31.Text);
        }

        private void PN_BtnNLieu32_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu32.Text);
        }

        private void PN_BtnNLieu33_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu33.Text);
        }

        private void PN_BtnNLieu34_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu34.Text);
        }

        private void PN_BtnNLieu35_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu35.Text);
        }

        private void PN_BtnNLieu36_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu36.Text);
        }

        private void PN_BtnNLieu37_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu37.Text);
        }

        private void PN_BtnNLieu38_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu38.Text);
        }

        private void PN_BtnNLieu39_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu39.Text);
        }

        private void PN_BtnNLieu40_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu40.Text);
        }

        private void PN_BtnNLieu41_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu41.Text);
        }

        private void PN_BtnNLieu42_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu42.Text);
        }

        private void PN_BtnNLieu43_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu43.Text);
        }

        private void PN_BtnNLieu44_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu44.Text);
        }

        private void PN_BtnNLieu45_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu45.Text);
        }

        private void PN_BtnNLieu46_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu46.Text);
        }

        private void PN_BtnNLieu48_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu48.Text);
        }

        private void PN_BtnNLieu49_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu49.Text);
        }

        private void PN_BtnNLieu50_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu50.Text);
        }

        private void PN_BtnNLieu51_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu51.Text);
        }

        private void PN_BtnNLieu52_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu52.Text);
        }

        private void PN_BtnNLieu53_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu53.Text);
        }

        private void PN_BtnNLieu54_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu54.Text);
        }

        private void PN_BtnNLieu55_Click(object sender, EventArgs e)
        {
            panel18.Visible = true;
            HienthiThongTinNguyenLieu(PN_BtnNLieu55.Text);
        }

        private void PN_btnHuyNLieu_Click(object sender, EventArgs e)
        {
            panel18.Visible = false;
        }

        private void PN_UpDownSoLuong_ValueChanged(object sender, EventArgs e)
        {
            int SoLuong = (int)PN_UpDownSoLuong.Value;

            float DonGia = float.Parse(PN_txtDonGia.Text);

            PN_txtThanhTien.Text = (SoLuong * DonGia).ToString();

            if (SoLuong > 0)
            {
                PN_btnXacNhanNLieu.Enabled = true;
            }
            else
            {
                PN_btnXacNhanNLieu.Enabled = false;
            }
        }

        private void PN_txtDonGia_TextChanged(object sender, EventArgs e)
        {
            int SoLuong = (int)PN_UpDownSoLuong.Value;

            float DonGia = float.Parse(PN_txtDonGia.Text);

            PN_txtThanhTien.Text = (SoLuong * DonGia).ToString();
        }

        private void PN_btnXacNhanNLieu_Click(object sender, EventArgs e)
        {
            string MaPNK = MaPhieuNhapKho;
            string MaNLieu = PN_txtMaNLieu.Text;
            int SoLuong = (int)PN_UpDownSoLuong.Value;
            float DonGia = float.Parse(PN_txtDonGia.Text);
            float ThanhTien = float.Parse(PN_txtThanhTien.Text);
            Regex regex = new Regex(@"^\d+(\.\d+)?$");

            if (!regex.IsMatch(DonGia.ToString()))
            {
                // Đơn giá không phải là số
                MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Open();

            //Kiếm tra xem có tồn tại hay chưa
            string queryKiemTra = $"SELECT COUNT(*) FROM ChiTietPhieuNhapKho WHERE MaPhieuNhapKho = '{MaPNK}' AND MaNguyenLieu = '{MaNLieu}'";
            SqlCommand commandKiemTra = new SqlCommand(queryKiemTra, connection);
            int count = (int)commandKiemTra.ExecuteScalar();
            if (count > 0)
            {
                string Query = $"UPDATE ChiTietPhieuNhapKho SET SoLuong = '{SoLuong}', DonGia = '{DonGia}', ThanhTien = '{ThanhTien}' WHERE MaPhieuNhapKho = '{MaPNK}' AND MaNguyenLieu = '{MaNLieu}'";
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Cập nhật thành công");
                PN_UpDownSoLuong.Value = 0;

                LoadDataToGridPN(MaPhieuNhapKho);

            }
            else
            {
                string Query = $"Insert into ChiTietPhieuNhapKho (MaPhieuNhapKho,MaNguyenLieu,SoLuong,DonGia,ThanhTien) Values ('{MaPNK}','{MaNLieu}','{SoLuong}','{DonGia}','{ThanhTien}')";
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                LoadDataToGridPN(MaPhieuNhapKho);
            }

            //Hiển thị tổng tiền 
            // Khai báo biến tổng
            float tongTien = 0;

            // Lặp qua từng dòng trong DataGridView
            foreach (DataGridViewRow row in dataGridViewPN.Rows)
            {
                // Kiểm tra nếu dòng không phải dòng mới
                if (!row.IsNewRow)
                {
                    // Lấy giá trị của cột thành tiền (giả sử cột đó có tên "colThanhTien")
                    float thanhTien = Convert.ToSingle(row.Cells["ThanhTien"].Value);

                    // Cộng giá trị thành tiền vào tổng
                    tongTien += thanhTien;
                }
            }
            PN_lbTongTien.Text = tongTien.ToString();

            panel18.Visible = false;
            PN_btnLuuPN.Enabled = true;
        }

        private void PN_btnLuuPN_Click(object sender, EventArgs e)
        {
            string MaPhieuNhap = MaPhieuNhapKho;
            string MaKhoHang = "KHO1";
            string MaNCC = PN_lbMaNCC.Text;
            string MaNV = PN_lbMaNV.Text;
            string NgayNhap = PN_lbNgayNhap.Text;
            string ThoiGianNhap = PN_lbThoiGianNhap.Text;

            DateTime ngayNhap;
            if (!DateTime.TryParseExact(NgayNhap, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayNhap))
            {
                // Ngày nhập không hợp lệ, xử lý lỗi tại đây
            }

            DateTime thoiGianNhap;
            if (!DateTime.TryParseExact(ThoiGianNhap, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out thoiGianNhap))
            {
                // Thời gian nhập không hợp lệ, xử lý lỗi tại đây
            }

            DateTime NgayGioNhap = ngayNhap.Date + thoiGianNhap.TimeOfDay;
            float TongTien = float.Parse(PN_lbTongTien.Text);


            if (PN_cmbMaNCC.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Nhà cung cấp", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(PN_cmbMaNCC, "");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            string query = "UPDATE PhieuNhapKho SET MaKhoHang = @MaKhoHang,MaNhanVien = @MaNhanVien, MaNhaCungCap = @MaNCC, NgayNhap = @NgayNhap, TongTien = @TongTien WHERE MaPhieuNhapKho = @MaPhieuNhap";

            // Khởi tạo kết nối và đối tượng Command
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm các tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("@MaPhieuNhap", MaPhieuNhap);
                    command.Parameters.AddWithValue("@MaNhanVien", MaNV);
                    command.Parameters.AddWithValue("@MaKhoHang", MaKhoHang);
                    command.Parameters.AddWithValue("@MaNCC", MaNCC);
                    command.Parameters.AddWithValue("@NgayNhap", NgayGioNhap);
                    command.Parameters.AddWithValue("@TongTien", TongTien);

                    // Mở kết nối
                    connection.Open();

                    // Thực thi câu lệnh UPDATE
                    command.ExecuteNonQuery();

                    // Đóng kết nối
                    connection.Close();

                    MessageBox.Show("Lưu Phiếu Thành Công");
                    TrangChu_Load(sender, e);
                    MenuPhieuNhap.Enabled = true;
                    PN_btnLuuPN.Enabled = false;
                    PN_CmbLNKLieu.Items.Clear();
                    PN_cmbMaNCC.Items.Clear();
                    PN_lbMaNCC.Text = "";
                    PN_lbTongTien.Text = "";
                    PN_lbTenNCC.Text = "";
                    PN_cmbMaNCC.Text = "";
                }

            }
            connection.Open();

            //Cộng thêm Số lượng vào bảng Nguyên Liệu 
            for (int i = 0; i < dataGridViewPN.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridViewPN.Rows[i];

                // Lấy giá trị từ các ô trong dòng
                string MaNLieu = row.Cells["MaNguyenLieu"].FormattedValue.ToString();
                int soLuong;
                if (int.TryParse(row.Cells["SoLuong"].FormattedValue.ToString(), out soLuong))
                {
                    // Thực hiện các thao tác cần thiết với giá trị soLuong đã chuyển đổi thành công
                    string QueryLaySLNguyenLieu = $"SELECT SoLuong FROM NguyenLieu WHERE MaNguyenLieu = '{MaNLieu}'";

                    using (SqlCommand Cmd = new SqlCommand(QueryLaySLNguyenLieu, connection))
                    {
                        SqlDataReader reader = Cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int SoLuongLay = reader.GetInt32(0);
                            int SLUpdate = SoLuongLay + soLuong;
                            reader.Close();
                            string QueryNhapSL = $"UPDATE NguyenLieu SET SoLuong = '{SLUpdate}' WHERE MaNguyenLieu = '{MaNLieu}'";
                            using (SqlCommand CmdUpdateSL = new SqlCommand(QueryNhapSL, connection))
                            {
                                CmdUpdateSL.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            connection.Close();




        }

        private void PN_btnHuyPN_Click(object sender, EventArgs e)
        {
            string MaPN = MaPhieuNhapKho;


            if (dataGridViewPN.RowCount >= 1)
            {
                connection.Open();
                string query = $"Delete from ChiTietPhieuNhapKho where MaPhieuNhapKho = '{MaPN}'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();


                string query2 = $"Delete from PhieuNhapKho where MaPhieuNhapKho = '{MaPN}'";
                SqlCommand sqlCommand = new SqlCommand(query2, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();



                MessageBox.Show("Hủy phiếu Thành công");
                TrangChu_Load(sender, e);
                PN_btnLuuPN.Enabled = false;
                MenuPhieuNhap.Enabled = true;
                PN_CmbLNKLieu.Items.Clear();
                PN_cmbMaNCC.Items.Clear();
                PN_lbMaNCC.Text = "";
                PN_lbTongTien.Text = "";
                PN_lbTenNCC.Text = "";
                PN_cmbMaNCC.Text = "";



            }
            else
            {
                connection.Open();
                string query2 = $"Delete from PhieuNhapKho where MaPhieuNhapKho = '{MaPN}'";
                SqlCommand sqlCommand = new SqlCommand(query2, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Hủy phiếu Thành công");
                TrangChu_Load(sender, e);
                MenuPhieuNhap.Enabled = true;
                PN_btnLuuPN.Enabled = false;
                PN_CmbLNKLieu.Items.Clear();
                PN_cmbMaNCC.Items.Clear();
                PN_lbMaNCC.Text = "";
                PN_lbTongTien.Text = "";
                PN_lbTenNCC.Text = "";
                PN_cmbMaNCC.Text = "";



            }

        }


        //////////////////////////////////////////// Phiếu Xuất \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void MenuPhieuXuat_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(PhieuXuat);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuPhieuXuat.Enabled = false;
            MenuQlyNhanVien.Enabled = true;
            MenuTinhTrangDon.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;

            PX_lbMaNV.Text = DN_txtManv.Text;
            // Lấy mã phiếu nhập kho tiếp theo
            string MaPhieuXuatKhoRandom = GetNextMaPX(connection);
            MaPhieuXuatKho = MaPhieuXuatKhoRandom;

            connection.Open();
            string Query = $"Insert Into PhieuXuatKho(MaPhieuXuatKho) Values ('{MaPhieuXuatKho}')";
            SqlCommand sqlCommand = new SqlCommand(Query, connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            LoadNLieu_PX();
            LoadCN_PX();
            LoadDataToGridPX(MaPhieuXuatKho);
        }

        private string GetNextMaPX(SqlConnection connection)
        {
            connection.Open(); // Mở kết nối với cơ sở dữ liệu

            string query = "SELECT MAX(MaPhieuXuatKho) FROM PhieuXuatKho";

            SqlCommand command = new SqlCommand(query, connection);

            object result = command.ExecuteScalar();

            connection.Close(); // Đóng kết nối với cơ sở dữ liệu

            string maxMaPN = result != DBNull.Value ? result.ToString() : string.Empty;

            if (!string.IsNullOrEmpty(maxMaPN))
            {
                int nextNumber = int.Parse(maxMaPN.Substring(2)) + 1;

                string nextMaPN = "PX" + nextNumber.ToString("D3");

                return nextMaPN;
            }
            else
            {
                return "PX001";
            }
        }

        public string MaPhieuXuatKho { get; set; }
        private void LoadDataToGridPX(string MaXuatNhap)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM ChiTietPhieuXuatKho Where MaPhieuXuatKho = '{MaXuatNhap}'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewPX.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private void LoadNLieu_PX()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM NguyenLieu";
                string queryTenSP = $"SELECT TenNguyenLieu FROM NguyenLieu";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieuX" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "PN_BtnNLieuX" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenNguyenLieu"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadCN_PX()
        {

            PX_lbMaPX.Text = MaPhieuXuatKho;
            // Hiển thị ngày tháng
            PX_lbNgayXuat.Text = DateTime.Now.ToString("yyyy/MM/dd");

            // Hiển thị thời gian hiện tịa
            timer2 = new Timer();
            timer2.Interval = 1000; // Cập nhật mỗi giây (1000ms)
            timer2.Tick += timer2_Tick;
            timer2.Start();




            //Truyền dữ liệu vào Cmb
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Chuỗi truy vấn SQL để lấy danh sách mã nhà cung cấp
                string query = "SELECT MaKhoHang FROM KhoHang";

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
                            string maKho = reader.GetString(0);

                            // Thêm giá trị vào ComboBox
                            PX_cmbMaKho.Items.Add(maKho);
                        }
                    }
                }
            }
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
                            string maNLieu = reader.GetString(0);

                            // Thêm giá trị vào ComboBox
                            PX_cmbLNLieu.Items.Add(maNLieu);
                        }
                        PX_cmbLNLieu.Items.Add("Tất cả");
                    }
                }
            }






        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            PX_lbThoiGianXuat.Text = DateTime.Now.ToString("HH:mm:ss:tt");

        }

        private void PX_cmbMaKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            PX_lbMaKho.Text = PX_cmbMaKho.Text;

        }
        private void EnabledBtnPX()
        {
            for (int i = 1; i <= 55; i++)
            {
                string buttonName = "PN_BtnNLieuX" + i.ToString();
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                if (button != null)
                {
                    button.Visible = false;
                }
            }
        }

        private void PX_cmbLNLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string LoaiNguyenLieu = PX_cmbLNLieu.Text;
            EnabledBtnPX();

            if(LoaiNguyenLieu == "Tất cả")
            {
                LoadNLieu_PX();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM NguyenLieu Where MaLoaiNguyenLieu = '{LoaiNguyenLieu}'";
                string queryTenSP = $"SELECT TenNguyenLieu FROM NguyenLieu whERE MaLoaiNguyenLieu = '{LoaiNguyenLieu}' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieuX" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "PN_BtnNLieuX" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenNguyenLieu"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PX_btnFind_Click(object sender, EventArgs e)
        {
            string TenNguyenLieu = PX_txtFindTenNLieu.Text;
            EnabledBtnPX();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM NguyenLieu Where TenNguyenLieu Like N'%{TenNguyenLieu}%'";
                string queryTenSP = $"SELECT TenNguyenLieu FROM NguyenLieu whERE TenNguyenLieu Like N'%{TenNguyenLieu}%' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieuX" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "PN_BtnNLieuX" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenNguyenLieu"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HienthiThongTinNguyenLieuPX(string TenNguyenLieu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM NguyenLieu WHERE TenNguyenLieu = N'{TenNguyenLieu}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Đọc dữ liệu từ cột và hiển thị lên các text box
                            PX_txtMaNLieu.Text = reader["MaNguyenLieu"].ToString();
                            PX_txtTenNLieu.Text = reader["TenNguyenLieu"].ToString();
                            // Thêm các trường còn lại tương ứng
                        }
                        else
                        {
                            MessageBox.Show("Nguyên Liệu không được tìm thấy");
                        }
                    }
                }
            }
        }



        private void PN_BtnNLieuX1_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX1.Text);
        }

        private void PN_BtnNLieuX2_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX2.Text);
        }

        private void PN_BtnNLieuX3_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX3.Text);
        }

        private void PN_BtnNLieuX4_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX4.Text);
        }

        private void PN_BtnNLieuX5_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX5.Text);
        }

        private void PN_BtnNLieuX6_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX6.Text);
        }

        private void PN_BtnNLieuX7_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX7.Text);
        }

        private void PN_BtnNLieuX8_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX8.Text);
        }

        private void PN_BtnNLieuX9_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX9.Text);
        }

        private void PN_BtnNLieuX10_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX10.Text);
        }

        private void PN_BtnNLieuX11_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX11.Text);
        }

        private void PN_BtnNLieuX12_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX12.Text);
        }

        private void PN_BtnNLieuX13_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX13.Text);
        }

        private void PN_BtnNLieuX14_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX14.Text);
        }

        private void PN_BtnNLieuX15_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX15.Text);
        }

        private void PN_BtnNLieuX16_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX16.Text);
        }

        private void PN_BtnNLieuX17_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX17.Text);
        }

        private void PN_BtnNLieuX18_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX18.Text);
        }

        private void PN_BtnNLieuX19_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX19.Text);
        }

        private void PN_BtnNLieuX20_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX20.Text);
        }

        private void PN_BtnNLieuX21_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX21.Text);
        }

        private void PN_BtnNLieuX22_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX22.Text);
        }

        private void PN_BtnNLieuX23_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX23.Text);
        }

        private void PN_BtnNLieuX24_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX24.Text);
        }

        private void PN_BtnNLieuX25_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX25.Text);
        }

        private void PN_BtnNLieuX26_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX26.Text);
        }

        private void PN_BtnNLieuX27_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX27.Text);
        }

        private void PN_BtnNLieuX28_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX28.Text);
        }

        private void PN_BtnNLieuX29_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX29.Text);
        }

        private void PN_BtnNLieuX30_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX30.Text);
        }

        private void PN_BtnNLieuX31_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX31.Text);
        }

        private void PN_BtnNLieuX32_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX32.Text);
        }

        private void PN_BtnNLieuX33_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX33.Text);
        }

        private void PN_BtnNLieuX34_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX34.Text);
        }

        private void PN_BtnNLieuX35_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX35.Text);
        }

        private void PN_BtnNLieuX36_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX36.Text);
        }

        private void PN_BtnNLieuX37_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX37.Text);
        }

        private void PN_BtnNLieuX38_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX38.Text);
        }

        private void PN_BtnNLieuX39_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX39.Text);
        }

        private void PN_BtnNLieuX40_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX40.Text);
        }

        private void PN_BtnNLieuX41_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX41.Text);
        }

        private void PN_BtnNLieuX42_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX42.Text);
        }

        private void PN_BtnNLieuX43_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX43.Text);
        }

        private void PN_BtnNLieuX44_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX44.Text);
        }

        private void PN_BtnNLieuX45_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX45.Text);
        }

        private void PN_BtnNLieuX46_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX46.Text);
        }

        private void PN_BtnNLieuX47_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX47.Text);
        }

        private void PN_BtnNLieuX48_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX48.Text);
        }

        private void PN_BtnNLieuX49_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX49.Text);
        }

        private void PN_BtnNLieuX50_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX50.Text);
        }

        private void PN_BtnNLieuX51_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX51.Text);
        }

        private void PN_BtnNLieuX52_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX52.Text);
        }

        private void PN_BtnNLieuX53_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX53.Text);
        }

        private void PN_BtnNLieuX54_Click(object sender, EventArgs e)
        {
            panel14.Visible = true;
            HienthiThongTinNguyenLieuPX(PN_BtnNLieuX54.Text);
        }

        private void PX_UpDownSL_ValueChanged(object sender, EventArgs e)
        {
            int SoLuong = (int)PX_UpDownSL.Value;


            if (SoLuong > 0)
            {
                PX_btnXacNhan.Enabled = true;
            }
            else
            {
                PX_btnXacNhan.Enabled = false;
            }
        }
        private void PX_btnHuy_Click(object sender, EventArgs e)
        {
            panel14.Visible = false;

        }

        private void PX_btnXacNhan_Click(object sender, EventArgs e)
        {
            string MaPXK = MaPhieuXuatKho;
            string MaNLieu = PX_txtMaNLieu.Text;
            int SoLuong = (int)PX_UpDownSL.Value;
            string DonVi = PX_cmbDonVi.Text;


            connection.Open();

            //Kiếm tra xem có tồn tại hay chưa
            string queryKiemTra = $"SELECT COUNT(*) FROM ChiTietPhieuXuatKho WHERE MaPhieuXuatKho = '{MaPXK}' AND MaNguyenLieu = '{MaNLieu}'";
            SqlCommand commandKiemTra = new SqlCommand(queryKiemTra, connection);
            int count = (int)commandKiemTra.ExecuteScalar();
            if (count > 0)
            {
                string Query = $"UPDATE ChiTietPhieuXuatKho SET SoLuong = '{SoLuong}',DonVi = '{DonVi}' WHERE MaPhieuXuatKho = '{MaPXK}' AND MaNguyenLieu = '{MaNLieu}'";
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Cập nhật thành công");
                LoadDataToGridPX(MaPhieuXuatKho);
            }
            else
            {
                string Query = $"Insert into ChiTietPhieuXuatKho (MaPhieuXuatKho,MaNguyenLieu,SoLuong,DonVi) Values ('{MaPXK}','{MaNLieu}','{SoLuong}','{DonVi}')";
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                LoadDataToGridPX(MaPhieuXuatKho);
            }





            panel14.Visible = false;
            PX_btnLuuPX.Enabled = true;
        }

        private void PX_btnLuuPX_Click(object sender, EventArgs e)
        {
            string MaPhieuXuat = MaPhieuXuatKho;
            string MaKhoHang = PX_lbMaKho.Text;
            string MaNV = PX_lbMaNV.Text;
            string NgayXuat = PX_lbNgayXuat.Text;
            string ThoiGianXuat = PX_lbThoiGianXuat.Text;

            string DateTimeXuatStr = NgayXuat + " " + ThoiGianXuat;
            MessageBox.Show(DateTimeXuatStr);
            DateTime DateTimeXuat;

            if (DateTime.TryParseExact(DateTimeXuatStr, "yyyy/MM/dd HH:mm:ss:tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeXuat))
            {
                MessageBox.Show("Ngày và giờ xuất không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (PX_lbMaKho.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Kho Hàng", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(PX_lbMaKho, "");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            string query = "UPDATE PhieuXuatKho SET MaKhoHang = @MaKhoHang, MaNhanVien = @MaNhanVien, NgayXuat = @NgayXuat  WHERE MaPhieuXuatKho = @MaPhieuXuat";

            // Khởi tạo kết nối và đối tượng Command
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm các tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("@MaKhoHang", MaKhoHang);
                    command.Parameters.AddWithValue("@MaNhanVien", "NV001");
                    command.Parameters.AddWithValue("@NgayXuat", DateTimeXuat);
                    command.Parameters.AddWithValue("@MaPhieuXuat", MaPhieuXuat);

                    // Mở kết nối
                    connection.Open();

                    // Thực thi câu lệnh UPDATE
                    command.ExecuteNonQuery();

                    // Đóng kết nối
                    connection.Close();

                    MessageBox.Show("Lưu Phiếu Thành Công");
                    TrangChu_Load(sender, e);
                    MenuPhieuXuat.Enabled = true;
                    PX_cmbLNLieu.Items.Clear();
                    PX_cmbMaKho.Items.Clear();
                    PX_lbMaKho.Text = "";
                    PX_cmbMaKho.Text = "";
                }
            }
            connection.Open();

            //Trừ thêm Số lượng vào bảng Nguyên Liệu 
            for (int i = 0; i < dataGridViewPX.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridViewPX.Rows[i];

                // Lấy giá trị từ các ô trong dòng
                string MaNLieu = row.Cells["MaNguyenLieu"].FormattedValue.ToString();
                int soLuong;
                if (int.TryParse(row.Cells["SoLuong"].FormattedValue.ToString(), out soLuong))
                {
                    // Thực hiện các thao tác cần thiết với giá trị soLuong đã chuyển đổi thành công
                    string QueryLaySLNguyenLieu = $"SELECT SoLuong FROM NguyenLieu WHERE MaNguyenLieu = '{MaNLieu}'";

                    using (SqlCommand Cmd = new SqlCommand(QueryLaySLNguyenLieu, connection))
                    {
                        SqlDataReader reader = Cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int SoLuongLay = reader.GetInt32(0);
                            int SLUpdate = SoLuongLay - soLuong;
                            reader.Close();
                            string QueryNhapSL = $"UPDATE NguyenLieu SET SoLuong = '{SLUpdate}' WHERE MaNguyenLieu = '{MaNLieu}'";
                            using (SqlCommand CmdUpdateSL = new SqlCommand(QueryNhapSL, connection))
                            {
                                CmdUpdateSL.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            connection.Close();


        }

        private void PX_btnHuyPX_Click(object sender, EventArgs e)
        {
            string MaPX = MaPhieuXuatKho;


            if (dataGridViewPX.RowCount >= 1)
            {
                connection.Open();
                string query = $"Delete from ChiTietPhieuXuatKho where MaPhieuXuatKho = '{MaPX}'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();


                string query2 = $"Delete from PhieuXuatKho where MaPhieuXuatKho = '{MaPX}'";
                SqlCommand sqlCommand = new SqlCommand(query2, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();



                MessageBox.Show("Hủy phiếu Thành công");
                TrangChu_Load(sender, e);
                MenuPhieuXuat.Enabled = true;
                PX_cmbLNLieu.Items.Clear();
                PX_cmbMaKho.Items.Clear();
                PX_lbMaKho.Text = "";
                PX_cmbMaKho.Text = "";



            }
            else
            {
                connection.Open();
                string query2 = $"Delete from PhieuXuatKho where MaPhieuXuatpKho = '{MaPX}'";
                SqlCommand sqlCommand = new SqlCommand(query2, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();



                MessageBox.Show("Hủy phiếu Thành công");
                TrangChu_Load(sender, e);
                MenuPhieuXuat.Enabled = true;
                PX_cmbLNLieu.Items.Clear();
                PX_cmbMaKho.Items.Clear();
                PX_lbMaKho.Text = "";
                PX_cmbMaKho.Text = "";
            }
        }



        ////////////////////////////////////////// Hóa Đơn \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


        // Lấy dữ liệu và xuất Hóa đơn PDF // 
        public string MaHoaDonGet { get { return HD_lbMaHD.Text; } }
        public string MaKhuyenMaiGet { get { return HD_lbGTGG.Text; } }
        private void GanDuLieuMaNV()
        {
            HD_lbMaNV.Text = DN_txtManv.Text;
            string query = @"SELECT TenNhanVien FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", DN_txtManv.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string tenNhanVien = reader.GetString(0);
                            TenNhanVien_lb.Text = tenNhanVien;
                            HD_lbTenNV.Text = tenNhanVien;
                        }
                    }
                }
            }
        }
        private void DN_txtManv_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void HD_lbTenNV_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MaChucVu FROM NhanVien WHERE TenNhanVien = @TenNhanVien";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenNhanVien", HD_lbTenNV.Text);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string MaChucVuGan = reader["MaChucVu"].ToString();
                    lbcc.Text = MaChucVuGan;
                }

                reader.Close();
            }
        }
        private void MenuHoaDon_Click(object sender, EventArgs e)
        {
            connection.Open();
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuHoaDon.Enabled = false;
            MenuTinhTrangDon.Enabled = true;
            MenuQlyNhanVien.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;
            string MaHoaDonRandom = GetNextMaHD(connection);
            MaHoaDon = MaHoaDonRandom;
            HD_lbMaHD.Text = MaHoaDon;

            HD_lbTenNV.Text = TenNhanVien_lb.Text;
            HD_lbMaNV.Text = DN_txtManv.Text;
            string insertHoaDonQuery = $"INSERT INTO HoaDon (MaHoaDon) VALUES ('{MaHoaDon}')";
            using (SqlCommand sqlCommand = new SqlCommand(insertHoaDonQuery, connection))
            {
                sqlCommand.ExecuteNonQuery();
            }

            string insertHoaDonTamQuery = $"INSERT INTO HoaDonTam (MaHoaDonTam) VALUES ('{MaHoaDon}')";
            using (SqlCommand sqlCommandTam = new SqlCommand(insertHoaDonTamQuery, connection))
            {
                sqlCommandTam.ExecuteNonQuery();
            }

            connection.Close();

            HD_cmbMaGiamGia.Items.Clear();
            HD_cmbLoaiMon.Items.Clear();
            panel23.Visible = false;
            LoadDataToGridHD(MaHoaDon);
            LoadCN_HD();
            LoadSanPham_HD();
            GanDuLieuMaNV();
        }

        public string MaHoaDon { get; set; }
        private string GetNextMaHD(SqlConnection connection)
        {
            string query = "SELECT MAX(MaHoaDon) FROM HoaDon";

            SqlCommand command = new SqlCommand(query, connection);

            object result = command.ExecuteScalar();


            string maxMaPN = result != DBNull.Value ? result.ToString() : string.Empty;

            if (!string.IsNullOrEmpty(maxMaPN))
            {
                int nextNumber = int.Parse(maxMaPN.Substring(2)) + 1;

                string nextMaPN = "HD" + nextNumber.ToString("D3");

                return nextMaPN;
            }
            else
            {
                return "HD001";


            }
        }

        private void LoadDataToGridHD(string MaHoaDon)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string queryCTHD = $"SELECT * FROM ChiTietHoaDon WHERE MaHoaDon = '{MaHoaDon}'";
                    SqlDataAdapter adapterCTHD = new SqlDataAdapter(queryCTHD, connection);
                    DataTable dataTableCTHD = new DataTable();
                    adapterCTHD.Fill(dataTableCTHD);

                    List<string> maSanPhamList = dataTableCTHD.AsEnumerable()
                        .Select(row => row.Field<string>("MaSanPham"))
                        .ToList();

                    string maSanPhamString = string.Join("','", maSanPhamList);
                    string querySanPham = $"SELECT MaSanPham, TenSanPham FROM SanPham WHERE MaSanPham IN ('{maSanPhamString}')";
                    SqlDataAdapter adapterSanPham = new SqlDataAdapter(querySanPham, connection);
                    DataTable dataTableSanPham = new DataTable();
                    adapterSanPham.Fill(dataTableSanPham);

                    dataTableCTHD.Columns.Add("TenSanPham", typeof(string));
                    foreach (DataRow rowCTHD in dataTableCTHD.Rows)
                    {
                        string maSanPham = rowCTHD.Field<string>("MaSanPham");
                        DataRow[] rowsSanPham = dataTableSanPham.Select($"MaSanPham = '{maSanPham}'");
                        if (rowsSanPham.Length > 0)
                        {
                            string tenSanPham = rowsSanPham[0].Field<string>("TenSanPham");
                            rowCTHD.SetField("TenSanPham", tenSanPham);
                        }
                    }

                    dataGridViewCTHD.DataSource = dataTableCTHD;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void ChangedColor()
        {
            // check bàn đó trạng thái là gì 

            for (int i = 1; i <= 81; i++)
            {
                string MaThe = "The" + i;
                connection.Open();
                string Query = $"SELECT TrangThai FROM TrangThaiSanPham WHERE MaTrangThaiThe = '{MaThe}'"; // Lấy danh sách MaThe có TrangThai = true
                SqlCommand cmd = new SqlCommand(Query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string trangThai = reader["TrangThai"].ToString();
                    if (trangThai == "True")
                    {
                        Button button = this.Controls.Find("The" + i, true).FirstOrDefault() as Button;
                        if (button != null)
                        {
                            button.BackColor = Color.Gray
                                ; // Đổi màu nền của nút thành màu xanh
                        }
                    }
                    else
                    {
                        Button button = this.Controls.Find("The" + i, true).FirstOrDefault() as Button;
                        if (button != null)
                        {
                            button.BackColor = Color.Transparent
                                ; // Đổi màu nền của nút thành màu xanh
                        }
                    }
                }
                connection.Close(); // Đóng kết nối sau khi sử dụng
            }
        }
        private void LoadCN_HD()
        {
            HD_lbMaHD.Text = MaHoaDon;
            HD_lbThoiGian.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


            ChangedColor();


            //Truyền dữ liệu vào Cmb
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Chuỗi truy vấn SQL để lấy danh sách mã nhà cung cấp
                string query = "SELECT MaLoaiSanPham FROM LoaiSanPham";

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
                            HD_cmbLoaiMon.Items.Add(maNCC);
                        }
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Chuỗi truy vấn SQL để lấy danh sách mã nhà cung cấp
                string query = "SELECT GiaTriKhuyenMai FROM KhuyenMai";

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
                            double maNCC = reader.GetDouble(0);

                            // Thêm giá trị vào ComboBox
                            HD_cmbMaGiamGia.Items.Add(maNCC);
                        }
                        HD_cmbMaGiamGia.Items.Add("Tất cả");
                    }
                }
            }
        }

        private void ChonBan(string TenButton)
        {
            connection.Open();
            string MaThe = "The" + TenButton;

            string Querycheck = $"  Select COUNT(*) from TrangThaiSanPham where MaTrangThaiThe = '{MaThe}' and TrangThai = 1";
            SqlCommand command = new SqlCommand(Querycheck, connection);
            int Count = (int)command.ExecuteScalar();
            if (Count > 0)
            {
                MessageBox.Show("Thẻ đang được sủ dụng vui lòng chọn thẻ Khác");
                connection.Close();
                return;

            }

            HD_lbSoThe.Text = TenButton;
            panel23.Visible = true;



            string Query = "UPDATE TrangThaiSanPham SET TrangThai = @TrangThai, ThoiGianNhap = @ThoiGianNhap WHERE MaTrangThaiThe = @MaThe";
            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@TrangThai", true);
            cmd.Parameters.AddWithValue("@ThoiGianNhap", DateTime.Now);
            cmd.Parameters.AddWithValue("@MaThe", MaThe);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void The1_Click(object sender, EventArgs e)
        {
            string TheBan = The1.Text;
            ChonBan(TheBan);
        }


        private void The74_Click(object sender, EventArgs e)
        {
            string TheBan = The74.Text;
            ChonBan(TheBan);
        }

        private void The73_Click(object sender, EventArgs e)
        {
            string TheBan = The73.Text;
            ChonBan(TheBan);
        }

        private void The75_Click(object sender, EventArgs e)
        {
            string TheBan = The75.Text;
            ChonBan(TheBan);
        }

        private void The81_Click(object sender, EventArgs e)
        {
            string TheBan = The81.Text;
            ChonBan(TheBan);
        }

        private void The72_Click(object sender, EventArgs e)
        {
            string TheBan = The72.Text;
            ChonBan(TheBan);
        }

        private void The80_Click(object sender, EventArgs e)
        {
            string TheBan = The80.Text;
            ChonBan(TheBan);
        }

        private void The76_Click(object sender, EventArgs e)
        {
            string TheBan = The76.Text;
            ChonBan(TheBan);
        }

        private void The79_Click(object sender, EventArgs e)
        {
            string TheBan = The79.Text;
            ChonBan(TheBan);
        }

        private void The70_Click(object sender, EventArgs e)
        {
            string TheBan = The70.Text;
            ChonBan(TheBan);
        }

        private void The78_Click(object sender, EventArgs e)
        {
            string TheBan = The78.Text;
            ChonBan(TheBan);
        }

        private void The77_Click(object sender, EventArgs e)
        {
            string TheBan = The77.Text;
            ChonBan(TheBan);

        }

        private void The71_Click(object sender, EventArgs e)
        {
            string TheBan = The71.Text;
            ChonBan(TheBan);
        }

        private void The69_Click(object sender, EventArgs e)
        {
            string TheBan = The69.Text;
            ChonBan(TheBan);

        }

        private void The68_Click(object sender, EventArgs e)
        {
            string TheBan = The68.Text;
            ChonBan(TheBan);

        }

        private void The67_Click(object sender, EventArgs e)
        {
            string TheBan = The67.Text;
            ChonBan(TheBan);

        }

        private void The66_Click(object sender, EventArgs e)
        {
            string TheBan = The66.Text;
            ChonBan(TheBan);

        }

        private void The65_Click(object sender, EventArgs e)
        {
            string TheBan = The65.Text;
            ChonBan(TheBan);
        }

        private void The64_Click(object sender, EventArgs e)
        {
            string TheBan = The64.Text;
            ChonBan(TheBan);
        }

        private void The63_Click(object sender, EventArgs e)
        {
            string TheBan = The63.Text;
            ChonBan(TheBan);
        }

        private void The62_Click(object sender, EventArgs e)
        {
            string TheBan = The62.Text;
            ChonBan(TheBan);
        }

        private void The61_Click(object sender, EventArgs e)
        {
            string TheBan = The61.Text;
            ChonBan(TheBan);
        }

        private void The60_Click(object sender, EventArgs e)
        {
            string TheBan = The60.Text;
            ChonBan(TheBan);
        }

        private void The59_Click(object sender, EventArgs e)
        {
            string TheBan = The59.Text;
            ChonBan(TheBan);
        }

        private void The58_Click(object sender, EventArgs e)
        {
            string TheBan = The58.Text;
            ChonBan(TheBan);
        }

        private void The57_Click(object sender, EventArgs e)
        {
            string TheBan = The57.Text;
            ChonBan(TheBan);
        }

        private void The56_Click(object sender, EventArgs e)
        {
            string TheBan = The56.Text;
            ChonBan(TheBan);
        }

        private void The55_Click(object sender, EventArgs e)
        {
            string TheBan = The55.Text;
            ChonBan(TheBan);
        }

        private void The54_Click(object sender, EventArgs e)
        {
            string TheBan = The54.Text;
            ChonBan(TheBan);
        }

        private void The53_Click(object sender, EventArgs e)
        {
            string TheBan = The53.Text;
            ChonBan(TheBan);
        }

        private void The52_Click(object sender, EventArgs e)
        {
            string TheBan = The52.Text;
            ChonBan(TheBan);
        }

        private void The51_Click(object sender, EventArgs e)
        {
            string TheBan = The51.Text;
            ChonBan(TheBan);
        }

        private void The50_Click(object sender, EventArgs e)
        {
            string TheBan = The50.Text;
            ChonBan(TheBan);
        }

        private void The49_Click(object sender, EventArgs e)
        {
            string TheBan = The49.Text;
            ChonBan(TheBan);
        }

        private void The48_Click(object sender, EventArgs e)
        {
            string TheBan = The48.Text;
            ChonBan(TheBan);
        }

        private void The47_Click(object sender, EventArgs e)
        {
            string TheBan = The47.Text;
            ChonBan(TheBan);
        }

        private void The46_Click(object sender, EventArgs e)
        {
            string TheBan = The46.Text;
            ChonBan(TheBan);
        }

        private void The37_Click(object sender, EventArgs e)
        {
            string TheBan = The37.Text;
            ChonBan(TheBan);

        }

        private void The38_Click(object sender, EventArgs e)
        {
            string TheBan = The38.Text;
            ChonBan(TheBan);
        }

        private void The39_Click(object sender, EventArgs e)
        {
            string TheBan = The39.Text;
            ChonBan(TheBan);
        }

        private void The40_Click(object sender, EventArgs e)
        {
            string TheBan = The40.Text;
            ChonBan(TheBan);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            string TheBan = button41.Text;
            ChonBan(TheBan);
        }

        private void The42_Click(object sender, EventArgs e)
        {
            string TheBan = The42.Text;
            ChonBan(TheBan);
        }

        private void The43_Click(object sender, EventArgs e)
        {
            string TheBan = The43.Text;
            ChonBan(TheBan);
        }

        private void The44_Click(object sender, EventArgs e)
        {
            string TheBan = The44.Text;
            ChonBan(TheBan);
        }

        private void The45_Click(object sender, EventArgs e)
        {
            string TheBan = The45.Text;
            ChonBan(TheBan);
        }

        private void The35_Click(object sender, EventArgs e)
        {
            string TheBan = The35.Text;
            ChonBan(TheBan);
        }

        private void The36_Click(object sender, EventArgs e)
        {
            string TheBan = The36.Text;
            ChonBan(TheBan);
        }

        private void The28_Click(object sender, EventArgs e)
        {
            string TheBan = The28.Text;
            ChonBan(TheBan);
        }

        private void The29_Click(object sender, EventArgs e)
        {
            string TheBan = The29.Text;
            ChonBan(TheBan);
        }

        private void The30_Click(object sender, EventArgs e)
        {
            string TheBan = The30.Text;
            ChonBan(TheBan);
        }

        private void The31_Click(object sender, EventArgs e)
        {
            string TheBan = The31.Text;
            ChonBan(TheBan);
        }

        private void The32_Click(object sender, EventArgs e)
        {
            string TheBan = The32.Text;
            ChonBan(TheBan);
        }

        private void The33_Click(object sender, EventArgs e)
        {
            string TheBan = The33.Text;
            ChonBan(TheBan);
        }

        private void The34_Click(object sender, EventArgs e)
        {
            string TheBan = The34.Text;
            ChonBan(TheBan);
        }

        private void The21_Click(object sender, EventArgs e)
        {
            string TheBan = The21.Text;
            ChonBan(TheBan);
        }

        private void The22_Click(object sender, EventArgs e)
        {
            string TheBan = The22.Text;
            ChonBan(TheBan);
        }

        private void The23_Click(object sender, EventArgs e)
        {
            string TheBan = The23.Text;
            ChonBan(TheBan);
        }

        private void The24_Click(object sender, EventArgs e)
        {
            string TheBan = The24.Text;
            ChonBan(TheBan);
        }

        private void The25_Click(object sender, EventArgs e)
        {
            string TheBan = The25.Text;
            ChonBan(TheBan);
        }

        private void The26_Click(object sender, EventArgs e)
        {
            string TheBan = The26.Text;
            ChonBan(TheBan);
        }

        private void The27_Click(object sender, EventArgs e)
        {
            string TheBan = The27.Text;
            ChonBan(TheBan);
        }

        private void The20_Click(object sender, EventArgs e)
        {
            string TheBan = The20.Text;
            ChonBan(TheBan);
        }

        private void The19_Click(object sender, EventArgs e)
        {
            string TheBan = The19.Text;
            ChonBan(TheBan);
        }

        private void The18_Click(object sender, EventArgs e)
        {
            string TheBan = The18.Text;
            ChonBan(TheBan);
        }

        private void The17_Click(object sender, EventArgs e)
        {
            string TheBan = The17.Text;
            ChonBan(TheBan);

        }

        private void The16_Click(object sender, EventArgs e)
        {
            string TheBan = The16.Text;
            ChonBan(TheBan);

        }

        private void The15_Click(object sender, EventArgs e)
        {
            string TheBan = The15.Text;
            ChonBan(TheBan);

        }

        private void The14_Click(object sender, EventArgs e)
        {
            string TheBan = The14.Text;
            ChonBan(TheBan);
        }

        private void The13_Click(object sender, EventArgs e)
        {
            string TheBan = The13.Text;
            ChonBan(TheBan);
        }

        private void The12_Click(object sender, EventArgs e)
        {
            string TheBan = The12.Text;
            ChonBan(TheBan);

        }

        private void The11_Click(object sender, EventArgs e)
        {
            string TheBan = The11.Text;
            ChonBan(TheBan);
        }

        private void The10_Click(object sender, EventArgs e)
        {
            string TheBan = The10.Text;
            ChonBan(TheBan);

        }

        private void The9_Click(object sender, EventArgs e)
        {
            string TheBan = The9.Text;
            ChonBan(TheBan);
        }

        private void The8_Click(object sender, EventArgs e)
        {
            string TheBan = The8.Text;
            ChonBan(TheBan);
        }

        private void The7_Click(object sender, EventArgs e)
        {
            string TheBan = The7.Text;
            ChonBan(TheBan);
        }

        private void The6_Click(object sender, EventArgs e)
        {
            string TheBan = The6.Text;
            ChonBan(TheBan);
        }

        private void The5_Click(object sender, EventArgs e)
        {
            string TheBan = The5.Text;
            ChonBan(TheBan);
        }

        private void The4_Click(object sender, EventArgs e)
        {
            string TheBan = The4.Text;
            ChonBan(TheBan);
        }

        private void The3_Click(object sender, EventArgs e)
        {
            string TheBan = The3.Text;
            ChonBan(TheBan);

        }

        private void The2_Click(object sender, EventArgs e)
        {
            string TheBan = The2.Text;
            ChonBan(TheBan);
        }


        private void LoadSanPham_HD()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM SanPham";
                string queryTenSP = $"SELECT TenSanPham FROM SanPham";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    /*// ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "PN_BtnNLieu" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }*/

                    int count = (int)command.ExecuteScalar();
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "HD_btnSP" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenSanPham"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void EnableSP()
        {
            HD_panelSP1.Visible = false;
            HD_panelSP2.Visible = false;
            HD_panelSP3.Visible = false;
            HD_panelSP4.Visible = false;
            HD_panelSP5.Visible = false;
            HD_panelSP6.Visible = false;
            HD_panelSP7.Visible = false;
            HD_panelSP8.Visible = false;
            HD_panelSP9.Visible = false;
            HD_panelSP10.Visible = false;
            HD_panelSP11.Visible = false;
            HD_panelSP12.Visible = false;
            HD_panelSP13.Visible = false;
            HD_panelSP14.Visible = false;
            HD_panelSP15.Visible = false;
            HD_panelSP16.Visible = false;
            HD_panelSP17.Visible = false;
            HD_panelSP18.Visible = false;
            HD_panelSP19.Visible = false;
            HD_panelSP20.Visible = false;
            HD_panelSP21.Visible = false;
            HD_panelSP22.Visible = false;
            HD_panelSP23.Visible = false;
            HD_panelSP24.Visible = false;
            HD_panelSP25.Visible = false;
            HD_panelSP26.Visible = false;
            HD_panelSP27.Visible = false;
            HD_panelSP28.Visible = false;
            HD_panelSP29.Visible = false;
            HD_panelSP30.Visible = false;
            HD_panelSP31.Visible = false;
            HD_panelSP32.Visible = false;
            HD_panelSP33.Visible = false;
            HD_panelSP34.Visible = false;
            HD_panelSP35.Visible = false;
            HD_panelSP36.Visible = false;
            HD_panelSP37.Visible = false;
            HD_panelSP38.Visible = false;
            HD_panelSP39.Visible = false;
            HD_panelSP40.Visible = false;


        }

        private void HD_btnFind_Click(object sender, EventArgs e)
        {
            string TenSP = HD_FindTenMon.Text;
            EnableSP();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM SanPham Where TenSanPham Like N'%{TenSP}%'";
                string queryTenSP = $"SELECT TenSanPham FROM SanPham whERE TenSanPham Like N'%{TenSP}%' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string panelName = "HD_panelSP" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(panelName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Panel)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "HD_btnSP" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenSanPham"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HD_cmbLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TenLSP = HD_cmbLoaiMon.Text;
            EnableSP();

            if(TenLSP == "Tất cả")
            {
                LoadSanPham_HD();
                return;
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM SanPham Where MaLoaiSanPham = '{TenLSP}'";
                string queryTenSP = $"SELECT TenSanPham FROM SanPham whERE MaLoaiSanPham = '{TenLSP}' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string panelName = "HD_panelSP" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(panelName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Panel)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "HD_btnSP" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["TenSanPham"].ToString();
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = productName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HienthiThongTinSanPham(string TenSanPham)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM SanPham WHERE TenSanPham = N'{TenSanPham}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Đọc dữ liệu từ cột và hiển thị lên các text box
                            HD_txtMaSP.Text = reader["MaSanPham"].ToString();
                            HD_txtTenSP.Text = reader["TenSanPham"].ToString();
                            HD_txtDonGiaSP.Text = reader["DonGia"].ToString();

                            // Thêm các trường còn lại tương ứng
                        }
                        else
                        {
                            MessageBox.Show("Sản phẩm không được tìm thấy");
                        }
                    }
                }
            }
        }


        private void HD_btnSP1_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP1.Text);
        }

        private void HD_btnSP2_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP2.Text);
        }

        private void HD_btnSP3_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP3.Text);
        }

        private void HD_btnSP4_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP4.Text);
        }

        private void HD_btnSP5_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP5.Text);
        }

        private void HD_btnSP6_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP6.Text);
        }

        private void HD_btnSP7_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP7.Text);
        }

        private void HD_btnSP8_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP8.Text);
        }

        private void HD_btnSP9_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP9.Text);
        }

        private void HD_btnSP10_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP10.Text);
        }

        private void HD_btnSP11_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP11.Text);
        }

        private void HD_btnSP12_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP12.Text);
        }

        private void HD_btnSP13_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP13.Text);
        }

        private void HD_btnSP14_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP14.Text);

        }

        private void HD_btnSP15_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP15.Text);
        }

        private void HD_btnSP16_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP16.Text);
        }

        private void HD_btnSP17_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP17.Text);
        }

        private void HD_btnSP18_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP18.Text);

        }

        private void HD_btnSP19_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP19.Text);
        }

        private void HD_btnSP20_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP20.Text);
        }

        private void HD_btnSP21_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP21.Text);
        }

        private void HD_btnSP22_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP22.Text);
        }

        private void HD_btnSP23_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP23.Text);
        }

        private void HD_btnSP24_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP24.Text);
        }

        private void HD_btnSP25_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP25.Text);
        }

        private void HD_btnSP26_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP26.Text);

        }

        private void HD_btnSP27_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP27.Text);
        }

        private void HD_btnSP28_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP28.Text);
        }

        private void HD_btnSP29_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP29.Text);
        }

        private void HD_btnSP30_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP30.Text);
        }

        private void HD_btnSP31_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP31.Text);
        }

        private void HD_btnSP32_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP32.Text);
        }

        private void HD_btnSP33_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP33.Text);

        }

        private void HD_btnSP34_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP34.Text);
        }

        private void HD_btnSP35_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP35.Text);
        }

        private void HD_btnSP36_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP36.Text);
        }

        private void HD_btnSP40_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP40.Text);
        }

        private void HD_btnSP39_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP39.Text);
        }

        private void HD_btnSP38_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP38.Text);
        }

        private void HD_btnSP37_Click(object sender, EventArgs e)
        {
            panel25.Visible = true;
            HienthiThongTinSanPham(HD_btnSP37.Text);
        }

        private void HD_UpDownSLSP_ValueChanged(object sender, EventArgs e)
        {
            int SL = (int)HD_UpDownSLSP.Value;

            if(HD_txtDonGiaSP.Text == "")
            {
                MessageBox.Show("Vui lòng nhập giá","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            if (SL > 0)
            {
                HD_btnXacNhan.Enabled = true;

            }
            else
            {
                HD_btnXacNhan.Enabled = false;

            }

            HD_txtThanhTien.Text = (float.Parse(HD_txtDonGiaSP.Text) * SL).ToString();
        }

        private void HD_btnHuy_Click(object sender, EventArgs e)
        {
            panel25.Visible = false;

        }

        private void HD_btnXacNhan_Click(object sender, EventArgs e)
        {
            string MaHD = MaHoaDon;
            string MaSP = HD_txtMaSP.Text;
            int SoLuong = (int)HD_UpDownSLSP.Value;
            float DonGia = float.Parse(HD_txtDonGiaSP.Text);
            float ThanhTien = float.Parse(HD_txtThanhTien.Text);
            string GhiChu = HD_txtGhiChu.Text;
            string MaThe = "The" + HD_lbSoThe.Text;

            connection.Open();

            //Kiếm tra xem có tồn tại hay chưa
            string queryKiemTra = $"SELECT COUNT(*) FROM (" +
                                  $"SELECT MaHoaDon FROM ChiTietHoaDon WHERE MaHoaDon = '{MaHD}' AND MaSanPham = '{MaSP}' " +
                                  $"UNION ALL " +
                                  $"SELECT MaHoaDonTam FROM ChiTietHoaDonTam WHERE MaHoaDonTam = '{MaHD}' AND MaSanPham = '{MaSP}'" +
                                  $") AS T";
            SqlCommand commandKiemTra = new SqlCommand(queryKiemTra, connection);
            int count = (int)commandKiemTra.ExecuteScalar();
            if (count > 0)
            {
                // Lưu vào chi tiết hóa đơn
                string Query = $"UPDATE ChiTietHoaDon SET SoLuong = '{SoLuong}', DonGia = '{DonGia}', ThanhTien = '{ThanhTien}' WHERE MaHoaDon = '{MaHD}' AND MaSanPham = '{MaSP}'";
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                sqlCommand.ExecuteNonQuery();

                // Lưu vào bảng ChiTietHoaDonTam
                string QueryTam = $"UPDATE ChiTietHoaDonTam SET SoLuong = '{SoLuong}', DonGia = '{DonGia}', ThanhTien = '{ThanhTien}' WHERE MaHoaDonTam = '{MaHD}' AND MaSanPham = '{MaSP}'";
                SqlCommand sqlCommand2 = new SqlCommand(QueryTam, connection);
                sqlCommand2.ExecuteNonQuery();

                // Lưu vào Chi tiết trạng thái sản phẩm 
                string Query2 = $"UPDATE ChiTietTrangThaiSanPham SET SoLuong = '{SoLuong}', GhiChu = '{GhiChu}' WHERE MaTrangThaiThe = '{MaThe}' AND MaSanPham = '{MaSP}'";
                SqlCommand sqlCommand3 = new SqlCommand(Query2, connection);
                sqlCommand3.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Cập nhật thành công");

                LoadDataToGridHD(MaHoaDon);
            }
            else
            {
                string Query = $"Insert into ChiTietHoaDon (MaHoaDon,MaSanPham,SoLuong,DonGia,ThanhTien) Values ('{MaHD}','{MaSP}','{SoLuong}','{DonGia}','{ThanhTien}')";
                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                sqlCommand.ExecuteNonQuery();

                string QueryTam = $"Insert into ChiTietHoaDonTam (MaHoaDonTam,MaSanPham,SoLuong,DonGia,ThanhTien) Values ('{MaHD}','{MaSP}','{SoLuong}','{DonGia}','{ThanhTien}')";
                SqlCommand sqlCommandTam = new SqlCommand(QueryTam, connection);
                sqlCommandTam.ExecuteNonQuery();

                string Query2 = $"Insert into ChiTietTrangThaiSanPham (MaTrangThaiThe,MaSanPham,SoLuong,GhiChu) Values ('{MaThe}','{MaSP}','{SoLuong}','{GhiChu}')";
                SqlCommand sqlCommand2 = new SqlCommand(Query2, connection);
                sqlCommand2.ExecuteNonQuery();


                connection.Close();

                LoadDataToGridHD(MaHoaDon);
            }

            //Hiển thị tổng tiền 
            // Khai báo biến tổng
            float tongTien = 0;

            // Lặp qua từng dòng trong DataGridView
            foreach (DataGridViewRow row in dataGridViewCTHD.Rows)
            {
                // Kiểm tra nếu dòng không phải dòng mới
                if (!row.IsNewRow)
                {
                    // Lấy giá trị của cột thành tiền (giả sử cột đó có tên "colThanhTien")
                    float thanhTien = Convert.ToSingle(row.Cells["ThanhTien"].Value);

                    // Cộng giá trị thành tiền vào tổng
                    tongTien += thanhTien;
                }
            }
            HD_lbTongTien.Text = tongTien.ToString();
            HD_txtTienKhach.Text = tongTien.ToString();

            panel25.Visible = false;
            HD_btnThanhToan.Enabled = true;
        }

        private void HD_txtTienKhach_TextChanged(object sender, EventArgs e)
        {
            float tongTien = float.Parse(HD_lbTongTien.Text);
            float TienKhach = float.Parse(HD_txtTienKhach.Text);
            HD_lbTienThua.Text = (TienKhach - tongTien).ToString();
        }
        private float GetGiaTriKhuyenMai(int maKhuyenMai)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT GiaTriKhuyenMai FROM KhuyenMai WHERE MaKhuyenMai = @MaKhuyenMai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToSingle(result);
                    }
                    return 0;
                }
            }
        }
        private void HD_cmbMaGiamGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            float tongTien = 0;
            foreach (DataGridViewRow row in dataGridViewCTHD.Rows)
            {
                if (!row.IsNewRow)
                {
                    float thanhTien = Convert.ToSingle(row.Cells["ThanhTien"].Value);
                    tongTien += thanhTien;
                }
            }
            HD_lbTongTien.Text = tongTien.ToString();
            string maKhuyenMai = (HD_cmbMaGiamGia.Text);
            string giaTriKhuyenMai = maKhuyenMai;
            HD_lbGTGG.Text = giaTriKhuyenMai.ToString();

            if (int.Parse(HD_cmbMaGiamGia.Text) != 0)
            {
                float GiaTriKhuyenMai = float.Parse(HD_cmbMaGiamGia.Text);
                HD_lbTongTien.Text = (float.Parse(HD_lbTongTien.Text) * GiaTriKhuyenMai / 100).ToString();
            }
        }

        private void HD_btnThanhToan_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string MaHD = MaHoaDon;
                string MaNV = HD_lbMaNV.Text;
                float GiatriKM = float.Parse(HD_cmbMaGiamGia.Text);
                string MaKM = "";

                string Query = $"Select MaKhuyenMai from KhuyenMai Where GiaTriKhuyenMai = {GiatriKM}";
                SqlCommand cmd = new SqlCommand(Query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (HD_cmbMaGiamGia.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn mã giảm giá");
                }
                else if (reader.Read())
                {
                    MaKM = reader.GetString(0);
                }

                reader.Close();
                string SoThe = HD_lbSoThe.Text;
                DateTime NgayBan = DateTime.Now;
                string PhuongThucTT = "";
                if (radioButton1.Checked == true)
                {
                    PhuongThucTT = "Tiền Mặt";
                }
                else if (radioButton2.Checked == true)
                {
                    PhuongThucTT = "Chuyển Khoản";
                }
                float TongTien = float.Parse(HD_lbTongTien.Text);
                float TienKhach = float.Parse(HD_txtTienKhach.Text);
                float TienThua = float.Parse(HD_lbTienThua.Text);

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string hoaDonQuery = $"UPDATE HoaDon SET MaKhuyenMai = @MaKhuyenMai, MaNhanVien = @MaNhanVien, SoThe = @SoThe, NgayBanHang = @NgayBanHang, PhuongThucThanhToan = N'{PhuongThucTT}', TongTien = @TongTien, TienKhach = @TienKhach, TienThua = @TienThua WHERE MaHoaDon = @MaHoaDon";

                    string hoaDonTamQuery = $"UPDATE HoaDonTam SET MaKhuyenMai = @MaKhuyenMai, MaNhanVien = @MaNhanVien, SoThe = @SoThe, NgayBanHang = @NgayBanHang, PhuongThucThanhToan = N'{PhuongThucTT}', TongTien = @TongTien, TienKhach = @TienKhach, TienThua = @TienThua WHERE MaHoaDonTam = @MaHoaDonTam";

                    using (SqlCommand hoaDonCommand = new SqlCommand(hoaDonQuery, connection, transaction))
                    {
                        hoaDonCommand.Parameters.AddWithValue("@MaKhuyenMai", MaKM);
                        hoaDonCommand.Parameters.AddWithValue("@MaNhanVien", MaNV);
                        hoaDonCommand.Parameters.AddWithValue("@SoThe", SoThe);
                        hoaDonCommand.Parameters.AddWithValue("@NgayBanHang", NgayBan);
                        hoaDonCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        hoaDonCommand.Parameters.AddWithValue("@TienKhach", TienKhach);
                        hoaDonCommand.Parameters.AddWithValue("@TienThua", TienThua);
                        hoaDonCommand.Parameters.AddWithValue("@MaHoaDon", MaHD);

                        hoaDonCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand hoaDonTamCommand = new SqlCommand(hoaDonTamQuery, connection, transaction))
                    {
                        hoaDonTamCommand.Parameters.AddWithValue("@MaKhuyenMai", MaKM);
                        hoaDonTamCommand.Parameters.AddWithValue("@MaNhanVien", MaNV);
                        hoaDonTamCommand.Parameters.AddWithValue("@SoThe", SoThe);
                        hoaDonTamCommand.Parameters.AddWithValue("@NgayBanHang", NgayBan);
                        hoaDonTamCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        hoaDonTamCommand.Parameters.AddWithValue("@TienKhach", TienKhach);
                        hoaDonTamCommand.Parameters.AddWithValue("@TienThua", TienThua);
                        hoaDonTamCommand.Parameters.AddWithValue("@MaHoaDonTam", MaHD);
                        hoaDonTamCommand.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();

                    RpHoaDon rpForm = new RpHoaDon();
                    rpForm.ShowDialog();

                    TrangChu_Load(sender, e);
                    ChangedColor();
                    MenuHoaDon.Enabled = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    connection.Close();

                    // Xử lý lỗi
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void button169_Click(object sender, EventArgs e)
        {
            try
            {
                string MaThe = "The" + HD_lbSoThe.Text;
                connection.Open();
                string Query = $"Update TrangThaiSanPham Set TrangThai = '{0}',ThoiGianNhap = NULL where MaTrangThaiThe = '{MaThe}'";
                SqlCommand cmd = new SqlCommand(Query, connection);
                cmd.ExecuteNonQuery();
                if (dataGridViewCTHD.RowCount >= 1)
                {

                    string QuerydeleteCTHD = $"Delete from ChiTietHoaDon Where MaHoaDon = '{HD_lbMaHD.Text}'";
                    SqlCommand cmdDeleteCT = new SqlCommand(QuerydeleteCTHD, connection);
                    cmdDeleteCT.ExecuteNonQuery();
                    string QuerydeleteCTHDT = $"Delete from ChiTietHoaDonTam Where MaHoaDonTam = '{HD_lbMaHD.Text}'";
                    SqlCommand cmdDeleteCTT = new SqlCommand(QuerydeleteCTHDT, connection);
                    cmdDeleteCTT.ExecuteNonQuery();


                    string QuerydeleteCTTTSP = $"Delete from ChiTietTrangThaiSanPham Where MaTrangThaiThe = '{MaThe}'";
                    SqlCommand cmdDeleteCTTTSP = new SqlCommand(QuerydeleteCTTTSP, connection);
                    cmdDeleteCTTTSP.ExecuteNonQuery();


                    string QuerydeleteHD = $"Delete from HoaDon Where MaHoaDon = '{HD_lbMaHD.Text}'";
                    SqlCommand cmdDelete = new SqlCommand(QuerydeleteHD, connection);
                    cmdDelete.ExecuteNonQuery();
                    string QuerydeleteHDT = $"Delete from HoaDonTam Where MaHoaDonTam = '{HD_lbMaHD.Text}'";
                    SqlCommand cmdDeleteT = new SqlCommand(QuerydeleteHDT, connection);
                    cmdDeleteT.ExecuteNonQuery();
                    panel23.Visible = false;

                }
                else
                {
                    string QuerydeleteHD = $"Delete from HoaDon Where MaHoaDon = '{HD_lbMaHD.Text}'";
                    SqlCommand cmdDelete = new SqlCommand(QuerydeleteHD, connection);
                    cmdDelete.ExecuteNonQuery();
                    panel23.Visible = false;

                }
                connection.Close();
                MessageBox.Show("Hủy Đơn Thành Công");
                TrangChu_Load(sender, e);
                ChangedColor();

                MenuHoaDon.Enabled = true;
                LoadCN_HD();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //////////////////////////////////////////////////////////////////////////////Trạng Thái Món\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void MenuTinhTrangDon_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(TinhTrangDon);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuTinhTrangDon.Enabled = false;
            MenuHoaDon.Enabled = true;
            MenuQlyNhanVien.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;

            LoadBan_TTM();
        }
        private void VisibleTheBan()
        {
            for (int i = 1; i <= 81; i++)
            {
                string controlName = "TTM_The" + i;
                Control[] controls = this.Controls.Find(controlName, true);

                if (controls.Length > 0 && controls[0] is Button)
                {
                    Button button = (Button)controls[0];
                    button.Visible = false;
                }
            }
        }
        private void LoadBan_TTM()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thực hiện truy vấn SQL
                string query = $"SELECT COUNT(*) FROM TrangThaiSanPham where TrangThai = 1";
                string queryTenSP = $"SELECT MaTrangThaiThe FROM TrangThaiSanPham where TrangThai = 1 ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar trả về giá trị đầu tiên từ kết quả truy vấn
                    int count = (int)command.ExecuteScalar();

                    // In ra số lượng Nguyên Liệu
                    for (int i = 1; i < count + 1; i++)
                    {
                        string buttonName = "TTM_The" + i;
                        // Tìm control theo tên
                        Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();
                        if (foundControl != null && foundControl is Button)
                        {
                            // Thiết lập thuộc tính Visible của control
                            foundControl.Visible = true;
                        }
                    }
                    using (SqlCommand commandTenSP = new SqlCommand(queryTenSP, connection))
                    {
                        // Sử dụng SqlDataReader để đọc kết quả truy vấn
                        using (SqlDataReader reader = commandTenSP.ExecuteReader())
                        {
                            for (int i = 1; i < count + 1; i++)
                            {
                                string buttonName = "TTM_The" + i;
                                string PicName = "ANHSPN" + i;
                                /*Thiếu hiển thị Ẩnh*/





                                // Tìm control theo tên
                                Control foundControl = this.Controls.Find(buttonName, true).FirstOrDefault();

                                if (foundControl != null && foundControl is Button)
                                {
                                    reader.Read();
                                    string productName = reader["MaTrangThaiThe"].ToString();
                                    string numberPart = productName.Substring(3); // Bỏ 3 ký tự đầu tiên ("The")
                                    // Thiết lập thuộc tính Visible của control
                                    foundControl.Visible = true;
                                    foundControl.Text = numberPart;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void LoadTT(string SoThe)
        {
            TTM_SoThe.Text = SoThe;

            string MaTTT = "The" + SoThe;
            LoadDataToGridTTM(MaTTT);
            connection.Open();
            string querySelectTG = $"Select ThoiGianNhap from TrangThaiSanPham where MaTrangThaiThe = '{MaTTT}'";
            SqlCommand commandSelectTG = new SqlCommand(querySelectTG, connection);
            SqlDataReader reader = commandSelectTG.ExecuteReader();
            if (reader.Read())
            {
                TTM_ThoiGian.Text = reader["ThoiGianNhap"].ToString();
            }


            int totalQuantity = 0;

            foreach (DataGridViewRow row in dataGridViewTTM.Rows)
            {
                if (row.Cells["SoLuong"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    totalQuantity += quantity;
                }
            }
            TTM_TongSoMon.Text = (totalQuantity).ToString();
            connection.Close();

        }

        private void LoadDataToGridTTM(string MaTrangThaiThe)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM ChiTietTrangThaiSanPham Where MaTrangThaiThe = '{MaTrangThaiThe}'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewTTM.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
                connection.Close();
            }

        }
        private void TTM_The1_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The1.Text;
            LoadTT(SoThe);

        }

        private void TTM_The74_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The74.Text;
            LoadTT(SoThe);
        }

        private void TTM_The73_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The73.Text;
            LoadTT(SoThe);
        }

        private void TTM_The75_Click(object sender, EventArgs e)
        {
            string SoThe = The75.Text;
            LoadTT(SoThe);
        }

        private void TTM_The81_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The81.Text;
            LoadTT(SoThe);
        }

        private void TTM_The72_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The72.Text;
            LoadTT(SoThe);
        }

        private void TTM_The80_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The80.Text;
            LoadTT(SoThe);

        }

        private void TTM_The76_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The76.Text;
            LoadTT(SoThe);
        }

        private void TTM_The79_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The79.Text;
            LoadTT(SoThe);
        }

        private void TTM_The70_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The70.Text;
            LoadTT(SoThe);
        }

        private void TTM_The78_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The78.Text;
            LoadTT(SoThe);
        }

        private void TTM_The77_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The77.Text;
            LoadTT(SoThe);
        }

        private void TTM_The71_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The71.Text;
            LoadTT(SoThe);
        }

        private void TTM_The69_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The69.Text;
            LoadTT(SoThe);
        }

        private void TTM_The68_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The68.Text;
            LoadTT(SoThe);
        }

        private void TTM_The67_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The67.Text;
            LoadTT(SoThe);
        }

        private void TTM_The66_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The66.Text;
            LoadTT(SoThe);
        }

        private void TTM_The65_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The65.Text;
            LoadTT(SoThe);
        }

        private void TTM_The64_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The64.Text;
            LoadTT(SoThe);

        }

        private void TTM_The63_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The63.Text;
            LoadTT(SoThe);
        }

        private void TTM_The62_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The62.Text;
            LoadTT(SoThe);

        }

        private void TTM_The61_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The61.Text;
            LoadTT(SoThe);
        }

        private void TTM_The60_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The60.Text;
            LoadTT(SoThe);
        }

        private void TTM_The59_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The59.Text;
            LoadTT(SoThe);
        }

        private void TTM_The58_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The58.Text;
            LoadTT(SoThe);
        }

        private void TTM_The57_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The57.Text;
            LoadTT(SoThe);

        }

        private void TTM_The56_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The56.Text;
            LoadTT(SoThe);

        }

        private void TTM_The55_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The55.Text;
            LoadTT(SoThe);

        }

        private void TTM_The54_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The54.Text;
            LoadTT(SoThe);

        }

        private void TTM_The53_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The53.Text;
            LoadTT(SoThe);

        }

        private void TTM_The52_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The52.Text;
            LoadTT(SoThe);
        }

        private void TTM_The51_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The51.Text;
            LoadTT(SoThe);
        }

        private void TTM_The50_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The50.Text;
            LoadTT(SoThe);
        }

        private void TTM_The49_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The49.Text;
            LoadTT(SoThe);
        }

        private void TTM_The48_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The48.Text;
            LoadTT(SoThe);

        }

        private void TTM_The47_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The47.Text;
            LoadTT(SoThe);

        }

        private void TTM_The46_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The46.Text;
            LoadTT(SoThe);
        }

        private void TTM_The37_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The37.Text;
            LoadTT(SoThe);
        }

        private void TTM_The38_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The38.Text;
            LoadTT(SoThe);
        }

        private void TTM_The39_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The39.Text;
            LoadTT(SoThe);

        }

        private void TTM_The40_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The40.Text;
            LoadTT(SoThe);
        }

        private void TTM_The41_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The41.Text;
            LoadTT(SoThe);
        }

        private void TTM_The42_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The42.Text;
            LoadTT(SoThe);

        }

        private void TTM_The43_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The43.Text;
            LoadTT(SoThe);

        }

        private void TTM_The44_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The44.Text;
            LoadTT(SoThe);
        }

        private void TTM_The45_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The45.Text;
            LoadTT(SoThe);
        }

        private void TTM_The35_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The35.Text;
            LoadTT(SoThe);
        }

        private void TTM_The36_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The36.Text;
            LoadTT(SoThe);
        }

        private void TTM_The28_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The28.Text;
            LoadTT(SoThe);

        }

        private void TTM_The29_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The29.Text;
            LoadTT(SoThe);
        }

        private void TTM_The30_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The30.Text;
            LoadTT(SoThe);

        }

        private void TTM_The31_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The31.Text;
            LoadTT(SoThe);
        }

        private void TTM_The32_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The32.Text;
            LoadTT(SoThe);
        }

        private void TTM_The33_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The33.Text;
            LoadTT(SoThe);

        }

        private void TTM_The34_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The34.Text;
            LoadTT(SoThe);
        }

        private void TTM_The21_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The21.Text;
            LoadTT(SoThe);
        }

        private void TTM_The22_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The22.Text;
            LoadTT(SoThe);

        }

        private void TTM_The23_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The23.Text;
            LoadTT(SoThe);
        }

        private void TTM_The24_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The24.Text;
            LoadTT(SoThe);
        }

        private void TTM_The25_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The25.Text;
            LoadTT(SoThe);

        }

        private void TTM_The26_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The26.Text;
            LoadTT(SoThe);
        }

        private void TTM_The27_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The27.Text;
            LoadTT(SoThe);
        }

        private void TTM_The20_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The20.Text;
            LoadTT(SoThe);
        }

        private void TTM_The19_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The19.Text;
            LoadTT(SoThe);
        }

        private void TTM_The18_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The18.Text;
            LoadTT(SoThe);
        }

        private void TTM_The17_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The17.Text;
            LoadTT(SoThe);
        }

        private void TTM_The16_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The16.Text;
            LoadTT(SoThe);
        }

        private void TTM_The15_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The15.Text;
            LoadTT(SoThe);
        }

        private void TTM_The14_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The14.Text;
            LoadTT(SoThe);

        }

        private void TTM_The13_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The13.Text;
            LoadTT(SoThe);
        }

        private void TTM_The12_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The12.Text;
            LoadTT(SoThe);

        }

        private void TTM_The11_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The11.Text;
            LoadTT(SoThe);

        }

        private void TTM_The10_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The10.Text;
            LoadTT(SoThe);
        }

        private void TTM_The9_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The9.Text;
            LoadTT(SoThe);

        }

        private void TTM_The8_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The8.Text;
            LoadTT(SoThe);
        }

        private void TTM_The7_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The7.Text;
            LoadTT(SoThe);
        }

        private void TTM_The6_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The6.Text;
            LoadTT(SoThe);
        }

        private void TTM_The5_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The5.Text;
            LoadTT(SoThe);

        }

        private void TTM_The4_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The4.Text;
            LoadTT(SoThe);
        }

        private void TTM_The3_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The3.Text;
            LoadTT(SoThe);
        }

        private void TTM_The2_Click(object sender, EventArgs e)
        {
            string SoThe = TTM_The2.Text;
            LoadTT(SoThe);
        }

        private void TTM_btnHoanThanh_Click(object sender, EventArgs e)
        {
            string MaThe = "The" + TTM_SoThe.Text;
            connection.Open();
            string Query = $"Update TrangThaiSanPham Set TrangThai = '{0}',ThoiGianNhap = NULL where MaTrangThaiThe = '{MaThe}'";
            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.ExecuteNonQuery();

            string queryXoa = $"Delete from ChiTietTrangThaiSanPham where MaTrangThaiThe = '{MaThe}' ";
            SqlCommand cmdXoa = new SqlCommand(queryXoa, connection);
            cmdXoa.ExecuteNonQuery();

            MessageBox.Show("Đã Hoàn Thành Đơn " + MaThe);
            VisibleTheBan();
            LoadBan_TTM();
            connection.Close();

        }
        ///////////////////////////////////////////////// QUẢN LÝ KHÁCH HÀNG/////////////////////////////////////
        private void MenuQlyKHang_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(QlyKhachHang);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuQlyKHang.Enabled = false;
            MenuTinhTrangDon.Enabled = true;
            MenuQlyNhanVien.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;

            LoadDataToGrildKH();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string nextMaKH = GetNextMaKH(connection);
                txtMaKH.Text = nextMaKH;
            }
        }
        private string GetNextMaKH(SqlConnection connection)
        {
            string query = "SELECT MAX(MaKhachHang) FROM KhachHang";
            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            string maxMaKH = Convert.ToString(result);

            if (!string.IsNullOrEmpty(maxMaKH) && maxMaKH.Length >= 3 && maxMaKH.StartsWith("KH"))
            {
                string numberPart = maxMaKH.Substring(2);
                if (int.TryParse(numberPart, out int currentNumber))
                {
                    int nextNumber = currentNumber + 1;
                    string nextMaKH = "KH" + nextNumber.ToString("D3");
                    return nextMaKH;
                }
            }

            return "KH001";
        }

        private void LoadDataToGrildKH()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuKH.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void DuLieuKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hàng hợp lệ
            {
                DataGridViewRow row = DuLieuKH.Rows[e.RowIndex];

                // Lấy giá trị từ ô được chọn và gán cho các điều khiển TextBox, DateTimePicker
                txtMaKH.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKH.Text = row.Cells["TenKhachHang"].Value.ToString();
                SdtKH.Text = row.Cells["SDT"].Value.ToString();
                datetimeNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                DiaChiKH.Text = row.Cells["DiaChi"].Value.ToString();
                txtDiemTLuy.Text = row.Cells["DiemTichLuy"].Value.ToString();

            }
            btnThemKH.Enabled = false;
            btnSuaKH.Enabled = true;
            btnXoaKH.Enabled = true;
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string MaKhachHang = GetNextMaKH(connection);
                    string TenKhachHang = txtTenKH.Text;
                    string SDT = SdtKH.Text;
                    DateTime selectNgaySinh = datetimeNgaySinh.Value;
                    string DiaChi = DiaChiKH.Text;
                    string DiemTichLuy = txtDiemTLuy.Text;

                    if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(DiemTichLuy))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                        return;
                    }
                    if (!IsValidTenKhachHang(TenKhachHang))
                    {
                        MessageBox.Show("Tên khách hàng không hợp lệ.");
                        return;
                    }

                    if (!IsValidSDT(SDT))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ.");
                        return;
                    }

                    if (!IsValidDiaChi(DiaChi))
                    {
                        MessageBox.Show("Địa chỉ không hợp lệ.");
                        return;
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO KhachHang (MaKhachHang, SDT, TenKhachHang, NgaySinh, DiaChi, DiemTichLuy) " +
                                         "VALUES (@MaKhachHang, @SDT, @TenKhachHang, @NgaySinh, @DiaChi, @DiemTichLuy)";
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);
                        command.Parameters.AddWithValue("@SDT", SDT);
                        command.Parameters.AddWithValue("@TenKhachHang", TenKhachHang);
                        command.Parameters.AddWithValue("@NgaySinh", selectNgaySinh);
                        command.Parameters.AddWithValue("@DiaChi", DiaChi);
                        command.Parameters.AddWithValue("@DiemTichLuy", DiemTichLuy);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Thêm khách hàng thành công!");
                        txtTenKH.Text = string.Empty;
                        SdtKH.Text = string.Empty;
                        datetimeNgaySinh.Value = DateTime.Now;
                        DiaChiKH.Text = string.Empty;
                        txtDiemTLuy.Text = string.Empty;
                        txtFindMaKH.Text = string.Empty;
                        txtFindTenKH.Text = string.Empty;
                        btnThemKH.Enabled = false;
                        btnSuaKH.Enabled = false;
                        btnXoaKH.Enabled = false;
                        LoadDataToGrildKH();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private bool IsValidTenKhachHang(string tenKhachHang)
        {
            Regex regexTenKhachHang = new Regex(@"^[a-zA-ZÀ-Ỹà-ỹẠ-Ỵạ-ỵĂăĐđĨĩŨũƠơƯưẰằỀềỒồỜờỪừỲỳÁáẮắẤấẾếÍíỐốỚớỨứÝý\s]+$");
            return regexTenKhachHang.IsMatch(tenKhachHang);
        }
        private bool IsValidSDT(string sdt)
        {
            Regex regexSDT = new Regex(@"^(0[1-9]{1}[\d]{8}|0[1-9]{1}[\d]{9})$");
            return regexSDT.IsMatch(sdt);
        }
        private bool IsValidDiaChi(string diaChi)
        {
            Regex regexDiaChi = new Regex(@"^[a-zA-Z0-9\sáàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ.,/-]+$");
            return regexDiaChi.IsMatch(diaChi);
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string MaKhachHang = txtMaKH.Text;
                    string TenKhachHang = txtTenKH.Text;
                    string SDT = SdtKH.Text;
                    DateTime selectNgaySinh = datetimeNgaySinh.Value;
                    string DiaChi = DiaChiKH.Text;
                    string DiemTichLuy = txtDiemTLuy.Text;
                    if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty(DiaChi))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                        return;
                    }
                    if (!IsValidTenKhachHang(TenKhachHang))
                    {
                        MessageBox.Show("Tên khách hàng không hợp lệ.");
                        return;
                    }

                    if (!IsValidSDT(SDT))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ.");
                        return;
                    }

                    if (!IsValidDiaChi(DiaChi))
                    {
                        MessageBox.Show("Địa chỉ không hợp lệ.");
                        return;
                    }
                    else
                    {
                        string updateQuery = "UPDATE KhachHang " +
                                         "SET TenKhachHang = @TenKhachHang, SDT = @SDT, NgaySinh = @NgaySinh, " +
                                         "DiaChi = @DiaChi, DiemTichLuy = @DiemTichLuy " +
                                         "WHERE MaKhachHang = @MaKhachHang";

                        SqlCommand command = new SqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@TenKhachHang", TenKhachHang);
                        command.Parameters.AddWithValue("@SDT", SDT);
                        command.Parameters.AddWithValue("@NgaySinh", selectNgaySinh);
                        command.Parameters.AddWithValue("@DiaChi", DiaChi);
                        command.Parameters.AddWithValue("@DiemTichLuy", DiemTichLuy);
                        command.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                            LoadDataToGrildKH();
                            txtTenKH.Text = string.Empty;
                            SdtKH.Text = string.Empty;
                            datetimeNgaySinh.Value = DateTime.Now;
                            DiaChiKH.Text = string.Empty;
                            txtDiemTLuy.Text = string.Empty;
                            txtFindMaKH.Text = string.Empty;
                            txtFindTenKH.Text = string.Empty;
                            btnThemKH.Enabled = false;
                            btnSuaKH.Enabled = false;
                            btnXoaKH.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng có mã tương ứng để cập nhật.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (DuLieuKH.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string MaKhachHang = txtMaKH.Text;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @MaKhachHang";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // MaKhachHang không tồn tại trong bảng HoaDon
                            MessageBox.Show("Mã Khách hàng này hiện tại không được xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();


                            string deleteQuery = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";

                            SqlCommand command = new SqlCommand(deleteQuery, connection);
                            command.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa khách hàng thành công!");
                                btnThemKH.Enabled = false;
                                btnSuaKH.Enabled = false;
                                btnXoaKH.Enabled = false;
                                LoadDataToGrildKH();

                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng có mã tương ứng để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.");
            }
        }

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string timTheoMaKhachHang = txtFindMaKH.Text.Trim();
            string timTheoTenKhachHang = txtFindTenKH.Text.Trim();
            
            if(txtFindTenKH.Text == "" || txtFindMaKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin khách hàng càn tìm","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM KhachHang WHERE MaKhachHang = @MaKhachHang OR TenKhachHang = @TenKhachHang";
                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@MaKhachHang", timTheoMaKhachHang);
                    command.Parameters.AddWithValue("@TenKhachHang", timTheoTenKhachHang);

                    SqlDataReader dataReader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    DuLieuKH.DataSource = dataTable;

                    dataReader.Close();

                    // Hiển thị kết quả tìm kiếm trong các ô textbox
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow firstRow = dataTable.Rows[0];
                        txtMaKH.Text = firstRow["MaKhachHang"].ToString();
                        txtTenKH.Text = firstRow["TenKhachHang"].ToString();
                        SdtKH.Text = firstRow["SDT"].ToString();
                        datetimeNgaySinh.Value = Convert.ToDateTime(firstRow["NgaySinh"]);
                        DiaChiKH.Text = firstRow["DiaChi"].ToString();
                        txtDiemTLuy.Text = firstRow["DiemTichLuy"].ToString();
                        btnThemKH.Enabled = false;
                    }
                    else
                    {
                        // Nếu không tìm thấy kết quả, xóa giá trị trong các ô textbox
                        txtTenKH.Text = string.Empty;
                        SdtKH.Text = string.Empty;
                        datetimeNgaySinh.Value = DateTime.Now;
                        DiaChiKH.Text = string.Empty;
                        txtDiemTLuy.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnMoiKH_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string nextMaKH = GetNextMaKH(connection);
                txtMaKH.Text = nextMaKH;
            }
            txtTenKH.Text = string.Empty;
            SdtKH.Text = string.Empty;
            datetimeNgaySinh.Value = DateTime.Now;
            DiaChiKH.Text = string.Empty;
            txtDiemTLuy.Text = string.Empty;
            txtFindMaKH.Text = string.Empty;
            txtFindTenKH.Text = string.Empty;
            btnThemKH.Enabled = true;
            btnSuaKH.Enabled = true;
            btnXoaKH.Enabled = true;

            LoadDataToGrildKH();
        }

        ///////////////////////////////////////////--------------------- QUẢN LÝ NHÂN VIÊN -----------------------///////////////////////////////////////////////////

        private void MenuQlyNhanVien_Click_1(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(QlyNhanVien);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuQlyNhanVien.Enabled = false;
            MenuQlyMenu.Enabled = true;
            MenuTinhTrangDon.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            panel22.Visible = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string nextMaNV = GetNextMaNV(connection);
                NV_txtMaNV.Text = nextMaNV;
                maChucVu();
            }
            LoadDataToGridNV();
        }

        private void ClearForm()
        {
            NV_txtTenNV.Text = string.Empty;
            NV_txtDiaChiNV.Text = string.Empty;
            NV_txtSdtNV.Text = string.Empty;
            NV_txtEmailNV.Text = string.Empty;
            genNam.Checked = false;
            genNu.Checked = false;
            txtMaKHDatBan.Text = string.Empty;
            txtSdtDatBanKH.Text = string.Empty;
            txtMaDatBan.Text = string.Empty;
            txtGhiChuDatBan.Text =string.Empty;
        }
        private void NV_DateNV_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = NV_DateNV.Value;
            DateTime currentDate = DateTime.Now;

            if (selectedDate < currentDate)
            {
                // Ngày được chọn là sau ngày hiện tại
                MessageBox.Show("Ngày được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDataToGridNV()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM NhanVien WHERE MaNhanVien <> 'AD000'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuNV.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private string GetNextMaNV(SqlConnection connection)
        {
            string query = "SELECT MAX(MaNhanVien) FROM NhanVien";
            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            string maxMaNV = Convert.ToString(result);

            if (!string.IsNullOrEmpty(maxMaNV) && maxMaNV.Length >= 3 && maxMaNV.StartsWith("NV"))
            {
                string numberPart = maxMaNV.Substring(2);
                if (int.TryParse(numberPart, out int currentNumber))
                {
                    int nextNumber = currentNumber + 1;
                    string nextMaNV = "NV" + nextNumber.ToString("D3");
                    return nextMaNV;
                }
            }
            return "NV001";
        }

        private void maChucVu()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaChucVu, TenChucVu FROM ChucVu WHERE MaChucVu <> 'ADMIN' AND TenChucVu <> 'Administrator'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    NV_cmbChucVu.DisplayMember = "MaChucVu";
                    NV_cmbChucVu.ValueMember = "TenChucVu";
                    NV_cmbChucVu.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void DuLieuNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                NV_btnThemNV.Enabled = false;
                DataGridViewRow row = DuLieuNV.Rows[e.RowIndex];

                NV_txtMaNV.Text = row.Cells["MaNhanVien"].FormattedValue.ToString();
                NV_cmbChucVu.Text = row.Cells["MaChucVu"].FormattedValue.ToString();
                NV_txtTenNV.Text = row.Cells["TenNhanVien"].FormattedValue.ToString();
                NV_txtSdtNV.Text = row.Cells["SDT"].FormattedValue.ToString();
                NV_txtDiaChiNV.Text = row.Cells["DiaChi"].FormattedValue.ToString();
                NV_txtEmailNV.Text = row.Cells["Email"].FormattedValue.ToString();
                NV_DateNV.Text = row.Cells["NgayVaoLam"].FormattedValue.ToString();
                string gioiTinh = row.Cells["GioiTinh"].FormattedValue.ToString();

                if (gioiTinh == "True")
                {
                    genNam.Checked = true;
                    genNu.Checked = false;
                }
                else
                {
                    genNam.Checked = false;
                    genNu.Checked = true;
                }
            }
        }

        private void NV_txtSdtNV_TextChanged(object sender, EventArgs e)
        {
            string input = NV_txtSdtNV.Text;

            string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length == 10)
            {
                string formattedSdt = string.Format("({0}) {1}-{2}",
                    digitsOnly.Substring(0, 3),
                    digitsOnly.Substring(3, 3),
                    digitsOnly.Substring(6));

                NV_txtSdtNV.Text = formattedSdt;
            }
        }

        static string RemoveDiacritics(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString();
        }

        static string RemoveWhitespace(string input)
        {
            return Regex.Replace(input, @"\s", "");
        }

        static string GenerateRandomString(int length)
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";

            Random random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = characters[random.Next(characters.Length)];
            }

            return new string(result);
        }
        private void ThemTaiKhoan(string TenNV, string MaNv, string MaCV, string Email)
        {
            // Loại bỏ dấu
            string removedDiacritics = RemoveDiacritics(TenNV);

            // Chuyển đổi thành kí tự thường
            string lowercase = removedDiacritics.ToLower();

            // Loại bỏ khoảng trắng
            string result = RemoveWhitespace(lowercase);

            string TaiKhoan = result + MaNv;
            string MatKhau = GenerateRandomString(8);

            connection.Open();
            string Query = $" Insert into TaiKhoan(TaiKhoan,MatKhau,MaChucVu,MaNhanVien,TrangThai) Values ('{TaiKhoan}','{MatKhau}','{MaCV}','{MaNv}', 1 );";
            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.ExecuteNonQuery();

            //Gửi mật khẩu về Gmail đó 
            try
            {
                string senderEmail = "nguyenkhang25506@gmail.com";
                string password = "ktqb cbkv fuwc lqxe";
                string recipientEmail = Email;
                string subject = "TÀI KHOẢN ĐĂNG NHẬP VÀO HỆ THỐNG QUẢN LÍ COFFEE";
                string body = MatKhau;
                string body2 = TaiKhoan;

                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.Body = "Tài khoản: " + body2 + "\nMật khẩu: " + body;


                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(senderEmail, password);

                smtpClient.Send(mail);

                MessageBox.Show("Vui lòng kiểm tra Email để lấy thông tin đăng nhập ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while sending the email: " + ex.ToString());
            }
            connection.Close();
        }

        private void NV_btnThemNV_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string maNV = GetNextMaNV(connection);
                    string maChucVu = NV_cmbChucVu.Text;
                    string tenNV = NV_txtTenNV.Text.Trim();
                    string sdtNV = NV_txtSdtNV.Text.Trim();
                    string emailNV = NV_txtEmailNV.Text.Trim();
                    DateTime ngayVaoLam = DateTime.Now;
                    string gioiTinh = "";
                    if (genNam.Checked)
                    {
                        gioiTinh = "Nam";
                    }
                    if (genNu.Checked)
                    {
                        gioiTinh = "Nữ";
                    }
                    string diachiNV = NV_txtDiaChiNV.Text.Trim();

                    if (string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(sdtNV) || string.IsNullOrEmpty(emailNV) || string.IsNullOrEmpty(diachiNV) || string.IsNullOrEmpty(gioiTinh))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                        return;
                    }
                    else if (!IsValidName(tenNV))
                    {
                        MessageBox.Show("Tên nhân viên không được chứa ký tự đặc biệt hoặc chữ số!");
                        return;
                    }
                    else if (!IsValidEmail(emailNV))
                    {
                        MessageBox.Show("Địa chỉ email không hợp lệ!");
                        return;
                    }
                    else if (!IsValidPhoneNumber(sdtNV))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ!");
                        return;
                    }
                    else if (!IsValidAddress(diachiNV))
                    {
                        MessageBox.Show("Địa chỉ không hợp lệ!");
                        return;
                    }

                    string insertQuery = "INSERT INTO NhanVien (MaNhanVien, MaChucVu, TenNhanVien, SDT, GioiTinh, DiaChi, NgayVaoLam, Email) " +
                        "VALUES (@MaNhanVien, @MaChucVu, @TenNhanVien, @SDT, @GioiTinh, @DiaChi, @NgayVaoLam, @Email)";

                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@MaNhanVien", maNV);
                    command.Parameters.AddWithValue("@MaChucVu", maChucVu);
                    command.Parameters.AddWithValue("@TenNhanVien", tenNV);
                    command.Parameters.AddWithValue("@SDT", sdtNV);
                    command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    command.Parameters.AddWithValue("@DiaChi", diachiNV);
                    command.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                    command.Parameters.AddWithValue("@Email", emailNV);

                    command.ExecuteScalar();

                    string maLuong = GenerateRandomMaLuong(10);

                    string insertLuongQuery = "INSERT INTO LuongNvien (MaLuong, MaNhanVien) VALUES (@MaLuong, @MaNhanVien)";
                    SqlCommand insertLuongCommand = new SqlCommand(insertLuongQuery, connection);
                    insertLuongCommand.Parameters.AddWithValue("@MaLuong", maLuong);
                    insertLuongCommand.Parameters.AddWithValue("@MaNhanVien", maNV);
                    insertLuongCommand.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Thêm dữ liệu thành công! ");

                    ThemTaiKhoan(tenNV, maNV, maChucVu, emailNV);
                    LoadDataToGridNV();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.ToString());
                }
            }
        }
        private string GenerateRandomMaLuong(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[\p{L}\s]+$");
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length == 10)
            {
                string formattedSdt = string.Format("({0}) {1}-{2}",
                    digitsOnly.Substring(0, 3),
                    digitsOnly.Substring(3, 3),
                    digitsOnly.Substring(6));

                NV_txtSdtNV.Text = formattedSdt;
                return true;
            }

            return false;
        }
        private bool IsValidAddress(string address)
        {
            return Regex.IsMatch(address, @"^[^@#$%^&*()\[\]{}\\|<>.,?:;'""~`+=_\-@#\$%\^&\*\(\)\[\]\{\}\\|<>,\.\?\:\;'""]+$");
        }

        private void NV_btnSuaNV_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string maNV = NV_txtMaNV.Text;
                    string maChucVu = NV_cmbChucVu.Text;
                    string tenNV = NV_txtTenNV.Text;
                    string sdtNV = NV_txtSdtNV.Text;
                    string emailNV = NV_txtEmailNV.Text;
                    string gioiTinh = "";
                    if (genNam.Checked)
                    {
                        gioiTinh = "Nam";
                    }
                    if (genNu.Checked)
                    {
                        gioiTinh = "Nữ";
                    }
                    string diachiNV = NV_txtDiaChiNV.Text;
                    if (!IsValidName(tenNV))
                    {
                        MessageBox.Show("Tên không hợp lệ. Vui lòng chỉ nhập chữ cái và khoảng trắng.");
                        return;
                    }

                    if (!IsValidPhoneNumber(sdtNV))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng chỉ nhập 10 chữ số.");
                        return;
                    }

                    if (!IsValidEmail(emailNV))
                    {
                        MessageBox.Show("Email không hợp lệ.");
                        return;
                    }

                    if (!IsValidAddress(diachiNV))
                    {
                        MessageBox.Show("Địa chỉ không hợp lệ. Vui lòng chỉ nhập chữ cái, chữ số, khoảng trắng và dấu '/'");
                        return;
                    }
                    else
                    {
                        string updateQuery = "UPDATE NhanVien SET MaChucVu = @MaChucVu, TenNhanVien = @TenNhanVien, " +
                        "SDT = @SDT, GioiTinh = @GioiTinh, DiaChi = @DiaChi, Email = @Email " +
                        "WHERE MaNhanVien = @MaNhanVien";

                        SqlCommand command = new SqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@MaNhanVien", maNV);
                        command.Parameters.AddWithValue("@MaChucVu", maChucVu);
                        command.Parameters.AddWithValue("@TenNhanVien", tenNV);
                        command.Parameters.AddWithValue("@SDT", sdtNV);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@DiaChi", diachiNV);
                        command.Parameters.AddWithValue("@Email", emailNV);

                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Cập nhật dữ liệu thành công! ");
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.ToString());
                }
                LoadDataToGridNV();
                ClearForm();
            }
        }

        private void NV_btnMoiNV_Click(object sender, EventArgs e)
        {
            ClearForm();
            NV_btnThemNV.Enabled = true;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string nextMaNV = GetNextMaNV(connection);
                NV_txtMaNV.Text = nextMaNV;
                maChucVu();
            }
        }

        private void NV_btnTimNV_Click(object sender, EventArgs e)
        {
            string maNVFind = NV_txtTimMaNV.Text;
            string tenNVFind = NV_txtTimTenNV.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM NhanVien WHERE TenNhanVien = @TenNhanVien OR MaNhanVien = @MaNhanVien";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenNhanVien", tenNVFind);
                        command.Parameters.AddWithValue("@MaNhanVien", maNVFind);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NV_txtMaNV.Text = reader["MaNhanVien"].ToString();
                                NV_cmbChucVu.Text = reader["MaChucVu"].ToString();
                                NV_txtTenNV.Text = reader["TenNhanVien"].ToString();
                                NV_txtSdtNV.Text = reader["SDT"].ToString();
                                NV_txtEmailNV.Text = reader["Email"].ToString();
                                NV_DateNV.Value = Convert.ToDateTime(reader["NgayVaoLam"]);
                                string gioiTinh = reader["GioiTinh"].ToString();
                                if (gioiTinh == "Nam")
                                {
                                    genNam.Checked = true;
                                    genNu.Checked = false;
                                }
                                else
                                {
                                    genNam.Checked = false;
                                    genNu.Checked = true;
                                }
                                NV_txtDiaChiNV.Text = reader["DiaChi"].ToString();
                                NV_btnThemNV.Enabled = false;

                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy dữ liệu phù hợp.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
            LoadDataToGridNV();
        }

        private void DuLieuNV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() == "0")
            {
                e.Value = "Nữ";
                e.FormattingApplied = true;
            }
            else if (e.Value != null && e.Value.ToString() == "1")
            {
                e.Value = "Nam";
                e.FormattingApplied = true;
            }
        }

        private void NV_btnVoHieuNV_Click(object sender, EventArgs e)
        {
            string maNV = NV_txtMaNV.Text;
            connection.Open();

            string QueryCheckNV = $"Select Count(*) from NhanVien Where MaNhanVien = '{maNV}'";
            SqlCommand cmdCheckNV = new SqlCommand(QueryCheckNV, connection);
            int count = (int)cmdCheckNV.ExecuteScalar();
            if (count > 0)
            {
                string query = $"Update TaiKhoan Set TrangThai = 0 Where MaNhanVien = '{maNV}';";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Đã Vô hiệu hóa Nhân Viên: " + maNV);
            }
            else
            {
                MessageBox.Show("Không có Nhân Viên: " + maNV);
            }
        }
                                                       
        ////////////////////////////////////////////// Phiếu Đặt Bàn \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void MenuPhieuDatBan_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add(PhieuDatBan);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyCongNhanVien);

            QLyChamCong.Enabled = true;
            MenuPhieuDatBan.Enabled = false;
            MenuQlyNhanVien.Enabled = true;
            MenuTinhTrangDon.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            MenuQlyMenu.Enabled = true;

            // Lấy dữ liệu khách hàng // 
            KhachHang();
            // Tạo mã đặt bàn tự động
            string nextMaDatBan = GetNextMaDatBan(connection);
            txtMaDatBan.Text = nextMaDatBan;
            // Load dữ liệu đặt bàn // 
            LoadDuLieuDatBan();
            // Load dữ liệu khu vực bàn // 
            KhuVucBan();
            // Lấy dữ liệu mã đặt bàn sang form chi tiết //

        }

        private void cmbKhuVucBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhuVucBan.Text == "K.Vực D1")
            {
                pnKhuD1.Enabled = true;
                pnKhuB.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD4.Enabled = false;
                pnKhuD3.Enabled = false;
            }
            else if (cmbKhuVucBan.Text == "K.Vực B")
            {
                pnKhuB.Enabled = true;
                pnKhuD1.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD4.Enabled = false;
                pnKhuD3.Enabled = false;
            }
            else if (cmbKhuVucBan.Text == "K.Vực C")
            {
                pnKhuC.Enabled = true;
                pnKhuB.Enabled = false;
                pnKhuD1.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD4.Enabled = false;
                pnKhuD3.Enabled = false;
            }
            else if (cmbKhuVucBan.Text == "K.Vực A")
            {
                pnKhuA.Enabled = true;
                pnKhuD1.Enabled = false;
                pnKhuB.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD4.Enabled = false;
                pnKhuD3.Enabled = false;
            }
            else if (cmbKhuVucBan.Text == "K.Vực D2")
            {
                pnKhuD2.Enabled = true;
                pnKhuD1.Enabled = false;
                pnKhuB.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD4.Enabled = false;
                pnKhuD3.Enabled = false;
            }
            else if (cmbKhuVucBan.Text == "K.Vực D4")
            {
                pnKhuD4.Enabled = true;
                pnKhuD1.Enabled = false;
                pnKhuB.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD3.Enabled = false;
            }
            else if (cmbKhuVucBan.Text == "K.Vực D3")
            {
                pnKhuD3.Enabled = true;
                pnKhuD1.Enabled = false;
                pnKhuB.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD4.Enabled = false;
            }
            else
            {
                pnKhuD1.Enabled = false;
                pnKhuB.Enabled = false;
                pnKhuC.Enabled = false;
                pnKhuA.Enabled = false;
                pnKhuD2.Enabled = false;
                pnKhuD4.Enabled = false;
                pnKhuD3.Enabled = false;
            }
        }

        private void KhachHang()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaKhachHang, TenKhachHang, SDT FROM KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmbTenKHDatBan.DisplayMember = "TenKhachHang";
                    cmbTenKHDatBan.ValueMember = "MaKhachHang";
                    cmbTenKHDatBan.DataSource = dataTable;

                    cmbTenKHDatBan.SelectedIndexChanged += CmbMaKHDatBan_SelectedIndexChanged;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }


        private void CmbMaKHDatBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTenKHDatBan.SelectedItem != null)
            {
                DataRowView selectedRow = cmbTenKHDatBan.SelectedItem as DataRowView;
                txtMaKHDatBan.Text = selectedRow["MaKhachHang"].ToString();
                txtSdtDatBanKH.Text = selectedRow["SDT"].ToString();
            }
        }

        // Tạo mã tự động //
        private string GetNextMaDatBan(SqlConnection connection)
        {
            connection.Open();
            string query = "SELECT MAX(MaDatBan) FROM DatBan";
            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            string maxMaDatBan = Convert.ToString(result);
            connection.Close();

            if (!string.IsNullOrEmpty(maxMaDatBan) && maxMaDatBan.Length >= 3 && maxMaDatBan.StartsWith("DB"))
            {
                string numberPart = maxMaDatBan.Substring(2);
                if (int.TryParse(numberPart, out int currentNumber))
                {
                    int nextNumber = currentNumber + 1;
                    string nextMaDatBan = "DB" + nextNumber.ToString("D2");
                    return nextMaDatBan;
                }
            }

            return "DB01";
        }

        // Phiếu Đặt Bàn // 
        // Load dữ liệu Bàn // 
        private void KhuVucBan()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM KhuVucBan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmbKhuVucBan.DisplayMember = "TenKhu";
                    cmbKhuVucBan.ValueMember = "MaKhu";
                    cmbKhuVucBan.DataSource = dataTable;
                    cmbKhuVucBan.Text = "----------- Chọn Bàn -----------";

                    connection.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
        private int bienDemTaoBan;

        private void btnChonBan_Click(object sender, EventArgs e)
        {
            bienDemTaoBan = 1;
            pnDatBan.Visible = false;
            string maDatBan = GetNextMaDatBan(connection);
            lblMaDatBan.Text = maDatBan;
            MessageBox.Show(maDatBan);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO DatBan (MaDatBan) VALUES (@MaDatBan)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDatBan", maDatBan);

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Đã tạo phiếu đặt bàn, vui lòng chọn khu vực và bàn mong muốn");
            }
            this.FormClosing += TrangChu_FormClosing;
            LoadDuLieuDatBan();
        }

        private void ChonBan(string bienKhu, string bienBan)
        {
            string maBanchinh = string.Empty; // Mã bàn

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT MaBan FROM BanNgoi WHERE soBan = @SoBan AND TenKhu = @TenKhu";

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@SoBan", bienBan);
                        selectCommand.Parameters.AddWithValue("@TenKhu", bienKhu);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                maBanchinh = reader.GetString(reader.GetOrdinal("MaBan"));
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy mã bàn.");
                                return;
                            }
                        }
                    }

                    string maKhuVuc = string.Empty;
                    string maKhuQuery = "SELECT MaKhu FROM BanNgoi WHERE MaBan = @MaBan";

                    using (SqlCommand command = new SqlCommand(maKhuQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaBan", maBanchinh);

                        using (SqlDataReader readerMaKhu = command.ExecuteReader())
                        {
                            if (readerMaKhu.Read())
                            {
                                maKhuVuc = readerMaKhu["MaKhu"].ToString();
                                lblMaKhu.Text = maKhuVuc;
                            }
                        }
                    }

                    string maDatBan = lblMaDatBan.Text;
                    string newMaBan = maBanchinh;
                    string maKhu = lblMaKhu.Text;
                    string tenKhu = cmbKhuVucBan.Text;
                    string soBan = bienBan;

                    string insertQuery = "INSERT INTO ChiTietPhieuDatBan (MaDatBan, MaBan, MaKhu, TenKhu, SoBan) " +
                                         "VALUES (@MaDatBan, @MaBan, @MaKhu, @TenKhu, @SoBan)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@MaDatBan", maDatBan);
                        insertCommand.Parameters.AddWithValue("@MaBan", newMaBan);
                        insertCommand.Parameters.AddWithValue("@MaKhu", maKhu);
                        insertCommand.Parameters.AddWithValue("@TenKhu", tenKhu);
                        insertCommand.Parameters.AddWithValue("@SoBan", soBan);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
        }


        private void Ban1D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban18D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban18D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban3D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban4D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban5D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban6D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban7D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban7D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp  
        }

        private void Ban8D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban8D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban9D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban9D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban10D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban10D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban11D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban11D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban12D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban12D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban13D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban13D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban14D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban14D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban15D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban15D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp
        }

        private void Ban16D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban16D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }

        private void Ban17D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban17D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2D1_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2D1.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }

        

        // khu B

        
        private void Ban6B_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6B.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2B_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2B.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban3B_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3B.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban4B_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4B.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban5B_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5B.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban1B_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1B.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }

        // Khu C
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Ban1C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban3C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban4C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4C.Text);
            Button button = (Button)sender;
            button.Enabled = false;

        }
        private void Ban5C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5C.Text);
            Button button = (Button)sender;
            button.Enabled = false;

        }
        private void Ban6C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban7C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban7C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban8C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban8C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban9C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban9C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban10C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban10C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban11C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban11C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban12C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban12C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban13C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban13C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban14C_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban14C.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }

        private void groupBox29_Enter(object sender, EventArgs e)
        {

        }
        //Khu D2

        private void Ban8D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban8D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban3D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban4D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban5D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban6D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban7D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban7D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban1D2_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1D2.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        //Khu A
        private void Ban8A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban8A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban4A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban3A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban6A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban5A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban1A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban7A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban7A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban10A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban10A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban9A_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban9A.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        //Khu D4
        private void Ban1D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban3D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban4D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban5D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban6D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban7D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban7D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban14D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban14D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban13D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban13D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban12D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban12D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban11D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban11D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban10D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban10D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban9D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban9D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban8D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban8D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban15D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban15D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban16D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban16D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban17D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban17D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban18D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban18D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban19D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban19D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban20D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban20D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban21D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban21D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban28D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban28D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban27D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban27D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban26D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban26D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban25D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban25D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban24D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban24D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp


        }
        private void Ban23D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban23D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban22D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban22D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban29D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban29D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban30D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban30D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban31D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban31D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban32D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban32D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban33D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban33D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban35D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban35D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban36D4_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban36D4.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }

        private void Ban12D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban12D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban2D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban2D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp


        }
        private void Ban3D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban3D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban4D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban4D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban5D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban5D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban6D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban6D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban7D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban7D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban8D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban8D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban9D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban9D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban10D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban10D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban11D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban11D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }
        private void Ban1D3_Click(object sender, EventArgs e)
        {
            ChonBan(cmbKhuVucBan.Text, Ban1D3.Text);
            Button button = (Button)sender;
            button.Enabled = false; // Enable lại nút bấm vừa nhấp

        }




        // Lưu phiếu đặt bàn // 
        private void dateNhanBan_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateTime1 = dateDatBan.Value;
            DateTime dateTime2 = dateNhanBan.Value;
            if (dateTime2 < dateTime1)
            {
                MessageBox.Show("Thời gian nhận bàn phải sau thời gian đặt bàn!");
            }
        }

        private void HienButton()
        {
            Ban1D1.Enabled = true;
            Ban2D1.Enabled = true;
            Ban3D1.Enabled = true;
            Ban4D1.Enabled = true;
            Ban5D1.Enabled = true;
            Ban6D1.Enabled = true;
            Ban7D1.Enabled = true;
            Ban8D1.Enabled = true;
            Ban9D1.Enabled = true;
            Ban10D1.Enabled = true;
            Ban11D1.Enabled = true;
            Ban12D1.Enabled = true;
            Ban13D1.Enabled = true;
            Ban14D1.Enabled = true;
            Ban15D1.Enabled = true;
            Ban16D1.Enabled = true;
            Ban17D1.Enabled = true;
            Ban18D1.Enabled = true;
            Ban1D2.Enabled = true;
            Ban2D2.Enabled = true;
            Ban3D2.Enabled = true;
            Ban4D2.Enabled = true;
            Ban5D2.Enabled = true;
            Ban6D2.Enabled = true;
            Ban7D2.Enabled = true;
            Ban8D2.Enabled = true;
            Ban1B.Enabled = true;
            Ban2B.Enabled = true;
            Ban3B.Enabled = true;
            Ban4B.Enabled = true;
            Ban5B.Enabled = true;
            Ban6B.Enabled = true;
            Ban1C.Enabled = true;
            Ban2C.Enabled = true;
            Ban3C.Enabled = true;
            Ban4C.Enabled = true;
            Ban5C.Enabled = true;
            Ban6C.Enabled = true;
            Ban7C.Enabled = true;
            Ban8C.Enabled = true;
            Ban9C.Enabled = true;
            Ban10C.Enabled = true;
            Ban11C.Enabled = true;
            Ban12C.Enabled = true;
            Ban13C.Enabled = true;
            Ban14C.Enabled = true;
            Ban1A.Enabled = true;
            Ban2A.Enabled = true;
            Ban3A.Enabled = true;
            Ban4A.Enabled = true;
            Ban5A.Enabled = true;
            Ban6A.Enabled = true;
            Ban1D3.Enabled = true;
            Ban2D3.Enabled = true;
            Ban3D3.Enabled = true;
            Ban4D3.Enabled = true;
            Ban5D3.Enabled = true;
            Ban6D3.Enabled = true;
            Ban7D4.Enabled = true;
            Ban8D4.Enabled = true;
            Ban9D4.Enabled = true;
            Ban10D3.Enabled = true;
            Ban11D3.Enabled = true;
            Ban12D3.Enabled = true;
            Ban13D4.Enabled = true;
            Ban14D4.Enabled = true;
            Ban15D4.Enabled = true;
            Ban16D4.Enabled = true;
            Ban17D4.Enabled = true;
            Ban18D4.Enabled = true;
            Ban19D4.Enabled = true;
            Ban20D4.Enabled = true;
            Ban21D4.Enabled = true;
            Ban22D4.Enabled = true;
            Ban23D4.Enabled = true;
            Ban24D4.Enabled = true;
            Ban25D4.Enabled = true;
            Ban26D4.Enabled = true;
            Ban27D4.Enabled = true;
            Ban28D4.Enabled = true;
            Ban29D4.Enabled = true;
            Ban30D4.Enabled = true;
            Ban31D4.Enabled = true;
            Ban32D4.Enabled = true;
            Ban33D4.Enabled = true;
            Ban35D4.Enabled = true;
            Ban36D4.Enabled = true;
            Ban1D3.Enabled = true;
            Ban2D3.Enabled = true;
            Ban3D3.Enabled = true;
            Ban4D3.Enabled = true;
            Ban5D3.Enabled = true;
            Ban4D3.Enabled = true;
            Ban5D3.Enabled = true;
            Ban6D3.Enabled = true;
            Ban7D3.Enabled = true;
            Ban8D3.Enabled = true;
            Ban9D3.Enabled = true;
            Ban10D3.Enabled = true;
            Ban11D3.Enabled = true;
            Ban12D3.Enabled = true;

        }

        private void btnDatBan_Click(object sender, EventArgs e)
        {
            string maDatBan = txtMaDatBan.Text;
            string maNhanVien = "NV001";
            DateTime ngayDatBan = dateDatBan.Value;
            string maKhachHang = txtMaKHDatBan.Text;
            DateTime thoiGianNhan = dateNhanBan.Value;
            int soLuong = 0;



            //Cộng Số lượng bàn đã chọn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo câu truy vấn SQL
                string query = $"SELECT COUNT(*) FROM ChiTietPhieuDatBan WHERE MaDatBan = '{lblMaDatBan.Text}'";

                // Thực thi câu truy vấn
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int totalCount = (int)command.ExecuteScalar();
                    soLuong = totalCount;
                }

                connection.Close();
            }

            if (maKhachHang == "")
            {
                MessageBox.Show("Vui lòng chọn lại mã khách hàng");
                return;
            }





            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    List<string> maBanList = new List<string>();

                    // Step 1: Select MaBan from ChiTietPhieuDatBan where MaDatBan = 'MDB vừa tạo'
                    connection.Open();

                    string query1 = "SELECT MaBan FROM ChiTietPhieuDatBan WHERE MaDatBan = @MaDatBan";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@MaDatBan", maDatBan);

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maBan = reader.GetString(0);
                            maBanList.Add(maBan);
                        }
                    }

                    List<string> maDatBanList = new List<string>();

                    // Step 2: Select MaDatBan from ChiTietPhieuDatBan where MaBan in ('các mã bàn vừa lấy ở truy vấn 1')
                    if (maBanList.Count > 0)
                    {
                        foreach (string maBan in maBanList)
                        {
                            string query2 = "SELECT MaDatBan FROM ChiTietPhieuDatBan WHERE MaBan = @MaBan";
                            SqlCommand command2 = new SqlCommand(query2, connection);
                            command2.Parameters.AddWithValue("@MaBan", maBan);

                            using (SqlDataReader reader = command2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string maDatBann = reader.GetString(0);
                                    maDatBanList.Add(maDatBann);
                                }
                            }
                        }
                    }

                    // Step 3: SELECT COUNT(*) FROM DatBan WHERE MaDatBan = 'các mã bàn đã lấy ở truy vấn 2' AND ThoiGianNhanBan = 'Trên datetimepicker'
                    if (maDatBanList.Count > 0)
                    {
                        DateTime thoiGianNhanBan = dateNhanBan.Value;
                        string TGNB = thoiGianNhanBan.ToString("yyyy/MM/dd");
                        int temp = 0;

                        foreach (string MaDatBan in maDatBanList)
                        {
                            string query3 = "SELECT COUNT(*) FROM DatBan WHERE MaDatBan = @MaDatBan AND ThoiGianNhanBan = @ThoiGianNhanBan";
                            SqlCommand command3 = new SqlCommand(query3, connection);
                            command3.Parameters.AddWithValue("@ThoiGianNhanBan", TGNB);
                            command3.Parameters.AddWithValue("@MaDatBan", MaDatBan);


                            int count = (int)command3.ExecuteScalar();
                            temp += count;


                        }

                        if (temp > 0)
                        {
                            MessageBox.Show("Hiện tại có bàn trùng ngày");
                            return;
                        }
                    }

                    string CheckCT = $"Select Count(*) from ChiTietPhieuDatBan where MaDatBan = '{maDatBan}'";
                    SqlCommand cmd = new SqlCommand(CheckCT, connection);
                    int countt = (int)cmd.ExecuteScalar();
                    if (countt == 0)
                    {
                        MessageBox.Show("Vui lòng chọn bàn để đặt", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }






                    string updateQuery = @"UPDATE DatBan 
                                           SET MaDatBan = @MaDatBan, MaNhanVien = @MaNhanVien, NgayDatBan = @NgayDatBan, 
                                           MaKhachHang = @MaKhachHang, ThoiGianNhanBan = @ThoiGianNhanBan, SoLuong = @SoLuong 
                                           WHERE MaDatBan = @OldMaDatBan";

                    using (SqlCommand command12 = new SqlCommand(updateQuery, connection))
                    {
                        command12.Parameters.AddWithValue("@MaDatBan", txtMaDatBan.Text);
                        command12.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        command12.Parameters.AddWithValue("@NgayDatBan", ngayDatBan);
                        command12.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                        command12.Parameters.AddWithValue("@ThoiGianNhanBan", thoiGianNhan);
                        command12.Parameters.AddWithValue("@SoLuong", soLuong);
                        command12.Parameters.AddWithValue("@OldMaDatBan", maDatBan);

                        int rowsAffected = command12.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đặt bàn thành công!");
                            if (cmbKhuVucBan.Text == "K.Vực D1")
                            {
                                pnKhuD1.Enabled = true;
                                pnKhuB.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD4.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            else if (cmbKhuVucBan.Text == "K.Vực B")
                            {
                                pnKhuB.Enabled = true;
                                pnKhuD1.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD4.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            else if (cmbKhuVucBan.Text == "K.Vực C")
                            {
                                pnKhuC.Enabled = true;
                                pnKhuB.Enabled = false;
                                pnKhuD1.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD4.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            else if (cmbKhuVucBan.Text == "K.Vực A")
                            {
                                pnKhuA.Enabled = true;
                                pnKhuD1.Enabled = false;
                                pnKhuB.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD4.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            else if (cmbKhuVucBan.Text == "K.Vực D2")
                            {
                                pnKhuD2.Enabled = true;
                                pnKhuD1.Enabled = false;
                                pnKhuB.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD4.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            else if (cmbKhuVucBan.Text == "K.Vực D4")
                            {
                                pnKhuD4.Enabled = true;
                                pnKhuD1.Enabled = false;
                                pnKhuB.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            else if (cmbKhuVucBan.Text == "K.Vực D3")
                            {
                                pnKhuD3.Enabled = true;
                                pnKhuD1.Enabled = false;
                                pnKhuB.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD4.Enabled = false;
                            }
                            else
                            {
                                pnKhuD1.Enabled = false;
                                pnKhuB.Enabled = false;
                                pnKhuC.Enabled = false;
                                pnKhuA.Enabled = false;
                                pnKhuD2.Enabled = false;
                                pnKhuD4.Enabled = false;
                                pnKhuD3.Enabled = false;
                            }
                            bienDemTaoBan++;
                            TrangChu_Load(sender, e);
                            MenuPhieuDatBan.Enabled = true;
                            pnDatBan.Visible = true;
                            ClearForm();
                            lblMaKhu.Text = "";
                            lblMaDatBan.Text = "";
                            HienButton();
                        }
                        else
                        {
                            MessageBox.Show("Đặt bàn thất bại!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.ToString());
                }
            }
        }

        // Hiển thị dữ liệu đặt bàn //  
        private void LoadDuLieuDatBan()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM DatBan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DuLieuDatBan.DataSource = dataTable;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private bool IsRecordDeleted(SqlConnection connection, int maDatBan)
        {
            string query = "SELECT COUNT(*) FROM ChiTietPhieuDatBan WHERE MaDatBan = @MaDatBan";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaDatBan", maDatBan);

                int count = (int)command.ExecuteScalar();
                return count == 0;
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            ChinhSuaChiTietDatBan formChiTiet = new ChinhSuaChiTietDatBan();
            string LayMaDatBan = lblMaDatBan.Text;
            formChiTiet.LayMaDatBan = LayMaDatBan;
            formChiTiet.Count = 1;
            formChiTiet.Show();
        }

        private void DuLieuDatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            connection.Open();
            ChinhSuaChiTietDatBan formChinhSua = new ChinhSuaChiTietDatBan();

            int RowIndex = DuLieuDatBan.CurrentCell.RowIndex;
            string maDatBan = DuLieuDatBan.Rows[RowIndex].Cells["MaDatBan"].FormattedValue.ToString();
            formChinhSua.LayMaDatBan = maDatBan;
            formChinhSua.Count = 0;
            formChinhSua.Show();
            connection.Close();
        }

        public void EnabledBtn(List<string> dataList)
        {
            foreach (string maban in dataList)
            {
                // Tìm Button có tên tương ứng
                Button button = Controls.Find(maban, true).FirstOrDefault() as Button;

                // Nếu tìm thấy Button, ẩn nó
                if (button != null)
                {
                    button.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Button có tên " + maban);
                }
            }

        }
        public void DemSoBanChiTiet()
        {
            int maDatBan;
            if (int.TryParse(lblMaDatBan.Text, out maDatBan))
            {
                int soLuongDatBan = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM ChiTietPhieuDatBan WHERE MaDatBan = @MaDatBan";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaDatBan", maDatBan);

                            soLuongDatBan = (int)command.ExecuteScalar();
                        }
                        bool isRecordDeleted = IsRecordDeleted(connection, maDatBan);
                        if (isRecordDeleted)
                        {
                            soLuongDatBan--;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        /// /////////////////////// CHẤM CÔNG ////////////////////////////////////// /////////////////////// CHẤM CÔNG ////////////////////////////////////// /////////////////////// CHẤM CÔNG ////////////////////////////////////// /////////////////////// CHẤM CÔNG ////////////////////////////////////// /////////////////////// CHẤM CÔNG ///////////////////////////////////
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            HS_ThoiGianThuc.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy, HH:mm:ss tt"); // Định dạng thời gian hiện tại theo HH:mm:ss
        }
        
        private void HS_LoadMaNhanVien()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MaNhanVien FROM NhanVien WHERE MaNhanVien <> 'AD000'";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string maNhanVien = reader.GetString(0);
                        HS_cmbMaNVien.Items.Add(maNhanVien);
                    }
                }
            }
        }
        private void HS_cmbMaNVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            HS_CheckInTime.Visible = true;
            string selectedMaNhanVien = HS_cmbMaNVien.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Truy vấn thông tin nhân viên từ bảng NhanVien
                string queryNhanVien = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                SqlCommand commandNhanVien = new SqlCommand(queryNhanVien, connection);
                commandNhanVien.Parameters.AddWithValue("@MaNhanVien", selectedMaNhanVien);

                using (SqlDataReader readerNhanVien = commandNhanVien.ExecuteReader())
                {
                    if (readerNhanVien.Read())
                    {
                        string hoTen = readerNhanVien.GetString(readerNhanVien.GetOrdinal("TenNhanVien"));
                        string diaChi = readerNhanVien.GetString(readerNhanVien.GetOrdinal("DiaChi"));
                        string soDienThoai = readerNhanVien.GetString(readerNhanVien.GetOrdinal("SDT"));
                        DateTime ngayVaoLam = readerNhanVien.GetDateTime(readerNhanVien.GetOrdinal("NgayVaoLam"));
                        string ngayVaoLamFormatted = ngayVaoLam.ToString("dd/MM/yyyy");
                        string Emaill = readerNhanVien.GetString(readerNhanVien.GetOrdinal("Email"));
                        string MaChucVu = readerNhanVien.GetString(readerNhanVien.GetOrdinal("MaChucVu"));

                        txtHSTenNV.Text = hoTen;
                        txtHSDiaChi.Text = diaChi;
                        txtHSSDT.Text = soDienThoai;
                        txtHSNgayVaoLam.Text = ngayVaoLamFormatted;
                        txtHSEmail.Text = Emaill;
                        HS_lblMaChucVu.Text = MaChucVu;
                    }
                }
                string queryChamCong = $"SELECT GioBatDau FROM ChamCong WHERE MaNhanVien = '{HS_cmbMaNVien.Text}' AND GioKetThuc IS NULL AND TongGioLam IS NULL";
                SqlCommand commandChamCong = new SqlCommand(queryChamCong, connection);
                commandChamCong.Parameters.AddWithValue("@MaNhanVien", selectedMaNhanVien);

                using (SqlDataReader readerChamCong = commandChamCong.ExecuteReader())
                {
                    if (readerChamCong.Read())
                    {
                        TimeSpan gioBatDau = readerChamCong.GetTimeSpan(readerChamCong.GetOrdinal("GioBatDau"));
                        string gioBatDauFormatted = DateTime.Today.Add(gioBatDau).ToString("HH:mm:ss");
                        HS_CheckInTime.Text = gioBatDauFormatted;
                    }
                    else
                    {
                        HS_CheckInTime.Text = "";

                    }
                }
            }
        }

        private void HS_lblMaChucVu_TextChanged(object sender, EventArgs e)
        {
            string maChucVu = HS_lblMaChucVu.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TenChucVu FROM ChucVu WHERE MaChucVu = @MaChucVu";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaChucVu", maChucVu);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    string tenChucVu = result.ToString();
                    HS_lblTenChucVu.Text = tenChucVu;
                }
                else
                {
                    HS_lblTenChucVu.Text = string.Empty;
                }
            }
        }
        private void HS_btnCheck_In_Click(object sender, EventArgs e)
        {
            if (HS_cmbMaNVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để chấm công.");
                return;
            }

            string gioBatDau = DateTime.Now.ToString("HH:mm:ss");
            string maNhanVienChamCong = HS_cmbMaNVien.Text;

            TimeSpan gioBatDauTime = DateTime.Now.TimeOfDay;
            TimeSpan gioKetThucTime1 = new TimeSpan(22, 30, 0);
            TimeSpan gioKetThucTime2 = new TimeSpan(4, 0, 0);

            if (gioBatDauTime >= gioKetThucTime1 || gioBatDauTime < gioKetThucTime2)
            {
                MessageBox.Show("Không thể chấm công trong khoảng từ 22:30 PM đến 4:00 AM.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM ChamCong WHERE MaNhanVien = @MaNhanVien AND GioKetThuc IS NULL AND TongGioLam IS NULL";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@MaNhanVien", maNhanVienChamCong);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Bạn cần phải chấm công ra để chấm công vào tiếp!");
                    return;
                }

                string query = "SELECT MaCaLamViec FROM CaLamViec WHERE @GioBatDau BETWEEN GioBatDau AND GioKetThuc";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GioBatDau", gioBatDau);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string maCaLamViec = reader["MaCaLamViec"].ToString();
                    reader.Close();

                    DateTime ngayLamViec = DateTime.Now;
                    string ngayLamViecCheck = ngayLamViec.ToString("yyyy/MM/dd");
                    string maChamCong = GenerateRandomMaChamCong();

                    string insertQuery = "INSERT INTO ChamCong (MaChamCong, MaNhanVien, MaCaLamViec, NgayLamViec, GioBatDau) " +
                                         "VALUES (@MaChamCong, @MaNhanVien, @MaCaLamViec, @NgayLamViec, @GioBatDau)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@MaChamCong", maChamCong);
                    insertCommand.Parameters.AddWithValue("@MaNhanVien", maNhanVienChamCong);
                    insertCommand.Parameters.AddWithValue("@MaCaLamViec", maCaLamViec);
                    insertCommand.Parameters.AddWithValue("@NgayLamViec", ngayLamViecCheck);
                    insertCommand.Parameters.AddWithValue("@GioBatDau", gioBatDau);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Đã chấm công vào cho nhân viên " + maNhanVienChamCong + " thuộc ca: " + maCaLamViec);
                    HS_CheckInTime.Text = gioBatDau;
                    HS_CheckInTime.Visible = true;
                    LoadDuLieuChamCong();
                }
            }
        }
        private string GenerateRandomMaChamCong()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void HS_Check_out_Click(object sender, EventArgs e)
        {
            if (HS_cmbMaNVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để chấm công.");
                return;
            }

            DateTime ngayLamViec = DateTime.Now;
            string gioKetThuc = ngayLamViec.ToString("HH:mm:ss");
            string gioBatDau = HS_CheckInTime.Text;

            TimeSpan gioBatDauTime = DateTime.Now.TimeOfDay;
            TimeSpan gioKetThucTime1 = new TimeSpan(22, 30, 0);
            TimeSpan gioKetThucTime2 = new TimeSpan(4, 0, 0);

            if (gioBatDauTime >= gioKetThucTime1 || gioBatDauTime < gioKetThucTime2)
            {
                MessageBox.Show("Không thể chấm công trong khoảng từ 22:30 PM đến 4:00 AM.");
                return;
            }
            string updateChamCongQuery = @"UPDATE ChamCong 
                             SET GioKetThuc = @GioKetThuc, 
                             TongGioLam = DATEDIFF(MINUTE, GioBatDau, @GioKetThuc) / 60.0 
                             WHERE GioBatDau = @GioBatDau;
                                UPDATE LuongNvien 
                                SET TongGioLam = (SELECT SUM(TongGioLam) FROM ChamCong WHERE MaNhanVien = @MaNhanVien),
                                TongTienLuong = (SELECT SUM(TongGioLam) * CASE NhanVien.MaChucVu
                                                            WHEN 'NVDN' THEN 17000 
                                                            WHEN 'NVPC' THEN 22000 
                                                            WHEN 'NVPV' THEN 15000 
                                                            WHEN 'NVQLK' THEN 14000 
                                                            WHEN 'NVTN' THEN 18000 
                                                            WHEN 'QALY' THEN 25000
                                                            ELSE 0 
                                                        END 
                                            FROM ChamCong JOIN NhanVien ON ChamCong.MaNhanVien = NhanVien.MaNhanVien 
                                            WHERE ChamCong.MaNhanVien = @MaNhanVien
                                            GROUP BY NhanVien.MaChucVu)
                                            WHERE MaNhanVien = @MaNhanVien;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateChamCongQuery, connection);
                command.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);
                command.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                command.Parameters.AddWithValue("@MaNhanVien", HS_cmbMaNVien.Text);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            MessageBox.Show("Đã chấm công ra cho nhân viên");
            HS_CheckInTime.Text = "";
            HS_CheckOutTime.Text = "";
            HS_CheckInTime.Visible = false;

            LoadDuLieuChamCong();
        }
        private void LoadDuLieuChamCong()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM ChamCong";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    DuLieuChamCongNhanVien.DataSource = dataTable;
                    DuLieuChamCongNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    DuLieuChamCongNhanVien.DefaultCellStyle.Font = new Font("Times New Roman", 10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void QLyChamCong_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Add(QlyCongNhanVien);

            QLyChamCong.Enabled = false;
            MenuQlyMenu.Enabled = true;
            MenuTinhTrangDon.Enabled = true;
            MenuHoaDon.Enabled = true;
            MenuPhieuDatBan.Enabled = true;
            MenuPhieuXuat.Enabled = true;
            MenuPhieuNhap.Enabled = true;
            MenuQlyKHang.Enabled = true;
            MenuQlyNhanVien.Enabled = true;
            MenuQlyNCC.Enabled = true;
            MenuQlyLMon.Enabled = true;
            LoadDuLieuChamCong();
        }
        ///////////////////////////////////////////////// Thông kê doanh thu \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void MenuTKDoanhThu_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyCongNhanVien);
            tabControl1.TabPages.Remove(TKdoanhthu);
            tabControl1.TabPages.Remove(TKTonKho);
            tabControl1.TabPages.Remove(BaoCao);
            tabControl1.TabPages.Add(TKdoanhthu);
            CN_TKDoanhThu();
            Load_bieudo();
        }

        private void CN_TKDoanhThu()
        {
            //Hiển thị số đơn hàng theo tháng 

            DateTime dateTime = DateTime.Now;
            string Thang = dateTime.ToString("MM");

            connection.Open();
            string QuerySLDon_Thang = $"SELECT COUNT(*) FROM HoaDon WHERE MONTH(NgayBanHang) = {Thang}";
            SqlCommand cmdSLDon_Thang = new SqlCommand(QuerySLDon_Thang, connection);
            int SLDon_Thang = (int)cmdSLDon_Thang.ExecuteScalar();
            TKDT_lbSLdon.Text = SLDon_Thang.ToString();


            string QueryTongTien_Thang = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE MONTH(NgayBanHang) = {Thang}";
            SqlCommand cmdTongTien_Thang = new SqlCommand(QueryTongTien_Thang, connection);
            object result = cmdTongTien_Thang.ExecuteScalar();

            double TongTien_Thang;
            if (result != DBNull.Value)
            {
                TongTien_Thang = Convert.ToDouble(result);
            }
            else
            {
                // Giá trị trả về là null, gán giá trị mặc định cho TongTien_Thang
                TongTien_Thang = 0.0; // Hoặc giá trị mặc định khác tùy ý
            }

            TKDT_lbDoanhThu.Text = TongTien_Thang.ToString();


            connection.Close();


        }

        private void TKDT_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TKDT_cmb.SelectedIndex == 0)
            {
                DateTime dateTime = DateTime.Now;
                string HomNay = dateTime.ToString("yyyy/MM/dd");

                connection.Open();
                string QuerySLDon_Thang = $"SELECT COUNT(*) FROM HoaDon WHERE CONVERT(date, NgayBanHang) = '{HomNay}'";
                SqlCommand cmdSLDon_Thang = new SqlCommand(QuerySLDon_Thang, connection);
                int SLDon_Thang = (int)cmdSLDon_Thang.ExecuteScalar();


                string QueryTongTien_Thang = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE CONVERT(date, NgayBanHang) =  '{HomNay}';";
                SqlCommand cmdTongTien_Thang = new SqlCommand(QueryTongTien_Thang, connection);
                object result = cmdTongTien_Thang.ExecuteScalar();

                double TongTien_Thang;
                if (result != DBNull.Value)
                {
                    TongTien_Thang = Convert.ToDouble(result);
                }
                else
                {
                    // Giá trị trả về là null, gán giá trị mặc định cho TongTien_Thang
                    TongTien_Thang = 0.0; // Hoặc giá trị mặc định khác tùy ý
                }

                TKDT_txtDoanhThu.Text = "Doanh thu hôm nay";
                TKDT_lbDoanhThu.Text = TongTien_Thang.ToString();

                TKDT_txtSLdon.Text = "Số lượng đơn hôm nay";
                TKDT_lbSLdon.Text = SLDon_Thang.ToString();


                connection.Close();
            }
            else if (TKDT_cmb.SelectedIndex == 1)
            {
                //Hiển thị số đơn hàng theo tháng 

                DateTime dateTime = DateTime.Now;
                string Thang = dateTime.ToString("MM");

                connection.Open();
                string QuerySLDon_Thang = $"SELECT COUNT(*) FROM HoaDon WHERE MONTH(NgayBanHang) = {Thang}";
                SqlCommand cmdSLDon_Thang = new SqlCommand(QuerySLDon_Thang, connection);
                int SLDon_Thang = (int)cmdSLDon_Thang.ExecuteScalar();


                string QueryTongTien_Thang = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE MONTH(NgayBanHang) = {Thang}";
                SqlCommand cmdTongTien_Thang = new SqlCommand(QueryTongTien_Thang, connection);
                object result = cmdTongTien_Thang.ExecuteScalar();

                double TongTien_Thang;
                if (result != DBNull.Value)
                {
                    TongTien_Thang = Convert.ToDouble(result);
                }
                else
                {
                    // Giá trị trả về là null, gán giá trị mặc định cho TongTien_Thang
                    TongTien_Thang = 0.0; // Hoặc giá trị mặc định khác tùy ý
                }

                TKDT_txtDoanhThu.Text = "Doanh thu Tháng nay";
                TKDT_lbDoanhThu.Text = TongTien_Thang.ToString();

                TKDT_txtSLdon.Text = "Số lượng đơn Tháng nay";
                TKDT_lbSLdon.Text = SLDon_Thang.ToString();


                connection.Close();
            }
            else if (TKDT_cmb.SelectedIndex == 2)
            {
                //Hiển thị số đơn hàng theo tháng 

                DateTime dateTime = DateTime.Now;
                string Nam = dateTime.ToString("yyyy");

                connection.Open();
                string QuerySLDon_Thang = $"SELECT COUNT(*) FROM HoaDon WHERE Year(NgayBanHang) =  {Nam}";
                SqlCommand cmdSLDon_Thang = new SqlCommand(QuerySLDon_Thang, connection);
                int SLDon_Thang = (int)cmdSLDon_Thang.ExecuteScalar();


                string QueryTongTien_Thang = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE Year(NgayBanHang) = {Nam}";
                SqlCommand cmdTongTien_Thang = new SqlCommand(QueryTongTien_Thang, connection);
                object result = cmdTongTien_Thang.ExecuteScalar();

                double TongTien_Thang;
                if (result != DBNull.Value)
                {
                    TongTien_Thang = Convert.ToDouble(result);
                }
                else
                {
                    // Giá trị trả về là null, gán giá trị mặc định cho TongTien_Thang
                    TongTien_Thang = 0.0; // Hoặc giá trị mặc định khác tùy ý
                }

                TKDT_txtDoanhThu.Text = "Doanh thu Năm nay";
                TKDT_lbDoanhThu.Text = TongTien_Thang.ToString();

                TKDT_txtSLdon.Text = "Số lượng đơn Năm nay";
                TKDT_lbSLdon.Text = SLDon_Thang.ToString();


                connection.Close();
            }
        }

        private void Load_bieudo()
        {
            DateTime now = DateTime.Now;
            int currentMonth = now.Month;
            int currentYear = now.Year;
            int currentDay = now.Day;

            //Theo ngày
            for (int day = 1; day <= now.Day; day++)
            {
                string dayLabel = day.ToString();
                connection.Open();
                string QueryTongTien_Ngay = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE Day(NgayBanHang) = {day} And  MONTH(NgayBanHang) = {currentMonth} And YEAR(NgayBanHang) = {currentYear}";
                SqlCommand cmdTongTien_Ngay = new SqlCommand(QueryTongTien_Ngay, connection);
                object result = cmdTongTien_Ngay.ExecuteScalar();
                connection.Close();
                TK_ChartTheoNgay.Series["TK_ChartTheoNgay"].Points.AddXY(dayLabel, result);
            }


            //Theo giờ
            DateTime startTime = new DateTime(now.Year, now.Month, now.Day, 5, 0, 0); // Thời gian bắt đầu (5:00 AM)
            DateTime endTime = new DateTime(now.Year, now.Month, now.Day, 22, 0, 0); // Thời gian kết thúc (10:00 PM)
            TimeSpan timeStep = TimeSpan.FromHours(1); // Khoảng thời gian cách nhau 1 giờ

            for (DateTime time = startTime; time <= endTime; time += timeStep)
            {
                string timeLabel = time.ToString("HH:mm"); // Format hiển thị giờ
                connection.Open();
                string QueryTongTien_Gio = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE DATEPART(hour, NgayBanHang) = {time.Hour} AND DAY(NgayBanHang) = {now.Day} AND MONTH(NgayBanHang) = {now.Month} AND YEAR(NgayBanHang) = {now.Year}";
                SqlCommand cmdTongTien_Gio = new SqlCommand(QueryTongTien_Gio, connection);
                object result = cmdTongTien_Gio.ExecuteScalar();
                connection.Close();
                TK_ChartTheoGio.Series["TK_ChartTheoGio"].Points.AddXY(timeLabel, result);
            }

            //Theo Thứ

            DateTime startDate = now.AddDays(-(int)now.DayOfWeek + 1); // Ngày bắt đầu từ thứ 2
            DateTime endDate = startDate.AddDays(6); // Ngày kết thúc là thứ 7 (7 ngày sau)

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                string dayLabel = date.ToString("dddd"); // Lấy tên thứ
                connection.Open();
                string QueryTongTien_Thu = $"SELECT SUM(TongTien) as TongTienThang3 FROM HoaDon WHERE DATEPART(weekday, NgayBanHang) = {((int)date.DayOfWeek + 1)} AND DAY(NgayBanHang) = {date.Day} AND MONTH(NgayBanHang) = {date.Month} AND YEAR(NgayBanHang) = {date.Year}";
                SqlCommand cmdTongTien_Thu = new SqlCommand(QueryTongTien_Thu, connection);
                object result = cmdTongTien_Thu.ExecuteScalar();
                connection.Close();
                TK_ChartTheoThu.Series["TK_ChartTheoThu"].Points.AddXY(dayLabel, result);
            }

        }
        ////////////////////////////////////////Thông kê tồn kho \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void MenuTKTonKho_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyCongNhanVien);
            tabControl1.TabPages.Remove(TKdoanhthu);
            tabControl1.TabPages.Remove(TKTonKho);
            tabControl1.TabPages.Remove(BaoCao);

            tabControl1.TabPages.Add(TKTonKho);
            TKTK_LoadDataGridView();
            TKTK_ChucNang();
        }

        private void TKTK_LoadDataGridView()
        {
            string query = "SELECT [MaLoaiNguyenLieu], [TenNguyenLieu], [SoLuong] ,[DonVi] ,[HanSuDung] FROM [QuanLyCafe].[dbo].[NguyenLieu]";

            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();

            TK_dataGridViewTK.DataSource = dataTable;
            // Tính tổng cột "SoLuong"
            int sum = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                int quantity = Convert.ToInt32(row["SoLuong"]);
                sum += quantity;
            }

            TKTK_lbSoluong.Text = sum.ToString();

        }

        private void TKTK_ChucNang()
        {
            string query = "SELECT [MaLoaiNguyenLieu] FROM [QuanLyCafe].[dbo].[LoaiNguyenLieu]";

            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string maNguyenLieu = reader["MaLoaiNguyenLieu"].ToString();
                TKTK_cmb.Items.Add(maNguyenLieu);

            }
                TKTK_cmb.Items.Add("Tất cả");

            reader.Close();
            connection.Close();
        }

        private void TKTK_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MaNguyenLieu = TKTK_cmb.Text;

            if (MaNguyenLieu == "Tất cả")
            {
                string query = $"SELECT [MaLoaiNguyenLieu], [TenNguyenLieu], [SoLuong] ,[DonVi] ,[HanSuDung] FROM [QuanLyCafe].[dbo].[NguyenLieu] ";

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();

                TK_dataGridViewTK.DataSource = dataTable;

                // Tính tổng cột "SoLuong"
                int sum = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    int quantity = Convert.ToInt32(row["SoLuong"]);
                    sum += quantity;
                }

                TKTK_lbSoluong.Text = sum.ToString();
            }
            else
            {
                string query = $"SELECT [MaLoaiNguyenLieu], [TenNguyenLieu], [SoLuong] ,[DonVi] ,[HanSuDung] FROM [QuanLyCafe].[dbo].[NguyenLieu] where MaLoaiNguyenLieu = '{MaNguyenLieu}'";

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();

                TK_dataGridViewTK.DataSource = dataTable;

                // Tính tổng cột "SoLuong"
                int sum = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    int quantity = Convert.ToInt32(row["SoLuong"]);
                    sum += quantity;
                }

                TKTK_lbSoluong.Text = sum.ToString();
            }

           
        }
        //////////////////////////////////////////////BÁO cáo thu chi \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void MenuBaoCaoThuChi_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Remove(HoaDonBan);
            tabControl1.TabPages.Remove(TinhTrangDon);
            tabControl1.TabPages.Remove(LoaiMon);
            tabControl1.TabPages.Remove(MenuMon);
            tabControl1.TabPages.Remove(PhieuXuat);
            tabControl1.TabPages.Remove(PhieuNhap);
            tabControl1.TabPages.Remove(PhieuDatBan);
            tabControl1.TabPages.Remove(QlyKhachHang);
            tabControl1.TabPages.Remove(QlyNhaCC);
            tabControl1.TabPages.Remove(QlyNhanVien);
            tabControl1.TabPages.Remove(QlyCongNhanVien);
            tabControl1.TabPages.Remove(TKdoanhthu);
            tabControl1.TabPages.Remove(TKTonKho);
            tabControl1.TabPages.Remove(BaoCao);

            tabControl1.TabPages.Add(BaoCao);

            DateTime date = DateTime.Now;
            string Ngay = date.ToString("dd/MM/yyyy");
            BC_Ngay.Text = Ngay;

            TongKetThu();
            TongKetChi();
            TongKetThuChi();

            TongGD();
        }

        private void TongKetThuChi()
        {

            //Tổng Thu
            BC_lbTKtongtienthu.Text = (float.Parse(BC_lbTKtongtienmatthu.Text) + float.Parse(BC_lbTKtongtienCKthu.Text) + float.Parse(BC_lbTKtongtienThethu.Text)).ToString();
            //Tổng chi
            BC_lbTKtongtienchi.Text = (float.Parse(BC_lbTKtongtienmatchi.Text) + float.Parse(BC_lbTKtongtienCKchi.Text) + float.Parse(BC_lbTKtongtienThechi.Text)).ToString();

            //Tổng Thu chi tiền mặt 
            BC_lbTKtongtienmat.Text = (float.Parse(BC_lbTKtongtienmatthu.Text) + float.Parse(BC_lbTKtongtienmatchi.Text)).ToString();

            //Tổng thu chi chuyển khoản
            BC_lbTKtongtienCK.Text = (float.Parse(BC_lbTKtongtienCKthu.Text) + float.Parse(BC_lbTKtongtienCKchi.Text)).ToString();

            //Tổng thu chi thẻ
            BC_lbTKtongtienThe.Text = (float.Parse(BC_lbTKtongtienThechi.Text) + float.Parse(BC_lbTKtongtienThethu.Text)).ToString();

            //Tổng thực thu
            BC_lbTKtongtien.Text = (float.Parse(BC_lbTKtongtienchi.Text) + float.Parse(BC_lbTKtongtienthu.Text)).ToString();


        }

        private void TongKetThu()
        {
            DateTime dateTime = DateTime.Now;
            string HomNay = dateTime.ToString("yyyy/MM/dd");


            // Tạo câu truy vấn SQL
            string query1 = $"SELECT SUM(TongTien) FROM HoaDon WHERE PhuongThucThanhToan = N'Tiền Mặt' AND CONVERT(date, NgayBanHang) = '{HomNay}'";

            // Thực hiện truy vấn và lấy tổng
            connection.Open();
            SqlCommand command1 = new SqlCommand(query1, connection);
            object result1 = command1.ExecuteScalar();
            connection.Close();

            // Kiểm tra kết quả và hiển thị tổng
            if (result1 != null && result1 != DBNull.Value)
            {
                decimal total = Convert.ToDecimal(result1);
                // Hiển thị tổng
                BC_lbTKtongtienmatthu.Text = total.ToString();
            }
            else
            {
                BC_lbTKtongtienmatthu.Text = "0";

            }

            // Tạo câu truy vấn SQL
            string query2 = $"SELECT SUM(TongTien) FROM HoaDon WHERE PhuongThucThanhToan = N'Chuyển Khoản' AND CONVERT(date, NgayBanHang) = '{HomNay}'";

            // Thực hiện truy vấn và lấy tổng
            connection.Open();
            SqlCommand command2 = new SqlCommand(query2, connection);
            object result2 = command2.ExecuteScalar();
            connection.Close();

            // Kiểm tra kết quả và hiển thị tổng
            if (result2 != null && result2 != DBNull.Value)
            {
                decimal total = Convert.ToDecimal(result2);
                // Hiển thị tổng
                BC_lbTKtongtienCKthu.Text = total.ToString();
            }
            else
            {
                BC_lbTKtongtienCKthu.Text = "0";

            }

        }

        private void TongKetChi()
        {
            DateTime dateTime = DateTime.Now;
            string HomNay = dateTime.ToString("yyyy/MM/dd");


            // Tạo câu truy vấn SQL
            string query1 = $"SELECT SUM(TongTien) FROM PhieuNhapKho WHERE  CONVERT(date, NgayNhap) = '{HomNay}'";

            // Thực hiện truy vấn và lấy tổng
            connection.Open();
            SqlCommand command1 = new SqlCommand(query1, connection);
            object result1 = command1.ExecuteScalar();
            connection.Close();

            // Kiểm tra kết quả và hiển thị tổng
            if (result1 != null && result1 != DBNull.Value)
            {
                decimal total = Convert.ToDecimal(result1);
                // Đảo ngược dấu của giá trị
                decimal negativeTotal = -total;
                // Hiển thị tổng âm
                BC_lbTKtongtienmatchi.Text = negativeTotal.ToString();
            }
            else
            {
                BC_lbTKtongtienmatchi.Text = "0";

            }
        }

        private void TongGD()
        {
            DateTime dateTime = DateTime.Now;
            string HomNay = dateTime.ToString("yyyy/MM/dd");


            // Tạo câu truy vấn SQL
            string query1 = $"SELECT COUNT(*) FROM HoaDon WHERE  CONVERT(date, NgayBanHang) = '{HomNay}'";

            // Thực hiện truy vấn và lấy tổng
            connection.Open();
            SqlCommand command1 = new SqlCommand(query1, connection);
            object result1 = command1.ExecuteScalar();
            connection.Close();

            // Kiểm tra kết quả và hiển thị tổng
            if (result1 != null && result1 != DBNull.Value)
            {
                decimal total = Convert.ToDecimal(result1);
                // Hiển thị tổng
                BC_lbSLHoaDon.Text = total.ToString();
            }
            else
            {
                BC_lbSLHoaDon.Text = "0";

            }

            // Tạo câu truy vấn SQL
            string query2 = $"SELECT COUNT(*) FROM HoaDon WHERE  PhuongThucThanhToan = N'Tiền Mặt' And CONVERT(date, NgayBanHang) = '{HomNay}'";

            // Thực hiện truy vấn và lấy tổng
            connection.Open();
            SqlCommand command2 = new SqlCommand(query2, connection);
            object result2 = command2.ExecuteScalar();
            connection.Close();

            // Kiểm tra kết quả và hiển thị tổng
            if (result2 != null && result2 != DBNull.Value)
            {
                decimal total = Convert.ToDecimal(result2);
                // Hiển thị tổng
                BC_lbSLHoaDonTienMat.Text = total.ToString();
            }
            else
            {
                BC_lbSLHoaDonTienMat.Text = "0";

            }

            // Tạo câu truy vấn SQL
            string query3 = $"SELECT COUNT(*) FROM HoaDon WHERE  PhuongThucThanhToan = N'Chuyển Khoản' And CONVERT(date, NgayBanHang) = '{HomNay}'";

            // Thực hiện truy vấn và lấy tổng
            connection.Open();
            SqlCommand command3 = new SqlCommand(query3, connection);
            object result3 = command3.ExecuteScalar();
            connection.Close();

            // Kiểm tra kết quả và hiển thị tổng
            if (result3 != null && result3 != DBNull.Value)
            {
                decimal total = Convert.ToDecimal(result3);
                // Hiển thị tổng
                BC_lbSLHoaDonCK.Text = total.ToString();
            }
            else
            {
                BC_lbSLHoaDonCK.Text = "0";

            }



            BC_lbTongGD.Text = (float.Parse(BC_lbSLHoaDon.Text) + float.Parse(BC_lbSLDatHang.Text)).ToString();
            BC_lbTongGDTienMat.Text = (float.Parse(BC_lbSLDatHangTienMat.Text) + float.Parse(BC_lbSLHoaDonTienMat.Text)).ToString();
            BC_lbTongGDCK.Text = (float.Parse(BC_lbSLDatHangCK.Text) + float.Parse(BC_lbSLHoaDonCK.Text)).ToString();


        }

        private void MenuQlyLuongNV_Click(object sender, EventArgs e)
        {
            QLyLuongNvien luong = new QLyLuongNvien();
            luong.ShowDialog();
        }
    }
}


