Create TABLE [dbo].[TE_Companys](
	[ID] [int] NOT NULL,                       /*��˾id       2λ����11��ʼ���*/
	[Name] [nvarchar](50) NOT NULL,            /*��˾����     50����*/
	[Tel] [varchar](50) NULL,                  /*�绰*/
	[Fax] [varchar](50) NULL,                  /*����*/
	[Zip] [char](6) NULL,                      /*�ʱ�*/
	[Site] [varchar](100) NULL,                /*��վ*/
	[Email] [varchar](100) NULL,               /*Email*/
	[Address] [nvarchar](50) NULL,             /*��˾�ص�*/
	[BankAccount] [nvarchar](100) NULL,        /*�����˻�*/
	[Introduction] [nvarchar](max) NULL,       /*���*/
	[LinkID] int NULL,       /*�����Ĺ�˾���*/
	[Linktype] int null,--�������ͣ�0ĸ��˾��2�ӹ�˾��
	[Manage] uniqueidentifier null,--ά������������
	[Uptime] datetime null,--��Ϣ����ʱ��
	[Ctype] varchar(500) null,--��˾����
	[Setuptime] datetime null,--��˾����ʱ��
	[Operatetime] datetime null,--��Ӫ����
	[Operate] text null,--��Ӫ��Χ
	[Route] text null,--�˳����﹫˾·��
	[State] bit null  --״̬��true��Ϣ���޸ģ�false��null�����޸�.��������ͨ�����޸Ĵ�����Ϊtrue,��Ϣ�������Զ����³�false��


 CONSTRAINT [PK_TE_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[TE_Departments]    Script Date: 07/02/2012 13:40:03 ******/
CREATE TABLE [dbo].[TE_Departments](
    [CompanyID] [int] NULL,                    /*��˾id */
	[DepartmentID] [int] NULL,                 /*��˾����id */
	[ID] [int] NULL,                           /*����id      3λ��101��ʼ*/
	[ParentID] [int] NULL,                     /*������id*/
	[Name] [nvarchar](50) NULL,                /*��������*/
	[Tel] [varchar](50) NULL,                  /*���ŵ绰*/
	[Fax] [varchar](50) NULL,                  /*����*/
	[Address] [nvarchar](50) NULL,             /*���ŵ�ַ*/
	[Sort] [int]  NULL,                        /*��������*/
	[Content] [ntext] NULL,                     /*���ż��*/
	[Host] [uniqueidentifier] NULL,            /*��������*/
	[Assistants] [nvarchar](200) NULL,         /*��������*/
	[UpHost] [uniqueidentifier] NULL,          /*�ϼ����ž���*/
	[UpHosts] [nvarchar](200) NULL            /*�ϼ����ŷֹ��쵼*/

) ON [PRIMARY]

USE [WXOA]
GO

/****** Object:  Table [dbo].[TU_Employees]    Script Date: 07/02/2012 13:44:08 ******/
CREATE TABLE [dbo].[TU_Employees](
    [CompanyID] [int] NULL,                    /*��˾id */
	[DepartmentID] [int] NOT NULL,             /*����id*/
	[UserID] [uniqueidentifier] NOT NULL,      /*�û�id �Զ�����*/
	[IDCard] [varchar(30)]  NOT NULL,          /*���֤����*/
	[RealName] [nvarchar](50) NOT NULL,        /*��ʵ����*/
	[DutyId] [int] NOT NULL,                   /*ְ��*/
	[Sex] [bit] NOT NULL,                      /*�Ա�*/
	[Birthday] [smalldatetime] NULL,           /*��������*/
	[Mobile] [varchar](20) NULL,               /*�����ֻ�*/
	[QQ] [varchar](20) NULL,                   /*QQ��*/
	[Email] [varchar](100) NULL,               /*Email*/
	[Tel] [varchar](20) NULL,                  /*�����绰*/
	[Sort] [int] NULL,                         /*����Ա������*/
	[Address] [nvarchar](50) NULL,             /*��ס�ص�*/
	[UserFace] [nvarchar](100) NULL,            /*ͷ��*/
	[Introduction] [nvarchar](MAX) NULL,        /*��ע*/
	[IsInsurance] int NULL default 0        /*�Ƿ��б��գ�0��,1�У�*/
 CONSTRAINT [PK_TU_Employees] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[TU_Audition]    Ա�����Ա� ******/
CREATE TABLE [dbo].[TU_Audition](
	[ID] int IDENTITY(1,1),					/*id */
    [UserID] [uniqueidentifier] NULL,		/*Ա��id */
	[FirstUser] [uniqueidentifier] NULL,	/*������id */
	[FirstOpinion] text NULL,				/*�������*/
	[FirstTime] [datetime] NULL,			/*����ʱ��id*/
	[AuditionUser] [uniqueidentifier] NULL,	/*������id*/
	[AuditionState] [int] default 0 NULL,	/*���Խ��0δ���ԣ�-1���ϸ�1�ϸ�*/
	[AuditionTime] [datetime] NULL,			/*����ʱ��*/
	[AddTime] [datetime] default getdate()	/*�ύ����ʱ��*/
) ON [PRIMARY]

USE [WXOA]
GO
CREATE TABLE [dbo].[TU_Employees_Contract] (--Ա����ͬ��
	[Id] bigint NOT NULL IDENTITY, 
	[CNO] varchar(50) null,--��ͬ���
	[UserID] varchar(200),--Ա�����
	[Annex] varchar(6000), --ɨ���
	[BeginTime] datetime null,  --ǩ��ʱ��
	[EndTime] datetime null,  --����ʱ��
	[ManageUserID] varchar(200),--�����˱��
	[Type] int Default 0,--���ͣ�0����Э�飬1��ͬ�� 
	[Addtime] datetime DEFAULT (getdate())--�ϴ�ʱ��
)