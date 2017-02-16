using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using System.Text;
using WX.Flow;
namespace wwwroot.Manage.Work
{
    public partial class Work_FillIn : System.Web.UI.Page
    {
        public int rFlowId
        {
            get
            {
                //return 1;
                return Convert.ToInt32(Request.QueryString["Flow_Id"]);
            }
        }
        public int FormId = 0; WX.Flow.Model.Flow.MODEL flow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //进入条件
                if (Request["Flow_Id"] != null && Request["Flow_Id"] != "")
                {
                    flow = WX.Flow.Model.Flow.GetCache(rFlowId); //WX.Flow.Model.Flow.NewDataModel(rFlowId);
                    this.FormId = flow.FormId.ToInt32();
                    MenuBar1.Param1 = this.rFlowId.ToString();
                    bool b = flow.GetProcessByStep(1).GetInAccess(null);
                    if (b == false||flow.IsVisible.ToInt32()==1)
                    {
                        this.btnSubmit.Enabled = false;
                        this.tooltip.InnerText = flow.GetProcessByStep(1).GetInMsg(null);
                    }
                    //填充流程信息及新工作流水号
                    flow.LoadNumberRule(false);
                    string name = flow.Name.value.ToString();

                    //表单
                    WX.Flow.Model.Form.MODEL formmodel = WX.Flow.Model.Form.NewDataModel(flow.FormId);
                    //WX.Flow.Model.Run.MODEL runmodel;
                    //runmodel = WX.Flow.Model.Run.NewDataModel();
                    //runmodel.FlowId.value = flow.Id.value;
                    ////2.装载Form表单
                    //runmodel.LoadMyFlow(false);
                    //runmodel.LoadMyForm(false);

                    WX.Flow.FormFieldCollection ffedit = new WX.Flow.FormFieldCollection();
                    WX.Flow.FormFieldCollection ffhidden = new WX.Flow.FormFieldCollection();
                    Literal1.Text = formmodel.GenerateHtmls(formmodel.Items_FormFieldCollection, ffedit, ffhidden, WX.Main.CurUser.UserID).Replace("-SYS_IP-", getIp());
                    

                }
                if (Request["RunID"] != null && Request["RunID"] != "")
                {
                    WX.Flow.Model.Run.MODEL runmodel;
                    runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + Request["RunID"]);
                    MenuBar1.Param1 = runmodel.FlowId.ToString();
                    flow = WX.Flow.Model.Flow.NewDataModel(runmodel.FlowId.ToInt32());
                    //2.装载Form表单
                    runmodel.LoadMyForm(false);
                    Literal1.Text = runmodel.GenerateHtmls(runmodel.Id.ToInt32());
                    this.txtSerialNumber.Text = runmodel.Name.ToString(); //String.Format("{0}({1})", name, number);
                    btnSubmit.Visible = false;
                }else
                {
                    this.txtSerialNumber.Text = String.Format("{0}({1})", flow.Name.ToString(), WX.Flow.Model.Run.GetFileCode(rFlowId));
                }
                this.labDescription.Text = flow.Description.value.ToString();
                //填充流程步骤列表
                System.Data.DataTable query;
                if (Request["RunID"] != null)
                    query = ULCode.QDA.XSql.GetDataTable("select A.StepNo,A.Name,A.Next_Nodes,emp.RealName+'：'+C.Content username from FL_Process A Left join FL_Run B on A.FlowID=B.FlowID left join FL_RunFeedBack C on B.ID=C.RunID and A.StepNo=C.StepNo left join TU_Users emp on C.CheckUserID=emp.UserId where A.FlowId=" + flow.Id.ToString() + " and B.ID=" + Request["RunID"] + " order by A.StepNo asc");
                else
                    query = ULCode.QDA.XSql.GetDataTable("select StepNo,Name,Next_Nodes,'' username from FL_Process where FlowId=" + flow.Id.ToString() + " order by StepNo asc");
                this.ProcessRepeater.DataSource = query;
                this.ProcessRepeater.DataBind();
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
        private string ShowNextNode(string nextNode)
        {
            StringBuilder builder = new StringBuilder();
            string[] nodes = nextNode.Split(',');
            if (string.IsNullOrEmpty(nextNode))
            {
                builder.Append("→结束");
            }
            else
            {
                for (int i = 0; i < nodes.Count(); i++)
                {
                    if (i > 0)
                    {
                        builder.Append(',');
                    }
                    builder.Append("→" + nodes[i]);
                }
            }

            return builder.ToString();
        }/// <summary>
        /// 保存表单及其它功能按钮
        /// </summary>
        private int Save(int rRunId, int rStepId)
        {
            //1.获取模型
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.NewDataModel(rRunId);
            WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + (runmodel.StepNo.ToInt32() + 1));
            if (process == null)
            {
                runmodel.Deal_Flag.value = DealFlag.HasOperated;
            }
            else
            {
                runmodel.Deal_Flag.value = DealFlag.NotReceived;
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
            string uploadUserId = WX.Main.CurUser.UserID;
            string uploadIp = WX.Main.getIp(this);
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                // 取文件后缀名
                string oldFileName = System.IO.Path.GetFileName(hpf.FileName);
                string ext = System.IO.Path.GetExtension(hpf.FileName);
                string newFileName = DateTime.Now.ToString("yyyyMMddhhmmss fff") + ext;
                string newPath = String.Format("/UploadFiles/Run/{0}", newFileName);
                if (hpf.ContentLength > 0)
                {
                    try
                    {
                        hpf.SaveAs(Server.MapPath(newPath));
                        //上传成功了
                        DateTime uploadTime = DateTime.Now;
                        string cmdText = String.Format("INSERT INTO FL_RunAttachs (RunId,StepNo,NewFileName,OldFileName,UploadUserID,UploadTime,UploadIP)"
                               + " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');SELECT @@IDENTITY as  IdentityID;"
                               , rRunId, rStepId, newPath, oldFileName, uploadUserId, DateTime.Now, uploadIp);
                        int id = ULCode.QDA.XSql.GetData(cmdText).ToInt32();
                        if (attach_nameList.Length > 0) attach_nameList = attach_nameList + ",";
                        attach_nameList = attach_nameList + oldFileName;
                        if (attache_idlist.Length > 0) attache_idlist = attache_idlist + ",";
                        attache_idlist = attache_idlist + id;
                    }
                    catch
                    {
                        ;
                    }
                }
            }
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
            return 0;// iR;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Process.MODEL process = new Process.MODEL();
            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(rFlowId); //WX.Flow.Model.Flow.NewDataModel(rFlowId);
            flow.LoadProcessList(false);
            if (flow.GetProcessByStep(1).ExecIn(null) == 0)
            {
                ULCode.Debug.Alert(this, "程序出错，请联系管理员！");
                return;
            }
            int newRunId = flow.NewWork(this.txtSerialNumber.Text);

