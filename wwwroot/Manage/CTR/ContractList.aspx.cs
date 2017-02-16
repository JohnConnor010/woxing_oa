using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using WX;
using System.Text;

namespace wwwroot.Manage.CTR
{
    public partial class ContractList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "SELECT A.*,B.Name,P.ProjectName FROM CTR_Contracts AS A LEFT JOIN CTR_Category AS B ON A.CategoryID=B.ID LEFT JOIN PRO_Projects AS P ON A.ProjectID=P.ID WHERE A.ID > 0 ";
                InitComponent(true,sql);
            }
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            int row = XSql.Execute("DELETE FROM CTR_Contracts WHERE ID=" + id);
            if (row > 0)
            {
                WX.Main.AddLog(LogType.Default, "合同删除成功！", null);
                ULCode.Debug.Alert("合同删除成功！", "ContractList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("合同删除失败！", "ContractList.aspx");
            }
        }
        private void InitComponent(bool start, string sql)
        {
            DataTable contractCategoryData = XSql.GetDataTable("SELECT * FROM CTR_Category");
            this.ddlCategory.DataSource = contractCategoryData;
            this.ddlCategory.DataValueField = "ID";
            this.ddlCategory.DataTextField = "Name";
            this.ddlCategory.DataBind();
            this.ddlCategory.Items.Insert(0, new ListItem("--所有类别--", ""));

            DataTable projectData = XSql.GetDataTable("SELECT * FROM Pro_Projects");
            this.ddlProject.DataSource = projectData;
            this.ddlProject.DataValueField = "ID";
            this.ddlProject.DataTextField = "ProjectName";
            this.ddlProject.DataBind();
            this.ddlProject.Items.Insert(0, new ListItem("--所有项目--", ""));


            DataTable contractData = WX.Main.GetPagedRows(sql, 0, "ORDER BY ContractID", 2, AspNetPager1.CurrentPageIndex);
            if (contractData == null)
            {
                Response.Write("aaa");
                Response.End();
            }
            var contracts = contractData.AsEnumerable().Select(c => new
                {
                    ID = c.Field<int>("ID"),
                    ContractID = c.Field<string>("ContractID"),
                    ContractName = c.Field<string>("ContractName"),
                    CategoryName = c.Field<string>("Name"),
                    ProjectName = c.Field<string>("ProjectName"),
                    Amount = String.Format("{0:0,0.00}", c.Field<decimal>("ContractAmount")),
                    Currency = c.Field<string>("Currency"),
                    SignedDate = c.Field<DateTime>("SignedDate").ToString("yyyy-MM-dd"),
                    EndDate = c.Field<DateTime>("EndDate").ToString("yyyy-MM-dd"),
                    ReceivablesPayment = c.Field<string>("ReceivablesPayment"),
                    Implementation = c.Field<string>("Implementation")
                });
            this.ContractListView.DataSource = contracts;
            this.ContractListView.DataBind();
            this.AspNetPager1.AlwaysShow = true;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 2;
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
            string sql = "SELECT A.*,B.Name,P.ProjectName FROM CTR_Contracts AS A LEFT JOIN CTR_Category AS B ON A.CategoryID=B.ID LEFT JOIN PRO_Projects AS P ON A.ProjectID=P.ID WHERE A.ID > 0 ";
            InitComponent(false, sql);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlCategory.SelectedItem.Value != "")
            {
                sqlBuilder.Append(" AND CategoryID=" + this.ddlCategory.SelectedItem.Value);
            }
            if (this.ddlProject.SelectedItem.Value != "")
            {
                sqlBuilder.Append(" AND ProjectID=" + this.ddlProject.SelectedItem.Value);
            }
            if (this.ddlImplementation.SelectedItem.Value != "")
            {
                sqlBuilder.Append(" AND Implementation='" + this.ddlImplementation.SelectedItem.Value + "'");
            }
            string sql = "SELECT A.*,B.Name,P.ProjectName FROM CTR_Contracts AS A LEFT JOIN CTR_Category AS B ON A.CategoryID=B.ID LEFT JOIN PRO_Projects AS P ON A.ProjectID=P.ID WHERE A.ID > 0 " + sqlBuilder.ToString();
            InitComponent(false, sql);
        }
    }
}