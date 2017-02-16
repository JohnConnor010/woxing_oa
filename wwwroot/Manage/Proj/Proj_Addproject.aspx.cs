using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_Addproject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                if (Request["id"] != null)
                {
                    WX.PRO.Project.MODEL model = WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" + Request["id"]);
                    if (model != null)
                    {
                        ui_Name.Text = model.ProjectName.ToString();
                        ui_days.Text = model.Days.ToString();
                        if (Convert.ToInt32(model.Persons.ToString()) > 0)
                        {
                            ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select pu.UserID,te.RealName from PRO_User pu left join TU_Users te on pu.UserID=te.UserID where pu.type=1 and pid=" + model.ID.ToString());
                            ui_Persons.Value = xdt.ToColValueList(",",0);
                            li_Persons.Text = xdt.ToColValueList(",", 1);
                        }
                        //ui_Persons.Value = model.Persons.ToString();
                        ui_fee.Text = model.Fee.ToString();
                        ui_content.Value = model.Content.ToString();
                        ui_Imagine.Text = model.Imagine.ToString();
                        if (model.Annex.ToString() != "")
                            li_annex.Text = "<a href='" + model.Annex.ToString() + "'>" + model.Annex.ToString() + "</a> <br/>";
                        if (ULCode.QDA.XSql.GetDataTable("select ID from PRO_Process where ProjID=" + model.ID.ToString()).Rows.Count > 0)
                            Button2.Visible = true;
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            WX.PRO.Project.MODEL model = WX.PRO.Project.NewDataModel();
            if (Request["id"] != null)
            {
                model = WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" + Request["id"]);
            }
            model.ProjectName.value = ui_Name.Text.Trim();
            model.Days.value = ui_days.Text.Trim();
            model.Fee.value = ui_fee.Text.Trim();
            model.Content.value = ui_content.Value.Trim();
            string[] userarry = ui_Persons.Value.Trim().Split(',');
            //4.业务处理过程
            model.Persons.value = ui_Persons.Value != null ? userarry.Length : 0;
            string fileDir = "";
            if (FileUpload1.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (!".rar.zip.doc.docx.ppt".Contains(fileExtension))
                {
                    ULCode.Debug.Alert(this, "照片格式必须为.rar.zip.doc.docx.ppt！");
                    return;
                }
                fileDir = "/UploadFiles/Proj/" + model.ProjectName.ToString() + DateTime.Now.ToString("-yyyyMMddHHmmss") + fileExtension;
                try
                {
                    FileUpload1.SaveAs(Server.MapPath(fileDir));
                }
                catch
                {
                    fileDir = "";
                }
            }
            if (fileDir != "")
            {
                model.Annex.value = fileDir;
            }
            model.Imagine.value = ui_Imagine.Text.Trim();
            model.State.value = ((Button)sender).ID == "Button1" ? 0 : 1;
            int pid = 0;
            string logstr = model.ProjectName.ToString() + "-添加";
            if (Request["id"] != null)
            {
                pid = Convert.ToInt32(model.ID.value);
                logstr = model.ProjectName.ToString() + "-修改";
                model.Update();
            }
            else
            {
                model.UserID.value = WX.Main.CurUser.UserID;
                pid = model.Insert(true);
            }
            if (ui_Persons.Value != null)
            {
                ULCode.QDA.XSql.Execute("delete from PRO_User where type=1 and pid=" + pid);
                for (int i = 0; i < userarry.Length; i++)
                {
                    WX.PRO.User.MODEL usermodel = WX.PRO.User.NewDataModel();
                    usermodel.PID.value = pid;
                    usermodel.type.value = 1;
                    usermodel.UserID.value = userarry[i];
                    usermodel.Insert();
                }
            }
            //5.登记日志
            WX.PRO.Log.AddLog(0, pid, logstr + (((Button)sender).ID == "Button1" ? "" : "并申请"), Request.UserHostAddress);
            //6.返回处理结果或返回其它页面。
            Response.Redirect("Proj_Project.aspx");

        }
    }
}