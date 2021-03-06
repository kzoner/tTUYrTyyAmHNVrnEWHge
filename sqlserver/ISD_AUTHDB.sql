USE [ISD_AUTHDB]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnCheckAuthorizationOnResourceByUserName]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UfnCheckAuthorizationOnResourceByUserName](@UserName varchar(50), @ResourceID int)
	RETURNS bit
AS
BEGIN

IF @UserName = 'admin' OR @UserName = 'root' RETURN 1;

IF EXISTS ( SELECT *
			FROM dbo.Membership
			WHERE Username = @UserName AND (RoleID = 10000000 OR RoleID = 10000001))
	RETURN 1;

IF EXISTS(SELECT PermissionID FROM [Permissions] P 
							  WHERE P.ResourceID=@ResourceID 
								AND UserName=@UserName AND P.OperationCode=1 AND Allow=1)
	RETURN 1;

IF EXISTS(SELECT DISTINCT PermissionID 
			FROM [Permissions] P INNER JOIN Roles R ON P.RoleID=R.RoleID
								 INNER JOIN Membership M ON R.RoleID=M.RoleID
			WHERE M.UserName=@UserName 
					AND P.ResourceID=@ResourceID 
					AND P.OperationCode=1 
					AND P.Allow = 1
					AND NOT EXISTS(SELECT PermissionID 
									FROM [Permissions] P1
									WHERE P1.UserName=@UserName 
											AND P1.ResourceID=@ResourceID 
											AND P1.OperationCode=1 
											AND P1.Allow = 0))
	RETURN 1;
RETURN 0;
END


