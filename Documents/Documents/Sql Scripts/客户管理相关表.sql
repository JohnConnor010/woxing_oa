USE [WXERP]
GO
/****** Object:  Table [dbo].[CRM_Type]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_Type] ON
INSERT [dbo].[CRM_Type] ([ID], [TypeName]) VALUES (1, N'试用客户')
INSERT [dbo].[CRM_Type] ([ID], [TypeName]) VALUES (2, N'潜在客户')
INSERT [dbo].[CRM_Type] ([ID], [TypeName]) VALUES (3, N'正式客户')
INSERT [dbo].[CRM_Type] ([ID], [TypeName]) VALUES (4, N'合作伙伴')
INSERT [dbo].[CRM_Type] ([ID], [TypeName]) VALUES (5, N'地区分销商')
SET IDENTITY_INSERT [dbo].[CRM_Type] OFF
/****** Object:  Table [dbo].[CRM_State]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_State](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_State] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_State] ON
INSERT [dbo].[CRM_State] ([ID], [StateName]) VALUES (1, N'未签约')
INSERT [dbo].[CRM_State] ([ID], [StateName]) VALUES (2, N'正在签约')
INSERT [dbo].[CRM_State] ([ID], [StateName]) VALUES (3, N'已签约')
INSERT [dbo].[CRM_State] ([ID], [StateName]) VALUES (4, N'已完结')
SET IDENTITY_INSERT [dbo].[CRM_State] OFF
/****** Object:  Table [dbo].[CRM_Stage]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Stage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StageName] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_Stage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_Stage] ON
INSERT [dbo].[CRM_Stage] ([ID], [StageName]) VALUES (1, N'售前跟踪')
INSERT [dbo].[CRM_Stage] ([ID], [StageName]) VALUES (2, N'合同执行')
INSERT [dbo].[CRM_Stage] ([ID], [StageName]) VALUES (3, N'售后服务')
SET IDENTITY_INSERT [dbo].[CRM_Stage] OFF
/****** Object:  Table [dbo].[CRM_Source]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Source](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SourceName] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_Source] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_Source] ON
INSERT [dbo].[CRM_Source] ([ID], [SourceName]) VALUES (1, N'网络广告')
INSERT [dbo].[CRM_Source] ([ID], [SourceName]) VALUES (2, N'朋友介绍')
INSERT [dbo].[CRM_Source] ([ID], [SourceName]) VALUES (3, N'展览会')
INSERT [dbo].[CRM_Source] ([ID], [SourceName]) VALUES (4, N'其他')
SET IDENTITY_INSERT [dbo].[CRM_Source] OFF
/****** Object:  Table [dbo].[CRM_Level]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Level](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_Level] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_Level] ON
INSERT [dbo].[CRM_Level] ([ID], [LevelName]) VALUES (1, N'普通')
INSERT [dbo].[CRM_Level] ([ID], [LevelName]) VALUES (2, N'重要')
SET IDENTITY_INSERT [dbo].[CRM_Level] OFF
/****** Object:  Table [dbo].[CRM_Information]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Information](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](50) NULL,
	[CustomerName] [varchar](50) NULL,
	[CustomerZJM] [varchar](50) NULL,
	[CategoryID] [int] NULL,
	[NatureID] [int] NULL,
	[RealName] [varchar](50) NULL,
	[LinkMan] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[WebSite] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[SourceID] [int] NULL,
	[IndustryID] [int] NULL,
	[StageID] [int] NULL,
	[TypeID] [int] NULL,
	[LevelID] [int] NULL,
	[StartDate] [datetime] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[ConsumptionMoney] [decimal](18, 0) NULL,
	[MaintainMoney] [decimal](18, 0) NULL,
	[Province] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Area] [varchar](50) NULL,
	[PreMoney] [decimal](18, 0) NULL,
	[Integral] [int] NULL,
	[StateID] [int] NULL,
	[Description] [text] NULL,
	[Account] [varchar](50) NULL,
	[BusinessCircles] [varchar](50) NULL,
	[BankName] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Products] [text] NULL,
	[AttachFile] [varchar](200) NULL,
 CONSTRAINT [PK_Cust_Information] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Province'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'City'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Area'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'PreMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工商登记号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'BusinessCircles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CRM_Information', @level2type=N'COLUMN',@level2name=N'Products'
GO
/****** Object:  Table [dbo].[CRM_Industry]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Industry](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IndustryName] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_Industry] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_Industry] ON
INSERT [dbo].[CRM_Industry] ([ID], [IndustryName]) VALUES (1, N'IT')
INSERT [dbo].[CRM_Industry] ([ID], [IndustryName]) VALUES (2, N'金融')
INSERT [dbo].[CRM_Industry] ([ID], [IndustryName]) VALUES (3, N'旅游')
SET IDENTITY_INSERT [dbo].[CRM_Industry] OFF
/****** Object:  Table [dbo].[CRM_CompanyNature]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_CompanyNature](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyNature] [varchar](50) NULL,
 CONSTRAINT [PK_Cust_CompanyNature] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_CompanyNature] ON
INSERT [dbo].[CRM_CompanyNature] ([ID], [CompanyNature]) VALUES (1, N'国有企业')
INSERT [dbo].[CRM_CompanyNature] ([ID], [CompanyNature]) VALUES (2, N'集体企业')
INSERT [dbo].[CRM_CompanyNature] ([ID], [CompanyNature]) VALUES (3, N'有限责任公司')
INSERT [dbo].[CRM_CompanyNature] ([ID], [CompanyNature]) VALUES (4, N'股份有限')
INSERT [dbo].[CRM_CompanyNature] ([ID], [CompanyNature]) VALUES (5, N'个人客户')
SET IDENTITY_INSERT [dbo].[CRM_CompanyNature] OFF
/****** Object:  Table [dbo].[CRM_Category]    Script Date: 08/07/2012 10:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_CustomerCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CRM_Category] ON
INSERT [dbo].[CRM_Category] ([ID], [CategoryName], [ParentId]) VALUES (1, N'客户分类', 0)
INSERT [dbo].[CRM_Category] ([ID], [CategoryName], [ParentId]) VALUES (2, N'供应商', 1)
INSERT [dbo].[CRM_Category] ([ID], [CategoryName], [ParentId]) VALUES (3, N'客户', 1)
SET IDENTITY_INSERT [dbo].[CRM_Category] OFF
/****** Object:  Default [DF_CustomerCategory_ParentId]    Script Date: 08/07/2012 10:58:15 ******/
ALTER TABLE [dbo].[CRM_Category] ADD  CONSTRAINT [DF_CustomerCategory_ParentId]  DEFAULT ((1)) FOR [ParentId]
GO
