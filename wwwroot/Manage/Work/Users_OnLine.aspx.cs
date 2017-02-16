using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Work
{
    public partial class Users_OnLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }
        //绑定数据
        public void BindData()
        {
            string sSql = "select A.UserId,B.RealName,LoginTime,LoginIp from tu_onlineUsers A inner join TU_Users B on A.UserId=B.UserId order by LoginTime desc";
            GridView1.DataSource = ULCode.QDA.XSql.GetDataTable(sSql);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            Response.Write(this.Request.Form["checksel"]); return;
            //1.验证用户权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            ULCode.Debug.we(String.Format("已经收到id:{0}", id));
            return;

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除用户({0})成功！", id), "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                //

                ULCode.Debug.Alert(this, "删除用户成功！");
            }
            else
            {
                ULCode.Debug.Alert(this, "删除用户失败！");
            }
        }
        //批量删除处理过程
        protected void DelSel(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string idList = this.Request.Form["checksel"];
            if (String.IsNullOrEmpty(idList))
            {
                ULCode.Debug.we(String.Format("没有收到任何idList"));
                return;
            }
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            ULCode.Debug.we(String.Format("已经收到idList:{0}", idList));
            return;

            //以下是程序开发者的任务
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "删除用户信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                //

                ULCode.Debug.Alert(this, "删除用户成功！");
            }
            else
            {
                ULCode.Debug.Alert(this, "删除用户失败！");
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        public string getSendBox(object oEval,object name)
        {
            return String.Format("javascript:PopupIFrame('../Main/Add_Message.aspx?UserID={0}','向 {1} 发信息',null,null,500,200)", oEval,name);
        }
        public string getEslapseStr(object oEval)
        {
            DateTime dt = Convert.ToDateTime(oEval);
            return WX.Main.GetTimeEslapseStr(dt, null, null);
        }
        public string getIpStr(object oEval)
        {
            String str = Convert.ToString(oEval);
            if (str == "::1")
                return "VS Test Port";
            else
                return str;
        }
    }
}