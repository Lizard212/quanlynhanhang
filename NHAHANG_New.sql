CREATE TABLE NHANVIEN(
						MANV INT NOT NULL,
						TENNV NVARCHAR(50),
						PRIMARY KEY(MANV)						
)
 
CREATE TABLE TAIKHOAN(
						USERS VARCHAR(20) NOT NULL,
						PASS VARCHAR(20) NOT NULL DEFAULT '1',
						MANV INT REFERENCES NHANVIEN(MANV) ON DELETE CASCADE ON UPDATE CASCADE,
						PRIMARY KEY(USERS)
)

CREATE TABLE KHUVUC(
						MAKV VARCHAR(10) NOT NULL,
						TENKV NVARCHAR(50),
						CP_TOITHIEU INT DEFAULT 0,
						PRIMARY KEY(MAKV)
)

CREATE TABLE BAN(
						MABAN INT NOT NULL IDENTITY(1,1),
						TENBAN NVARCHAR(20),
						MAKV VARCHAR(10) REFERENCES KHUVUC(MAKV) ON DELETE CASCADE ON UPDATE CASCADE,
						PRIMARY KEY(MABAN) 
)

CREATE TABLE NHOMSP(
						MANHOM VARCHAR(10) NOT NULL,
						TENNHOM NVARCHAR(50),
						PRIMARY KEY(MANHOM)
)

CREATE TABLE SANPHAM(
						MASP INT NOT NULL IDENTITY(1,1),
						TENSP NVARCHAR(50),
						GIA INT DEFAULT 0,
						GIAMGIA INT DEFAULT 0,
						DONVI NVARCHAR(10),
						TINHTRANG BIT DEFAULT 1,
						MANHOM VARCHAR(10) REFERENCES NHOMSP(MANHOM) ON DELETE CASCADE ON UPDATE CASCADE,
						PRIMARY KEY(MASP)
)

CREATE TABLE HOADON(
						MAHD INT NOT NULL IDENTITY(1,1),
						NGAYLAP DATETIME,
						SLKHACH INT DEFAULT 1,
						PHIKHAC INT DEFAULT 0,
						GHICHU NVARCHAR(300),
						MANV INT REFERENCES NHANVIEN(MANV) ON DELETE SET NULL ON UPDATE CASCADE,
						MABAN INT REFERENCES BAN(MABAN) ON DELETE SET NULL ON UPDATE CASCADE,
						PRIMARY KEY(MAHD) 
)

CREATE TABLE CTHOADON(
						MAHD  INT REFERENCES HOADON(MAHD) ON DELETE CASCADE ON UPDATE CASCADE,
						MASP INT REFERENCES SANPHAM(MASP) ON DELETE CASCADE ON UPDATE CASCADE,
						SOLUONG INT DEFAULT 1,
						GIA INT,
						GIAMGIA INT DEFAULT 0,		
						PRIMARY KEY(MAHD,MASP)
)


create proc searchKhuvuc @tenkv nvarchar(50)
as
	begin
		select * from khuvuc where TENKV like @tenkv
	end

create proc searchBan @tenban nvarchar(20),@tenkv nvarchar(50)
as
	begin
		select * from ban where TENBAN like @tenban and MAKV=(select MAKV from KHUVUC where TENKV=@tenkv) 
	end

CREATE PROC searchNhomsp @tennhom nvarchar(50)
as
	begin
		select * from NHOMSP where tennhom like @tennhom
	end

create proc searchSanpham @tensp nvarchar(50), @tennhom nvarchar(50)
as
	begin
		select * from sanpham where tensp like @tensp and manhom=(select manhom from nhomsp where tennhom=@tennhom)
	end

create proc searchNV @tennv nvarchar(50)
as
	begin
	 select * from nhanvien where tennv like @tennv
	end

create proc searchHD @mahd nvarchar(50)
as
	begin
		select * from HOADON where MAHD like @mahd
	end


 
	




	