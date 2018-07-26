-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************


--************************************** [dbo].[PN_TASK_TYPE]

CREATE TABLE [dbo].[PN_TASK_TYPE]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(50) NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,

 CONSTRAINT [PK_PN_TASK_TYPE] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TASK_TYPE] ADD  CONSTRAINT [DF_PN_TASK_TYPE_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TASK_TYPE] ADD  CONSTRAINT [DF_PN_TASK_TYPE_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TASK_TYPE] ADD  CONSTRAINT [DF_PN_TASK_TYPE_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TASK_TYPE] ADD  CONSTRAINT [DF_PN_TASK_TYPE_deleted]  DEFAULT ((0)) FOR [deleted]
GO



--************************************** [dbo].[PN_REPORT_KIND]

CREATE TABLE [dbo].[PN_REPORT_KIND]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(30) NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,

 CONSTRAINT [PK_PN_REPORT_KIND] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_REPORT_KIND] ADD  CONSTRAINT [DF_PN_REPORT_KIND_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_REPORT_KIND] ADD  CONSTRAINT [DF_PN_REPORT_KIND_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_REPORT_KIND] ADD  CONSTRAINT [DF_PN_REPORT_KIND_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_REPORT_KIND] ADD  CONSTRAINT [DF_PN_REPORT_KIND_deleted]  DEFAULT ((0)) FOR [deleted]
GO


--************************************** [dbo].[PN_IDEA]

CREATE TABLE [dbo].[PN_IDEA]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[title]				NVARCHAR(200) NOT NULL ,
	[content]			NVARCHAR(MAX) NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,

 CONSTRAINT [PK_PN_IDEA] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_IDEA] ADD  CONSTRAINT [DF_PN_IDEA_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_IDEA] ADD  CONSTRAINT [DF_PN_IDEA_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_IDEA] ADD  CONSTRAINT [DF_PN_IDEA_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_IDEA] ADD  CONSTRAINT [DF_PN_IDEA_deleted]  DEFAULT ((0)) FOR [deleted]
GO





--************************************** [dbo].[PN_TASK_STATUS]

CREATE TABLE [dbo].[PN_TASK_STATUS]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(50) NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,

 CONSTRAINT [PK_PN_TASK_STATUS] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TASK_STATUS] ADD  CONSTRAINT [DF_PN_TASK_STATUS_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TASK_STATUS] ADD  CONSTRAINT [DF_PN_TASK_STATUS_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TASK_STATUS] ADD  CONSTRAINT [DF_PN_TASK_STATUS_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TASK_STATUS] ADD  CONSTRAINT [DF_PN_TASK_STATUS_deleted]  DEFAULT ((0)) FOR [deleted]
GO



--************************************** [dbo].[PN_TARGET_PRIORITY]

CREATE TABLE [dbo].[PN_TARGET_PRIORITY]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(100) NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted] [bit]		NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,

 CONSTRAINT [PK_PN_TARGET_PRIORITY] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET_PRIORITY] ADD  CONSTRAINT [DF_PN_TARGET_PRIORITY_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET_PRIORITY] ADD  CONSTRAINT [DF_PN_TARGET_PRIORITY_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET_PRIORITY] ADD  CONSTRAINT [DF_PN_TARGET_PRIORITY_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET_PRIORITY] ADD  CONSTRAINT [DF_PN_TARGET_PRIORITY_deleted]  DEFAULT ((0)) FOR [deleted]
GO



--************************************** [dbo].[PN_TARGET_TYPE]

CREATE TABLE [dbo].[PN_TARGET_TYPE]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(100) NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,

 CONSTRAINT [PK_PN_TARGET_TYPE] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET_TYPE] ADD  CONSTRAINT [DF_PN_TARGET_TYPE_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET_TYPE] ADD  CONSTRAINT [DF_PN_TARGET_TYPE_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET_TYPE] ADD  CONSTRAINT [DF_PN_TARGET_TYPE_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET_TYPE] ADD  CONSTRAINT [DF_PN_TARGET_TYPE_deleted]  DEFAULT ((0)) FOR [deleted]
GO


--************************************** [dbo].[PN_REPORT]

CREATE TABLE [dbo].[PN_REPORT]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[date]				DATETIME NOT NULL ,
	[title]				NVARCHAR(200) NOT NULL ,
	[content]			NVARCHAR(MAX) NULL ,
	[kind_id]			UNIQUEIDENTIFIER NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_REPORT] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_REPORT] ADD  CONSTRAINT [DF_PN_REPORT_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_REPORT] ADD  CONSTRAINT [DF_PN_REPORT_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_REPORT] ADD  CONSTRAINT [DF_PN_REPORT_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_REPORT] ADD  CONSTRAINT [DF_PN_REPORT_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_REPORT]  WITH CHECK ADD CONSTRAINT [FK_PN_REPORT_PN_REPORT_KIND] FOREIGN KEY ([kind_id]) 
REFERENCES [dbo].[PN_REPORT_KIND]([id])

