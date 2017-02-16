using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.Manage.DownLoad
{
    public partial class UpLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["AnnexID"] != null && Request["AnnexID"] != "")
                {
                    WX.Down.Model.Annex.MODEL annexmodel = WX.Down.Model.Annex.NewDataModel(Request["AnnexID"]);
                    if (Request["del"] != null && annexmodel.UserID.ToString() == WX.Main.CurUser.UserID)
                    {
                        File.Delete(Server.MapPath(annexmodel.Annex.ToString()));
                        annexmodel.Delete();
                        Response.Redirect("Index.aspx");
                    }
                    else if (annexmodel.UserID.ToString() == WX.Main.CurUser.UserID)
                    {
                        TextBox1.Text = annexmodel.Demo.ToString();
                    }
                    else
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request["AnnexID"] != null && Request["AnnexID"] != "")
            {
                WX.Down.Model.Annex.MODEL annexmodel = WX.Down.Model.Annex.NewDataModel(Request["AnnexID"]);
                if (this.FileUpload1.HasFile)
                {
                    string annexpath = this.SavaAnnex();
                    if (annexpath == "-1")
                    {
                        ULCode.Debug.Alert(this, "附件格式不正确，请选择excel表格文件！");
                        return;
                    }
                    else
                    {
                        try
                        {
                            File.Delete(Server.MapPath(annexmodel.Annex.ToString()));
                        }
                        catch { }
                        annexmodel.Annex.value = annexpath.Split('|')[0];
                        WX.Main.CurUser.LoadMyDepartment(false);
                        annexmodel.Name.value = WX.Main.CurUser.MyDepartMent.Name.ToString() + "-" + annexpath.Split('|')[1];
                    }
                }
                annexmodel.Demo.value = this.TextBox1.Text;
                annexmodel.Update();
                WX.Main.AddLog(WX.LogType.Default, "修改表格文件！", String.Format("{0}-{1}", annexmodel.Id.ToString(), annexmodel.Name.ToString()));
            }
            else
            {
                WX.Down.Model.Annex.MODEL annexmodel = WX.Down.Model.Annex.NewDataModel();
                WX.Main.CurUser.LoadMyDepartment(false);
                if (this.FileUpload1.HasFile)
                {
                    string annexpath = this.SavaAnnex();
                    if (annexpath == "-1")
                    {
                        ULCode.Debug.Alert(this, "附件格式不正确，请选择excel表格文件！");
                        return;
                    }
                    else
                    {
                        annexmodel.Annex.value = annexpath.Split('|')[0];
                        annexmodel.Name.value =WX.Main.CurUser.MyDepartMent.Name.ToString()+"-"+ annexpath.Split('|')[1];
                    }
                }
                else
                {
                    ULCode.Debug.Alert(this, "请选择您要上传的excel文件！");
                    return;
                }
                annexmodel.UserID.value = WX.Main.CurUser.UserID;
                annexmodel.DeptID.value = WX.Main.CurUser.UserModel.DepartmentID.ToString();
                annexmodel.Demo.value = TextBox1.Text;
                annexmodel.Count.value = 0;
                annexmodel.Addtime.value = DateTime.Now;
                int id = annexmodel.Insert(true);
                WX.Main.AddLog(WX.LogType.Default, "上传表格文件！", String.Format("{0}-{1}", id, annexmodel.Name.ToString()));
            }
            Response.Redirect("Index.aspx");
        }
        public string SavaAnnex()
        {
            bool fileOK = false;
            string filepath = "/UploadFiles/Excel/";
            string path = Server.MapPath("~" + filepath);
            if (this.FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".xls", ".xlsx" };
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