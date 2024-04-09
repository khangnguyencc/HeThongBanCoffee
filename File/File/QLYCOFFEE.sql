

-- Bảng KhachHang
CREATE TABLE KhachHang (
    MaKhachHang VARCHAR(20) PRIMARY KEY,
    SDT VARCHAR(20),
    TenKhachHang VARCHAR(100),
    NgaySinh DATE,
    DiaChi VARCHAR(255),
    DiemTichLuy INT
);

-- Bảng DatBan
CREATE TABLE DatBan (
    MaDatBan VARCHAR(20) PRIMARY KEY,
    MaNhanVien VARCHAR(20),
    NgayDatBan DATE,
    MaKhachHang VARCHAR(20),
    ThoiGianNhanBan DATETIME,
    SoLuong INT,
    GhiChu TEXT,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);

-- Bảng ChamCong
CREATE TABLE ChamCong (
    MaChamCong VARCHAR(20) PRIMARY KEY,
    MaNhanVien VARCHAR(20),
    MaCaLamViec VARCHAR(10),
    NgayLamViec DATE,
    GioBatDau TIME,
    GioKetThuc TIME,
    TongGioLam FLOAT,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaCaLamViec) REFERENCES CaLamViec(MaCaLamViec)
);

-- Bảng CaLamViec
CREATE TABLE CaLamViec (
    MaCaLamViec VARCHAR(10) PRIMARY KEY,
    TenCaLamViec VARCHAR(100),
    GioBatDau TIME,
    GioKetThuc TIME
);

-- Bảng KhuyenMai
CREATE TABLE KhuyenMai (
    MaKhuyenMai VARCHAR(20) PRIMARY KEY,
    GiaTriKhuyenMai FLOAT,
    SoLuong INT,
    NgayBatDau DATETIME,
    NgayKetThuc DATETIME
);

-- Bảng HoaDon
CREATE TABLE HoaDon (
    MaHoaDon VARCHAR(20) PRIMARY KEY,
    MaKhachHang VARCHAR(20),
    MaNhanVien VARCHAR(20),
    MaKhuyenMai VARCHAR(20),
    SoThe VARCHAR(20),
    NgayBanHang DATETIME,
    PhuongThucThanhToan VARCHAR(50),
    TongTien FLOAT,
    TienKhach FLOAT,
    TienThua FLOAT,
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai)
);
CREATE TABLE HoaDonTam (
    MaHoaDonTam VARCHAR(20) PRIMARY KEY,
    MaKhachHang VARCHAR(20),
    MaNhanVien VARCHAR(20),
    MaKhuyenMai VARCHAR(20),
    SoThe VARCHAR(20),
    NgayBanHang DATETIME,
    PhuongThucThanhToan VARCHAR(50),
    TongTien FLOAT,
    TienKhach FLOAT,
    TienThua FLOAT,
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai)
);

-- Bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaHoaDon VARCHAR(20),
    MaSanPham VARCHAR(20),
    DonGia FLOAT,
    SoLuong INT,
    ThanhTien FLOAT,
    PRIMARY KEY (MaHoaDon, MaSanPham),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);
CREATE TABLE ChiTietHoaDonTam (
    MaHoaDonTam VARCHAR(20),
    MaSanPham VARCHAR(20),
    DonGia FLOAT,
    SoLuong INT,
    ThanhTien FLOAT,
    PRIMARY KEY (MaHoaDonTam, MaSanPham),
    FOREIGN KEY (MaHoaDonTam) REFERENCES HoaDonTam(MaHoaDonTam),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);

-- Bảng ChucVu
CREATE TABLE ChucVu (
    MaChucVu VARCHAR(10) PRIMARY KEY,
    TenChucVu VARCHAR(100)
);


-- Bảng TaiKhoan
CREATE TABLE TaiKhoan (
    TaiKhoan VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(100),
    MaChucVu VARCHAR(10),
    TrangThai BIT,
    FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu)
);

-- Bảng LuongNvien
CREATE TABLE LuongNvien (
    MaLuong VARCHAR(20) PRIMARY KEY,
    MaNhanVien VARCHAR(20),
    TongGioLam FLOAT,
    TongTienLuong FLOAT,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Bảng NhanVien
CREATE TABLE NhanVien (
    MaNhanVien VARCHAR(20) PRIMARY KEY,
    MaChucVu VARCHAR(10),
    TenNhanVien VARCHAR(100),
    SDT VARCHAR(20),
    GioiTinh VARCHAR(5),
    DiaChi VARCHAR(255),
    NgayVaoLam DATETIME,
    FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu)
);

