USE [master]
GO
/****** Object:  Database [MostriVsEroi]    Script Date: 03/09/2021 14:51:56 ******/
CREATE DATABASE [MostriVsEroi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MostriVsEroi', FILENAME = N'C:\Users\Laura Martines\MostriVsEroi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MostriVsEroi_log', FILENAME = N'C:\Users\Laura Martines\MostriVsEroi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MostriVsEroi] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MostriVsEroi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ARITHABORT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MostriVsEroi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MostriVsEroi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MostriVsEroi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MostriVsEroi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MostriVsEroi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MostriVsEroi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MostriVsEroi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MostriVsEroi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MostriVsEroi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MostriVsEroi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MostriVsEroi] SET  MULTI_USER 
GO
ALTER DATABASE [MostriVsEroi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MostriVsEroi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MostriVsEroi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MostriVsEroi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MostriVsEroi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MostriVsEroi] SET QUERY_STORE = OFF
GO
USE [MostriVsEroi]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [MostriVsEroi]
GO
/****** Object:  Table [dbo].[Arma]    Script Date: 03/09/2021 14:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Arma](
	[ID] [int] NOT NULL,
	[CategoriaID] [int] NOT NULL,
	[Nome] [nvarchar](20) NOT NULL,
	[PuntiDanno] [int] NOT NULL,
 CONSTRAINT [PK_Arma] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[CategoriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 03/09/2021 14:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID] [int] NOT NULL,
	[Nome] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eroe]    Script Date: 03/09/2021 14:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eroe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](20) NOT NULL,
	[CategoriaID] [int] NOT NULL,
	[ArmaID] [int] NOT NULL,
	[Livello] [int] NOT NULL,
	[PuntiAccumulati] [int] NOT NULL,
	[UsernameOwner] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Ero2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Giocatore]    Script Date: 03/09/2021 14:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Giocatore](
	[Username] [nvarchar](15) NOT NULL,
	[Psswrd] [nvarchar](15) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Giocatore] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mostro]    Script Date: 03/09/2021 14:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mostro](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](20) NOT NULL,
	[CategoriaID] [int] NOT NULL,
	[ArmaID] [int] NOT NULL,
	[Livello] [int] NOT NULL,
 CONSTRAINT [PK_Mostro] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (101, 101, N'Alabarda', 15)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (102, 101, N'Ascia', 8)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (103, 101, N'Mazza', 5)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (104, 101, N'Spada', 10)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (105, 101, N'Spadone', 15)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (201, 102, N'Arco e frecce', 8)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (202, 102, N'Bacchetta', 5)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (203, 102, N'Bastone magico', 10)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (204, 102, N'Onda d''urto', 15)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (205, 102, N'Pugnale', 5)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (301, 201, N'Discorso noioso', 4)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (302, 201, N'Farneticazione', 7)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (303, 201, N'Imprecazione', 5)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (304, 201, N'Magia nera', 3)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (401, 202, N'Arco', 7)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (402, 202, N'Clava', 5)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (403, 202, N'Spada rotta', 3)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (404, 202, N'Mazza chiodata', 10)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (501, 203, N'Alabarda del drago', 30)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (502, 203, N'Divinazione', 15)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (503, 203, N'Fulmine', 10)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (504, 203, N'Fulmine celeste', 15)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (505, 203, N'Tempesta', 8)
INSERT [dbo].[Arma] ([ID], [CategoriaID], [Nome], [PuntiDanno]) VALUES (506, 203, N'tempesta oscura', 15)
GO
INSERT [dbo].[Categoria] ([ID], [Nome]) VALUES (101, N'Guerriero')
INSERT [dbo].[Categoria] ([ID], [Nome]) VALUES (102, N'Mago')
INSERT [dbo].[Categoria] ([ID], [Nome]) VALUES (201, N'Cultista')
INSERT [dbo].[Categoria] ([ID], [Nome]) VALUES (202, N'Orco')
INSERT [dbo].[Categoria] ([ID], [Nome]) VALUES (203, N'Signore del Male')
GO
SET IDENTITY_INSERT [dbo].[Eroe] ON 

INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (1, N'Gandalf', 102, 204, 4, 10, N'MicTor')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (2, N'Merlino', 102, 203, 4, 60, N'GiuBia')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (4, N'Silente', 102, 202, 5, 0, N'LuiVer')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (5, N'Guerriero1', 101, 102, 2, 0, N'Kikka')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (6, N'Forzuto', 101, 105, 2, 15, N'GiuBia')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (7, N'Guerriero2', 101, 101, 3, 0, N'LaPoetessa')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (8, N'Guerriero3', 101, 105, 5, 167, N'MicTor')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (10, N'Guerrierissimo', 101, 102, 4, 35, N'ValRos')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (11, N'Scarsone', 101, 101, 1, 15, N'Kikka')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (12, N'bohboh', 102, 202, 2, 22, N'LaPoetessa')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (13, N'ciaone ', 102, 203, 1, 12, N'kikka')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (1003, N'Magomago', 101, 105, 2, 20, N'GiuBia')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (1004, N'forzuto', 101, 104, 1, 0, N'ValRos')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (1009, N'Scarsissimo', 101, 104, 1, 0, N'kikka')
INSERT [dbo].[Eroe] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello], [PuntiAccumulati], [UsernameOwner]) VALUES (1010, N'speroMeglio', 102, 204, 1, 10, N'SabWer')
SET IDENTITY_INSERT [dbo].[Eroe] OFF
GO
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'GiuBia', N'bohboh12', 1)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'kikka', N'K!kk4', 0)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'LaPoetessa', N'nelMezzodelCam', 1)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'Laua', N'laua', 0)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'LuiVer', N'ciao1', 1)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'MicTor', N'PortoRico', 1)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'SabWer', N'querty', 0)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'TheDealer', N'macheneso98', 0)
INSERT [dbo].[Giocatore] ([Username], [Psswrd], [IsAdmin]) VALUES (N'ValRos', N'12345', 1)
GO
SET IDENTITY_INSERT [dbo].[Mostro] ON 

INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (4, N'Mostro1', 202, 402, 2)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (6, N'Cultista1', 201, 301, 1)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (7, N'Signore1', 203, 502, 3)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (8, N'Orco2', 202, 403, 4)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (9, N'Cultista2', 201, 301, 2)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (10, N'Mostraccio', 203, 504, 5)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (1003, N'Orco3', 202, 401, 1)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (1004, N'SigMale', 203, 503, 1)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (1005, N'Mostrerrimo', 203, 506, 3)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (2002, N'Tenebroso', 203, 501, 2)
INSERT [dbo].[Mostro] ([ID], [Nome], [CategoriaID], [ArmaID], [Livello]) VALUES (2003, N'nomecorto', 202, 401, 3)
SET IDENTITY_INSERT [dbo].[Mostro] OFF
GO
/****** Object:  Index [UQ__Arma__3214EC26A202D0A0]    Script Date: 03/09/2021 14:51:57 ******/
ALTER TABLE [dbo].[Arma] ADD UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Arma]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaArmaE] FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[Arma] CHECK CONSTRAINT [FK_CategoriaArmaE]
GO
ALTER TABLE [dbo].[Eroe]  WITH CHECK ADD  CONSTRAINT [FK_ArmaEroe] FOREIGN KEY([ArmaID], [CategoriaID])
REFERENCES [dbo].[Arma] ([ID], [CategoriaID])
GO
ALTER TABLE [dbo].[Eroe] CHECK CONSTRAINT [FK_ArmaEroe]
GO
ALTER TABLE [dbo].[Eroe]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaEroe] FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[Eroe] CHECK CONSTRAINT [FK_CategoriaEroe]
GO
ALTER TABLE [dbo].[Eroe]  WITH CHECK ADD  CONSTRAINT [FK_GiocatoreEroe] FOREIGN KEY([UsernameOwner])
REFERENCES [dbo].[Giocatore] ([Username])
GO
ALTER TABLE [dbo].[Eroe] CHECK CONSTRAINT [FK_GiocatoreEroe]
GO
ALTER TABLE [dbo].[Mostro]  WITH CHECK ADD  CONSTRAINT [FK_ArmaMostro] FOREIGN KEY([ArmaID], [CategoriaID])
REFERENCES [dbo].[Arma] ([ID], [CategoriaID])
GO
ALTER TABLE [dbo].[Mostro] CHECK CONSTRAINT [FK_ArmaMostro]
GO
ALTER TABLE [dbo].[Mostro]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaMostro] FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[Mostro] CHECK CONSTRAINT [FK_CategoriaMostro]
GO
ALTER TABLE [dbo].[Eroe]  WITH CHECK ADD CHECK  (([Livello]<=(5)))
GO
ALTER TABLE [dbo].[Eroe]  WITH CHECK ADD  CONSTRAINT [CK_Categoria] CHECK  (([CategoriaID]=(102) OR [CategoriaID]=(101)))
GO
ALTER TABLE [dbo].[Eroe] CHECK CONSTRAINT [CK_Categoria]
GO
ALTER TABLE [dbo].[Eroe]  WITH CHECK ADD  CONSTRAINT [Ck_Livello_Punti] CHECK  (([Livello]=(1) AND [Puntiaccumulati]>=(0) AND [PuntiAccumulati]<(30) OR [Livello]=(2) AND [Puntiaccumulati]>=(0) AND [PuntiAccumulati]<(60) OR [Livello]=(3) AND [Puntiaccumulati]>=(0) AND [PuntiAccumulati]<(90) OR [Livello]=(4) AND [Puntiaccumulati]>=(0) AND [PuntiAccumulati]<(120) OR [Livello]=(5) AND [PuntiAccumulati]>=(0)))
GO
ALTER TABLE [dbo].[Eroe] CHECK CONSTRAINT [Ck_Livello_Punti]
GO
ALTER TABLE [dbo].[Mostro]  WITH CHECK ADD  CONSTRAINT [CK_CategoriaMostro] CHECK  (([CategoriaID]=(203) OR [CategoriaID]=(202) OR [CategoriaID]=(201)))
GO
ALTER TABLE [dbo].[Mostro] CHECK CONSTRAINT [CK_CategoriaMostro]
GO
USE [master]
GO
ALTER DATABASE [MostriVsEroi] SET  READ_WRITE 
GO
