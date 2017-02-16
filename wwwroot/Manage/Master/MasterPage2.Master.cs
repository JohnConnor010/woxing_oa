using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.Manage
{
    public partial class MasterPage2 : System.Web.UI.MasterPage
    {
        //本功能编号，用于权限
        public int FuncID = 0;
        public int PermissionNum = 0;
        private bool _a_read = false;
        public bool A_Read
        {
            get { return _a_read; }
            set { _a_read = value; }
        }
        private bool _a_del = false;
        public bool A_Del
        {
            get { return _a_del; }
            set { _a_del = value; }
        }
        private bool _a_edit = false;
        public bool A_Edit
        {
            get { return _a_edit; }
            set { _a_edit = value; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            this.PermissionNum = WX.Main.GetPermission(false);

            //this.FuncID = WX.Main.GetFuncID();
           
            
            if (this.PermissionNum == -1)
            {
                Response.Write("此功能没有打开！");
                Response.End();
                return;
            }
            else if (this.PermissionNum == -2)
            {
                Response.Write("对不起，您没有权限访问此模块请联系管理员授权并升级，电话：0531-");
                Response.End();
                return;
            }/*
            this.A_Read = this.PermissionNum >= Convert.ToInt32(PermissionType.Read);
            this.A_Edit = this.PermissionNum >= Convert.ToInt32(PermissionType.Edit);
            this.A_Del = this.PermissionNum >= Convert.ToInt32(PermissionType.Del);
            //1.验证当前用户页面权限
            if (!A_Read)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.验证常用变量
            */
            this.A_Read = true;
            this.A_Edit = true;
            this.A_Del = true;
        }
    }
}