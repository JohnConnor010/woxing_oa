<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getData.aspx.cs" Inherits="fckeditor_editor_plugins_td_data_fetch_getData" %>
<%if(Request["dataSrc"]!=null && Request["dataSrc"]=="1"){ %>
<select name="dataField" id="dataField" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="8">签章控件:sdf</option>
  <option value="1">交办人</option>
  <option value="2">承办人</option>
  <option value="3">交办日期</option>
  <option value="4">要求完成日期</option>
  <option value="">日期控件：要求完成日期</option>
  <option value="5">承办人工作内容</option>
  <option value="6">承办人工作完成情况</option>
  <option value="7">交办人意见</option>
</select>
— <input type="text" name="itemTitle" id="itemTitle" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="2") {%>
<select name="dataField" id="Select1" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">办结时间</option>
  <option value="2">主办内容及要求</option>
  <option value="3">主办单位办理情况</option>
  <option value="4">督办单位意见</option>
</select>
— <input type="text" name="itemTitle" id="Text1" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>

<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="3") {%>
<select name="dataField" id="Select2" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">收文时间</option>
  <option value="">日期控件：收文时间</option>
  <option value="2">密级</option>
  <option value="3">来文单位</option>
  <option value="4">文件名称</option>
  <option value="5">主题词</option>
  <option value="6">页码</option>
  <option value="7">摘要</option>
  <option value="8">拟办意见</option>
  <option value="9">领导批示</option>
  <option value="10">归档人</option>
</select>
— <input type="text" name="itemTitle" id="Text2" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="4") {%>
<select name="dataField" id="Select3" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">订单编号</option>
  <option value="2">客户要求</option>
  <option value="3">业务员</option>
  <option value="4">订单日期</option>
  <option value="5">完工反馈</option>
  <option value="6">反馈人</option>
  <option value="7">完工反馈日期</option>
  <option value="8">发货单号</option>
  <option value="9">收货人</option>
  <option value="10">联系电话</option>
  <option value="11">发货方式</option>
  <option value="12">公司名称</option>
  <option value="13">收货地址</option>
  <option value="14">运费结算方式</option>
  <option value="15">业务经理</option>
  <option value="16">发货日期</option>
  <option value="17">发货员发货信息</option>
  <option value="18">发货员</option>
  <option value="19">发货确认日期</option>
  <option value="20">业务确认</option>
  <option value="21">反馈业务员</option>
  <option value="22">反馈日期</option>
</select>
— <input type="text" name="itemTitle" id="Text3" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="5") {%>
<select name="dataField" id="Select4" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">提案人</option>
  <option value="2">申请日期</option>
  <option value="">日期控件：申请日期</option>
  <option value="3">申请原因</option>
  <option value="4">产品类型</option>
  <option value="5">申请内容</option>
  <option value="6">审批与处理情况</option>
  <option value="7">备注</option>
</select>
— <input type="text" name="itemTitle" id="Text4" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="6") {%>
<select name="dataField" id="Select5" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">起草单位</option>
  <option value="2">起草单位拟稿人</option>
  <option value="3">起草单位负责人</option>
  <option value="4">秘书科初审人</option>
  <option value="5">办公室核稿人</option>
  <option value="6">公文标题</option>
  <option value="7">收文字号</option>
  <option value="8">发文字号</option>
  <option value="9">主题词</option>
  <option value="10">主送机关</option>
  <option value="11">抄送机关</option>
  <option value="12">校对人</option>
  <option value="13">印制份数</option>
</select>
— <input type="text" name="itemTitle" id="Text5" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="7") {%>
<select name="dataField" id="Select6" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">列表测试</option>
  <option value="2">数值1</option>
  <option value="3">数值2</option>
  <option value="4">数值3</option>
  <option value="5">最大值</option>
  <option value="6">人民币大写</option>
  <option value="7">车辆</option>
  <option value="8">客户名称</option>
  <option value="9">当前用户的短信</option>
  <option value="10">查询某部门的用户</option>
  <option value="11">当前日期</option>
  <option value="12">当前日期中文</option>
  <option value="13">当前用户姓名</option>
  <option value="14">文号计数器</option>
  <option value="15">IP地址</option>
  <option value="">部门人员控件：选择部门人员</option>
  <option value="16">选择部门人员</option>
  <option value="17">办理日期</option>
  <option value="">日期控件：办理日期</option>
  <option value="18">选择项-是否批准</option>
  <option value="19">下拉菜单</option>
  <option value="20">签章控件：领导签名</option>
  <option value="21">签章控件：部门签字</option>
  <option value="22">日期差值计算</option>
  <option value="23">保密字段</option>
</select>
— <input type="text" name="itemTitle" id="Text6" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="8") {%>
<select name="dataField" id="Select7" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">受理日期</option>
  <option value="2">具体时间</option>
  <option value="3">受理人</option>
  <option value="4">受理方式</option>
  <option value="5">单位</option>
  <option value="6">电话</option>
  <option value="7">联系人</option>
  <option value="8">事由</option>
  <option value="9">受理情况</option>
  <option value="10">督办意见</option>
  <option value="11">协办情况</option>
</select>
— <input type="text" name="itemTitle" id="Text7" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="9") {%>
<select name="dataField" id="Select8" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">申请人</option>
  <option value="2">申请日期</option>
  <option value="3">部门</option>
  <option value="4">部门负责人</option>
  <option value="5">申领物品</option>
  <option value="6">申领说明</option>
  <option value="7">审批结果</option>
  <option value="8">审批意见</option>
</select>
— <input type="text" name="itemTitle" id="Text8" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="10") {%>
<select name="dataField" id="Select9" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">申请人</option>
  <option value="2">申请日期</option>
  <option value="3">会议时间</option>
  <option value="4">参会人数</option>
  <option value="5">会议室</option>
  <option value="6">会议内容</option>
  <option value="7">参会人员</option>
  <option value="8">会场布置及物品准备要求</option>
  <option value="9">审批结果</option>
  <option value="10">审批意见</option>
</select>
— <input type="text" name="itemTitle" id="Text9" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="11") {%>
<select name="dataField" id="Select10" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">申请人</option>
  <option value="2">申请日期</option>
  <option value="3">部门</option>
  <option value="4">车型</option>
  <option value="5">申请出发时间</option>
  <option value="6">申请返回时间</option>
  <option value="7">目的地及路线</option>
  <option value="8">申请事由</option>
  <option value="9">部门主管意见</option>
  <option value="10">管理部门意见</option>
  <option value="11">起始数</option>
  <option value="12">归来时间</option>
  <option value="13">出车费用</option>
  <option value="14">终止数</option>
  <option value="15">行驶里程</option>
</select>
— <input type="text" name="itemTitle" id="Text10" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="12") {%>
<select name="dataField" id="Select11" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">签印人</option>
  <option value="2">复印日期</option>
  <option value="3">部门</option>
  <option value="4">纸型</option>
  <option value="5">文件名称</option>
  <option value="6">文件页数</option>
  <option value="7">复印份数</option>
  <option value="8">总计页数</option>
</select>
— <input type="text" name="itemTitle" id="Text11" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>

<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="13") {%>
<select name="dataField" id="Select12" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">申请人</option>
  <option value="2">申请日期</option>
  <option value="3">部门</option>
  <option value="4">假期类别</option>
  <option value="5">请假开始时间</option>
  <option value="">日期控件：请假开始时间</option>
  <option value="6">请假结束时间</option>
  <option value="">日期控件：请假结束时间</option>
  <option value="7">扣假形式</option>
  <option value="8">请假理由</option>
  <option value="9">审批人</option>
  <option value="10">审批日期</option>
  <option value="11">审批结果</option>
  <option value="12">审批意见</option>
</select>
— <input type="text" name="itemTitle" id="Text12" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>

<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="14") {%>
<select name="dataField" id="Select13" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">申请人</option>
  <option value="2">申请日期</option>
  <option value="3">部门</option>
  <option value="4">交通工具</option>
  <option value="5">出差开始时间</option>
  <option value="">日期控件：出差开始时间</option>
  <option value="6">出差结束时间</option>
  <option value="">日期控件：出差结束时间</option>
  <option value="7">出差地点</option>
  <option value="8">出差事由</option>
  <option value="9">审批人</option>
  <option value="10">审批日期</option>
  <option value="11">审批结果</option>
  <option value="12">审批意见</option>
</select>
— <input type="text" name="itemTitle" id="Text13" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
    
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="15") {%>
<select name="dataField" id="Select14" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">登记人</option>
  <option value="2">登记日期</option>
  <option value="3">部门</option>
  <option value="4">加班地点</option>
  <option value="5">加班开始时间</option>
  <option value="">日期控件：加班开始时间</option>
  <option value="6">加班结束时间</option>
  <option value="">日期控件：加班结束时间</option>
  <option value="7">加班内容</option>
  <option value="8">证明人</option>
  <option value="9">审核人</option>
  <option value="10">审核日期</option>
  <option value="11">审核结果</option>
  <option value="12">审核意见</option>
</select>
— <input type="text" name="itemTitle" id="Text14" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>

<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="16") {%>
<select name="dataField" id="Select15" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">分类</option>
  <option value="2">拟案日期</option>
  <option value="3">姓名</option>
  <option value="4">部门</option>
  <option value="5">具体事录</option>
  <option value="6">提案内容</option>
  <option value="7">人事主管</option>
  <option value="8">单位领导</option>
  <option value="9">备注</option>
</select>
— <input type="text" name="itemTitle" id="Text15" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="17") {%>
<select name="dataField" id="Select16" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">申请人</option>
  <option value="2">部门</option>
  <option value="3">用途</option>
  <option value="4">款额</option>
  <option value="5">支付方式</option>
  <option value="6">部门主管</option>
  <option value="7">财务主管</option>
  <option value="8">总经理</option>
  <option value="9">备注</option>
</select>
— <input type="text" name="itemTitle" id="Text16" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="18") {%>
<select name="dataField" id="Select17" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">日期</option>
  <option value="2">凭证编号</option>
  <option value="3">摘要</option>
  <option value="4">借方金额</option>
  <option value="5">贷方金额</option>
  <option value="6">科目</option>
  <option value="7">领款人</option>
  <option value="8">批准人</option>
  <option value="9">备注</option>
</select>
— <input type="text" name="itemTitle" id="Text17" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="19") {%>
<select name="dataField" id="Select18" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">产品名称</option>
  <option value="2">检查日期</option>
  <option value="3">检查方法</option>
  <option value="4">检查质量标准</option>
  <option value="5">主要问题</option>
  <option value="6">处理意见</option>
  <option value="7">准备采取的对策</option>
  <option value="8">领导审核审批</option>
</select>
— <input type="text" name="itemTitle" id="Text18" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="20") {%>
<select name="dataField" id="Select19" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">项目名称</option>
  <option value="2">申请日期</option>
  <option value="3">项目概述</option>
  <option value="4">项目效益预期</option>
  <option value="5">人员需求</option>
  <option value="6">项目进度</option>
  <option value="7">项目费用预算</option>
  <option value="8">领导审核意见</option>
</select>
— <input type="text" name="itemTitle" id="Text19" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="21") {%>
<select name="dataField" id="Select20" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">日期</option>
  <option value="2">单位</option>
  <option value="3">价格</option>
  <option value="4">电话</option>
  <option value="5">联系人</option>
  <option value="6">底单传真</option>
  <option value="7">联系方式</option>
  <option value="8">客户信息</option>
  <option value="9">邮件发送序列号</option>
  <option value="10">开具发票</option>
  <option value="11">刻盘</option>
  <option value="12">装盒</option>
  <option value="13">填写EMS单据</option>
  <option value="14">邮局发货</option>
  <option value="15">备注</option>
</select>
— <input type="text" name="itemTitle" id="Text20" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="22") {%>
<select name="dataField" id="Select21" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">单位</option>
  <option value="2">姓名</option>
  <option value="3">电话</option>
  <option value="4">称谓</option>
  <option value="5">电子邮箱</option>
  <option value="6">QQ</option>
  <option value="7">MSN</option>
  <option value="8">地址</option>
  <option value="9">用户需求</option>
  <option value="10">主管意见</option>
</select>
— <input type="text" name="itemTitle" id="Text21" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}else if(Request["dataSrc"]!=null && Request["dataSrc"]=="23") {%>
<select name="dataField" id="Select22" type="SmallSelect">
  <option value="">请选择表单字段</option>
  <option value="1">登记人</option>
  <option value="2">登记日期</option>
  <option value="3">客户名称</option>
  <option value="4">联系人</option>
  <option value="5">电话</option>
  <option value="6">反馈方式</option>
  <option value="7">反馈内容</option>
  <option value="8">分析及处理</option>
  <option value="9">主管意见</option>
</select>
— <input type="text" name="itemTitle" id="Text22" size=10>
<a href="javascript:;" class="orgAdd" onclick="add();">添加</a>
<%}%>