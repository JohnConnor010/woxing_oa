/*表单类型*/
CREATE TABLE [dbo].[FL_FormCatagory](
	[Id] [smallint] IDENTITY(11,1) NOT NULL, --目录编号
	[Name] [nvarchar](50) NOT NULL,          --目录名称
	[ParentId] [smallint] NULL,              --父目录编号
	[Sort] [smallint] NULL,                  --排序
) ON [PRIMARY]
/*表单*/
CREATE TABLE [dbo].[FL_Forms](
	[Id] [smallint] IDENTITY(11,1) NULL,     --表单编号
	[CatagoryId] [smallint] NULL,            --表单目录
	[Name] [nvarchar](50) NULL,              --表单名称
	[Module] [nvarchar](max) NULL,           --完整模板
    [Module_Short] [nvarchar](max) NULL,     --简易模板
	[Script] [nvarchar](max) NULL,           --脚本
	[Css] [nvarchar](max) NULL,              --样式
	[Items] [nvarchar](500) NULL,            --字段列表,添加与修改提交时生成
	[Sort] [smallint] NULL,                  --排序
	[DepartmentId] [int] NULL default(0)     --部门编号
) ON [PRIMARY]
/*流程类型*/
CREATE TABLE [dbo].[FL_FlowCatagory](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,  --目录编号
	[Name] [nvarchar](50) NOT NULL,          --目录名称
	[ParentId] [smallint] NULL,              --父目录编号
	[Sort] [smallint] NULL,                  --排序
) ON [PRIMARY]
/*流程*/
CREATE TABLE [dbo].[FL_Flows](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,  --流程编号
	[CatagoryId] [smallint] NULL,            --流程目录
	[Type] [tinyint] NULL,                   --流程类型（固定流程,自动流程）
	[FormId] [smallint] NULL,                --表单
 	[Name] [nvarchar](50) NULL,              --名称
	[AllowAttach] [bit] NULL,                --是否允许附件
	[Sort] [smallint] NULL,                  --排序
	[Description] [nvarchar](300) NULL,      --描述
	[AuthorizeMode] [tinyint] NULL,          --委托模式（自由委托,按步骤设置的经办权限委托,仅允许委托当前步骤经办人,禁止委托）
	[ExtendFields] [nvarchar](300) NULL,     --扩展字段
	[AllowView] [bit] NULL,                  --允许传阅
	[ViewPriv] [nvarchar](max) NULL,         --传阅人员
	[NumberRuleId] [smallint] NULL,          --流水号规则(<-FL_NumberRules)
) ON [PRIMARY]
/*流程委托*/
CREATE TABLE [dbo].[FL_FlowAuthorization](
	[Id] [int] IDENTITY(1,1) NOT NULL,       --委托编号
	[FlowId] [smallint] NULL,                --流程编号
	[FromUserId] [uniqueidentifier] NULL,    --委托人
	[ToUserId] [uniqueidentifier] NULL,      --被委托人
	[BeginDate] [smalldatetime] NULL,        --开始日期
	[EndDate] [smalldatetime] NULL,          --结束日期
	[Status] [bit] NULL                      --是否为委托状态
) ON [PRIMARY]
/*流程管理*/
CREATE TABLE [dbo].[FL_FlowManage](
	[Id] [smallint] NOT NULL,                --管理编号
	[FlowId] [smallint] NOT NULL,            --流程编号
	[ManageType] [tinyint] NOT NULL,         --管理类型(管理,监控,查询,编号,点评)
	[Scope] [tinyint] NOT NULL,              --范围(本机构,本部门,所有部门,自定义部门)
	[UserList] [nvarchar](max) NULL,         --用户列表
	[DeptList] [nvarchar](max) NULL,         --部门列表
	[DutyList] [nvarchar](max) NULL,         --职务列表
) ON [PRIMARY]
/*流程提醒*/
CREATE TABLE [dbo].[FL_FlowTimer](
	[Id] [int] IDENTITY(1,1) NOT NULL,       --提醒编号
	[FlowId] [int] NOT NULL,                 --流程编号
	[UserList] [nvarchar](max) NOT NULL,     --用户列表
	[RemindType] [tinyint] NOT NULL,         --提醒类型(只允许一次，每日一次，每周一次，每月一次，每年一次)
	[RemindTime] [datetime] NOT NULL,        --提醒时间
	[LastTime] [datetime] NOT NULL,          --最后提醒时间
) ON [PRIMARY]
/*流程步骤*/
CREATE TABLE [dbo].[FL_Process](
	[Id] [int] NULL,                         --步骤编号
	[FlowId] [smallint] NULL,                --流程编号
	[StepNo] [tinyint] NULL,                 --步骤顺序编号
	[Name] [nvarchar](50) NULL,              --名称
	[NodeType] [tinyint] NULL,               --节点类型(步骤节点、子流程节点、外部流程链接节点)
	[Next_Nodes] [nvarchar](100) NULL,       --下一节点顺序编号
	[Priv_UserList] [nvarchar](max) NULL,    --经办人用户列表
	[Priv_DutyList] [nvarchar](max) NULL,    --经办人职务列表
	[Priv_DeptList] [nvarchar](max) NULL,    --经办人部门列表
	[Fields_Editable] [nvarchar](max) NULL,  --可编辑字段列表
	[Fields_Hidden] [nvarchar](max) NULL,    --隐藏字段列表
	[VML_Top] [smallint] NULL,               --VML图上边距
	[VML_Left] [smallint] NULL,              --VML图左边距
	[Condition_In] [nvarchar](max) NULL,     --进入条件
	[Condition_Out] [nvarchar](max) NULL,    --退出条件
	[Plug_In] [nvarchar](max) NULL,          --进入插件
	[Plug_Out] [nvarchar](max) NULL,         --退出插件
	[Auto_Type] [tinyint] NULL,              --智能选人类型
											 --0.不进行自动选择
											 --1.自动选择流程发起人
											 --2.自动选择本部门主管
											 --3.自动选择本部门助理
											 --4.自动选择上级主管领导
											 --5.自动选择上级分管领导
											 --6.自动选择一级部门主管
											 --7.指定自动选择默认人员
											 --8.按表单字段选择
											 --9.自动选择指定步骤主办人
											 --10.自动选择本部门内符合条件所有人员
											 --11.自动选择本一级部门内符合条件所有人员
	[Auto_UserList] [nvarchar](max) NULL,    --经办人列表
	[Auto_UserOP] [nvarchar](50) NULL,       --主办人列表
	[Auto_FilterMode] [tinyint] NULL,        --智能过滤人选项
	                                         --0.允许选择全部的经办人
	                                         --1.允许选择本部门的经办人
	                                         --2.允许选择上级部门的经办人
	                                         --3.允许选择下级部门的经办人	                                         	                                         	                                         
	                                         --4.允许选择本角色的经办人	                                         
	[Auto_OPMode] [tinyint] NULL,            --主办人选项
	                                         --0.明确指定主办人
	                                         --1.无主办人会签
	                                         --2.先接收者为主办人
	[Auto_OpChangeMode] [tinyint] NULL,      --是否允许修改主办人相关选项及默认经办人(不允许,允许)
	[Auto_BaseUnit] [tinyint] NULL,          --第几步
	[Sign_Look] [tinyint] NULL,              --会签可见性(总是可见，针对本步骤之间不可见，针对其它步骤不可见)
	[Sign_Mode] [tinyint] NULL,              --会签模式(允许会签，禁止会签，强制会签)
	[Pass_OpForce] [tinyint] NULL,           --主办人可强制转交（经办人未办理完毕时是否允许主办人强制转交）
	[Pass_RollBack] [tinyint] NULL,          --回退(不允许，允许退到上一步，允许退到之前步骤)
	[Msg_ViewMode] [tinyint] NULL,           --提醒设置
	[Msg_ViewUsers] [tinyint] NULL,          --提醒用户群	
	[Sync_DealMode] [tinyint] NULL,          --并发选项（禁止,允许,强制）
	[Sync_CombineMode] [tinyint] NULL,        --并发合并选项（强制合并,非强制合并）
	IsBeginUser  int null default 0,		--是否允许申请人本人审核（0否，1是）
	UpdateTable varchar(500) null,			--状态改变表名
	UpdateKeyValue varchar(2000) null		--状态改变键值
) ON [PRIMARY]
/*添加工作任务*/
CREATE TABLE [dbo].[FL_Run](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --工作任务编号
	[ParentId] [bigint] NULL,                --父编号
	[Name] [nvarchar](50) NULL,              --名称
	[FlowId] [smallint] NULL,                --流程编号
	[BeginUser] [uniqueidentifier] NULL,     --发起工作的用户
	[FromUser] [uniqueidentifier] NULL,      --起因用户(?)	
	[BeginTime] [datetime] NULL,             --开始时间
	[EndTime] [datetime] NULL,               --结束时间
	[Attach_IdList] [nvarchar](100) NULL,    --附件列表
	[Attach_NameList] [nvarchar](200) NULL,  --附件名称列表
	[ForusUsers] [nvarchar](200) NULL,       --关注用户
	[ViewUsers] [nvarchar](200) NULL,        --查看过的用户
	[Archive] tinyint NULL,                  --存档
	[Del_Flag] [bit] NULL default(0),        --删除标志
	[Deal_Flag] int null,					--状态
	[StepNo] int null,						--当前步骤
	[AIP_Files] [nvarchar](max) NULL,        --(?)
) ON [PRIMARY]
/*工作日志*/
CREATE TABLE [dbo].[FL_RunLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --日志编号
	[RunId] [bigint] NULL,                   --工作编号
	[Name] [nvarchar](50) NULL,              --工作名称
	[FlowId] [smallint] NULL,                --流程编号
	[StepNo] [tinyint] NULL,                 --步骤顺序编号
	[UserId] [uniqueidentifier] NULL,        --用户
	[Time] [datetime] NULL,                  --时间
	[IP] [varchar](20) NULL,                 --IP
	[Type] [tinyint] NULL,                   --类型(?)
	[Content] [nvarchar](100) NULL,          --内容
) ON [PRIMARY]
/*工作签办*/
CREATE TABLE [dbo].[FL_RunFeedBack](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --签办编号
	[RunId] [bigint] NULL,                   --工作编号
	[StepNO] [tinyint] NULL,                 --步骤顺序编号
	[UserId] [uniqueidentifier] NULL,        --用户
	[Content] [nvarchar](200) NULL,          --内容
	[Attach_IdList] [varchar](100) NULL,     --附件列表
	[Attach_NameList] [varchar](100) NULL,   --附件名称列表
	[EditTime] [datetime] NULL,              --编辑时间
	[FeedFlag] [tinyint] NULL,               --签办标志(?)
	[SignData] [nvarchar](max) NULL,         --手写签办数据
	[ReplyId] [bigint] NULL,                 --回复编号
) ON [PRIMARY]
/*工作数据*/
CREATE TABLE [dbo].[FL_RunDatas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --工作表单数据编号
	[RunId] [bigint] NULL,                   --工作编号
	[FormId] [bigint] NULL,                  --工作编号
	[Name] [nvarchar](100) NULL,             --名称
	[BeginUserId] [uniqueidentifier] NULL,   --发起用户
	[BeginTime] [datetime] NULL,             --时间
	[Datas] [nvarchar](max) NULL,            --数据
) ON [PRIMARY]
/*工作附件*/
CREATE TABLE [dbo].[FL_RunAttachs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --附件编号
	[RunId] [bigint] NULL,                   --工作编号
	[StepNo] [tinyint] NULL,                 --流程顺序编号
	[NewFileName] [varchar](100) NULL,       --新文件名
	[OldFileName] [nvarchar](100) NULL,      --原文件名
	[UploadUserID] [uniqueidentifier] NULL,  --上传用户
	[UploadTime] [datetime] NULL,            --上传时间
	[UploadIP] [varchar](20) NULL,           --上传时间
) ON [PRIMARY]
/*工作步骤详细*/
CREATE TABLE [dbo].[FL_RunProcess](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,    --工作步骤编号
	[RunId] [bigint] NULL,                   --工作编号
	[StepNo] [tinyint] NULL,                 --步骤顺序编号
	[ParentNo] [tinyint] NULL,               --父顺序编号
	[UserId] [uniqueidentifier] NULL,        --用户
	[WorkTime] [datetime] NULL,              --工作时间
	[DeliverTime] [datetime] NULL,           --转交时间
	[Deal_Flag] [tinyint] NULL,              --处理标志
	[OP_Flag] [tinyint] NULL,                --操作标志
	[TimeOutFlag] [tinyint] NULL,            --超时标志
	[OtherUsers] [nvarchar](100) NULL,       --其它用户
	[FromUsers] [nvarchar](100) NULL,        --来自用户
	[CreateTime] [datetime] NULL default(GetDate()),
	                                         --时间
	[ActiveTime] [datetime] NULL,            --存档时间
	[Comment] [nvarchar](100) NULL,          --说明
) ON [PRIMARY]
