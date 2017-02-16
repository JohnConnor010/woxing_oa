using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;
using System.IO;

namespace wwwroot.Manage.HR
{
    public partial class HR_AddIntojobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            //1.validate user data
            string userId = WX.Request.rUserId;
            //2.init controls
            WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList1, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList2, null, null, null);
            //3.如果已经入职，则不能再入职
            
            //4.显示用户档案信息
            Employee.MODEL employee = WX.Request.rEmpolyee;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;

            MenuBar1.Param2 = usermodel.State.ToString() != "" ? usermodel.State.ToString() : "0";
            if (employee != null)
            {
                li_name.Text = usermodel.RealName.ToString();
                li_sex.Text = ((bool)employee.Sex.value ? "男" : "女");
                try
                {
                    li_age.Text = (DateTime.Now.Year - ((DateTime)employee.Birthday.value).Year).ToString();
                }
                catch { }
                li_edu.Text = employee.Edu.ToString();
                li_ForeignL.Text = employee.ForeignL.ToString();
                li_Rating.Text = employee.Rating.ToString();
                li_Ethnic.Text = employee.Ethnic.ToString();
                li_Marital.Text = employee.Marital.ToString() == "0" ? "未婚" : "已婚";
                li_Health.Text = employee.Health.ToString();
                string[] addarry = employee.Address.ToString().Split('|');
                if (addarry.Length > 1)
                {
                    li_jg.Text = addarry[1];
                    li_hjd.Text = addarry[2];
                }
                li_IDCard.Text = employee.IDCard.ToString();
                li_Mobile.Text = employee.Mobile.ToString();
              
                if (!employee.UserFace.isEmpty)
                    this.li_face.Text = "<img id=\"preZoomImage\" src=\"" + (employee.UserFace.ToString()) + "\" alt=\"\" style=\"width: 100%; height: 100%; \" />";
                else
                    this.li_face.Text="<img id=\"preZoomImage\" src=\"/Images/nophoto.gif\" alt=\"\" style=\"width: 99%; height: 99%;\" />";
                li_left.Text = getxmlString("Priv-Skill", employee.Skill.ToString(), "个人技能")+getxmlString("Priv-Edu", employee.Education.ToString(), "教育经历") + getxmlString("Priv-Work", employee.Work.ToString(), "工作经历") + getxmlString("Priv-Family", employee.Family.ToString(), "家庭成员");

                li_linkman.Text = getxmlString("Priv-UrgentLink", employee.UrgentLink.ToString(), "紧急联系人");
                ListItem li = ddlDepartment.Items.FindByValue(employee.DepartmentID.ToString());
                li_dept.Text = li != null ? li.Text : "";
                WX.Model.DutyDetail.MODEL dutydetail;
                if (!employee.DutyId.isEmpty)
                {
                    dutydetail = WX.Model.DutyDetail.GetModel("select * from TE_DutyDetail where ID=" + employee.DutyId.ToString());
                    li_duty.Text = dutydetail != null ? dutydetail.Name.ToString() : "";
                }
                li_salary.Text = employee.Salary.ToString();

            }
            WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + userId + "'");
            ui_addtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (intojob != null)
            {
                ddlDepartment.SelectedValue = intojob.deptid.ToString();
                bindjob();
                userId = intojob.UserID.ToString();
                ui_jobname.SelectedValue = intojob.jobsname.ToString();
                DropDownList1.SelectedValue = intojob.salary.ToString();
                DropDownList2.SelectedValue = intojob.PSalary.ToString();
                ui_content.Text = intojob.dempOpinion.ToString();
                ui_addtime.Text = Convert.ToDateTime(intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_SignUserID.Text = WX.CommonUtils.GetRealNameListByUserIdList(intojob.SignUserID.ToString());
            }
            else
            {
                ddlDepartment.SelectedValue = employee.DepartmentID.ToString();
                li_SignUserID.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.Main.CurUser.UserID);
                bindjob();
            }
            Button1.Enabled =ddlDepartment.Enabled =FileUpload1.Visible= ImageButton1.Visible = GridView1.Columns[1].Visible = usermodel.State.ToInt32() >= 10 ? false : true;
            try
            {
                if (Request["mes"] != null)
                    WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%HR_AddIntojobs.aspx?UserID={1}%'", WX.Main.CurUser.UserID,WX.Request.rUserId));
            }
            catch
            {
            }
            ContractBind();
        }
        public string getxmlString(string appkey, string xmlstring,string xmlname)
        {
            string[] items = System.Configuration.ConfigurationManager.AppSettings[appkey].ToString().Split('|');
            string pagestr = "<tr style='font-weight:bold; height:28px;'>";
            if (xmlname != "")
                pagestr += "<td rowspan='50' width='80'>" + xmlname + "</td>";
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
                        pagestr += "<td>"+kxs9.GetItemValue(item_2[0].Substring(0, item_2[0].IndexOf("{")))+ "</td>";
                    else
                        pagestr += "<td>"+kxs9.GetItemValue(item_2[0]) + "</td>";
                }
                pagestr += "</tr>";
                n++;
            }
            return "<table class=\"table3\" style='text-align:center;'>" + pagestr + "</table>";
        }
        public void bindjob()
        {
            DataTable dt;
            WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + WX.Request.rUserId + "'");
            if (intojob != null)
            {
                dt = WX.Model.DutyDetail.GetTableDepartent(ddlDepartment.SelectedValue);
            }
            else
            {
                WX.Model.User.MODEL user = WX.Request.rUser;
                dt = WX.Model.DutyDetail.GetTablenullDepartent(this.ddlDepartment.SelectedValue, user.RealName.ToString());
            }
            this.ui_jobname.DataSource = dt;
            this.ui_jobname.DataTextField = "Name";
            this.ui_jobname.DataValueField = "ID";
            this.ui_jobname.DataBind();
            if (this.ui_jobname.Items.Count == 0)
                Button1.Enabled = false;
            else
                Button1.Enabled = true;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //1.validate user data
            string userId = WX.Request.rUserId;

            //2.入职手续            
            WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.NewDataModel();
            intojob.UserID.value = userId;
            intojob.Addtime.value = DateTime.Now;
            try
            {
                intojob.Addtime.value = ui_addtime.Text.Trim();
            }
            catch { }
            intojob.jobsname.value = ui_jobname.SelectedValue;
            intojob.deptid.value = ddlDepartment.SelectedValue;
            intojob.salary.value = DropDownList1.SelectedValue;
            intojob.PSalary.value = DropDownList2.SelectedValue;
            intojob.dempOpinion.value = ui_content.Text + "(" + DateTime.Now + ")";
            //intojob.GradeID.value = ui_grade.SelectedValue;
            intojob.SignUserID.value = WX.Main.CurUser.UserID;
            int jobid = intojob.Insert(true);
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(Convert.ToInt32(ui_jobname.SelectedValue));
            //3.更新用户职务状态
            usermodel.State.value = 10;
            usermodel.CompanyID.value = WX.Main.DefaultCompanyId;
            usermodel.DepartmentID.set(ddlDepartment.SelectedValue);
            usermodel.DutyId.set(ui_jobname.SelectedValue);
            usermodel.ArchiveBySelf.value = 0;
            usermodel.Grade.value = DropDownList1.SelectedValue;
            usermodel.Update();
            WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=0", "UserID='" + usermodel.UserID.ToString() + "'");
            ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetail.ID.ToString()+" and State>6 and State<40");
            dutydetail.UsersName.value = xdt.ToColValueList(",", 0);
            if (dutydetail.UsersName.ToString() != "")
            {
                dutydetail.UsersName.value = dutydetail.UsersName.ToString() + ",";
                dutydetail.Update();
            }
            //4.入职日志
            WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
            log.UserID.value = userId;
            log.NowDutyID.value = usermodel.DutyId.value;
            log.NowDempID.value = usermodel.DepartmentID.value;
            log.Backtableid.value = 0;
            log.Backcolumid.value = jobid;
            log.Starttime.value = DateTime.Now;
            log.GradeID.value = dutydetail.GradeID.value;
            log.Content.value = "新员工入职";
            WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + userId + "' order by Starttime desc");
            if (backlog != null)
            {
                backlog.stoptime.value = DateTime.Now;
                backlog.Nowtableid.value = 0;
                backlog.Nowcolumid.value = jobid;
                backlog.Update();
            }
            log.Insert();
            //办理完入职1、向部门发送人员入职通知
            WX.Main.MessageSend(usermodel.RealName.ToString() + "已办理入职，" + WX.CommonUtils.GetDeptNameListByDeptIdList(usermodel.DepartmentID.ToString()) + "接收——入职通知", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 8, 0);
            //2、向人资发送入职通知并提醒签合同、办保险
            WX.Main.MessageSend(usermodel.RealName.ToString() + "已办理入职——入职通知", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 8, 0);
            //3、向综管发送通知提示配备办公用品
            WX.Main.MessageSend("<a href=/Manage/Assets/Ass_AddConsuming.aspx?UserID="+usermodel.UserID.ToString()+"&mes=1>" + WX.CommonUtils.GetDeptNameListByDeptIdList(usermodel.DepartmentID.ToString()) + "新来同事" + usermodel.RealName.ToString() + "，请配备办公用品——入职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 8, 0);
            //4、向财务发送通知提示办理工资卡
            WX.Main.MessageSend("<a href=/Manage/Finance/FD_NewUserList.aspx?mes=1>" + WX.CommonUtils.GetDeptNameListByDeptIdList(usermodel.DepartmentID.ToString()) + "新来同事" + usermodel.RealName.ToString() + "，请办理工资卡等相关工作——入职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetFDUserID, WX.Main.CurUser.UserID, 8, 0);
            //5、向入职人员发送入职办理清单
            WX.Main.MessageSend("<a href=/Manage/Private/IntoJobList.aspx?mes=1>欢迎加入我行大家庭！入职相关事宜请查看新人入职附表——入职通知</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 8, 0);
            Response.Redirect("HR_Intojobs.aspx?state=10");
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }
        private void ContractBind()
        {
            GridView1.DataSource = WX.Model.EmployeeContract.GetList(WX.Request.rUserId,0);
            GridView1.DataBind();
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            WX.Model.EmployeeContract.MODEL model = WX.Model.EmployeeContract.NewDataModel();
            model.Name.value = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            model.UserId.value =WX.Request.rUserId;
            model.ManageUserID.value = WX.Main.CurUser.UserID;
            model.Type.value = 0;
            if (FileUpload1.HasFile)
            {
                string filepath = "/UploadFiles/Contract/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") +Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
            }            
                model.Insert();
                ContractBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WX.Model.EmployeeContract.MODEL model = WX.Model.EmployeeContract.NewDataModel(e.CommandArgument);
            model.Delete();
            ContractBind();
        }
    }
}