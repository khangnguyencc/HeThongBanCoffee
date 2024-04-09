using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace HeThongQuanLyCaPhe
{
    public partial class DangNhap : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);

        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnHuyDN_Click(object sender, EventArgs e)
        {

        }

        private int soLanSaiMatKhau = 0; // Biến đếm số lần nhập sai mật khẩu
        private DateTime thoiGianKhoa = DateTime.MinValue; // Thời gian bắt đầu khóa tài khoản
        private Timer timer;

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra xem tài khoản có bị khóa không
                    if (thoiGianKhoa != DateTime.MinValue && DateTime.Now < thoiGianKhoa.AddMinutes(5))
                    {
                        TimeSpan remainingTime = thoiGianKhoa.AddMinutes(5) - DateTime.Now;
                        string remainingTimeString = string.Format("{0}:{1}:{2}", remainingTime.Hours.ToString("00"), remainingTime.Minutes.ToString("00"), remainingTime.Seconds.ToString("00"));

                        lblTgianKhoa.Text = $"Tài khoản đã bị khóa. Thời gian còn lại: {remainingTimeString}";
                        lblTgianKhoa.Visible = true; // Hiển thị Label thời gian khóa

                        MessageBox.Show("Tài khoản đã bị khóa. Vui lòng thử lại sau 5 phút.");
                        return;
                    }
                    else
                    {
                        lblTgianKhoa.Text = ""; // Xóa hiển thị thời gian khóa
                        lblTgianKhoa.Visible = false; // Ẩn Label thời gian khóa
                    }

                    string queryTrangThai = "SELECT TrangThai FROM TaiKhoan WHERE TaiKhoan = @TaiKhoan";
                    using (SqlCommand commandTrangThai = new SqlCommand(queryTrangThai, connection))
                    {
                        commandTrangThai.Parameters.AddWithValue("@TaiKhoan", txtDangNhap.Text);
                        object trangThaiResult = commandTrangThai.ExecuteScalar();

                        bool trangThai;
                        if (trangThaiResult != null && bool.TryParse(trangThaiResult.ToString(), out trangThai))
                        {
                            if (trangThai)
                            {
                                soLanSaiMatKhau = 0;

                                string queryMaNhanVien = "SELECT MaNhanVien FROM TaiKhoan WHERE TaiKhoan = @TaiKhoan";
                                using (SqlCommand commandMaNhanVien = new SqlCommand(queryMaNhanVien, connection))
                                {
                                    commandMaNhanVien.Parameters.AddWithValue("@TaiKhoan", txtDangNhap.Text);
                                    object maNhanVienResult = commandMaNhanVien.ExecuteScalar();

                                    if (maNhanVienResult != null)
                                    {
                                        string dNhapManv = maNhanVienResult.ToString();
                                        HSDN_lb1.Text = dNhapManv;
                                        MessageBox.Show("Đăng nhập thành công.");
                                        TrangChu trangChuform = new TrangChu();
                                        trangChuform.DN_txtManvText = dNhapManv;
                                        trangChuform.Show();
                                        this.Hide();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ Quản Trị Viên.");
                            }
                        }
                        else
                        {
                            soLanSaiMatKhau++;

                            if (soLanSaiMatKhau >= 5)
                            {
                                thoiGianKhoa = DateTime.Now;
                                StartTimer();
                                lblTgianKhoa.Text = "Tài khoản đã bị khóa. Thời gian còn lại: 05:00:00";
                                lblTgianKhoa.Visible = true;
                                MessageBox.Show("Bạn đã nhập sai mật khẩu quá 5 lần. Tài khoản đã bị khóa trong 5 phút.");
                            }
                            else if (soLanSaiMatKhau == 3)
                            {
                                DialogResult result = MessageBox.Show("Bạn đã nhập sai mật khẩu 3 lần. Bạn còn 2 lần nhập sai trước khi tài khoản bị khóa trong 5 phút. Tiếp tục nhập?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.No)
                                {
                                    thoiGianKhoa = DateTime.Now;
                                    StartTimer();
                                    lblTgianKhoa.Text = "Tài khoản đã bị khóa. Thời gian còn lại: 05:00:00";
                                    lblTgianKhoa.Visible = true;
                                    MessageBox.Show("Tài khoản đã bị khóa trong 5 phút.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (thoiGianKhoa != DateTime.MinValue && DateTime.Now < thoiGianKhoa.AddMinutes(5))
            {
                TimeSpan remainingTime = thoiGianKhoa.AddMinutes(5) - DateTime.Now;
                string remainingTimeString = string.Format("{0}:{1}:{2}", remainingTime.Hours.ToString("00"), remainingTime.Minutes.ToString("00"), remainingTime.Seconds.ToString("00"));

                lblTgianKhoa.Text = $"Tài khoản đã bị khóa. Thời gian còn lại: {remainingTimeString}";
            }
            else
            {
                lblTgianKhoa.Text = ""; 
                lblTgianKhoa.Visible = false; 
                timer.Stop();
            }
        }
        public string EmailQMK;
        public string MaXacThuc;
        private int countdownSeconds = 60;
        private Timer countdownTimer;
        private void lblQMK_Click(object sender, EventArgs e)
        {
            pnQMK.Visible = true;
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            pnQMK.Visible = false;
            txtEmailQMK.Text = string.Empty;
            txtDangNhap.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
        }
        private void btnFindQMK_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string email = txtEmailQMK.Text;
                string query = $"SELECT COUNT(*) FROM NhanVien WHERE Email = '{email}'";
                SqlCommand command = new SqlCommand(query, connection);

                int count = Convert.ToInt32(command.ExecuteScalar());
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng nhập email!");
                }
                else if (count == 0)
                {
                    label4.Text = "Không tìm thấy Email";
                    label4.ForeColor = Color.Red;
                    MessageBox.Show("Email không tồn tại!");
                    label4.Text = "Vui lòng nhập email để tìm kiếm tài khoản của bạn";
                    label4.ForeColor = Color.Blue;
                }
                else
                {
                    MessageBox.Show("Waiting...!");
                    pnQMK.Visible = false;
                    pnMXT.Visible = true;
                    EmailQMK = email;
                }
            }
        }
        private void btnBack2_Click(object sender, EventArgs e)
        {
            pnQMK.Visible = true;
            pnMXT.Visible = false;
            txtMXT.Text = string.Empty;
            btnLayMa.Text = "";
        }
        private string MaXacThucRandom(int length)
        {
            Random random = new Random();
            const string chars = "cTvLJzXp4Z48H123r19D1uY12M5Asv63Bw27gPAGVf1Q35y05q1N35Oo6WbVl2G12n2IjRiK5kFmU1xSdTeEhCa6cTvLJzXp4Z48H123r19D1uY12M5Asv63Bw27gPAGVf1Q35y05q1N35Oo6WbVl2G12n2IjRiK5kFmU1xSdTeEhCa6";
            char[] captchaChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                captchaChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(captchaChars);
        }

        private void btnXacNhanDMK_Click(object sender, EventArgs e)
        {
            string MKa = txtMKa.Text;
            string MKb = txtMKb.Text;
            string maNV = "";
            string tenDN = "";

            if (MKb == MKa)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT MaNhanVien FROM NhanVien WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", EmailQMK);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            maNV = reader["MaNhanVien"].ToString();
                        }
                        connection.Close();
                    }

                    connection.Open();
                    string queryTenDN = $"SELECT TaiKhoan FROM TaiKhoan WHERE MaNhanVien = '{maNV}'";
                    using (SqlCommand commandTenDN = new SqlCommand(queryTenDN, connection))
                    {
                        SqlDataReader readerTenDN = commandTenDN.ExecuteReader();
                        if (readerTenDN.Read())
                        {
                            tenDN = readerTenDN["TaiKhoan"].ToString();
                        }
                        connection.Close();
                    }


                    connection.Open();
                    string Query = $"Update TaiKhoan Set MatKhau = '{MKa}' where TaiKhoan = '{tenDN}'";
                    SqlCommand Command = new SqlCommand(Query, connection);

                    Command.ExecuteNonQuery();
                    MessageBox.Show("Mật khẩu của bạn đã được thay đổi");


                    pnDMK.Visible = false;
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại Mật khẩu");
            }
        }

        private void btnBack3_Click(object sender, EventArgs e)
        {
            pnDMK.Visible = false;
            pnMXT.Visible = true;
            txtMKa.Text = string.Empty;
            txtMKb.Text = string.Empty;
        }

        private void btnLayMa_Click(object sender, EventArgs e)
        {
            btnLayMa.Enabled = false;
            txtMXT.Enabled = true;
            try
            {
                string senderEmail = "nguyenkhang25506@gmail.com";
                string password = "ktqb cbkv fuwc lqxe";
                string recipientEmail = EmailQMK;
                string subject = " HỆ THỐNG QUẢN LÍ COFFEE ";
                string body = MaXacThucRandom(8);
                MaXacThuc = body;

                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.Body = "Mã xác thực của bạn là :" + body;

                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(senderEmail, password);

                smtpClient.Send(mail);

                MessageBox.Show("Vui lòng kiểm tra Email để lấy mã xác thực ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while sending the email: " + ex.ToString());
            }
            countdownSeconds = 60;
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();

            btnLayMa.Text = countdownSeconds + "(s)";
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownSeconds--;

            if (countdownSeconds > 0)
            {
                btnLayMa.Text = countdownSeconds + "(s)";
            }
            else
            {
                countdownTimer.Stop();
                btnLayMa.Text = "0(s)";
            }
        }

        private void btnXNMa_Click(object sender, EventArgs e)
        {
            string MaXacThuca = txtMXT.Text;
            string MaXachucCheck = MaXacThuc;

            if (MaXachucCheck == MaXacThuca)
            {
                pnDMK.Visible = true;
                pnMXT.Visible = false;
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại Mã Xác Thực");
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void UnHidePass_CheckedChanged(object sender, EventArgs e)
        {
           
            if (UnHidePass.Checked)
            {
                // Nếu được chọn, hiển thị mật khẩu
                txtMatKhau.PasswordChar = '\0'; // Sử dụng ký tự null để hiển thị ký tự gốc
            }
            else
            {
                // Nếu không được chọn, ẩn mật khẩu
                txtMatKhau.PasswordChar = '•'; // Sử dụng dấu "•" để ẩn ký tự mật khẩu
            }
        }
    }
}
