using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class NotifyFileDetail : System.Web.UI.Page
    {
        WX.XZ.NotifyFiles.MODEL model;
        public int stepno = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                model = WX.Request.rNotifyFile;
                ui_title.Text = model.Title.ToString();
                ui_Code.Text = model.Code.ToString();
                ui_Area.Text = WX.XZ.NotifyFiles.Areaarry[model.Area.ToInt32()];
                ui_username.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.UserID.ToString());
                if (model.Depms.ToString() != "")
                    ui_Area.Text += "(" + WX.CommonUtils.GetDeptNameListByDeptIdList(model.Depms.ToString()) + ")";
                else if (model.Depms.ToString() != "")
                    ui_Area.Text += "(" + WX.CommonUtils.GetRealNameListByUserIdList(model.Users.ToString()) + ")";
                li_Addtime.Text = model.Addtime.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                li_content.Text = model.Content.ToString();
                try
                {
                    string[] annexs = model.Annex.ToString().Split('|');
                    li_content.Text += annexs.Length == 2 && annexs[0] != "" ? "<br/>查看附件：<a href='" + annexs[0] + "'>" + annexs[1] + "</a><br/><br/>" : "";
                }
                catch { }
                pageinit();
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%NotifyFileDetail.aspx?NotifyFileId={1}%'", WX.Main.CurUser.UserID, WX.Request.rNotifyFileId));
       
            }
        }
        private void pageinit()
        {
            model = WX.Request.rNotifyFile;
            stepno = model.StepNo.ToInt32();
            string[] signarry = WX.Flow.Model.Run.GetSignList(model.RunID.ToInt32(), WX.Main.CurUser.UserID);
            this.txt_sign.Text = signarry[0];
            this.txtSealData.Value = signarry[1];
            this.liCurUserSealButton.Text = signarry[2];
            this.liSignList.Text = signarry[3];
            bool flag = true;
            if (model.state.ToInt32() >= 4)
            {
                flag = false;
                Button2.Visible = model.state.ToInt32() == 4;
            }
            else
            {
                WX.Main.CurUser.LoadDutyUser(); WX.Main.CurUser.LoadMyDepartment(flag);
                WX.Flow.Model.Process.MODEL proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + model.FlowID.ToString() + " and StepNo=" + model.StepNo.ToString());
                int next_n = model.StepNo.ToInt32();
                if (proc != null && proc.Next_Nodes.ToString() != "")
                    next_n = proc.StepNo.ToInt32();
                WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + model.FlowID.ToString() + " and StepNo=" + next_n + " and(Priv_UserList like '%" + WX.Main.CurUser.UserID + "%'	or Priv_DutyList like'%" + WX.Main.CurUser.DutyUser.ID.ToString() + "%' or Priv_DeptList like'%" + WX.Main.CurUser.MyDepartMent.ID.ToString() + "%')");
                WX.Flow.Model.Process.MODEL proc2 = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + model.FlowID.ToString() + " and StepNo=" + next_n);


                WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + model.RunID.ToString());
                WX.Model.User.MODEL squser = WX.Model.User.GetCache(model.UserID.ToString());
                if (runmodel.Deal_Flag.ToInt32() < 3)
                {
                    if (process != null)//经办人设置为我的
                        flag = true;
                    else if (proc2 != null)
                    {
                        if (proc2.Auto_Type.ToString() == "1" && runmodel.BeginUser.ToString() == WX.Main.CurUser.UserID)//经办人为流程发起人的
                            flag = true;
                        else if (proc2.Auto_Type.ToString() == "2" && WX.WXUser.GetDeptIDByUserID(runmodel.BeginUser.ToString()) == WX.Main.CurUser.MyDepartMent.ID.ToInt32() && (WX.Main.CurUser.MyDepartMent.Host.ToString() == WX.Main.CurUser.UserID || WX.Main.IsBestDuty(WX.Main.CurUser.MyDepartMent.ID.ToInt32(), WX.Main.CurUser.UserID)))//经办人为部门主管的
                            flag = true;
                        else if (proc2.Auto_Type.ToString() == "4" && WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "Host") == WX.Main.CurUser.UserID)
                            flag = true;
                        else if (proc2.Auto_Type.ToString() == "5" && WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "SubHosts") == WX.Main.CurUser.UserID)
                            flag = true;
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                        flag = false;
                }
            }
            qz.Visible = flag;
            System.Data.DataTable query = ULCode.QDA.XSql.GetDataTable("select A.StepNo,A.Name,A.Next_Nodes from FL_Process A Left join FL_Run B on A.FlowID=B.FlowID where A.FlowId=" + model.FlowID.ToString() + " and B.ID=" + model.RunID.ToString() + " order by A.StepNo asc");
            this.ProcessRepeater.DataSource = query;
            this.ProcessRepeater.DataBind();
        }
        /// <summary>
        /// 保存表单及其它功能按钮
        /// </summary>
        private int Save(int state)
        {
            WX.XZ.NotifyFiles.MODEL model = WX.Request.rNotifyFile;
            //1.获取模型
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + model.RunID.ToString());
            int stepno = model.StepNo.ToInt32();
            //2.取表单值
            runmodel.LoadMyForm(false);
            WX.Flow.FormFieldCollection ffc = runmodel.MyForm.GetPostedDatas();
            //3.上传附件并取得附件列表
            string attach_nameList = String.Empty;
            string attache_idlist = String.Empty;
            string uploadUserId = WX.Main.CurUser.UserID;
            string uploadIp = WX.Main.getIp(this);
            HttpFileCollection hfc = Request.Files;
            //WX.Flow.FormFieldCollection ffc = new WX.Flow.FormFieldCollection();
            //foreach (WX.Flow.FormField ff in runmodel.MyForm.Items_FormFieldCollection)
            //{
            //    ff.Value = this.Request.Form[ff.Id] == null ? "" : this.Request.Form[ff.Id];
            //    ffc.Add(ff);
            //}
            //            
            //4.取得手写与签章信息
            string sealData = this.txtSealData.Value;
            WX.Flow.Model.Process.MODEL proc;

            WX.Flow.Model.Process.MODEL process = null;
            int iR = 0;
            if (state == 2)
            {
                model.state.value = 2;
                model.StepNo.value = 1;
                model.StepName.value = "文件拟写";
                proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + model.StepNo.ToInt32());
                process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + proc.Next_Nodes.ToInt32());
                runmodel.StepNo.value = model.StepNo.value;
                runmodel.Next_Nodes.value = proc.Next_Nodes.value;
                WX.Main.AddLog(WX.LogType.Default, "文件通知审批未通过！", String.Format("{0}-{1}", model.ID.ToString(), model.Title.ToString()));
                //向拟写人发消息
                WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFiles.aspx?mes=1&NotifyFileId=" + model.ID.ToString() + ">你似写的文件《" + model.Title.ToString() + "》——审批未通过被退回</a>", "/Manage/Main/messagelist.aspx", model.UserID.ToString(), WX.Main.CurUser.UserID, 5, 0);

            }
            else
            {
                proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + model.StepNo.ToInt32());
                if (proc != null && proc.Next_Nodes.ToString() != "" && proc.Next_Nodes.ToInt32() > 0)
                    process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + proc.Next_Nodes.ToInt32());

                if (process == null)
                {
                    runmodel.Deal_Flag.value = WX.Flow.DealFlag.HasOperated;
                    model.state.value = 4;
                    model.StepNo.value = 0;
                    model.StepName.value = "行政发布";
                    //审批完成，向行政发消息发布文件

                    WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">文件《" + model.Title.ToString() + "》通过审批！请行政尽快发布</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 5, 0);
                    WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">文件《" + model.Title.ToString() + "》通过审批！请行政尽快发布</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetAdminUserID, WX.Main.CurUser.UserID, 5, 0);

                }
                else
                {
                    runmodel.Deal_Flag.value = 1;
                    runmodel.StepNo.value = model.StepNo.value = process.StepNo.value;
                    if (process.Next_Nodes.ToInt32() > 0)
                        runmodel.Next_Nodes.value = process.Next_Nodes.value;
                    else
                        runmodel.Next_Nodes.value = 0;
                    iR = 1;
                    model.state.value = 3;
                    model.StepName.value = process.Name.value;
                    //向下一个人发消息，提醒审批
                    // WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">文件《" + model.Title.ToString() + "》通过审批！请行政尽快发布</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 5, 0);
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
                        System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select UserID from Tu_Users where 1=1" + (process.Priv_UserList.ToString() != "" ? " and UserID in('" + process.Priv_UserList.ToString().Replace(",","','") + "')" : "") + (process.Priv_DutyList.ToString() != "" ? " and DutyId in(select ID from TE_DutyDetail where DutyID in(" + process.Priv_DutyList.ToString() + "))" : "") + (process.Priv_DeptList.ToString() != "" ? " and Priv_DeptList in(" + process.Priv_DeptList.ToString() + ")" : ""));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            WX.Main.MessageSend("<a href=/Manage/XZ/NotifyFileDetail.aspx?NotifyFileId=" + model.ID.ToString() + ">" + squser.RealName.ToString() + "拟写的文件《" + model.Title.ToString() + "》！申请审批</a>", "/Manage/Main/messagelist.aspx", dt.Rows[i][0].ToString(), WX.Main.CurUser.UserID, 5, 0);
                        }
                    }
                }
                WX.Main.AddLog(WX.LogType.Default, "文件通知审批通过！", String.Format("{0}-{1}", model.ID.ToString(), model.Title.ToString()));
            }
            model.PublishTime.value = DateTime.Now;
            model.Update();
            runmodel.Save(stepno, ffc, attache_idlist, attach_nameList, this.txt_sign.Text, sealData, 1);

            pageinit();
            return iR;
        }
        protected void btn_EnterPass(object sender, EventArgs e)
        {
            int iR = this.Save(1);

        }
        protected void btn_Cancel(object sender, EventArgs e)
        {
            int iR = this.Save(2);
        }
        protected void btn_Send(object sender, EventArgs e)
        {
            WX.Public.CurUser.LoadMyDepartment();
            model = WX.Request.rNotifyFile;
            if (model.state.ToInt32() == 4)
            {
                model.state.value = 5;
                model.PublishTime.value = DateTime.Now;
                model.CheckMess();
                model.Update();
                WX.Main.AddLog(WX.LogType.Default, "文件发布！", String.Format("{0}-{1}", model.ID.ToString(), model.Title.ToString()));
            }
            Button2.Visible = false;
        }
    }
}