using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace wwwroot.Manage.CRM
{
    public partial class Crm_SingleM_ShowCustomerBusiness : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string mode = Request.QueryString["PageMode"];
                if (mode == "my")
                {
                    this.lblTitle.Text = "我的客户";
                    this.MenuBar1.Key = "MyCustomer-Modi";
                }
                else if (mode == "manager")
                {
                    this.lblTitle.Text = "我的管理";
                    this.MenuBar1.Key = "Customer-Modi";
                }
                else
                {
                    this.lblTitle.Text = "我的管理";
                    this.MenuBar1.Key = "Customer-Modi-Maintain";
                }
               // Response.Write(Request["CustomerID"].Trim().Length); return;
                if (Request["CustomerID"] != null && Request["CustomerID"].Trim().Length>0)
                {
                    this.MenuBar1.Param1 = "{Q:CustomerID}";
                    WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(WX.Request.rCustomerID);
                    Literal1.Text = customer.CustomerID.ToString();
                    Literal2.Text = customer.CustomerName.ToString();
                    Literal3.Text = ULCode.QDA.XSql.GetDataTable("select StageName from CRM_Stage where ID="+customer.StageID.ToString()).Rows[0][0].ToString();
                }
                else if (Request["UserID"] != null && Request["UserID"] != "")
                {
                    div1.Visible = false;
                    div2.InnerHtml = "当前查看用户：<b>"+WX.CommonUtils.GetRealNameListByUserIdList(Request["UserID"])+"</b>";
                }
                else
                {
                    div1.Visible = false;
                    div2.Visible = false;
                }
                pageInit(true);
            }
        }
        private void pageInit(bool start)
        {
            string wherestr = " where 1=1";
            //if (Request.QueryString["PageMode"] == "my")
            //{
            //    wherestr += " and ct.UserID='" + WX.Main.CurUser.UserID + "'";
            //}
            if (Request["CustomerID"] != null && Request["CustomerID"] != "")
            {
                wherestr += " and ct.CustomerID=" + Request["CustomerID"];
            }
            else
            {
                if (Request["UserID"] != null && Request["UserID"] != "")
                {
                    WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel(Request["UserID"]);
                    wherestr += " and tu.DepartmentID=" + usermodel.DepartmentID.ToString();
                }
                else
                {
                        WX.Main.CurUser.LoadMyDepartment();
                    if (WX.CommonUtils.GetBossUserID != WX.Main.CurUser.UserID && WX.Main.CurUser.MyDepartMent.ID.ToString() != System.Configuration.ConfigurationManager.AppSettings["Dept_CA"])
                    {
                        string ids = WX.Main.GetUserDeptids(WX.Main.CurUser.UserID);
                        wherestr += " and tu.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ")";
                    }
                }
            }

            if (Request["fee"] != null && Request["fee"] == "1")
            {
                wherestr += " and ct.Fee>0 and ct.State=1";
            }
            if (Request["UserID"] != null && Request["UserID"] != "")
                wherestr +=" AND cc.EmployeeID='" + Request["UserID"] + "'";
            string sql = "select ct.*,tu.RealName,cc.CustomerName from CRM_Track ct left join CRM_Customers cc on ct.CustomerID=cc.ID left join TU_Users tu on ct.UserID=tu.UserID";
            if (Request["type"] != null && Request["type"] =="1")
            {
                wherestr += " and ProcessState<5";
                this.MenuBar1.CurIndex = 3;
            }
            else if(Request["type"] != null && Request["type"] =="2")
            {
                wherestr += " and ProcessState>4 and ProcessState<9";
                this.MenuBar1.CurIndex =4;
            }
            else if (Request["type"] != null && Request["type"] == "3")
            {
                this.MenuBar1.CurIndex = 5;
                wherestr += " and ProcessState>8";
            }
            var supplierData = WX.Main.GetPagedRows(sql+wherestr, 0, "ORDER BY TrackTime desc", 50, AspNetPager1.CurrentPageIndex);
            System.Data.DataTable dataTable = supplierData;
            Gv_customer.DataSource = dataTable;
            Gv_customer.DataBind();
            if (Gv_customer.Rows.Count > 0)
            {
                Gv_customer.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_customer.HeaderStyle.Height = Unit.Pixel(40);
            }

            this.AspNetPager1.AlwaysShow = true;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql + wherestr);
                this.AspNetPager1.PageSize = 50;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            else
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            pageInit(false);
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
                if(arr_remark.Length>=1&&!String.IsNullOrEmpty(arr_remark[0]))
                   sbRemarks.AppendFormat("目标预测：{0}", arr_remark[0]);
                if(arr_remark.Length>=2&&!String.IsNullOrEmpty(arr_remark[1]))
                   sbRemarks.AppendFormat("<br/>难&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;点：{0}", arr_remark[1]);
                if(arr_remark.Length>=3&&!String.IsNullOrEmpty(arr_remark[2]))
                   sbRemarks.AppendFormat("<br/>解决方法：{0}", arr_remark[2]);
                if(arr_remark.Length>=4&&!String.IsNullOrEmpty(arr_remark[3]))
                   sbRemarks.AppendFormat("<br/>目标达成：{0}", arr_remark[3]);
                return sbRemarks.ToString();
            }
        }
        protected void Gv_customer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                WX.CRM.Track.MODEL track = WX.CRM.Track.NewDataModel(e.CommandArgument.ToString());
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(track.CustomerID.ToString());
                WX.Main.ExecuteDelete("CRM_CustomerProgram", "TrackID", track.id.ToString());
                track.Delete();
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(), customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 11, "删除“" + WX.CRM.Track.ProcessState[track.ProcessState.ToInt32()] + "”");
            }
            else
            {
                WX.CRM.Track.MODEL track = WX.CRM.Track.NewDataModel(e.CommandArgument.ToString());
                track.State.value = 1;
                track.TrackTime.value = DateTime.Now;
                track.Update();
            }
            pageInit(false);
        }
    }
}