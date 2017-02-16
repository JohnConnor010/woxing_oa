using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class TrainDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["TrainID"] != null && Request["TrainID"] != "")
                {
                    WX.XZ.Train.MODEL trainmodel = WX.XZ.Train.NewDataModel(Request["TrainID"]);
                    li_title.Text = trainmodel.Title.ToString();
                    drop_type.SelectedValue = trainmodel.Type.ToString();
                    li_addr.Text = trainmodel.Addr.ToString();
                    li_runtime.Text = trainmodel.RunTime.ToString();
                    li_usersname.Text = trainmodel.UsersName.ToString();
                    li_content.Text = trainmodel.Content.ToString();
                    string userid = WX.Main.CurUser.UserID;
                    if (Request["UserID"] != null && Request["UserID"] != "")
                    {
                        userid = Request["UserID"];
                    }
                    WX.XZ.TrainUsers.MODEL tusermodel = WX.XZ.TrainUsers.GetModelToTrainID(trainmodel.ID.ToInt32(), userid);
                    if (tusermodel != null)
                    {
                        if (tusermodel.RunID.ToString() != "")
                        {
                            WX.Flow.Model.Run.MODEL runmodel= WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + tusermodel.RunID.ToString());
                            //2.装载Form表单
                            runmodel.LoadMyForm(false);
                            li_formcontent.Text = runmodel.GenerateHtmls(runmodel.Id.ToInt32());
                            
                        }
                        else if (trainmodel.FlowID.ToString() != "")
                        {
                            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(trainmodel.FlowID.ToInt32());
                            WX.Flow.Model.Form.MODEL formmodel = WX.Flow.Model.Form.NewDataModel(flow.FormId);
                            WX.Flow.FormFieldCollection ffedit = new WX.Flow.FormFieldCollection();
                            WX.Flow.FormFieldCollection ffhidden = new WX.Flow.FormFieldCollection();
                            li_formcontent.Text = formmodel.GenerateHtmls(formmodel.Items_FormFieldCollection, ffedit, ffhidden, WX.Main.CurUser.UserID).Replace("-SYS_IP-", getIp());

                            if (Request["UserID"] == null || Request["UserID"].ToString() == "")
                            {
                                Button1.Visible = true;
                                if (tusermodel.State.ToInt32() == 0)
                                {
                                    tusermodel.State.value = 1;
                                    tusermodel.Update();
                                }
                            }
                        }
                        if (Request["UserID"] == null || Request["UserID"].ToString() == "")
                        {
                            try
                            {
                                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and RedirectToUrl like'%?TrainID={1}%'",WX.Main.CurUser.UserID, Request["TrainID"]));
                            }
                            catch
                            {
                            }
                        }
                        if (li_formcontent.Text != "")
                            Literal1.Text = "学习心得";
                    }
                }
            }
        }
        private string getIp()
        {
            // 穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (Request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            Ip = "202.96.134.133";
                }
                else
                    Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                Ip = Request.UserHostAddress;
            }
            return Ip;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.XZ.Train.MODEL trainmodel = WX.XZ.Train.NewDataModel(Request["TrainID"]);
            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(trainmodel.FlowID.ToInt32()); //WX.Flow.Model.Flow.NewDataModel(rFlowId);
            flow.LoadProcessList(false);
            if (flow.GetProcessByStep(1).ExecIn(null) == 0)
            {
                ULCode.Debug.Alert(this, "程序出错，请联系管理员！");
                return;
            }
            int newRunId = flow.NewWork("《"+flow.Name.ToString()+"》学习心得");

            this.Save(newRunId, 1);
            WX.XZ.TrainUsers.MODEL tusermodel = WX.XZ.TrainUsers.GetModelToTrainID(trainmodel.ID.ToInt32(), WX.Main.CurUser.UserID);
            tusermodel.RunID.value = newRunId;
            tusermodel.State.value = 2;
            tusermodel.Update();
            ULCode.Debug.Alert(this, "提交成功！");
            Button1.Visible = false;
        }
        private int Save(int rRunId, int rStepId)
        {
            //1.获取模型
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.NewDataModel(rRunId);
            WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + (runmodel.StepNo.ToInt32() + 1));
            if (process == null)
            {
                runmodel.Deal_Flag.value =WX.Flow.DealFlag.HasOperated;
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
            runmodel.LoadMyForm(false);
            WX.Flow.FormFieldCollection ffc = runmodel.MyForm.GetPostedDatas();
            //3.上传附件并取得附件列表
            string attach_nameList = String.Empty;
            string attache_idlist = String.Empty;
            //4.取得手写与签章信息
            int iR = runmodel.Save(rStepId, ffc, attache_idlist, attach_nameList, "", "", 0);//最后两个参数为会签意见和手写签章信息
            runmodel.Update();
            return 0;// iR;
        }
    }
}