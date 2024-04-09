using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongQuanLyCaPhe
{
    public partial class RpHoaDon : Form
    {
        private const string connectionString = @"Data Source=KHANGNGUYEN;Initial Catalog=QuanLyCafe;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(connectionString);
        public RpHoaDon()
        {
            InitializeComponent();
        }
        public TrangChu TrangChu { get; set; }
        private void RpHoaDon_Load(object sender, EventArgs e)
        {
            // Hóa đơn 
            List<HoaDonTam> listHoaDonReport = new List<HoaDonTam>();
            using (HoaDonTamContext hoaDonContext = new HoaDonTamContext())
            {
                List<HoaDonTam> listHoaDon = hoaDonContext.HoaDonTams.ToList();
                foreach (HoaDonTam hoaDon in listHoaDon)
                {
                    HoaDonTam tempHoaDon = new HoaDonTam();
                    tempHoaDon.MaHoaDonTam = hoaDon.MaHoaDonTam;    
                    tempHoaDon.MaNhanVien = hoaDon.MaNhanVien;
                    tempHoaDon.SoThe = hoaDon.SoThe;
                    tempHoaDon.NgayBanHang = hoaDon.NgayBanHang;
                    KhuyenMai khuyenMai = hoaDonContext.KhuyenMais.FirstOrDefault(x => x.MaKhuyenMai ==  hoaDon.MaKhuyenMai);
                    if (khuyenMai != null )
                    {
                        tempHoaDon.MaKhuyenMai = khuyenMai.GiaTriKhuyenMai.ToString();
                    }    
                    tempHoaDon.PhuongThucThanhToan = hoaDon.PhuongThucThanhToan != null
                    ? hoaDon.PhuongThucThanhToan.ToString(): string.Empty;
                    tempHoaDon.TongTien = hoaDon.TongTien;
                    tempHoaDon.TienKhach = hoaDon.TienKhach;
                    tempHoaDon.TienThua = hoaDon.TienThua;
                    listHoaDonReport.Add(tempHoaDon);
                    MessageBox.Show(tempHoaDon.PhuongThucThanhToan);
                }
            }
            // Chi tiết hóa đơn 
            List<ChiTietHoaDonTam> listChiTietHDReport = new List<ChiTietHoaDonTam>();
            using (HoaDonTamContext chiTietHoaDonContext = new HoaDonTamContext())
            {
                List<ChiTietHoaDonTam> listChiTietHD = chiTietHoaDonContext.ChiTietHoaDonTams.ToList();
                foreach (ChiTietHoaDonTam chiTietHD in listChiTietHD)
                {
                    ChiTietHoaDonTam temp = new ChiTietHoaDonTam();
                    temp.DonGia = chiTietHD.DonGia;
                    temp.SoLuong = chiTietHD.SoLuong;
                    temp.ThanhTien = chiTietHD.ThanhTien;

                    // Lấy thông tin TenSanPham từ bảng SanPham
                    SanPham sanPham = chiTietHoaDonContext.SanPhams.FirstOrDefault(sp => sp.MaSanPham == chiTietHD.MaSanPham);
                    if (sanPham != null)
                    {
                        temp.MaSanPham = sanPham.TenSanPham;
                    }

                    listChiTietHDReport.Add(temp);
                }
            }


            reportViewer1.LocalReport.ReportPath = "rptHoaDon.rdlc";
            var resourceHD = new ReportDataSource("HoaDonTamDataSet", listHoaDonReport);
            var resourceCTHD = new ReportDataSource("ChiTietHoaDonTamDataSet", listChiTietHDReport);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(resourceHD);
            reportViewer1.LocalReport.DataSources.Add(resourceCTHD);
            this.reportViewer1.RefreshReport();
        }

        private void RpHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteChiTietHoaDonTamQuery = "DELETE FROM ChiTietHoaDonTam";
                using (SqlCommand command = new SqlCommand(deleteChiTietHoaDonTamQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                string deleteHoaDonTamQuery = "DELETE FROM HoaDonTam";
                using (SqlCommand command = new SqlCommand(deleteHoaDonTamQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