GO
/****** Object:  Table [dbo].[ActionLogs201708]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionLogs201708](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[IP] [varchar](19) NULL,
	[UserName] [nvarchar](50) NULL,
	[Path] [nvarchar](100) NULL,
	[PageTitle] [nvarchar](100) NULL,
	[Operation] [nvarchar](50) NULL,
	[Data] [ntext] NULL,
 CONSTRAINT [PK_ActionLogs201708] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[ActionLogs_CurrentView]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE VIEW [dbo].[ActionLogs_CurrentView]
	AS
	SELECT     ID, Date, IP, UserName, [Path], PageTitle, Operation, Data
	FROM ActionLogs201708
GO
/****** Object:  View [dbo].[ActionLogs_View]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ActionLogs_View] AS 
					SELECT ID, Date, IP, UserName, [Path], PageTitle, Operation, Data FROM ActionLogs201708

GO
/****** Object:  Table [dbo].[Applications]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(10000000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[Status] [tinyint] NULL,
	[Domain] [varchar](50) NULL,
	[Image] [varchar](50) NULL,
	[Token] [varchar](50) NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Auth_Log]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auth_Log](
	[UserName] [nvarchar](50) NULL,
	[Token] [varchar](50) NULL,
	[ResourceName] [nvarchar](250) NULL,
	[Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ErrorLogs]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLogs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[CurrentUser] [nvarchar](50) NULL,
	[Path] [nvarchar](200) NULL,
	[Message] [nvarchar](max) NULL,
	[StackTrace] [ntext] NULL,
	[TargetFunction] [varchar](100) NULL,
 CONSTRAINT [PK_ErrorLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoginLogs]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[IP] [varchar](19) NULL,
	[LoginResult] [int] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_LoginLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membership]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[Username] [varchar](128) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[Username] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuID] [int] IDENTITY(10000000,1) NOT NULL,
	[DisplayName] [nvarchar](100) NULL,
	[ResourceID] [int] NULL,
	[ParentMenuID] [int] NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messenger]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messenger](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](128) NULL,
	[FromUser] [varchar](128) NULL,
	[Messenger] [ntext] NULL,
	[CreateDate] [datetime] NULL,
	[IsNew] [bit] NULL,
 CONSTRAINT [PK_Messenger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OperationCategories]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationCategories](
	[OperationCode] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_OperationCategories] PRIMARY KEY CLUSTERED 
(
	[OperationCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Operations]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operations](
	[OperationCode] [int] NOT NULL,
	[ResourceTypeCode] [varchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[OperationCode] ASC,
	[ResourceTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionID] [int] IDENTITY(10000000,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[OperationCode] [int] NOT NULL,
	[Username] [varchar](128) NULL,
	[RoleID] [int] NULL,
	[Allow] [bit] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Questions]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(10000000,1) NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resources]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[ResourceID] [int] IDENTITY(10000000,1) NOT NULL,
	[ResourceTypeCode] [varchar](50) NOT NULL,
	[Path] [varchar](250) NULL,
	[FileName] [varchar](50) NULL,
	[Link] [varchar](250) NULL,
	[ApplicationID] [int] NOT NULL,
	[ResourceName] [nvarchar](250) NULL,
	[Status] [bit] NULL,
	[Token] [varchar](50) NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResourceTypes]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceTypes](
	[ResourceTypeCode] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_ResourceTypes] PRIMARY KEY CLUSTERED 
(
	[ResourceTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(10000000,1) NOT NULL,
	[ApplicationID] [int] NULL,
	[RoleCode] [nvarchar](50) NULL,
	[RoleName] [nvarchar](150) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [varchar](128) NOT NULL,
	[Password] [varbinary](150) NOT NULL,
	[FullName] [nvarchar](64) NULL,
	[Email] [varchar](128) NULL,
	[Blocked] [bit] NULL,
	[BlockedDate] [datetime] NULL,
	[CreatedTime] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[LastIP] [varchar](15) NULL,
	[FailPassword] [tinyint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Applications] ON 

INSERT [dbo].[Applications] ([ApplicationID], [Name], [Description], [Status], [Domain], [Image], [Token]) VALUES (10000006, N'Admin', N'Quản lý các tài nguyên và phân quyền trên hệ thống..', 0, N'beta.inside.gate.vn', N'NULL', NULL)
INSERT [dbo].[Applications] ([ApplicationID], [Name], [Description], [Status], [Domain], [Image], [Token]) VALUES (10000016, N'Inside System', N'Hệ thống Inside', 1, NULL, NULL, N'789D5669-1FA0-4E82-83F0-CA66B672B001')
INSERT [dbo].[Applications] ([ApplicationID], [Name], [Description], [Status], [Domain], [Image], [Token]) VALUES (10000017, N'Incident', N'Ứng dụng quản lý sự cố', 0, NULL, NULL, N'1C87E66F-0F65-4E86-B076-F6BBBD05372B')
INSERT [dbo].[Applications] ([ApplicationID], [Name], [Description], [Status], [Domain], [Image], [Token]) VALUES (10000018, N'test', N'sdf dsfsf', 0, NULL, NULL, N'F0FB2D75-5E47-4E0D-8351-7F8A2AAF2F25')
INSERT [dbo].[Applications] ([ApplicationID], [Name], [Description], [Status], [Domain], [Image], [Token]) VALUES (10000019, N'Statistic Report for Partners', N'Ứng dụng báo cáo số liệu cho đối tác', 0, NULL, NULL, N'61AFC831-396F-4E27-8A8A-F0A3971DA320')
INSERT [dbo].[Applications] ([ApplicationID], [Name], [Description], [Status], [Domain], [Image], [Token]) VALUES (10000020, N'Izon Outside', N'Trang duyệt đơn hàng cho Izon', 0, NULL, NULL, N'8F8BBFB1-6A70-4670-8F85-93C5F22356C3')
SET IDENTITY_INSERT [dbo].[Applications] OFF
SET IDENTITY_INSERT [dbo].[LoginLogs] ON 

INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (1, N'longndh', N'127.0.0.1', 1, CAST(N'2017-07-27T20:12:17.513' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (2, N'longndh', N'127.0.0.1', 1, CAST(N'2017-08-08T19:24:19.910' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (3, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:36:00.553' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (4, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.620' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (5, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.747' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (6, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.767' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (7, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.803' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (8, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.930' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (9, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.960' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (10, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:37.983' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (11, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.027' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (12, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.050' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (13, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.070' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (14, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.093' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (15, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.200' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (16, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.220' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (17, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.243' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (18, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.263' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (19, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.283' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (20, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.303' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (21, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.323' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (22, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.343' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (23, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.373' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (24, N'longndh', N'::1', 1, CAST(N'2017-08-08T19:40:38.400' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (25, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:07.910' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (26, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.097' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (27, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.147' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (28, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.180' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (29, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.207' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (30, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.227' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (31, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.277' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (32, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.310' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (33, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.367' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (34, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.400' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (35, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.537' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (36, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.617' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (37, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.657' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (38, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.687' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (39, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.707' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (40, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.723' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (41, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.783' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (42, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.847' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (43, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.907' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (44, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:08.957' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (45, N'longndh', N'::1', 1, CAST(N'2017-08-09T22:32:09.007' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (46, N'longndh', N'127.0.0.1', 1, CAST(N'2017-08-16T20:11:14.173' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (47, N'admin', N'127.0.0.1', 1, CAST(N'2017-08-16T20:23:15.080' AS DateTime))
INSERT [dbo].[LoginLogs] ([ID], [UserName], [IP], [LoginResult], [Date]) VALUES (48, N'luupt', N'127.0.0.1', 1, CAST(N'2017-08-16T20:45:25.627' AS DateTime))
SET IDENTITY_INSERT [dbo].[LoginLogs] OFF
INSERT [dbo].[Membership] ([Username], [RoleID]) VALUES (N'admin', 10000000)
INSERT [dbo].[Membership] ([Username], [RoleID]) VALUES (N'longndh', 10000000)
INSERT [dbo].[Membership] ([Username], [RoleID]) VALUES (N'luupt', 10000002)
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000000, N'Hệ thống', 10000028, 10000023, 10000000)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000001, N'Tài khoản', 10000002, 10000000, 10000001)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000010, N'Ứng dụng', 10000008, 10000000, 10000010)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000021, N'Reset mật khẩu', 10000027, 10000001, 10000026)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000023, N'Chức năng', 10000031, 0, 10000023)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000024, N'Danh sách tài khoản', 10000019, 10000001, 10000021)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000025, N'Danh sách nhóm', 10000007, 10000001, 10000024)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000026, N'Danh sách thao tác', 10000016, 10000001, 10000025)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000030, N'Danh sách loại tài nguyên', 10000010, 10000010, 10000032)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000031, N'Danh sách tài nguyên', 10000011, 10000010, 10000031)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000032, N'Danh sách ứng dụng', 10000013, 10000010, 10000033)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000033, N'Danh sách menu', 10000018, 10000010, 10000030)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000445, N'Error Log', 10000518, 10000713, 10000445)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000713, N'Nhật ký', 10000824, 10000000, 10000713)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000714, N'Action Logs', 10000825, 10000713, 10000714)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000715, N'Login Logs', 10000826, 10000713, 10000715)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10000731, N'Xem quyền user', 10000846, 10000001, 10000731)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10002448, N'Đối tác - Khách hàng', 10002665, 10000023, 10002448)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10002450, N'Sản phẩm', 10002668, 10000023, 10002450)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10002452, N'Xuất - Nhập', 10002670, 10000023, 10002452)
INSERT [dbo].[Menus] ([MenuID], [DisplayName], [ResourceID], [ParentMenuID], [Priority]) VALUES (10002454, N'Chi phí', 10002671, 10000023, 10002454)
SET IDENTITY_INSERT [dbo].[Menus] OFF
SET IDENTITY_INSERT [dbo].[OperationCategories] ON 

INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (1, N'VIEW', N'Thao tác truy vấn dữ liệu')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (2, N'INSERT', N'Thao tác thêm mới dữ liệu')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (3, N'UPDATE', N'Thao tác cập nhật dữ liệu')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (4, N'DELETE', N'Thao tác xóa dữ liệu')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (5, N'IMPORT', N'Nhập dữ liệu vào hệ thống')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (6, N'EXPORT', N'Trích xuất dữ liệu từ hệ thống')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (7, N'PUBLISH', N'Đăng tin lên trang chủ')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (8, N'APPROVE', N'Thao tác kiểm duyệt, chấp thuận')
INSERT [dbo].[OperationCategories] ([OperationCode], [Name], [Description]) VALUES (9, N'SEARCH', N'Tìm kiếm')
SET IDENTITY_INSERT [dbo].[OperationCategories] OFF
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (1, N'DEFAULT', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (1, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (2, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (3, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (4, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (5, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (6, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (7, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (8, N'WEB_FORM', NULL)
INSERT [dbo].[Operations] ([OperationCode], [ResourceTypeCode], [Description]) VALUES (9, N'WEB_FORM', NULL)
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000187, 10000028, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000189, 10000002, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000190, 10000027, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000191, 10000027, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000192, 10000027, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000193, 10000027, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000194, 10000027, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000195, 10000027, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000196, 10000027, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000197, 10000027, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000198, 10000019, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000199, 10000019, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000200, 10000019, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000201, 10000019, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000202, 10000019, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000203, 10000019, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000204, 10000019, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000205, 10000019, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000206, 10000007, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000207, 10000007, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000208, 10000007, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000209, 10000007, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000210, 10000007, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000211, 10000007, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000212, 10000007, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000213, 10000007, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000214, 10000016, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000215, 10000016, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000216, 10000016, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000217, 10000016, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000218, 10000016, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000219, 10000016, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000220, 10000016, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000221, 10000016, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10000222, 10000008, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009766, 10000018, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009767, 10000018, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009768, 10000018, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009769, 10000018, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009770, 10000018, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009771, 10000018, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009772, 10000018, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009773, 10000018, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009774, 10000011, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009775, 10000011, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009776, 10000011, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009777, 10000011, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009778, 10000011, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009779, 10000011, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009780, 10000011, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009781, 10000011, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009782, 10000010, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009783, 10000010, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009784, 10000010, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009785, 10000010, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009786, 10000010, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009787, 10000010, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009788, 10000010, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009789, 10000010, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009790, 10000013, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009791, 10000013, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009792, 10000013, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009793, 10000013, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009794, 10000013, 5, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009795, 10000013, 6, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009796, 10000013, 7, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009797, 10000013, 8, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009854, 10000020, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009855, 10000020, 2, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009856, 10000020, 3, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10009857, 10000020, 4, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048211, 10000031, 1, NULL, 10000000, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048212, 10000031, 1, NULL, 10000001, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048213, 10000031, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048214, 10002671, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048215, 10002680, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048216, 10002681, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048217, 10002679, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048218, 10002665, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048219, 10002675, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048220, 10002674, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048221, 10002668, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048222, 10002673, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048223, 10002672, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048224, 10002670, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048225, 10002677, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048226, 10002678, 1, NULL, 10000002, 1)
INSERT [dbo].[Permissions] ([PermissionID], [ResourceID], [OperationCode], [Username], [RoleID], [Allow]) VALUES (10048227, 10002676, 1, NULL, 10000002, 1)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
SET IDENTITY_INSERT [dbo].[Resources] ON 

INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000002, N'DEFAULT', N'', N'', N'', 10000016, N'Hệ thống_Tài khoản', 1, N'DEFAULT')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000004, N'WEB_FORM', N'/Admin/Users/Create/', N'Default.aspx', N'/Admin/Users/Create/Default.aspx', 10000016, N'Hệ Thống_Tài khoản_Tạo tài khoản', 1, N'C2EAF301-7607-4A42-A971-A4F6FDDCFE7F')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000006, N'WEB_FORM', N'/Admin/Roles/Create/', N'Default.aspx', N'/Admin/Roles/Create/Default.aspx', 10000016, N'Hệ thống_Danh sách nhóm_Tạo nhóm', 1, N'82E6945F-D257-4A7D-AEA6-500114CC1BDA')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000007, N'WEB_FORM', N'/Admin/Roles/Manage/', N'Default.aspx', N'/Admin/Roles/Manage/Default.aspx', 10000016, N'Hệ thống_Tài khoản_Danh sách nhóm', 1, N'1F6835C4-796C-465C-9EB0-95949AE157E1')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000008, N'DEFAULT', N'', N'', N'', 10000016, N'Hệ thống_Ứng dụng', 1, N'DEFAULT')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000009, N'WEB_FORM', N'/Admin/Resources/Add/', N'Default.aspx', N'/Admin/Resources/Add/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Tạo tài nguyên', 1, N'F4F6B131-6F12-4164-BBDD-7425021440AA')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000010, N'WEB_FORM', N'/Admin/Resources/ResourceTypes/', N'Default.aspx', N'/Admin/Resources/ResourceTypes/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Quản lý loại tài nguyên', 1, N'540B8997-7DA3-44E5-A2B0-544DAB42FD24')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000011, N'WEB_FORM', N'/Admin/Resources/Manage/', N'Default.aspx', N'/Admin/Resources/Manage/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Quản lý tài nguyên', 1, N'6041BDB8-377C-4A92-8EFF-E17AFB7BA862')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000013, N'WEB_FORM', N'/Admin/Application/Manage/', N'Default.aspx', N'/Admin/Application/Manage/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Danh sách ứng dụng', 1, N'007947F0-DDCA-4354-AFE5-2FAFA27CC8DD')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000014, N'WEB_FORM', N'/Admin/Application/Create/', N'Default.aspx', N'/Admin/Application/Create/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Danh sách ứng dụng_Thêm mới ứng ụng', 1, N'605C5B59-D58E-4149-8F67-C2ACE6DCCAAA')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000016, N'WEB_FORM', N'/Admin/OperationCat/Manage/', N'Default.aspx', N'/Admin/OperationCat/Manage/Default.aspx', 10000016, N'Hệ Thống_Tài khoản_Danh sách loại thao tác', 1, N'EC258DE0-2E43-4230-84CF-085F3CEBB876')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000017, N'WEB_FORM', N'/Admin/OperationCat/Create/', N'Default.aspx', N'/Admin/OperationCat/Create/Default.aspx', 10000016, N'Hệ thống_Tài khoản_Thêm mới loại thao tác', 1, N'DA84FA7C-57C9-4BC8-9171-58A59F67F1A2')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000018, N'WEB_FORM', N'/Admin/Menus/Manage/', N'Default.aspx', N'/Admin/Menus/Manage/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Quản lý menu', 1, N'18A1C5D8-7FF7-4126-937E-572BDC508C47')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000019, N'WEB_FORM', N'/Admin/Users/Manage/', N'Default.aspx', N'/Admin/Users/Manage/Default.aspx', 10000016, N'Hệ Thống_Tài khoản_Danh sách tài khoản', 1, N'6970B412-C6B9-4150-9D0E-1C935F87F835')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000020, N'WEB_FORM', N'/Admin/Resources/Edit/', N'Default.aspx', N'/Admin/Resources/Edit/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Cập nhật tài nguyên', 1, N'6184317B-6C05-471B-8CFE-0F5ED50A9879')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000021, N'WEB_FORM', N'/Admin/Resources/SetPermission/', N'Default.aspx', N'/Admin/Resources/SetPermission/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Phân quyền cho tài nguyên', 1, N'4336E033-865D-4E98-B1F8-33693CD67A90')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000022, N'WEB_FORM', N'/Admin/Users/Update/', N'Default.aspx', N'/Admin/Users/Update/Default.aspx', 10000016, N'Hệ thống_Tài khoản_Cập nhật tài khoản', 1, N'36E6991C-0347-44CC-A8E0-E7D8E3288530')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000023, N'WEB_FORM', N'/Admin/Users/UserInRole/', N'Default.aspx', N'/Admin/Users/UserInRole/', 10000016, N'Hệ thống_Tài khoản_Danh sách nhóm_Quản lý user trong role', 1, N'2FBC2869-A5F2-4A6F-813D-CD819989E109')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000024, N'WEB_FORM', N'/Admin/Roles/Update/', N'Default.aspx', N'/Admin/Roles/Update/Default.aspx', 10000016, N'Hệ thống_Tài khoản_Cập nhật role', 1, N'0647FF94-92C0-4E7E-A32D-D49C50839BB0')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000025, N'WEB_FORM', N'/Admin/Application/Update/', N'Default.aspx', N'/Admin/Application/Update/Default.aspx', 10000016, N'Hệ thống_Ứng dụng_Cập nhật ứng dụng', 1, N'7A9B00AE-B85B-4DD1-B4EC-6548C6562B50')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000026, N'WEB_FORM', N'/Admin/OperationCat/Edit/', N'Default.aspx', N'/Admin/OperationCat/Edit/Default.aspx', 10000016, N'Hệ thống_Tài khoản_Cập nhật loại thao tác', 1, N'9B1F492D-698F-4DD7-9EF4-ADFF69D73606')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000027, N'WEB_FORM', N'/Admin/Users/Manage/ResetPwd/', N'Default.aspx', N'/Admin/Users/Manage/ResetPwd/', 10000016, N'Hệ thống_Tài khoản_Reset mật khẩu', 1, N'453B84E9-1BCE-44BD-A7E6-7F7EE579C118')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000028, N'DEFAULT', N'', N'', N'', 10000016, N'Hệ thống', 1, N'DEFAULT')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000031, N'DEFAULT', N'/', N'Default.aspx', N'/', 10000016, N'Inside', 1, N'789D5669-1FA0-4E82-83F0-CA66B672B001')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000518, N'WEB_FORM', N'/Admin/ErrorLog/', N'Default.aspx', N'/Admin/ErrorLog/', 10000016, N'Hệ thống_Nhật ký_ErrorLog', 1, N'5AFF9742-390A-4E49-BC39-1BDA4B7EBEBB')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000824, N'DEFAULT', N'', N'', N'', 10000016, N'Hệ thống_Nhật Ký', 1, N'DEFAULT')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000825, N'WEB_FORM', N'/Admin/ActionLogs/', N'Default.aspx', N'/Admin/ActionLogs/', 10000016, N'Hệ Thống_Nhật ký_ActionLogs', 1, N'C2ABB748-FF39-4FFF-BA3C-AB024367E99C')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000826, N'WEB_FORM', N'/Admin/LoginLogs/', N'Default.aspx', N'/Admin/LoginLogs/', 10000016, N'Hệ Thống_Nhật ký_LoginLogs', 1, N'274D7D25-AD73-4621-915D-2E21DDBD0ECE')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10000846, N'WEB_FORM', N'/Admin/Users/RemovePermission/', N'Default.aspx', N'/Admin/Users/RemovePermission/', 10000016, N'Hệ thống_Tài khoản_Gỡ bỏ quyền user', 1, N'DAC10BF3-FC7B-4851-92A2-89EC9F253269')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002665, N'WEB_FORM', N'/Function/Account/', N'Default.aspx', N'/Function/Account/Search/Default.aspx', 10000016, N'Inside_Đối tác_Khách hàng', 1, N'493925DE-175A-4811-9FC5-766D94868753')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002668, N'WEB_FORM', N'/Function/Product/', N'Default.aspx', N'/Function/Product/Default.aspx', 10000016, N'Inside_Sản phẩm', 1, N'4212EE6F-E3FB-4C55-B874-F83B5700C8D5')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002670, N'WEB_FORM', N'/Function/Order/', N'Default.aspx', N'/Function/Order/Default.aspx', 10000016, N'Inside_Xuất_Nhập', 1, N'566A63D5-6429-47AE-A94C-4D54DF3B74AC')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002671, N'WEB_FORM', N'/Function/Fee/', N'Default.aspx', N'/Function/Fee/Default.aspx', 10000016, N'Inside_Chi phí', 1, N'F813CD4B-8A45-4A99-8802-986E8F2DE642')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002672, N'WEB_FORM', N'/Function/Product/Add/', N'Default.aspx', N'/Function/Product/Add/Default.aspx', 10000016, N'Inside_Sản phẩm_Thêm mới', 1, N'5C716BEB-9647-43AB-BC92-216B676ED2C9')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002673, N'WEB_FORM', N'/Function/Product/Edit/', N'Default.aspx', N'/Function/Product/Edit/Default.aspx', 10000016, N'Inside_Sản phẩm_Cập nhật', 1, N'145F1D7E-A756-4B4B-9582-6F25F41AC56B')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002674, N'WEB_FORM', N'/Function/Account/Add/', N'Default.aspx', N'/Function/Account/Add/Default.aspx', 10000016, N'Inside_Đối tác_Khách hàng_Thêm mới', 1, N'24D92393-704E-4F24-9C94-0274FCB0DDBA')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002675, N'WEB_FORM', N'/Function/Account/Edit/', N'Default.aspx', N'/Function/Account/Edit/Default.aspx', 10000016, N'Inside_Đối tác_Khách hàng_Cập nhật', 1, N'77B8FCDB-D981-4C0C-8D91-B862FBDB1F13')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002676, N'WEB_FORM', N'/Function/Order/Add/', N'Default.aspx', N'/Function/Order/Add/Default.aspx', 10000016, N'Inside_Xuất_Nhập_Thêm mới', 1, N'EDEF245C-9CEB-420D-BFA6-744A3FFD7096')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002677, N'WEB_FORM', N'/Function/Order/Edit/', N'Default.aspx', N'/Function/Order/Edit/Default.aspx', 10000016, N'Inside_Xuất_Nhập_Cập nhật', 1, N'C461C88F-79D8-425A-824E-3ED16E711D27')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002678, N'WEB_FORM', N'/Function/Order/Detail/', N'Default.aspx', N'/Function/Order/Detail/Default.aspx', 10000016, N'Inside_Xuất_Nhập_Chi tiết', 1, N'7CCF3109-8182-4541-9439-813C731B251C')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002679, N'WEB_FORM', N'/Function/Fee/Add/', N'Default.aspx', N'/Function/Fee/Add/Default.aspx', 10000016, N'Inside_Chi phí_Thêm mới', 1, N'9B02B705-64B1-47B4-86BF-6F3DE95AA85E')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002680, N'WEB_FORM', N'/Function/Fee/Edit/', N'Default.aspx', N'/Function/Fee/Edit/Default.aspx', 10000016, N'Inside_Chi phí_Cập nhật', 1, N'54A080EE-7F8E-43AD-9B67-138383AAD78F')
INSERT [dbo].[Resources] ([ResourceID], [ResourceTypeCode], [Path], [FileName], [Link], [ApplicationID], [ResourceName], [Status], [Token]) VALUES (10002681, N'WEB_FORM', N'/Function/Fee/FeeType/', N'Default.aspx', N'/Function/Fee/FeeType/Default.aspx', 10000016, N'Inside_Chi phí_Loại chi phí', 1, N'9A000FD6-D756-46BC-A761-27BB6633DB38')
SET IDENTITY_INSERT [dbo].[Resources] OFF
INSERT [dbo].[ResourceTypes] ([ResourceTypeCode], [Name], [Description]) VALUES (N'DEFAULT', N'Tài nguyên rỗng', N'NULL')
INSERT [dbo].[ResourceTypes] ([ResourceTypeCode], [Name], [Description]) VALUES (N'WEB_FORM', N'Giao diện web', NULL)
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [ApplicationID], [RoleCode], [RoleName]) VALUES (10000000, 10000016, N'ISD_INSIDE_ADMIN', N'Inside Admin')
INSERT [dbo].[Roles] ([RoleID], [ApplicationID], [RoleCode], [RoleName]) VALUES (10000001, 10000016, N'ISD_INSIDE_MOD', N'Inside Mod')
INSERT [dbo].[Roles] ([RoleID], [ApplicationID], [RoleCode], [RoleName]) VALUES (10000002, 10000016, N'ISD_INSIDE_USER', N'Inside User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[Users] ([Username], [Password], [FullName], [Email], [Blocked], [BlockedDate], [CreatedTime], [LastLogin], [LastIP], [FailPassword]) VALUES (N'admin', 0x9A6DC0581479F5FBB1829E72F9669459CB92F917, N'Quản trị hệ thống', N'longndh@live.com', 0, CAST(N'2017-03-15T07:02:02.507' AS DateTime), CAST(N'2010-11-24T17:12:59.503' AS DateTime), CAST(N'2017-08-16T20:23:15.080' AS DateTime), N'127.0.0.1', 0)
INSERT [dbo].[Users] ([Username], [Password], [FullName], [Email], [Blocked], [BlockedDate], [CreatedTime], [LastLogin], [LastIP], [FailPassword]) VALUES (N'longndh', 0x9A6DC0581479F5FBB1829E72F9669459CB92F917, N'Nguyễn Đình Hoàng Long', N'longndh@live.com', 0, CAST(N'2017-04-04T09:28:59.097' AS DateTime), CAST(N'2012-04-12T09:06:44.487' AS DateTime), CAST(N'2017-08-16T20:11:14.177' AS DateTime), N'127.0.0.1', 0)
INSERT [dbo].[Users] ([Username], [Password], [FullName], [Email], [Blocked], [BlockedDate], [CreatedTime], [LastLogin], [LastIP], [FailPassword]) VALUES (N'luupt', 0x7C4A8D09CA3762AF61E59520943DC26494F8941B, N'Phạm Trung Lưu', N'info.hiepphatcorp@gmail.com', 0, NULL, CAST(N'2017-08-16T20:45:15.600' AS DateTime), CAST(N'2017-08-16T20:45:25.627' AS DateTime), N'127.0.0.1', 0)
ALTER TABLE [dbo].[Permissions] ADD  CONSTRAINT [DF_Permissions_Allow]  DEFAULT ((1)) FOR [Allow]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_FailPassword]  DEFAULT ((0)) FOR [FailPassword]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_Membership_Roles]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_Users] FOREIGN KEY([Username])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_Membership_Users]
GO
ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menus_Resources] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resources] ([ResourceID])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menus_Resources]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_OperationCategories] FOREIGN KEY([OperationCode])
REFERENCES [dbo].[OperationCategories] ([OperationCode])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_OperationCategories]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_ResourceTypes] FOREIGN KEY([ResourceTypeCode])
REFERENCES [dbo].[ResourceTypes] ([ResourceTypeCode])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_ResourceTypes]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Resources] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resources] ([ResourceID])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Resources]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Roles]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Users] FOREIGN KEY([Username])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Users]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_Applications]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_ResourceTypes] FOREIGN KEY([ResourceTypeCode])
REFERENCES [dbo].[ResourceTypes] ([ResourceTypeCode])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_ResourceTypes]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Applications]
GO
/****** Object:  StoredProcedure [dbo].[Usp_ActionLogs_Create]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ThuNM
-- Create date: 2011-07-25
-- Description:	Tao bang moi luu du lieu ActionLog
-- =============================================

CREATE PROCEDURE [dbo].[Usp_ActionLogs_Create] 
	@Date DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
		
			DECLARE @Script VARCHAR(MAX)
			SET @Script = '
				CREATE TABLE [dbo].[ActionLogs{@Date}](
				[ID] [bigint] IDENTITY(1,1) NOT NULL,
				[Date] [datetime] NULL,
				[IP] [varchar](19) NULL,
				[UserName] [nvarchar](50) NULL,
				[Path] [nvarchar](100) NULL,
				[PageTitle] [nvarchar](100) NULL,
				[Operation] [nvarchar](50) NULL,
				[Data] [ntext] NULL,
				 CONSTRAINT [PK_ActionLogs{@Date}] PRIMARY KEY CLUSTERED 
				(
					[ID] ASC
				)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
				) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			'

			SET @Script = REPLACE(@Script,'{@Date}',CONVERT(VARCHAR(6),@Date,112))
			EXEC(@Script)
			--PRINT(@Script)

END



GO
/****** Object:  StoredProcedure [dbo].[Usp_ActionLogs_Search]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [Usp_ActionLogs_Search] '2015-01-20', '2015-01-20', 2, ''
CREATE PROCEDURE [dbo].[Usp_ActionLogs_Search]
	 @FromDate datetime
	,@ToDate datetime
	,@SearchType int	-- 1-IP  2-UserName  3-Path
	,@Keyword nvarchar(100)	
AS
BEGIN

	DECLARE @Script nvarchar(max)
	SET @Script = '

	SELECT [ID]
		  ,[Date]
		  ,[IP]
		  ,[UserName]
		  ,[Path]
		  ,[PageTitle]
		  ,[Operation]
		  ,[Data]
	  FROM [dbo].[ActionLogs_View]
	 WHERE [Date] BETWEEN ''{FromDate}'' AND ''{ToDate}'' AND
		   [Operation] <> ''Login''
'

	SET @Script = REPLACE(@Script, '{FromDate}', CONVERT(varchar(10), @FromDate, 120) + ' 00:00:00')
	SET @Script = REPLACE(@Script, '{ToDate}', CONVERT(varchar(10), @ToDate, 120) + ' 23:59:59.998')

	IF @Keyword = '' AND @SearchType IN (1,2)
	BEGIN
		SET @Script = @Script + ' ORDER BY [Date] DESC'
		EXEC (@Script)
	END
	ELSE
	BEGIN
		IF @SearchType = 1
			SET @Script = @Script + ' AND [IP] = COALESCE(''{Keyword}'', [IP])'
		ELSE
			IF @SearchType = 2
				SET @Script = @Script + ' AND [UserName] = COALESCE(''{Keyword}'', [UserName])'
			ELSE
				IF @SearchType = 3
					SET @Script = @Script + ' AND [Path] LIKE ''%{Keyword}%'''
		SET @Script = @Script + ' ORDER BY [Date] DESC'	   		
		SET @Script = REPLACE(@Script, '{Keyword}', @Keyword)

		EXEC (@Script)
	END

	
	
END


GO
/****** Object:  StoredProcedure [dbo].[Usp_ActionLogs_UpdateCurrentView]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		thunm
-- Create date: 2011-07-25
-- Description:	Cap nhat view ActionLogs de xac dinh TABLE dung hien tai
-- =============================================
CREATE PROCEDURE [dbo].[Usp_ActionLogs_UpdateCurrentView]-- '2011-06-25'
	@Date DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @TableName VARCHAR(30)
	SET @TableName = 'ActionLogs'+ CONVERT(VARCHAR(6),@Date,112)
	
	DECLARE @Script VARCHAR(MAX)
	SET @Script = '
	ALTER VIEW [dbo].[ActionLogs_CurrentView]
	AS
	SELECT     ID, Date, IP, UserName, [Path], PageTitle, Operation, Data
	FROM ' + @TableName

	EXECUTE(@Script)
	--PRINT @Script
END



GO
/****** Object:  StoredProcedure [dbo].[Usp_ActionLogs_UpdateView]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ThuNM
-- Create date: 2011-07-25
-- Description:	Cap nhat View ActionLog
-- =============================================

CREATE PROCEDURE [dbo].[Usp_ActionLogs_UpdateView] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Script VARCHAR(MAX)
	SET @Script = 'ALTER VIEW [dbo].[ActionLogs_View] AS'
	
	DECLARE tableCur CURSOR FOR
	SELECT [Name] FROM sys.tables
	WHERE NAME LIKE  'ActionLogs%'
	ORDER BY [Name]

	OPEN tableCur

	DECLARE @Name VARCHAR(50)
	FETCH NEXT FROM tableCur INTO @Name
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @Script = 'ALTER VIEW [dbo].[ActionLogs_View] AS'
				SET @Script = @Script + ' 
					SELECT ID, Date, IP, UserName, [Path], PageTitle, Operation, Data FROM ' + @Name
			ELSE
				SET @Script = @Script + '
					UNION ALL 
					SELECT ID, Date, IP, UserName, [Path], PageTitle, Operation, Data FROM ' + @Name
	
	
		FETCH NEXT FROM tableCur INTO @Name
	END
	CLOSE tableCur
	DEALLOCATE tableCur
	
	--PRINT @Script
	EXECUTE(@Script)
END




GO
/****** Object:  StoredProcedure [dbo].[Usp_Membership_IsUserInRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Usp_Membership_IsUserInRole 'anhah', 'ISD_TDK_EDITORS'
CREATE PROCEDURE [dbo].[Usp_Membership_IsUserInRole]
	 @Username varchar(128)
	,@RoleCode nvarchar(50)
AS
BEGIN
	SELECT 
		 Username
		,M.RoleID
		,RoleCode
	FROM 
		[dbo].[Membership] M INNER JOIN
		[dbo].[Roles] R ON M.RoleID = R.RoleID
	WHERE 
		Username = @Username AND
		RoleCode = @RoleCode
END




GO
/****** Object:  StoredProcedure [dbo].[UspAddUserToRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspAddUserToRole]
	 @Username varchar(128)
	,@RoleID int
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	IF NOT EXISTS(SELECT Username FROM [dbo].[Users] WHERE Username = @Username)
		RAISERROR(N'Add user to role fail. User does not exist', 16, 1)

	IF NOT EXISTS(SELECT RoleID FROM [dbo].[Roles] WHERE RoleID = @RoleID)
		RAISERROR(N'Add user to role fail. Role code was not found', 16, 1)

	IF NOT EXISTS(SELECT * FROM [dbo].[Membership] WHERE Username = @Username AND RoleID = @RoleID)
	BEGIN
		INSERT INTO [dbo].[Membership]
           ([Username]
           ,[RoleID])
		 VALUES
			   (@Username
			   ,@RoleID)
	END

	SET @ErrorNumber = 0
	SET @ErrorMessage = N'Success'

END TRY

BEGIN CATCH

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspBlockUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspBlockUser]
	@Username varchar(128)
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	UPDATE Users
	   SET Blocked = 1, BlockedDate = GETDATE()
	 WHERE Username = @Username

	IF @@ROWCOUNT = 0
		RAISERROR(N'Không tìm thấy user', 16, 1)

	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Khóa user thành công'
	
END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspChangeUserPassword]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspChangeUserPassword]
	@Username varchar(128)
   ,@CurrentPassword varchar(64)
   ,@NewPassword varchar(64)
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	DECLARE @BinOldPass varbinary(150)
	DECLARE @BinNewPass varbinary(150)

	SET @BinOldPass = HashBytes('SHA1', @CurrentPassword)
	SET @BinNewPass = HashBytes('SHA1', @NewPassword)
	
	IF NOT EXISTS(SELECT Username FROM [dbo].[Users] WHERE Username = @Username AND [Password] = @BinOldPass)
		RAISERROR(N'User không tồn tại hoặc password cũ không đúng.', 16, 1)

	UPDATE Users SET [Password] = @BinNewPass WHERE Username = @Username AND [Password] = @BinOldPass

	IF @@ROWCOUNT = 0
		RAISERROR (N'Cập nhật không thành công', 16, 1)
	ELSE
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Success'
	END
	
END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCheckAuthorization]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCheckAuthorization]
	@UserName varchar(50),
	@Token	varchar(50)--,
	--@Result int output
AS
DECLARE @ResourceID int
DECLARE @RET bit

SELECT @ResourceID=ResourceID FROM [dbo].[Resources] WHERE Token=@Token

SELECT @RET = dbo.UfnCheckAuthorizationOnResourceByUserName(@UserName, @ResourceID)

IF @RET <> 1 BEGIN
	--SET @Result=0
	RETURN	
END

	
	IF @UserName = 'admin' OR @UserName = 'longndh' BEGIN		
		DECLARE @Allow bit
		SET @Allow = 1
		SELECT OperationCode, @Allow AS Allow FROM [dbo].[OperationCategories]
		RETURN
	END

	IF EXISTS ( SELECT *
			FROM [dbo].[Membership]
			WHERE Username = @UserName AND (RoleID = 10000000 OR RoleID = 10000001))
	BEGIN		
		SET @Allow = 1
		SELECT OperationCode, @Allow AS Allow FROM [dbo].[OperationCategories]
		RETURN
	END
	
	--INSERT INTO Auth_Log
	--SELECT @UserName, @Token, '', GETDATE()
	
	SELECT OperationCode, Allow FROM [dbo].[Permissions]
	WHERE ResourceID=@ResourceID AND UserName=@UserName
	UNION 
	SELECT P.OperationCode, P.Allow 
	FROM [dbo].[Permissions] P INNER JOIN Roles R ON P.RoleID=R.RoleID
					 INNER JOIN Membership M ON R.RoleID=M.RoleID
			WHERE M.UserName=@UserName 
					AND P.ResourceID=@ResourceID
					AND NOT EXISTS(SELECT PermissionID FROM [dbo].[Permissions] P1
										WHERE P1.ResourceID=@ResourceID 
											  AND P1.UserName=@UserName 
											  AND Allow=0)
	
	--SET @Result=1



GO
/****** Object:  StoredProcedure [dbo].[UspCheckExistApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Thunm
-- Create date: 5/4/2040
-- Description:	Kiem tra mot Application da ton tai chua ?
-- =============================================
CREATE PROCEDURE [dbo].[UspCheckExistApplication] 
	@AppName NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF EXISTS(SELECT [Name] FROM [dbo].[Applications] WHERE [Name] = @AppName AND [Status]=1)
	BEGIN
		RETURN 1
	END
	ELSE
		RETURN 0

END



GO
/****** Object:  StoredProcedure [dbo].[UspCheckPermission]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCheckPermission]
	 @ResourceID int
	,@OperationCode int
	,@ObjectName varchar(128)
	,@ObjectType int = 0   -- 0:user, 1:role
	,@Allow bit output
AS

	IF @ObjectType = 0
		SELECT @Allow = Allow FROM [dbo].[Permissions] WHERE ResourceID = @ResourceID AND OperationCode = @OperationCode AND Username = @ObjectName
	ELSE
		SELECT @Allow = Allow FROM [dbo].[Permissions] WHERE ResourceID = @ResourceID AND OperationCode = @OperationCode AND CONVERT(varchar(128),RoleID) = @ObjectName

	SET @Allow = ISNULL(@Allow, 0)



GO
/****** Object:  StoredProcedure [dbo].[UspCheckUserLogin]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
declare @T int
exec @T = [dbo].[UspCheckUserLogin] 'hieplh', '123456', 'sadasd'
print @T
*/

