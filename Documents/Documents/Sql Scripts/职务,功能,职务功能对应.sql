/*公司职务表*/
CREATE TABLE [dbo].[TE_Duties](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,   --职务ID
	[CompanyID] [int] NOT NULL,               --公司ID
	[Name] [nvarchar](50) NULL,               --职务名称
	[Menus] [nvarchar](max) NULL,             --职务菜单  由TE_Functions与TE_FunctionsInDuties生成
	[Description] text null						--职责描述
 CONSTRAINT [PK_TE_Duties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
/*职能表*/
/****** Object:  Table [dbo].[TE_Functions]    Script Date: 07/04/2012 13:19:17 ******/
CREATE TABLE [dbo].[TE_Functions](
	[ID] [smallint] NOT NULL,                  --功能ID
	[ParentID] [smallint] NULL,                --父功能ID
	[Name] [nvarchar](50) NULL,                --功能名称
	[State] [tinyint] NULL default(0),         --功能状态  0-关闭 1-打开普通功能，2-打开读写删功能
	[Title] [nvarchar](50) NULL,               --功能标题
	[Url] [nvarchar](100) NULL,                --功能地址
	[Urls] [nvarchar](Max) NULL,               --页面列表
	[Degree] int  NULL,                        --等级
	[Htmls] [varchar](200) NULL,               --功能Html代码，如果不为空，则代替Title与Url功能
	[Icon] [varchar](50) NULL,                 --功能图标
	[OrderID] int	NULL,					   --排序字段
	[TypeID] int	NULL					   --分类
	)
	
create TABLE [dbo].[TE_FunctionType] (--功能分类
	[ID] int IDENTITY(1,1) NOT NULL,--自动编号
	[TypeName] varchar(50) null,	--分类名称
	[Money] int null,				--金额
	demo text null					--注释备注
)
 CONSTRAINT [PK_TE_Accesses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
/*职务功能表*/
/****** Object:  Table [dbo].[TE_FunctionsInDuties]    Script Date: 07/04/2012 13:15:22 ******/
CREATE TABLE [dbo].[TE_FunctionsInDuties](
	[FunctionID] [smallint] NOT NULL,           --功能ID
	[DutyID] [smallint] NOT NULL,               --职务ID
	[Flag] [tinyint] NULL default(0),           --状态 当FunctionState为0时不起作用
	                                            --     当FunctionState为1时只允许为0,1
	                                            --     当FunctionState为2时允许为0,1,2,3
 CONSTRAINT [PK_TE_FunctionsInDuties] PRIMARY KEY CLUSTERED 
(
	[FunctionID] ASC,
	[DutyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


