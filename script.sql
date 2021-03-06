USE [master]
GO
/****** Object:  Database [TiemDoChoi]    Script Date: 7/22/2021 3:35:45 PM ******/
CREATE DATABASE [TiemDoChoi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TiemDoChoi', FILENAME = N'G:\PinkDev\CongNghePhanMem\DatabaseTiemDoChoi\TiemDoChoi.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TiemDoChoi_log', FILENAME = N'G:\PinkDev\CongNghePhanMem\DatabaseTiemDoChoi\TiemDoChoi_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TiemDoChoi] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TiemDoChoi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TiemDoChoi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TiemDoChoi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TiemDoChoi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TiemDoChoi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TiemDoChoi] SET ARITHABORT OFF 
GO
ALTER DATABASE [TiemDoChoi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TiemDoChoi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TiemDoChoi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TiemDoChoi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TiemDoChoi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TiemDoChoi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TiemDoChoi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TiemDoChoi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TiemDoChoi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TiemDoChoi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TiemDoChoi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TiemDoChoi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TiemDoChoi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TiemDoChoi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TiemDoChoi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TiemDoChoi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TiemDoChoi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TiemDoChoi] SET RECOVERY FULL 
GO
ALTER DATABASE [TiemDoChoi] SET  MULTI_USER 
GO
ALTER DATABASE [TiemDoChoi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TiemDoChoi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TiemDoChoi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TiemDoChoi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TiemDoChoi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TiemDoChoi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TiemDoChoi', N'ON'
GO
ALTER DATABASE [TiemDoChoi] SET QUERY_STORE = OFF
GO
USE [TiemDoChoi]
GO
/****** Object:  UserDefinedFunction [dbo].[fnAlphaIDENT]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnAlphaIDENT] (@i int)
RETURNS CHAR(3) AS
BEGIN
 DECLARE @Alpha CHAR(3)
 IF @i < 0 or @i > 26*26*26 return (NULL);
 SET @Alpha = CHAR(65+@i /(26*26)) +CHAR(65+(@i % (26*26)) / 26)+ CHAR(65+@i % 26)
 RETURN (@Alpha)
END
GO
/****** Object:  Table [dbo].[AlphaIDENT]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlphaIDENT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AlphaID]  AS ([dbo].[fnAlphaIDENT]([id])),
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTHoaDon]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTHoaDon](
	[MaHoaDon] [int] NOT NULL,
	[MaKhachHang] [int] NOT NULL,
 CONSTRAINT [PK_CTHoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTNhapKho]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTNhapKho](
	[MaNhapKho] [int] NULL,
	[MaNhanVien] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DSBanHang]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DSBanHang](
	[MaHoaDon] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[SLBan] [int] NOT NULL,
	[TongTien] [int] NOT NULL,
 CONSTRAINT [PK_DSBanHang] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DSNhapKho]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DSNhapKho](
	[MaSanPham] [int] NOT NULL,
	[MaNhapKho] [int] NOT NULL,
	[SoLuongNhap] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDonBanHang]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonBanHang](
	[MaHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[TinhTrangThanhToan] [bit] NULL,
	[NgayLap] [datetime] NOT NULL,
	[DiaChiNhanHang] [nvarchar](100) NULL,
 CONSTRAINT [PK_HoaDonBanHang_1] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[TenKhachHang] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SoDienThoai] [varchar](15) NULL,
	[TenDangNhapKH] [varchar](20) NULL,
	[MatKhau] [varchar](20) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiHangHoa]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHangHoa](
	[MaLoai] [varchar](10) NOT NULL,
	[TenLoai] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiHangHoa] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [varchar](10) NOT NULL,
	[TenNhanVien] [nvarchar](50) NOT NULL,
	[MKTruyCap] [varchar](16) NOT NULL,
	[MaChucVu] [varchar](15) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[SoDienThoai] [varchar](15) NOT NULL,
	[SoCMND] [varchar](15) NOT NULL,
	[GioiTinh] [char](5) NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuNhapKho]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuNhapKho](
	[MaNhapKho] [int] IDENTITY(1,1) NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[NgayNhap] [datetime] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_PhieuNhapKho] PRIMARY KEY CLUSTERED 
(
	[MaNhapKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuyDinh]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuyDinh](
	[MaQuyDinh] [nchar](10) NOT NULL,
	[TenQuyDinh] [nvarchar](50) NULL,
	[ThamSo] [real] NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_QuyDinh] PRIMARY KEY CLUSTERED 
(
	[MaQuyDinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuyenTruyCap]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuyenTruyCap](
	[MaChucVu] [varchar](15) NOT NULL,
	[TenChucVu] [nvarchar](30) NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_QuyenTruyCap] PRIMARY KEY CLUSTERED 
(
	[MaChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 7/22/2021 3:35:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [int] IDENTITY(1,1) NOT NULL,
	[TenSanPham] [nvarchar](50) NOT NULL,
	[MaLoai] [varchar](10) NOT NULL,
	[XuatXu] [nvarchar](50) NOT NULL,
	[DonViTinh] [varchar](10) NOT NULL,
	[SLTon] [int] NOT NULL,
	[GiaThuMua] [money] NOT NULL,
	[GiaBan] [money] NOT NULL,
	[NgaySanXuat] [date] NULL,
	[GhiChu] [nvarchar](500) NULL,
	[HinhAnh] [varchar](100) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AlphaIDENT] ON 

INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (1, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (2, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (3, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (4, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (5, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (6, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (7, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (8, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (9, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (10, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (11, CAST(N'2021-01-02' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (12, CAST(N'2021-01-03' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (13, CAST(N'2021-01-03' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (14, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (15, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (16, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (17, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (18, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (19, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (20, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (21, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (22, CAST(N'2021-01-11' AS Date))
INSERT [dbo].[AlphaIDENT] ([ID], [ModifiedDate]) VALUES (23, CAST(N'2021-01-11' AS Date))
SET IDENTITY_INSERT [dbo].[AlphaIDENT] OFF
GO
INSERT [dbo].[CTHoaDon] ([MaHoaDon], [MaKhachHang]) VALUES (2, 2)
GO
INSERT [dbo].[DSBanHang] ([MaHoaDon], [MaSanPham], [SLBan], [TongTien]) VALUES (2, 8, 6, 700000)
INSERT [dbo].[DSBanHang] ([MaHoaDon], [MaSanPham], [SLBan], [TongTien]) VALUES (2, 9, 4, 350000)
GO
SET IDENTITY_INSERT [dbo].[HoaDonBanHang] ON 

INSERT [dbo].[HoaDonBanHang] ([MaHoaDon], [TinhTrangThanhToan], [NgayLap], [DiaChiNhanHang]) VALUES (1, 0, CAST(N'2021-07-21T21:20:03.477' AS DateTime), N'Binh dinh')
INSERT [dbo].[HoaDonBanHang] ([MaHoaDon], [TinhTrangThanhToan], [NgayLap], [DiaChiNhanHang]) VALUES (2, 0, CAST(N'2021-07-21T21:27:53.200' AS DateTime), N'Binh dinh')
SET IDENTITY_INSERT [dbo].[HoaDonBanHang] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [DiaChi], [SoDienThoai], [TenDangNhapKH], [MatKhau]) VALUES (2, N'Binh Dep Train', CAST(N'2021-07-06' AS Date), N'123', N'123', N'pinkman', N'123')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
INSERT [dbo].[LoaiHangHoa] ([MaLoai], [TenLoai], [GhiChu]) VALUES (N'FI', N'Figure', NULL)
INSERT [dbo].[LoaiHangHoa] ([MaLoai], [TenLoai], [GhiChu]) VALUES (N'MT', N'Mô hình(Model Toy)', N'oke bla bla')
INSERT [dbo].[LoaiHangHoa] ([MaLoai], [TenLoai], [GhiChu]) VALUES (N'PUZ', N'Đồ chơi giải đố', NULL)
INSERT [dbo].[LoaiHangHoa] ([MaLoai], [TenLoai], [GhiChu]) VALUES (N'TECH', N'Đồ chơi công nghệ', NULL)
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [MKTruyCap], [MaChucVu], [NgaySinh], [DiaChi], [SoDienThoai], [SoCMND], [GioiTinh]) VALUES (N'admin', N'Bình đẹp trai', N'admin', N'ADMIN', CAST(N'2000-06-16' AS Date), N'Binh duong', N'0969696969', N'696969696', N'Nam  ')
GO
INSERT [dbo].[QuyenTruyCap] ([MaChucVu], [TenChucVu], [GhiChu]) VALUES (N'ACCOUNTANT', N'Kê toán', NULL)
INSERT [dbo].[QuyenTruyCap] ([MaChucVu], [TenChucVu], [GhiChu]) VALUES (N'ADMIN', N'ADMIN', NULL)
INSERT [dbo].[QuyenTruyCap] ([MaChucVu], [TenChucVu], [GhiChu]) VALUES (N'BOSS', N'Giám Đốc', N'anh dep trai')
INSERT [dbo].[QuyenTruyCap] ([MaChucVu], [TenChucVu], [GhiChu]) VALUES (N'MANAGER', N'Quản lý', NULL)
INSERT [dbo].[QuyenTruyCap] ([MaChucVu], [TenChucVu], [GhiChu]) VALUES (N'SELLER', N'Nhân viên bán hàng', NULL)
INSERT [dbo].[QuyenTruyCap] ([MaChucVu], [TenChucVu], [GhiChu]) VALUES (N'WSTAFF', N'Quản lý kho', NULL)
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (7, N'Mega Man X1', N'FI', N'China', N'Cai', 3, 300000.0000, 350000.0000, CAST(N'2020-12-03' AS Date), N'From 2077 with luv', N'figure1.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (8, N'Vader Star War', N'MT', N'USA', N'Cai', 1, 600000.0000, 700000.0000, CAST(N'2020-12-03' AS Date), N'May the force be with yeu', N'gundam1.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (9, N'Gundam MG 1', N'FI', N'Japan', N'Cai', 5, 300000.0000, 350000.0000, CAST(N'2021-03-06' AS Date), N'BONK', N'figure2.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (10, N'Con Cóc', N'TECH', N'China', N'Cái', 33, 300000.0000, 350000.0000, NULL, NULL, N'28xp-pepefrog-articleInline.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (11, N'Con Cóc11', N'TECH', N'China', N'Cái', 33, 300000.0000, 350000.0000, NULL, NULL, N'28xp-pepefrog-articleInline.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (12, N'Doan Chi Pink bla bla', N'MT', N'China', N'Cái', 2, 300000.0000, 350000.0000, NULL, NULL, N'images.png')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (13, N'Kawaiiiii', N'FI', N'Japan', N'Cái', 1, 300000.0000, 350000.0000, CAST(N'2021-07-06' AS Date), N'<p>Alo alo alo</p>', N'e010fd6183a2b3791aebdb0c44f00c75.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (14, N'Doan Chi Pink 1113', N'MT', N'China asdasdasd', N' asdasdasd', 111111, 50000000000.0000, 350000.0000, CAST(N'2021-06-27' AS Date), N'<p>xin chao dong bao</p>', N'gundam1.jpg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (15, N'Alo test day test day', N'TECH', N'China', N'Cái', 11, 300000.0000, 350000.0000, NULL, N'<p>abc 123</p>', N'map-italy-cs.jpeg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (16, N'test test', N'TECH', N'China', N'Cái', 1, 300000.0000, 350000.0000, CAST(N'2021-07-05' AS Date), N'<p>Ghi ch&uacute; mo tả về sản phẩm!!!</p>', N'figure2.jfif')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (17, N'test 247', N'PUZ', N'China', N'Cái', 331, 300000.0000, 350000.0000, NULL, N'<p>Ghi ch&uacute; mo tả về sản phẩm!!!</p>', N'map-italy-cs.jpeg')
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MaLoai], [XuatXu], [DonViTinh], [SLTon], [GiaThuMua], [GiaBan], [NgaySanXuat], [GhiChu], [HinhAnh]) VALUES (18, N'Doan Chi Pink vjp pro bla bla bla', N'TECH', N'China', N'Cái', 1, 300000.0000, 350000.0000, CAST(N'2021-06-27' AS Date), N'<p>h&ecirc; l&ocirc; chao x&igrave;n d&acirc;y l&agrave; pinkman hhhhhhhhh&nbsp;<strong>hhhhhhhhhhhhh&nbsp;<em>hhhhhhhhhhhhhhhhh&nbsp;</em></strong></p>', N'y-thien-do-long-ky.cf0b9.cover.jpg')
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
ALTER TABLE [dbo].[AlphaIDENT] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[CTHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CTHoaDon_HoaDonBanHang] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDonBanHang] ([MaHoaDon])
GO
ALTER TABLE [dbo].[CTHoaDon] CHECK CONSTRAINT [FK_CTHoaDon_HoaDonBanHang]
GO
ALTER TABLE [dbo].[CTHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_CTHoaDon_KhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[CTHoaDon] CHECK CONSTRAINT [FK_CTHoaDon_KhachHang]
GO
ALTER TABLE [dbo].[CTNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_CTNhapKho_NhanVien] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[CTNhapKho] CHECK CONSTRAINT [FK_CTNhapKho_NhanVien]
GO
ALTER TABLE [dbo].[CTNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_CTNhapKho_PhieuNhapKho1] FOREIGN KEY([MaNhapKho])
REFERENCES [dbo].[PhieuNhapKho] ([MaNhapKho])
GO
ALTER TABLE [dbo].[CTNhapKho] CHECK CONSTRAINT [FK_CTNhapKho_PhieuNhapKho1]
GO
ALTER TABLE [dbo].[DSBanHang]  WITH CHECK ADD  CONSTRAINT [FK_DSBanHang_HoaDonBanHang] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDonBanHang] ([MaHoaDon])
GO
ALTER TABLE [dbo].[DSBanHang] CHECK CONSTRAINT [FK_DSBanHang_HoaDonBanHang]
GO
ALTER TABLE [dbo].[DSBanHang]  WITH CHECK ADD  CONSTRAINT [FK_DSBanHang_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[DSBanHang] CHECK CONSTRAINT [FK_DSBanHang_SanPham]
GO
ALTER TABLE [dbo].[DSNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_DSNhapKho_PhieuNhapKho] FOREIGN KEY([MaNhapKho])
REFERENCES [dbo].[PhieuNhapKho] ([MaNhapKho])
GO
ALTER TABLE [dbo].[DSNhapKho] CHECK CONSTRAINT [FK_DSNhapKho_PhieuNhapKho]
GO
ALTER TABLE [dbo].[DSNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_DSNhapKho_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[DSNhapKho] CHECK CONSTRAINT [FK_DSNhapKho_SanPham]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_QuyenTruyCap] FOREIGN KEY([MaChucVu])
REFERENCES [dbo].[QuyenTruyCap] ([MaChucVu])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_QuyenTruyCap]
GO
ALTER TABLE [dbo].[PhieuNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhapKho_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[PhieuNhapKho] CHECK CONSTRAINT [FK_PhieuNhapKho_SanPham]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoaiHangHoa] FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LoaiHangHoa] ([MaLoai])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoaiHangHoa]
GO
/****** Object:  StoredProcedure [dbo].[NhapSP]    Script Date: 7/22/2021 3:35:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhapSP]
@masp varchar(10),
@tensp nvarchar(50),
@maloai varchar(10),
@xuatxu nvarchar(50),
@dvt varchar(10),
@slt int,
@giamua money,
@giaban money,
@nsx date,
@ghichu nvarchar(100),
@hinh image
AS
insert into SanPham 
values
(
	@masp ,@tensp ,@maloai ,
	@xuatxu ,@dvt ,@slt ,
	@giamua ,@giaban ,@nsx ,
	@ghichu,@hinh
)
GO
/****** Object:  StoredProcedure [dbo].[ThemDSBanHang]    Script Date: 7/22/2021 3:35:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ThemDSBanHang]
@mahoadon int, @masanpham varchar(10), @slban int
as
insert into DSBanHang(MaHoaDon, MaSanPham, SLBan) values (@mahoadon,@masanpham,@slban)
update SanPham set SLTon = SLTon - @slban where MaSanPham = @masanpham
GO
USE [master]
GO
ALTER DATABASE [TiemDoChoi] SET  READ_WRITE 
GO