CREATE PROCEDURE [dbo].[UspCheckUserLogin]
	@UserName varchar(128),
	@Password varchar(64),
	@IP varchar(19)
AS
	--RETURN 1;

	DECLARE @Date datetime
	SET @Date = GetDate()

	DECLARE @LoginResult int
	SET @LoginResult = 0	-- Dang nhap khong thanh cong

	DECLARE @HashedPassword varbinary(150)
	
		SET @HashedPassword = HashBytes('SHA1', @Password)
		--THỰC HIỆN HASH @Password
	
	DECLARE @U varchar(128), @P varbinary(150), @B bit
	SELECT @U = UserName, @P = Password, @B = Blocked FROM [dbo].[Users] WHERE LOWER(UserName)=LOWER(@UserName)

	IF @U IS NULL BEGIN
		EXEC UspInsertLoginLog @UserName, @IP, @LoginResult, @Date
		RETURN 3 --TAI KHOAN KHONG TON TAI
	END

	IF @B <> 0 BEGIN
		EXEC UspInsertLoginLog @UserName, @IP, @LoginResult, @Date
		RETURN 2 --TAI KHOAN DA BI KHOA
	END 
	
	DECLARE @LastLogin  DATETIME
	SELECT @LastLogin = LastLogin FROM [dbo].[Users]
	WHERE [UserName] = @UserName

	IF CONVERT(VARCHAR(10),@LastLogin,120) <> CONVERT(VARCHAR(10),GETDATE(),120)
		UPDATE dbo.Users 
		SET FailPassword = 0  --RESET FailPassword
		WHERE UserName=@UserName

	IF @HashedPassword = @P BEGIN
		UPDATE dbo.Users 
		SET FailPassword = 0,  --RESET FailPassword
			LastIP=@IP,
			LastLogin=GETDATE()
		WHERE UserName=@UserName

		SET @LoginResult = 1
		EXEC UspInsertLoginLog @UserName, @IP, @LoginResult, @Date

		RETURN 1 -- TAI KHOAN HOP LE
	END

	UPDATE dbo.Users 
	SET FailPassword = FailPassword + 1,  --INCREASE FailPassword
		Blocked = (CASE WHEN FailPassword < 5 THEN Blocked ELSE 1 END), --NEU SAI 5 LAN THI KHOA TAI KHOAN
		BlockedDate = (CASE WHEN FailPassword < 5 THEN GETDATE() ELSE BlockedDate END)
	WHERE UserName=@UserName
	
	EXEC UspInsertLoginLog @UserName, @IP, @LoginResult, @Date
	RETURN 4 --MAT KHAU KHONG CHINH XAC



GO
/****** Object:  StoredProcedure [dbo].[UspClearErrorLog]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspClearErrorLog]
AS
	/*
	DELETE [dbo].[ErrorLogs]
	WHERE ID NOT IN (SELECT TOP 15 ID FROM [ErrorLogs] ORDER BY ID DESC)
	*/
		
	--WITH CTE ([Message], CurrentUser, DuplicateCount)
	--AS
	--(
	--	SELECT [Message], CurrentUser, ROW_NUMBER() OVER(PARTITION BY [Message], CurrentUser ORDER BY [Message], CurrentUser) AS DuplicateCount
	--	FROM ErrorLogs
	--)
	--DELETE
	--FROM CTE
	--WHERE DuplicateCount > 1
	
	DELETE ErrorLogs WHERE [ID] IN (SELECT ID FROM
	(
		SELECT [ID], [Date], [Path], [Message], CurrentUser,
			ROW_NUMBER() OVER(PARTITION BY [Path], [Message], CurrentUser ORDER BY [Date] DESC) AS RN
		FROM ErrorLogs) Q1
		WHERE Q1.RN <> 1)
	
	DELETE [dbo].[ErrorLogs] WHERE CurrentUser IN ('longndh', 'hiepln', 'dainnl', 'ngantl', 'lochv', 'longpht')


