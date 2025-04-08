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

drop table MONHOC

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
    MaBangDiem INT IDENTITY(1,1) PRIMARY KEY,
    Diem FLOAT,
    MaDeThi VARCHAR(10), -- Không dùng foreign key
    MaMonHoc VARCHAR(10), -- Không dùng foreign key
    MaSinhVien VARCHAR(20), -- Không dùng foreign key
    MaKhoa VARCHAR(10) -- Không dùng foreign key
);

ALTER TABLE BANGDIEM
ALTER COLUMN MaSinhVien VARCHAR(20);

drop table BANGDIEM

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
select * from MONHOC
select* from DETHI
select * from CAUHOI
select * from BANGDIEM
select* from DETHI
select * from CAUHOI

drop table DeThi


ALTER TABLE SINHVIEN
ALTER COLUMN MaSinhVien VARCHAR(15);

select SINHVIEN.MaSinhVien, SINHVIEN.HoTen, SINHVIEN.GioiTinh, SINHVIEN.NgaySinh, SINHVIEN.QueQuan, LOP.TenLop, KHOA.TenKhoa
from SINHVIEN 
join LOP on SINHVIEN.MaLop = LOP.MaLop 
join KHOA on LOP.MaKhoa = KHOA.MaKhoa
where SINHVIEN.MaSinhVien = '73DCHT22115'

select MONHOC.TenMonHoc, MONHOC.MaMonHoc 
from MONHOC  join KHOA on KHOA.MaKhoa = MONHOC.MaKhoa  
join LOP on LOP.MaKhoa = KHOA.MaKhoa 
join SINHVIEN on SINHVIEN.MaLop = LOP.MaLop 
where SINHVIEN.MaSinhVien = '73DCHT22115'

select DETHI.MaDeThi, DETHI.TenDeThi from DETHI  
join MONHOC on MONHOC.MaMonHoc = DETHI.MaMonHoc  
join LOP on LOP.MaLop = DETHI.MaLop 
join SINHVIEN on SINHVIEN.MaLop = Lop.MaLop 
where MONHOC.MaMonHoc = DETHI.MaMonHoc and SINHVIEN.MaSinhVien = '73DCHT22115'

select BANGDIEM.MaBangDiem, DETHI.TenDeThi, BANGDIEM.Diem, DETHI.ThoiGianThi, DETHI.SoLuongCauHoi, DETHI.ThoiGianBatDau, DETHI.ThoiGianKetThuc
from BANGDIEM 
join DETHI on DETHI.MaDeThi = BANGDIEM.MaDeThi
where BANGDIEM.MaDeThi = DETHI.MaDeThi

select DETHI.MaDeThi, DETHI.TenDeThi, DETHI.ThoiGianThi, DETHI.ThoiGianBatDau, DETHI.ThoiGianKetThuc, DETHI.SoLuongCauHoi
from DETHI join MONHOC on MONHOC.MaMonHoc = DETHI.MaMonHoc
where MONHOC.MaMonHoc = 'MH001'

select * from DETHI
select * from CAUHOI
select * from SINHVIEN

select SINHVIEN.MaSinhVien, SINHVIEN.HoTen, DETHI.MaDeThi, count(CAUHOI.MaCauHoi) as SoLuongCauHoi, DETHI.ThoiGianThi, DETHI.TenDeThi
from SINHVIEN
join DETHI on DETHI.MaLop = SINHVIEN.MaLop
join CAUHOI on CAUHOI.MaDeThi = DETHI.MaDeThi
where SINHVIEN.MaSinhVien = 'giang' and DETHI.MaDeThi = 'DTCNTT054'
group by 
	SINHVIEN.MaSinhVien, 
    SINHVIEN.HoTen, 
    DETHI.MaDeThi, 
    DETHI.ThoiGianThi, 
    DETHI.TenDeThi

select * from DETHI
select * from CAUHOI

select CAUHOI.MaCauHoi
from CAUHOI
join DETHI on DETHI.MaDeThi = CauHoi.MaDeThi

select CAUHOI.NoiDungCauHoi, CAUHOI.DapAnA, CAUHOI.DapAnB, CAUHOI.DapAnC, CAUHOI.DapAnD 
from CAUHOI 
where CAUHOI.MaCauHoi = '1'

select * from BANGDIEM
delete BANGDIEM
select * from DETHI
select * from SINHVIEN
select * from CAUHOI
delete CAUHOI
select * from SINHVIEN
select * from GIANGVIEN
select * from MONHOC

delete SINHVIEN

select SINHVIEN.MaSinhVien, SINHVIEN.HoTen, SINHVIEN.GioiTinh, SINHVIEN.NgaySinh, BANGDIEM.Diem  from SINHVIEN  join BANGDIEM on BANGDIEM.MaSinhVien = SINHVIEN.MaSinhVien where BANGDIEM.MaDeThi = 'DTCNTT054'