ALTER TABLE [dbo].[PN_REPORT] CHECK CONSTRAINT [FK_PN_REPORT_PN_REPORT_KIND]
GO

--SKIP Index: [fkIdx_244]


--************************************** [dbo].[PN_TARGET]

CREATE TABLE [dbo].[PN_TARGET]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(200) NOT NULL ,
	[content]			NVARCHAR(250) NULL ,
	[target_type_id]	UNIQUEIDENTIFIER NOT NULL ,
	[idea_id]			UNIQUEIDENTIFIER NOT NULL ,
	[priority_id]		UNIQUEIDENTIFIER NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish] [bit]		NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted] [bit]		NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_TARGET] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET] ADD  CONSTRAINT [DF_PN_TARGET_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET] ADD  CONSTRAINT [DF_PN_TARGET_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET] ADD  CONSTRAINT [DF_PN_TARGET_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET] ADD  CONSTRAINT [DF_PN_TARGET_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_TARGET]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_PN_TARGET_TYPE] FOREIGN KEY ([target_type_id]) 
REFERENCES [dbo].[PN_TARGET_TYPE]([id])

ALTER TABLE [dbo].[PN_TARGET] CHECK CONSTRAINT [FK_PN_TARGET_PN_TARGET_TYPE]
GO

ALTER TABLE [dbo].[PN_TARGET]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_PN_IDEA] FOREIGN KEY ([idea_id]) 
REFERENCES [dbo].[PN_IDEA]([id])

ALTER TABLE [dbo].[PN_TARGET] CHECK CONSTRAINT [FK_PN_TARGET_PN_IDEA]
GO

ALTER TABLE [dbo].[PN_TARGET]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_PN_TARGET_PRIORITY] FOREIGN KEY ([priority_id]) 
REFERENCES [dbo].[PN_TARGET_PRIORITY]([id])

ALTER TABLE [dbo].[PN_TARGET] CHECK CONSTRAINT [FK_PN_TARGET_PN_TARGET_PRIORITY]
GO


--SKIP Index: [fkIdx_201]

--SKIP Index: [fkIdx_206]

--SKIP Index: [fkIdx_210]


--************************************** [dbo].[PN_TARGET_PLANNING]

CREATE TABLE [dbo].[PN_TARGET_PLANNING]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(250) NOT NULL ,
	[description]		NVARCHAR(MAX) NULL ,
	[start_date]		DATETIME NULL ,
	[end_date]			DATETIME NULL ,
	[target_id]			UNIQUEIDENTIFIER NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_TARGET_PLANNING] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_PLANNING_PN_TARGET] FOREIGN KEY ([target_id]) 
REFERENCES [dbo].[PN_TARGET]([id])

ALTER TABLE [dbo].[PN_TARGET_PLANNING] CHECK CONSTRAINT [FK_PN_TARGET_PLANNING_PN_TARGET]
GO


--SKIP Index: [fkIdx_219]


--************************************** [dbo].[PN_TARGET_QUESTION]

CREATE TABLE [dbo].[PN_TARGET_QUESTION]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[target_id]			UNIQUEIDENTIFIER NOT NULL ,
	[question]			NVARCHAR(250) NOT NULL ,
	[answer]			NVARCHAR(500) NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_TARGET_QUESTION] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET_QUESTION] ADD  CONSTRAINT [DF_PN_TARGET_QUESTION_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET_QUESTION] ADD  CONSTRAINT [DF_PN_TARGET_QUESTION_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET_QUESTION] ADD  CONSTRAINT [DF_PN_TARGET_QUESTION_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET_QUESTION] ADD  CONSTRAINT [DF_PN_TARGET_QUESTION_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_TARGET_QUESTION]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_QUESTION_PN_TARGET] FOREIGN KEY ([target_id]) 
REFERENCES [dbo].[PN_TARGET]([id])

ALTER TABLE [dbo].[PN_TARGET_QUESTION] CHECK CONSTRAINT [FK_PN_TARGET_QUESTION_PN_TARGET]
GO


--SKIP Index: [fkIdx_215]


--************************************** [dbo].[PN_TARGET_PLANNING_REPORT]

CREATE TABLE [dbo].[PN_TARGET_PLANNING_REPORT]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[date]				DATETIME NOT NULL ,
	[title]				NVARCHAR(250) NOT NULL ,
	[content]			NVARCHAR(MAX) NULL ,
	[planning_id]		UNIQUEIDENTIFIER NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_TARGET_PLANNING_REPORT] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_REPORT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_REPORT_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_REPORT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_REPORT_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_REPORT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_REPORT_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_REPORT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_REPORT_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_REPORT]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_PLANNING_REPORT_PN_TARGET_PLANNING] FOREIGN KEY ([planning_id]) 
REFERENCES [dbo].[PN_TARGET_PLANNING]([id])

ALTER TABLE [dbo].[PN_TARGET_PLANNING_REPORT] CHECK CONSTRAINT [FK_PN_TARGET_PLANNING_REPORT_PN_TARGET_PLANNING]
GO


