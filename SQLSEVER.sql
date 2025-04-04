﻿CREATE TABLE KHOA (
    MaKhoa VARCHAR(10) PRIMARY KEY,
    TenKhoa NVARCHAR(50)
);

-- Tạo bảng MONHOC
CREATE TABLE MONHOC (
    MaMonHoc VARCHAR(10) PRIMARY KEY,
    TenMonHoc NVARCHAR(50),
    MaLop VARCHAR(10) -- Không dùng foreign key
);

-- Tạo bảng LOP
CREATE TABLE LOP (
    MaLop VARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(50),
    MaKhoa VARCHAR(10) -- Không dùng foreign key
);



-- Tạo bảng SINHVIEN
CREATE TABLE SINHVIEN (
    MaSinhVien VARCHAR(20) PRIMARY KEY,
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

INSERT INTO MONHOC (MaMonHoc, TenMonHoc, MaKhoa)
VALUES 
    ('MH001', N'Cơ Sở Dữ Liệu', 'K001'),
    ('MH002', N'Kinh Tế Vi Mô', 'K002');

INSERT INTO LOP (MaLop, TenLop, MaKhoa)
VALUES 
    ('L001', N'Lớp CNTT K55', 'K001'),
    ('L002', N'Lớp Kinh Tế K56', 'K002');

INSERT INTO SINHVIEN (MaSinhVien, HoTen, GioiTinh, NgaySinh, QueQuan, MaLop, MatKhau)
VALUES 
    ('qwe', N'Nguyễn Văn A', N'Nam', '2000-05-15', N'Hà Nội', 'L001', 'qwe'),
    ('giang', N'Trần Thị B', N'Nữ', '2001-08-20', N'TP HCM', 'L002', '123');
delete SINHVIEN
drop table SINHVIEN

select * from SINHVIEN
select * from LOP
select * from KHOA

drop table DeThi


ALTER TABLE SINHVIEN
ALTER COLUMN MaSinhVien VARCHAR(15);

