using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.Manage.XZ
{
    public partial class NotifyFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ui_Code.Text = "济行政字[" + DateTime.Now.Year + "]XX号";
                if (WX.Request.rNotifyFileId > 0)
                {
                    WX.XZ.NotifyFiles.MODEL model = WX.Request.rNotifyFile;
                    ui_title.Text = model.Title.ToString();
                    hidden_UserList.Value = model.Users.ToString();
                    txtUserList.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Users.ToString());
                    hidden_DepartmentList.Value = model.Depms.ToString();
                    txtDepartmentList.Text = WX.CommonUtils.GetDeptNameListByDeptIdList(model.Depms.ToString());
                    ui_content1.Value = model.Content.ToString();
                    ui_Area.SelectedValue = model.Area.ToString();
                    try
                    {
                        string[] annexs = model.Annex.ToString().Split('|');
                        Annex_li.Text = annexs.Length == 2 ? "<a target='_blank' href='" + annexs[0] + "'>" + annexs[1] + "</a><br/>" : "";
                    }
                    catch { }
                    if (model.state.ToInt32() > 2)
                        Button1.Visible = Button2.Visible = Button3.Visible = true;
                    if (Request["mes"] != null)
                        WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%NotifyFiles.aspx?mes=1&NotifyFileId={1}%'", WX.Main.CurUser.UserID, WX.Request.rNotifyFileId));
                }
            }
        }
        private void SaveData(int state, int runid)
        {
            WX.XZ.NotifyFiles.MODEL model;

            //业务处理过程
            if (WX.Request.rNotifyFileId > 0)
                model = WX.Request.rNotifyFile;
            else
            {
                model = WX.XZ.NotifyFiles.NewDataModel();

                model.UserID.value = WX.Main.CurUser.UserID;
            }

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
            model.Code.value = ui_Code.Text;
            model.Title.value = ui_title.Text;

            model.Users.value = hidden_UserList.Value;
            model.Depms.value = hidden_DepartmentList.Value;
            model.Content.value = ui_content1.Value;
            model.Area.value = ui_Area.SelectedValue;
            model.PublishTime.value = DateTime.Now;
            if (runid > 0)
                model.RunID.value = runid;
            ////填写主要业务逻辑代码、登记日志

            model.state.value = state;
            if (state == 4)
            {
                model.StepNo.value = 0;
                model.StepName.value = "行政发布";
                WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">文件《" + model.Title.ToString() + "》通过审批！请行政尽快发布</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 5, 0);
                WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">文件《" + model.Title.ToString() + "》通过审批！请行政尽快发布</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetAdminUserID, WX.Main.CurUser.UserID, 5, 0);

            }
            else
            {
                model.FlowID.value = 14;
                model.StepNo.value = state == 1 ? 1 : 2;
                model.StepName.value = WX.Flow.Model.Process.GetCache(model.FlowID.ToInt32(), model.StepNo.ToInt32()).Name.value;
            }
            if (WX.Request.rNotifyFileId > 0)
            {
                model.Update();
                WX.Main.AddLog(WX.LogType.Default, "修改文件通知！", String.Format("{0}", model.Title.ToString()));
            }
            else
            {
                int id = model.Insert(true);
                model.ID.value = id;
                WX.Main.AddLog(WX.LogType.Default, "拟写文件通知！", String.Format("{0}-{1}", id, model.Title.ToString()));
            }
            if (state == 3)
            {
                WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetCache(model.FlowID.ToInt32(), model.StepNo.ToInt32());
                WX.Model.User.MODEL squser = WX.Model.User.NewDataModel(model.UserID.ToString());
                if (process.Auto_Type.ToString() == "1")//经办人为流程发起人的
                    WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">" + squser.RealName.ToString() + "拟写的文件《" + model.Title.ToString() + "》！申请审批</a>", "/Manage/Main/messagelist.aspx", model.UserID.ToString(), WX.Main.CurUser.UserID, 5, 0);
                else if (process.Auto_Type.ToString() == "2")//经办人为部门主管的
                    WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">" + squser.RealName.ToString() + "拟写的文件《" + model.Title.ToString() + "》申请主管审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", squser.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 5, 0);
                else if (process.Auto_Type.ToString() == "4")
                    WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">" + squser.RealName.ToString() + "拟写的文件《" + model.Title.ToString() + "》申请上级审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "Host"), WX.Main.CurUser.UserID, 5, 0);
                else if (process.Auto_Type.ToString() == "5")
                    WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">" + squser.RealName.ToString() + "拟写的文件《" + model.Title.ToString() + "》申请分管领导审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "SubHosts"), WX.Main.CurUser.UserID, 5, 0);
                else
                {
                    System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select UserID from Tu_Users where 1=1" + (process.Priv_UserList.ToString() != "" ? " and UserID in(" + process.Priv_UserList.ToString() + ")" : "") + (process.Priv_DutyList.ToString() != "" ? " and DutyId in(select ID from TE_DutyDetail where DutyID in(" + process.Priv_DutyList.ToString() + "))" : "") + (process.Priv_DeptList.ToString() != "" ? " and Priv_DeptList in(" + process.Priv_DeptList.ToString() + ")" : ""));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">" + squser.RealName.ToString() + "拟写的文件《" + model.Title.ToString() + "》！申请审批</a>", "/Manage/Main/messagelist.aspx", dt.Rows[i][0].ToString(), WX.Main.CurUser.UserID, 5, 0);
                    }
                }
            }
        }
        protected void SubmitData1(object sender, EventArgs e)
        {
            SaveData(1, 0);
            //返回处理结果或返回其它页面。
            Response.Redirect("MyFiles.aspx");
        }
        protected void SubmitData3(object sender, EventArgs e)
        {
            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(14); //WX.Flow.Model.Flow.NewDataModel(rFlowId);
            flow.LoadProcessList(false);
            if (flow.GetProcessByStep(1).ExecIn(null) == 0)
            {
                ULCode.Debug.Alert(this, "程序出错，请联系管理员！");
                return;
            }
            int newRunId;
            if (WX.Request.rNotifyFileId > 0)
                newRunId = WX.Request.rNotifyFile.RunID.ToInt32();
            else
            {
                newRunId = flow.NewWork(flow.Name.ToString() + "——" + ui_title.Text);

            }
            this.Save(newRunId, 2);
            SaveData(3, newRunId);
            //返回处理结果或返回其它页面。
            Response.Redirect("MyFiles.aspx");
        }
        /// <summary>
        /// 保存表单及其它功能按钮
        /// </summary>
        private int Save(int rRunId, int rStepId)
        {
            //1.获取模型
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.NewDataModel(rRunId);
            WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + rStepId);
            if (process == null)
            {
                runmodel.Deal_Flag.value = WX.Flow.DealFlag.HasOperated;
            }
            else
            {
                runmodel.Deal_Flag.value = WX.Flow.DealFlag.NotReceived;
                if (process.Next_Nodes.ToString() != "")
                    runmodel.Next_Nodes.value = process.Next_Nodes.value;
                else
                {
                    WX.Flow.Model.Process.MODEL proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + runmodel.StepNo.ToInt32());
                    runmodel.Next_Nodes.value = proc.Next_Nodes.value;
                }
                runmodel.StepNo.value = process.StepNo.value;
            }
            //2.取表单值

            WX.Flow.FormFieldCollection ffc = new WX.Flow.FormFieldCollection();
            //3.上传附件并取得附件列表
            string attach_nameList = String.Empty;
            string attache_idlist = String.Empty;

            //WX.Flow.FormFieldCollection ffc = new WX.Flow.FormFieldCollection();
            //foreach (WX.Flow.FormField ff in runmodel.MyForm.Items_FormFieldCollection)
            //{
            //    ff.Value = this.Request.Form[ff.Id] == null ? "" : this.Request.Form[ff.Id];
            //    ffc.Add(ff);
            //}
            //            
            //4.取得手写与签章信息
            int iR = runmodel.Save(rStepId, ffc, attache_idlist, attach_nameList, "", "", 0);//最后两个参数为会签意见和手写签章信息
            runmodel.Update();
            return iR;
        }
        protected void SubmitData4(object sender, EventArgs e)
        {
            SaveData(4, 0);
            //返回处理结果或返回其它页面。
            Response.Redirect("MyFiles.aspx");
        }
        public string SavaAnnex()
        {
            bool fileOK = false;
            string filepath = "/UploadFiles/Notify/";
            string path = Server.MapPath("~" + filepath);
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