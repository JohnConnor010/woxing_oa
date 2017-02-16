using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Main
{
    public partial class showmessage : System.Web.UI.Page
    {
        public string sid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            //get sid
            if (!ULCode.Validation.IsGuid(Request.QueryString["Id"]))
            {
                Response.Write("你无权访问此页");
                return;
            }
            sid = Convert.ToString(Request.QueryString["Id"]);
            //display msg
            string fromuserid = "", senduserid = "";
            WX.Model.HistoryMessages.MODEL hismodel = WX.Model.HistoryMessages.GetModel("SELECT * FROM TM_HistoryMessages where ID='" + sid + "'");
            if (hismodel != null)
            {   //如果是查看历史记录
                if (hismodel.SendToUserId.value.ToString() != WX.Authentication.GetUserID())
                {
                    lab_typename.Text = "对不起您无权查看此信息！";
                    return;
                }
                lab_typename.Text = GetTypeName(hismodel.Type.ToString());
                //lab_from.Text =WX.CommonUtils.GetRealNameListByUserIdList(hismodel.FromUserId.ToString());
                //lab_time.Text = hismodel.SendTime.ToString();
                //lab_title.Text = hismodel.Title.ToString();

                //sid = hismodel.ID.value.ToString();
                //更新历史记录状态(正常运行后，下面两句可删除)
                hismodel.State.set(1);
                hismodel.Update();
                fromuserid = hismodel.FromUserId.ToString();
                senduserid = hismodel.SendToUserId.ToString();
            }
            else
            {   //查看即时记录
                WX.Model.Message.MODEL model = WX.Model.Message.GetModel("SELECT * FROM TM_Messages where ID='" + sid + "'");

                if (model.SendToUserId.value.ToString() != WX.Authentication.GetUserID())
                {
                    lab_typename.Text = "对不起您无权查看此信息！";
                    return;
                }
                lab_typename.Text = GetTypeName(model.Type.ToString());
                //lab_from.Text = model.FromUserId.ToString();
                //lab_time.Text = model.SendTime.ToString();
                //lab_title.Text = model.Title.ToString();

                fromuserid = model.FromUserId.ToString();
                senduserid = model.SendToUserId.ToString();
                //WX.Main.ExecuteDelete("TM_Messages", "ID", Request["id"]);
                //sid = model.ID.value.ToString();
                //更新即时记录状态
                //model.State.set(1);
                //model.Update();
                //看完后直接转到历史记录表中
                try
                {
                    WX.Main.MessageToHistory("'"+sid+"'");
                   
                    model = null;
                }
                catch
                {
                }
            }
            if (senduserid == WX.Authentication.GetUserID())
            {
                senddiv.Visible = true;
            }

            this.dataBind(fromuserid, senduserid);
            }
        }
        private string GetTypeName(string type)
        {
            switch (type)
            {
                case "1": return "我的消息";
                case "2": return "审核信息"; 
                case "3": return "催办信息"; 
                case "4": return "提醒信息"; 
                case "5": return "公告信息"; 
                case "6": return "考核培训信息";
                case "7": return "面试通知";
                case "8": return "入职通知"; 
                case "9": return "转正申请";
                case "10": return "调岗申请";
                case "11": return "离职申请"; 
                default: return "我的消息"; 
            }
        }
        private void dataBind(string fromid,string sendid)
        {

            System.Data.DataTable dlist = ULCode.QDA.XSql.GetDataTable("Select *,(CASE SendToUserId WHEN '" + sendid + "' THEN 'left' ELSE 'right' END) txtalign from view_Messages where (SendToUserId='" + sendid + "' and FromUserId='" + fromid + "') or (SendToUserId='" + fromid + "' and FromUserId='" + sendid + "') order by SendTime asc");
            DataList1.DataSource = dlist;
            DataList1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Model.HistoryMessages.MODEL hismodel = WX.Model.HistoryMessages.GetModel("SELECT * FROM TM_HistoryMessages where ID='" + Request.QueryString["Id"] + "'");
       
            //WX.Authentic
            WX.Model.Message.MODEL model = WX.Model.Message.NewDataModel();
            model.Title.value = txtContent.Text.Trim();
            model.ID.value = Guid.NewGuid();
            model.SendToUserId.value = hismodel.FromUserId.value;
            model.FromUserId.value = hismodel.SendToUserId.value;
            model.SendTime.value = DateTime.Now;
            model.RedirectToUrl.value = "/Manage/Main/messagelist.aspx";
            //model.State.value = 0;
            //model.Type.value = DropDownList1.SelectedValue;
            model.Type.value = "1";
            model.Insert();
            txtContent.Text = "";
            this.dataBind(hismodel.FromUserId.ToString(),hismodel.SendToUserId.ToString());
        }
    }
}