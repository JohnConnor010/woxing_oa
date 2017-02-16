using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
namespace wwwroot.Manage.Sys
{
    public partial class Dept_CompanysPartnerEdit : System.Web.UI.Page
    {
        WX.Model.Company_Partner.MODEL model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                if (Request["del"] != null)
                {
                    Button2.Visible = true;
                }
                WX.Data.Dict.BindListCtrl_DeptList(this.ui_DepartentID, null, "", "");
                for (int i = 0; i < WX.Model.Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(WX.Model.Employee.eduarray[i], WX.Model.Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                ui_starttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (Request["id"] != null)
                {
                    model = WX.Model.Company_Partner.GetModel("Select * from [TE_Companys_Partner] where Id=" + Request["id"]);
                    ui_title.Text = model.Title.ToString();
                    ui_content.Text = model.Content.ToString();
                    ui_LNO.Text = model.LNO.ToString();

                    ui_DepartentID.SelectedValue = model.DepartentID.ToString();
                    ui_Manage.Value = model.Manage.ToString();
                    li_Manage.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Manage.ToString());
                    WX.Model.Employee.MODEL partner = WX.Model.Employee.GetModelToID( model.EmployeeID.ToString() );
                    WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(model.EmployeeID.ToString());
                    ui_RealName.Text = usermodel.RealName.ToString();
                    ui_sex.SelectedValue = Convert.ToBoolean(partner.Sex.ToString()) ? "1" : "0";
                    ui_PoliticalL.Text = model.PoliticalL.ToString();
                    ui_edu.SelectedValue = partner.Edu.ToString();
                    ui_Legal.Checked = (model.Legal.ToString() == "1");
                    ui_Shareholder.Checked = (model.Shareholder.ToString() == "1");
                    ui_Directors.Checked = (model.Directors.ToString() == "1");
                    ui_Share.Text = model.Share.ToString();
                    ui_Assets.Text = model.Assets.ToString();
                    ui_starttime.Text = Convert.ToDateTime(model.Starttime.ToString()).ToString("yyyy-MM-dd");
                    string[] annexarry = model.Annex.ToString().Split(',');
                    for (int i = 0; i < annexarry.Length; i++)
                    {
                        Label lit = (Label)Literal0.Parent.FindControl("Literal" + i);
                        if (annexarry[i] != "")
                        {
                            lit.Text = "<a href='Dept_AnnexDetail.aspx?id=" + Request["id"] + "&aid=" + i + "&companyID=" + model.CompanyID.ToString() + "'>" + annexarry[i].Split('|')[0] + "</a>&nbsp;&nbsp;&nbsp;";//<a href=\"javascript:setspan('Literal" + i + "');\">删除</a>

                        }
                        if (lit.Text == "")
                        {
                            ((Button)Literal0.Parent.FindControl("but" + i)).Visible = false;
                        }
                    }
                    for (int i = annexarry.Length - 1; i < 5; i++)
                    {
                        ((Button)Literal0.Parent.FindControl("but" + i)).Visible = false;
                    }
                    btnSelect1.Visible = false;
                    btnSelect2.Visible = false;
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Write(HiddenField1.Value+"--"+ui_ID.Value); return;
            model = WX.Model.Company_Partner.NewDataModel();
            string annex = "";

            WX.Model.Employee.MODEL partner;
            if (Request["id"] != null)
            {
                model = WX.Model.Company_Partner.GetModel("Select * from [TE_Companys_Partner] where Id=" + Request["id"]);
            }
            else if (ui_ID.Value!="")
            {
                model = WX.Model.Company_Partner.GetModel("Select * from [TE_Companys_Partner] where Id=" + ui_ID.Value);
            }
            WX.Model.User.MODEL usermodel=WX.Model.User.GetCache(model.EmployeeID.ToString());
            if (Request["del"] != null)
            {
                partner = WX.Model.Employee.GetModelToID( model.EmployeeID.ToString());

                ULCode.QDA.XSql.Execute("update [TE_Companys_Partner] set [State]=1,Stoptime=getdate() where Id=" + model.Id.ToString());
                WX.Model.Company.AddLogs(Convert.ToInt32(Request["CompanyId"]), 5, "取消" + usermodel.RealName.ToString() + "的" + WX.Model.Company_Partner.Legalarray[Convert.ToInt32(model.Legal.value)] + WX.Model.Company_Partner.Shareholderarray[Convert.ToInt32(model.Shareholder.value)] + WX.Model.Company_Partner.Directorsarray[Convert.ToInt32(model.Directors.value)] + "身份" + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
                Response.Redirect("Dept_CompanysPartner.aspx?companyID=" + model.CompanyID.ToString() );
            }
            model.Title.value = ui_title.Text;
            model.Content.value = ui_content.Text;
            annex += this.getannex(FileUpload0, Literal0);
            annex += this.getannex(FileUpload1, Literal1);
            annex += this.getannex(FileUpload2, Literal2);
            annex += this.getannex(FileUpload3, Literal3);
            annex += this.getannex(FileUpload4, Literal4);
            if (annex != "")
            {
                model.Annex.value = annex;
            }
            model.LNO.value = ui_LNO.Text;

            model.DepartentID.value = ui_DepartentID.SelectedValue;
            model.Manage.value = ui_Manage.Value;
            model.PoliticalL.value = ui_PoliticalL.Text;
            model.Directors.value = ui_Directors.Checked ? 1 : 0;
            model.Shareholder.value = ui_Shareholder.Checked ? 1 : 0;
            model.Legal.value = ui_Legal.Checked ? 1 : 0;
            if(ui_Share.Text.Trim()!="")
            model.Share.value = ui_Share.Text;
            if(ui_Assets.Text.Trim()!="")
            model.Assets.value = ui_Assets.Text;
            model.Starttime.value = ui_starttime.Text;
            string type3str = "";
            if (Request["id"] != null || ui_ID.Value != "")
            {
                partner = WX.Model.Employee.GetModelToID( model.EmployeeID.ToString() );
                usermodel = WX.Model.User.GetCache(model.EmployeeID.ToString());
                usermodel.RealName.value = ui_RealName.Text;
                partner.Sex.value = ui_sex.SelectedValue == "0" ? false : true;
                partner.Edu.value = ui_edu.SelectedValue;
                partner.IDCard.value = model.LNO.value;
                model.State.value = 0;
                usermodel.Update();
                partner.Update();
                model.Update();
            }
            else
            {
                model.CompanyID.value = Request["companyID"];
                if (HiddenField1.Value != "")
                {
                    partner = WX.Model.Employee.GetModelToID( HiddenField1.Value );
                    usermodel = WX.Model.User.GetCache(HiddenField1.Value);
                }
                else
                {
                    partner = WX.Model.Employee.NewDataModel();
                    usermodel=WX.Model.User.NewDataModel();
                    usermodel.UserID.value = Guid.NewGuid().ToString();
                    partner.UserID.value = usermodel.UserID.value;
                }
                usermodel.RealName.value = ui_RealName.Text;
                usermodel.CompanyID.value = model.CompanyID.value;
                partner.Sex.value = ui_sex.SelectedValue == "0" ? false : true;
                partner.Edu.value = ui_edu.SelectedValue;
                partner.IDCard.value = model.LNO.value;
                if (HiddenField1.Value != "")
                {
                    usermodel.Update();
                    partner.Update();
                }
                else
                {
                    usermodel.Insert();
                    usermodel.SaveIntoCaches();
                partner.Insert();
                }
                model.PoliticalL.value = ui_PoliticalL.Text;
                string uid = partner.UserID.ToString();
                model.EmployeeID.value = uid;
                model.Addtime.value = DateTime.Now;
                model.Save();
            }
            type3str =  "(" + usermodel.RealName.ToString() + ")加入时间："+ui_starttime.Text;
            //6.登记日志
            string logstr = "";
            int type = 5;
            logstr = (Request["id"] != null ? "修改" : "添加") + "-" + type3str;
            WX.Model.Company.AddLogs(Convert.ToInt32(model.CompanyID.ToString()), type,logstr+"["+ui_logcontent.Text+"]",WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
            Response.Redirect("User_EditUser.aspx?id=" + model.EmployeeID.ToString() + "&companyid=" + model.CompanyID.ToString());
        }
        private string getannex(FileUpload file, Label lit)
        {
            if (file.HasFile)
            {
                string filepath = "/UploadFiles/cmp/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath(filepath));
                return file.FileName + "|" + filepath + ",";
            }
            else
            {
                if (lit.Text != "")
                {
                    string[] annexarry = model.Annex.ToString().Split(',');
                    return annexarry[int.Parse(lit.ID.Replace("Literal", ""))] + ",";
                }
            }
            return "";
        }
        protected void but2_Click(object sender, EventArgs e)
        {
            ((Label)Literal0.Parent.FindControl("Literal" + ((Button)sender).ID.Replace("but", ""))).Text = "";
            ((Button)sender).Visible = false;
        }
    }
}