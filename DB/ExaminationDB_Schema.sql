USE [master]
GO
/****** Object:  Database [Examination2]    Script Date: 6/4/2022 2:06:52 PM ******/
CREATE DATABASE [Examination2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Examination2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Examination2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Examination2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Examination2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Examination2] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Examination2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Examination2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Examination2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Examination2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Examination2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Examination2] SET ARITHABORT OFF 
GO
ALTER DATABASE [Examination2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Examination2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Examination2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Examination2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Examination2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Examination2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Examination2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Examination2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Examination2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Examination2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Examination2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Examination2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Examination2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Examination2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Examination2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Examination2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Examination2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Examination2] SET RECOVERY FULL 
GO
ALTER DATABASE [Examination2] SET  MULTI_USER 
GO
ALTER DATABASE [Examination2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Examination2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Examination2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Examination2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Examination2] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Examination2', N'ON'
GO
ALTER DATABASE [Examination2] SET QUERY_STORE = OFF
GO
USE [Examination2]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Examination2]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 6/4/2022 2:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [text] NULL,
	[QuestionID] [int] NOT NULL,
	[IsValid] [bit] NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 6/4/2022 2:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [text] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 6/4/2022 2:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](255) NULL,
	[Difficulty] [int] NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestQuestions]    Script Date: 6/4/2022 2:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TestID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
 CONSTRAINT [PK_TestQuestions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([ID])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Question]
GO
ALTER TABLE [dbo].[TestQuestions]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestions_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([ID])
GO
ALTER TABLE [dbo].[TestQuestions] CHECK CONSTRAINT [FK_TestQuestions_Question]
GO
ALTER TABLE [dbo].[TestQuestions]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestions_Test] FOREIGN KEY([TestID])
REFERENCES [dbo].[Test] ([ID])
GO
ALTER TABLE [dbo].[TestQuestions] CHECK CONSTRAINT [FK_TestQuestions_Test]
GO
USE [master]
GO
ALTER DATABASE [Examination2] SET  READ_WRITE 
GO
