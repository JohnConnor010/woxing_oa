create table PLAN_Plan--计划表
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(2000) null,	--标题
	Total int null,				--预计总完成数
	[Current] int null,			--当前完成数
	UserID [uniqueidentifier] null,--用户编号
	[DepartmentID] [int] NULL,	--部门编号
	[Content] text null,		--描述
	Summary text null,			--总结
	Appraise varchar(2000) null,--总结评语
	[Type] int null,			--计划分类（1日计划，2周计划，3月计划）
	RangeType int null,			--计划范围（1个人计划，2部门计划，3公司计划）
	Starttime datetime null,	--开始时间
	Stoptime datetime null,		--结束时间
	Addtime datetime null DEFAULT (getdate())		--添加时间
	PlanState int null,         --任务状态（-1未通过，0未审核，1提交审核，2通过）
	Reason varchar(5000)		--（未通过或通过）原因
)
create table PLAN_Task--任务表
(
	id int IDENTITY(1,1) NOT NULL,
	PlanID int not null,		--计划编号
	Title varchar(2000) null,	--标题
	UserIDs text null,			--用户编号集合（部门计划中使用，指定执行人）
	[Content] text null,		--描述
	Appraise varchar(2000) null,--评价评语
	[State] int null,			--任务状态（0未完成、1审核中、2已完成）
	Etime varchar(200) null,	--预计用时（1天、2个小时、20分钟等自由填写）
	Statetime datetime null		--状态时间
)
create talbe PLAN_Appraise
(
	id int IDENTITY(1,1) NOT NULL,
	PlanID int not null,		--计划编号
	Appraise int null,			--评价
	Content varchar(500) null,	--评语
	UserID [uniqueidentifier] null,--评价人
	AddTime datetime null DEFAULT (getdate())--评价时间
)

