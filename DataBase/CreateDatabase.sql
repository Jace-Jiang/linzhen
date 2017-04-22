USE [XGhmsdb]
GO
/****** Object:  Table [dbo].[xg_users]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[role_type] [int] NULL,
	[user_number] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[user_name] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[password] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[is_lock] [int] NULL,
	[add_time] [datetime] NULL,
 CONSTRAINT [PK_xg_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'role_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'role_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'role_type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员类型1超管2系管' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'role_type'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'user_number'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学生学号和教师工号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'user_number'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'user_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'user_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'password'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'password'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'is_lock'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否锁定（1代表锁定，0代表正常）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'is_lock'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users', N'COLUMN',N'add_time'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users', @level2type=N'COLUMN',@level2name=N'add_time'
GO
SET IDENTITY_INSERT [dbo].[xg_users] ON
INSERT [dbo].[xg_users] ([id], [role_id], [role_type], [user_number], [user_name], [password], [is_lock], [add_time]) VALUES (3, 1, 1, N'Administrator', N'Administrator', N'7C4A8D09CA3762AF61E59520943DC26494F8941B', 0, NULL)
SET IDENTITY_INSERT [dbo].[xg_users] OFF
/****** Object:  Table [dbo].[xg_user_message]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_user_message]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_user_message](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [tinyint] NULL,
	[sender] [int] NULL,
	[receiver] [int] NULL,
	[content] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
	[is_read] [tinyint] NULL,
	[send_time] [datetime] NULL,
	[read_time] [datetime] NULL,
	[last_id] [int] NULL,
 CONSTRAINT [PK_xg_user_message] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短信息类型：  0系统消息  1学生消息  2老师消息  3管理员消息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'type'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'sender'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'sender'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'receiver'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'receiver'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'content'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'content'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'is_read'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否查看0未阅1已阅' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'is_read'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'send_time'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'send_time'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'read_time'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'阅读时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'read_time'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_user_message', N'COLUMN',N'last_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'根据上个ID回复的信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_user_message', @level2type=N'COLUMN',@level2name=N'last_id'
GO
/****** Object:  Table [dbo].[xg_term]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_term]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_term](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[term_name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_xg_term] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_term', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学期编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_term', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_term', N'COLUMN',N'term_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学期名称（2014~2015年度第二学期）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_term', @level2type=N'COLUMN',@level2name=N'term_name'
GO
/****** Object:  Table [dbo].[xg_role]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[role_type] [tinyint] NULL,
	[is_sys] [tinyint] NULL,
 CONSTRAINT [PK_xg_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_role', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_role', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_role', N'COLUMN',N'role_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_role', @level2type=N'COLUMN',@level2name=N'role_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_role', N'COLUMN',N'role_type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色类型1超管2系管' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_role', @level2type=N'COLUMN',@level2name=N'role_type'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_role', N'COLUMN',N'is_sys'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否系统默认0否1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_role', @level2type=N'COLUMN',@level2name=N'is_sys'
GO
SET IDENTITY_INSERT [dbo].[xg_role] ON
INSERT [dbo].[xg_role] ([id], [role_name], [role_type], [is_sys]) VALUES (1, N'Administrator', 1, 1)
INSERT [dbo].[xg_role] ([id], [role_name], [role_type], [is_sys]) VALUES (2, N'Admin', 2, 1)
INSERT [dbo].[xg_role] ([id], [role_name], [role_type], [is_sys]) VALUES (3, N'CollegeAdmin', NULL, 1)
INSERT [dbo].[xg_role] ([id], [role_name], [role_type], [is_sys]) VALUES (4, N'HeadTeacher', NULL, 1)
INSERT [dbo].[xg_role] ([id], [role_name], [role_type], [is_sys]) VALUES (5, N'Teacher', NULL, 1)
INSERT [dbo].[xg_role] ([id], [role_name], [role_type], [is_sys]) VALUES (6, N'Student', NULL, 1)
SET IDENTITY_INSERT [dbo].[xg_role] OFF
/****** Object:  Table [dbo].[xg_homework_student]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_homework_student]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_homework_student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NULL,
	[student_id] [int] NULL,
	[submit_time] [datetime] NULL,
	[submit_file] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
	[submit_content] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
	[homework_score] [int] NULL,
	[homework_comment] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
	[homework_status] [int] NULL,
 CONSTRAINT [PK_xg_homework_student] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'homework_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'homework_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'student_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学生编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'student_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'submit_time'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'submit_time'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'submit_file'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业附件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'submit_file'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'submit_content'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'submit_content'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'homework_score'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该次作业的分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'homework_score'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'homework_comment'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老师评价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'homework_comment'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_homework_student', N'COLUMN',N'homework_status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业的状态，（0：未完成（默认）1：送交批阅中，2：正在批阅，3：重做，4：完成）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_homework_student', @level2type=N'COLUMN',@level2name=N'homework_status'
GO
/****** Object:  Table [dbo].[xg_course_student]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_course_student]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_course_student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[student_id] [int] NULL,
 CONSTRAINT [PK_xg_course_student] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_student', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_student', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_student', N'COLUMN',N'course_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课程编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_student', @level2type=N'COLUMN',@level2name=N'course_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_student', N'COLUMN',N'student_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学生编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_student', @level2type=N'COLUMN',@level2name=N'student_id'
GO
/****** Object:  Table [dbo].[xg_course_homework]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_course_homework]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_course_homework](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[homework_name] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[homework_info] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
	[homework_beginTime] [datetime] NULL,
	[homework_endTime] [datetime] NULL,
 CONSTRAINT [PK_xg_course_homework] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_homework', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_homework', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_homework', N'COLUMN',N'course_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课程ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_homework', @level2type=N'COLUMN',@level2name=N'course_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_homework', N'COLUMN',N'homework_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_homework', @level2type=N'COLUMN',@level2name=N'homework_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_homework', N'COLUMN',N'homework_info'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业的说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_homework', @level2type=N'COLUMN',@level2name=N'homework_info'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_homework', N'COLUMN',N'homework_beginTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_homework', @level2type=N'COLUMN',@level2name=N'homework_beginTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course_homework', N'COLUMN',N'homework_endTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course_homework', @level2type=N'COLUMN',@level2name=N'homework_endTime'
GO
/****** Object:  Table [dbo].[xg_course]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_course]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_course](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_number] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[course_name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[term_id] [int] NULL,
	[teacher] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[other_teacher] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[student_leader] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[course_info] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[college_id] [int] NULL,
 CONSTRAINT [PK_xg_course] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号，课程编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'course_number'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'专业课程编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'course_number'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'course_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课程名称（例：ASP.NET程序设计）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'course_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'term_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学期编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'term_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'teacher'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授课老师' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'teacher'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'other_teacher'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'助教，其他老师，多名时用；隔开' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'other_teacher'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'student_leader'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学生负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'student_leader'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'course_info'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'课程说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'course_info'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_course', N'COLUMN',N'college_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学院ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_course', @level2type=N'COLUMN',@level2name=N'college_id'
GO
/****** Object:  Table [dbo].[xg_college]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_college]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_college](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[college_name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[college_admin] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_xg_college] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_college', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_college', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_college', N'COLUMN',N'college_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学院（部门）名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_college', @level2type=N'COLUMN',@level2name=N'college_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_college', N'COLUMN',N'college_admin'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学院（部门）管理教师的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_college', @level2type=N'COLUMN',@level2name=N'college_admin'
GO
/****** Object:  Table [dbo].[xg_classes]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_classes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_classes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[class_name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[college_id] [int] NULL,
	[head_teacher] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[class_leader] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[squad_leader] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[class_group_secretary] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[study_secretary] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[life_secretary] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_xg_class] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级编号（自增列）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'class_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级名称（例：11级信息管理与信息系统班）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'class_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'college_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学院ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'college_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'head_teacher'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班主任（辅导员）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'head_teacher'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'class_leader'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'class_leader'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'squad_leader'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副班长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'squad_leader'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'class_group_secretary'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团支书' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'class_group_secretary'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'study_secretary'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学习委员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'study_secretary'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_classes', N'COLUMN',N'life_secretary'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生活委员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_classes', @level2type=N'COLUMN',@level2name=N'life_secretary'
GO
/****** Object:  Table [dbo].[xg_users_info]    Script Date: 05/07/2015 22:54:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xg_users_info]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xg_users_info](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[real_name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[sex] [int] NULL,
	[birthday] [datetime] NULL,
	[telephone] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL,
	[email] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL,
	[college_id] [int] NULL,
	[class_id] [int] NULL,
	[major] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[address] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[explain] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_xg_users_info] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'user_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'user_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'role_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'role_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'real_name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'real_name'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'sex'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别，0女性，1男性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'sex'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'birthday'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'birthday'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'telephone'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'telephone'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'email'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'email'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'college_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学院ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'college_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'class_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'class_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'major'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学生专业，老师的职称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'major'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'address'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'address'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'xg_users_info', N'COLUMN',N'explain'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'个人简介' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'xg_users_info', @level2type=N'COLUMN',@level2name=N'explain'
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListTerSend]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListTerSend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	老师消息的列表，已发送的消息，已知发送者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListTerSend]
	-- Add the parameters for the stored procedure here
	(@sender int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     sender = @sender and xg_user_message.type = 2
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListTerReceive]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListTerReceive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	老师消息的列表，已接收的消息，已知接收者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListTerReceive]
	-- Add the parameters for the stored procedure here
	(@receiver int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     receiver = @receiver and xg_user_message.type = 2
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListSysReceive]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListSysReceive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	系统消息的列表，已接收的消息，已知接收者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListSysReceive]
	-- Add the parameters for the stored procedure here
	(@receiver int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     receiver = @receiver and xg_user_message.type = 0
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListStuSend]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListStuSend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	学生消息的列表，已发送的消息，已知发送者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListStuSend]
	-- Add the parameters for the stored procedure here
	(@sender int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     sender = @sender and xg_user_message.type = 1
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListStuReceive]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListStuReceive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	学生消息的列表，已接收的消息，已知接收者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListStuReceive]
	-- Add the parameters for the stored procedure here
	(@receiver int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     receiver = @receiver and xg_user_message.type = 1
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListAllSend]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListAllSend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	全部的消息列表，已发送的消息，已知发送者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListAllSend]
	-- Add the parameters for the stored procedure here
	(@sender int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     sender = @sender 
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListAllRecive]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListAllRecive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	全部的消息列表，已收到的消息，已知接收者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListAllRecive]
	-- Add the parameters for the stored procedure here
	(@receiverID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     receiver = @receiverID 
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListAdmSend]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListAdmSend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	管理员消息的列表，已发送的消息，已知发送者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListAdmSend]
	-- Add the parameters for the stored procedure here
	(@sender int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     sender = @sender and xg_user_message.type = 3
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelMsgListAdmReceive]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelMsgListAdmReceive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-7
-- Description:	管理员消息的列表，已接收的消息，已知接收者ID
-- =============================================
CREATE PROCEDURE  [dbo].[user_message_SelMsgListAdmReceive]
	-- Add the parameters for the stored procedure here
	(@receiver int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY is_read , id desc) AS RowNum, id
			  FROM      xg_user_message
			  WHERE     receiver = @receiver and xg_user_message.type = 3
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelectMessageListBymesID]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelectMessageListBymesID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015/4/7
-- Description:	根据ID查询message的列表，递归查询
-- =============================================
CREATE PROCEDURE [dbo].[user_message_SelectMessageListBymesID]
	-- Add the parameters for the stored procedure here
	(@mesID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	With T
	As(Select id,last_id From xg_user_message Where xg_user_message.id=@mesID
				 Union All
				   Select xg_user_message.id,xg_user_message.last_id From xg_user_message 
					   Inner Join T 
					   on xg_user_message.id=T.last_id )
	Select * From T order by id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_SelectMessageById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_SelectMessageById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2014-12-31
-- Description:	根据ID来查询该条消息
-- =============================================
CREATE PROCEDURE [dbo].[user_message_SelectMessageById]
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[type]
      ,[sender]
      ,[receiver]
      ,[content]
      ,[is_read]
      ,[send_time]
      ,[read_time]
      ,[last_id]
  FROM [XGhmsdb].[dbo].[xg_user_message]
	WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_InsertNewMsg]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_InsertNewMsg]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-8
-- Description:	插入消息，此消息类型为回复别人消息
-- =============================================
CREATE PROCEDURE [dbo].[user_message_InsertNewMsg]
	-- Add the parameters for the stored procedure here
	(@type int,@sender int,@receiver int,@content ntext)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [xg_user_message]
           ([type]
           ,[sender]
           ,[receiver]
           ,[content]
           ,[is_read]
           ,[send_time]
           ,[read_time]
           ,[last_id])
     VALUES
           (@type
           ,@sender
           ,@receiver
           ,@content
           ,0
           ,getdate()
           ,NULL 
           ,NULL)

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[user_message_InsertByOtherID]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_message_InsertByOtherID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-8
-- Description:	插入消息，此消息类型为回复别人消息
-- =============================================
CREATE PROCEDURE [dbo].[user_message_InsertByOtherID]
	-- Add the parameters for the stored procedure here
	(@type int,@sender int,@receiver int,@content ntext,@last_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [xg_user_message]
           ([type]
           ,[sender]
           ,[receiver]
           ,[content]
           ,[is_read]
           ,[send_time]
           ,[read_time]
           ,[last_id])
     VALUES
           (@type
           ,@sender
           ,@receiver
           ,@content
           ,0
           ,getdate()
           ,NULL 
           ,@last_id)

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[term_SelectTermById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[term_SelectTermById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询学期
-- =============================================
CREATE PROCEDURE [dbo].[term_SelectTermById]
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[term_name]
  FROM [XGhmsdb].[dbo].[xg_term]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[role_SelectRoleById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[role_SelectRoleById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询角色
-- =============================================
CREATE PROCEDURE [dbo].[role_SelectRoleById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[role_name]
      ,[role_type]
      ,[is_sys]
  FROM [XGhmsdb].[dbo].[xg_role]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[homework_student_terListByStatus]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[homework_student_terListByStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-6
-- Description:	根据课程ID和作业状态获取分页信息(有作业状态)
-- =============================================
CREATE PROCEDURE [dbo].[homework_student_terListByStatus]
	-- Add the parameters for the stored procedure here
	(@CourseID int,@status int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY xg_homework_student.id desc) AS RowNum, xg_homework_student.id
			  FROM      xg_homework_student,xg_course_homework
			  WHERE     homework_id=xg_course_homework.id and course_id=@CourseID and homework_status=@status
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[homework_student_terListByAllStatus]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[homework_student_terListByAllStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-6
-- Description:	根据课程ID和作业状态获取分页信息
-- =============================================
CREATE PROCEDURE [dbo].[homework_student_terListByAllStatus]
	-- Add the parameters for the stored procedure here
	(@CourseID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY xg_homework_student.id desc) AS RowNum, xg_homework_student.id
			  FROM      xg_homework_student,xg_course_homework
			  WHERE     homework_id=xg_course_homework.id and course_id=@CourseID
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[homework_student_SelectStuHWListBycidandstuidandstatus]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[homework_student_SelectStuHWListBycidandstuidandstatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-29
-- Description:	根据课程ID和学生ID获取作业的列表
-- =============================================
CREATE PROCEDURE [dbo].[homework_student_SelectStuHWListBycidandstuidandstatus] 
	-- Add the parameters for the stored procedure here
	(@stuID int,@courseID int,@status int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY xg_homework_student.id desc) AS RowNum, xg_homework_student.id
			  FROM      xg_homework_student,xg_course_homework
			  WHERE     xg_homework_student.homework_id=xg_course_homework.id and student_id=@stuID and course_id=@courseID and homework_status=@status
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[homework_student_SelectStuHWListBycidandstuid]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[homework_student_SelectStuHWListBycidandstuid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-29
-- Description:	根据课程ID和学生ID获取作业的列表
-- =============================================
CREATE PROCEDURE [dbo].[homework_student_SelectStuHWListBycidandstuid] 
	-- Add the parameters for the stored procedure here
	(@stuID int,@courseID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY xg_homework_student.id desc) AS RowNum, xg_homework_student.id
			  FROM      xg_homework_student,xg_course_homework
			  WHERE     xg_homework_student.homework_id=xg_course_homework.id and student_id=@stuID and course_id=@courseID
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[homework_student_SelectStuHomById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[homework_student_SelectStuHomById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询学生的作业
-- =============================================
CREATE PROCEDURE [dbo].[homework_student_SelectStuHomById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[homework_id]
      ,[student_id]
      ,[submit_time]
      ,[submit_file]
      ,[submit_content]
      ,[homework_score]
      ,[homework_comment]
      ,[homework_status]
  FROM [XGhmsdb].[dbo].[xg_homework_student]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[homework_student_SelectHWByStuIdandPage]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[homework_student_SelectHWByStuIdandPage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2014-4-4
-- Description:	分页查询学生的作业
-- =============================================
CREATE PROCEDURE [dbo].[homework_student_SelectHWByStuIdandPage]
	-- Add the parameters for the stored procedure here
	(@PageBeginNum int,@PageEndNum int,@stuId int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_homework_student
			  WHERE     student_id = @stuId
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[course_student_SelectCurStuById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[course_student_SelectCurStuById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询某门课程对应的学生
-- =============================================
CREATE PROCEDURE [dbo].[course_student_SelectCurStuById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[course_id]
      ,[student_id]
  FROM [XGhmsdb].[dbo].[xg_course_student]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[course_SelectPageOfNum]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[course_SelectPageOfNum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	根据学生的ID分页显示课程
-- =============================================
CREATE PROCEDURE [dbo].[course_SelectPageOfNum]
	-- Add the parameters for the stored procedure here
	(@PageBeginNum int,@PageEndNum int,@stuId int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  course_id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, course_id
			  FROM      xg_course_student
			  WHERE     student_id = @stuId
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[course_SelectCourseById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[course_SelectCourseById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询课程
-- =============================================
CREATE PROCEDURE [dbo].[course_SelectCourseById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[course_number]
      ,[course_name]
      ,[term_id]
      ,[teacher]
      ,[other_teacher]
      ,[student_leader]
      ,[course_info]
      ,[college_id]
  FROM [XGhmsdb].[dbo].[xg_course]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[course_SelectByTremandCollegeandPage]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[course_SelectByTremandCollegeandPage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-4
-- Description:	课程表根据学期和学院的分页的代码
-- =============================================
CREATE PROCEDURE [dbo].[course_SelectByTremandCollegeandPage]
	-- Add the parameters for the stored procedure here
	(@term_id int,@college_id int,@PageBeginNum int,@PageEndNum int)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_course
			  WHERE     term_id = @term_id and college_id=@college_id
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[course_homework_SelectHomeWorkById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[course_homework_SelectHomeWorkById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询课程相对于的作业
-- =============================================
CREATE PROCEDURE [dbo].[course_homework_SelectHomeWorkById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[course_id]
      ,[homework_name]
      ,[homework_info]
      ,[homework_beginTime]
      ,[homework_endTime]
  FROM [XGhmsdb].[dbo].[xg_course_homework]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[course_homework_SelectHomeWorkByCidPage]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[course_homework_SelectHomeWorkByCidPage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-9
-- Description:	课程作业表，根据课程ID来查询作业，用于分页
-- =============================================
CREATE PROCEDURE [dbo].[course_homework_SelectHomeWorkByCidPage]
	-- Add the parameters for the stored procedure here
	(@courId int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_course_homework
			  WHERE     course_id = @courId
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[college_SelectCollegeById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[college_SelectCollegeById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询学院
-- =============================================
CREATE PROCEDURE [dbo].[college_SelectCollegeById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[college_name]
      ,[college_admin]
  FROM [XGhmsdb].[dbo].[xg_college]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[classes_SelectClassById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[classes_SelectClassById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询班级
-- =============================================
CREATE PROCEDURE [dbo].[classes_SelectClassById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[class_name]
      ,[college_id]
      ,[head_teacher]
      ,[class_leader]
      ,[squad_leader]
      ,[class_group_secretary]
      ,[study_secretary]
      ,[life_secretary]
  FROM [XGhmsdb].[dbo].[xg_classes]
	WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectUserById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectUserById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询用户
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectUserById] 
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[role_id]
      ,[role_type]
      ,[user_number]
      ,[user_name]
      ,[password]
      ,[is_lock]
      ,[add_time]
  FROM [XGhmsdb].[dbo].[xg_users]
  WHERE id=@id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectIDByTerForColidIsNull]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectIDByTerForColidIsNull]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	根据老师角色的用户，当学院ID为空时
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectIDByTerForColidIsNull]
	-- Add the parameters for the stored procedure here
	(@roleID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_users_info
			  WHERE     role_id = @roleID and college_id is NULL
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectIDByTerForColid]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectIDByTerForColid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	根据老师角色的用户，根据学院ID来查询
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectIDByTerForColid]
	-- Add the parameters for the stored procedure here
	(@roleID int,@collegeID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_users_info
			  WHERE     role_id = @roleID and college_id = @collegeID
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectIDByStuForColidIsNull]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectIDByStuForColidIsNull]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	根据学生角色的用户，当学院ID为空时的用户列表
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectIDByStuForColidIsNull]
	-- Add the parameters for the stored procedure here
	(@roleID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_users_info
			  WHERE     role_id = @roleID and college_id is NULL
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectIDByStuForColidAndClsIsBull]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectIDByStuForColidAndClsIsBull]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	根据学生角色的用户，根据学院ID，当班级ID为空时的用户列表
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectIDByStuForColidAndClsIsBull]
	-- Add the parameters for the stored procedure here
	(@roleID int,@collegeID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_users_info
			  WHERE     role_id = @roleID and college_id = @collegeID and class_id is NULL
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectIDByStuForColidAndClsid]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectIDByStuForColidAndClsid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	根据学生角色的用户，根据学院ID和班级ID
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectIDByStuForColidAndClsid]
	-- Add the parameters for the stored procedure here
	(@roleID int,@collegeID int,@classID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_users_info
			  WHERE     role_id = @roleID and college_id = @collegeID and class_id = @classID
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_SelectIDByAdm]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_SelectIDByAdm]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-4-5
-- Description:	查询所有的管理员的角色，并分页显示
-- =============================================
CREATE PROCEDURE [dbo].[users_SelectIDByAdm]
	-- Add the parameters for the stored procedure here
	(@roleID int,@PageBeginNum int,@PageEndNum int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  id
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY id desc) AS RowNum, id
			  FROM      xg_users_info
			  WHERE     role_id = @roleID
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageBeginNum
		AND RowNum <= @PageEndNum
	ORDER BY RowNum
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_info_SelectUserInfoById]    Script Date: 05/07/2015 22:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_info_SelectUserInfoById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		陈旭东
-- Create date: 2015-3-15
-- Description:	根据ID来查询用户信息
-- =============================================
CREATE PROCEDURE [dbo].[users_info_SelectUserInfoById]  
	-- Add the parameters for the stored procedure here
	(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[user_id]
      ,[role_id]
      ,[real_name]
      ,[sex]
      ,[birthday]
      ,[telephone]
      ,[email]
      ,[college_id]
      ,[class_id]
      ,[major]
      ,[address]
      ,[explain]
  FROM [XGhmsdb].[dbo].[xg_users_info]
  WHERE id=@id
END
' 
END
GO
/****** Object:  Default [DF_xg_homework_student_homework_status]    Script Date: 05/07/2015 22:54:16 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_xg_homework_student_homework_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[xg_homework_student]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_xg_homework_student_homework_status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[xg_homework_student] ADD  CONSTRAINT [DF_xg_homework_student_homework_status]  DEFAULT ((0)) FOR [homework_status]
END


End
GO
/****** Object:  Default [DF_xg_user_message_is_read]    Script Date: 05/07/2015 22:54:16 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_xg_user_message_is_read]') AND parent_object_id = OBJECT_ID(N'[dbo].[xg_user_message]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_xg_user_message_is_read]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[xg_user_message] ADD  CONSTRAINT [DF_xg_user_message_is_read]  DEFAULT ((0)) FOR [is_read]
END


End
GO
/****** Object:  ForeignKey [FK_xg_users_info_xg_users]    Script Date: 05/07/2015 22:54:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_xg_users_info_xg_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[xg_users_info]'))
ALTER TABLE [dbo].[xg_users_info]  WITH CHECK ADD  CONSTRAINT [FK_xg_users_info_xg_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[xg_users] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_xg_users_info_xg_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[xg_users_info]'))
ALTER TABLE [dbo].[xg_users_info] CHECK CONSTRAINT [FK_xg_users_info_xg_users]
GO
