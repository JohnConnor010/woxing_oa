create table CRM_Customer_Laber  --�ͻ�ģ���ǩ��
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--��ǩ������
	Name varchar(200) null,		--��ǩ���ƣ�Ӣ�ģ�
	Content varchar(2000) null,	--��ǩ����
	Style int null,			--��ǩ���ͣ�������sql�����ã������ݣ�
	Format text null,		--��ǩ��չʾ��ʽ
	UserID uniqueidentifier null,	--��ǩ������
	Type int null,			--���ࣨ������Э�飬����������
	Addtime datetime null		--����ʱ��
)
create table CRM_Customer_Temp  --�ͻ�ģ���
(
	id int IDENTITY(1,1) NOT NULL,
	Title varchar(200) null,	--ģ�����
	Name varchar(200) null,		--ģ���ļ�����Ӣ������
	Content text null,		--ģ������
	UserID uniqueidentifier null,	--ģ�崴����
	Type int null,			--ģ����ࣨ������Э�飬����������
	Addtime datetime null		--����ʱ��
)