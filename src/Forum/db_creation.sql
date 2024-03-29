USE [master]
GO
/****** Object:  Database [Forum]    Script Date: 1/9/2024 12:20:33 PM ******/
CREATE DATABASE [Forum]
GO
ALTER DATABASE [Forum] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Forum].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Forum] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Forum] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Forum] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Forum] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Forum] SET ARITHABORT OFF 
GO
ALTER DATABASE [Forum] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Forum] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Forum] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Forum] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Forum] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Forum] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Forum] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Forum] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Forum] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Forum] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Forum] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Forum] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Forum] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Forum] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Forum] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Forum] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Forum] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Forum] SET RECOVERY FULL 
GO
ALTER DATABASE [Forum] SET  MULTI_USER 
GO
ALTER DATABASE [Forum] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Forum] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Forum] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Forum] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Forum] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Forum] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Forum', N'ON'
GO
ALTER DATABASE [Forum] SET QUERY_STORE = OFF
GO
USE [Forum]
GO
/****** Object:  DatabaseRole [df]    Script Date: 1/9/2024 12:20:33 PM ******/
CREATE ROLE [df]
GO
/****** Object:  Schema [df]    Script Date: 1/9/2024 12:20:33 PM ******/
CREATE SCHEMA [df]
GO
/****** Object:  Schema [test2_loginu]    Script Date: 1/9/2024 12:20:33 PM ******/
CREATE SCHEMA [test2_loginu]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment_Likes]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment_Likes](
	[Comment_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Comment_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](1000) NOT NULL,
	[Date_Created] [datetime] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Post_Id] [int] NOT NULL,
	[Date_Edited] [datetime] NULL,
 CONSTRAINT [PK__Comments__3214EC07A447704F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post_Category]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post_Category](
	[Post_Id] [int] NOT NULL,
	[Category_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Post_Id] ASC,
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post_Likes]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post_Likes](
	[Post_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Post_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Text] [nvarchar](2000) NULL,
	[Date_Created] [datetime] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Date_Edited] [datetime] NULL,
 CONSTRAINT [PK__Posts__3214EC0796F960D3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Refresh_tokens]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Refresh_tokens](
	[User_Id] [int] NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Issued_at] [datetime] NOT NULL,
	[Expires_at] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Replies]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Replies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](1000) NOT NULL,
	[Date_Created] [datetime] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Reply_User_Id] [int] NULL,
	[Comment_Id] [int] NOT NULL,
	[Date_Edited] [datetime] NULL,
 CONSTRAINT [PK__Replies__3214EC07386022B5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reply_Likes]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reply_Likes](
	[Reply_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Reply_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Bio] [nvarchar](100) NULL,
	[Registered_At] [datetime] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Salt] [varchar](100) NOT NULL,
	[Role_Id] [int] NULL,
 CONSTRAINT [PK__Users__3214EC07DB1D5427] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [Email_unique] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [Username_unique] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF__Comments__Date__10566F31]  DEFAULT (getutcdate()) FOR [Date_Created]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF__Posts__Date__59063A47]  DEFAULT (getutcdate()) FOR [Date_Created]
