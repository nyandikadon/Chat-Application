USE [master]
GO
/****** Object:  Database [WebMessenger]    Script Date: 23/04/2015 15:59:49 ******/
CREATE DATABASE [WebMessenger] ON  PRIMARY 
( NAME = N'WebMessenger', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WebMessenger.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebMessenger_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WebMessenger_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebMessenger].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebMessenger] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebMessenger] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebMessenger] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebMessenger] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebMessenger] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebMessenger] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebMessenger] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WebMessenger] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebMessenger] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebMessenger] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebMessenger] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebMessenger] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebMessenger] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebMessenger] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebMessenger] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebMessenger] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebMessenger] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebMessenger] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebMessenger] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebMessenger] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebMessenger] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebMessenger] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebMessenger] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebMessenger] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebMessenger] SET  MULTI_USER 
GO
ALTER DATABASE [WebMessenger] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebMessenger] SET DB_CHAINING OFF 
GO
USE [WebMessenger]
GO
/****** Object:  StoredProcedure [dbo].[generate_serials]    Script Date: 23/04/2015 15:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[generate_serials](@code as varchar(10)) 
AS
BEGIN

	if not exists(select 1 from serials where code=@code )
	begin
		insert into serials(code ,value_count ,value)
		values(@code ,2,@code +'1')			
	end
	else
	begin
	   update serials
	   set value_count = value_count +1,value =@code + cast(value_count as varchar(10))
	   where code = @code 
	end
	
	select distinct value from serials where code = @code
	
	

END

GO
/****** Object:  Table [dbo].[friends]    Script Date: 23/04/2015 15:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[friends](
	[Index] [int] IDENTITY(1,1) NOT NULL,
	[friend_id] [nvarchar](50) NOT NULL,
	[username_1] [nvarchar](50) NOT NULL,
	[username_2] [nvarchar](50) NOT NULL,
	[status] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[messages]    Script Date: 23/04/2015 15:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[messages](
	[Index] [int] IDENTITY(1,1) NOT NULL,
	[user_from] [nvarchar](50) NOT NULL,
	[user_to] [nvarchar](50) NOT NULL,
	[message] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[serials]    Script Date: 23/04/2015 15:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[serials](
	[Index] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[value] [nvarchar](50) NOT NULL,
	[value_count] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 23/04/2015 15:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[Index] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [nvarchar](50) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](50) NOT NULL,
	[status] [nvarchar](50) NULL,
	[date_of_birth] [date] NOT NULL,
	[image] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[friends] ON 

INSERT [dbo].[friends] ([Index], [friend_id], [username_1], [username_2], [status]) VALUES (1, N'FRND1', N'nyandika', N'nomajnr', N'FRIENDS')
INSERT [dbo].[friends] ([Index], [friend_id], [username_1], [username_2], [status]) VALUES (2, N'FRND2', N'nyandika', N'bellah', N'FRIENDS')
SET IDENTITY_INSERT [dbo].[friends] OFF
SET IDENTITY_INSERT [dbo].[messages] ON 

INSERT [dbo].[messages] ([Index], [user_from], [user_to], [message], [status]) VALUES (1, N'nyandika', N'nomajnr', N'Vipi bro!', NULL)
INSERT [dbo].[messages] ([Index], [user_from], [user_to], [message], [status]) VALUES (2, N'nomajnr', N'nyandika', N'Sema mse!', NULL)
SET IDENTITY_INSERT [dbo].[messages] OFF
SET IDENTITY_INSERT [dbo].[serials] ON 

INSERT [dbo].[serials] ([Index], [code], [value], [value_count]) VALUES (1, N'USER', N'USER10', N'11')
INSERT [dbo].[serials] ([Index], [code], [value], [value_count]) VALUES (2, N'FRND', N'FRND2', N'3')
SET IDENTITY_INSERT [dbo].[serials] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([Index], [user_id], [username], [password], [first_name], [last_name], [gender], [status], [date_of_birth], [image]) VALUES (4, N'USR1', N'nyandika', N'1234', N'Nyandika', N'Donald', N'Male', N'OFFLINE', CAST(0x0F1A0B00 AS Date), NULL)
INSERT [dbo].[users] ([Index], [user_id], [username], [password], [first_name], [last_name], [gender], [status], [date_of_birth], [image]) VALUES (8, N'USER5', N'gisumwa', N'12345', N'Bismark', N'Gisumwa', N'Male', N'OFFLINE', CAST(0x73180B00 AS Date), NULL)
INSERT [dbo].[users] ([Index], [user_id], [username], [password], [first_name], [last_name], [gender], [status], [date_of_birth], [image]) VALUES (9, N'USER6', N'kioko', N'12345', N'Kelvin', N'Kioko', N'Male', N'OFFLINE', CAST(0x7D190B00 AS Date), NULL)
INSERT [dbo].[users] ([Index], [user_id], [username], [password], [first_name], [last_name], [gender], [status], [date_of_birth], [image]) VALUES (1002, N'USER7', N'bellah', N'12345', N'Christabel', N'Kemunto', N'Female', N'ONLINE', CAST(0xDE1C0B00 AS Date), NULL)
INSERT [dbo].[users] ([Index], [user_id], [username], [password], [first_name], [last_name], [gender], [status], [date_of_birth], [image]) VALUES (2005, N'USER8', N'pascal', N'12345', N'Pascal', N'Ondiek', N'Male', N'OFFLINE', CAST(0x5B950A00 AS Date), NULL)
INSERT [dbo].[users] ([Index], [user_id], [username], [password], [first_name], [last_name], [gender], [status], [date_of_birth], [image]) VALUES (2006, N'USER10', N'nomajnr', N'12345', N'Norbert', N'Maosa', N'Male', N'OFFLINE', CAST(0xF2140B00 AS Date), NULL)
SET IDENTITY_INSERT [dbo].[users] OFF
USE [master]
GO
ALTER DATABASE [WebMessenger] SET  READ_WRITE 
GO
