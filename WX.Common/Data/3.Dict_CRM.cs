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
        #region //1.客户内部分类
        public static DataTable GetDataTable_InnerCategory()
        {
            return GetDataTable("select ID,CategoryName from CRM_InnerCategory");
        }
        public static ListItem[] GetListItems_InnerCategory()
        {
            return GetListItems(GetDataTable_InnerCategory());
        }
        public static void BindListCtrl_InnerCategory(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_InnerCategory(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //2.客户企业性质
        public static DataTable GetDataTable_CompanyNature()
        {
            return GetDataTable("select ID,CompanyNature from CRM_CompanyNature");
        }
        public static ListItem[] GetListItems_CompanyNature()
        {
            return GetListItems(GetDataTable_CompanyNature());
        }
        public static void BindListCtrl_CompanyNature(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_CompanyNature(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //3.客户行业分类
        public static DataTable GetDataTable_Industry()
        {
            return GetDataTable("select ID,IndustryName from CRM_Industry");
        }
        public static ListItem[] GetListItems_Industry()
        {
            return GetListItems(GetDataTable_Industry());
        }
        public static void BindListCtrl_Industry(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Industry(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //4.客户业务分类
        public static DataTable GetDataTable_BusinessLevel()
        {
            return GetDataTable("select ID,LevelName from CRM_BusinessLevel");
        }
        public static ListItem[] GetListItems_BusinessLevel()
        {
            return GetListItems(GetDataTable_BusinessLevel());
        }
        public static void BindListCtrl_BusinessLevel(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_BusinessLevel(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //5.客户来源
        public static DataTable GetDataTable_Source()
        {
            return GetDataTable("select ID,SourceName from CRM_Source");
        }
        public static ListItem[] GetListItems_Source()
        {
            return GetListItems(GetDataTable_Source());
        }
        public static void BindListCtrl_Source(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Source(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //6.客户跟踪
        public static DataTable GetDataTable_Stage()
        {
            return GetDataTable("select ID,StageName from CRM_Stage order By ID");
        }
        public static ListItem[] GetListItems_Stage()
        {
            return GetListItems(GetDataTable_Stage());
        }
        public static void BindListCtrl_Stage(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Stage(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //7.客户忠诚度
        public static DataTable GetDataTable_BuyHabbit()
        {
            return GetDataTable("select * from CRM_BuyHabbit Order By ID");
        }
        public static ListItem[] GetListItems_BuyHabbit()
        {
            return GetListItems(GetDataTable_BuyHabbit());
        }
        public static void BindListCtrl_BuyHabbit(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_BuyHabbit(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        
    }
}
