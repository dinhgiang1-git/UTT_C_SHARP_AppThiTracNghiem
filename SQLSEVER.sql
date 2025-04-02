CREATE TABLE KHOA (
    MaKhoa VARCHAR(10) PRIMARY KEY,
    TenKhoa NVARCHAR(50)
);

-- Tạo bảng MONHOC
CREATE TABLE MONHOC (
    MaMonHoc VARCHAR(10) PRIMARY KEY,
    TenMonHoc NVARCHAR(50),
    MaKhoa VARCHAR(10) -- Không dùng foreign key
);

-- Tạo bảng LOP
CREATE TABLE LOP (
    MaLop VARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(50),
    MaKhoa VARCHAR(10) -- Không dùng foreign key
);

-- Tạo bảng SINHVIEN
CREATE TABLE SINHVIEN (
    MaSinhVien VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(50),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    QueQuan NVARCHAR(50),
    MaLop VARCHAR(10), -- Không dùng foreign key
    MatKhau VARCHAR(50)
);

-- Tạo bảng GIANGVIEN (Đã thêm cột MaKhoa)
CREATE TABLE GIANGVIEN (
    MaGiangVien VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(50),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    QueQuan NVARCHAR(50),
    MatKhau VARCHAR(50),
    MaKhoa VARCHAR(10) -- Thêm cột MaKhoa, không dùng foreign key
);

-- Tạo bảng CAUHOI
CREATE TABLE CAUHOI (
    MaCauHoi INT IDENTITY(1,1) PRIMARY KEY,
    NoiDungCauHoi NVARCHAR(500),
    DapAnA NVARCHAR(100),
    DapAnB NVARCHAR(100),
    DapAnC NVARCHAR(100),
    DapAnD NVARCHAR(100),
    DapAnDung NVARCHAR(100),
    MaDeThi VARCHAR(10) -- Không dùng foreign key
);

-- Tạo bảng DETHI
CREATE TABLE DETHI (
    MaDeThi VARCHAR(10) PRIMARY KEY,
    TenDeThi NVARCHAR(100),
    MaKhoa VARCHAR(10), -- Không dùng foreign key
    MaMonHoc VARCHAR(10), -- Không dùng foreign key
    MaSinhVien VARCHAR(10), -- Không dùng foreign key
    ThoiGianThi INT,
    ThoiGianBatDau DATETIME,
    ThoiGianKetThuc DATETIME,
    SoLuongCauHoi INT,
    MaLop VARCHAR(10) -- Không dùng foreign key
);

-- Tạo bảng BANGDIEM
CREATE TABLE BANGDIEM (
    MaBangDiem VARCHAR(10) PRIMARY KEY,
    Diem FLOAT,
    MaDeThi VARCHAR(10), -- Không dùng foreign key
    MaMonHoc VARCHAR(10), -- Không dùng foreign key
    MaSinhVien VARCHAR(10), -- Không dùng foreign key
    MaKhoa VARCHAR(10) -- Không dùng foreign key
);

INSERT INTO GIANGVIEN (MaGiangVien, HoTen, GioiTinh, NgaySinh, QueQuan, MatKhau, MaKhoa)
VALUES 
    ('123', N'Đinh Đức Giang', N'Nam', '2004-01-29', N'Quảng Ninh', '123', 'K001'),
    ('345', N'Phạm Thị D', N'Nữ', '1980-03-10', N'Hà Nội', '345', 'K002');

INSERT INTO KHOA (MaKhoa, TenKhoa)
VALUES 
    ('K001', N'Công Nghệ Thông Tin'),
    ('K002', N'Kinh Tế');

-- Chèn dữ liệu mẫu vào bảng MONHOC
INSERT INTO MONHOC (MaMonHoc, TenMonHoc, MaKhoa)
VALUES 
    ('MH001', N'Cơ Sở Dữ Liệu', 'K001'),
    ('MH002', N'Kinh Tế Vi Mô', 'K002');

select * from KHOA 


