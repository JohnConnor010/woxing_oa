create table CRM_Customer_Laber  --客户模板标签表
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--标签中文名
	Name varchar(200) null,		--标签名称（英文）
	Content varchar(2000) null,	--标签内容
	Style int null,			--标签类型（变量，sql语句调用，纯内容）
	Format text null,		--标签的展示格式
	UserID uniqueidentifier null,	--标签创建人
	Type int null,			--分类（方案，协议，。。。。）
	Addtime datetime null		--创建时间
)
create table CRM_Customer_Temp  --客户模板表
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--模板标题
	Name varchar(200) null,		--模板文件名（英文名）
	Content text null,		--模板内容
	UserID uniqueidentifier null,	--模板创建人
	Type int null,			--模板分类（方案，协议，。。。。）
	Addtime datetime null		--创建时间
)