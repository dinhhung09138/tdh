
CREATE TABLE [dbo].[SYS_MODULE](
	[code] [varchar](50) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[default_action] [varchar](60) NULL,
	[icon] [nvarchar](20) NULL,
	[ordering] [smallint] NOT NULL,
 CONSTRAINT [PK_SYS_MODULE] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SYS_MODULE] ADD  CONSTRAINT [DF_SYS_MODULE_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[SYS_FUNCTION]
ADD [module_code] [varchar](50) NOT NULL
CONSTRAINT [DK_SYS_FUNCTION_module_code] DEFAULT ('none')
WITH VALUES

ALTER TABLE [dbo].[SYS_FUNCTION]  WITH CHECK ADD  CONSTRAINT [FK_SYS_FUNCTION_SYS_MODULE] FOREIGN KEY([module_code])
REFERENCES [dbo].[SYS_MODULE] ([code])
GO

ALTER TABLE [dbo].[SYS_FUNCTION] CHECK CONSTRAINT [FK_SYS_FUNCTION_SYS_MODULE]
GO


-- Them data bang module

INSERT [dbo].[SYS_MODULE] ([code], [title], [default_action], [icon], [ordering]) VALUES (N'money', N'TÀI CHÍNH CÁ NHÂN', N'/administrator/admmoney/index', N'fa-money', 20)
GO
INSERT [dbo].[SYS_MODULE] ([code], [title], [default_action], [icon], [ordering]) VALUES (N'none', N'None', N'none', N'none', 21)
GO
INSERT [dbo].[SYS_MODULE] ([code], [title], [default_action], [icon], [ordering]) VALUES (N'system', N'HỆ THỐNG', N'/administrator/admsystem/index', N'fa-cogs', 19)
GO

-- cap nhat cot module code, trong bang function
-- luu ý dò đúng dòng để cho nó chạy chính xác function

INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'money_account', N'Thu chi - Tài khoản', N'administrator', N'admmoney', N'account', 8, N'Tài khoản', N'money')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'money_account_type', N'Thu chi - Loại tài khoản', N'administrator', N'admmoney', N'accounttype', 9, N'Loại tài khoản', N'money')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'money_category', N'Thu chi - Danh mục thu chi', N'administrator', N'admmoney', N'category', 6, N'Danh mục thu chi', N'money')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'money_flow', N'Thu chi - Dòng tiền', N'administrator', N'admmoney', N'flowhistory', 5, N'Thu chi', N'money')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'money_group', N'Thu chi - Quy tắc chi tiêu', N'administrator', N'admmoney', N'group', 7, N'Quy tắc chi tiêu', N'money')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'money_report', N'Thu chi - Báo cáo', N'administrator', N'admmoney', N'report', 10, N'Báo cáo', N'money')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'post_about', N'Giới thiệu', N'administrator', N'admpost', N'about', 6, N'Giới thiệu', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'post_category', N'Nhóm chủ đề', N'administrator', N'admpost', N'category', 8, N'Nhóm chủ đề', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'post_navigation', N'Danh mục', N'administrator', N'admpost', N'navigation', 9, N'Danh mục', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'post_news', N'Bài viết', N'administrator', N'admpost', N'news', 7, N'Bài viết', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'setting_category', N'Chủ đề hiển thị', N'administrator', N'admsetting', N'category', 4, N'Chủ đề hiển thị', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'setting_configuration', N'Thiết lập chung', N'administrator', N'admsetting', N'configuration', 3, N'Thiết lập chung', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'setting_navigation', N'Danh  mục hiển thị', N'administrator', N'admsetting', N'navigation', 5, N'Danh mục hiển thị', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'system_error_log', N'Lỗi hệ thống', N'administrator', N'admsystem', N'errorlog', 1, N'Lỗi hệ thống', N'system')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'system_role', N'Nhóm quyền', N'administrator', N'admsystem', N'role', 3, N'Nhóm quyền', N'system')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'system_user', N'Tài khoản', N'administrator', N'admsystem', N'user', 2, N'Tài khoản', N'system')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'target_dailytask', N'Mục tiêu - Kế hoạch hàng ngày', N'administrator', N'admtarget', N'daily', 2, N'Kế hoạch hàng ngày', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'target_dashboard', N'Mục tiêu - Dashboard', N'administrator', N'admtarget', N'dashboard', 3, N'Dashboard', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'target_idea', N'Ý tưởng', N'administrator', N'admtarget', N'idea', 5, N'Ý tưởng', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'target_overview', N'Mục tiêu - Tổng quan', N'administrator', N'admtarget', N'overview', 4, N'Tổng quan', N'none')
GO
INSERT [dbo].[SYS_FUNCTION] ([code], [name], [area], [controller], [action], [ordering], [title], [module_code]) VALUES (N'working_report', N'Công việc - Báo cáo', N'administrator', N'admworking', N'report', 1, N'Báo cáo hàng ngày', N'none')
GO