GO
/****** Object:  StoredProcedure [dbo].[UspCreateApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateApplication]
	 @Name nvarchar(50)
	,@Description nvarchar(250)
	,@ApplicationID int output
AS
	DECLARE @ResourceID int
	DECLARE @MenuID int

	BEGIN TRANSACTION
	BEGIN TRY
		--them application 
		INSERT INTO [dbo].[Applications]
			   ([Name]
			   ,[Description]
			   ,Status)
		 VALUES
			   (@Name
			   ,@Description
			   ,1)
	
		SET @ApplicationID = @@IDENTITY
		
		-- tao resource va menuitem cho application vua tao de lam node ROOT
		
		INSERT INTO [dbo].[Resources](ResourceTypeCode, ApplicationID, ResourceName, Status, Token)
		VALUES('DEFAULT', @ApplicationID, @Name, 1, NewID())
		
		SET @ResourceID = @@IDENTITY

		INSERT INTO [dbo].[Menus](DisplayName, ResourceID)
		VALUES(@Name, @ResourceID)

		SET @MenuID = @@IDENTITY
		-- update Priority cho Menu = MenuID
		UPDATE [Menus] SET Priority = @MenuID WHERE MenuID = @MenuID

		-- update token cho application khop voi node resource dau tien
		UPDATE Applications
		SET Token = (SELECT Token FROM Resources WHERE ResourceID = @ResourceID)
		WHERE ApplicationID = @ApplicationID
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateMenu]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateMenu]
	@DisplayName nvarchar(100)
   ,@ResourceID int
   ,@ParentMenuID int
   ,@MenuID int output
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF NOT EXISTS(SELECT ResourceID FROM Resources WHERE ResourceID = @ResourceID)
		RAISERROR(N'Create menu fail. Resource can not be found', 16, 1)

	IF NOT @ParentMenuID IS NULL
		IF NOT EXISTS(SELECT MenuID FROM Menus WHERE MenuID = @ParentMenuID)
			RAISERROR(N'Create menu fail. Parent menu was not found', 16, 1)

	INSERT INTO [dbo].[Menus]
			   ([DisplayName]
			   ,[ResourceID]
			   ,[ParentMenuID])
		 VALUES
			   (@DisplayName
			   ,@ResourceID
			   ,@ParentMenuID)
	
	IF @@ROWCOUNT > 0
	BEGIN
		SET @MenuID = @@IDENTITY

		--Update: Priority = New menuID
		UPDATE [Menus] SET Priority = @MenuID WHERE MenuID = @MenuID

		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Create menu successfully'
	END
	ELSE
		RAISERROR(N'Create menu fail. Database error!', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateOperation]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateOperation]
	@OperationCode varchar(50)
   ,@ResourceTypeCode varchar(50)
   ,@Description nvarchar(250)
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF NOT EXISTS (SELECT OperationCode FROM OperationCategories WHERE OperationCode = @OperationCode)
		RAISERROR(N'Create new operation fail. Invalid operation code', 16, 1)

	IF NOT EXISTS (SELECT ResourceTypeCode FROM ResourceTypes WHERE ResourceTypeCode = @ResourceTypeCode)
		RAISERROR(N'Create new operation fail. Invalid resource type code', 16, 1)

--	IF EXISTS(SELECT OperationCode FROM Operations WHERE OperationCode = @OperationCode AND ResourceTypeCode = @ResourceTypeCode)
--		RAISERROR(N'Create new operation fail. Operation already exists', 16, 1)

	DELETE FROM [dbo].[Operations]
      WHERE OperationCode = @OperationCode AND ResourceTypeCode = @ResourceTypeCode

	INSERT INTO [dbo].[Operations]
			   ([OperationCode]
			   ,[ResourceTypeCode]
			   ,[Description])
		 VALUES
			   (@OperationCode
			   ,@ResourceTypeCode
			   ,@Description)

	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Create new operation successfully'
	END
	ELSE
		RAISERROR(N'Database error!', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateOperationCategory]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateOperationCategory]
	@OperationCode int output
	,@Name nvarchar(250)
	,@Description nvarchar(500)
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	IF EXISTS (SELECT OperationCode FROM dbo.OperationCategories WHERE [Name] = @Name)
		RAISERROR (N'Tên thao tác đã tồn tại.', 16, 1)

	INSERT INTO [OperationCategories]
			   ([Name]
				,[Description])
		 VALUES
			   (@Name
				,@Description)
	SET @OperationCode = @@Identity
	SET @ErrorNumber = 0
	SET @ErrorMessage = N'Tạo loại thao tác thành công.'

END TRY


BEGIN CATCH
	
	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateResource]
	@ResourceTypeCode varchar(50)
   ,@Path varchar(250)
   ,@FileName varchar(250)
   ,@Link varchar(250)
   ,@ApplicationID int
   ,@ResourceName nvarchar(250)
   ,@Status bit
   ,@Token varchar(50)=NULL
   ,@ResourceID int output
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF NOT EXISTS(SELECT ApplicationID FROM Applications WHERE ApplicationID = @ApplicationID AND [Status]=1)
		RAISERROR(N'Create resource fail. Application was not found', 16, 1)

	IF NOT EXISTS(SELECT ResourceTypeCode FROM ResourceTypes WHERE ResourceTypeCode = @ResourceTypeCode)
		RAISERROR(N'Create resource fail. Resource type was not found', 16, 1)

	IF EXISTS(SELECT ResourceName FROM Resources WHERE [ResourceTypeCode] = @ResourceTypeCode AND [ApplicationID] = @ApplicationID AND [ResourceName] = @ResourceName)
		RAISERROR(N'Create resource fail. The resource name already exists', 16, 1)

	IF EXISTS(SELECT ResourceID FROM Resources WHERE Token = @Token AND @Token NOT IN(NULL, 'DEFAULT'))
		RAISERROR(N'Create resource fail. Token has already being used', 16, 1)

	INSERT INTO [dbo].[Resources]
			   ([ResourceTypeCode]
			   ,[Path]
			   ,[FileName]
			   ,[Link]
			   ,[ApplicationID]
			   ,[ResourceName]
			   ,[Status]
			   ,[Token])
		 VALUES
			   (@ResourceTypeCode
			   ,@Path
			   ,@FileName
			   ,@Link
			   ,@ApplicationID
			   ,@ResourceName
			   ,@Status
			   ,@Token)
	
	IF @@ROWCOUNT > 0
	BEGIN
		SET @ResourceID = @@IDENTITY
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Create resource sucessfully'
	END
	ELSE
		RAISERROR(N'Create resource fail. Database error!', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateResourceType]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateResourceType]
			@ResourceTypeCode varchar(50)
           ,@Name nvarchar(50)           
		   ,@ErrorNumber int output
		   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF EXISTS(SELECT ResourceTypeCode FROM ResourceTypes WHERE ResourceTypeCode = @ResourceTypeCode)
		RAISERROR(N'Thêm mới không thành công. Mã loại đã tồn tại.', 16, 1)

	INSERT INTO [dbo].[ResourceTypes]
			   ([ResourceTypeCode]
			   ,[Name])
			   
		 VALUES
			   (@ResourceTypeCode
			   ,@Name)
			   

	SET @ErrorNumber = 0
	SET @ErrorMessage = ''


END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateRole]
	@RoleID int OUTPUT
	, @RoleCode nvarchar(50)
    ,@RoleName nvarchar(50)
	,@ApplicationID int
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY
	IF EXISTS(SELECT RoleCode FROM Roles WHERE RoleCode = @RoleCode AND ApplicationID = @ApplicationID)
		RAISERROR (N'Create role fail. The role code already existed.', 16, 1)

	INSERT INTO [dbo].[Roles]
			   ([RoleCode]
			   ,[RoleName]
				,ApplicationID)
		 VALUES
			   (@RoleCode
			   ,@RoleName
				,@ApplicationID)

	SET @ErrorNumber = 0
	SET @ErrorMessage = N'Create role successfully'
	SET @RoleID = @@IDENTITY

END TRY

BEGIN CATCH

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspCreateUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspCreateUser]
	@Username varchar(128)
   ,@Password varchar(64)
   ,@FullName nvarchar(64)
   ,@Email varchar(128)
   ,@Blocked bit = 0
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	DECLARE @BinPass varbinary(150)
	DECLARE @BinAnswer varbinary(150)

	-- validate existed user
	IF EXISTS(SELECT Username FROM Users WHERE Username = @Username)
		RAISERROR(N'Tạo user không thành công: Tên đăng nhập đã tồn tại', 16, 1)

	IF len(@Password) < 6
		RAISERROR(N'Tạo user không thành công: Mật khẩu ít hơn 6 ký tự', 16, 1)

	IF EXISTS(SELECT Email FROM Users WHERE Email = @Email)
		RAISERROR(N'Tạo user không thành công: Email đã tồn tại', 16, 1)

	-- hash password
	SET @BinPass = HashBytes('SHA1', @Password)

	-- insert new record
	INSERT INTO [dbo].[Users]
			   ([Username]
			   ,[Password]
			   ,[FullName]
			   ,[Email]
			   ,[Blocked]
			   ,[BlockedDate]
			   ,[CreatedTime]
			   ,[LastLogin]
			   ,[LastIP])
		 VALUES
			   (@Username
			   ,@BinPass
			   ,@FullName
			   ,@Email
			   ,@Blocked 
			   ,NULL
			   ,GETDATE()
			   ,NULL
			   ,NULL)

	
	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Create user successfully'

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspFindOperationsByCategory]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindOperationsByCategory]
	@OperationCode varchar(50)
AS

SELECT * FROM dbo.Operations WHERE  OperationCode = @OperationCode



GO
/****** Object:  StoredProcedure [dbo].[UspFindOperationsByResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindOperationsByResource]
	@ResourceID int
AS

SELECT O.* 
  FROM Operations O 
		INNER JOIN ResourceTypes R ON O.ResourceTypeCode = R.ResourceTypeCode
		INNER JOIN Resources R2 ON R.ResourceTypeCode = R2.ResourceTypeCode
WHERE R2.ResourceID = @ResourceID



GO
/****** Object:  StoredProcedure [dbo].[UspFindOperationsByResourceType]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindOperationsByResourceType]
	@ResourceTypeCode varchar(50)
AS

SELECT 
	 O.* 
	,OC.Name
  FROM 
		dbo.OperationCategories OC
		INNER JOIN Operations O  ON OC.OperationCode = O.OperationCode
		INNER JOIN ResourceTypes R ON O.ResourceTypeCode = R.ResourceTypeCode		
WHERE R.ResourceTypeCode = @ResourceTypeCode



GO
/****** Object:  StoredProcedure [dbo].[UspFindResourcesByApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindResourcesByApplication]
    @ApplicationID int
AS

	SELECT [ResourceID]      
	  ,[ResourceName]
	  ,[ResourceTypeCode]
      ,[Path]
      ,[FileName]
      ,[Link]            
      ,[ApplicationID]
	  ,[Status]	 
	  ,[Token] 	  
  FROM [Resources] R
	 WHERE ApplicationID = @ApplicationID
  ORDER BY [ResourceName]



GO
/****** Object:  StoredProcedure [dbo].[UspFindResourcesByName]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindResourcesByName]
    @ResourceName nvarchar(250)
AS

	SELECT [ResourceID]      
	  ,[ResourceName]
	  ,[ResourceTypeCode]
      ,[Path]
      ,[FileName]
      ,[Link]                  
      ,[ApplicationID]
	  ,[Status]
	  ,[Token]	  
  FROM [Resources] R
WHERE (ResourceName like '%' + @ResourceName + '%') or (@ResourceName = '')
ORDER BY [Path] DESC


GO
/****** Object:  StoredProcedure [dbo].[UspFindResourcesByPath]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindResourcesByPath]
    @Path nvarchar(250)
AS

SELECT [ResourceID]      
	  ,[ResourceName]
	  ,[ResourceTypeCode]
      ,[Path]
      ,[FileName]
      ,[Link]                  
      ,[ApplicationID]
	  ,[Status]	
	  ,[Token]  	  
  FROM [Resources] R
  WHERE ([Path] like '%' + @Path + '%') or (@Path = '')
  ORDER BY [Path] DESC


GO
/****** Object:  StoredProcedure [dbo].[UspFindResourcesByToken]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindResourcesByToken]
    @Token varchar(50)
AS

	SELECT [ResourceID]      
	  ,[ResourceName]
	  ,[ResourceTypeCode]
      ,[Path]
      ,[FileName]
      ,[Link]                  
      ,[ApplicationID]
	  ,[Status]
	  ,[Token]	  
  FROM [Resources] R
WHERE [Token] = @Token
ORDER BY [Path] DESC


GO
/****** Object:  StoredProcedure [dbo].[UspFindRolesByCode]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindRolesByCode]
	 @RoleCode varchar(128)
	,@ApplicationID int 
AS
BEGIN
    SELECT RoleID, ApplicationID, RoleCode, RoleName
	FROM dbo.Roles
	WHERE RoleCode like '%' + @RoleCode + '%' AND ApplicationID = @ApplicationID
END



GO
/****** Object:  StoredProcedure [dbo].[UspFindUserByUserName]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindUserByUserName]
	@UserName varchar(128)
AS
	SELECT Username, FullName, Email, Blocked, BlockedDate, CreatedTime, LastLogin, LastIP
	  FROM dbo.Users
	 WHERE Username LIKE '%' + @UserName + '%'



GO
/****** Object:  StoredProcedure [dbo].[UspFindUserRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindUserRole]
	@UserName varchar(50), 
	@RoleCode varchar(50)
AS
/*
	- neu ca 2 bien deu null: se ko tim thay dong nao
	- 1 trong 2 bien khac null: 
		+ se lay bien khac null la keyword va tim theo gia tri do
		+ neu bien khac null co gia tri la '' thi se tim tat ca
	- ca 2 bien deu co gia tri la '': ket qua se la tat ca cac dong trong Roles va Users
*/
SELECT '' RoleID,  '' RoleCode, UserName, FullName, 'User' [Type]
FROM Users
WHERE UserName LIKE '%' + COALESCE(@UserName, '!@#') + '%'
UNION ALL
SELECT RoleID, RoleCode, '' UserName, '' FullName, 'Role' [Type]
FROM Roles
WHERE RoleCode LIKE '%' + COALESCE(@RoleCode, '!@#') + '%'



