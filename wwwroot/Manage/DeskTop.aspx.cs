using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LazyOA
{
    public partial class DeskTop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (WX.Main.CurUser.IsEmployeeUser)
                {
                    this.spanEmployee.Visible = true;
                    //Load UserName
                    this.lblUserName.InnerText = WX.Authentication.GetUserName();
                    //Load Msg
                    this.LoadPrivateMsg(true);
                    //Load Private State
                    WX.WXUser user = WX.Main.CurUser;
                    WX.Model.Employee.MODEL employee = WX.Model.Employee.GetModelToID(user.UserID);
                    user.LoadUserModel(true);
                    if (employee.IDCard.ToString() == "" || employee.Email.ToString() == "")
                        Response.Redirect("Private/Priv_EditUser.aspx");
                    user.LoadMyDepartment(true);
                    user.LoadDutyDetailUser(true);
                    user.LoadDutyUser(true);
                    user.LoadMyGrade(false);
                    //判断防止第一次进入错误
                    int grade = 0;
                    if (user.MyGrade != null) grade = user.DutyDetailUser.GradeID.ToInt32(); //grade = user.MyGrade.Sort.ToInt32();

                    grade = user.UserModel.Grade.ToInt32();

                    string deptName = null;
                    if (user.MyDepartMent != null) deptName = user.MyDepartMent.Name.ToString();
                    string dutyName = null;
                    if (user.DutyUser != null) dutyName = user.DutyUser.Name.ToString();
                    this.lblPrivateState.Text = String.Format("{0},{1},{2}"
                        , deptName, dutyName, WX.Model.Grade.GetModel(grade).Name.ToString()+"("+grade+")");
                }
                else
                {
                    this.lblUserName.InnerText = WX.Authentication.GetUserName();
                    this.spanEmployee.Visible = false;
                }
            }
        } 
        //绑定数据
        public void LoadPrivateMsg(bool start)
        {
            WX.Model.User.MODEL user =WX.Model.User.GetCache(WX.Main.CurUser.UserID);
            
            string sSql = "Select top 5 XZ_Notify.*,RealName,XZ_NotifyCategory.Name CategoryName from XZ_Notify "
                     +"left join TU_Users on XZ_Notify.UserID=TU_Users.UserID "
                     +"left join XZ_NotifyCategory on XZ_Notify.CategoryID=XZ_NotifyCategory.ID "
                     +"where "
                          //+"Starttime<='" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + "' "
                          //+"and (Stoptime>'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + "' or Stoptime is null) "
                          + "(depms='' or depms='*' or ','+depms+',' like '%," + user.DepartmentID.ToString() + ",%') "
                          +"and (dutys='' or dutys='*' or ','+dutys+',' like '%," + user.DutyId.ToString() + ",%') "
                          +"and (Users='' or users='*' or ','+users+',' like '%," + user.UserID.ToString() + ",%') "
                                +"order by Istop desc, Starttime desc";
            GridView1.DataSource = ULCode.QDA.XSql.GetDataTable(sSql);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.HeaderStyle.Height = Unit.Pixel(30);
            }
            WX.Main.CurUser.LoadUserModel(false);
            WX.Main.CurUser.LoadMyDepartment(false);
            string sSql2 = "Select XZ_NotifyFiles.*,RealName from XZ_NotifyFiles left join TU_Users on XZ_NotifyFiles.UserID=TU_Users.UserID left join TE_Departments dept on dept.ID=TU_Users.DepartmentID " +
"where ((XZ_NotifyFiles.Area=1 and Depms like '%" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + "%') " +
"or (XZ_NotifyFiles.Area=1 and Depms is null) " +
"or (XZ_NotifyFiles.Area=2 and dept.ID= " + WX.Main.CurUser.UserModel.DepartmentID.ToString() + ") " +
"or (XZ_NotifyFiles.Area=3 and dept.ParentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + ") " +
"or (XZ_NotifyFiles.Area=5 and Users like '%" + WX.Main.CurUser.UserID + "%') " +
"or (XZ_NotifyFiles.Area=4 and dbo.get_oneid(dept.ID) = dbo.get_oneid(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + "))" +
") and XZ_NotifyFiles.State=5 order by Istop desc, PublishTime desc";
            GridView2.DataSource = ULCode.QDA.XSql.GetDataTable(sSql2);
            GridView2.DataBind();
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.HeaderStyle.Height = Unit.Pixel(30);
            }
        }
    }
}