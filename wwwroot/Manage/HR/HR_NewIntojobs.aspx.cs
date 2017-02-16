using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.HR
{
    public partial class HR_NewIntojobs : System.Web.UI.Page
    {
        private bool rAll { get { return Convert.ToInt32(Request.QueryString["All"]) == 1; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.Main.CurUser.LoadUserModel(false);
            if (!IsPostBack)
            {
                WX.Main.CurUser.LoadDutyUser();
                //if(!rAll)
                //    if (WX.Main.CurUser.DutyUser.DutyCatagoryID.ToString() != "1")
                //    {
                //        Response.Write("你没有权限访问此功能！");
                //        Response.End();
                //        return;
                //    }
                pageinit();
            }
        }
        private void pageinit()
        {
            string sSql = String.Format("select *,0 stateid FROM vw_EmployeesState5_HR WHERE State=5 {0} order by Sort asc"
                , rAll ? "" : String.Format(" and (DepartmentID={0} or DepartmentID=0)", WX.Main.CurUser.UserModel.DepartmentID));
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            Gv_intojobs.DataSource = dt;
            Gv_intojobs.DataBind();
            if (Gv_intojobs.Rows.Count > 0)
            {
                Gv_intojobs.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_intojobs.HeaderStyle.Height = Unit.Pixel(40);
            }
        }
        protected void Gv_intojobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(e.CommandArgument);
            usermodel.State.value = e.CommandName == "state1" ? 6 : 2;
            usermodel.ArchiveBySelf.set(e.CommandName == "state1" ? 0 : 1);
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
                WX.Main.MessageSend("<a href=/Manage/HR/HR_AddIntojobs.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "面试通过！请尽快办理入职——入职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 8, 0);
                WX.Main.MessageSend("<a href=/Manage/Private/Priv_EditUser.aspx?mes=1>恭喜面试成功！请进一步完善个人资料并办理入职——入职通知</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 8, 0);
            }
            pageinit();
        }
    }
}