CREATE TABLE [dbo].[TM_Messages](
	[ID] [uniqueidentifier] NOT NULL,           /*Msg 编号*/
	[Title] [nvarchar](200) NULL,               /*Msg 标题*/
	[RedirectToUrl] [varchar](200) NULL,        /*功能指向页面*/
	[SendToUserId] [uniqueidentifier] NOT NULL, /*发给某个用户的id*/
	[FromUserId] [uniqueidentifier] NOT NULL,   /*发送者*/
	[SendTime] [datetime] NULL,                 /*发送时间*/
	[Type] [smallint] NULL,                     /*发送类型*/
	[State] [tinyint] NOT NULL,                 /*信息状态，0-未发送，1-已发送*/
	[Role] int not null							/*发送人角色，0-系统，1-具体某人*/
 CONSTRAINT [PK_TM_Messages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
USE [WXOA]
GO

/****** Object:  Table [dbo].[TU_OnlineUsers]    Script Date: 07/03/2012 16:34:18 ******/

CREATE TABLE [dbo].[TU_OnlineUsers](
	[LoginID] [varchar](50) NOT NULL,          /*用来区别同一用户打开多个窗口的字符串*/
	[UserID] [uniqueidentifier] NOT NULL,      /*用户的编号*/
	[LoginTime] [datetime] NULL,               /*登录时间*/
	[LoginOffTime] [datetime] NULL,            /*登出时间*/
	[LoginIP] [varchar](20) NULL,              /*登录IP*/
	[LastUpdateTime] [datetime] NULL,          /*最后一次更新时间，是判断在线状态的重要字段*/
 CONSTRAINT [PK_TU_OnlineUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
