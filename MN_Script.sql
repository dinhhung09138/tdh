

CREATE TABLE [dbo].[MN_ACCOUNT_TYPE](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_ACCOUNT_TYPE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_TYPE] ADD  CONSTRAINT [DF_MN_ACCOUNT_TYPE_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_TYPE] ADD  CONSTRAINT [DF_MN_ACCOUNT_TYPE_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_TYPE] ADD  CONSTRAINT [DF_MN_ACCOUNT_TYPE_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_TYPE] ADD  CONSTRAINT [DF_MN_ACCOUNT_TYPE_deleted]  DEFAULT ((0)) FOR [deleted]
GO

-- /////////////// [MN_ACCOUNT]

CREATE TABLE [dbo].[MN_ACCOUNT](
	[id] [uniqueidentifier] NOT NULL,
	[account_type_id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[start] [numeric](18, 1) NOT NULL,
	[input] [numeric](18, 1) NOT NULL,
	[output] [numeric](18, 1) NOT NULL,
	[end] [numeric](18, 1) NOT NULL,
	[ordering] [numeric] NOT NULL,
	[publish] [bit] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_start]  DEFAULT ((0)) FOR [start]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_input]  DEFAULT ((0)) FOR [input]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_output]  DEFAULT ((0)) FOR [output]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_end]  DEFAULT ((0)) FOR [end]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_ACCOUNT] ADD  CONSTRAINT [DF_MN_ACCOUNT_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[MN_ACCOUNT]  WITH CHECK ADD  CONSTRAINT [FK_MN_ACCOUNT_MN_ACCOUNT_TYPE] FOREIGN KEY([account_type_id])
REFERENCES [dbo].[MN_ACCOUNT_TYPE] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[MN_ACCOUNT] CHECK CONSTRAINT [FK_MN_ACCOUNT_MN_ACCOUNT_TYPE]
GO


SET QUOTED_IDENTIFIER ON
GO

-- /////////////// [MN_ACCOUNT_SETTING]

CREATE TABLE [dbo].[MN_ACCOUNT_SETTING](
	[id] [uniqueidentifier] NOT NULL,
	[yearmonth] [numeric](6, 0) NOT NULL,
	[account_id] [uniqueidentifier] NOT NULL,
	[start] [numeric](18, 1) NOT NULL,
	[input] [numeric](18, 1) NOT NULL,
	[output] [numeric](18, 1) NOT NULL,
	[end] [numeric](18, 1) NOT NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_ACCOUNT_SETTING] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_start]  DEFAULT ((0)) FOR [start]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_input]  DEFAULT ((0)) FOR [input]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_output]  DEFAULT ((0)) FOR [output]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_end]  DEFAULT ((0)) FOR [end]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] ADD  CONSTRAINT [DF_MN_ACCOUNT_SETTING_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING]  WITH CHECK ADD  CONSTRAINT [FK_MN_ACCOUNT_SETTING_MN_ACCOUNT] FOREIGN KEY([account_id])
REFERENCES [dbo].[MN_ACCOUNT] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MN_ACCOUNT_SETTING] CHECK CONSTRAINT [FK_MN_ACCOUNT_SETTING_MN_ACCOUNT]
GO


-- /////////////// [MN_GROUP]


CREATE TABLE [dbo].[MN_GROUP](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NOT NULL,
	[is_input] [bit] NOT NULL,
	[percent_setting] [tinyint] NOT NULL,
	[percent_current] [tinyint] NOT NULL,
	[money_setting] [numeric](38, 1) NOT NULL,
	[money_current] [numeric](38, 1) NOT NULL,
	[startmonth] [numeric](6, 0) NOT NULL,
	[endmonth] [numeric](6, 0) NOT NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_GROUP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_is_input]  DEFAULT ((0)) FOR [is_input]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_percent_setting]  DEFAULT ((0)) FOR [percent_setting]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_percent_current]  DEFAULT ((0)) FOR [percent_current]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_money_setting]  DEFAULT ((0)) FOR [money_setting]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_money_current]  DEFAULT ((0)) FOR [money_current]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_GROUP] ADD  CONSTRAINT [DF_MN_GROUP_deleted]  DEFAULT ((0)) FOR [deleted]
GO