--SKIP Index: [fkIdx_223]


--************************************** [dbo].[PN_TARGET_PLANING_SPRINT]

CREATE TABLE [dbo].[PN_TARGET_PLANNING_SPRINT]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[name]				NVARCHAR(50) NOT NULL ,
	[start_date]		DATETIME NULL ,
	[end_date]			DATETIME NULL ,
	[status_done]		SMALLINT NULL ,
	[planning_id]		UNIQUEIDENTIFIER NOT NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_TARGET_PLANNING_SPRINT] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_SPRINT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_SPRINT_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_SPRINT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_SPRINT_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_SPRINT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_SPRINT_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_SPRINT] ADD  CONSTRAINT [DF_PN_TARGET_PLANNING_SPRINT_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_TARGET_PLANNING_SPRINT]  WITH CHECK ADD CONSTRAINT [FK_PN_TARGET_PLANING_SPRINT_PN_TARGET_PLANNING] FOREIGN KEY ([planning_id]) 
REFERENCES [dbo].[PN_TARGET_PLANNING]([id])

ALTER TABLE [dbo].[PN_TARGET_PLANNING_SPRINT] CHECK CONSTRAINT [FK_PN_TARGET_PLANING_SPRINT_PN_TARGET_PLANNING]
GO


--SKIP Index: [fkIdx_228]


--************************************** [dbo].[PN_TASK]

CREATE TABLE [dbo].[PN_TASK]
(
	[id]				UNIQUEIDENTIFIER NOT NULL ,
	[sprint_id]			UNIQUEIDENTIFIER NOT NULL ,
	[task_status_id]	UNIQUEIDENTIFIER NOT NULL ,
	[task_type_id]		UNIQUEIDENTIFIER NOT NULL ,
	[title]				NVARCHAR(200) NOT NULL ,
	[description]		NVARCHAR(MAX) NULL ,
	[start_date]		DATETIME NULL ,
	[end_date]			DATETIME NULL ,
	[parent_id]			UNIQUEIDENTIFIER NULL ,
	[ordering]			[smallint] NOT NULL,
	[publish]			[bit] NOT NULL,
	[created_by]		[uniqueidentifier] NOT NULL,
	[created_date]		[datetime] NOT NULL,
	[updated_by]		[uniqueidentifier] NULL,
	[updated_date]		[datetime] NULL,
	[deleted]			[bit] NOT NULL,
	[deleted_by]		[uniqueidentifier] NULL,
	[deleted_date]		[datetime] NULL,
	CONSTRAINT [PK_PN_TASK] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

ALTER TABLE [dbo].[PN_TASK] ADD  CONSTRAINT [DF_PN_TASK_ordering]  DEFAULT ((1)) FOR [ordering]
GO

ALTER TABLE [dbo].[PN_TASK] ADD  CONSTRAINT [DF_PN_TASK_publish]  DEFAULT ((1)) FOR [publish]
GO

ALTER TABLE [dbo].[PN_TASK] ADD  CONSTRAINT [DF_PN_TASK_create_date]  DEFAULT (getdate()) FOR [created_date]
GO

ALTER TABLE [dbo].[PN_TASK] ADD  CONSTRAINT [DF_PN_TASK_deleted]  DEFAULT ((0)) FOR [deleted]
GO

ALTER TABLE [dbo].[PN_TASK]  WITH CHECK ADD CONSTRAINT [FK_PN_TASK_PN_TARGET_PLANNING_SPRINT] FOREIGN KEY ([sprint_id]) 
REFERENCES [dbo].[PN_TARGET_PLANNING_SPRINT]([id])

ALTER TABLE [dbo].[PN_TASK] CHECK CONSTRAINT [FK_PN_TASK_PN_TARGET_PLANNING_SPRINT]
GO

ALTER TABLE [dbo].[PN_TASK]  WITH CHECK ADD CONSTRAINT [FK_PN_TASK_PN_TASK_STATUS] FOREIGN KEY ([task_status_id]) 
REFERENCES [dbo].[PN_TASK_STATUS]([id])

ALTER TABLE [dbo].[PN_TASK] CHECK CONSTRAINT [FK_PN_TASK_PN_TASK_STATUS]
GO

ALTER TABLE [dbo].[PN_TASK]  WITH CHECK ADD CONSTRAINT [FK_PN_TASK_PN_TASK] FOREIGN KEY ([parent_id]) 
REFERENCES [dbo].[PN_TASK]([id])

ALTER TABLE [dbo].[PN_TASK] CHECK CONSTRAINT [FK_PN_TASK_PN_TASK]
GO

ALTER TABLE [dbo].[PN_TASK]  WITH CHECK ADD CONSTRAINT [FK_PN_TASK_PN_TASK_TYPE] FOREIGN KEY ([task_type_id]) 
REFERENCES [dbo].[PN_TASK_TYPE]([id])

ALTER TABLE [dbo].[PN_TASK] CHECK CONSTRAINT [FK_PN_TASK_PN_TASK_TYPE]
GO



