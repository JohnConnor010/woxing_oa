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
    public partial class Crm_My_CustomerInfo : System.Web.UI.Page
    {
        public string tablestr = "";

        private string GetCustomerNames(DataTable dtCustomers)
        { 
            StringBuilder sb=new StringBuilder();
            foreach (DataRow dr in dtCustomers.Rows)
            {
                if (sb.Length != 0) sb.Append("　");
                sb.AppendFormat("<a class='customer' href=\"javascript:personView({1})\">{0}</a>", dr["CustomerName"], dr["ID"]);
            }
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadHelperInfo();
            
            DataTable dtcount = ULCode.QDA.XSql.GetDataTable("exec Get_CRM_Tongji '"+WX.Main.CurUser.UserID+"',"+WX.Main.CurUser.UserModel.DepartmentID.ToInt32());
            limycount.Text = dtcount.Rows[0]["mycount"].ToString();
            limyTempcount.Text = dtcount.Rows[0]["myTempcount"].ToString();

            string sql = "SELECT  case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName FROM CRM_CustomersTemp where State>-1 and EmployeeID='"+WX.Main.CurUser.UserID+"'" 
                      + " union all "
                      + " SELECT  case CustomerZJM when '' then CustomerName else CustomerZJM end CustomerName FROM CRM_ContactTemp as cctemp left join CRM_Customers AS C  on cctemp.CustomerID=C.ID "
                       + " where cctemp.CheckState>-1 and  C.EmployeeID='"+WX.Main.CurUser.UserID+"'" ;
            ULCode.QDA.XDataTable tempdt = ULCode.QDA.XSql.GetXDataTable(sql);
            Literal1.Text = tempdt.ToColValueList("，", 0);
            Literal3.Text = dtcount.Rows[0]["myGTcount"].ToString();

            DataTable gtdt = ULCode.QDA.XSql.GetDataTable("select top 10 CustomerZJM  CustomerName,ID from CRM_Customers where EmployeeID='" + WX.Main.CurUser.UserID + "' and StageID<2 and State>0");
            Literal4.Text = this.GetCustomerNames(gtdt);
            Literal6.Text = dtcount.Rows[0]["myGZcount"].ToString();

            DataTable gzdt = ULCode.QDA.XSql.GetDataTable("select top 10 CustomerZJM  CustomerName,ID from CRM_Customers where EmployeeID='" + WX.Main.CurUser.UserID + "' and StageID=2 and State>0");
            Literal7.Text = this.GetCustomerNames(gzdt);
            Literal9.Text = dtcount.Rows[0]["myQYcount"].ToString();

            DataTable qydt = ULCode.QDA.XSql.GetDataTable("select top 10 CustomerZJM  CustomerName,ID from CRM_Customers where EmployeeID='" + WX.Main.CurUser.UserID + "' and StageID=3 and State>0");
            Literal10.Text = this.GetCustomerNames(qydt);
            Literal12.Text = dtcount.Rows[0]["myWHcount"].ToString();

            DataTable whdt = ULCode.QDA.XSql.GetDataTable("select top 10 CustomerZJM  CustomerName,ID from CRM_Customers where EmployeeID='" + WX.Main.CurUser.UserID + "' and StageID=4 and State>0");
            Literal13.Text = this.GetCustomerNames(whdt);
            
            Literal15.Text = dtcount.Rows[0]["cmpmonthcount"].ToString();
            Literal16.Text = dtcount.Rows[0]["deptmonthcount"].ToString();
            Literal17.Text = dtcount.Rows[0]["mymonthcount"].ToString();
            Literal18.Text = dtcount.Rows[0]["cmpmonthQYcount"].ToString();
            Literal19.Text = dtcount.Rows[0]["deptmonthQYcount"].ToString();
            Literal20.Text = dtcount.Rows[0]["mymonthQYcount"].ToString();
            Literal21.Text = dtcount.Rows[0]["cmpmonthGZcount"].ToString();
            Literal22.Text = dtcount.Rows[0]["deptmonthGZcount"].ToString();
            Literal23.Text = dtcount.Rows[0]["mymonthGZcount"].ToString();
            Literal24.Text = dtcount.Rows[0]["cmpmonthTrackcount"].ToString();
            Literal25.Text = dtcount.Rows[0]["deptmonthTrackcount"].ToString();
            Literal26.Text = dtcount.Rows[0]["mymonthTrackcount"].ToString();
            Literal27.Text =  string.Format("{0:C2}", dtcount.Rows[0]["cmpmonthTrackfee"]);
            Literal28.Text = string.Format("{0:C2}",dtcount.Rows[0]["deptmonthTrackfee"]);
            Literal29.Text = string.Format("{0:C2}",dtcount.Rows[0]["mymonthTrackfee"]);

            Literal30.Text = dtcount.Rows[0]["cmpyearcount"].ToString();
            Literal35.Text = dtcount.Rows[0]["deptyearcount"].ToString();
            Literal40.Text = dtcount.Rows[0]["myyearcount"].ToString();
            Literal31.Text = dtcount.Rows[0]["cmpyearQYcount"].ToString();
            Literal36.Text = dtcount.Rows[0]["deptyearQYcount"].ToString();
            Literal41.Text = dtcount.Rows[0]["myyearQYcount"].ToString();
            Literal32.Text = dtcount.Rows[0]["cmpyearGZcount"].ToString();
            Literal37.Text = dtcount.Rows[0]["deptyearGZcount"].ToString();
            Literal42.Text = dtcount.Rows[0]["myyearGZcount"].ToString();
            Literal33.Text = dtcount.Rows[0]["cmpyearTrackcount"].ToString();
            Literal38.Text = dtcount.Rows[0]["deptyearTrackcount"].ToString();
            Literal43.Text = dtcount.Rows[0]["myyearTrackcount"].ToString();
            Literal34.Text = string.Format("{0:C2}",dtcount.Rows[0]["cmpyearTrackfee"]);
            Literal39.Text = string.Format("{0:C2}",dtcount.Rows[0]["deptyearTrackfee"]);
            Literal44.Text = string.Format("{0:C2}",dtcount.Rows[0]["myyearTrackfee"]);

            DataTable categorydt = ULCode.QDA.XSql.GetDataTable("select CategoryID,count(CategoryID) count,max(CategoryName) cname  from CRM_Customers cc left join CRM_InnerCategory cic on cc.CategoryID=cic.ID where EmployeeID='"+WX.Main.CurUser.UserID+"' and CategoryID is not null group by CategoryID ");
            for (int i = 0; i < categorydt.Rows.Count; i++)
            {
                Literal45.Text +=(i>0?"，":"")+ categorydt.Rows[i]["cname"] + categorydt.Rows[i]["count"] + "个";
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
        private void LoadHelperInfo()
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select cc.ID,ContactName,BabySex,cc.CustomerID,CustomerName from CRM_Contact cc inner join CRM_Customers ccu on cc.CustomerID=ccu.ID where ccu.EmployeeID='" + WX.Main.CurUser.UserID + "' and datediff(day,BabyBirthday,getdate())=0 and (BabySex='男' or BabySex='女')");
            int no = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tablestr += "<tr><th style=\"width: 30px;\"></th><td> " + no + "、" + dt.Rows[i]["CustomerName"] + dt.Rows[i]["ContactName"] + (dt.Rows[i]["BabySex"].ToString() == "男" ? "儿子" : "女儿") + "今天生日！</td><td class=\"manage\" style=\"width: 100px;\"><a href=\"Crm_Single_AddContact.aspx?PageMode=my&Action=Edit&&ContactID=" + dt.Rows[i]["ID"] + "&CustomerID=" + dt.Rows[i]["CustomerID"] + "\">维护</a></td></tr>";
                no++;
            }

            DataTable dt2 = ULCode.QDA.XSql.GetDataTable("select ct.ID,ct.ProcessState,cc.CustomerID CustomerNo,cc.CustomerName from CRM_Track ct left join CRM_Customers cc on ct.CustomerID=cc.ID where ct.UserID='" + WX.Main.CurUser.UserID + "'  and datediff(day,TrackTime,getdate())>=0 and ct.State=0");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                tablestr += "<tr><th style=\"width: 30px;\"></th><td> " + no + "、" + dt2.Rows[i]["CustomerName"] + "今日应该" + WX.CRM.Track.ProcessState[Convert.ToInt32(dt2.Rows[i]["ProcessState"])] + "！</td><td class=\"manage\" style=\"width: 100px;\"><a href=\"javascript:PopupIFrame('CRM_SingleM_EditTrack.aspx?TrackID=" + dt2.Rows[i]["ID"] + "','提交跟踪信息','','',700,450)\">维护</a></td></tr>";
                no++;
            }
            if (String.IsNullOrEmpty(tablestr))
            {
                tablestr = "<tr><td style='padding-left:30px;color:red;'>还没有任何提示！</td></tr>";
            }            
            this.liHelper.Text = tablestr;
        }
    }
}