-- /////////////// [MN_GROUP_SETTING]

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MN_GROUP_SETTING](
	[id] [uniqueidentifier] NOT NULL,
	[group_id] [uniqueidentifier] NOT NULL,
	[year_month] [numeric](6, 0) NOT NULL,
	[percent_setting] [tinyint] NOT NULL,
	[percent_current] [tinyint] NOT NULL,
	[money_setting] [numeric](38, 1) NOT NULL,
	[money_current] [numeric](38, 1) NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_GROUP_SETTING] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] ADD  CONSTRAINT [DF_MN_GROUP_SETTING_percent_setting]  DEFAULT ((0)) FOR [percent_setting]
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] ADD  CONSTRAINT [DF_MN_GROUP_SETTING_percent_current]  DEFAULT ((0)) FOR [percent_current]
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] ADD  CONSTRAINT [DF_MN_GROUP_SETTING_money_setting]  DEFAULT ((0)) FOR [money_setting]
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] ADD  CONSTRAINT [DF_MN_GROUP_SETTING_money_current]  DEFAULT ((0)) FOR [money_current]
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] ADD  CONSTRAINT [DF_MN_GROUP_SETTING_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] ADD  CONSTRAINT [DF_MN_GROUP_SETTING_deleted]  DEFAULT ((0)) FOR [deleted]
GO




ALTER TABLE [dbo].[MN_GROUP_SETTING]  WITH CHECK ADD  CONSTRAINT [FK_MN_GROUP_SETTING_MN_GROUP] FOREIGN KEY([group_id])
REFERENCES [dbo].[MN_GROUP] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MN_GROUP_SETTING] CHECK CONSTRAINT [FK_MN_GROUP_SETTING_MN_GROUP]
GO


-- /////////////// [MN_CATEGORY]

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MN_CATEGORY](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NOT NULL,
	[group_id] [uniqueidentifier] NOT NULL,
	[percent_setting] [tinyint] NOT NULL,
	[percent_current] [tinyint] NOT NULL,
	[money_setting] [numeric](38, 1) NOT NULL,
	[money_current] [numeric](38, 1) NOT NULL,
	[startmonth] [numeric](6, 0) NOT NULL,
	[endmonth] [numeric](6, 0) NOT NULL,
	[ordering] [smallint] NOT NULL,
	[publish] [bit] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_percent_setting]  DEFAULT ((0)) FOR [percent_setting]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_percent_current]  DEFAULT ((0)) FOR [percent_current]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_money_setting]  DEFAULT ((0)) FOR [money_setting]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_money_current]  DEFAULT ((0)) FOR [money_current]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_CATEGORY] ADD  CONSTRAINT [DF_MN_CATEGORY_deleted]  DEFAULT ((0)) FOR [deleted]
GO


ALTER TABLE [dbo].[MN_CATEGORY]  WITH CHECK ADD  CONSTRAINT [FK_MN_CATEGORY_MN_GROUP] FOREIGN KEY([group_id])
REFERENCES [dbo].[MN_GROUP] ([id])
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

ALTER TABLE [dbo].[MN_CATEGORY] CHECK CONSTRAINT [FK_MN_CATEGORY_MN_GROUP]
GO



-- /////////////// [MN_CATEGORY_SETTING]

CREATE TABLE [dbo].[MN_CATEGORY_SETTING](
	[id] [uniqueidentifier] NOT NULL,
	[category_id] [uniqueidentifier] NOT NULL,
	[year_month] [numeric](6, 0) NOT NULL,
	[percent_setting] [tinyint] NOT NULL,
	[percent_current] [tinyint] NOT NULL,
	[money_setting] [numeric](38, 1) NOT NULL,
	[money_current] [numeric](38, 1) NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_CATEGORY_SETTING] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] ADD  CONSTRAINT [DF_MN_CATEGORY_SETTING_percent_setting]  DEFAULT ((0)) FOR [percent_setting]
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] ADD  CONSTRAINT [DF_MN_CATEGORY_SETTING_percent_current]  DEFAULT ((0)) FOR [percent_current]
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] ADD  CONSTRAINT [DF_MN_CATEGORY_SETTING_money_setting]  DEFAULT ((0)) FOR [money_setting]
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] ADD  CONSTRAINT [DF_MN_CATEGORY_SETTING_money_current]  DEFAULT ((0)) FOR [money_current]
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] ADD  CONSTRAINT [DF_MN_CATEGORY_SETTING_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] ADD  CONSTRAINT [DF_MN_CATEGORY_SETTING_deleted]  DEFAULT ((0)) FOR [deleted]
GO




ALTER TABLE [dbo].[MN_CATEGORY_SETTING]  WITH CHECK ADD  CONSTRAINT [FK_MN_CATEGORY_SETTING_MN_CATEGORY] FOREIGN KEY([category_id])
REFERENCES [dbo].[MN_CATEGORY] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MN_CATEGORY_SETTING] CHECK CONSTRAINT [FK_MN_CATEGORY_SETTING_MN_CATEGORY]
GO



-- /////////////// [MN_PAYMENT]

