using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HeThongQuanLyCaPhe
{
    public partial class HoaDonTamContext : DbContext
    {
        public HoaDonTamContext()
            : base("name=HoaDonTamContext")
        {
        }

        public virtual DbSet<BanNgoi> BanNgois { get; set; }
        public virtual DbSet<CaLamViec> CaLamViecs { get; set; }
        public virtual DbSet<CongThuc> CongThucs { get; set; }
        public virtual DbSet<ChamCong> ChamCongs { get; set; }
        public virtual DbSet<ChiTietCongThuc> ChiTietCongThucs { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTietHoaDonTam> ChiTietHoaDonTams { get; set; }
        public virtual DbSet<ChiTietPhieuDatBan> ChiTietPhieuDatBans { get; set; }
        public virtual DbSet<ChiTietPhieuNhapKho> ChiTietPhieuNhapKhoes { get; set; }
        public virtual DbSet<ChiTietPhieuXuatKho> ChiTietPhieuXuatKhoes { get; set; }
        public virtual DbSet<ChiTietTrangThaiSanPham> ChiTietTrangThaiSanPhams { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<DatBan> DatBans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<HoaDonTam> HoaDonTams { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhoHang> KhoHangs { get; set; }
        public virtual DbSet<KhuVucBan> KhuVucBans { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<LichSuQuanLyKho> LichSuQuanLyKhoes { get; set; }
        public virtual DbSet<LoaiNguyenLieu> LoaiNguyenLieux { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<LuongNvien> LuongNviens { get; set; }
        public virtual DbSet<NguyenLieu> NguyenLieux { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuNhapKho> PhieuNhapKhoes { get; set; }
        public virtual DbSet<PhieuXuatKho> PhieuXuatKhoes { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TrangThaiSanPham> TrangThaiSanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanNgoi>()
                .Property(e => e.MaBan)
                .IsUnicode(false);

            modelBuilder.Entity<BanNgoi>()
                .Property(e => e.MaKhu)
                .IsUnicode(false);

            modelBuilder.Entity<BanNgoi>()
                .HasMany(e => e.ChiTietPhieuDatBans)
                .WithRequired(e => e.BanNgoi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaLamViec>()
                .Property(e => e.MaCaLamViec)
                .IsUnicode(false);

            modelBuilder.Entity<CaLamViec>()
                .Property(e => e.TenCaLamViec)
                .IsUnicode(false);

            modelBuilder.Entity<CongThuc>()
                .Property(e => e.MaCongThucSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<CongThuc>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<CongThuc>()
                .HasMany(e => e.ChiTietCongThucs)
                .WithRequired(e => e.CongThuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChamCong>()
                .Property(e => e.MaChamCong)
                .IsUnicode(false);

            modelBuilder.Entity<ChamCong>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<ChamCong>()
                .Property(e => e.MaCaLamViec)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietCongThuc>()
                .Property(e => e.MaNguyenLieu)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietCongThuc>()
                .Property(e => e.MaCongThucSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietCongThuc>()
                .Property(e => e.DungTich)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDonTam>()
                .Property(e => e.MaHoaDonTam)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDonTam>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuDatBan>()
                .Property(e => e.MaDatBan)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuDatBan>()
                .Property(e => e.MaBan)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuDatBan>()
                .Property(e => e.MaKhu)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuNhapKho>()
                .Property(e => e.MaPhieuNhapKho)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuNhapKho>()
                .Property(e => e.MaNguyenLieu)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuNhapKho>()
                .Property(e => e.DonVi)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuXuatKho>()
                .Property(e => e.MaPhieuXuatKho)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuXuatKho>()
                .Property(e => e.MaNguyenLieu)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietTrangThaiSanPham>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietTrangThaiSanPham>()
                .Property(e => e.MaTrangThaiThe)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietTrangThaiSanPham>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietTrangThaiSanPham>()
                .Property(e => e.Ghichu)
                .IsUnicode(false);

            modelBuilder.Entity<ChucVu>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<DatBan>()
                .Property(e => e.MaDatBan)
                .IsUnicode(false);

            modelBuilder.Entity<DatBan>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<DatBan>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<DatBan>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<DatBan>()
                .HasMany(e => e.ChiTietPhieuDatBans)
                .WithRequired(e => e.DatBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaKhuyenMai)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.SoThe)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDonTam>()
                .Property(e => e.MaHoaDonTam)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTam>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTam>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTam>()
                .Property(e => e.MaKhuyenMai)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTam>()
                .Property(e => e.SoThe)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTam>()
                .Property(e => e.PhuongThucThanhToan)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTam>()
                .HasMany(e => e.ChiTietHoaDonTams)
                .WithRequired(e => e.HoaDonTam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhoHang>()
                .Property(e => e.MaKhoHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhoHang>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<KhoHang>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<KhuVucBan>()
                .Property(e => e.MaKhu)
                .IsUnicode(false);

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.MaKhuyenMai)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuQuanLyKho>()
                .Property(e => e.MaKhoHang)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuQuanLyKho>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiNguyenLieu>()
                .Property(e => e.MaLoaiNguyenLieu)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiNguyenLieu>()
                .Property(e => e.MaKhoHang)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiSanPham>()
                .Property(e => e.MaLoaiSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiSanPham>()
                .Property(e => e.MaKhoHang)
                .IsUnicode(false);

            modelBuilder.Entity<LuongNvien>()
                .Property(e => e.MaLuong)
                .IsUnicode(false);

            modelBuilder.Entity<LuongNvien>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NguyenLieu>()
                .Property(e => e.MaNguyenLieu)
                .IsUnicode(false);

            modelBuilder.Entity<NguyenLieu>()
                .Property(e => e.MaLoaiNguyenLieu)
                .IsUnicode(false);

            modelBuilder.Entity<NguyenLieu>()
                .Property(e => e.DonVi)
                .IsUnicode(false);

            modelBuilder.Entity<NguyenLieu>()
                .HasMany(e => e.ChiTietCongThucs)
                .WithRequired(e => e.NguyenLieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguyenLieu>()
                .HasMany(e => e.ChiTietPhieuNhapKhoes)
                .WithRequired(e => e.NguyenLieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguyenLieu>()
                .HasMany(e => e.ChiTietPhieuXuatKhoes)
                .WithRequired(e => e.NguyenLieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.MaNhaCungCap)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuXuatKhoes)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.MaNhanVien);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuXuatKhoes1)
                .WithOptional(e => e.NhanVien1)
                .HasForeignKey(e => e.MaNhanVien);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaPhieuNhapKho)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaKhoHang)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaNhaCungCap)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .HasMany(e => e.ChiTietPhieuNhapKhoes)
                .WithRequired(e => e.PhieuNhapKho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuXuatKho>()
                .Property(e => e.MaPhieuXuatKho)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuXuatKho>()
                .Property(e => e.MaKhoHang)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuXuatKho>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuXuatKho>()
                .HasMany(e => e.ChiTietPhieuXuatKhoes)
                .WithRequired(e => e.PhieuXuatKho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaLoaiSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDonTams)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietTrangThaiSanPhams)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TaiKhoan1)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<TrangThaiSanPham>()
                .Property(e => e.MaTrangThaiThe)
                .IsUnicode(false);

            modelBuilder.Entity<TrangThaiSanPham>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<TrangThaiSanPham>()
                .HasMany(e => e.ChiTietTrangThaiSanPhams)
                .WithRequired(e => e.TrangThaiSanPham)
                .WillCascadeOnDelete(false);
        }
    }
}
