create table PLAN_Plan--�ƻ���
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(2000) null,	--����
	Total int null,				--Ԥ���������
	[Current] int null,			--��ǰ�����
	UserID [uniqueidentifier] null,--�û����
	[DepartmentID] [int] NULL,	--���ű��
	[Content] text null,		--����
	Summary text null,			--�ܽ�
	Appraise varchar(2000) null,--�ܽ�����
	[Type] int null,			--�ƻ����ࣨ1�ռƻ���2�ܼƻ���3�¼ƻ���
	RangeType int null,			--�ƻ���Χ��1���˼ƻ���2���żƻ���3��˾�ƻ���
	Starttime datetime null,	--��ʼʱ��
	Stoptime datetime null,		--����ʱ��
	Addtime datetime null DEFAULT (getdate())		--���ʱ��
	PlanState int null,         --����״̬��-1δͨ����0δ��ˣ�1�ύ��ˣ�2ͨ����
	Reason varchar(5000)		--��δͨ����ͨ����ԭ��
)
create table PLAN_Task--�����
(
	id int IDENTITY(1,1) NOT NULL,
	PlanID int not null,		--�ƻ����
	Title varchar(2000) null,	--����
	UserIDs text null,			--�û���ż��ϣ����żƻ���ʹ�ã�ָ��ִ���ˣ�
	[Content] text null,		--����
	Appraise varchar(2000) null,--��������
	[State] int null,			--����״̬��0δ��ɡ�1����С�2����ɣ�
	Etime varchar(200) null,	--Ԥ����ʱ��1�졢2��Сʱ��20���ӵ�������д��
	Statetime datetime null		--״̬ʱ��
)
create talbe PLAN_Appraise
(
	id int IDENTITY(1,1) NOT NULL,
	PlanID int not null,		--�ƻ����
	Appraise int null,			--����
	Content varchar(500) null,	--����
	UserID [uniqueidentifier] null,--������
	AddTime datetime null DEFAULT (getdate())--����ʱ��
)

