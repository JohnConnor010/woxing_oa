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
        /// 部门列表，
        /// 1.allFields=false时 字段：id,name
        /// 2.allFields=true时  字段：node_id，node_name，node_level,node_path,CompanyId,ID,ParentID,Name,Tel,Fax,Address,UserID,Sort,Content,
        ///</summary>
        /// <param name="allFields"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static DataTable GetDataTable_DeptList(bool allFields, bool top)
        {
            return GetDataTable("exec " + (allFields ? "sp_get_tree_multi_table" : "sp_get_tree_table") + " 'TE_Departments','Id','Name','ParentId','Sort','0'," + (top ? "1" : "0") + ",3");
        }
        public static DataTable GetDataTable_GradeList(bool allFields, bool top)
        {
            return GetDataTable("select Sort,cast(Name as varchar(50))+'('+cast([Wage]+[Wage_Jobs]+[Allowance_Hous]+[Allowance_Traffic]+[Allowance_Repast]+[Allowance_Newsletter]+[Wage_Score]+[Allowance_SpecialPost]+[MonthBonus] as varchar(50))+')' from [HR_Grade] where Sort>0 order by Sort asc");
        }
        /// <summary>
        /// 公司列表，字段：ID,Name
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable_Companys()
        {
            return GetDataTable("select ID,Name from TE_Companys order by ID");
        }
        /// <summary>
        /// 职务列表，字段：ID,Name
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable_Duties()
        {
            return GetDataTable("select ID,Name from TE_Duties where CompanyID=11 order by ID");
        }
        /// <summary>
        /// 功能列表，
        /// 1.allFields=false时 字段：id,name
        /// 2.allFields=true时  字段：node_id，node_name，node_level,node_path,ID ，ParentID Name，State Title ，Url，Htmls,Urls，Icon,Degree,OrderID
        ///</summary>
        /// <param name="allFields"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static DataTable GetDataTable_FuncList(bool allFields, bool top)
        {
            return GetDataTable("exec " + (allFields ? "sp_get_tree_multi_table" : "sp_get_tree_table") + " 'TE_Functions','Id','Name','ParentId','OrderId','0'," + (top ? "1" : "0") + ",4");
        }
        public static DataTable GetDataTable_NotifyCategoryList(bool allFields, bool top)
        {
            return GetDataTable("select * from XZ_NotifyCategory");
        }
        /// <summary>
        /// 功能列表，
        /// 1.allFields=false时 字段：id,name
        /// 2.allFields=true时  字段：node_id，node_name，node_level,node_path,ID ，ParentID Name，State Title ，Url，Htmls,Urls，Icon,Degree,OrderID
        ///</summary>
        /// <param name="allFields"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static DataTable GetDataTable_MenuList(bool allFields, bool top)
        {
            return GetDataTable("exec " + (allFields ? "sp_get_tree_multi_table" : "sp_get_tree_table") + " 'TE_Menus','Id','Name','ParentId','OrderId','0'," + (top ? "1" : "0") + ",4");
        }
        public static DataTable GetDataTable_DutyGrade()
        {
            return GetDataTable("select Sort,Name from HR_Grade order by Sort");
        }
        #endregion

        #region //2.GetListItems
        public static ListItem[] GetListItems_DeptList(bool top)
        {
            return GetListItems(GetDataTable_DeptList(false, top));
        }
        public static ListItem[] GetListItems_GradeList(bool top)
        {
            return GetListItems(GetDataTable_GradeList(false, top));
        }
        public static ListItem[] GetListItems_Companys()
        {
            return GetListItems(GetDataTable_Companys());
        }
        public static ListItem[] GetListItems_Duties()
        {
            return GetListItems(GetDataTable_Duties());
        }
        public static ListItem[] GetListItems_FuncList(bool top)
        {
            return GetListItems(GetDataTable_FuncList(false, top));
        }
        public static ListItem[] GetListItems_MenuList(bool top)
        {
            return GetListItems(GetDataTable_MenuList(false, top));
        }
        public static ListItem[] GetListItems_NotifyCategoryList(bool top)
        {
            return GetListItems(GetDataTable_NotifyCategoryList(false, top));
        }
        public static ListItem[] GetListItems_DutyGrade()
        {
            return GetListItems(GetDataTable_DutyGrade());
        }
        #endregion

        #region //3.BindListCtrl
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_DeptList(object listCtrl, String textFormat, string defaultValue, string SelectedValue)
        {
            bool top = String.IsNullOrEmpty(defaultValue);
            BindListCtrl(GetListItems_DeptList(top), listCtrl, textFormat, defaultValue, SelectedValue);
        } /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_GradeList(object listCtrl, String textFormat, string defaultValue, string SelectedValue)
        {
            bool top = String.IsNullOrEmpty(defaultValue);
            BindListCtrl(GetListItems_GradeList(top), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_Companys(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Companys(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_Duties(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Duties(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_FuncList(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            bool top = String.IsNullOrEmpty(defaultValue);
            BindListCtrl(GetListItems_FuncList(top), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_MenuList(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            bool top = String.IsNullOrEmpty(defaultValue);
            BindListCtrl(GetListItems_MenuList(top), listCtrl, textFormat, defaultValue, SelectedValue);
        } 
        /// <summary>
        /// 直接填充到列表控件
        /// </summary>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl_NotifyCategoryList(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            bool top = String.IsNullOrEmpty(defaultValue);
            BindListCtrl(GetListItems_NotifyCategoryList(top), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion

        #region //4.职务分类
        public static DataTable GetDataTable_DutyCatagory()
        {
            return GetDataTable("select ID,Name from TE_DutyCatagory order by Sort");
        }
        public static ListItem[] GetListItems_DutyCatagory()
        {
            return GetListItems(GetDataTable_DutyCatagory());
        }
        public static void BindListCtrl_DutyCatagory(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_DutyCatagory(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl_DutyGrade(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_DutyGrade(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
    }
}