/*销售管理-产品相关表*/
CREATE TABLE [dbo].[PDT_Products] (--产品表
	[ID] int NOT NULL IDENTITY, 		--自动编号
	[CategoryID] int, 					--分类编号
	[ProductID] varchar(50), 			--产品编号
	[ProductName] varchar(50), 			--产品名称
	[Specification] varchar(50), 		--规格
	[Units] varchar(50), 				--单位
	[SalesPrice] decimal(18, 2), 		--销售价
	[DiscountedPrice] decimal(18, 2), 	--优惠价
	[CostPrice] decimal(18, 2), 		--成本价
	[Remark] text, 						--产品说明
	[Services] text, 					--服务内容
	[IsEnable] int DEFAULT ((1)), 		--是否启用(1启用，2禁用）
	[Addtime] datetime DEFAULT (getdate()),--添加时间
	CONSTRAINT [PK_PDT_Products] PRIMARY KEY ([ID])
)

create TABLE [dbo].[PDT_ProductCategory] (--产品分类表
	[ID] int NOT NULL IDENTITY, 		-- 自动编号
	[Name] varchar(50), 				--名称
	[ParentID] int DEFAULT ((0)),		--父级编号
	CONSTRAINT [PK_PDT_ProductCategory] PRIMARY KEY ([ID])
)

create TABLE [dbo].[PDT_ProductDept] (--产品部门表
	[ID] int NOT NULL IDENTITY, 		-- 自动编号
	[ProductID] int null,				--产品编号
	[DeptID] int null,					--部门编号
	[MonthFee]  decimal(18, 2) null, 	--月维护费用
	[MonthFeeType] int null,			--月维护费计算方式（0固定费用，1产品成本价比率,2协议执行价比率）
	[Fee]  decimal(18, 2) null, 		--制作费用
	[FeeType] int null,					--制作费用计算方式（0固定费用，1产品成本价比率,2协议执行价比率）
	Remarks text null					--部门服务内容
	CONSTRAINT [PDT_ProductDept] PRIMARY KEY ([ID])
)
/*客户管理 -- 客户跟踪记录*/
create table CRM_Track--客户跟踪记录表
(
	id int IDENTITY(1,1) NOT NULL,
	CustomerID int not null,	--客户编号
	UserID uniqueidentifier null,--跟踪人
	ProcessState int null,		--跟踪过程状态
	TrackNo int null,	--跟踪次数
	Remarks varchar(8000) null,	--情况汇总
	Fee int null,				--花销
	[State] int null,			--任务状态（0未执行、1已执行）
	IP varchar(50) null,		--IP
	LogParaments nvarchar(MAX) null,--其它信息
	TrackTime datetime null,	--跟踪日期时间
	Addtime datetime default getdate()		--录入时间
)

create table CRM_CustomerProgram --客户方案
(
	id int IDENTITY(1,1) NOT NULL,
	CustomerID int not null,	--客户编号
	TrackID int not null,		--跟踪编号 
	Title varchar(500) null,	--方案标题
	Content text null,			--方案内容
	Remarks varchar(500) null,	--方案落款
	--ProductCategoryID int not null,--合作形式编号（销售-产品分类）
	--BSFee int null,				--报送价格
	ZDFee int null,				--最低执行价格
	--SSFee int null,				--实施价格
	--DCFee int null,				--达成价格
	Program varchar(5000) null,	--方案（电子版上传）
	Type int default 0,			--类型（0具体合作形式，1合作形式总计）
	ProgramTime datetime		--方案递交时间
	Addtime datetime default getdate(),--录入时间	
	UserID uniqueidentifier null,--递交人
	[State] int null,			--状态（0无状态，1成功）
	Updatetime datetime null	--成功时间
)
create table CRM_CustomerAgreement  --客户协议表
(
	id int IDENTITY(1,1) NOT NULL,
	Code varchar(200) null,		--合同编号
	CustomerID int not null,	--客户编号
	TrackID int not null,		--跟踪编号 
	TempID int null,			--模板编号
	Content text null,			--方案内容
	AllFee numeric(18, 2) null,	--协议执行总价格
	Fee numeric(18, 2) null,	--已付金额
	OverFee numeric(18, 2) null,--余额
	OverTime datetime null,		--余额支付时间
	Invoice numeric(18, 2) null,--已开发票金额
	OverInvoice numeric(18, 2) null,--剩余发票金额	
	StartTime datetime null,		--合同生效时间
	StopTime datetime null,			--合同结束时间
	UserID uniqueidentifier null,	--签订协议的业务员编号
	Addtime datetime null,			--签订时间
	ProgramID int null,				--促成方案
	IsCheck int null,				--是否审核(0未审，1已审，2已存档)
	CheckUser uniqueidentifier null,--审核人
	CheckTime datetime null 		--审核时间
)
create table CRM_Customer_Label  --客户模板标签表
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--标签中文名
	Name varchar(200) null,		--标签名称（英文）
	Content varchar(2000) null,	--标签内容
	Style int null,					--标签类型（变量，sql语句调用，纯内容）
	Format text null,				--标签的展示格式
	UserID uniqueidentifier null,	--标签创建人
	Type int null,					--分类（方案，协议，。。。。）
	Addtime datetime null			--创建时间
)
create table CRM_Customer_Temp  --客户模板表
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--模板标题
	Name varchar(200) null,		--模板文件名（英文名）
	Content text null,			--模板内容
	UserID uniqueidentifier null,	--模板创建人
	Type int null,					--模板分类（方案，协议，。。。。）
	Addtime datetime null			--创建时间
)
create table CRM_CustomerProducts --方案协议_产品表
(
	id int IDENTITY(1,1) NOT NULL,
	CustomerID int not null,	--客户编号
	PID int not null,			--父编号（方案或协议编号）
	ProductID int not null,		--产品编号
	ZDFee int null,				--最低执行价格
	Type int default 0,			--类型（1方案，2协议）
	Remarks text null			--描述、要求
	[ProductName] varchar(50), 			--产品名称
	[Specification] varchar(50), 		--规格
	[Units] varchar(50), 				--单位
	[SalesPrice] decimal(18, 2), 		--销售价
	[DiscountedPrice] decimal(18, 2), 	--优惠价
	[CostPrice] decimal(18, 2), 		--成本价
	[Remark] text, 						--产品说明
	[Services] text 					--服务内容
)
create TABLE [dbo].[CRM_CustomerDept] (--产品部门表
	[ID] int IDENTITY(1,1) NOT NULL, 		-- 自动编号
	[ProductID] int null,				--产品编号
	[DeptID] int null,					--部门编号
	AgreementID int,					--协议编号
	[MonthFee]  decimal(18, 2) null, 	--月维护费用
	[MonthFeeType] int null,			--月维护费计算方式（0固定费用，1产品成本价比率,2协议执行价比率）
	[Fee]  decimal(18, 2) null, 		--制作费用
	[FeeType] int null,					--制作费用计算方式（0固定费用，1产品成本价比率,2协议执行价比率）
	Remarks text null					--部门服务内容
)
create TABLE [dbo].[WorkOrder_Orders] (--工作单表
	[ID] int IDENTITY(1,1) NOT NULL, 			-- 自动编号
	[PID] int null,				--父编号
	[DeptWorkID] int null,			--部门工作号
	UserID uniqueidentifier null,	--提交人
	AssignUserID uniqueidentifier null,	--分配人
	ExecUserID uniqueidentifier null,	--执行人
	[Remarks] text null,				--要求内容
	[Title] varchar(500) null,			--标题
	[Count] int null,					--工作量
	[Proj] int null,					--工作项目（0网上济宁，1山东频道，2OA）
	[Type] int null,					--工作分类（0开发，1维护，2其它）
	[State] int default 0,				--状态(0编辑中，1未分配，2已分配，3未接收，4已接收，5执行中，6已暂停，7待验收，8已完成，9已取消
	[YJTime] datetime default getdate(), 	--预计完成时间,任务期限
	[AddTime] datetime default getdate(), 	--创建时间
	[SubTime] datetime default getdate(), 	--提交时间
	[FPTime] datetime default getdate(), 	--分配时间
	[YSTime] datetime default getdate(), 	--待验收时间
	[StopTime] datetime default getdate(), 	--完成时间
	[IsTop] int null, 						--是否置顶（1代表置顶，其它代表不置顶）
	[TopOrder] int null, 					--置顶排序
	[TopTime] datetime null, 				--置顶时间
	[TopCount] varchar(50) null, 			--预计完成天数/小时数/分钟数
	[StateTime] datetime default getdate() null 	--状态时间
)
create TABLE [dbo].[WorkOrder_Dept] (--工作单_X_部门表
	[ID] int IDENTITY(1,1) NOT NULL, 			--自动编号
	[WID] int null,				--工作单编号
	[DeptID] int null,			--部门号
	[State] int default 0,		--状态(0编辑中，1未分配，2已分配，3未接收，4已接收，5执行中，6已暂停，7待验收，8已完成，9已取消
	[SubTime] datetime default getdate(), 	--提交时间
	[FPTime] datetime default getdate(), 	--分配时间
	[YSTime] datetime default getdate(), 	--待验收时间
	[StopTime] datetime default getdate(), 	--完成时间
	[StateTime] datetime default getdate() null 	--状态时间
)
create TABLE [dbo].[WorkOrder_Message] (--工作单_X_消息交流表
	[ID] int IDENTITY(1,1) NOT NULL, 			--自动编号
	[WID] int null,				--工作单编号
	[FromUserID] uniqueidentifier null,	--消息发送人
	[ToUserID] uniqueidentifier null,	--消息接收人
	Remarks text null,					--消息内容
	[State] int  default 0,				--消息状态（是否查看0未查看，1已查看）
	[AddTime] datetime default getdate()--消息时间
)
create TABLE [dbo].[WorkOrder_Appraisal] (--工作单_X_评价表
	[ID] int IDENTITY(1,1) NOT NULL, 				--自动编号
	[WID] int null,					--工作单编号
	[UserID] uniqueidentifier null,	--评价人
	Remarks text null,				--评价内容
	[State] int  default 0,			--评价状态（是否查看0未查看，1已查看）
	[AddTime] datetime default getdate() --评价时间
)
create TABLE [dbo].[XZ_Train] (--考核培训表
	[ID] int IDENTITY(1,1) NOT NULL, 			--自动编号
	[Title] varchar(200) null,					--标题
	[Type] int null,							--类型（1考核，2培训）
	[UserID] uniqueidentifier null,				--发布人
	RunTime datetime null,						--与会时间
	Addr varchar(500) null,						--地点
	FlowID int null,							--关联流程
	Content text null,							--详细内容
	UsersID varchar(8000) null,					--与会人员编号
	UsersName varchar(5000) null,				--与会人员姓名
	[AddTime] datetime default getdate() --创建时间
)
create TABLE [dbo].[XZ_TrainUsers] (--考核培训_X_与会人员表
	[ID] int IDENTITY(1,1) NOT NULL, 			--自动编号
	[TrainID] int null,							--培训号
	[UserID] uniqueidentifier null,				--员工号
	RETime datetime null,						--接收时间
	[State] int null,							--接收状态（0未接收，1已接收，2已参与）
	RunID int null,								--提交的流程表单
	[AddTime] datetime default getdate()		--创建时间
)