GO
ALTER TABLE [dbo].[Replies] ADD  CONSTRAINT [DF__Replies__Date__5DCAEF64]  DEFAULT (getutcdate()) FOR [Date_Created]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Registere__267ABA7A]  DEFAULT (getutcdate()) FOR [Registered_At]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Role_Id__498EEC8D]  DEFAULT (NULL) FOR [Role_Id]
GO
ALTER TABLE [dbo].[Comment_Likes]  WITH CHECK ADD  CONSTRAINT [FK__Comment_L__Comme__29221CFB] FOREIGN KEY([Comment_Id])
REFERENCES [dbo].[Comments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment_Likes] CHECK CONSTRAINT [FK__Comment_L__Comme__29221CFB]
GO
ALTER TABLE [dbo].[Comment_Likes]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Likes_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comment_Likes] CHECK CONSTRAINT [FK_Comment_Likes_Users]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK__Comments__Post_I__123EB7A3] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK__Comments__Post_I__123EB7A3]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[Post_Category]  WITH CHECK ADD  CONSTRAINT [FK__Post_Cate__Post___0C85DE4D] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post_Category] CHECK CONSTRAINT [FK__Post_Cate__Post___0C85DE4D]
GO
ALTER TABLE [dbo].[Post_Category]  WITH CHECK ADD  CONSTRAINT [FK_Post_Category_Categories] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post_Category] CHECK CONSTRAINT [FK_Post_Category_Categories]
GO
ALTER TABLE [dbo].[Post_Likes]  WITH CHECK ADD  CONSTRAINT [FK__Post_Like__Post___02FC7413] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post_Likes] CHECK CONSTRAINT [FK__Post_Like__Post___02FC7413]
GO
ALTER TABLE [dbo].[Post_Likes]  WITH CHECK ADD  CONSTRAINT [FK_Post_Likes_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Post_Likes] CHECK CONSTRAINT [FK_Post_Likes_Users]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO
ALTER TABLE [dbo].[Replies]  WITH CHECK ADD  CONSTRAINT [FK_Replies_Comments] FOREIGN KEY([Comment_Id])
REFERENCES [dbo].[Comments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Replies] CHECK CONSTRAINT [FK_Replies_Comments]
GO
ALTER TABLE [dbo].[Replies]  WITH CHECK ADD  CONSTRAINT [FK_Replies_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Replies] CHECK CONSTRAINT [FK_Replies_Users]
GO
ALTER TABLE [dbo].[Replies]  WITH CHECK ADD  CONSTRAINT [FK_Replies_Users1] FOREIGN KEY([Reply_User_Id])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Replies] CHECK CONSTRAINT [FK_Replies_Users1]
GO
ALTER TABLE [dbo].[Reply_Likes]  WITH CHECK ADD  CONSTRAINT [FK__Reply_Lik__Reply__06CD04F7] FOREIGN KEY([Reply_Id])
REFERENCES [dbo].[Replies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reply_Likes] CHECK CONSTRAINT [FK__Reply_Lik__Reply__06CD04F7]
GO
ALTER TABLE [dbo].[Reply_Likes]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Likes_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Reply_Likes] CHECK CONSTRAINT [FK_Reply_Likes_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__Role_Id__4A8310C6] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__Role_Id__4A8310C6]
GO
/****** Object:  StoredProcedure [dbo].[proc_monthly_posts]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_monthly_posts] @Year int
AS

WITH AllMonths AS (
    SELECT
        month
    FROM
        (VALUES (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11), (12)) AS Months(month)
)

SELECT
    COUNT(p.id) AS posts
FROM
    AllMonths m
LEFT JOIN
    posts p ON m.month = MONTH(p.date_created) AND YEAR(p.date_created) = @Year
GROUP BY
    m.month, YEAR(p.date_created)
ORDER BY
    month;
GO
/****** Object:  StoredProcedure [dbo].[proc_monthly_users]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_monthly_users] @Year int
AS

WITH AllMonths AS (
    SELECT
        month
    FROM
        (VALUES (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11), (12)) AS Months(month)
)

SELECT
    COUNT(u.id) AS users
FROM
    AllMonths m
LEFT JOIN
    Users u ON m.month = MONTH(u.Registered_At) AND YEAR(u.Registered_At) = @Year
GROUP BY
    m.month, YEAR(u.Registered_At)
ORDER BY
    month;
GO
/****** Object:  Trigger [dbo].[update_comment_trigger]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[update_comment_trigger] ON [dbo].[Comments] After Update
AS

Update Comments set Date_Edited = GETUTCDATE() WHERE Id = (select Id from inserted)
GO
ALTER TABLE [dbo].[Comments] ENABLE TRIGGER [update_comment_trigger]
GO
/****** Object:  Trigger [dbo].[update_post_trigger]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[update_post_trigger] ON [dbo].[Posts] After Update
AS

Update Posts set Date_Edited = GETUTCDATE() WHERE Id = (select Id from inserted)
GO
ALTER TABLE [dbo].[Posts] ENABLE TRIGGER [update_post_trigger]
GO
/****** Object:  Trigger [dbo].[update_reply_trigger]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[update_reply_trigger] ON [dbo].[Replies] After Update
AS

Update Replies set Date_Edited = GETUTCDATE() WHERE Id = (select Id from inserted)
GO
ALTER TABLE [dbo].[Replies] ENABLE TRIGGER [update_reply_trigger]
GO
/****** Object:  Trigger [dbo].[delete_user_trigger]    Script Date: 1/9/2024 12:20:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[delete_user_trigger] ON [dbo].[Users] INSTEAD OF DELETE
AS
declare @id int = (select Id from deleted);

DELETE FROM Reply_Likes WHERE Reply_Likes.User_Id = @Id;
DELETE FROM Comment_Likes WHERE Comment_Likes.User_Id = @Id;
DELETE FROM Post_Likes WHERE Post_Likes.User_Id = @Id;
DELETE FROM Replies WHERE Replies.User_Id = @Id;
DELETE FROM Comments WHERE Comments.User_Id = @Id;
DELETE FROM Posts WHERE Posts.User_Id = @Id;
DELETE FROM Refresh_tokens WHERE Refresh_tokens.User_Id = @Id;
DELETE FROM Users WHERE Users.Id = @Id;


GO
ALTER TABLE [dbo].[Users] ENABLE TRIGGER [delete_user_trigger]
GO
USE [master]
GO
ALTER DATABASE [Forum] SET  READ_WRITE 
GO