GO
/****** Object:  StoredProcedure [dbo].[UspFindUsersByEmail]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspFindUsersByEmail]
	@Email varchar(128)
AS
	SELECT Username, FullName, Email, Blocked, BlockedDate, CreatedTime, LastLogin, LastIP
	  FROM dbo.Users
	 WHERE Email = @Email



GO
/****** Object:  StoredProcedure [dbo].[UspFindUsersByFullName]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UspFindUsersByFullName]
	@FullName NVARCHAR(64)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Username, FullName, Email, Blocked, BlockedDate, CreatedTime, LastLogin, LastIP, FailPassword
	FROM dbo.Users
	WHERE FullName LIKE N'%' + @FullName + '%'
END



GO
/****** Object:  StoredProcedure [dbo].[UspGenerateToken]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGenerateToken]
AS
BEGIN
	DECLARE @Token varchar(50)
	SET @Token = NEWID()	
	WHILE EXISTS(SELECT Token FROM dbo.Resources WHERE Token = @Token)
	BEGIN
		SET @Token = NEWID()
	END

	SELECT @Token AS NewToken
END



GO
/****** Object:  StoredProcedure [dbo].[UspGetActionLogs]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetActionLogs]
	 @FromDate datetime
	,@ToDate datetime
	,@IP varchar(19)
	,@UserName nvarchar(50)	
AS
BEGIN

	SET @FromDate = CONVERT(varchar(10), @FromDate, 120) + ' 00:00:00'
	SET @ToDate = CONVERT(varchar(10), @ToDate, 120) + ' 23:59:59.998'

	SELECT [ID]
		  ,[Date]
		  ,[IP]
		  ,[UserName]
		  ,[Path]
		  ,[PageTitle]
		  ,[Operation]
		  ,[Data]
	  FROM [dbo].[ActionLogs_View]
	 WHERE [Date] BETWEEN @FromDate AND @ToDate
	   AND [IP] = COALESCE(@IP, [IP])
	   AND [UserName] = COALESCE(@UserName, [UserName])
	   AND [Operation] <> 'Login'
	ORDER BY [Date] DESC
END


GO
/****** Object:  StoredProcedure [dbo].[UspGetAllApplications]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllApplications]
AS
	SELECT * FROM Applications WHERE [Status]=1 ORDER BY ApplicationID



GO
/****** Object:  StoredProcedure [dbo].[UspGetAllOperationCategories]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllOperationCategories]
AS

SET NOCOUNT ON;
 
SELECT * FROM dbo.OperationCategories



GO
/****** Object:  StoredProcedure [dbo].[UspGetAllOrphanResources]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllOrphanResources]
	@ApplicationID int
AS

	SELECT [ResourceID]      
		  ,[ResourceName]
		  ,[ResourceTypeCode]
		  ,[Path]
		  ,[FileName]      
		  ,[Link]      
		  ,[ApplicationID]
		  ,[Status]
		  ,[Token]
	FROM [Resources]
	WHERE [Status]=1 AND ApplicationID=@ApplicationID 
			--AND ResourceID NOT IN (SELECT ResourceID FROM [Menus])
	ORDER BY [ResourceName]



GO
/****** Object:  StoredProcedure [dbo].[UspGetAllQuestions]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllQuestions]
AS
	SELECT * FROM Questions



GO
/****** Object:  StoredProcedure [dbo].[UspGetAllResources]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllResources]
AS

SELECT [ResourceID]      
	  ,[ResourceName]
	  ,[ResourceTypeCode]
      ,[Path]
      ,[FileName]      
      ,[Link]      
      ,[ApplicationID]
	  ,[Status]
	  ,[Token]
  FROM [Resources] R
  ORDER BY [ResourceID] DESC


GO
/****** Object:  StoredProcedure [dbo].[UspGetAllResourceTypes]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllResourceTypes]
AS

SELECT * FROM ResourceTypes



GO
/****** Object:  StoredProcedure [dbo].[UspGetAllRoles]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllRoles]
AS

SET NOCOUNT ON;

SELECT * FROM Roles
ORDER BY RoleCode


GO
/****** Object:  StoredProcedure [dbo].[UspGetAllRolesUsersByResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllRolesUsersByResource]
	@ResourceID int
AS
SELECT DISTINCT P.RoleID, RL.RoleCode, P.UserName, U.FullName, 
		CASE WHEN P.RoleID IS NOT NULL THEN 'Role' ELSE 'User' END AS [Type]
FROM Resources R INNER JOIN [Permissions] P ON R.ResourceID=P.ResourceID
				LEFT JOIN Roles RL ON P.RoleID=RL.RoleID
				LEFT JOIN Users U ON P.UserName=U.UserName
WHERE R.ResourceID=@ResourceID



GO
/****** Object:  StoredProcedure [dbo].[UspGetAllUsers]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetAllUsers]
AS
	SELECT Username, FullName, Email, Blocked, BlockedDate, CreatedTime, LastLogin, LastIP
	  FROM dbo.Users
	  ORDER BY UserName



GO
/****** Object:  StoredProcedure [dbo].[UspGetApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetApplication]
	@ApplicationID int	
AS
	SELECT * FROM Applications WHERE ApplicationID = @ApplicationID AND [Status]=1



GO
/****** Object:  StoredProcedure [dbo].[UspGetApplicationByUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetApplicationByUser]
	@UserName varchar(50)
AS
--lay danh cach cac ung dung ma user co quyen truy xuat trên đó
SELECT DISTINCT R.ApplicationID, A.[Name], A.Description
FROM Resources R
			INNER JOIN Applications A ON R.ApplicationID=A.ApplicationID 								
WHERE dbo.UfnCheckAuthorizationOnResourceByUserName(@UserName, R.ResourceID)=1 
			AND A.[Status]=1



GO
/****** Object:  StoredProcedure [dbo].[UspGetErrorLog]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[UspGetErrorLog] '2011-09-12', '2011-09-12', ''
CREATE PROCEDURE [dbo].[UspGetErrorLog]
	 @FromDate datetime
	,@ToDate datetime
	,@CurrentUser nvarchar(50) = null
AS
BEGIN

	IF @CurrentUser = ''
		SET @CurrentUser = NULL

	SET @FromDate = @FromDate + ' 00:00:00'
	SET @ToDate = @ToDate + ' 23:59:59.998'

	SELECT [ID]
      ,[Date]
      ,[CurrentUser]
      ,[Path]
      ,[Message]
      ,[StackTrace]
      ,[TargetFunction]
  FROM [dbo].[ErrorLogs]
  WHERE
	  Date BETWEEN @FromDate AND @ToDate
	  AND CurrentUser = COALESCE(@CurrentUser, CurrentUser)
	  AND [Message] <> 'Thread was being aborted.'
  ORDER BY Date DESC
	
END




GO
/****** Object:  StoredProcedure [dbo].[UspGetLoginLogs]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetLoginLogs]
	 @FromDate datetime
	,@ToDate datetime
	,@UserName nvarchar(50) = null
AS
BEGIN

	SET @FromDate = CONVERT(varchar(10), @FromDate, 120) + ' 00:00:00'
	SET @ToDate = CONVERT(varchar(10), @ToDate, 120) + ' 23:59:59.998'

	IF @UserName = ''
		SET @UserName = null

	SELECT [ID]
		  ,[UserName]
		  ,[IP]
		  ,[LoginResult]
		  ,[Date]
	  FROM [dbo].[LoginLogs]
	 WHERE [Date] BETWEEN @FromDate AND @ToDate
	   AND [UserName] = COALESCE(@UserName, [UserName])
END


GO
/****** Object:  StoredProcedure [dbo].[UspGetMenuByUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetMenuByUser]
	@UserName varchar(50),
	@ApplicationID int
AS
SET NOCOUNT ON
	DECLARE @RootID int
	SELECT @RootID=ResourceID FROM [dbo].[Resources] R 
							INNER JOIN [dbo].[Applications] A 
								ON R.ApplicationID=A.ApplicationID AND R.Token = A.Token --AND R.ResourceName=A.[Name]
	WHERE R.ResourceTypeCode='DEFAULT' AND R.ApplicationID=@ApplicationID

IF @UserName = 'admin' OR @UserName = 'longndh' OR EXISTS ( SELECT *
														FROM dbo.Membership (NOLOCK)
														WHERE Username = @UserName AND RoleID = 10000000) 
BEGIN
	SET NOCOUNT ON
	--LAY CAU TRUC PHAN CAP UNG VOI NODE ROOT VUA TIM DUOC
	;WITH AppTree(MenuID, DisplayName, [Path], [FileName], [Link], ResourceID, ParentMenuID, Depth, SortCol)
	AS
	(
		SELECT DISTINCT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, 0, CAST(M.Priority AS VARBINARY(MAX))
		FROM Menus M (NOLOCK) INNER JOIN Resources R (NOLOCK) ON M.ResourceID=R.ResourceID
					  INNER JOIN [Permissions] P ON R.ResourceID=P.ResourceID
		WHERE M.ResourceID = @RootID
					AND R.Status = 1
					--AND dbo.UfnCheckAuthorizationOnResourceByUserName(@Username, @RootID)=1
		UNION ALL
		SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, A.Depth+1, CAST(SortCol + CAST(M.Priority AS BINARY(4)) AS VARBINARY(MAX))
		FROM Menus M (NOLOCK) INNER JOIN Resources R (NOLOCK) ON M.ResourceID=R.ResourceID 
				JOIN AppTree A ON M.ParentMenuID=A.MenuID
		WHERE R.Status = 1 
				--AND dbo.UfnCheckAuthorizationOnResourceByUserName(@Username, R.ResourceID)=1
	)	

	SELECT MenuID, DisplayName, COALESCE([Path], '') AS [Path], COALESCE([FileName], '') AS [FileName], COALESCE([Path], '') + COALESCE([FileName], '') AS [Link], A.ResourceID, COALESCE(ParentMenuID,0) AS ParentMenuID, Depth, DisplayName FunctionName
	FROM AppTree A
	ORDER BY SortCol, DisplayName	
END
ELSE IF EXISTS ( SELECT * FROM dbo.Membership (NOLOCK) WHERE Username = @UserName AND RoleID = 10000001)
BEGIN
	SET NOCOUNT ON
	--LAY CAU TRUC PHAN CAP UNG VOI NODE ROOT VUA TIM DUOC
	;WITH AppTree(MenuID, DisplayName, [Path], [FileName], [Link], ResourceID, ParentMenuID, Depth, SortCol)
	AS
	(
		SELECT DISTINCT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, 0, CAST(M.Priority AS VARBINARY(MAX))
		FROM Menus M (NOLOCK) INNER JOIN Resources R (NOLOCK) ON M.ResourceID=R.ResourceID
					  INNER JOIN [Permissions] P ON R.ResourceID=P.ResourceID
		WHERE M.ResourceID = @RootID
					AND R.Status = 1
					--AND dbo.UfnCheckAuthorizationOnResourceByUserName(@Username, @RootID)=1
		UNION ALL
		SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, A.Depth+1, CAST(SortCol + CAST(M.Priority AS BINARY(4)) AS VARBINARY(MAX))
		FROM Menus M (NOLOCK) INNER JOIN Resources R (NOLOCK) ON M.ResourceID=R.ResourceID 
				JOIN AppTree A ON M.ParentMenuID=A.MenuID
		WHERE R.Status = 1 AND M.MenuID NOT IN (10000000, 10002324)
				--AND dbo.UfnCheckAuthorizationOnResourceByUserName(@Username, R.ResourceID)=1
	)	

	SELECT MenuID, DisplayName, COALESCE([Path], '') AS [Path], COALESCE([FileName], '') AS [FileName], COALESCE([Path], '') + COALESCE([FileName], '') AS [Link], A.ResourceID, COALESCE(ParentMenuID,0) AS ParentMenuID, Depth, DisplayName FunctionName
	FROM AppTree A
	ORDER BY SortCol, DisplayName	
END
ELSE
BEGIN
	CREATE TABLE #UserPermission(ResourceID int)
	INSERT INTO #UserPermission
		SELECT DISTINCT ResourceID
		FROM [Permissions] P
		WHERE
			(
				(Username = @Username AND RoleID IS NULL) OR
				(Username IS NULL AND RoleID IN (SELECT RoleID FROM Membership WHERE Username = @Username))
			) AND Allow = 1 AND OperationCode = 1
			AND ResourceID NOT IN (
				SELECT ResourceID
				FROM [Permissions] P
				WHERE Allow = 0 AND OperationCode = 1 AND Username = @Username		
			)

	SET NOCOUNT ON
	--LAY CAU TRUC PHAN CAP UNG VOI NODE ROOT VUA TIM DUOC
	;WITH AppTree(MenuID, DisplayName, [Path], [FileName], [Link], ResourceID, ParentMenuID, Depth, SortCol)
	AS
	(
		SELECT DISTINCT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, 0, CAST(M.Priority AS VARBINARY(MAX))
		FROM Menus M (NOLOCK) INNER JOIN Resources R (NOLOCK) ON M.ResourceID=R.ResourceID					  
					  INNER JOIN #UserPermission UP ON R.ResourceID=UP.ResourceID
		WHERE M.ResourceID = @RootID
					AND R.Status = 1					
		UNION ALL
		SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, A.Depth+1, CAST(SortCol + CAST(M.Priority AS BINARY(4)) AS VARBINARY(MAX))
		FROM Menus M (NOLOCK) INNER JOIN Resources R (NOLOCK) ON M.ResourceID=R.ResourceID 
				JOIN AppTree A ON M.ParentMenuID=A.MenuID
				INNER JOIN #UserPermission UP ON R.ResourceID=UP.ResourceID
		WHERE R.Status = 1 				
	)	

	SELECT MenuID, DisplayName, ISNULL([Path], '') AS [Path], ISNULL([FileName], '') AS [FileName], ISNULL([Path], '') + ISNULL([FileName], '') AS [Link], A.ResourceID, ISNULL(ParentMenuID,0) AS ParentMenuID, Depth, DisplayName FunctionName
	FROM AppTree A
	ORDER BY SortCol, DisplayName
	
	DROP TABLE #UserPermission
END



GO
/****** Object:  StoredProcedure [dbo].[UspGetMenuByUser_BackUp]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UspGetMenuByUser_BackUp]
	@UserName varchar(50),
	@ApplicationID int
AS
BEGIN
	DECLARE @RootID int
	SELECT @RootID=ResourceID FROM [dbo].[Resources] R 
							INNER JOIN [dbo].[Applications] A 
								ON R.ApplicationID=A.ApplicationID AND R.Token = A.Token --AND R.ResourceName=A.[Name]
	WHERE R.ResourceTypeCode='DEFAULT' AND R.ApplicationID=@ApplicationID


	DECLARE @Script varchar(max)
	SET @Script =' 	

	--LAY CAU TRUC PHAN CAP UNG VOI NODE ROOT VUA TIM DUOC
	;WITH AppTree(MenuID, DisplayName, [Path], [FileName], [Link], ResourceID, ParentMenuID, Depth, SortCol)
	AS
	(
		SELECT distinct M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, 0, CAST(M.Priority AS VARBINARY(MAX))
		FROM Menus M INNER JOIN Resources R ON M.ResourceID=R.ResourceID
					 {JOIN}
		WHERE M.ResourceID = {RootID}
					AND R.Status = 1
					AND dbo.UfnCheckAuthorizationOnResourceByUserName(''{UserName}'', {RootID})=1
		UNION ALL
		SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID, A.Depth+1, CAST(SortCol + CAST(M.Priority AS BINARY(4)) AS VARBINARY(MAX))
		FROM Menus M INNER JOIN Resources R ON M.ResourceID=R.ResourceID 
				JOIN AppTree A ON M.ParentMenuID=A.MenuID
		WHERE R.Status = 1 AND 
			  dbo.UfnCheckAuthorizationOnResourceByUserName(''{UserName}'', R.ResourceID)=1
	)	

	SELECT MenuID, DisplayName, COALESCE([Path], '''') AS [Path], COALESCE([FileName], '''') AS [FileName], COALESCE([Path], '''') + COALESCE([FileName], '''') AS [Link], A.ResourceID, COALESCE(ParentMenuID,0) AS ParentMenuID, Depth, DisplayName FunctionName
	FROM AppTree A
	ORDER BY SortCol, DisplayName'

	SET @Script = REPLACE(@Script, '{UserName}', @UserName)
	SET @Script = REPLACE(@Script, '{RootID}', @RootID)
	SET @Script = REPLACE(@Script, '{JOIN}', CASE WHEN @UserName <> 'admin' AND @UserName <> 'root' THEN ' INNER JOIN [Permissions] P ON R.ResourceID=P.ResourceID' ELSE '' END)

	PRINT (@Script)
	EXEC (@Script)
END



GO
/****** Object:  StoredProcedure [dbo].[UspGetMenuItem]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetMenuItem]
	@MenuID int
AS

	SELECT * FROM Menus WHERE MenuID = @MenuID



GO
/****** Object:  StoredProcedure [dbo].[UspGetMenuItemInfo]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	UspGetMenuItemInfo 10000024
*/
CREATE PROCEDURE [dbo].[UspGetMenuItemInfo]
	@MenuID int
AS
SELECT M.*, R.ResourceID, R.ResourceTypeCode, R.ResourceName, R.ApplicationID, RT.[Name] ResourceTypeName,
		A.[Name] ApplicationName
FROM Menus M INNER JOIN Resources R ON M.ResourceID=R.ResourceID
			 INNER JOIN Applications A ON R.ApplicationID=A.ApplicationID
			 INNER JOIN ResourceTypes RT ON R.ResourceTypeCode = RT.ResourceTypeCode
WHERE M.MenuID = @MenuID



GO
/****** Object:  StoredProcedure [dbo].[UspGetMenuItemsByApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
[dbo].[UspGetMenuItemsByApplication] 10000016

SELECT CAST(1 AS VARBINARY(MAX))
SELECT CAST(10000004 AS VARBINARY(MAX))


*/

CREATE PROCEDURE [dbo].[UspGetMenuItemsByApplication]
	@ApplicationID int
AS
--	SELECT M1.*
--	  FROM Menus M1 
--			INNER JOIN Resources R1 ON M1.ResourceID = R1.ResourceID
--			INNER JOIN Menus M2 ON (M1.ParentMenuID = M2.MenuID)
--	 WHERE R1.ApplicationID = 10000016

--LAY NODE ROOT TUONG UNG VOI UNG DUNG
DECLARE @RootID int
SELECT @RootID=ResourceID FROM [dbo].[Resources] R 
						INNER JOIN [dbo].[Applications] A 
							ON R.ApplicationID=A.ApplicationID AND R.Token = A.Token --AND R.ResourceName=A.[Name]
WHERE R.ResourceTypeCode='DEFAULT' AND R.ApplicationID=@ApplicationID
--
--	DECLARE @T TABLE(MenuID int, DisplayName nvarchar(100), [Path] varchar(250), [FileName] varchar(50), [Link] varchar(250), ResourceID int, ParentMenuID int)
--	INSERT INTO @T
--	SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, M.ParentMenuID
--	FROM Menus M INNER JOIN Resources R ON M.ResourceID=R.ResourceID