-- Bảng PhieuXuatKho
CREATE TABLE PhieuXuatKho (
    MaPhieuXuatKho VARCHAR(20) PRIMARY KEY,
    MaKhoHang VARCHAR(20),
    MaNhanVien VARCHAR(20),
    NgayXuat DATETIME,
    FOREIGN KEY (MaKhoHang) REFERENCES KhoHang(MaKhoHang),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Bảng SanPham
CREATE TABLE SanPham (
    MaSanPham VARCHAR(20) PRIMARY KEY,
    MaLoaiSanPham VARCHAR(20),
    TenSanPham VARCHAR(100),
    DonGia FLOAT,
    HinhAnh VARCHAR(255),
    FOREIGN KEY (MaLoaiSanPham) REFERENCES LoaiSanPham(MaLoaiSanPham)
);

-- Bảng ChiTietTrangThaiSanPham
CREATE TABLE ChiTietTrangThaiSanPham (
    HinhAnh VARCHAR(255),
    MaTrangThaiThe VARCHAR(20),
    MaSanPham VARCHAR(20),
    SoLuong INT,
    Ghichu TEXT,
    PRIMARY KEY (MaTrangThaiThe, MaSanPham),
    FOREIGN KEY (MaTrangThaiThe) REFERENCES TrangThaiSanPham(MaTrangThaiThe),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);

-- Bảng TrangThaiSanPham
CREATE TABLE TrangThaiSanPham (
    MaTrangThaiThe VARCHAR(20) PRIMARY KEY,
    MaNhanVien VARCHAR(20),
    ThoiGianNhap DATETIME,
    TrangThai VARCHAR(50),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Bảng NguyenLieu
CREATE TABLE NguyenLieu (
    MaNguyenLieu VARCHAR(20) PRIMARY KEY,
    MaLoaiNguyenLieu VARCHAR(20),
    TenNguyenLieu VARCHAR(100),
    HanSuDung DATE,
    FOREIGN KEY (MaLoaiNguyenLieu) REFERENCES LoaiNguyenLieu(MaLoaiNguyenLieu)
);

-- Bảng ChiTietPhieuXuatKho
CREATE TABLE ChiTietPhieuXuatKho (
    MaPhieuXuatKho VARCHAR(20),
    MaNguyenLieu VARCHAR(20),
    SoLuong INT,
    PRIMARY KEY (MaPhieuXuatKho, MaNguyenLieu),
    FOREIGN KEY (MaPhieuXuatKho) REFERENCES PhieuXuatKho(MaPhieuXuatKho),
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu)
);

-- Bảng CongThuc
CREATE TABLE CongThuc (
    MaCongThucSanPham VARCHAR(20) PRIMARY KEY,
    MaSanPham VARCHAR(20),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);

-- Bảng ChiTietCongThuc
CREATE TABLE ChiTietCongThuc (
    MaNguyenLieu VARCHAR(20),
    MaCongThucSanPham VARCHAR(20),
    DungTich VARCHAR(20),
    PRIMARY KEY (MaNguyenLieu, MaCongThucSanPham),
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu),
    FOREIGN KEY (MaCongThucSanPham) REFERENCES CongThuc(MaCongThucSanPham)
);

-- Bảng LoaiNguyenLieu
CREATE TABLE LoaiNguyenLieu (
    MaLoaiNguyenLieu VARCHAR(20) PRIMARY KEY,
    MaKhoHang VARCHAR(20),
    TenLoaiNguyenLieu VARCHAR(100),
    FOREIGN KEY (MaKhoHang) REFERENCES KhoHang(MaKhoHang)
);

-- Bảng LoaiSanPham
CREATE TABLE LoaiSanPham (
    MaLoaiSanPham VARCHAR(20) PRIMARY KEY,
    MaKhoHang VARCHAR(20),
    TenLoaiSanPham VARCHAR(100),
    FOREIGN KEY (MaKhoHang) REFERENCES KhoHang(MaKhoHang)
);

-- Bảng KhoHang
CREATE TABLE KhoHang (
    MaKhoHang VARCHAR(20) PRIMARY KEY,
    MaNhanVien VARCHAR(20),
    NgayBatDau DATE,
    GhiChu TEXT,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Bảng ChiTietPhieuNhapKho
CREATE TABLE ChiTietPhieuNhapKho (
    MaPhieuNhapKho VARCHAR(20),
    MaNguyenLieu VARCHAR(20),
    SoLuong INT,
    DonGia FLOAT,
    DonVi VARCHAR(50),
    ThanhTien FLOAT,
    PRIMARY KEY (MaPhieuNhapKho, MaNguyenLieu),
    FOREIGN KEY (MaPhieuNhapKho) REFERENCES PhieuNhapKho(MaPhieuNhapKho),
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu)
);

-- Bảng PhieuNhapKho
CREATE TABLE PhieuNhapKho (
    MaPhieuNhapKho VARCHAR(20) PRIMARY KEY,
    MaKhoHang VARCHAR(20),
    MaNhaCungCap VARCHAR(20),
    NgayNhap DATETIME,
    TongTien FLOAT,
    FOREIGN KEY (MaKhoHang) REFERENCES KhoHang(MaKhoHang),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);

-- Bảng LichSuQuanLyKho
CREATE TABLE LichSuQuanLyKho (
    STT INT IDENTITY PRIMARY KEY,
    MaKhoHang VARCHAR(20),
    MaNhanVien VARCHAR(20),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    TrangThai VARCHAR(50),
    FOREIGN KEY (MaKhoHang) REFERENCES KhoHang(MaKhoHang),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Bảng NhaCungCap
CREATE TABLE NhaCungCap (
    MaNhaCungCap VARCHAR(20) PRIMARY KEY,
    TenNhaCungCap VARCHAR(100),
    SDT VARCHAR(20),
    Email VARCHAR(100)
);
