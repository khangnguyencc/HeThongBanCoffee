USE [master]
GO
/****** Object:  Database [QLYCOFFEE]    Script Date: 15/03/2024 11:22:26 CH ******/
CREATE DATABASE [QLYCOFFEE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLYCOFFEE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLYCOFFEE.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLYCOFFEE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLYCOFFEE_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLYCOFFEE] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLYCOFFEE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLYCOFFEE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLYCOFFEE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLYCOFFEE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLYCOFFEE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLYCOFFEE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLYCOFFEE] SET  MULTI_USER 
GO
ALTER DATABASE [QLYCOFFEE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLYCOFFEE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLYCOFFEE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLYCOFFEE] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLYCOFFEE] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QLYCOFFEE]
GO
/****** Object:  Table [dbo].[CaLamViec]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CaLamViec](
	[MaCaLamViec] [varchar](10) NOT NULL,
	[TenCaLamViec] [varchar](100) NULL,
	[GioBatDau] [time](7) NULL,
	[GioKetThuc] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCaLamViec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChamCong]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChamCong](
	[MaChamCong] [varchar](20) NOT NULL,
	[MaNhanVien] [varchar](20) NULL,
	[MaCaLamViec] [varchar](10) NULL,
	[NgayLamViec] [date] NULL,
	[GioBatDau] [time](7) NULL,
	[GioKetThuc] [time](7) NULL,
	[TongGioLam] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChamCong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietCongThuc]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietCongThuc](
	[MaNguyenLieu] [varchar](20) NOT NULL,
	[MaCongThucSanPham] [varchar](20) NOT NULL,
	[DungTich] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNguyenLieu] ASC,
	[MaCongThucSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaHoaDon] [varchar](20) NOT NULL,
	[MaSanPham] [varchar](20) NOT NULL,
	[DonGia] [float] NULL,
	[SoLuong] [int] NULL,
	[ThanhTien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietPhieuNhapKho]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietPhieuNhapKho](
	[MaPhieuNhapKho] [varchar](20) NOT NULL,
	[MaNguyenLieu] [varchar](20) NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [float] NULL,
	[DonVi] [varchar](50) NULL,
	[ThanhTien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhieuNhapKho] ASC,
	[MaNguyenLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietPhieuXuatKho]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietPhieuXuatKho](
	[MaPhieuXuatKho] [varchar](20) NOT NULL,
	[MaNguyenLieu] [varchar](20) NOT NULL,
	[SoLuong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhieuXuatKho] ASC,
	[MaNguyenLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietTrangThaiSanPham]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietTrangThaiSanPham](
	[HinhAnh] [varchar](255) NULL,
	[MaTrangThaiThe] [varchar](20) NOT NULL,
	[MaSanPham] [varchar](20) NOT NULL,
	[SoLuong] [int] NULL,
	[Ghichu] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTrangThaiThe] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChucVu](
	[MaChucVu] [varchar](10) NOT NULL,
	[TenChucVu] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CongThuc]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CongThuc](
	[MaCongThucSanPham] [varchar](20) NOT NULL,
	[MaSanPham] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCongThucSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DatBan]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DatBan](
	[MaDatBan] [varchar](20) NOT NULL,
	[MaNhanVien] [varchar](20) NULL,
	[NgayDatBan] [date] NULL,
	[MaKhachHang] [varchar](20) NULL,
	[ThoiGianNhanBan] [datetime] NULL,
	[SoLuong] [int] NULL,
	[GhiChu] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDatBan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [varchar](20) NOT NULL,
	[MaKhachHang] [varchar](20) NULL,
	[MaNhanVien] [varchar](20) NULL,
	[MaKhuyenMai] [varchar](20) NULL,
	[SoThe] [varchar](20) NULL,
	[NgayBanHang] [datetime] NULL,
	[PhuongThucThanhToan] [varchar](50) NULL,
	[TongTien] [float] NULL,
	[TienKhach] [float] NULL,
	[TienThua] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKhachHang] [varchar](20) NOT NULL,
	[SDT] [varchar](20) NULL,
	[TenKhachHang] [varchar](100) NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [varchar](255) NULL,
	[DiemTichLuy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhoHang]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhoHang](
	[MaKhoHang] [varchar](20) NOT NULL,
	[MaNhanVien] [varchar](20) NULL,
	[NgayBatDau] [date] NULL,
	[GhiChu] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhoHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhuyenMai]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhuyenMai](
	[MaKhuyenMai] [varchar](20) NOT NULL,
	[GiaTriKhuyenMai] [float] NULL,
	[SoLuong] [int] NULL,
	[NgayBatDau] [datetime] NULL,
	[NgayKetThuc] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhuyenMai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LichSuQuanLyKho]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LichSuQuanLyKho](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[MaKhoHang] [varchar](20) NULL,
	[MaNhanVien] [varchar](20) NULL,
	[NgayBatDau] [date] NULL,
	[NgayKetThuc] [date] NULL,
	[TrangThai] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoaiNguyenLieu]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoaiNguyenLieu](
	[MaLoaiNguyenLieu] [varchar](20) NOT NULL,
	[MaKhoHang] [varchar](20) NULL,
	[TenLoaiNguyenLieu] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoaiNguyenLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoaiSanPham]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoaiSanPham](
	[MaLoaiSanPham] [varchar](20) NOT NULL,
	[MaKhoHang] [varchar](20) NULL,
	[TenLoaiSanPham] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoaiSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LuongNvien]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LuongNvien](
	[MaLuong] [varchar](20) NOT NULL,
	[MaNhanVien] [varchar](20) NULL,
	[TongGioLam] [float] NULL,
	[TongTienLuong] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NguyenLieu]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NguyenLieu](
	[MaNguyenLieu] [varchar](20) NOT NULL,
	[MaLoaiNguyenLieu] [varchar](20) NULL,
	[TenNguyenLieu] [varchar](100) NULL,
	[HanSuDung] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNguyenLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNhaCungCap] [varchar](20) NOT NULL,
	[TenNhaCungCap] [varchar](100) NULL,
	[SDT] [varchar](20) NULL,
	[Email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhaCungCap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [varchar](20) NOT NULL,
	[MaChucVu] [varchar](10) NULL,
	[TenNhanVien] [varchar](100) NULL,
	[SDT] [varchar](20) NULL,
	[GioiTinh] [varchar](5) NULL,
	[DiaChi] [varchar](255) NULL,
	[NgayVaoLam] [datetime] NULL,
	[Email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PhieuNhapKho]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PhieuNhapKho](
	[MaPhieuNhapKho] [varchar](20) NOT NULL,
	[MaKhoHang] [varchar](20) NULL,
	[MaNhaCungCap] [varchar](20) NULL,
	[NgayNhap] [datetime] NULL,
	[TongTien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhieuNhapKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PhieuXuatKho]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PhieuXuatKho](
	[MaPhieuXuatKho] [varchar](20) NOT NULL,
	[MaKhoHang] [varchar](20) NULL,
	[MaNhanVien] [varchar](20) NULL,
	[NgayXuat] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhieuXuatKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [varchar](20) NOT NULL,
	[MaLoaiSanPham] [varchar](20) NULL,
	[TenSanPham] [varchar](100) NULL,
	[DonGia] [float] NULL,
	[HinhAnh] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[TaiKhoan] [varchar](50) NOT NULL,
	[MatKhau] [varchar](100) NULL,
	[MaChucVu] [varchar](10) NULL,
	[TrangThai] [bit] NULL,
	[MaNhanVien] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrangThaiSanPham]    Script Date: 15/03/2024 11:22:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrangThaiSanPham](
	[MaTrangThaiThe] [varchar](20) NOT NULL,
	[MaNhanVien] [varchar](20) NULL,
	[ThoiGianNhap] [datetime] NULL,
	[TrangThai] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTrangThaiThe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ChamCong]  WITH CHECK ADD FOREIGN KEY([MaCaLamViec])
REFERENCES [dbo].[CaLamViec] ([MaCaLamViec])
GO
ALTER TABLE [dbo].[ChamCong]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[ChiTietCongThuc]  WITH CHECK ADD FOREIGN KEY([MaCongThucSanPham])
REFERENCES [dbo].[CongThuc] ([MaCongThucSanPham])
GO
ALTER TABLE [dbo].[ChiTietCongThuc]  WITH CHECK ADD FOREIGN KEY([MaNguyenLieu])
REFERENCES [dbo].[NguyenLieu] ([MaNguyenLieu])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([MaNguyenLieu])
REFERENCES [dbo].[NguyenLieu] ([MaNguyenLieu])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([MaPhieuNhapKho])
REFERENCES [dbo].[PhieuNhapKho] ([MaPhieuNhapKho])
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([MaNguyenLieu])
REFERENCES [dbo].[NguyenLieu] ([MaNguyenLieu])
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([MaPhieuXuatKho])
REFERENCES [dbo].[PhieuXuatKho] ([MaPhieuXuatKho])
GO
ALTER TABLE [dbo].[ChiTietTrangThaiSanPham]  WITH CHECK ADD FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[ChiTietTrangThaiSanPham]  WITH CHECK ADD FOREIGN KEY([MaTrangThaiThe])
REFERENCES [dbo].[TrangThaiSanPham] ([MaTrangThaiThe])
GO
ALTER TABLE [dbo].[CongThuc]  WITH CHECK ADD FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[DatBan]  WITH CHECK ADD FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[DatBan]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaKhuyenMai])
REFERENCES [dbo].[KhuyenMai] ([MaKhuyenMai])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[KhoHang]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[LichSuQuanLyKho]  WITH CHECK ADD FOREIGN KEY([MaKhoHang])
REFERENCES [dbo].[KhoHang] ([MaKhoHang])
GO
ALTER TABLE [dbo].[LichSuQuanLyKho]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[LoaiNguyenLieu]  WITH CHECK ADD FOREIGN KEY([MaKhoHang])
REFERENCES [dbo].[KhoHang] ([MaKhoHang])
GO
ALTER TABLE [dbo].[LoaiSanPham]  WITH CHECK ADD FOREIGN KEY([MaKhoHang])
REFERENCES [dbo].[KhoHang] ([MaKhoHang])
GO
ALTER TABLE [dbo].[LuongNvien]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[NguyenLieu]  WITH CHECK ADD FOREIGN KEY([MaLoaiNguyenLieu])
REFERENCES [dbo].[LoaiNguyenLieu] ([MaLoaiNguyenLieu])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([MaChucVu])
REFERENCES [dbo].[ChucVu] ([MaChucVu])
GO
ALTER TABLE [dbo].[PhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([MaKhoHang])
REFERENCES [dbo].[KhoHang] ([MaKhoHang])
GO
ALTER TABLE [dbo].[PhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([MaNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([MaNhaCungCap])
GO
ALTER TABLE [dbo].[PhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([MaKhoHang])
REFERENCES [dbo].[KhoHang] ([MaKhoHang])
GO
ALTER TABLE [dbo].[PhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([MaLoaiSanPham])
REFERENCES [dbo].[LoaiSanPham] ([MaLoaiSanPham])
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD FOREIGN KEY([MaChucVu])
REFERENCES [dbo].[ChucVu] ([MaChucVu])
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[TrangThaiSanPham]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
USE [master]
GO
ALTER DATABASE [QLYCOFFEE] SET  READ_WRITE 
GO