CREATE TABLE [dbo].[MN_PAYMENT](
	[id] [uniqueidentifier] NOT NULL,
	[account_id] [uniqueidentifier] NOT NULL,
	[category_id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[date] [timestamp] NOT NULL,
	[purpose] [nvarchar](255) NULL,
	[notes] [nvarchar](255) NULL,
	[money] [numeric](18, 1) NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_PAYMENT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_PAYMENT] ADD  CONSTRAINT [DF_MN_PAYMENT_money]  DEFAULT ((0)) FOR [money]
GO

ALTER TABLE [dbo].[MN_PAYMENT] ADD  CONSTRAINT [DF_MN_PAYMENT_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_PAYMENT] ADD  CONSTRAINT [DF_MN_PAYMENT_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[MN_PAYMENT]  WITH CHECK ADD  CONSTRAINT [FK_MN_PAYMENT_MN_ACCOUNT] FOREIGN KEY([account_id])
REFERENCES [dbo].[MN_ACCOUNT] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[MN_PAYMENT] CHECK CONSTRAINT [FK_MN_PAYMENT_MN_ACCOUNT]
GO

ALTER TABLE [dbo].[MN_PAYMENT]  WITH CHECK ADD  CONSTRAINT [FK_MN_PAYMENT_MN_CATEGORY] FOREIGN KEY([category_id])
REFERENCES [dbo].[MN_CATEGORY] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[MN_PAYMENT] CHECK CONSTRAINT [FK_MN_PAYMENT_MN_CATEGORY]
GO


-- /////////////// [MN_INCOME]

CREATE TABLE [dbo].[MN_INCOME](
	[id] [uniqueidentifier] NOT NULL,
	[account_id] [uniqueidentifier] NOT NULL,
	[category_id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[date] [timestamp] NOT NULL,
	[purpose] [nvarchar](255) NULL,
	[notes] [nvarchar](255) NULL,
	[money] [numeric](18, 1) NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_INCOME] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_INCOME] ADD  CONSTRAINT [DF_MN_INCOME_money]  DEFAULT ((0)) FOR [money]
GO

ALTER TABLE [dbo].[MN_INCOME] ADD  CONSTRAINT [DF_MN_INCOME_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_INCOME] ADD  CONSTRAINT [DF_MN_INCOME_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[MN_INCOME]  WITH CHECK ADD  CONSTRAINT [FK_MN_INCOME_MN_ACCOUNT] FOREIGN KEY([account_id])
REFERENCES [dbo].[MN_ACCOUNT] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[MN_INCOME] CHECK CONSTRAINT [FK_MN_INCOME_MN_ACCOUNT]
GO

ALTER TABLE [dbo].[MN_INCOME]  WITH CHECK ADD  CONSTRAINT [FK_MN_INCOME_MN_CATEGORY] FOREIGN KEY([category_id])
REFERENCES [dbo].[MN_CATEGORY] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[MN_INCOME] CHECK CONSTRAINT [FK_MN_INCOME_MN_CATEGORY]
GO

-- /////////////// [MN_TRANSFER]

CREATE TABLE [dbo].[MN_TRANSFER](
	[id] [uniqueidentifier] NOT NULL,
	[account_from] [uniqueidentifier] NOT NULL,
	[account_to] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[date] [timestamp] NOT NULL,
	[purpose] [nvarchar](255) NULL,
	[notes] [nvarchar](255) NULL,
	[money] [numeric](18, 1) NOT NULL,
	[fee] [numeric](18, 1) NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_MN_TRANSFER] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MN_TRANSFER] ADD  CONSTRAINT [DF_MN_TRANSFER_money]  DEFAULT ((0)) FOR [money]
GO

ALTER TABLE [dbo].[MN_TRANSFER] ADD  CONSTRAINT [DF_MN_TRANSFER_fee]  DEFAULT ((0)) FOR [fee]
GO

ALTER TABLE [dbo].[MN_TRANSFER] ADD  CONSTRAINT [DF_MN_TRANSFER_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

ALTER TABLE [dbo].[MN_TRANSFER] ADD  CONSTRAINT [DF_MN_TRANSFER_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[MN_TRANSFER]  WITH CHECK ADD  CONSTRAINT [FK_MN_TRANSFER_MN_ACCOUNT_FROM] FOREIGN KEY([account_from])
REFERENCES [dbo].[MN_ACCOUNT] ([id])
GO

ALTER TABLE [dbo].[MN_TRANSFER] CHECK CONSTRAINT [FK_MN_TRANSFER_MN_ACCOUNT_FROM]
GO

ALTER TABLE [dbo].[MN_TRANSFER]  WITH CHECK ADD  CONSTRAINT [FK_MN_TRANSFER_MN_ACCOUNT_TO] FOREIGN KEY([account_to])
REFERENCES [dbo].[MN_ACCOUNT] ([id])
GO

ALTER TABLE [dbo].[MN_TRANSFER] CHECK CONSTRAINT [FK_MN_TRANSFER_MN_ACCOUNT_TO]
GO

-- ///////////////


-- ///////////////



