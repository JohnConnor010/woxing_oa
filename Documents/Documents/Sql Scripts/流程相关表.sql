/*������*/
CREATE TABLE [dbo].[FL_FormCatagory](
	[Id] [smallint] IDENTITY(11,1) NOT NULL, --Ŀ¼���
	[Name] [nvarchar](50) NOT NULL,          --Ŀ¼����
	[ParentId] [smallint] NULL,              --��Ŀ¼���
	[Sort] [smallint] NULL,                  --����
) ON [PRIMARY]
/*��*/
CREATE TABLE [dbo].[FL_Forms](
	[Id] [smallint] IDENTITY(11,1) NULL,     --�����
	[CatagoryId] [smallint] NULL,            --��Ŀ¼
	[Name] [nvarchar](50) NULL,              --������
	[Module] [nvarchar](max) NULL,           --����ģ��
    [Module_Short] [nvarchar](max) NULL,     --����ģ��
	[Script] [nvarchar](max) NULL,           --�ű�
	[Css] [nvarchar](max) NULL,              --��ʽ
	[Items] [nvarchar](500) NULL,            --�ֶ��б�,������޸��ύʱ����
	[Sort] [smallint] NULL,                  --����
	[DepartmentId] [int] NULL default(0)     --���ű��
) ON [PRIMARY]
/*��������*/
CREATE TABLE [dbo].[FL_FlowCatagory](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,  --Ŀ¼���
	[Name] [nvarchar](50) NOT NULL,          --Ŀ¼����
	[ParentId] [smallint] NULL,              --��Ŀ¼���
	[Sort] [smallint] NULL,                  --����
) ON [PRIMARY]
/*����*/
CREATE TABLE [dbo].[FL_Flows](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,  --���̱��
	[CatagoryId] [smallint] NULL,            --����Ŀ¼
	[Type] [tinyint] NULL,                   --�������ͣ��̶�����,�Զ����̣�
	[FormId] [smallint] NULL,                --��
 	[Name] [nvarchar](50) NULL,              --����
	[AllowAttach] [bit] NULL,                --�Ƿ�������
	[Sort] [smallint] NULL,                  --����
	[Description] [nvarchar](300) NULL,      --����
	[AuthorizeMode] [tinyint] NULL,          --ί��ģʽ������ί��,���������õľ���Ȩ��ί��,������ί�е�ǰ���辭����,��ֹί�У�
	[ExtendFields] [nvarchar](300) NULL,     --��չ�ֶ�
	[AllowView] [bit] NULL,                  --������
	[ViewPriv] [nvarchar](max) NULL,         --������Ա
	[NumberRuleId] [smallint] NULL,          --��ˮ�Ź���(<-FL_NumberRules)
) ON [PRIMARY]
/*����ί��*/
CREATE TABLE [dbo].[FL_FlowAuthorization](
	[Id] [int] IDENTITY(1,1) NOT NULL,       --ί�б��
	[FlowId] [smallint] NULL,                --���̱��
	[FromUserId] [uniqueidentifier] NULL,    --ί����
	[ToUserId] [uniqueidentifier] NULL,      --��ί����
	[BeginDate] [smalldatetime] NULL,        --��ʼ����
	[EndDate] [smalldatetime] NULL,          --��������
	[Status] [bit] NULL                      --�Ƿ�Ϊί��״̬
) ON [PRIMARY]
/*���̹���*/
CREATE TABLE [dbo].[FL_FlowManage](
	[Id] [smallint] NOT NULL,                --������
	[FlowId] [smallint] NOT NULL,            --���̱��
	[ManageType] [tinyint] NOT NULL,         --��������(����,���,��ѯ,���,����)
	[Scope] [tinyint] NOT NULL,              --��Χ(������,������,���в���,�Զ��岿��)
	[UserList] [nvarchar](max) NULL,         --�û��б�
	[DeptList] [nvarchar](max) NULL,         --�����б�
	[DutyList] [nvarchar](max) NULL,         --ְ���б�
) ON [PRIMARY]
/*��������*/
CREATE TABLE [dbo].[FL_FlowTimer](
	[Id] [int] IDENTITY(1,1) NOT NULL,       --���ѱ��
	[FlowId] [int] NOT NULL,                 --���̱��
	[UserList] [nvarchar](max) NOT NULL,     --�û��б�
	[RemindType] [tinyint] NOT NULL,         --��������(ֻ����һ�Σ�ÿ��һ�Σ�ÿ��һ�Σ�ÿ��һ�Σ�ÿ��һ��)
	[RemindTime] [datetime] NOT NULL,        --����ʱ��
	[LastTime] [datetime] NOT NULL,          --�������ʱ��
) ON [PRIMARY]
/*���̲���*/
CREATE TABLE [dbo].[FL_Process](
	[Id] [int] NULL,                         --������
	[FlowId] [smallint] NULL,                --���̱��
	[StepNo] [tinyint] NULL,                 --����˳����
	[Name] [nvarchar](50) NULL,              --����
	[NodeType] [tinyint] NULL,               --�ڵ�����(����ڵ㡢�����̽ڵ㡢�ⲿ�������ӽڵ�)
	[Next_Nodes] [nvarchar](100) NULL,       --��һ�ڵ�˳����
	[Priv_UserList] [nvarchar](max) NULL,    --�������û��б�
	[Priv_DutyList] [nvarchar](max) NULL,    --������ְ���б�
	[Priv_DeptList] [nvarchar](max) NULL,    --�����˲����б�
	[Fields_Editable] [nvarchar](max) NULL,  --�ɱ༭�ֶ��б�
	[Fields_Hidden] [nvarchar](max) NULL,    --�����ֶ��б�
	[VML_Top] [smallint] NULL,               --VMLͼ�ϱ߾�
	[VML_Left] [smallint] NULL,              --VMLͼ��߾�
	[Condition_In] [nvarchar](max) NULL,     --��������
	[Condition_Out] [nvarchar](max) NULL,    --�˳�����
	[Plug_In] [nvarchar](max) NULL,          --������
	[Plug_Out] [nvarchar](max) NULL,         --�˳����
	[Auto_Type] [tinyint] NULL,              --����ѡ������
											 --0.�������Զ�ѡ��
											 --1.�Զ�ѡ�����̷�����
											 --2.�Զ�ѡ�񱾲�������
											 --3.�Զ�ѡ�񱾲�������
											 --4.�Զ�ѡ���ϼ������쵼
											 --5.�Զ�ѡ���ϼ��ֹ��쵼
											 --6.�Զ�ѡ��һ����������
											 --7.ָ���Զ�ѡ��Ĭ����Ա
											 --8.�����ֶ�ѡ��
											 --9.�Զ�ѡ��ָ������������
											 --10.�Զ�ѡ�񱾲����ڷ�������������Ա
											 --11.�Զ�ѡ��һ�������ڷ�������������Ա
	[Auto_UserList] [nvarchar](max) NULL,    --�������б�
	[Auto_UserOP] [nvarchar](50) NULL,       --�������б�
	[Auto_FilterMode] [tinyint] NULL,        --���ܹ�����ѡ��
	                                         --0.����ѡ��ȫ���ľ�����
	                                         --1.����ѡ�񱾲��ŵľ�����
	                                         --2.����ѡ���ϼ����ŵľ�����
	                                         --3.����ѡ���¼����ŵľ�����	                                         	                                         	                                         
	                                         --4.����ѡ�񱾽�ɫ�ľ�����	                                         
	[Auto_OPMode] [tinyint] NULL,            --������ѡ��
	                                         --0.��ȷָ��������
	                                         --1.�������˻�ǩ
	                                         --2.�Ƚ�����Ϊ������
	[Auto_OpChangeMode] [tinyint] NULL,      --�Ƿ������޸����������ѡ�Ĭ�Ͼ�����(������,����)
	[Auto_BaseUnit] [tinyint] NULL,          --�ڼ���
	[Sign_Look] [tinyint] NULL,              --��ǩ�ɼ���(���ǿɼ�����Ա�����֮�䲻�ɼ�������������費�ɼ�)
	[Sign_Mode] [tinyint] NULL,              --��ǩģʽ(�����ǩ����ֹ��ǩ��ǿ�ƻ�ǩ)
	[Pass_OpForce] [tinyint] NULL,           --�����˿�ǿ��ת����������δ�������ʱ�Ƿ�����������ǿ��ת����
	[Pass_RollBack] [tinyint] NULL,          --����(�����������˵���һ���������˵�֮ǰ����)
	[Msg_ViewMode] [tinyint] NULL,           --��������
	[Msg_ViewUsers] [tinyint] NULL,          --�����û�Ⱥ	
	[Sync_DealMode] [tinyint] NULL,          --����ѡ���ֹ,����,ǿ�ƣ�
	[Sync_CombineMode] [tinyint] NULL,        --�����ϲ�ѡ�ǿ�ƺϲ�,��ǿ�ƺϲ���
	IsBeginUser  int null default 0,		--�Ƿ����������˱�����ˣ�0��1�ǣ�
	UpdateTable varchar(500) null,			--״̬�ı����
	UpdateKeyValue varchar(2000) null		--״̬�ı��ֵ
) ON [PRIMARY]
/*��ӹ�������*/
CREATE TABLE [dbo].[FL_Run](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --����������
	[ParentId] [bigint] NULL,                --�����
	[Name] [nvarchar](50) NULL,              --����
	[FlowId] [smallint] NULL,                --���̱��
	[BeginUser] [uniqueidentifier] NULL,     --���������û�
	[FromUser] [uniqueidentifier] NULL,      --�����û�(?)	
	[BeginTime] [datetime] NULL,             --��ʼʱ��
	[EndTime] [datetime] NULL,               --����ʱ��
	[Attach_IdList] [nvarchar](100) NULL,    --�����б�
	[Attach_NameList] [nvarchar](200) NULL,  --���������б�
	[ForusUsers] [nvarchar](200) NULL,       --��ע�û�
	[ViewUsers] [nvarchar](200) NULL,        --�鿴�����û�
	[Archive] tinyint NULL,                  --�浵
	[Del_Flag] [bit] NULL default(0),        --ɾ����־
	[Deal_Flag] int null,					--״̬
	[StepNo] int null,						--��ǰ����
	[AIP_Files] [nvarchar](max) NULL,        --(?)
) ON [PRIMARY]
/*������־*/
CREATE TABLE [dbo].[FL_RunLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --��־���
	[RunId] [bigint] NULL,                   --�������
	[Name] [nvarchar](50) NULL,              --��������
	[FlowId] [smallint] NULL,                --���̱��
	[StepNo] [tinyint] NULL,                 --����˳����
	[UserId] [uniqueidentifier] NULL,        --�û�
	[Time] [datetime] NULL,                  --ʱ��
	[IP] [varchar](20) NULL,                 --IP
	[Type] [tinyint] NULL,                   --����(?)
	[Content] [nvarchar](100) NULL,          --����
) ON [PRIMARY]
/*����ǩ��*/
CREATE TABLE [dbo].[FL_RunFeedBack](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --ǩ����
	[RunId] [bigint] NULL,                   --�������
	[StepNO] [tinyint] NULL,                 --����˳����
	[UserId] [uniqueidentifier] NULL,        --�û�
	[Content] [nvarchar](200) NULL,          --����
	[Attach_IdList] [varchar](100) NULL,     --�����б�
	[Attach_NameList] [varchar](100) NULL,   --���������б�
	[EditTime] [datetime] NULL,              --�༭ʱ��
	[FeedFlag] [tinyint] NULL,               --ǩ���־(?)
	[SignData] [nvarchar](max) NULL,         --��дǩ������
	[ReplyId] [bigint] NULL,                 --�ظ����
) ON [PRIMARY]
/*��������*/
CREATE TABLE [dbo].[FL_RunDatas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --���������ݱ��
	[RunId] [bigint] NULL,                   --�������
	[FormId] [bigint] NULL,                  --�������
	[Name] [nvarchar](100) NULL,             --����
	[BeginUserId] [uniqueidentifier] NULL,   --�����û�
	[BeginTime] [datetime] NULL,             --ʱ��
	[Datas] [nvarchar](max) NULL,            --����
) ON [PRIMARY]
/*��������*/
CREATE TABLE [dbo].[FL_RunAttachs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --�������
	[RunId] [bigint] NULL,                   --�������
	[StepNo] [tinyint] NULL,                 --����˳����
	[NewFileName] [varchar](100) NULL,       --���ļ���
	[OldFileName] [nvarchar](100) NULL,      --ԭ�ļ���
	[UploadUserID] [uniqueidentifier] NULL,  --�ϴ��û�
	[UploadTime] [datetime] NULL,            --�ϴ�ʱ��
	[UploadIP] [varchar](20) NULL,           --�ϴ�ʱ��
) ON [PRIMARY]
/*����������ϸ*/
CREATE TABLE [dbo].[FL_RunProcess](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --����������
	[RunId] [bigint] NULL,                   --�������
	[StepNo] [tinyint] NULL,                 --����˳����
	[ParentNo] [tinyint] NULL,               --��˳����
	[UserId] [uniqueidentifier] NULL,        --�û�
	[WorkTime] [datetime] NULL,              --����ʱ��
	[DeliverTime] [datetime] NULL,           --ת��ʱ��
	[Deal_Flag] [tinyint] NULL,              --�����־
	[OP_Flag] [tinyint] NULL,                --������־
	[TimeOutFlag] [tinyint] NULL,            --��ʱ��־
	[OtherUsers] [nvarchar](100) NULL,       --�����û�
	[FromUsers] [nvarchar](100) NULL,        --�����û�
	[CreateTime] [datetime] NULL default(GetDate()),
	                                         --ʱ��
	[ActiveTime] [datetime] NULL,            --�浵ʱ��
	[Comment] [nvarchar](100) NULL,          --˵��
) ON [PRIMARY]