            this.Save(newRunId, 1);
            if (newRunId > 0)
            {
                WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + newRunId);
                WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetCache(runmodel.FlowId.ToInt32(), runmodel.StepNo.ToInt32());
                WX.Model.User.MODEL squser = WX.Model.User.NewDataModel(runmodel.BeginUser.ToString());
                if (process.Auto_Type.ToString() == "1")//经办人为流程发起人的
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + flow.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", runmodel.BeginUser.ToString(), WX.Main.CurUser.UserID, 12, 0);
                else if (process.Auto_Type.ToString() == "2")//经办人为部门主管的
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + flow.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", squser.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 12, 0);
                else if (process.Auto_Type.ToString() == "4")
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + flow.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "Host"), WX.Main.CurUser.UserID, 12, 0);
                else if (process.Auto_Type.ToString() == "5")
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + flow.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "SubHosts"), WX.Main.CurUser.UserID, 12, 0);
                else
                {
                    System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select UserID from Tu_Users where 1=1" + (process.Priv_UserList.ToString() != "" ? " and UserID in(" + process.Priv_UserList.ToString() + ")" : "") + (process.Priv_DutyList.ToString() != "" ? " and DutyId in(select ID from TE_DutyDetail where DutyID in(" + process.Priv_DutyList.ToString() + "))" : "") + (process.Priv_DeptList.ToString() != "" ? " and Priv_DeptList in(" + process.Priv_DeptList.ToString() + ")" : ""));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + flow.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", dt.Rows[i][0].ToString(), WX.Main.CurUser.UserID, 12, 0);
                    }
                }

                //转到下一页
                Response.Redirect("/Manage/Work/Work_MyWork.aspx?flag=0 ");
            }
        }
    }
}