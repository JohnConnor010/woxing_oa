﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Share_Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                string userId = WX.Authentication.GetUserID();
                this.InitDropDownList();
                this.InitCustomerRepeater(true);
            }
        }
          public void InitDropDownList()
        {
            WX.Data.Dict.BindListCtrl_InnerCategory(this.ddlCustomerCategory, null, "#所有内部分类", null);
            WX.Data.Dict.BindListCtrl_CompanyNature(this.ddlCompanyNature, null, "#所有企业性质", null);
            WX.Data.Dict.BindListCtrl_Source(this.ddlSource, null, "#所有来源", null);
            WX.Data.Dict.BindListCtrl_Industry(this.ddlIndustry, null, "#所有行业", null);
            WX.Data.Dict.BindListCtrl_BusinessLevel(this.ddlBusinessLevel, null, "#所有合作分类", null);
        }
          private void InitCustomerRepeater(bool start)
          {
              StringBuilder sqlBuilder = new StringBuilder();
              if (!string.IsNullOrEmpty(this.txtCustomerName.Text))
                  sqlBuilder.Append(" AND C.CustomerName like '%" + this.txtCustomerName.Text.Trim() + "%'");
              if (this.ddlCustomerCategory.SelectedValue != "")
                  sqlBuilder.Append(" AND C.CategoryID=" + this.ddlCustomerCategory.SelectedValue);
              if (this.ddlCompanyNature.SelectedValue != "")
                  sqlBuilder.Append(" AND C.NatureId=" + this.ddlCompanyNature.SelectedValue);
              if (this.ddlSource.SelectedValue != "")
                  sqlBuilder.Append(" AND C.SourceID=" + this.ddlSource.SelectedValue);
              if (this.ddlIndustry.SelectedValue != "")
                  sqlBuilder.Append(" AND C.IndustryID=" + this.ddlIndustry.SelectedValue);
              if (this.ddlBusinessLevel.SelectedValue != "")
                  sqlBuilder.Append(" AND C.BusinessLevel=" + this.ddlBusinessLevel.SelectedValue);
              string sql = "SELECT C.ID,C.CustomerID,C.StageId,C.CustomerName,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CS.SourceName,CB.LevelName,CStage.StageName,tu2.RealName FROM CRM_Customers AS C "
                         + " INNER JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID "
                         + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                         + " Left join CRM_Source As CS On C.SourceId=CS.Id"
                         + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                         + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                         + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id"
                         + " Left Join TU_Users As tu2 On C.EmployeeID=tu2.UserID"
                         + " where C.State>-1 and IsShare=1" + sqlBuilder.ToString();
              DataTable dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID desc", this.AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
              var Customers = dataTable.AsEnumerable().Select(customer => new
              {
                  ID = customer.Field<Nullable<int>>("ID"),
                  CustomerID = customer.Field<string>("CustomerID"),
                  StageID = customer.Field<Nullable<int>>("StageID"),
                  CustomerName = customer.Field<string>("CustomerName"),
                  CustomerCategory = customer.Field<string>("CategoryName"),
                  CompanyNature = customer.Field<string>("CompanyNature"),
                  SourceName = customer.Field<string>("SourceName"),
                  LevelName = customer.Field<string>("LevelName"),
                  IndustryName = customer.Field<string>("IndustryName"),
                  StageName = customer.Field<string>("StageName"),
                  RealName = customer.Field<string>("RealName")
              }); 
              if (start)
              {
                  this.AspNetPager1.AlwaysShow = true;
                  this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                  this.AspNetPager1.PageSize = 20;
                  this.AspNetPager1.CurrentPageIndex = 1;
              }
           
              this.CustomerRepeaterShare.DataSource = Customers;
              this.CustomerRepeaterShare.DataBind();
          }
          protected void btnSearchAll_Click(object sender, EventArgs e)
          {
              this.txtCustomerName.Text = String.Empty;
              this.ddlBusinessLevel.SelectedValue = String.Empty;
              this.ddlCompanyNature.SelectedValue = String.Empty;
              this.ddlCustomerCategory.SelectedValue = String.Empty;
              this.ddlIndustry.SelectedValue = String.Empty;
              this.ddlSource.SelectedValue = String.Empty;
              this.InitCustomerRepeater(true);
          }
          protected void AspNetPager1_PageChanged(object sender, EventArgs e)
          {
              string userId = WX.Authentication.GetUserID();
              int pageIndex = this.AspNetPager1.CurrentPageIndex;
              ProcessPaged(userId);
          }
          protected void btnSearch_Click(object sender, EventArgs e)
          {
              this.InitCustomerRepeater(true);
          }
          private void ProcessPaged(string userId)
          {
              InitCustomerRepeater(false);
          }
    }
}