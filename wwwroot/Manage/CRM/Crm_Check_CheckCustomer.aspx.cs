using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace wwwroot.Manage.CRM
{
    public partial class Crm_Check_CheckCustomer : System.Web.UI.Page
    {
        string ids = "";
        protected void Page_Load(object sender, EventArgs e)
        {ids = WX.Main.GetUserDeptids(WX.Main.CurUser.UserID);
            if (!IsPostBack)
            {
                string userId = WX.Authentication.GetUserID();
                this.InitCustomerRepeater();
                InitRecycle();
                InitTrack();
            }
        }
        private void InitRecycle()
        {
            string sql = "SELECT C.ID,C.CustomerID,C.CustomerName,tu.RealName,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CB.LevelName,CStage.StageName,C.State,1 checktype,0 ContactID FROM CRM_Customers AS C "
                // + " inner JOIN CRM_Customers AS Customers ON C.CustomersID=Customers.ID "
                      + " left JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID "
                      + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                      + " left join TU_Users tu on C.EmployeeID=tu.UserID"
                      + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                      + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                      + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id where C.State=3" + (WX.Main.CurUser.UserID == WX.CommonUtils.GetBossUserID || ids == "" ? "" : " and tu.DepartmentID in(" + ids + ")");
            GridView1.DataSource = ULCode.QDA.XSql.GetDataTable(sql);
            GridView1.DataBind();
        }
        private void InitTrack()
        {
            string sql = "select ct.*,tu.RealName,cc.CustomerName,cc.CustomerID from CRM_Track ct left join CRM_Customers cc on ct.CustomerID=cc.ID left join TU_Users tu on cc.EmployeeID=tu.UserID where ct.State=1 and ct.Type=0" + (WX.Main.CurUser.UserID == WX.CommonUtils.GetBossUserID || ids == "" ? "" : " and tu.DepartmentID in(" + ids + ")") + " order by TrackTime desc";
            GridView2.DataSource = ULCode.QDA.XSql.GetDataTable(sql);
            GridView2.DataBind();
        }
        public string GetMemo(object oRemarks)
        {
            if (oRemarks == null || oRemarks == Convert.DBNull)
                return null;
            else
            {
                string sRemarks = Convert.ToString(oRemarks);
                string[] arr_remark = sRemarks.Split('|');
                StringBuilder sbRemarks = new StringBuilder();
                if (arr_remark.Length >= 1 && !String.IsNullOrEmpty(arr_remark[0]))
                    sbRemarks.AppendFormat("目标预测：{0}", arr_remark[0]);
                if (arr_remark.Length >= 2 && !String.IsNullOrEmpty(arr_remark[1]))
                    sbRemarks.AppendFormat("<br/>难&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;点：{0}", arr_remark[1]);
                if (arr_remark.Length >= 3 && !String.IsNullOrEmpty(arr_remark[2]))
                    sbRemarks.AppendFormat("<br/>解决方法：{0}", arr_remark[2]);
                if (arr_remark.Length >= 4 && !String.IsNullOrEmpty(arr_remark[3]))
                    sbRemarks.AppendFormat("<br/>目标达成：{0}", arr_remark[3]);
                return sbRemarks.ToString();
            }
        }
        private void InitCustomerRepeater()
        {
            WX.Main.CurUser.LoadDutyDetailUser();
            string sql = "SELECT C.ID,C.CustomerID,C.CustomerName,tu.RealName,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CB.LevelName,CStage.StageName,C.CustomersID,C.State,1 checktype,0 ContactID FROM CRM_CustomersTemp AS C "
                      // + " inner JOIN CRM_Customers AS Customers ON C.CustomersID=Customers.ID "
                       + " left JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID "
                       + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                       + " left join TU_Users tu on C.EmployeeID=tu.UserID"
                       + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                       + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                       + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id where C.State>-1" + (WX.Main.CurUser.UserID == WX.CommonUtils.GetBossUserID || ids == "" ? "" : " and tu.DepartmentID in(" + ids + ")")
                       + " union all "
                       + " SELECT cctemp.ID,C.CustomerID,C.CustomerName,tu.RealName,CA.CategoryName,CN.CompanyNature,CI.IndustryName,"
                        + " CB.LevelName,CStage.StageName,cctemp.CustomerID,cctemp.State State,2 checktype,cctemp.ContactID FROM CRM_ContactTemp as cctemp"
                        + " left join CRM_Customers AS C  on cctemp.CustomerID=C.ID"
                        + " left join TU_Users tu on C.EmployeeID=tu.UserID"
                        + " left JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID"
                        + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                        + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                        + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                        + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id"
                        + " where cctemp.CheckState>-1" + (WX.Main.CurUser.UserID==WX.CommonUtils.GetBossUserID|| ids == "" ? "" : " and tu.DepartmentID in(" + ids + ")") + " ORDER BY CustomersID,ID desc";
            System.Data.DataTable dataTable = ULCode.QDA.XSql.GetDataTable(sql);
            Gv_customer.DataSource = dataTable;
            Gv_customer.DataBind();
            if (Gv_customer.Rows.Count > 0)
            {
                Gv_customer.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_customer.HeaderStyle.Height = Unit.Pixel(40);
            }
        }

        protected void Gv_customer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del3")
            {
            }
            else
            {
                WX.CRM.Track.MODEL track = WX.CRM.Track.NewDataModel(e.CommandArgument.ToString());
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(track.CustomerID);

                track.Type.value = e.CommandName == "del" ? 1 : -1;

                track.Update();
                WX.CRM.Customer.AddLog(track.CustomerID.ToInt32(), customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 14, WX.CRM.Track.ProcessState[track.ProcessState.ToInt32()] + "--" + e.CommandName == "del" ? "通过" : "未通过");

                InitTrack();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WX.CRM.Customer.MODEL model = WX.CRM.Customer.NewDataModel(e.CommandArgument);
            if (e.CommandName == "state1")
            {
                model.State.value = -1;
            }
            else
            {
                model.State.value = 2;
            }
            model.UpTime.value = DateTime.Now;
            model.Update();
            WX.CRM.Customer.AddLog(model.ID.ToInt32(), model.CustomerName.ToString(), WX.Main.CurUser.UserID, 13, model.State.ToInt32()==2?"不通过":"通过");
            InitRecycle();

        }

    }
}