--LAY CAU TRUC PHAN CAP UNG VOI NODE ROOT VUA TIM DUOC
;WITH AppTree(MenuID, DisplayName, [Path], [FileName], [Link], ResourceID, ResourceName, ParentMenuID, Depth, SortCol)
AS
(
	SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, R.ResourceName, M.ParentMenuID, 0, CAST(M.Priority AS VARBINARY(MAX))
	FROM Menus M INNER JOIN Resources R ON M.ResourceID=R.ResourceID
	WHERE M.ResourceID = @RootID
	UNION ALL
	SELECT M.MenuID, M.DisplayName, R.[Path],  R.[FileName], R.[Link], R.ResourceID, R.ResourceName, M.ParentMenuID, A.Depth+1, CAST(SortCol + CAST(M.Priority AS BINARY(4)) AS VARBINARY(MAX))
	FROM Menus M INNER JOIN Resources R ON M.ResourceID=R.ResourceID 
			JOIN AppTree A ON M.ParentMenuID=A.MenuID
)	
SELECT MenuID, DisplayName, COALESCE([Path], '') AS [Path], COALESCE([FileName], '') AS [FileName], COALESCE([Link], '') AS [Link], ResourceID, ResourceName, COALESCE(ParentMenuID,0) AS ParentMenuID, Depth, (LEFT(ResourceName, CHARINDEX('_', ResourceName)) + DisplayName) FunctionName
FROM AppTree
ORDER BY SortCol



GO
/****** Object:  StoredProcedure [dbo].[UspGetOperation]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetOperation]
	@OperationCode varchar(50)
   ,@ResourceTypeCode varchar(50)
AS

SELECT * FROM Operations WHERE OperationCode = @OperationCode AND ResourceTypeCode = @ResourceTypeCode



GO
/****** Object:  StoredProcedure [dbo].[UspGetOperationCatByResourceType]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetOperationCatByResourceType]
	@ResourceTypeCode varchar(50)
AS
SELECT DISTINCT O.OperationCode, OC.[Name], OC.[Description]
FROM Operations O INNER JOIN OperationCategories OC ON O.OperationCode=OC.OperationCode
WHERE ResourceTypeCode = @ResourceTypeCode



GO
/****** Object:  StoredProcedure [dbo].[UspGetOperationCategory]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetOperationCategory]
	 @OperationCode varchar(50)
AS

SET NOCOUNT ON;
 
SELECT * FROM dbo.OperationCategories WHERE OperationCode = @OperationCode



GO
/****** Object:  StoredProcedure [dbo].[UspGetPermission]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetPermission]
	@ApplicationID int
AS
	SELECT P.*
	  FROM [Permissions] P INNER JOIN [Resources] R ON P.ResourceID = R.ResourceID
	 WHERE R.ApplicationID = @ApplicationID



GO
/****** Object:  StoredProcedure [dbo].[UspGetPermissionOnResourceByUserRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetPermissionOnResourceByUserRole]
	@ResourceID int,
	@UserName varchar(50),
	@RoleCode varchar(50),
	@IsAllowed bit
AS
/*
	Cac trang thai co the co
		+ Cho phép: ton tai 1 dong voi gia tri allow=1
		+ Cấm: ton tai 1 dong voi gia tri allow=0
		+ Không cho phép cũng ko cam: khong tại dong nao
	Neu Allow=0: se overright len quyen cua Role mà User nào truc thuoc (neu co)
*/
--neu bi Denied thi trong DB se ton tai 1 row co gia tri Allow=0, de xac dinh quyen nay dang bi cam
--cung 1 luc chi query theo 1 dieu kien, hoac la theo username hoac theo rolecode
IF @UserName IS NOT NULL 
	SET @RoleCode = NULL
SELECT P.PermissionID, P.ResourceID, P.OperationCode, P.UserName, P.RoleID, P.Allow
FROM [Permissions] P LEFT JOIN Roles R ON P.RoleID=R.RoleID
WHERE P.ResourceID=@ResourceID AND (R.RoleCode=COALESCE(@RoleCode, '#') OR UserName=COALESCE(@UserName, '#'))
		AND Allow=@IsAllowed



GO
/****** Object:  StoredProcedure [dbo].[UspGetResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetResource]
    @ResourceID int
AS

	SELECT * FROM Resources WHERE ResourceID = @ResourceID



GO
/****** Object:  StoredProcedure [dbo].[UspGetResourceOperationByUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetResourceOperationByUser]
	 @UserName nvarchar(50)
	,@ApplicationID int
AS
BEGIN
	DECLARE @Script nvarchar(max)

	SET @Script = '
	SELECT 
		 R.ResourceID
		,R.ResourceName
		,R.Path
	'

	DECLARE curOperation CURSOR FOR
		SELECT OperationCode, [Name] 
		FROM OperationCategories 
		WHERE OperationCode IN (
			SELECT DISTINCT OperationCode 
			FROM [Permissions] P 
			WHERE (RoleID IN (SELECT RoleID FROM Membership WHERE Username = @UserName) OR Username = @UserName)
			AND ResourceID  NOT IN (SELECT ResourceID FROM Resources WHERE ApplicationID <> @ApplicationID))

	OPEN curOperation

	DECLARE @OpID int
	DECLARE @OpName nvarchar(100)
	FETCH NEXT FROM curOperation INTO @OpID, @OpName

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Script = @Script + '	,(CASE WHEN OperationCode = ' + CONVERT(varchar, @OpID) + ' THEN Allow ELSE -1 END) AS [' + @OpName + ']
	'
		FETCH NEXT FROM curOperation INTO @OpID, @OpName
	END

	SET @Script = @Script + 'FROM [Permissions] P INNER JOIN Resources R ON P.ResourceID = R.ResourceID
	WHERE RoleID IN (SELECT RoleID FROM Membership WHERE Username = ''{UserName}'') OR Username = ''{UserName}''
	'
	SET @Script = REPLACE(@Script, '{UserName}', @UserName)
	SET @Script = REPLACE(@Script, '{ApplicationID}', @ApplicationID)

	CLOSE curOperation
	DEALLOCATE curOperation

	PRINT @Script
	EXEC (@Script)
END


GO
/****** Object:  StoredProcedure [dbo].[UspGetResourceType]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetResourceType]
	@ResourceTypeCode varchar(15)
AS

SELECT * FROM ResourceTypes WHERE ResourceTypeCode = @ResourceTypeCode or @ResourceTypeCode is null



GO
/****** Object:  StoredProcedure [dbo].[UspGetRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetRole]
	 @RoleID INT
AS

SET NOCOUNT ON;

SELECT * FROM Roles WHERE RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[UspGetRolePermissions]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetRolePermissions]
	 @ApplicationID int
	,@RoleID int
AS
	SELECT P.* 
	  FROM [Permissions] P INNER JOIN Resources R ON P.ResourceID = R.ResourceID
	 WHERE R.ApplicationID = @ApplicationID AND P.RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[UspGetRoles_In_App]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ThuNM
-- Create date: 2010-04-05
-- Description:	Lay danh  sach Role trong mot Application
-- =============================================
CREATE PROCEDURE [dbo].[UspGetRoles_In_App]
	@AppID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT RoleID, ApplicationID, RoleCode, RoleName
	FROM dbo.Roles
	WHERE ApplicationID = @AppID
	ORDER BY RoleCode
END



GO
/****** Object:  StoredProcedure [dbo].[UspGetRolesOfUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Thunm
-- Create date: 15/04/2010
-- Description: lay danh sach Role cua mot user
-- =============================================
CREATE PROCEDURE [dbo].[UspGetRolesOfUser]
	@UserName varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT dbo.Roles.RoleID, ApplicationID, RoleCode, RoleName
	FROM dbo.Roles,dbo.Membership
	WHERE dbo.Membership.Username = @UserName
	AND dbo.Roles.RoleID = dbo.Membership.RoleID
END



GO
/****** Object:  StoredProcedure [dbo].[UspGetRolesUsersByResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetRolesUsersByResource]
	@ResourceID int
AS
SELECT DISTINCT P.RoleID, RL.RoleCode, P.UserName, U.FullName, 
		CASE WHEN P.RoleID IS NOT NULL THEN 'Role' ELSE 'User' END AS [Type]
FROM Resources R INNER JOIN [Permissions] P ON R.ResourceID=P.ResourceID
				LEFT JOIN Roles RL ON P.RoleID=RL.RoleID
				LEFT JOIN Users U ON P.UserName=U.UserName
WHERE R.ResourceID=@ResourceID AND P.Allow=1



GO
/****** Object:  StoredProcedure [dbo].[UspGetUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetUser]
	@Username varchar(128)
AS
	SELECT Username, FullName, Email, Blocked, BlockedDate, CreatedTime, LastLogin, LastIP
	  FROM dbo.Users
	 WHERE Username = @Username



GO
/****** Object:  StoredProcedure [dbo].[UspGetUserPermissions]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [UspGetUserPermissions] 10000016, 'khanhlx'
CREATE PROCEDURE [dbo].[UspGetUserPermissions]
	 @ApplicationID int
	,@UserName varchar(128)
AS

	SELECT   P.PermissionID
			,P.ResourceID
			,R.ResourceName
			,(R.Path + R.FileName) AS FullPath
			,P.OperationCode
			,OP.Name AS OperationName
			,P.Username			
			,(CASE WHEN Allow = 1 THEN 1 ELSE 0 END) AS [Allow]
			,(CASE WHEN Allow = 0 THEN 1 ELSE 0 END) AS [Deny]
	FROM [Permissions] P INNER JOIN
		 [OperationCategories] OP ON P.OperationCode = OP.OperationCode INNER JOIN
		 [Resources] R ON P.ResourceID = R.ResourceID
	WHERE (Username = @Username)
	AND P.ResourceID IN (SELECT ResourceID FROM Resources WHERE ApplicationID = @ApplicationID)
	ORDER BY ResourceName



GO
/****** Object:  StoredProcedure [dbo].[UspGetUserPermissionsByRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetUserPermissionsByRole]
	 @ApplicationID int
	,@UserName varchar(128)

AS
BEGIN
	DECLARE @Table TABLE (ResourceID int, ResourceName nvarchar(250), FullPath varchar(250), OperationName nvarchar(200))

	INSERT INTO @Table
		SELECT   DISTINCT P.ResourceID
				,R.ResourceName
				,(R.Path + R.FileName) AS FullPath
				,''			
		FROM [Permissions] P INNER JOIN		 
			 [Resources] R ON P.ResourceID = R.ResourceID
		WHERE	P.RoleID IN (SELECT RoleID FROM [MemberShip] WHERE Username = @UserName) AND 
				P.ResourceID IN (SELECT ResourceID FROM Resources WHERE ApplicationID = @ApplicationID)	
		ORDER BY ResourceName

	DECLARE Cur CURSOR FOR
	SELECT	
			 ResourceID
			,OP.Name OperationName
	FROM	[Permissions] P INNER JOIN
			[OperationCategories] OP ON P.OperationCode = OP.OperationCode		
	WHERE	P.RoleID IN (SELECT RoleID FROM [MemberShip] WHERE Username = @UserName) AND 
			P.ResourceID IN (SELECT ResourceID FROM Resources WHERE ApplicationID = @ApplicationID)	

	OPEN CUR
	DECLARE @ResourceID int
	DECLARE @OperationName nvarchar(50)
	FETCH NEXT FROM Cur INTO @ResourceID, @OperationName
	WHILE @@FETCH_STATUS = 0
	BEGIN
		--PRINT CONVERT(varchar,@ResourceID) + '     ' + @OperationName
		UPDATE @Table
			SET OperationName = OperationName + @OperationName + ' ; '
		WHERE
			ResourceID = @ResourceID AND
			CHARINDEX(@OperationName, OperationName) = 0
		FETCH NEXT FROM Cur INTO @ResourceID, @OperationName
	END
	CLOSE CUR
	DEALLOCATE CUR

	SELECT * FROM @Table
END



GO
/****** Object:  StoredProcedure [dbo].[UspGetUsersAndRolesByResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetUsersAndRolesByResource]
	@ResourceID int
AS
	-- Select users duoc phan quyen vao resource
	SELECT
		 P.UserName as MemberName
		,P.UserName as MemberID		
		,'user' as MemberType
		,'User' as [Type]
		,P.OperationCode
		,P.Allow
	FROM
		[Permissions] P
	WHERE
		P.ResourceID = @ResourceID AND P.RoleID IS NULL
		

	UNION ALL

	--Select Roles duoc phan quyen vao resource
	SELECT
		 R.RoleCode as MemberName
		,CONVERT(varchar(128),P.RoleID) as MemberID		
		,'role' as MemberType
		,'Role' as [Type]
		,P.OperationCode
		,P.Allow
	FROM
		[Permissions] P INNER JOIN Roles R ON P.RoleID = R.RoleID
	WHERE
		P.ResourceID = @ResourceID AND P.UserName IS NULL



GO
/****** Object:  StoredProcedure [dbo].[UspGetUsersInRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspGetUsersInRole]
	@RoleID INT
AS
	SELECT U.Username, FullName, Email, Blocked, BlockedDate, CreatedTime, LastLogin, LastIP, FailPassword
	  FROM Users U INNER JOIN Membership M ON U.Username = M.Username
	 WHERE M.RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[UspInsertActionLog]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspInsertActionLog]
	@Date DateTime,
	@IP varchar(19),
	@UserName nvarchar(50),
	@Path nvarchar(100),
	@PageTitle nvarchar(100),
	@Operation nvarchar(50),
	@Data ntext
AS

	DECLARE @TableName VARCHAR(50) 
	SET	@TableName = 'ActionLogs{@Date}'
	SET @TableName = REPLACE(@TableName,'{@Date}',CONVERT(VARCHAR(6),@Date,112))

	IF NOT EXISTS( SELECT [NAME] FROM SysObjects WHERE [NAME] = @TableName ) -- Tao bang moi cho thang
	BEGIN
		EXEC dbo.Usp_ActionLogs_Create @Date
		EXEC dbo.Usp_ActionLogs_UpdateView
		EXEC dbo.Usp_ActionLogs_UpdateCurrentView @Date
	END
	INSERT INTO ActionLogs_CurrentView(Date, IP, UserName, [Path], PageTitle, Operation, Data)
	VALUES(@Date, @IP, @UserName, @Path, @PageTitle, @Operation, @Data)



GO
/****** Object:  StoredProcedure [dbo].[UspInsertErrorLogs]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspInsertErrorLogs]
	@Date DateTime,
	@CurrentUser nvarchar(50),
	@Path nvarchar(200),
	@Message nvarchar(MAX),
	@StackTrace	ntext,
	@TargetFunction nvarchar(MAX)
AS

	IF @Message <> 'Thread was being aborted.'
	BEGIN
		INSERT INTO dbo.ErrorLogs(Date, CurrentUser, [Path], [Message], StackTrace, TargetFunction)
		VALUES(@Date, @CurrentUser, @Path, @Message, @StackTrace, @TargetFunction)
	END
	



GO
/****** Object:  StoredProcedure [dbo].[UspInsertLoginLog]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspInsertLoginLog]
	 @UserName nvarchar(50)
	,@IP varchar(19)
	,@LoginResult int
	,@Date datetime
AS
BEGIN
	INSERT INTO [dbo].[LoginLogs]
			   ([UserName]
			   ,[IP]
			   ,[LoginResult]
			   ,[Date])
		 VALUES
			   (@UserName
			   ,@IP
			   ,@LoginResult
			   ,@Date)
END


GO
/****** Object:  StoredProcedure [dbo].[UspIsUserInRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspIsUserInRole]
	 @Username varchar(128)
	,@RoleCode varchar(15)
	,@ReturnCode int output
AS

SET NOCOUNT ON;

IF EXISTS( SELECT * FROM Membership WHERE Username = @Username AND RoleID = @RoleCode)
	SET @ReturnCode = 1 -- user is in role
ELSE
	SET @ReturnCode = 0 -- user is not in role



GO
/****** Object:  StoredProcedure [dbo].[UspMoveMenu]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UspMoveMenu]
	 @MenuID int
	,@Direction int -- 1: up, 0: down
	,@ErrorNumber int output
    ,@ErrorMessage nvarchar(150) output
AS
BEGIN

	BEGIN TRY
		DECLARE @CurrentPriority int, @ParentMenuID int
		DECLARE @TargetID int, @TargetPriority int

		SET @CurrentPriority = (SELECT Priority FROM Menus WHERE MenuID = @MenuID)
		SET @ParentMenuID = (SELECT ParentMenuID FROM Menus WHERE MenuID = @MenuID)

		IF @Direction = 1 -- Up
		BEGIN
			
			SET @TargetID = (SELECT TOP 1 MenuID FROM Menus WHERE (Priority < @CurrentPriority) AND (ParentMenuID = @ParentMenuID) ORDER BY Priority DESC)

			IF @TargetID IS NOT NULL
			BEGIN
				SET @TargetPriority = (SELECT Priority FROM Menus WHERE MenuID = @TargetID)
				UPDATE Menus SET Priority = @TargetPriority WHERE MenuID = @MenuID
				UPDATE Menus SET Priority = @CurrentPriority WHERE MenuID = @TargetID
			END

		END
		ELSE	-- Down
		BEGIN
					
			SET @TargetID = (SELECT TOP 1 MenuID FROM Menus WHERE Priority > @CurrentPriority AND (ParentMenuID = @ParentMenuID) ORDER BY Priority ASC)

			IF @TargetID IS NOT NULL
			BEGIN
				SET @TargetPriority = (SELECT Priority FROM Menus WHERE MenuID = @TargetID)
				UPDATE Menus SET Priority = @TargetPriority WHERE MenuID = @MenuID
				UPDATE Menus SET Priority = @CurrentPriority WHERE MenuID = @TargetID
			END

		END

		IF @@ROWCOUNT > 0
		BEGIN
			SET @ErrorNumber = 0
			SET @ErrorMessage = N'Sắp xếp menu thành công.'
		END
		ELSE
			RAISERROR(N'Có lỗi xảy ra.', 16, 1)

	END TRY

	BEGIN CATCH
		SET @ErrorNumber = ERROR_NUMBER()
		SET @ErrorMessage = ERROR_MESSAGE()
	END CATCH

	

END





GO
/****** Object:  StoredProcedure [dbo].[UspMoveMenu_Step]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspMoveMenu_Step]
	 @MenuID int
	,@Direction int -- 1:up  -- 0:down
	,@Step int = 1
AS
BEGIN	
	DECLARE @i int 
	SET @i = 1
	WHILE @i <= @Step
	BEGIN	
		EXEC dbo.UspMoveMenu @MenuID, @Direction, null, null
		SET @i = @i + 1
	END
END



GO
/****** Object:  StoredProcedure [dbo].[UspRecoveryUserPassword]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UspRecoveryUserPassword]
	@Username varchar(128)
   ,@QuestionID int
   ,@Answer varchar(64)
   ,@Password varchar(10) output
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	DECLARE @NewPass varchar(64)
	DECLARE @BinAnswer varbinary(150)

	SET @BinAnswer = HashBytes('SHA1', @Answer)

	IF EXISTS(SELECT Username FROM Users WHERE Username = @Username AND QuestionID = @QuestionID AND Answer = @BinAnswer)
	BEGIN
		-- generate password
		SET @NewPass = RIGHT(NEWID(), 10)

		-- update Password
		UPDATE Users SET Password = HashBytes('SHA1', @NewPass) WHERE Username = @Username

		IF @@ROWCOUNT > 0
		BEGIN
			SET @Password = @NewPass
			SET @ErrorNumber = 0
			SET @ErrorMessage = 'Success'
		END
		ELSE
			RAISERROR(N'Database error', 16, 1)
	END
	ELSE
		RAISERROR(N'Wrong recovery information', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH


GO
/****** Object:  StoredProcedure [dbo].[UspRemoveApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveApplication]
	 @ApplicationID int
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(100) output
AS

SET NOCOUNT ON;

BEGIN TRY
	
	BEGIN TRANSACTION

	-- Disable application
	UPDATE dbo.Applications SET [Status]=0 WHERE ApplicationID = @ApplicationID

	IF @@ROWCOUNT > 0 
	BEGIN

		-- Disable all related permissions
		DELETE dbo.[Permissions] 
		WHERE ResourceID IN (
			SELECT ResourceID FROM dbo.Resources WHERE ApplicationID = @ApplicationID
		)

		-- Disable all resources
		UPDATE dbo.Resources 
		SET [Status]=0
		WHERE ApplicationID = @ApplicationID

	END

	SET @ErrorNumber  = 0
	SET @ErrorMessage = N'The application has been removed successfully.'

	COMMIT TRANSACTION

END TRY

BEGIN CATCH

	
	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

	ROLLBACK TRANSACTION
	

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveMenu]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
declare @x int, @y nvarchar(150)
EXEC UspRemoveMenu '10000007', @x output, @y output
print @x
print @y
*/