/*���۹���-��Ʒ��ر�*/
CREATE TABLE [dbo].[PDT_Products] (--��Ʒ��
	[ID] int NOT NULL IDENTITY, 		--�Զ����
	[CategoryID] int, 					--������
	[ProductID] varchar(50), 			--��Ʒ���
	[ProductName] varchar(50), 			--��Ʒ����
	[Specification] varchar(50), 		--���
	[Units] varchar(50), 				--��λ
	[SalesPrice] decimal(18, 2), 		--���ۼ�
	[DiscountedPrice] decimal(18, 2), 	--�Żݼ�
	[CostPrice] decimal(18, 2), 		--�ɱ���
	[Remark] text, 						--��Ʒ˵��
	[Services] text, 					--��������
	[IsEnable] int DEFAULT ((1)), 		--�Ƿ�����(1���ã�2���ã�
	[Addtime] datetime DEFAULT (getdate()),--���ʱ��
	CONSTRAINT [PK_PDT_Products] PRIMARY KEY ([ID])
)

create TABLE [dbo].[PDT_ProductCategory] (--��Ʒ�����
	[ID] int NOT NULL IDENTITY, 		-- �Զ����
	[Name] varchar(50), 				--����
	[ParentID] int DEFAULT ((0)),		--�������
	CONSTRAINT [PK_PDT_ProductCategory] PRIMARY KEY ([ID])
)

create TABLE [dbo].[PDT_ProductDept] (--��Ʒ���ű�
	[ID] int NOT NULL IDENTITY, 		-- �Զ����
	[ProductID] int null,				--��Ʒ���
	[DeptID] int null,					--���ű��
	[MonthFee]  decimal(18, 2) null, 	--��ά������
	[MonthFeeType] int null,			--��ά���Ѽ��㷽ʽ��0�̶����ã�1��Ʒ�ɱ��۱���,2Э��ִ�м۱��ʣ�
	[Fee]  decimal(18, 2) null, 		--��������
	[FeeType] int null,					--�������ü��㷽ʽ��0�̶����ã�1��Ʒ�ɱ��۱���,2Э��ִ�м۱��ʣ�
	Remarks text null					--���ŷ�������
	CONSTRAINT [PDT_ProductDept] PRIMARY KEY ([ID])
)
/*�ͻ����� -- �ͻ����ټ�¼*/
create table CRM_Track--�ͻ����ټ�¼��
(
	id int IDENTITY(1,1) NOT NULL,
	CustomerID int not null,	--�ͻ����
	UserID uniqueidentifier null,--������
	ProcessState int null,		--���ٹ���״̬
	TrackNo int null,	--���ٴ���
	Remarks varchar(8000) null,	--�������
	Fee int null,				--����
	[State] int null,			--����״̬��0δִ�С�1��ִ�У�
	IP varchar(50) null,		--IP
	LogParaments nvarchar(MAX) null,--������Ϣ
	TrackTime datetime null,	--��������ʱ��
	Addtime datetime default getdate()		--¼��ʱ��
)

create table CRM_CustomerProgram --�ͻ�����
(
	id int IDENTITY(1,1) NOT NULL,
	CustomerID int not null,	--�ͻ����
	TrackID int not null,		--���ٱ�� 
	Title varchar(500) null,	--��������
	Content text null,			--��������
	Remarks varchar(500) null,	--�������
	--ProductCategoryID int not null,--������ʽ��ţ�����-��Ʒ���ࣩ
	--BSFee int null,				--���ͼ۸�
	ZDFee int null,				--���ִ�м۸�
	--SSFee int null,				--ʵʩ�۸�
	--DCFee int null,				--��ɼ۸�
	Program varchar(5000) null,	--���������Ӱ��ϴ���
	Type int default 0,			--���ͣ�0���������ʽ��1������ʽ�ܼƣ�
	ProgramTime datetime		--�����ݽ�ʱ��
	Addtime datetime default getdate(),--¼��ʱ��	
	UserID uniqueidentifier null,--�ݽ���
	[State] int null,			--״̬��0��״̬��1�ɹ���
	Updatetime datetime null	--�ɹ�ʱ��
)
create table CRM_CustomerAgreement  --�ͻ�Э���
(
	id int IDENTITY(1,1) NOT NULL,
	Code varchar(200) null,		--��ͬ���
	CustomerID int not null,	--�ͻ����
	TrackID int not null,		--���ٱ�� 
	TempID int null,			--ģ����
	Content text null,			--��������
	AllFee numeric(18, 2) null,	--Э��ִ���ܼ۸�
	Fee numeric(18, 2) null,	--�Ѹ����
	OverFee numeric(18, 2) null,--���
	OverTime datetime null,		--���֧��ʱ��
	Invoice numeric(18, 2) null,--�ѿ���Ʊ���
	OverInvoice numeric(18, 2) null,--ʣ�෢Ʊ���	
	StartTime datetime null,		--��ͬ��Чʱ��
	StopTime datetime null,			--��ͬ����ʱ��
	UserID uniqueidentifier null,	--ǩ��Э���ҵ��Ա���
	Addtime datetime null,			--ǩ��ʱ��
	ProgramID int null,				--�ٳɷ���
	IsCheck int null,				--�Ƿ����(0δ��1����2�Ѵ浵)
	CheckUser uniqueidentifier null,--�����
	CheckTime datetime null 		--���ʱ��
)
create table CRM_Customer_Label  --�ͻ�ģ���ǩ��
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--��ǩ������
	Name varchar(200) null,		--��ǩ���ƣ�Ӣ�ģ�
	Content varchar(2000) null,	--��ǩ����
	Style int null,					--��ǩ���ͣ�������sql�����ã������ݣ�
	Format text null,				--��ǩ��չʾ��ʽ
	UserID uniqueidentifier null,	--��ǩ������
	Type int null,					--���ࣨ������Э�飬����������
	Addtime datetime null			--����ʱ��
)
create table CRM_Customer_Temp  --�ͻ�ģ���
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--ģ�����
	Name varchar(200) null,		--ģ���ļ�����Ӣ������
	Content text null,			--ģ������
	UserID uniqueidentifier null,	--ģ�崴����
	Type int null,					--ģ����ࣨ������Э�飬����������
	Addtime datetime null			--����ʱ��
)
create table CRM_CustomerProducts --����Э��_��Ʒ��
(
	id int IDENTITY(1,1) NOT NULL,
	CustomerID int not null,	--�ͻ����
	PID int not null,			--����ţ�������Э���ţ�
	ProductID int not null,		--��Ʒ���
	ZDFee int null,				--���ִ�м۸�
	Type int default 0,			--���ͣ�1������2Э�飩
	Remarks text null			--������Ҫ��
	[ProductName] varchar(50), 			--��Ʒ����
	[Specification] varchar(50), 		--���
	[Units] varchar(50), 				--��λ
	[SalesPrice] decimal(18, 2), 		--���ۼ�
	[DiscountedPrice] decimal(18, 2), 	--�Żݼ�
	[CostPrice] decimal(18, 2), 		--�ɱ���
	[Remark] text, 						--��Ʒ˵��
	[Services] text 					--��������
)
create TABLE [dbo].[CRM_CustomerDept] (--��Ʒ���ű�
	[ID] int IDENTITY(1,1) NOT NULL, 		-- �Զ����
	[ProductID] int null,				--��Ʒ���
	[DeptID] int null,					--���ű��
	AgreementID int,					--Э����
	[MonthFee]  decimal(18, 2) null, 	--��ά������
	[MonthFeeType] int null,			--��ά���Ѽ��㷽ʽ��0�̶����ã�1��Ʒ�ɱ��۱���,2Э��ִ�м۱��ʣ�
	[Fee]  decimal(18, 2) null, 		--��������
	[FeeType] int null,					--�������ü��㷽ʽ��0�̶����ã�1��Ʒ�ɱ��۱���,2Э��ִ�м۱��ʣ�
	Remarks text null					--���ŷ�������
)
create TABLE [dbo].[WorkOrder_Orders] (--��������
	[ID] int IDENTITY(1,1) NOT NULL, 			-- �Զ����
	[PID] int null,				--�����
	[DeptWorkID] int null,			--���Ź�����
	UserID uniqueidentifier null,	--�ύ��
	AssignUserID uniqueidentifier null,	--������
	ExecUserID uniqueidentifier null,	--ִ����
	[Remarks] text null,				--Ҫ������
	[Title] varchar(500) null,			--����
	[Count] int null,					--������
	[Proj] int null,					--������Ŀ��0���ϼ�����1ɽ��Ƶ����2OA��
	[Type] int null,					--�������ࣨ0������1ά����2������
	[State] int default 0,				--״̬(0�༭�У�1δ���䣬2�ѷ��䣬3δ���գ�4�ѽ��գ�5ִ���У�6����ͣ��7�����գ�8����ɣ�9��ȡ��
	[YJTime] datetime default getdate(), 	--Ԥ�����ʱ��,��������
	[AddTime] datetime default getdate(), 	--����ʱ��
	[SubTime] datetime default getdate(), 	--�ύʱ��
	[FPTime] datetime default getdate(), 	--����ʱ��
	[YSTime] datetime default getdate(), 	--������ʱ��
	[StopTime] datetime default getdate(), 	--���ʱ��
	[IsTop] int null, 						--�Ƿ��ö���1�����ö������������ö���
	[TopOrder] int null, 					--�ö�����
	[TopTime] datetime null, 				--�ö�ʱ��
	[TopCount] varchar(50) null, 			--Ԥ���������/Сʱ��/������
	[StateTime] datetime default getdate() null 	--״̬ʱ��
)
create TABLE [dbo].[WorkOrder_Dept] (--������_X_���ű�
	[ID] int IDENTITY(1,1) NOT NULL, 			--�Զ����
	[WID] int null,				--���������
	[DeptID] int null,			--���ź�
	[State] int default 0,		--״̬(0�༭�У�1δ���䣬2�ѷ��䣬3δ���գ�4�ѽ��գ�5ִ���У�6����ͣ��7�����գ�8����ɣ�9��ȡ��
	[SubTime] datetime default getdate(), 	--�ύʱ��
	[FPTime] datetime default getdate(), 	--����ʱ��
	[YSTime] datetime default getdate(), 	--������ʱ��
	[StopTime] datetime default getdate(), 	--���ʱ��
	[StateTime] datetime default getdate() null 	--״̬ʱ��
)
create TABLE [dbo].[WorkOrder_Message] (--������_X_��Ϣ������
	[ID] int IDENTITY(1,1) NOT NULL, 			--�Զ����
	[WID] int null,				--���������
	[FromUserID] uniqueidentifier null,	--��Ϣ������
	[ToUserID] uniqueidentifier null,	--��Ϣ������
	Remarks text null,					--��Ϣ����
	[State] int  default 0,				--��Ϣ״̬���Ƿ�鿴0δ�鿴��1�Ѳ鿴��
	[AddTime] datetime default getdate()--��Ϣʱ��
)
create TABLE [dbo].[WorkOrder_Appraisal] (--������_X_���۱�
	[ID] int IDENTITY(1,1) NOT NULL, 				--�Զ����
	[WID] int null,					--���������
	[UserID] uniqueidentifier null,	--������
	Remarks text null,				--��������
	[State] int  default 0,			--����״̬���Ƿ�鿴0δ�鿴��1�Ѳ鿴��
	[AddTime] datetime default getdate() --����ʱ��
)
create TABLE [dbo].[XZ_Train] (--������ѵ��
	[ID] int IDENTITY(1,1) NOT NULL, 			--�Զ����
	[Title] varchar(200) null,					--����
	[Type] int null,							--���ͣ�1���ˣ�2��ѵ��
	[UserID] uniqueidentifier null,				--������
	RunTime datetime null,						--���ʱ��
	Addr varchar(500) null,						--�ص�
	FlowID int null,							--��������
	Content text null,							--��ϸ����
	UsersID varchar(8000) null,					--�����Ա���
	UsersName varchar(5000) null,				--�����Ա����
	[AddTime] datetime default getdate() --����ʱ��
)
create TABLE [dbo].[XZ_TrainUsers] (--������ѵ_X_�����Ա��
	[ID] int IDENTITY(1,1) NOT NULL, 			--�Զ����
	[TrainID] int null,							--��ѵ��
	[UserID] uniqueidentifier null,				--Ա����
	RETime datetime null,						--����ʱ��
	[State] int null,							--����״̬��0δ���գ�1�ѽ��գ�2�Ѳ��룩
	RunID int null,								--�ύ�����̱�
	[AddTime] datetime default getdate()		--����ʱ��
)

