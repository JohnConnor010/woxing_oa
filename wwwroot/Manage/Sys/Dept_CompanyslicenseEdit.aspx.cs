using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.Manage.Sys
{
    public partial class Dept_CompanyslicenseEdit : System.Web.UI.Page
    {
        WX.Model.CompanyLicense.MODEL model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["del"] != null)
                {
                    Button2.Visible = true;
                }
                WX.Data.Dict.BindListCtrl_DeptList(this.ui_DepartentID, null, "", "");
                WX.Data.Dict.BindListCtrl_DeptList(this.ui_CheckDepartentID, null, "", "");
                for (int i = 0; i < WX.Model.Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(WX.Model.Employee.eduarray[i], WX.Model.Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                if (Request["LicenseID"] != null)
                {
                    model = WX.Model.CompanyLicense.GetRequestedModel();
                    ui_title.Text = model.Title.ToString();
                    ui_type.SelectedValue = model.Type.ToString();
                    ui_content.Text = model.Content.ToString();
                    ui_Addtime.Text = ((DateTime)model.Addtime.value).ToString("yyyy-MM-dd");
                    ui_LNO.Text = model.LNO.ToString();
                    ui_Ischeck.SelectedValue = model.Ischeck.ToString();
                    try
                    {
                        ui_Checktime.Text = Convert.ToDateTime(model.Checktime.ToString()).ToString("yyyy-MM-dd");
                    }
                    catch { } try
                    {
                        ui_Checkstoptime.Text = Convert.ToDateTime(model.Checkstoptime.ToString()).ToString("yyyy-MM-dd");
                    }
                    catch { } try
                    {
                        ui_Valid.Text = Convert.ToDateTime(model.Valid.ToString()).ToString("yyyy-MM-dd");
                    }
                    catch { } try
                    {
                        ui_Validstop.Text = Convert.ToDateTime(model.Validstop.ToString()).ToString("yyyy-MM-dd");
                    }
                    catch { }
                    ui_Checkdata.Text = model.Checkdata.ToString();
                    ui_Warn.Text = model.Warn.ToString();
                    ui_CheckDepartentID.SelectedValue = model.CheckDepartentID.ToString();
                    ui_CheckManage.Value = model.CheckManage.ToString();
                    li_CheckManage.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.CheckManage.ToString());
                    ui_Unit.Text = model.Unit.ToString();
                    ui_DepartentID.SelectedValue = model.DepartentID.ToString();
                    ui_Manage.Value = model.Manage.ToString();
                    li_Manage.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Manage.ToString());
                    //if (ui_type.SelectedValue == "3")
                    //{
                    //    WX.Model.Company_Partner.MODEL partner = WX.Model.Company_Partner.GetModel("select * from [TE_Companys_Partner] where Id=" + model.PartnerUID.ToString());
                    //    ui_RealName.Text = partner.RealName.ToString();
                    //    ui_sex.SelectedValue = Convert.ToBoolean(partner.Sex.ToString()) ? "1" : "0";
                    //    ui_PoliticalL.Text = partner.PoliticalL.ToString();
                    //    ui_edu.SelectedValue = partner.Edu.ToString();
                    //    ui_LorS.SelectedValue = model.LorS.ToString();
                    //}
                    MenuBar1.CurIndex = (int)model.Type.value + 2;
                    string[] annexarry = model.Annex.ToString().Split(',');
                    for (int i = 0; i < annexarry.Length; i++)
                    {
                        Label lit = (Label)Literal0.Parent.FindControl("Literal" + i);
                        if (annexarry[i] != "")
                        {
                            lit.Text = "<a href='Dept_AnnexDetail.aspx?id=" + Request["id"] + "&aid=" + i + "&companyID=" + model.CompanyId.ToString() + "'>" + annexarry[i].Split('|')[0] + "</a>&nbsp;&nbsp;&nbsp;";//<a href=\"javascript:setspan('Literal" + i + "');\">删除</a>

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
                }
                else
                {
                    // ui_Addtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    MenuBar1.CurIndex = Convert.ToInt32(Request["type"]) + 2;
                    ui_type.SelectedValue = Request["type"];
                }
                if (ui_type.SelectedValue == "3")
                {
                    table2.Visible = true;
                }
                else if (ui_type.SelectedValue == "5" || ui_type.SelectedValue == "6")
                {
                    table1.Visible = false;
                    table3.Visible = false;
                }
                else
                {
                    table1.Visible = true;
                    table3.Visible = true;
                    if (ui_type.SelectedValue == "1" || ui_type.SelectedValue == "4")
                    {
                        tr_time.Visible = true;
                    }
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            model = WX.Model.CompanyLicense.NewDataModel();
            string annex = "";
            if (Request["id"] != null)
            {
                model = WX.Model.CompanyLicense.GetModel("Select * from [TE_Companys_license] where Id=" + Request["id"]);
                if (Request["del"] != null)
                {
                    ULCode.QDA.XSql.Execute("delete from [TE_Companys_license] where Id=" + Request["id"]);
                    int type2 = 3;
                    switch (ui_type.SelectedValue)
                    {
                        case "1": type2 = 3; break;
                        case "2": type2 = 4; break;
                        case "3": type2 = 5; break;
                        case "4": type2 = 7; break;
                        case "5": type2 = 8; break;
                        case "6": type2 = 9; break;
                        case "7": type2 = 10; break;
                    }
                    WX.Model.Company.AddLogs(Convert.ToInt32(model.CompanyId.ToString()), type2, model.Title.ToString() + "删除！" + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
                    Response.Redirect("Dept_Companyslicense.aspx?companyID=" + model.CompanyId.ToString() + "&type=" + model.Type.ToString());
                }
            }
            model.Title.value = ui_title.Text;
            model.Content.value = ui_content.Text;
            model.Type.value = int.Parse(ui_type.SelectedValue);
            annex += this.getannex(FileUpload0, Literal0);
            annex += this.getannex(FileUpload1, Literal1);
            annex += this.getannex(FileUpload2, Literal2);
            annex += this.getannex(FileUpload3, Literal3);
            annex += this.getannex(FileUpload4, Literal4);
            model.Annex.value = annex;

            //if (model.Type.ToString() == "1")
            //{
            model.Addtime.value = ui_Addtime.Text;
            //}
            model.LNO.value = ui_LNO.Text;
            bool flag = false;
            if (ui_type.SelectedValue == "1" || ui_type.SelectedValue == "4")
            {
                model.Ischeck.value = ui_Ischeck.SelectedValue;
                model.Checktime.value = ui_Checktime.Text;
                model.Checkstoptime.value = ui_Checkstoptime.Text;
                model.Valid.value = ui_Valid.Text;
                model.Validstop.value = ui_Validstop.Text;
                model.Checkdata.value = ui_Checkdata.Text;
                model.Warn.value = ui_Warn.Text;
                model.CheckDepartentID.value = ui_CheckDepartentID.SelectedValue;
                model.CheckManage.value = ui_CheckManage.Value;
                if (Request["ischeck"] != null && ui_Ischeck.SelectedValue == "1" && Convert.ToDateTime(model.Checkstoptime.value) > DateTime.Now)
                {
                    flag = true;
                }
            }
            model.Unit.value = ui_Unit.Text;
            model.DepartentID.value = ui_DepartentID.SelectedValue;
            model.Manage.value = ui_Manage.Value;
            string type3str = "";
            if (Request["id"] != null)
            {
                //if (ui_type.SelectedValue == "3")
                //{
                //    model.LorS.value = ui_LorS.SelectedValue;
                //    WX.Model.Company_Partner.MODEL partner = WX.Model.Company_Partner.GetModel("select * from [TE_Companys_Partner] where Id=" + model.PartnerUID.ToString());
                //    partner.RealName.value = ui_RealName.Text;
                //    partner.Sex.value = ui_sex.SelectedValue == "0" ? false : true;
                //    partner.PoliticalL.value = ui_PoliticalL.Text;
                //    partner.Edu.value = ui_edu.SelectedValue;
                //    partner.IDCard.value = model.LNO.value;
                //    partner.Update();
                //    type3str = ui_LorS.SelectedItem.Text + "(" + partner.RealName.ToString() + ")";
                //}
                model.Update();
            }
            else
            {
                model.CompanyId.value = WX.Request.rCompanyId;
                //if (ui_type.SelectedValue == "3")
                //{
                //    WX.Model.Company_Partner.MODEL partner = WX.Model.Company_Partner.NewDataModel();
                //    partner.RealName.value = ui_RealName.Text;
                //    partner.Sex.value = ui_sex.SelectedValue == "0" ? false : true;
                //    partner.PoliticalL.value = ui_PoliticalL.Text;
                //    partner.Edu.value = ui_edu.SelectedValue;
                //    partner.CompanyID.value = model.CompanyId.value;
                //    partner.IDCard.value = model.LNO.value;
                //    int uid = partner.Insert(true);
                //    model.PartnerUID.value = uid;
                //    model.LorS.value = ui_LorS.SelectedValue;
                //    type3str = ui_LorS.SelectedItem.Text + "(" + partner.RealName.ToString() + ")";
                //}
                model.Save();
            }
            //6.登记日志
            string logstr = "";
            int type = 3;
            switch (ui_type.SelectedValue)
            {
                case "1": logstr = flag ? "年审" : (Request["id"] != null ? "修改" : "添加"); type = 3; break;
                case "2": logstr = Request["id"] != null ? "修改" : "添加"; type = 4; break;
                case "3": logstr = (Request["id"] != null ? "修改" : "添加") + "-" + type3str; type = 5; break;
                case "4": logstr = flag ? "年审" : (Request["id"] != null ? "修改" : "添加"); type = 7; break;
                case "5": logstr = Request["id"] != null ? "修改" : "添加"; type = 8; break;
                case "6": logstr = Request["id"] != null ? "修改" : "添加"; type = 9; break;
                case "7": logstr = Request["id"] != null ? "修改" : "添加"; type = 10; break;
            }
            WX.Model.Company.AddLogs(Convert.ToInt32(model.CompanyId.ToString()), type, model.Title.ToString() + "-" + logstr + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
            if (ui_type.SelectedValue == "3")
            {
                Response.Redirect("User_EditUser.aspx?id=" + model.PartnerUID.ToString() + "&companyid=" + model.CompanyId.ToString());
            }
            Response.Redirect("Dept_Companyslicense.aspx?companyID=" + model.CompanyId.ToString() + "&type=" + model.Type.ToString());
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