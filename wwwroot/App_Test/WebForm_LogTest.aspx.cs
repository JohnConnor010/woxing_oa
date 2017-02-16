using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.App_Test
{
    public partial class WebForm_LogTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtUserId.Text = WX.Main.CurUser.UserID;
            }
        }
        public static void AddLog(int logTypeID, string title, string parameters)
        {
            string tableName = (string)XSql.GetValue("SELECT TableName FROM TL_LogType WHERE ID=" + logTypeID);
            string param = parameters;
            string userId = WX.Main.CurUser.UserID;
            if (string.IsNullOrEmpty(param))
            {
                param = "NULL";
            }
            else
            {
                param = String.Format("'{0}'", param);
            }
            if (!string.IsNullOrEmpty(tableName))
            {
                string cmdText = String.Format("INSERT INTO " + tableName + " (Title,UserID,LogType,LogTime,LogIP,LogParaments) VALUES ('{0}','{1}',{2},'{3}','{4}',{5})",title,userId,logTypeID,DateTime.Now,HttpContext.Current.Request.UserHostAddress,param);
                XSql.Execute(cmdText); 
            }

            //待开发
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int logType = Convert.ToInt32(this.DropDownList1.SelectedItem.Value);
            string title = this.txtTitle.Text.Trim();            
            WX.Main.AddLog(logType, title,null);
        }
    }
}