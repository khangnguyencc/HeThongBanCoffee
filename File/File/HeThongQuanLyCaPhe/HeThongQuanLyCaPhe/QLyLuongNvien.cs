using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongQuanLyCaPhe
{
    public partial class QLyLuongNvien : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);
        public QLyLuongNvien()
        {
            InitializeComponent();
        }
        private void QLyLuongNvien_Load(object sender, EventArgs e)
        {
            LoadDuLieuChamCong();
            LoadDuLieuLuongNvien();
        }
        private void LoadDuLieuChamCong()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaChamCong, MaNhanVien, NgayLamViec, GioBatDau, GioKetThuc, TongGioLam FROM ChamCong";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 10);
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
        private void LoadDuLieuLuongNvien()
        {
            string query = "SELECT LuongNvien.MaNhanVien, NhanVien.TenNhanVien, NhanVien.MaChucVu, ChucVu.TenChucVu, LuongNvien.TongGioLam, LuongNvien.TongTienLuong, NhanVien.Email " +
                           "FROM LuongNvien " +
                           "INNER JOIN NhanVien ON LuongNvien.MaNhanVien = NhanVien.MaNhanVien " +
                           "INNER JOIN ChucVu ON NhanVien.MaChucVu = ChucVu.MaChucVu";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                dataGridView2.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TongGioLam")
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    e.Value = value + " giờ";
                    e.FormattingApplied = true;
                }
            }
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "TongGioLam")
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    e.Value = value + " giờ";
                    e.FormattingApplied = true;
                }
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "TongTienLuong")
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    e.Value = value + " VND";
                    e.FormattingApplied = true;
                }
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "NgayVaoLam")
            {
                if (e.Value != null && e.Value is DateTime)
                {
                    string formattedValue = ((DateTime)e.Value).ToString("dd/MM/yyyy");
                    e.Value = formattedValue;
                    e.FormattingApplied = true;
                }
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "TenChucVu")
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    if (value.StartsWith("Nhân Viên "))
                    {
                        value = value.Substring(10); 
                    }
                    e.Value = value;
                    e.FormattingApplied = true;
                }
            }
        }
        
        private void btnGuiLuong_Click(object sender, EventArgs e)
        {
            try
            {
                string senderEmail = "nguyenkhang25506@gmail.com";
                string password = "ktqb cbkv fuwc lqxe";
                string subject = "THÔNG TIN LƯƠNG THƯỞNG NHÂN VIÊN";

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.IsNewRow) // Bỏ qua dòng mới (nếu có)
                    {
                        continue;
                    }

                    bool hasData = false;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            hasData = true;
                            break;
                        }
                    }

                    if (hasData)
                    {
                        string recipientEmail = row.Cells["Email"].Value != null ? row.Cells["Email"].Value.ToString() : string.Empty;

                        string bodyno = (row.Cells["TenNhanVien"].Value?.ToString() ?? "") + " (" + (row.Cells["MaNhanVien"].Value?.ToString() ?? "") + ")";
                        string body = (row.Cells["TongGioLam"].Value?.ToString() ?? "") + " giờ";
                        string body2 = (row.Cells["TongTienLuong"].Value?.ToString() ?? "") + " VND";

                        string emailBody = "Thông tin nhân viên: " + bodyno + "\nTổng giờ: " + body + "\nTổng lương: " + body2;

                        MailMessage mail = new MailMessage();
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                        mail.From = new MailAddress(senderEmail);
                        mail.To.Add(recipientEmail);
                        mail.Subject = subject;
                        mail.Body = emailBody;

                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(senderEmail, password);

                        smtpClient.Send(mail);
                    }
                }

                MessageBox.Show("Đã gửi thông tin lương thưởng cho nhân viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message).ToString();
            }

        }
      
    }
}
