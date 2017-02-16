Create TABLE [dbo].[TE_Companys](
	[ID] [int] NOT NULL,                       /*公司id       2位，从11开始编号*/
	[Name] [nvarchar](50) NOT NULL,            /*公司名称     50个字*/
	[Tel] [varchar](50) NULL,                  /*电话*/
	[Fax] [varchar](50) NULL,                  /*传真*/
	[Zip] [char](6) NULL,                      /*邮编*/
	[Site] [varchar](100) NULL,                /*网站*/
	[Email] [varchar](100) NULL,               /*Email*/
	[Address] [nvarchar](50) NULL,             /*公司地点*/
	[BankAccount] [nvarchar](100) NULL,        /*银行账户*/
	[Introduction] [nvarchar](max) NULL,       /*简介*/
	[LinkID] int NULL,       /*关联的公司编号*/
	[Linktype] int null,--关联类型（0母公司，2子公司）
	[Manage] uniqueidentifier null,--维护管理责任人
	[Uptime] datetime null,--信息更新时间
	[Ctype] varchar(500) null,--公司类型
	[Setuptime] datetime null,--公司成立时间
	[Operatetime] datetime null,--经营期限
	[Operate] text null,--经营范围
	[Route] text null,--乘车到达公司路线
	[State] bit null  --状态（true信息可修改，false或null不可修改.流程审批通过后修改此属性为true,信息更改完自动更新成false）


 CONSTRAINT [PK_TE_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[TE_Departments]    Script Date: 07/02/2012 13:40:03 ******/
CREATE TABLE [dbo].[TE_Departments](
    [CompanyID] [int] NULL,                    /*公司id */
	[DepartmentID] [int] NULL,                 /*公司部门id */
	[ID] [int] NULL,                           /*部门id      3位从101开始*/
	[ParentID] [int] NULL,                     /*父部门id*/
	[Name] [nvarchar](50) NULL,                /*部门名称*/
	[Tel] [varchar](50) NULL,                  /*部门电话*/
	[Fax] [varchar](50) NULL,                  /*传真*/
	[Address] [nvarchar](50) NULL,             /*部门地址*/
	[Sort] [int]  NULL,                        /*部门排序*/
	[Content] [ntext] NULL,                     /*部门简介*/
	[Host] [uniqueidentifier] NULL,            /*部门主管*/
	[Assistants] [nvarchar](200) NULL,         /*部门助理*/
	[UpHost] [uniqueidentifier] NULL,          /*上级部门经理*/
	[UpHosts] [nvarchar](200) NULL            /*上级部门分管领导*/

) ON [PRIMARY]

USE [WXOA]
GO

/****** Object:  Table [dbo].[TU_Employees]    Script Date: 07/02/2012 13:44:08 ******/
CREATE TABLE [dbo].[TU_Employees](
    [CompanyID] [int] NULL,                    /*公司id */
	[DepartmentID] [int] NOT NULL,             /*部门id*/
	[UserID] [uniqueidentifier] NOT NULL,      /*用户id 自动生成*/
	[IDCard] [varchar(30)]  NOT NULL,          /*身份证号码*/
	[RealName] [nvarchar](50) NOT NULL,        /*真实姓名*/
	[DutyId] [int] NOT NULL,                   /*职务*/
	[Sex] [bit] NOT NULL,                      /*性别*/
	[Birthday] [smalldatetime] NULL,           /*出生日期*/
	[Mobile] [varchar](20) NULL,               /*个人手机*/
	[QQ] [varchar](20) NULL,                   /*QQ号*/
	[Email] [varchar](100) NULL,               /*Email*/
	[Tel] [varchar](20) NULL,                  /*工作电话*/
	[Sort] [int] NULL,                         /*部门员工排序*/
	[Address] [nvarchar](50) NULL,             /*居住地点*/
	[UserFace] [nvarchar](100) NULL,            /*头像*/
	[Introduction] [nvarchar](MAX) NULL,        /*备注*/
	[IsInsurance] int NULL default 0        /*是否有保险（0无,1有）*/
 CONSTRAINT [PK_TU_Employees] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[TU_Audition]    员工面试表 ******/
CREATE TABLE [dbo].[TU_Audition](
	[ID] int IDENTITY(1,1),					/*id */
    [UserID] [uniqueidentifier] NULL,		/*员工id */
	[FirstUser] [uniqueidentifier] NULL,	/*初审人id */
	[FirstOpinion] text NULL,				/*初审意见*/
	[FirstTime] [datetime] NULL,			/*初审时间id*/
	[AuditionUser] [uniqueidentifier] NULL,	/*面试人id*/
	[AuditionState] [int] default 0 NULL,	/*面试结果0未面试，-1不合格，1合格*/
	[AuditionTime] [datetime] NULL,			/*面试时间*/
	[AddTime] [datetime] default getdate()	/*提交简历时间*/
) ON [PRIMARY]

USE [WXOA]
GO
CREATE TABLE [dbo].[TU_Employees_Contract] (--员工合同表
	[Id] bigint NOT NULL IDENTITY, 
	[CNO] varchar(50) null,--合同编号
	[UserID] varchar(200),--员工编号
	[Annex] varchar(6000), --扫描件
	[BeginTime] datetime null,  --签订时间
	[EndTime] datetime null,  --到期时间
	[ManageUserID] varchar(200),--办理人编号
	[Type] int Default 0,--类型（0试用协议，1合同） 
	[Addtime] datetime DEFAULT (getdate())--上传时间
)