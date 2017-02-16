using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Data
{
    public partial class Dict:DictBase
    {
        #region //1.GetTable
        /// <summary>
        /// 表单列表，字段：ID,Name
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable_FormCatagory()
        {
            return GetDataTable("select Id,Name from FL_FormCatagory order by Sort");
        }
        /// <summary>
        /// 流程列表，字段：ID,Name
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable_FlowCatagory()
        {
            return GetDataTable("select Id,Name from FL_FlowCatagory order by Sort");
        }
        public static DataTable GetDataTable_Forms()
        {
            return GetDataTable("select Id,Name from FL_Forms order by Sort");
        }
        public static DataTable GetDataTable_Flows()
        {
            return GetDataTable("select Id,Name from FL_Flows order by Sort");
        }
        public static DataTable GetDataTable_NumberRules()
        {
            return GetDataTable("select Id,Name from FL_NumberRules");
        }
        public static DataTable GetDataTable_VarDefine()
        {
            return GetDataTable("select Id,Name from TE_VarDefine");
        }
        #endregion

        #region //2.GetListItems
        public static ListItem[] GetListItems_FormCatagory()
        {
            return GetListItems(GetDataTable_FormCatagory());
        }
        public static ListItem[] GetListItems_FlowCatagory()
        {
            return GetListItems(GetDataTable_FlowCatagory());
        }
        public static ListItem[] GetListItems_Forms()
        {
            return GetListItems(GetDataTable_Forms());
        }
        public static ListItem[] GetListItems_Flows()
        {
            return GetListItems(GetDataTable_Flows());
        }
        public static ListItem[] GetListItems_VarDefine()
        {
            return GetListItems(GetDataTable_VarDefine());
        }
        public static ListItem[] GetListItems_NumberRules()
        {
            return GetListItems(GetDataTable_NumberRules());
        }
        public static ListItem[] GetListItems_enum_LogType()
        {
            return new ListItem[] 
            { 
                new ListItem("默认", "0"), 
                new ListItem("账户", "1") 
            };
        }
        public static ListItem[] GetListItems_enum_PermissionType()
        {
            return new ListItem[] 
            { 
                new ListItem("无权限", "0"), 
                new ListItem("阅读权限", "1"), 
                new ListItem("编辑与添加", "2"), 
                new ListItem("删除与审核", "3")
            };
        }
        public static ListItem[] GetListItems_enum_AttachType()
        {
            return new ListItem[] 
            { 
                new ListItem("流程", "1")
            };
        }
        public static ListItem[] GetListItems_enum_FlowType()
        {
            return new ListItem[] 
            { 
                new ListItem("固定流程", "1"), 
                new ListItem("自由流程", "2")
            };
        }
        public static ListItem[] GetListItems_enum_FlowAuthorizeMode()
        {
            return new ListItem[] 
            { 
                new ListItem("禁止委托", "0"), 
                new ListItem("自由委托", "1"), 
                new ListItem("按步骤设置的经办权限委托", "2"), 
                new ListItem("仅允许委托当前步骤经办人", "3")
            };
        }
        public static ListItem[] GetListItems_enum_ManageType()
        {
            return new ListItem[] 
            { 
                new ListItem("管理", "1"), 
                new ListItem("监控", "2"), 
                new ListItem("查询", "3"), 
                new ListItem("编辑", "4"), 
                new ListItem("点评", "5")
            };
        }
        public static ListItem[] GetListItems_enum_ManageScope()
        {
            return new ListItem[] 
            { 
                new ListItem("本机构", "1"), 
                new ListItem("本部门", "2"), 
                new ListItem("所有部门", "3"), 
                new ListItem("自定义部门", "4")
            };
        }
        public static ListItem[] GetListItems_enum_RemindType()
        {
            return new ListItem[] 
            { 
                new ListItem("仅此一次", "1"), 
                new ListItem("每天一次", "2"), 
                new ListItem("每周一次", "3"), 
                new ListItem("每月一次", "4"),
                new ListItem("每年一次", "5")
            };
        }
        public static ListItem[] GetListItems_enum_NodeType()
        {
            return new ListItem[] 
            { 
                new ListItem("步骤节点", "1"), 
                new ListItem("子节点", "2"), 
                new ListItem("外部流程链接节点", "3")
            };
        }
        public static ListItem[] GetListItems_enum_AutoSelOpType()
        {
            return new ListItem[] 
            { 
                new ListItem("不进行自动选择", "0"), 
                new ListItem("自动选择流程发起人", "1"), 
                new ListItem("自动选择本部门主管", "2"), 
                new ListItem("自动选择本部门助理", "3"), 
                new ListItem("自动选择上级部门主管领导", "4"), 
                new ListItem("自动选择上级部门分管领导", "5"), 
                new ListItem("自动选择一级部门主管", "6"), 
                new ListItem("指定自动选择默认人员", "7"), 
                new ListItem("按表单字段选择", "8"), 
                new ListItem("自动选择指定步骤主办人", "9"),
                new ListItem("自动选择本部门符合条件的所有人员", "10"),
                new ListItem("自动选择一级部门内符合条件的所有人员", "11")
            };
        }
        public static ListItem[] GetListItems_enum_AutoSelOpFilter()
        {
            return new ListItem[] 
            { 
                new ListItem("允许选择全部的经办人", "1"), 
                new ListItem("允许选择本部门的经办人", "2"), 
                new ListItem("允许选择上级部门的经办人", "3"), 
                new ListItem("允许选择下级部门的经办人", "4")
            };
        }
        public static ListItem[] GetListItems_enum_AutoOpMode()
        {
            return new ListItem[] 
            { 
                new ListItem("无主办人会签", "0"), 
                new ListItem("明确指定主办人", "1"), 
                new ListItem("先接收者为主办人", "2")
            };
        }
        public static ListItem[] GetListItems_enum_SignMode()
        {
            return new ListItem[] 
            { 
                new ListItem("禁止会签", "0"), 
                new ListItem("允许会签", "1"), 
                new ListItem("强制会签", "2")
            };
        }
        public static ListItem[] GetListItems_enum_SignLookMode()
        {
            return new ListItem[] 
            { 
                new ListItem("总是可见", "1"), 
                new ListItem("针对本步骤之间不可见", "2"), 
                new ListItem("针对其它步骤不可见", "3")
            };
        }
        public static ListItem[] GetListItems_enum_RollBackMode()
        {
            return new ListItem[] 
            { 
                new ListItem("禁止回退", "0"), 
                new ListItem("允许回退一步", "1"), 
                new ListItem("允许回退到之前", "2")
            };
        }
        public static ListItem[] GetListItems_enum_Sync_DealMode()
        {
            return new ListItem[] 
            { 
                new ListItem("不允许并发", "0"), 
                new ListItem("允许并发", "1"), 
                new ListItem("强制并发", "2")
            };
        }
        public static ListItem[] GetListItems_enum_Sync_CombineMode()
        {
            return new ListItem[] 
            { 
                new ListItem("非强制合并", "0"), 
                new ListItem("强制合并", "1") 
            };
        }
        public static ListItem[] GetListItems_enum_NumberAutoMode()
        {
            return new ListItem[] 
            { 
                new ListItem("永远自动编号", "0"), 
                new ListItem("每年一次", "1"), 
                new ListItem("每月一次", "2"), 
                new ListItem("每日一次", "3") 
            };
        }
        public static ListItem[] GetListItems_enum_NumberUserMode()
        {
            return new ListItem[] 
            { 
                new ListItem("不允许修改", "0"), 
                new ListItem("允许修改", "1"), 
                new ListItem("仅允许添加前缀", "2"), 
                new ListItem("仅允许添加后缀", "3"),
                new ListItem("仅允许添加前后缀", "4")
            };
        }
        public static ListItem[] GetListItems_enum_VarType()
        {
            return new ListItem[] 
            { 
                new ListItem("纯文本值", "0"), 
                new ListItem("格式化字符串", "1"), 
                new ListItem("OA环境变量", "2"), 
                new ListItem("Sql获取单值", "3"),
                new ListItem("Sql获取列表", "4"),
                new ListItem("插件获取值", "5"),
                new ListItem("插件获取列表", "6"),
            };
        }
        public static ListItem[] GetListItem_enum_DealFlag()
        {
            return new ListItem[] 
            { 
                new ListItem("未接收", "0"), 
                new ListItem("办理中", "1"), 
                new ListItem("已办理", "2"), 
                new ListItem("已办结", "3"),
                new ListItem("已挂起", "5"),
            };
        }
        public static string GetItemTextByValue(ListItem[] items,string value)
        {
            return items.SingleOrDefault(item => item.Value == value).Text;
        }

        #endregion

        #region //3.BindListCtrl
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_FormCatagory(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_FormCatagory(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_FlowCatagory(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_FlowCatagory(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_Forms(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Forms(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_Flows(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Flows(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_VarDefine(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_VarDefine(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_NumberRules(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_NumberRules(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_LogType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_LogType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_PermissionType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_PermissionType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_AttachType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_AttachType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_FlowType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_FlowType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_FlowAuthorizeMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_FlowAuthorizeMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_ManageType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_ManageType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_ManageScope(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_ManageScope(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_RemindType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_RemindType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_NodeType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_NodeType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_AutoSelOpType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_AutoSelOpType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_AutoSelOpFilter(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_AutoSelOpFilter(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_AutoOpMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_AutoOpMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_SignMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_SignMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_SignLookMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_SignLookMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_RollBackMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_RollBackMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_Sync_DealMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_Sync_DealMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_Sync_CombineMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_Sync_CombineMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_NumberAutoMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_NumberAutoMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_NumberUserMode(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_NumberUserMode(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_VarType(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_enum_VarType(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_enum_DealFlag(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItem_enum_DealFlag(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
    }
}
