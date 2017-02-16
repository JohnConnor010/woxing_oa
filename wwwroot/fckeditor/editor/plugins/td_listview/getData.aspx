<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getData.aspx.cs" Inherits="fckeditor_editor_plugins_td_listview_td_listview" %>
<%if (Request["dataSrc"] != null && Request["dataSrc"] == "CUSTOMER")
  { %>
<select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="CUSTOMER_NAME">客户名称</option>
	  <option value="CUSTOMER_CODE">客户编号</option>
	  <option value="TEL_NO">客户电话</option>
	  <option value="FAX_NO">客户传真</option>
	  <option value="CUSTOMER_WWW">客户网址</option>
	  <option value="CUSTOMER_AREA">客户地区</option>
</select>
<%}
  else if (Request["dataSrc"] != null && Request["dataSrc"] == "PRODUCT")
  {%>
  <select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="PRODUCT_CODE">产品编码</option>
	  <option value="PRODUCT_NAME">产品名称</option>
	  <option value="PRODUCT_MODE">产品类别</option>
	  <option value="PRODUCT_TYPE">产品型号</option>
	  <option value="MEASURE_UNIT">计量单位</option>
	  <option value="COST_PRICE">成本价</option>
	  <option value="STANDARD_PRICE">出售价</option>
	  <option value="PRODUCT_DESC">产品描述</option>
</select>
<%}
  else if (Request["dataSrc"] != null && Request["dataSrc"] == "CRM_ACCOUNT")
  {%>
<select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="account_name">客户名称</option>
	  <option value="account_code">客户编号</option>
	  <option value="account_phone">客户电话</option>
	  <option value="account_mobile">手机号码</option>
	  <option value="account_url">客户网址</option>
	  <option value="account_email">E-MAIL</option>
</select>
<%}
  else if (Request["dataSrc"] != null && Request["dataSrc"] == "CRM_PRODUCT")
  {%>
  <select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="product_code">产品编号</option>
	  <option value="product_name">产品名称</option>
	  <option value="product_specification">规则型号</option>
	  <option value="product_band">生产厂商</option>
	  <option value="product_place">生产地</option>
	  <option value="product_cost">成本价</option>
	  <option value="product_price">出售价</option>
	  <option value="remark">备注</option>
</select>
<%}
  else if (Request["dataSrc"] != null && Request["dataSrc"] == "OFFICE_PRODUCTS")
  {%>
  <select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="PRO_NAME">办公用品名称</option>
	  <option value="PRO_DESC">办公用品描述</option>
	  <option value="PRO_CODE">办公用品编码</option>
	  <option value="PRO_PRICE">单价</option>
	  <option value="PRO_SUPPLIER">供应商</option>
	  <option value="PRO_STOCK">当前库存</option>
	  <option value="PRO_LOWSTOCK">最低警戒库存</option>
	  <option value="PRO_MAXSTOCK">最高警戒库存</option>
</select>
<%}
  else if (Request["dataSrc"] != null && Request["dataSrc"] == "HR_STAFF_INFO")
  {%>
  <select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="USER_ID">用户名</option>
	  <option value="DEPT_ID">部门</option>
	  <option value="STAFF_NO">员工编号</option>
	  <option value="WORK_NO">员工工号</option>
	  <option value="WORK_TYPE">工种</option>
	  <option value="STAFF_NAME">员工姓名</option>
	  <option value="STAFF_E_NAME">英文名</option>
	  <option value="STAFF_CARD_NO">身份证号码</option>
	  <option value="STAFF_BIRTH">出生日期</option>
	  <option value="STAFF_AGE">年龄</option>
	  <option value="STAFF_NATIVE_PLACE">籍贯</option>
	  <option value="STAFF_DOMICILE_PLACE">户口所在地</option>
	  <option value="YES_OTHER_P">是否异地户口</option>
	  <option value="STAFF_NATIONALITY">民族</option>
	  <option value="STAFF_MARITAL_STATUS">婚姻状况</option>
	  <option value="STAFF_POLITICAL_STATUS">政治面貌</option>
	  <option value="JOIN_PARTY_TIME">入党团时间</option>
	  <option value="STAFF_PHONE">联系电话</option>
	  <option value="STAFF_MOBILE">手机号码</option>
	  <option value="STAFF_LITTLE_SMART">小灵通号码</option>
	  <option value="STAFF_EMAIL">E_MAIL</option>
	  <option value="STAFF_MSN">MSN</option>
	  <option value="STAFF_QQ">QQ</option>
	  <option value="HOME_ADDRESS">家庭地址</option>
	  <option value="OTHER_CONTACT">其它联系方式</option>
	  <option value="JOB_BEGINNING">参加工作时间</option>
	  <option value="WORK_AGE">总工龄</option>
	  <option value="STAFF_HEALTH">健康状况</option>
	  <option value="STAFF_HIGHEST_SCHOOL">最高学历</option>
	  <option value="STAFF_HIGHEST_DEGREE">最高学位</option>
	  <option value="GRADUATION_DATE">毕业时间</option>
	  <option value="GRADUATION_SCHOOL">毕业学校</option>
	  <option value="STAFF_MAJOR">专业</option>
	  <option value="COMPUTER_LEVEL">计算机水平</option>
	  <option value="FOREIGN_LANGUAGE1">外语语种1</option>
	  <option value="FOREIGN_LEVEL1">外语水平1</option>
	  <option value="FOREIGN_LANGUAGE2">外语语种2</option>
	  <option value="FOREIGN_LEVEL2">外语水平2</option>
	  <option value="FOREIGN_LANGUAGE3">外语语种3</option>
	  <option value="FOREIGN_LEVEL3">外语水平3</option>
	  <option value="STAFF_SKILLS">特长</option>
	  <option value="STAFF_OCCUPATION">员工类型</option>
	  <option value="ADMINISTRATION_LEVEL">行政等级</option>
	  <option value="JOB_POSITION">职务</option>
	  <option value="PRESENT_POSITION">职称</option>
	  <option value="DATES_EMPLOYED">入职时间</option>
	  <option value="JOB_AGE">本单位工龄</option>
	  <option value="BEGIN_SALSRY_TIME">起薪时间</option>
	  <option value="WORK_STATUS">在职状态</option>
	  <option value="STAFF_CS">合同签订时间</option>
	  <option value="STAFF_CTR">合同到期时间</option>
	  <option value="REMARK">备注</option>
	  <option value="STAFF_COMPANY">所在单位</option>
	  <option value="RESUME">简历</option>
</select>
<%}
  else if (Request["dataSrc"] != null && Request["dataSrc"] == "HR_STAFF_CONTRACT")
  {%>
  <select type="SmallSelect">
  <option value="">请选择数据库字段</option>
	  <option value="STAFF_NAME">用户ID</option>
	  <option value="STAFF_CONTRACT_NO">合同编号</option>
	  <option value="CONTRACT_TYPE">合同类型</option>
	  <option value="CONTRACT_SPECIALIZATION">合同属性</option>
	  <option value="MAKE_CONTRACT">合同签订日期</option>
	  <option value="TRAIL_EFFECTIVE_TIME">试用生效日期</option>
	  <option value="PROBATIONARY_PERIOD">试用期限</option>
	  <option value="TRAIL_OVER_TIME">试用到期日期</option>
	  <option value="PASS_OR_NOT">合同是否转正</option>
	  <option value="PROBATION_END_DATE">合同转正日期</option>
	  <option value="PROBATION_EFFECTIVE_DATE">合同生效日期</option>
	  <option value="ACTIVE_PERIOD">合同期限</option>
	  <option value="CONTRACT_END_TIME">合同到期日期</option>
	  <option value="REMARK">备注</option>
</select>
<%}%>