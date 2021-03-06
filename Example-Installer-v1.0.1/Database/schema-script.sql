USE [master]
GO
/****** Object:  Database [QLDL]    Script Date: 03-Jun-17 5:56:22 PM ******/
CREATE DATABASE [QLDL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLDL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.LAM\MSSQL\DATA\QLDL.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLDL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.LAM\MSSQL\DATA\QLDL_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLDL] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLDL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLDL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLDL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLDL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLDL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLDL] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLDL] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLDL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLDL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLDL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLDL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLDL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLDL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLDL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLDL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLDL] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLDL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLDL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLDL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLDL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLDL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLDL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLDL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLDL] SET RECOVERY FULL 
GO
ALTER DATABASE [QLDL] SET  MULTI_USER 
GO
ALTER DATABASE [QLDL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLDL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLDL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLDL] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLDL] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLDL', N'ON'
GO
ALTER DATABASE [QLDL] SET QUERY_STORE = OFF
GO
USE [QLDL]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [QLDL]
GO
/****** Object:  Table [dbo].[DAILY]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DAILY](
	[MADL] [int] IDENTITY(1,1) NOT NULL,
	[TENDL] [nvarchar](50) NULL,
	[LOAIDL] [int] NULL,
	[DIENTHOAI] [nvarchar](50) NULL,
	[DIACHI] [nvarchar](50) NULL,
	[MAQUAN] [int] NULL,
	[NGAYTIEPNHAN] [date] NULL,
	[SONO] [numeric](18, 0) NULL,
	[TINHTRANG] [int] NULL,
 CONSTRAINT [PK_DAILY] PRIMARY KEY CLUSTERED 
(
	[MADL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LOAIDL]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAIDL](
	[MALOAI] [int] IDENTITY(1,1) NOT NULL,
	[TENLOAI] [nvarchar](10) NULL,
	[SONOTOIDA] [numeric](18, 0) NULL,
 CONSTRAINT [PK_LOAIDL] PRIMARY KEY CLUSTERED 
(
	[MALOAI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vwDAILY-LOAIDL]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwDAILY-LOAIDL]
AS
SELECT       dbo.DAILY.TENDL, dbo.DAILY.MADL, dbo.DAILY.LOAIDL, dbo.DAILY.DIENTHOAI, dbo.DAILY.DIACHI, dbo.DAILY.MAQUAN, dbo.DAILY.NGAYTIEPNHAN, dbo.DAILY.SONO, dbo.DAILY.TINHTRANG, dbo.LOAIDL.TENLOAI, 
                         dbo.LOAIDL.MALOAI, dbo.LOAIDL.SONOTOIDA
FROM            dbo.DAILY INNER JOIN
                         dbo.LOAIDL ON dbo.DAILY.LOAIDL = dbo.LOAIDL.MALOAI

GO
/****** Object:  Table [dbo].[QUAN]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUAN](
	[MAQUAN] [int] IDENTITY(1,1) NOT NULL,
	[TENQUAN] [nvarchar](50) NULL,
	[SODLTOIDA] [int] NULL,
 CONSTRAINT [PK_QUAN] PRIMARY KEY CLUSTERED 
(
	[MAQUAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vwDAILY-LOAIDL-QUAN]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwDAILY-LOAIDL-QUAN]
AS
SELECT       dbo.DAILY.MADL, dbo.DAILY.TENDL, dbo.DAILY.DIENTHOAI, dbo.DAILY.DIACHI, dbo.DAILY.NGAYTIEPNHAN, dbo.DAILY.SONO, dbo.DAILY.TINHTRANG, dbo.LOAIDL.TENLOAI, dbo.QUAN.TENQUAN, dbo.LOAIDL.MALOAI, 
                         dbo.QUAN.MAQUAN
FROM            dbo.DAILY INNER JOIN
                         dbo.LOAIDL ON dbo.DAILY.LOAIDL = dbo.LOAIDL.MALOAI INNER JOIN
                         dbo.QUAN ON dbo.DAILY.MAQUAN = dbo.QUAN.MAQUAN

GO
/****** Object:  Table [dbo].[CHUCVU]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHUCVU](
	[MACHUCVU] [int] NOT NULL,
	[TENCHUCVU] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[MACHUCVU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CTPX]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTPX](
	[MAPHIEU] [int] NOT NULL,
	[MAHANG] [int] NOT NULL,
	[SOLUONG] [int] NULL,
 CONSTRAINT [PK_CTPX] PRIMARY KEY CLUSTERED 
(
	[MAPHIEU] ASC,
	[MAHANG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DVT]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DVT](
	[MADVT] [int] IDENTITY(1,1) NOT NULL,
	[DVT] [nvarchar](50) NULL,
 CONSTRAINT [PK_DVT] PRIMARY KEY CLUSTERED 
(
	[MADVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MATHANG]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATHANG](
	[MAHANG] [int] IDENTITY(1,1) NOT NULL,
	[TENHANG] [nvarchar](50) NULL,
	[MADVT] [int] NULL,
	[DONGIA] [numeric](18, 0) NULL,
 CONSTRAINT [PK_MATHANG] PRIMARY KEY CLUSTERED 
(
	[MAHANG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MANV] [int] IDENTITY(1,1) NOT NULL,
	[TENNV] [nchar](10) NULL,
	[NGAYSINH] [date] NULL,
	[DIACHI] [nvarchar](50) NULL,
	[MACHUCVU] [int] NOT NULL,
 CONSTRAINT [PK_NHANVIEN] PRIMARY KEY CLUSTERED 
(
	[MANV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHIEUTHUTIEN]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUTHUTIEN](
	[MAPHIEU] [int] IDENTITY(1,1) NOT NULL,
	[MADL] [int] NULL,
	[NGAYTHUTIEN] [date] NULL,
	[SOTIEN] [numeric](18, 0) NULL,
	[NGUOITHU] [int] NULL,
 CONSTRAINT [PK_PHIEUTHUTIEN] PRIMARY KEY CLUSTERED 
(
	[MAPHIEU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHIEUXUATHANG]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUXUATHANG](
	[MAPHIEU] [int] IDENTITY(1,1) NOT NULL,
	[MADL] [int] NULL,
	[NGAYLAP] [date] NULL,
	[TONGTIEN] [numeric](18, 0) NULL,
	[SOTIENTRA] [numeric](18, 0) NULL,
	[CONLAI] [numeric](18, 0) NULL,
	[NGUOIXUAT] [int] NULL,
 CONSTRAINT [PK_PHIEUXUATHANG] PRIMARY KEY CLUSTERED 
(
	[MAPHIEU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 03-Jun-17 5:56:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[MATK] [int] IDENTITY(1,1) NOT NULL,
	[TENDANGNHAP] [nchar](20) NULL,
	[PASSWORD] [text] NULL,
	[MANV] [int] NULL,
 CONSTRAINT [PK_TAIKHOAN] PRIMARY KEY CLUSTERED 
(
	[MATK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CTPX]  WITH CHECK ADD  CONSTRAINT [FK_CTPX_MATHANG] FOREIGN KEY([MAHANG])
REFERENCES [dbo].[MATHANG] ([MAHANG])
GO
ALTER TABLE [dbo].[CTPX] CHECK CONSTRAINT [FK_CTPX_MATHANG]
GO
ALTER TABLE [dbo].[CTPX]  WITH CHECK ADD  CONSTRAINT [FK_CTPX_PHIEUXUATHANG] FOREIGN KEY([MAPHIEU])
REFERENCES [dbo].[PHIEUXUATHANG] ([MAPHIEU])
GO
ALTER TABLE [dbo].[CTPX] CHECK CONSTRAINT [FK_CTPX_PHIEUXUATHANG]
GO
ALTER TABLE [dbo].[DAILY]  WITH CHECK ADD  CONSTRAINT [FK_DAILY_LOAIDL] FOREIGN KEY([LOAIDL])
REFERENCES [dbo].[LOAIDL] ([MALOAI])
GO
ALTER TABLE [dbo].[DAILY] CHECK CONSTRAINT [FK_DAILY_LOAIDL]
GO
ALTER TABLE [dbo].[DAILY]  WITH CHECK ADD  CONSTRAINT [FK_DAILY_QUAN] FOREIGN KEY([MAQUAN])
REFERENCES [dbo].[QUAN] ([MAQUAN])
GO
ALTER TABLE [dbo].[DAILY] CHECK CONSTRAINT [FK_DAILY_QUAN]
GO
ALTER TABLE [dbo].[MATHANG]  WITH CHECK ADD  CONSTRAINT [FK_MATHANG_DVT] FOREIGN KEY([MADVT])
REFERENCES [dbo].[DVT] ([MADVT])
GO
ALTER TABLE [dbo].[MATHANG] CHECK CONSTRAINT [FK_MATHANG_DVT]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [FK_NHANVIEN_MACHUCVU_CHUCVU] FOREIGN KEY([MACHUCVU])
REFERENCES [dbo].[CHUCVU] ([MACHUCVU])
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [FK_NHANVIEN_MACHUCVU_CHUCVU]
GO
ALTER TABLE [dbo].[PHIEUTHUTIEN]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUTHUTIEN_DAILY] FOREIGN KEY([MADL])
REFERENCES [dbo].[DAILY] ([MADL])
GO
ALTER TABLE [dbo].[PHIEUTHUTIEN] CHECK CONSTRAINT [FK_PHIEUTHUTIEN_DAILY]
GO
ALTER TABLE [dbo].[PHIEUTHUTIEN]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUTHUTIEN_NHANVIEN] FOREIGN KEY([NGUOITHU])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[PHIEUTHUTIEN] CHECK CONSTRAINT [FK_PHIEUTHUTIEN_NHANVIEN]
GO
ALTER TABLE [dbo].[PHIEUXUATHANG]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUXUATHANG_DAILY] FOREIGN KEY([MADL])
REFERENCES [dbo].[DAILY] ([MADL])
GO
ALTER TABLE [dbo].[PHIEUXUATHANG] CHECK CONSTRAINT [FK_PHIEUXUATHANG_DAILY]
GO
ALTER TABLE [dbo].[PHIEUXUATHANG]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUXUATHANG_NHANVIEN] FOREIGN KEY([NGUOIXUAT])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[PHIEUXUATHANG] CHECK CONSTRAINT [FK_PHIEUXUATHANG_NHANVIEN]
GO
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD  CONSTRAINT [FK_TAIKHOAN_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[TAIKHOAN] CHECK CONSTRAINT [FK_TAIKHOAN_NHANVIEN]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DAILY"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 259
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LOAIDL"
            Begin Extent = 
               Top = 6
               Left = 255
               Bottom = 119
               Right = 425
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwDAILY-LOAIDL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwDAILY-LOAIDL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DAILY"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "LOAIDL"
            Begin Extent = 
               Top = 6
               Left = 255
               Bottom = 118
               Right = 425
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QUAN"
            Begin Extent = 
               Top = 6
               Left = 463
               Bottom = 118
               Right = 633
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwDAILY-LOAIDL-QUAN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwDAILY-LOAIDL-QUAN'
GO
USE [master]
GO
ALTER DATABASE [QLDL] SET  READ_WRITE 
GO
