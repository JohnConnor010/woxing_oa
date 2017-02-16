using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.HR
{
    public partial class HR_Intojobs : System.Web.UI.Page
    {
        string state = "20";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["state"] != null && Request["state"] != "")
                    state = Request["state"];
                switch (state)
                {
                    case "0": Literal1.Text = "新增入职信息"; MenuBar1.CurIndex = 1; break;
                    case "5": Literal1.Text = "注册审核"; MenuBar1.CurIndex = 1; break;
                    case "6": Literal1.Text = "办理入职"; MenuBar1.CurIndex = 2; break;
                    case "10": Literal1.Text = "新入职员工"; MenuBar1.CurIndex = 3;break;
                    //case "30": Literal1.Text = "人才储备"; MenuBar1.CurIndex = 3;  break;
                    case "40": Literal1.Text = "离职员工"; MenuBar1.CurIndex = 5; break;
                    default: Literal1.Text = "正式员工"; MenuBar1.CurIndex = 4; break;
                }
                pageinit("order by DepartmentID, Sort asc");
            }
        }
        private void pageinit(string orderBy)
        {
            string vwnmae = Request["state"] == "5" ? "vw_EmployeesState5_HR" : "vw_Employees_HR";
            string where = Request["state"] == "5" ? " State<=5" : " State=" + state;
            if (Request["redo"] != null)
            {
                WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(Request["UserID"]);
                usermodel.State.value = 5;
                usermodel.Update();

                WX.Model.Audition.MODEL auditionmodel = WX.Model.Audition.GetModel(usermodel.UserID.ToString());
                auditionmodel.AuditionState.value = 0;
                auditionmodel.Update();
                WX.Model.Employee.MODEL employee = WX.Request.rEmpolyee;
                WX.Main.MessageSend("<a href=/Manage/HR/User_Resume.aspx?UserID=" + employee.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "——面试通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", employee.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 7, 0);

            }
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select *,0 stateid FROM "+vwnmae+" WHERE " + where + orderBy);
            if (state == "0" && dt.Rows.Count <= 0)
            {
                Response.Redirect("/Manage/HR/User_AddUser.aspx");
            }
            Gv_intojobs.DataSource = dt;
            Gv_intojobs.DataBind();
            if (Gv_intojobs.Rows.Count > 0)
            {
                Gv_intojobs.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_intojobs.HeaderStyle.Height = Unit.Pixel(40);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text == "True" ? "男" : "女";
                switch (e.Row.Cells[7].Text)
                {
                    case "2": e.Row.Cells[7].Text = "未通过&nbsp;&nbsp;<a href='?UserID=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "&redo=1&state=5'>重新面试</a>"; break;
                    case "5": e.Row.Cells[7].Text = "等待审核"; break;
                    case "6": e.Row.Cells[7].Text = "<a href='HR_AddIntojobs.aspx?UserId=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>审核并入职</a>"; break;
                    case "10": e.Row.Cells[7].Text = "<a href='HR_AddIntojobs.aspx?UserId=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>员工档案</a><a href='HR_AddTransferKong.aspx?type=1&UserId=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>调岗</a><a href='HR_Official.aspx?UserID=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>转正</a><a href='HR_Leavejobs2.aspx?UserID=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>离职</a>"; break;
                    case "20":
                        {
                            e.Row.Cells[7].Text = "<a href='HR_AddTransferKong.aspx?type=1&UserId=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>调岗</a><a href='HR_AddTransferKong.aspx?type=2&UserID=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>升职</a><a href='HR_Userjobs.aspx?UserId=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>加/撤职</a><a href='HR_Leavejobs2.aspx?UserID=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>离职</a>";
                    ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select te.Name from TU_User_X_DutyDetail tuxd left join TE_DutyDetail te on tuxd.DutyDetailID=te.ID where tuxd.UserID='" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'");
                        string str=xdt.ToColValueList("，+", 0);
                        if(str!="")
                        {
                    e.Row.Cells[5].Text +="，+"+str ; 
                        }break;
                    }
                    //case "30": e.Row.Cells[7].Text = "<a href='HR_AddTransferKong.aspx?type=1&uid=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>调岗</a>&nbsp;&nbsp;<a href='HR_AddTransferKong.aspx?type=2&uid=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>升职</a>&nbsp;&nbsp;<a href='HR_Leavejobs.aspx?uid=" + Gv_intojobs.DataKeys[e.Row.RowIndex].Value + "'>离职</a>"; break;
                    case "40": e.Row.Cells[7].Text = "离职中。。。"; break;
                    default: break;
                }
            }
        }

        protected void Gv_intojobs_Sorting(object sender, GridViewSortEventArgs e)
        {
            Literal li = (Literal)Gv_intojobs.Parent.FindControl("liHidden_" + e.SortExpression);
            this.pageinit(String.Format("order by {0} {1}", e.SortExpression, li.Text));
            if (li.Text == "")
                li.Text = "Desc";
            else if (li.Text.EndsWith("Asc"))
                li.Text = li.Text.Replace("Asc", "Desc");
            else
                li.Text = li.Text.Replace("Desc", "Asc");
        }
    }
}