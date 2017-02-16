using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;

namespace wwwroot.App_Demo
{
    public partial class WebForm_Permissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                this.FillDutyType();
            }
        }
        private void FillDutyType()
        {
            this.DutyType.Items.AddRange(new ListItem[] {
                  new ListItem("--请选择--",""),
                  new ListItem("总经理","101"),
                  new ListItem("行政主管","102"),
                  new ListItem("财务主管","103"),
                  new ListItem("技术主管","104")
            });
        }
        protected void SubmitData(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            int id = Convert.ToInt32(this.DutyID.Text);
            string name = this.DutyName.Text;
            string dutyType = this.DutyType.SelectedValue;

            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            ULCode.Debug.we(String.Format("已经收到<br/>id:{0}<br/>name:{1}<br/>type:{2}", id, name, dutyType));

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
                WX.Main.AddLog(LogType.Default, "添加用户信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Alert("添加用户成功！");
            }
            else
            {
                ULCode.Debug.Alert("添加用户失败！");
            }
        }
    }
}