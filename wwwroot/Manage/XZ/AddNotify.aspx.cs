using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.Manage.XZ
{
    public partial class AddNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_NotifyCategoryList(ui_category, null, null, null);
                if (WX.Request.rNotifyId > 0)
                {
                    WX.XZ.Notify.MODEL model = WX.Request.rNotify;
                    ui_category.SelectedValue = model.CategoryID.ToString();
                    ui_title.Text = model.Title.ToString();
                    hidden_UserList.Value = model.Users.ToString();
                    txtUserList.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Users.ToString());
                    hidden_RoleList.Value = model.Dutys.ToString();
                    txtRoleList.Text = WX.CommonUtils.GetDutyNameListByDutyIdList(model.Dutys.ToString());
                    hidden_DepartmentList.Value = model.Depms.ToString();
                    txtDepartmentList.Text = WX.CommonUtils.GetDeptNameListByDeptIdList(model.Depms.ToString());
                    ui_starttime.Text = Convert.ToDateTime(model.Starttime.ToString()).ToString("yyyy-MM-dd");
                    ui_stoptime.Text = model.Stoptime.ToString() != "" ? Convert.ToDateTime(model.Stoptime.ToString()).ToString("yyyy-MM-dd") : "";
                    ui_ismes.Checked = model.Ismes.ToString() == "1" ? true : false;
                    ui_istop.Checked = model.Istop.ToString() == "1" ? true : false;
                    ui_content1.Value = model.Content.ToString();
                    try
                    {
                        string[] annexs = model.Annex.ToString().Split('|');
                        Annex_li.Text =annexs.Length==2? "<a target='_blank' href='" + annexs[0] + "'>" +annexs[1] + "</a><br/>":"";
                    }
                    catch { }
                }
                else
                {
                    ui_starttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
        }
        protected void SubmitData(object sender, EventArgs e)
        {
            WX.XZ.Notify.MODEL model;

            //业务处理过程
            if (WX.Request.rNotifyId > 0)
                model = WX.Request.rNotify;
            else
                model = WX.XZ.Notify.NewDataModel();
            if (this.FileUpload1.HasFile)
            {
                string annexpath = this.SavaAnnex();
                if (annexpath == "-1")
                {
                    ULCode.Debug.Alert(this, "附件格式不正确，请选择.PDF格式的文件！");
                    return;
                }
                else
                {
                    string[] annexs = model.Annex.ToString().Split('|');
                    if (annexs.Length == 2 && annexs[0] != "")
                        try
                        {
                            File.Delete(Server.MapPath(annexs[0]));
                        }
                        catch { }
                    model.Annex.value = annexpath;
                }
            }
            model.Annex.value = model.Annex.ToString() == "" ? "|" : model.Annex.ToString();
           // Response.Write(model.Annex.ToString()); return;
            //2.取得用户变量
            model.CategoryID.value = ui_category.SelectedValue;
            model.Title.value = ui_title.Text;

            model.Users.value = hidden_UserList.Value;
            model.Dutys.value = hidden_RoleList.Value;
            model.Depms.value = hidden_DepartmentList.Value;
            model.Starttime.value = ui_starttime.Text;
            if (ui_stoptime.Text != "")
            {
                model.Stoptime.value = ui_stoptime.Text;
            }
            model.Ismes.value = ui_ismes.Checked ? ui_ismes.ToolTip : "0";
            model.Istop.value = ui_istop.Checked ? 1 : 0;
            model.Content.value = ui_content1.Value;
            ////填写主要业务逻辑代码、登记日志
            if (WX.Request.rNotifyId > 0)
            {
                model.Update();
                WX.Main.AddLog(WX.LogType.Default, "公告更新成功！", String.Format("{0}", model.Title.ToString()));
            }
            else
            {
                model.UserID.value = WX.Main.CurUser.UserID;
                int id = model.Insert(true);
                WX.Main.AddLog(WX.LogType.Default, "创建公告成功！", String.Format("{0}-{1}", id, model.Title.ToString()));
            }

            //返回处理结果或返回其它页面。
            Response.Redirect("NotifyList.aspx");
        }
        public string SavaAnnex()
        {
            bool fileOK = false;
            string filepath = "/UploadFiles/Notify/";
            string path = Server.MapPath("~"+filepath);
            if (this.FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".PDF", ".pdf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
                if (fileOK)
                {

                    string houzui = Path.GetExtension(FileUpload1.FileName);
                    // 服务器上保存的文件名称                
                    string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + houzui;
                    FileUpload1.SaveAs(path + filename);
                    return filepath + filename + "|" + FileUpload1.FileName;
                }
                else
                {
                    return "-1";
                }
            }
            return "|";
        }
    }
}