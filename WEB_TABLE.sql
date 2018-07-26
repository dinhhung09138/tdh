
CREATE TABLE [dbo].[WEB_ABOUT](
	[id] [uniqueidentifier] NOT NULL,
	[content] [nvarchar](max) NULL,
	[link] [nvarchar](200) NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
	[image] [nvarchar](200) NULL,
	[meta_title] [nvarchar](120) NOT NULL,
	[meta_description] [nvarchar](200) NOT NULL,
	[meta_keywords] [nvarchar](255) NULL,
	[meta_next] [nvarchar](170) NULL,
	[meta_og_site_name] [nvarchar](100) NULL,
	[meta_og_image] [nvarchar](200) NULL,
	[meta_twitter_image] [nvarchar](200) NULL,
	[meta_article_name] [nvarchar](200) NULL,
	[meta_article_tag] [nvarchar](255) NULL,
	[meta_article_section] [nvarchar](200) NULL,
	[meta_article_publish] [datetime] NOT NULL,
 CONSTRAINT [PK_WEB_ABOUT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[WEB_NAVIGATION](
	[id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](70) NOT NULL,
	[alias] [varchar](100) NOT NULL,
	[description] [nvarchar](250) NULL,
	[image] [nvarchar](200) NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[meta_title] [nvarchar](120) NOT NULL,
	[meta_description] [nvarchar](200) NOT NULL,
	[meta_keywords] [nvarchar](255) NULL,
	[meta_next] [nvarchar](170) NULL,
	[meta_og_site_name] [nvarchar](100) NULL,
	[meta_og_image] [nvarchar](200) NULL,
	[meta_twitter_image] [nvarchar](200) NULL,
	[meta_article_name] [nvarchar](200) NULL,
	[meta_article_tag] [nvarchar](255) NULL,
	[meta_article_section] [nvarchar](200) NULL,
	[meta_article_publish] [datetime] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
	[no_child] [bit] NOT NULL,
 CONSTRAINT [PK_WEB_NAVIGATION] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WEB_NAVIGATION] ADD  CONSTRAINT [DF_WEB_NAVIGATION_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[WEB_NAVIGATION] ADD  CONSTRAINT [DF_WEB_NAVIGATION_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[WEB_NAVIGATION] ADD  CONSTRAINT [DF_WEB_NAVIGATION_meta_article_publish]  DEFAULT (getdate()) FOR [meta_article_publish]
GO

ALTER TABLE [dbo].[WEB_NAVIGATION] ADD  CONSTRAINT [DF_WEB_NAVIGATION_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[WEB_NAVIGATION] ADD  CONSTRAINT [DF_WEB_NAVIGATION_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[WEB_NAVIGATION] ADD  CONSTRAINT [FK_WEB_NAVIGATION_no_child]  DEFAULT ((0)) FOR [no_child]
GO


CREATE TABLE [dbo].[WEB_CATEGORY](
	[id] [uniqueidentifier] NOT NULL,
	[navigation_id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](70) NOT NULL,
	[alias] [varchar](100) NOT NULL,
	[description] [nvarchar](250) NULL,
	[show_on_nav] [bit] NOT NULL,
	[image] [nvarchar](200) NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[meta_title] [nvarchar](120) NOT NULL,
	[meta_description] [nvarchar](200) NOT NULL,
	[meta_keywords] [nvarchar](255) NULL,
	[meta_next] [nvarchar](170) NULL,
	[meta_og_site_name] [nvarchar](100) NULL,
	[meta_og_image] [nvarchar](200) NULL,
	[meta_twitter_image] [nvarchar](200) NULL,
	[meta_article_name] [nvarchar](200) NULL,
	[meta_article_tag] [nvarchar](255) NULL,
	[meta_article_section] [nvarchar](200) NULL,
	[meta_article_publish] [datetime] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_WEB_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WEB_CATEGORY] ADD  CONSTRAINT [DF_WEB_CATEGORY_show_on_nav]  DEFAULT ((0)) FOR [show_on_nav]
GO

ALTER TABLE [dbo].[WEB_CATEGORY] ADD  CONSTRAINT [DF_WEB_CATEGORY_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[WEB_CATEGORY] ADD  CONSTRAINT [DF_WEB_CATEGORY_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[WEB_CATEGORY] ADD  CONSTRAINT [DF_WEB_CATEGORY_meta_article_publish]  DEFAULT (getdate()) FOR [meta_article_publish]
GO

ALTER TABLE [dbo].[WEB_CATEGORY] ADD  CONSTRAINT [DF_WEB_CATEGORY_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[WEB_CATEGORY] ADD  CONSTRAINT [DF_WEB_CATEGORY_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[WEB_CATEGORY]  WITH CHECK ADD  CONSTRAINT [FK_WEB_CATEGORY_WEB_NAVIGATION] FOREIGN KEY([navigation_id])
REFERENCES [dbo].[WEB_NAVIGATION] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WEB_CATEGORY] CHECK CONSTRAINT [FK_WEB_CATEGORY_WEB_NAVIGATION]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cho phép danh mục hiển thị ở bên trong navigation ở trang chủ hay không' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WEB_CATEGORY', @level2type=N'COLUMN',@level2name=N'show_on_nav'
GO


CREATE TABLE [dbo].[WEB_POST](
	[id] [uniqueidentifier] NOT NULL,
	[category_id] [uniqueidentifier] NULL,
	[navigation_id] [uniqueidentifier] NULL,
	[is_navigation] [bit] NOT NULL,
	[title] [nvarchar](150) NOT NULL,
	[alias] [varchar](150) NOT NULL,
	[description] [nvarchar](250) NULL,
	[content] [nvarchar](max) NULL,
	[image] [nvarchar](200) NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[meta_title] [nvarchar](120) NOT NULL,
	[meta_description] [nvarchar](200) NOT NULL,
	[meta_keywords] [nvarchar](255) NULL,
	[meta_next] [nvarchar](170) NULL,
	[meta_og_site_name] [nvarchar](100) NULL,
	[meta_og_image] [nvarchar](200) NULL,
	[meta_twitter_image] [nvarchar](200) NULL,
	[meta_article_name] [nvarchar](200) NULL,
	[meta_article_tag] [nvarchar](255) NULL,
	[meta_article_section] [nvarchar](200) NULL,
	[meta_article_publish] [datetime] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
	[view] [int] NOT NULL,
	[comment] [int] NOT NULL,
 CONSTRAINT [PK_WEB_POST] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_is_navigation]  DEFAULT ((0)) FOR [is_navigation]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_meta_article_publish]  DEFAULT (getdate()) FOR [meta_article_publish]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_view]  DEFAULT ((0)) FOR [view]
GO

ALTER TABLE [dbo].[WEB_POST] ADD  CONSTRAINT [DF_WEB_POST_comment]  DEFAULT ((0)) FOR [comment]
GO

ALTER TABLE [dbo].[WEB_POST]  WITH CHECK ADD  CONSTRAINT [FK_WEB_POST_WEB_CATEGORY] FOREIGN KEY([category_id])
REFERENCES [dbo].[WEB_CATEGORY] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WEB_POST] CHECK CONSTRAINT [FK_WEB_POST_WEB_CATEGORY]
GO

ALTER TABLE [dbo].[WEB_POST]  WITH CHECK ADD  CONSTRAINT [FK_WEB_POST_WEB_NAVIGATION] FOREIGN KEY([navigation_id])
REFERENCES [dbo].[WEB_NAVIGATION] ([id])
GO

ALTER TABLE [dbo].[WEB_POST] CHECK CONSTRAINT [FK_WEB_POST_WEB_NAVIGATION]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRUE: Bài viết liên kết trực tiếp đến danh mục, FALSE: Bài viết được liên kết đến chủ đề' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WEB_POST', @level2type=N'COLUMN',@level2name=N'is_navigation'
GO


CREATE TABLE [dbo].[WEB_CONFIGURATION](
	[key] [varchar](20) NOT NULL,
	[description] [nvarchar](500) NULL,
	[value] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_WEB_CONFIGURATION] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WEB_ERROR_LOG](
	[id] [uniqueidentifier] NOT NULL,
	[file_name] [varchar](40) NULL,
	[method_name] [varchar](40) NULL,
	[source] [varchar](300) NULL,
	[stack_trace] [varchar](300) NULL,
	[inner_exception] [nvarchar](max) NULL,
	[message] [nvarchar](max) NULL,
	[date] [date] NOT NULL,
	[create_by] [uniqueidentifier] NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_WEB_ERROR_LOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[WEB_ERROR_LOG] ADD  CONSTRAINT [DF_WEB_ERROR_LOG_date]  DEFAULT (getdate()) FOR [date]
GO

ALTER TABLE [dbo].[WEB_ERROR_LOG] ADD  CONSTRAINT [DF_WEB_ERROR_LOG_create_date]  DEFAULT (getdate()) FOR [create_date]
GO



insert into  [dbo].[WEB_ABOUT]
select * from  [dbo].[ABOUT];

INSERT INTO [dbo].[WEB_NAVIGATION] 
SELECT * FROM [dbo].[NAVIGATION];

INSERT INTO [dbo].[WEB_CATEGORY]
SELECT * FROM [dbo].[CATEGORY];

INSERT INTO [dbo].[WEB_POST]
SELECT * FROM [dbo].[POST];

INSERT INTO [dbo].[WEB_CONFIGURATION]
SELECT * FROM [dbo].[CONFIGURATION];
-- 
DROP TABLE [dbo].[POST];
DROP TABLE [dbo].[CATEGORY];
DROP TABLE [dbo].[NAVIGATION]; 
DROP TABLE [dbo].[ABOUT]; 
DROP TABLE [dbo].[CONFIGURATION];
DROP TABLE [dbo].[ERROR_LOG]