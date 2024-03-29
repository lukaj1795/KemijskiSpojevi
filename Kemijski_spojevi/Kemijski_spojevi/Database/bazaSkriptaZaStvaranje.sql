USE [master]
GO
/****** Object:  Database [BazaZaKemSpojeve]    Script Date: 19.8.2018. 22:59:51 ******/
CREATE DATABASE [BazaZaKemSpojeve]
 CONTAINMENT = NONE

ALTER DATABASE [BazaZaKemSpojeve2] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BazaZaKemSpojeve].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BazaZaKemSpojeve] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET ARITHABORT OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET RECOVERY FULL 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET  MULTI_USER 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BazaZaKemSpojeve] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BazaZaKemSpojeve] SET DELAYED_DURABILITY = DISABLED 
GO
USE [BazaZaKemSpojeve]
GO
/****** Object:  Table [dbo].[Element]    Script Date: 19.8.2018. 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Element](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Symbol] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Element] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Spoj]    Script Date: 19.8.2018. 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spoj](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TypeId] [int] NOT NULL,
 CONSTRAINT [PK_Spoj] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpojElement]    Script Date: 19.8.2018. 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpojElement](
	[SpojId] [int] NOT NULL,
	[ElementId] [int] NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_SpojElement] PRIMARY KEY CLUSTERED 
(
	[SpojId] ASC,
	[ElementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VrstaSpoja]    Script Date: 19.8.2018. 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VrstaSpoja](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_VrstaSpoja] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Element] ON 

INSERT [dbo].[Element] ([Id], [Name], [Symbol]) VALUES (1, N'Vodik', N'H         ')
INSERT [dbo].[Element] ([Id], [Name], [Symbol]) VALUES (2, N'Dušik', N'N         ')
INSERT [dbo].[Element] ([Id], [Name], [Symbol]) VALUES (3, N'Ugljik', N'C         ')
INSERT [dbo].[Element] ([Id], [Name], [Symbol]) VALUES (4, N'Natrij', N'Na        ')
INSERT [dbo].[Element] ([Id], [Name], [Symbol]) VALUES (5, N'Kisik', N'O         ')
SET IDENTITY_INSERT [dbo].[Element] OFF
SET IDENTITY_INSERT [dbo].[Spoj] ON 

INSERT [dbo].[Spoj] ([Id], [Name], [TypeId]) VALUES (1, N'Voda', 3)
INSERT [dbo].[Spoj] ([Id], [Name], [TypeId]) VALUES (13, N'Metan', 1)
SET IDENTITY_INSERT [dbo].[Spoj] OFF
INSERT [dbo].[SpojElement] ([SpojId], [ElementId], [Count]) VALUES (1, 1, 2)
INSERT [dbo].[SpojElement] ([SpojId], [ElementId], [Count]) VALUES (1, 5, 1)
INSERT [dbo].[SpojElement] ([SpojId], [ElementId], [Count]) VALUES (13, 1, 4)
INSERT [dbo].[SpojElement] ([SpojId], [ElementId], [Count]) VALUES (13, 3, 1)
SET IDENTITY_INSERT [dbo].[VrstaSpoja] ON 

INSERT [dbo].[VrstaSpoja] ([Id], [Name]) VALUES (1, N'Organski')
INSERT [dbo].[VrstaSpoja] ([Id], [Name]) VALUES (2, N'Anorganski')
INSERT [dbo].[VrstaSpoja] ([Id], [Name]) VALUES (3, N'Kovalentni')
INSERT [dbo].[VrstaSpoja] ([Id], [Name]) VALUES (4, N'Ionski')
SET IDENTITY_INSERT [dbo].[VrstaSpoja] OFF
ALTER TABLE [dbo].[Spoj]  WITH CHECK ADD  CONSTRAINT [FK_Spoj_VrstaSpoja] FOREIGN KEY([TypeId])
REFERENCES [dbo].[VrstaSpoja] ([Id])
GO
ALTER TABLE [dbo].[Spoj] CHECK CONSTRAINT [FK_Spoj_VrstaSpoja]
GO
ALTER TABLE [dbo].[SpojElement]  WITH CHECK ADD  CONSTRAINT [FK_SpojElement_Element] FOREIGN KEY([ElementId])
REFERENCES [dbo].[Element] ([Id])
GO
ALTER TABLE [dbo].[SpojElement] CHECK CONSTRAINT [FK_SpojElement_Element]
GO
ALTER TABLE [dbo].[SpojElement]  WITH CHECK ADD  CONSTRAINT [FK_SpojElement_Spoj] FOREIGN KEY([SpojId])
REFERENCES [dbo].[Spoj] ([Id])
GO
ALTER TABLE [dbo].[SpojElement] CHECK CONSTRAINT [FK_SpojElement_Spoj]
GO
USE [master]
GO
ALTER DATABASE [BazaZaKemSpojeve2] SET  READ_WRITE 
GO
