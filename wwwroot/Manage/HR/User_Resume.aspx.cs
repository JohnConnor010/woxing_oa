using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;

namespace wwwroot.Manage.HR
{
    public partial class User_Resume : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();

            }
        }
        private void PageInit()
        {
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
            for (int i = 0; i < Employee.eduarray.Length; i++)
            {
                ui_edu.Items.Add(new ListItem(Employee.eduarray[i], Employee.eduarray[i]));
            }
            ui_edu.SelectedValue = "大专";

            Employee.MODEL employee = WX.Request.rEmpolyee;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            txtRealName.Text = usermodel.RealName.ToString();
            txtIDCard.Text = employee.IDCard.ToString();
            txtEmail.Text = employee.Email.ToString();
            ddlDepartment.SelectedValue = employee.DepartmentID.ToString();
            bindjob();
            ui_jobname.SelectedValue = employee.DutyId.ToString();
            ui_salary.Text = employee.Salary.ToString();
            rblSex.SelectedValue = employee.Sex.ToString();
            ui_Ethnic.Text = employee.Ethnic.ToString();


            txtBirthday.Text = employee.Birthday.f("{0:yyyy-MM-dd}");
            this.txtMobile.Text = employee.Mobile.ToString();
            if (Convert.ToBoolean(employee.Sex.ToString()))
            {
                this.rblSex.SelectedValue = "1";
            }
            else
            {
                this.rblSex.SelectedValue = "0";
            }
            this.txtTelephone.Text = employee.Tel.ToString();
            this.ui_Titles.Text = employee.Titles.ToString();
            this.ui_edu.SelectedValue = employee.Edu.ToString();
            this.ui_Prof.Text = employee.Prof.ToString();
            this.ui_ForeignL.Text = employee.ForeignL.ToString();
            this.ui_Rating.Text = employee.Rating.ToString();
            this.ui_Marital.Text = employee.Marital.ToString();
            this.ui_Health.Text = employee.Health.ToString();
            string[] addrarry = employee.Address.ToString().Split('|');
            li_Skill.Text = getxmlString("Priv-Skill", employee.Skill.ToString(), "个人技能");
            li_edu.Text = getxmlString("Priv-Edu", employee.Education.ToString(), "教育经历") ;
            li_work.Text = getxmlString("Priv-Work", employee.Work.ToString(), "工作经历");

            WX.Model.Audition.MODEL auditionmodel = WX.Model.Audition.GetModel(employee.UserID.ToString());
            if (auditionmodel != null)
            {
                TextBox1.Text = WX.Main.CurUser.UserModel.DepartmentID.ToInt32() != 801 ? (WX.CommonUtils.GetRealNameListByUserIdList(auditionmodel.FirstUser.ToString()) + "：" + auditionmodel.FirstOpinion.ToString() + "（" + auditionmodel.FirstTime.ToString() + "）") : auditionmodel.FirstOpinion.ToString();
                WX.Main.CurUser.LoadUserModel(false);
            }
            if (usermodel.State.ToInt32() >= 10)
                Submit1.Visible = Submit2.Visible = Submit3.Visible = false;
            else if (WX.Main.CurUser.UserModel.DepartmentID.ToInt32() != 801)
            {
                Submit3.Visible = false;
                Submit1.Visible = Submit2.Visible = true;// auditionmodel.AuditionState.ToInt32() == 0;
            }
            else
                Submit3.Visible = auditionmodel.AuditionState.ToInt32() == 0;
            if (addrarry.Length > 1)
            {
                this.txtAddress.Text = addrarry[0];
                this.ui_hkd.Text = addrarry[2];
            }
            this.txtAddress.Text = addrarry[0];
            if (employee.UserFace.isEmpty)
            {
                this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"/Images/nophoto.gif\" alt=\"\" style=\"width: 99%; height: 99%;\" />";
            }
            else
            {
                this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"" + (employee.UserFace.ToString()) + "\" alt=\"\" style=\"width: 99%; height: 99%; \" />";
            }
            this.txtContent.Text = employee.Introduction.ToString();
            try
            {
                if(Request["mes"]!=null)
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%User_Resume.aspx?UserID={1}%'", WX.Main.CurUser.UserID,WX.Request.rUserId));
            }
            catch
            {
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }

        public void bindjob()
        {
            DataTable dt;
            dt = WX.Model.DutyDetail.GetTableDepartent(ddlDepartment.SelectedValue);
            this.ui_jobname.DataSource = dt;
            this.ui_jobname.DataTextField = "Name";
            this.ui_jobname.DataValueField = "ID";
            this.ui_jobname.DataBind();
        }
        protected void RegisterUser(object sender, EventArgs e)
        {
            Employee.MODEL employee = WX.Request.rEmpolyee;
            employee.DepartmentID.value = ddlDepartment.SelectedValue;
            employee.DutyId.value = ui_jobname.SelectedValue;
            employee.Salary.value = ui_salary.Text;
            employee.Update();
            WX.Model.Audition.MODEL auditionmodel = WX.Model.Audition.GetModel(employee.UserID.ToString());
            if (auditionmodel == null)
            {
                auditionmodel = WX.Model.Audition.NewDataModel();
                auditionmodel.UserID.value = employee.UserID.value;
                auditionmodel.FirstUser.value = WX.Main.CurUser.UserID;
                auditionmodel.FirstOpinion.value = TextBox1.Text;
                auditionmodel.FirstTime.value = DateTime.Now;
                auditionmodel.Insert();
            }
            else
            {
                auditionmodel.FirstUser.value = WX.Main.CurUser.UserID;
                auditionmodel.FirstOpinion.value = TextBox1.Text;
                auditionmodel.FirstTime.value = DateTime.Now;
                auditionmodel.Update();
            }
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            string SendUserID = WX.CommonUtils.GetUserIDListByWhereStr(1, "DepartmentID="+employee.DepartmentID.ToString()+" and State in(10,20) order by Grade desc");
            WX.Main.MessageSend("<a href=/Manage/HR/User_Resume.aspx?UserID=" + employee.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "——面试通知</a>", "/Manage/Main/messagelist.aspx", SendUserID, WX.Main.CurUser.UserID, 7, 0);
            Response.Redirect("HR_NewIntojobs.aspx?All=1");
        }
        private void SetUserState(int state)
        {
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            usermodel.State.value = state;
            usermodel.ArchiveBySelf.set(state == 6 ? 0 : 1);
            usermodel.Update();
            WX.Model.Audition.MODEL auditionmodel = WX.Model.Audition.GetModel(usermodel.UserID.ToString());
            bool flag = true;
            if (auditionmodel == null)
            {
                flag = false;
                auditionmodel = WX.Model.Audition.NewDataModel();
                auditionmodel.UserID.value = usermodel.UserID.value;
            }
            auditionmodel.AuditionUser.value = WX.Main.CurUser.UserID;
            auditionmodel.AuditionState.value = usermodel.State.ToInt32() == 6 ? 1 : -1;
            auditionmodel.AuditionTime.value = DateTime.Now;
            if (!flag)
                auditionmodel.Insert();
            else
                auditionmodel.Update();
            if (auditionmodel.AuditionState.ToInt32() == 1)
            {
                WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=0", "UserID='" + usermodel.UserID.ToString() + "'");
                WX.Main.MessageSend("<a href=/Manage/HR/HR_AddIntojobs.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "面试通过！请尽快办理入职手续和签订试用协议——入职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 8, 0);
                WX.Main.MessageSend("<a href=/Manage/Private/Priv_EditUser.aspx?mes=1>恭喜面试成功！请进一步完善个人资料并办理入职——入职通知</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 8, 0);
                WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
                log.UserID.value = usermodel.UserID.value;
                log.NowDutyID.value = usermodel.DutyId.value;
                log.NowDempID.value = usermodel.DepartmentID.value;
                log.Backtableid.value = 7;
                log.Backcolumid.value = 0;
                log.Starttime.value = DateTime.Now;
                log.Content.value = "面试成功";
                log.Insert();
            }
        }
        public string getxmlString(string appkey, string xmlstring, string xmlname)
        {
            string[] items = System.Configuration.ConfigurationManager.AppSettings[appkey].ToString().Split('|');
            string pagestr = "<tr style='font-weight:bold; height:28px;'>";
            for (int i = 0; i < items.Length; i++)
            {
                string[] item_2 = items[i].Split(';');
                if (item_2[0].IndexOf("{") > -1)
                    pagestr += "<td>" + item_2[0].Substring(0, item_2[0].IndexOf("{")) + "</td>";
                else
                    pagestr += "<td>" + item_2[0] + "</td>";
            }
            pagestr += "</tr>";
            //装载
            ULCode.KeyXmlString kxs10 = new ULCode.KeyXmlString();
            if (xmlstring.IndexOf("<KeyXmlString>") > -1)
            {
                kxs10.LoadData(xmlstring.Replace("&nbsp;", ""));
            }
            int n = 0;
            foreach (String s in kxs10.GetItemValues("Node"))
            {
                pagestr += "<tr height='24'>";
                ULCode.KeyXmlString kxs9 = new ULCode.KeyXmlString();
                kxs9.LoadData(s);
                for (int i = 0; i < items.Length; i++)
                {
                    string[] item_2 = items[i].Split(';');
                    if (item_2[0].IndexOf("{") > -1)
                        pagestr += "<td>" + kxs9.GetItemValue(item_2[0].Substring(0, item_2[0].IndexOf("{"))) + "</td>";
                    else
                        pagestr += "<td>" + kxs9.GetItemValue(item_2[0]) + "</td>";
                }
                pagestr += "</tr>";
                n++;
            }
            return "<table class=\"table3\" style='text-align:center;'>" + pagestr + "</table>";
        }
        protected void State6User(object sender, EventArgs e)
        {
            SetUserState(6);
           
            Response.Redirect("HR_NewIntojobs.aspx");
        }
        protected void State2User(object sender, EventArgs e)
        {
            SetUserState(2);
            Response.Redirect("HR_NewIntojobs.aspx");
        }
    }
}