CREATE PROCEDURE [dbo].[UspRemoveMenu]
	@MenuID int
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF EXISTS(SELECT MenuID FROM Menus WHERE ParentMenuID = @MenuID) BEGIN
		SET @ErrorNumber = 1
		SET @ErrorMessage = 'You must remove all its child menus'
		RETURN
	END

	DELETE Menus WHERE MenuID = @MenuID
	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Update menu successfully'

END TRY

BEGIN CATCH
	SET @ErrorNumber = 999
	SET @ErrorMessage = ERROR_MESSAGE()
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveOperation]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveOperation]
	@OperationCode varchar(50)
   ,@ResourceTypeCode varchar(50)
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(100) output
AS

BEGIN TRY

	BEGIN TRANSACTION
	
	IF EXISTS (SELECT OperationCode FROM Operations WHERE OperationCode = @OperationCode AND ResourceTypeCode = @ResourceTypeCode)
	BEGIN
		-- remove all permission
		DELETE [Permissions] WHERE OperationCode = @OperationCode AND ResourceID IN (SELECT ResourceID FROM [Resources] WHERE ResourceTypeCode = @ResourceTypeCode)
		
		-- remove operation
		DELETE Operations WHERE OperationCode = @OperationCode AND ResourceTypeCode = @ResourceTypeCode	
	END
	COMMIT TRANSACTION

	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Remove operation successfully'

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveOperationCategory]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveOperationCategory]
	 @OperationCode int
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;
 
BEGIN TRY

	IF EXISTS(SELECT OperationCode FROM Operations WHERE OperationCode = @OperationCode)
		RAISERROR(N'Can not remove operation category. It already in use', 16, 1)
	
	IF @OperationCode = 1
		RAISERROR(N'Cannot delete this operation category. It`s default operation category', 16, 1)

	DELETE OperationCategories WHERE OperationCode = @OperationCode
	
	SET @ErrorNumber = 0
	SET @ErrorMessage = N'The operation category has been remove successfully'
	
END TRY


BEGIN CATCH

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemovePermission]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemovePermission]
	@ResourceID int,
	@OperationCode int,
	@ObjectName varchar(128),
	@ObjectType int
AS
BEGIN
	DECLARE @ResourceTypeCode varchar(50)

	-- checking for valid resource
	SELECT @ResourceTypeCode = ResourceTypeCode FROM Resources WHERE ResourceID = @ResourceID

	IF @@ROWCOUNT = 0
		RAISERROR(N'Remove permission fail. Invalid resource', 16, 1)


	-- checking for valid operation
	IF NOT EXISTS(SELECT OperationCode FROM Operations WHERE ResourceTypeCode = @ResourceTypeCode)
		RAISERROR(N'Remove permission fail. Invalid operation', 16, 1)

	IF @ObjectType = 0
		DELETE [dbo].[Permissions] WHERE
				ResourceID = @ResourceID
			AND	OperationCode = @OperationCode
			AND	UserName = @ObjectName
			AND	RoleID IS NULL
	ELSE IF @ObjectType = 1
		DELETE [dbo].[Permissions] WHERE
				ResourceID = @ResourceID
			AND	OperationCode = @OperationCode
			AND	UserName IS NULL
			AND	CONVERT(varchar(128),RoleID) = @ObjectName
	ELSE
		RAISERROR(N'Remove permission fail. Invalid object type, object type must be 0 for user or 1 for role', 16, 1)	

END



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveResource]
    @ResourceID int
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY
	
	BEGIN TRANSACTION 
	
	-- remove all related menus  
	DELETE Menus WHERE ResourceID = @ResourceID

	-- remove all permissions on this resource
	DELETE [Permissions] WHERE ResourceID = @ResourceID

	-- remove resource
	DELETE Resources WHERE ResourceID = @ResourceID

	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Remove resource successfully'

	COMMIT TRANSACTION

END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveResourceType]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveResourceType]
			@ResourceTypeCode varchar(50)
		   ,@ErrorNumber int output
		   ,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	IF EXISTS(SELECT ResourceTypeCode FROM Operations WHERE ResourceTypeCode = @ResourceTypeCode) OR
		EXISTS(SELECT ResourceTypeCode FROM Resources WHERE ResourceTypeCode = @ResourceTypeCode)
		RAISERROR(N'Loại tài nguyên đang sử dụng. Không thể xóa được.', 16, 1)

	DELETE ResourceTypes WHERE ResourceTypeCode = @ResourceTypeCode

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveRole]
	 @RoleID int
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	BEGIN TRANSACTION
 
	-- remove all permission relate to this role
	DELETE dbo.[Permissions] WHERE RoleID = @RoleID

	-- remove all membership relate to this role
	DELETE dbo.[Membership] WHERE RoleID = @RoleID

	-- remove role
	DELETE dbo.[Roles] WHERE RoleID = @RoleID

	SET @ErrorNumber = 0
	SET @ErrorMessage = N'Remove role successfully'

	COMMIT TRANSACTION

END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveUser]
	@Username varchar(128)
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	BEGIN TRANSACTION
	-- remove user from permission
	DELETE dbo.Permissions WHERE Username = @Username

	-- remove user from role
	DELETE dbo.Membership WHERE Username = @Username

	-- remove user
	DELETE dbo.Users where Username = @Username

	COMMIT TRANSACTION
	SET @ErrorNumber = 0
	SET @ErrorMessage = ''

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()
	
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveUserFromResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveUserFromResource]
	@ResourceID int,	
	@ObjectName varchar(128),
	@ObjectType int
AS
BEGIN
	DECLARE @ResourceTypeCode varchar(50)

	-- checking for valid resource
	SELECT @ResourceTypeCode = ResourceTypeCode FROM Resources WHERE ResourceID = @ResourceID

	IF @@ROWCOUNT = 0
		RAISERROR(N'Remove permission fail. Invalid resource', 16, 1)

	IF @ObjectType = 0
		DELETE [dbo].[Permissions] WHERE
				ResourceID = @ResourceID			
			AND	UserName = @ObjectName
			AND	RoleID IS NULL
	ELSE IF @ObjectType = 1
		DELETE [dbo].[Permissions] WHERE
				ResourceID = @ResourceID			
			AND	UserName IS NULL
			AND	CONVERT(varchar(128),RoleID) = @ObjectName
	ELSE
		RAISERROR(N'Remove permission fail. Invalid object type, object type must be 0 for user or 1 for role', 16, 1)	

END



GO
/****** Object:  StoredProcedure [dbo].[UspRemoveUserFromRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspRemoveUserFromRole]
	 @Username varchar(128)
	,@RoleID varchar(15)
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150)output
AS

SET NOCOUNT ON;

BEGIN TRY

	DELETE Membership WHERE Username = @Username AND RoleID = @RoleID

	SET @ErrorNumber = 0
	SET @ErrorMessage = N'Success'

END TRY

BEGIN CATCH

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspSetPermission]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
DECLARE @PermissionID int
DECLARE @ErrorNumber int
DECLARE @ErrorMessage nvarchar(150)

EXEC [UspSetPermission] '10000024', '3', 'huylq', 0, 1, @PermissionID, @ErrorNumber, @ErrorMessage

*/


CREATE PROCEDURE [dbo].[UspSetPermission]
	 @ResourceID int
	,@OperationCode int
	,@ObjectName varchar(128)
	,@ObjectType int = 0   -- 0:User, 1:Role
	,@Allow bit = 0 --0:Deny, 1:Allow
	,@PermissionID int output
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	DECLARE @ResourceTypeCode varchar(50)

	-- checking for valid resource
	SELECT @ResourceTypeCode = ResourceTypeCode FROM Resources WHERE ResourceID = @ResourceID

	IF @@ROWCOUNT = 0
		RAISERROR(N'Set permission fail. Invalid resource', 16, 1)


	-- checking for valid operation
	IF NOT EXISTS(SELECT OperationCode FROM Operations WHERE ResourceTypeCode = @ResourceTypeCode)
		RAISERROR(N'Set permission fail. Invalid operation', 16, 1)

	--Neu chua co user/role tuong ung voi operation nay trong table: insert
	IF NOT EXISTS(
		SELECT PermissionID
		FROM [dbo].[Permissions]
		WHERE ResourceID = @ResourceID AND OperationCode = @OperationCode
			AND ((UserName = @ObjectName AND RoleID IS NULL) OR (UserName IS NULL AND CONVERT(varchar(128), RoleID) = @ObjectName))
	)
	BEGIN
		IF @ObjectType = 0
		BEGIN
			INSERT INTO [dbo].[Permissions]
					   ([ResourceID]
					   ,[OperationCode]
					   ,[Username]
					   ,[RoleID]
					   ,[Allow])
				 VALUES
					   (@ResourceID
					   ,@OperationCode
					   ,@ObjectName
					   ,NULL
					   ,@Allow)
			SET @PermissionID = @@IDENTITY
		END
		ELSE IF @ObjectType = 1
		BEGIN
			INSERT INTO [dbo].[Permissions]
					   ([ResourceID]
					   ,[OperationCode]
					   ,[Username]
					   ,[RoleID]
					   ,[Allow])
				 VALUES
					   (@ResourceID
					   ,@OperationCode
					   ,NULL
					   ,@ObjectName
					   ,@Allow)
			SET @PermissionID = @@IDENTITY
		END
		ELSE
			RAISERROR(N'Set permission fail. Invalid object type, object type must be 0 for user or 1 for role', 16, 1)
	END
	ELSE	--Da ton tai user/role nay trong table: update
	BEGIN
		IF @ObjectType = 0	--User
		BEGIN
			UPDATE [dbo].[Permissions]
			SET [Allow] = @Allow
			WHERE 
				UserName = @ObjectName AND RoleID IS NULL AND OperationCode = @OperationCode AND ResourceID = @ResourceID

			SET @PermissionID = (SELECT PermissionID FROM [Permissions] WHERE UserName = @ObjectName AND RoleID IS NULL AND OperationCode = @OperationCode AND ResourceID = @ResourceID)
		END
		ELSE IF @ObjectType = 1	--Role
		BEGIN
			UPDATE [dbo].[Permissions]
			SET [Allow] = @Allow
			WHERE 
				UserName IS NULL AND CONVERT(varchar(128), RoleID) = @ObjectName AND OperationCode = @OperationCode AND ResourceID = @ResourceID

			SET @PermissionID = (SELECT PermissionID FROM [Permissions] WHERE UserName IS NULL AND CONVERT(varchar(128),RoleID) = @ObjectName AND OperationCode = @OperationCode AND ResourceID = @ResourceID)
		END
		ELSE
			RAISERROR(N'Update permission fail. Invalid object type, object type must be 0 for user or 1 for role', 16, 1)
	END

	

	IF @@ROWCOUNT >= 1
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = N'Set permission successfully'
	END
	ELSE
		RAISERROR(N'Set permission fail. Database error!', 16, 1)



END TRY

BEGIN CATCH
	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspSetPermissionForRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
DECLARE @DataPermission varchar(max)
SET @DataPermission = '<Permissions><Permission RoleCode="Role6" ResourceID="10000011" OperationCode="3" IsAllowed="1" /><Permission RoleCode="Role6" ResourceID="10000011" OperationCode="6" IsAllowed="1" /><Permission RoleCode="Role6" ResourceID="10000011" OperationCode="8" IsAllowed="1" /><Permission RoleCode="Role6" ResourceID="10000011" OperationCode="9" IsAllowed="1" /></Permissions>'
EXEC UspSetPermissionForRole @DataPermission
*/
CREATE PROCEDURE [dbo].[UspSetPermissionForRole]
	@ApplicationID int,
	@RoleCode varchar(50),
	@ResourceID int,
	@DataPermission varchar(max),
	@ErrorNumber int output,
	@ErrorMessage nvarchar(100) output
