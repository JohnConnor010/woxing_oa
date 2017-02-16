using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
namespace wwwroot.Manage.CRM
{
    public partial class Crm_Manage_CustomerInfo : System.Web.UI.Page
    {
        public string tablestr = "";
        private string GetCustomerNames(DataTable dtCustomers)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dtCustomers.Rows)
            {
                if (sb.Length != 0) sb.Append("　");
                sb.AppendFormat("<a class='customer' href=\"javascript:personView({1})\">{0}</a>", dr["CustomerName"], dr["ID"]);
            }
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.Main.CurUser.LoadDutyDetailUser();
            string sql = "select cc.ID,ContactName,BabySex,cc.CustomerID,CustomerName from CRM_Contact cc inner join CRM_Customers ccu on cc.CustomerID=ccu.ID left join Tu_Users tu on ccu.EmployeeID=tu.UserID where datediff(day,BabyBirthday,getdate())=0 and (BabySex='男' or BabySex='女')" + (WX.Main.CurUser.DutyDetailUser.DutyID.ToInt32() >= 900 ? "" : " and tu.DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString());
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sql);
            int no = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tablestr += "<tr><th style=\"width: 30px;\"></th><td> " + no + "、" + dt.Rows[i]["CustomerName"] + dt.Rows[i]["ContactName"] + (dt.Rows[i]["BabySex"].ToString() == "男" ? "儿子" : "女儿") + "今天生日！</td><td class=\"manage\" style=\"width: 100px;\"><a href=\"Crm_Single_AddContact.aspx?PageMode=my&Action=Edit&&ContactID=" + dt.Rows[i]["ID"] + "&CustomerID=" + dt.Rows[i]["CustomerID"] + "\">维护</a></td></tr>";
                no++;
            }
            sql = "select ct.ID,ct.ProcessState,cc.CustomerID CustomerNo,cc.CustomerName from CRM_Track ct left join CRM_Customers cc on ct.CustomerID=cc.ID  left join Tu_Users tu on cc.EmployeeID=tu.UserID where datediff(day,TrackTime,getdate())=0 and ct.State=0"+(WX.Main.CurUser.DutyDetailUser.DutyID.ToInt32() >= 900 ? "" : " and tu.DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString());
            DataTable dt2 = ULCode.QDA.XSql.GetDataTable(sql);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                tablestr += "<tr><th style=\"width: 30px;\"></th><td> " + no + "、" + dt2.Rows[i]["CustomerName"] + "今日应该" + WX.CRM.Track.ProcessState[Convert.ToInt32(dt2.Rows[i]["ProcessState"])] + "！</td><td class=\"manage\" style=\"width: 100px;\"><a href=\"javascript:PopupIFrame('CRM_SingleM_EditTrack.aspx?TrackID=" + dt2.Rows[i]["ID"] + "','提交跟踪信息','','',700,450)\">维护</a></td></tr>";
                no++;
            }
            string ids = WX.Main.GetUserDeptids(WX.Main.CurUser.UserID);
            WX.Main.CurUser.LoadMyDepartment();
            DataTable dtcount = ULCode.QDA.XSql.GetDataTable("exec " + (WX.CommonUtils.GetBossUserID == WX.Main.CurUser.UserID || WX.Main.CurUser.MyDepartMent.ID.ToString() == System.Configuration.ConfigurationManager.AppSettings["Dept_CA"] ? "Get_CRM_ManagerLDTongji" : "Get_CRM_ManagerTongji2 '" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + "'"));
            limycount.Text = dtcount.Rows[0]["mycount"].ToString();
            limyTempcount.Text = dtcount.Rows[0]["myTempcount"].ToString();
            sql = "SELECT  case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName FROM CRM_CustomersTemp C left join TU_Users tu on C.EmployeeID=tu.UserID where C.State>-1" + (WX.Main.CurUser.DutyDetailUser.DutyID.ToInt32() >= 900 ? "" : " and tu.DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString())
                       + " union all "
                       + " SELECT  case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName FROM CRM_ContactTemp as cctemp left join CRM_Customers AS C  on cctemp.CustomerID=C.ID left join TU_Users tu on C.EmployeeID=tu.UserID"
                        + " where cctemp.CheckState>-1" + (WX.Main.CurUser.UserID==WX.CommonUtils.GetBossUserID ? "" : " and tu.DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString());
            ULCode.QDA.XDataTable tempdt = ULCode.QDA.XSql.GetXDataTable(sql);
            Literal1.Text = tempdt.ToColValueList("，", 0);
            Literal3.Text = dtcount.Rows[0]["myGTcount"].ToString();
            DataTable gtdt = ULCode.QDA.XSql.GetDataTable("select top 10 case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName,cc.ID from CRM_Customers cc" + (WX.Main.CurUser.UserID != WX.CommonUtils.GetBossUserID ? " left join Tu_Users tu on cc.EmployeeID=tu.UserID where cc.State>0 and tu.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ") and StageID<2" : " where cc.State>0 and StageID<2 "));
            //Literal4.Text = gtdt.ToColValueList("，", 0);
            Literal4.Text = this.GetCustomerNames(gtdt);
            Literal6.Text = dtcount.Rows[0]["myGZcount"].ToString();
            DataTable gzdt = ULCode.QDA.XSql.GetDataTable("select top 10 case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName,cc.ID from CRM_Customers cc" + (WX.Main.CurUser.UserID != WX.CommonUtils.GetBossUserID ? " left join Tu_Users tu on cc.EmployeeID=tu.UserID where cc.State>0 and tu.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ") and StageID=2" : " where cc.State>0 and StageID=2 "));
            Literal7.Text = this.GetCustomerNames(gzdt);
            Literal9.Text = dtcount.Rows[0]["myQYcount"].ToString();
            DataTable qydt = ULCode.QDA.XSql.GetDataTable("select top 10 case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName,cc.ID from CRM_Customers cc" + (WX.Main.CurUser.UserID != WX.CommonUtils.GetBossUserID ? " left join Tu_Users tu on cc.EmployeeID=tu.UserID where cc.State>0 and tu.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ") and StageID=3" : " where cc.State>0 and StageID=3 "));
            Literal10.Text = this.GetCustomerNames(qydt);
            Literal12.Text = dtcount.Rows[0]["myWHcount"].ToString();
            DataTable whdt = ULCode.QDA.XSql.GetDataTable("select top 10 case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName,cc.ID from CRM_Customers cc" + (WX.Main.CurUser.UserID != WX.CommonUtils.GetBossUserID ? " left join Tu_Users tu on cc.EmployeeID=tu.UserID where cc.State>0 and  tu.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ") and StageID=4" : " where cc.State>0 and StageID=4 "));
            Literal13.Text = this.GetCustomerNames(whdt);
            Literal15.Text = dtcount.Rows[0]["cmpmonthcount"].ToString();
            Literal16.Text = dtcount.Rows[0]["deptmonthcount"].ToString();
            Literal18.Text = dtcount.Rows[0]["cmpmonthQYcount"].ToString();
            Literal19.Text = dtcount.Rows[0]["deptmonthQYcount"].ToString();
            Literal21.Text = dtcount.Rows[0]["cmpmonthGZcount"].ToString();
            Literal22.Text = dtcount.Rows[0]["deptmonthGZcount"].ToString();
            Literal24.Text = dtcount.Rows[0]["cmpmonthTrackcount"].ToString();
            Literal25.Text = dtcount.Rows[0]["deptmonthTrackcount"].ToString();
            Literal27.Text = string.Format("{0:C2}",dtcount.Rows[0]["cmpmonthTrackfee"]);
            Literal28.Text = string.Format("{0:C2}",dtcount.Rows[0]["deptmonthTrackfee"]);


            Literal30.Text = dtcount.Rows[0]["cmpyearcount"].ToString();
            Literal35.Text = dtcount.Rows[0]["deptyearcount"].ToString();
            Literal31.Text = dtcount.Rows[0]["cmpyearQYcount"].ToString();
            Literal36.Text = dtcount.Rows[0]["deptyearQYcount"].ToString();
            Literal32.Text = dtcount.Rows[0]["cmpyearGZcount"].ToString();
            Literal37.Text = dtcount.Rows[0]["deptyearGZcount"].ToString();
            Literal33.Text = dtcount.Rows[0]["cmpyearTrackcount"].ToString();
            Literal38.Text = dtcount.Rows[0]["deptyearTrackcount"].ToString();
            Literal34.Text = string.Format("{0:C2}",dtcount.Rows[0]["cmpyearTrackfee"]);
            Literal39.Text = string.Format("{0:C2}",dtcount.Rows[0]["deptyearTrackfee"]);
            if (WX.Main.CurUser.DutyDetailUser.DutyID.ToInt32() >= 900)
            {
                monthdiv.Visible = false;
                yeardiv.Visible = false;
            }
            DataTable categorydt = ULCode.QDA.XSql.GetDataTable("select CategoryID,count(CategoryID) count,max(CategoryName) cname  from CRM_Customers cc left join CRM_InnerCategory cic on cc.CategoryID=cic.ID where EmployeeID='" + WX.Main.CurUser.UserID + "' and CategoryID is not null group by CategoryID ");
            for (int i = 0; i < categorydt.Rows.Count; i++)
            {
                Literal45.Text += (i > 0 ? "，" : "") + categorydt.Rows[i]["cname"] + categorydt.Rows[i]["count"] + "个";
            }
            DataTable Naturedt = ULCode.QDA.XSql.GetDataTable("select NatureID,count(NatureID) count,max(CompanyNature) cname  from CRM_Customers cc left join CRM_CompanyNature cin on cc.NatureID=cin.ID where EmployeeID='" + WX.Main.CurUser.UserID + "' and NatureID is not null group by NatureID");
            for (int i = 0; i < Naturedt.Rows.Count; i++)
            {
                Literal46.Text += (i > 0 ? "，" : "") + Naturedt.Rows[i]["cname"] + Naturedt.Rows[i]["count"] + "个";
            }
            DataTable Sourcedt = ULCode.QDA.XSql.GetDataTable("select SourceID,count(SourceID) count,max(SourceName) cname  from CRM_Customers cc left join CRM_Source cs on cc.SourceID=cs.ID where EmployeeID='" + WX.Main.CurUser.UserID + "' and SourceID is not null group by SourceID ");
            for (int i = 0; i < Sourcedt.Rows.Count; i++)
            {
                Literal47.Text += (i > 0 ? "，" : "") + Sourcedt.Rows[i]["cname"] + Sourcedt.Rows[i]["count"] + "个";
            }
            DataTable Industrydt = ULCode.QDA.XSql.GetDataTable("select IndustryID,count(IndustryID) count,max(IndustryName) cname  from CRM_Customers cc left join CRM_Industry ci on cc.IndustryID=ci.ID where EmployeeID='" + WX.Main.CurUser.UserID + "' and IndustryID is not null group by IndustryID");
            for (int i = 0; i < Industrydt.Rows.Count; i++)
            {
                Literal48.Text += (i > 0 ? "，" : "") + Industrydt.Rows[i]["cname"] + Industrydt.Rows[i]["count"] + "个";
            }
        }
    }
}