AS
BEGIN TRY
	BEGIN TRANSACTION
	
	DECLARE @RoleID int
	
	--lay RoleID dua tren ApplicationID va RoleCode
	SELECT @RoleID=RoleID FROM Roles WHERE ApplicationID=@ApplicationID AND RoleCode=@RoleCode
	
	--xoa het cac quyen da duoc thiet lap truoc do
	DELETE FROM dbo.[Permissions] WHERE RoleID=@RoleID AND ResourceID=@ResourceID
	
	--thiet lap lai cac quyen da duoc chi dinh
	DECLARE @iDoc int
	EXEC sp_xml_preparedocument @iDoc OUTPUT, @DataPermission
	INSERT INTO [Permissions](ResourceID, OperationCode, RoleID, Allow)
	SELECT ResourceID, OperationCode, @RoleID, IsAllowed FROM OPENXML(@iDoc, '/Permissions/Permission',1) WITH(RoleCode varchar(50), ResourceID int, OperationCode int, IsAllowed int)
	EXEC sp_xml_removedocument @iDoc
	SET @ErrorNumber  = 0
	SET @ErrorMessage = N'Set permission for role {1} on resource {2} is successful'
	SET @ErrorMessage = REPLACE(@ErrorMessage, '{1}', @RoleID)
	SET @ErrorMessage = REPLACE(@ErrorMessage, '{2}', STR(@ResourceID))
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()
	ROLLBACK TRANSACTION
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspSetPermissionForUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
DECLARE @DataPermission varchar(max)
DECLARE @DataPermission varchar(max)
DECLARE @DataPermission varchar(max)
DECLARE @DataPermission varchar(max)
DECLARE @DataPermission varchar(max)
SET @DataPermission = '<Permissions><Permission UserName="hieplh" ResourceID="10000011" OperationCode="2" IsAllowed="1" /><Permission UserName="hieplh" ResourceID="10000011" OperationCode="3" IsAllowed="0" /><Permission UserName="hieplh" ResourceID="10000011" OperationCode="8" IsAllowed="1" /></Permissions>'
EXEC UspSetPermissionForUser @DataPermission

*/
CREATE PROCEDURE [dbo].[UspSetPermissionForUser]
	@UserName varchar(50),
	@ResourceID int,
	@DataPermission varchar(max),
	@ErrorNumber int output,
	@ErrorMessage nvarchar(max) output
AS
BEGIN TRY
	BEGIN TRANSACTION
	--xoa het cac quyen da duoc thiet lap truoc do
	DELETE FROM dbo.[Permissions] WHERE UserName=@UserName AND ResourceID=@ResourceID
	
	--thiet lap lai cac quyen da duoc chi dinh
	DECLARE @iDoc int

	EXEC sp_xml_preparedocument @iDoc OUTPUT, @DataPermission

	INSERT INTO [Permissions](ResourceID, OperationCode, UserName, Allow)
	SELECT ResourceID, OperationCode, UserName, IsAllowed FROM OPENXML(@iDoc, '/Permissions/Permission',1) WITH(UserName varchar(50), ResourceID int, OperationCode int, IsAllowed int)

	EXEC sp_xml_removedocument @iDoc

	SET @ErrorNumber  = 0
	SET @ErrorMessage = N'Set permission for user {1} on Resource {2} is successful'
	SET @ErrorMessage = REPLACE(@ErrorMessage, '{1}', @UserName)
	SET @ErrorMessage = REPLACE(@ErrorMessage, '{2}', STR(@ResourceID))

	COMMIT TRANSACTION

END TRY

BEGIN CATCH
	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

	ROLLBACK TRANSACTION

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspSetUserPassword]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UspSetUserPassword]
	 @Username varchar(128)
	,@Password varchar(64)
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS
	
	SET NOCOUNT ON;

BEGIN TRY
	
	IF len(@Password) < 6
		RAISERROR(N'Đặt mật khẩu không thành công: Mật khẩu ít hơn 6 ký tự', 16, 1)

	DECLARE @BinPass varbinary(150)

	SET @BinPass = HashBytes('SHA1', @Password)

	UPDATE Users SET Password = @BinPass, Blocked = 0 WHERE Username = @Username

	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Đặt mật khẩu thành công'
	END
	ELSE
		RAISERROR(N'Không tìm thấy tài khoản.', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUnblockUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUnblockUser]
	@Username varchar(128)
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	UPDATE Users
	   SET Blocked = 0, FailPassword = 0
	 WHERE Username = @Username

	IF @@ROWCOUNT = 0
		RAISERROR(N'Không tìm thấy user', 16, 1)

	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Mở khóa user thành công'
	
END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateApplication]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateApplication]
	 @ApplicationID int
	,@Name nvarchar(50)
	,@Description nvarchar(250)
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(100) output
AS
BEGIN TRY

	DECLARE @ResourceID int
	DECLARE @Flag bit
	SET @Flag = 1
	--lay ID(ResourceID) cua node ROOT (tuong ung voi Application) de tien hanh cap nhat ResourceName và DisplayName
	SELECT @ResourceID = ResourceID FROM [dbo].[Resources] R 
										 INNER JOIN dbo.Applications A ON R.ResourceName=A.[Name]
	WHERE R.ApplicationID=@ApplicationID AND ResourceTypeCode='DEFAULT'

	IF EXISTS(SELECT [Name] FROM dbo.Applications WHERE [Name] = @Name AND ApplicationID <> @ApplicationID AND [Status]=1)
	BEGIN
		SET @ErrorNumber  = 1
		SET @ErrorMessage = N'Update application fail. The application Name Exists.'	
	END
	ELSE BEGIN
		BEGIN TRANSACTION
		UPDATE [dbo].[Applications]
		   SET [Name] = @Name
			  ,[Description] = @Description
		 WHERE [ApplicationID] = @ApplicationID
		
		IF @@ROWCOUNT > 0 
		BEGIN
			--tien hanh cap nhat cho node ROOT
			UPDATE [dbo].[Resources] SET ResourceName=@Name WHERE ResourceID=@ResourceID
			UPDATE [dbo].[Menus] SET DisplayName=@Name WHERE ResourceID=@ResourceID	
			IF @@ROWCOUNT > 0 BEGIN	
				SET @ErrorNumber  = 0
				SET @ErrorMessage = N'The application has been updated successfully.'	
				SET @Flag = 1
			END
			ELSE BEGIN
				SET @ErrorNumber  = 1
				SET @ErrorMessage = N'Update application fail. Cannot find the ROOT Node of this Application.'	
				SET @Flag = 0
			END
		END
		ELSE BEGIN
			SET @ErrorNumber  = 1
			SET @ErrorMessage = N'Update application fail. The application can not be found.'	
			SET @Flag = 0
		END
		IF @Flag=1
			COMMIT
		ELSE
			ROLLBACK
	END	
END TRY
BEGIN CATCH
	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateMenu]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateMenu]
	@MenuID int
   ,@DisplayName nvarchar(100)
   ,@ResourceID int
   ,@ParentMenuID int
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF NOT EXISTS(SELECT ResourceID FROM Resources WHERE ResourceID = @ResourceID)
		RAISERROR(N'Update menu fail. Resource can not be found', 16, 1)

	IF NOT @ParentMenuID IS NULL AND @ParentMenuID <> 0
		IF NOT EXISTS(SELECT MenuID FROM Menus WHERE MenuID = @ParentMenuID)
			RAISERROR(N'Update menu fail. Parent menu was not found', 16, 1)

	UPDATE [dbo].[Menus]
	   SET [DisplayName] = @DisplayName
		  ,[ResourceID] = @ResourceID
		  ,[ParentMenuID] = @ParentMenuID
	 WHERE MenuID = @MenuID
	
	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Update menu successfully'
	END
	ELSE
		RAISERROR(N'Update menu fail. Database error!', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateOperation]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateOperation]
	@OperationCode varchar(50)
   ,@ResourceTypeCode varchar(50)
   ,@Description nvarchar(250)
   ,@ErrorNumber int output
   ,@ErrorMessage int output
AS

BEGIN TRY

	UPDATE [dbo].[Operations]
	   SET [Description] = @Description
	 WHERE [OperationCode] = @OperationCode AND
		   [ResourceTypeCode] = @ResourceTypeCode

	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Create new operation successfully'
	END
	ELSE
		RAISERROR(N'Database error!', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateOperationCategory]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateOperationCategory]
	 @OperationCode int
	,@Name nvarchar(250)
	,@Description nvarchar(500)
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;
 
BEGIN TRY

	IF EXISTS (SELECT OperationCode FROM dbo.OperationCategories WHERE [Name] = @Name AND OperationCode <> @OperationCode)
		RAISERROR (N'Tên thao tác đã tồn tại.', 16, 1)

	UPDATE [dbo].[OperationCategories]
	   SET [Name] = @Name,[Description] = @Description
	 WHERE [OperationCode] = @OperationCode

	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = N'Cập nhật loại thao tác thành công.'
	END
	ELSE
		RAISERROR (N'Không tìm thấy loại thao tác.', 16, 1)

END TRY


BEGIN CATCH

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdatePermission]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdatePermission]
	 @PermissionID int
	,@Allow int
AS
BEGIN
	IF @Allow = -1
	BEGIN
		DELETE [Permissions] WHERE PermissionID = @PermissionID
	END
	ELSE
	BEGIN
		UPDATE [Permissions] SET Allow = @Allow WHERE PermissionID = @PermissionID
	END		
END


GO
/****** Object:  StoredProcedure [dbo].[UspUpdateResource]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateResource]
    @ResourceID int
   ,@ResourceTypeCode varchar(50)
   ,@ApplicationID int
   ,@ResourceName nvarchar(250)
   ,@Path varchar(250)
   ,@FileName varchar(50)
   ,@Link varchar(250)
   ,@Status bit
   ,@Token varchar(50)=NULL
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF NOT EXISTS(SELECT ResourceID FROM [dbo].[Resources] WHERE ResourceID = @ResourceID)
		RAISERROR(N'Cập nhật thất bại: Không tìm thấy tài nguyên.', 16, 1)

	IF EXISTS(SELECT ResourceID FROM [dbo].[Resources] WHERE ResourceID <> @ResourceID AND Token=@Token AND @Token NOT IN(NULL, 'DEFAULT'))
		RAISERROR(N'Cập nhật thất bại: Token đã tồn tại trong hệ thống.', 16, 1)

	IF EXISTS (SELECT ResourceID FROM [dbo].[Resources] WHERE ResourceID <> @ResourceID AND ApplicationID = @ApplicationID AND ResourceName = @ResourceName)
		RAISERROR(N'Cập nhật thất bại: Tên tài nguyên đã tồn tại trong hệ thống.', 16,1)

	IF NOT EXISTS(SELECT ResourceID FROM [dbo].[Resources] WHERE ResourceID = @ResourceID AND ResourceTypeCode = @ResourceTypeCode)
		AND EXISTS(SELECT ResourceID FROM [dbo].[Permissions] WHERE ResourceID = @ResourceID)
			RAISERROR(N'Tài nguyên đã được phân quyền. Vui lòng gỡ bỏ các phân quyền trước khi cập nhật.', 16, 1)

	UPDATE [dbo].[Resources]
	   SET [ResourceName] = @ResourceName
		  ,[Path] = @Path
		  ,[FileName] = @FileName
		  ,[Link] = @Link
		  ,[Status] = @Status
		  ,[Token] = @Token
	 WHERE ResourceID = @ResourceID

	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Cập nhật thành công.'

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateResourceStatus]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateResourceStatus]
    @ResourceID int
   ,@Status bit
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

BEGIN TRY

	IF NOT EXISTS(SELECT ResourceID FROM Resources WHERE ResourceID = @ResourceID)
		RAISERROR(N'Update resource fail. Resource can not be found', 16, 1)

	IF NOT EXISTS(SELECT ResourceID FROM Resources WHERE ResourceID = @ResourceID)
		AND EXISTS(SELECT ResourceID FROM [Permissions] WHERE ResourceID = @ResourceID)
			RAISERROR(N'You must remove all permissions on this resource before updating information', 16, 1)

	UPDATE [dbo].[Resources]
	   SET [Status] = @Status
	 WHERE ResourceID = @ResourceID

	SET @ErrorNumber = 0
	SET @ErrorMessage = 'Update resource status sucessfully'

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateResourceType]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateResourceType]
			@ResourceTypeCode varchar(50)
           ,@Name nvarchar(50)           
		   ,@ErrorNumber int output
		   ,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	UPDATE [dbo].[ResourceTypes]
	   SET [Name] = @Name		  
	 WHERE [ResourceTypeCode] = @ResourceTypeCode

	IF @@ROWCOUNT = 0
		RAISERROR(N'Cập nhật không thành công. Không tìm thấy mã loại.', 16, 1)

	SET @ErrorNumber = 0
	SET @ErrorMessage = ''

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateRole]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateRole]
	@RoleID int
	,@RoleCode varchar(50)
    ,@RoleName nvarchar(50)
	, @ApplicationID int
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON;

BEGIN TRY

	IF EXISTS(SELECT ApplicationID FROM [dbo].[Roles] WHERE [RoleID] = @RoleID AND ApplicationID <> @ApplicationID )
		DELETE [dbo].[Permissions] WHERE RoleID = @RoleID

	UPDATE [dbo].[Roles]
	   SET [RoleName] = @RoleName
			,[Rolecode] = @RoleCode
			, ApplicationID = @ApplicationID
	 WHERE [RoleID] = @RoleID

	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 0
		SET @ErrorMessage = N'Update role successfully'
	END
	ELSE
		RAISERROR (N'Update role fail. The role code can not be found.', 16, 1)

END TRY

BEGIN CATCH

	SET @ErrorNumber  = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspUpdateUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspUpdateUser]
	@Username varchar(128)
   ,@FullName nvarchar(64)
   ,@Email varchar(128)
	,@Blocked bit
   ,@ErrorNumber int output
   ,@ErrorMessage nvarchar(150) output
AS

SET NOCOUNT ON

BEGIN TRY

	UPDATE [dbo].[Users]
	   SET [FullName] = @FullName
		  ,[Email] = @Email
			,Blocked = @Blocked
			,FailPassword = 0
	 WHERE Username = @Username

	IF @@ROWCOUNT > 0
	BEGIN 
		SET @ErrorNumber = 0
		SET @ErrorMessage = 'Update user successfully'
	END
	ELSE
		RAISERROR(N'Update user information fail. Username was not found', 16, 1)

END TRY

BEGIN CATCH
 
	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



GO
/****** Object:  StoredProcedure [dbo].[UspValidateUser]    Script Date: 8/16/2017 8:56:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UspValidateUser]
	 @Username varchar(128)
	,@Password varchar(64)
	,@ErrorNumber int output
	,@ErrorMessage nvarchar(150) output
AS
BEGIN TRY
	
	DECLARE @BinPass varbinary(150)
	DECLARE @UserBinPass varbinary(150)
	
	SET @BinPass = HashBytes('SHA1', @Password)

	SELECT @UserBinPass = Password FROM Users WHERE Username = @Username

	IF @@ROWCOUNT > 0
	BEGIN
		SET @ErrorNumber = 1 
		SET @ErrorMessage = N'Invalid user name'
	END

	IF @BinPass <> @UserBinPass
	BEGIN
		SET @ErrorNumber = 2 
		SET @ErrorMessage = N'Wrong password'
	END
	ELSE
	BEGIN
		SET @ErrorNumber = 0 
		SET @ErrorMessage = N'Success'
	END

END TRY

BEGIN CATCH

	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorMessage = ERROR_MESSAGE()

END CATCH



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
         Begin Table = "ActionLogs201107"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActionLogs_CurrentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActionLogs_CurrentView'
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
         Begin Table = "ActionLogs201107"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 4
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
         Column = 1530
         Alias = 900
         Table = 1905
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActionLogs_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActionLogs_View'
GO
