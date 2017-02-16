using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
namespace wwwroot.Manage.Work
{
    public partial class Run_SignForm : System.Web.UI.Page
    {
        public int rFlowId
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["Flow_Id"]);
            }
        }
        public int rRunId
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["Run_Id"]);
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //1.获取当前工作对象
                WX.Flow.Model.Run.MODEL runmodel;
                runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + this.rRunId);
                //2.装载Form表单
                runmodel.LoadMyForm(false);
                Literal1.Text = runmodel.GenerateHtmls(runmodel.Id.ToInt32());

                #region //3.装载附件
                string[] attid = runmodel.Attach_IdList.ToString().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] attname = runmodel.Attach_NameList.ToString().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sbAttachList = new StringBuilder();
                for (int i = 0; i < attid.Length; i++)
                {
                    sbAttachList.AppendFormat("<div id='attach_{2}'>{0}&nbsp;&nbsp;&nbsp;&nbsp;{1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:deleteAttach({2},{3})'>删除</a></div>", i + 1, attname[i], attid[i], rRunId);
                }
                liAttachList.Text = sbAttachList.ToString();
                #endregion

                #region //4.装载签办文本信息
                hiddrunid.Value = this.rRunId.ToString();
                hiddstepno.Value = runmodel.StepNo.ToString();
                DataTable dt = ULCode.QDA.XSql.GetDataTable(String.Format("select rfb.*,p.Name runname,emp.RealName username from FL_RunFeedBack rfb "
                    + " left join FL_Run run on rfb.RunId=run.Id"
                    + " left join FL_Process p on run.FlowId=p.FlowId and rfb.StepNo=p.StepNo "

                    + " left join TU_Users emp on rfb.UserId=emp.UserId "
                    + " where RunId ={0}", rRunId));
                bool flag = true;
                WX.Main.CurUser.LoadDutyUser();
                WX.Main.CurUser.LoadMyDepartment();
                WX.Flow.Model.Process.MODEL proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + runmodel.StepNo.ToString());
                int next_n=runmodel.Next_Nodes.ToInt32();
                if (proc.Next_Nodes.ToString() != "")
                    next_n = proc.StepNo.ToInt32();
                WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + next_n + " and(Priv_UserList like '%" + WX.Main.CurUser.UserID + "%'	or Priv_DutyList like'%" + WX.Main.CurUser.DutyUser.ID.ToString() + "%' or Priv_DeptList like'%" + WX.Main.CurUser.MyDepartMent.ID.ToString() + "%')");
                WX.Flow.Model.Process.MODEL proc2 = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + next_n);
                WX.Main.CurUser.LoadMyDepartment(flag);
                
                if (runmodel.Deal_Flag.ToInt32() < 3)
                {
                    if (process != null)//经办人设置为我的
                        flag = true;
                    else if (proc2!=null &&proc2.Auto_Type.ToString() == "1" && runmodel.BeginUser.ToString() == WX.Main.CurUser.UserID)//经办人为流程发起人的
                        flag = true;
                    else if (proc2 != null && proc2.Auto_Type.ToString() == "2" && WX.WXUser.GetDeptIDByUserID(runmodel.BeginUser.ToString()) == WX.Main.CurUser.MyDepartMent.ID.ToInt32()&&(WX.Main.CurUser.DutyUser.ID.ToInt32() == 500||WX.Main.IsBestDuty(WX.Main.CurUser.MyDepartMent.ID.ToInt32(),WX.Main.CurUser.UserID)))//经办人为部门主管的
                        flag = true;
                    else
                        flag = false;
                }
                else
                    flag = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["FeedFlag"]) == 0 && Convert.ToString(dt.Rows[i]["UserId"]) == WX.Main.CurUser.UserID)
                    {
                        this.FORM_CONTENT.Text = dt.Rows[i]["Content"].ToString();
                        if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["SignData"])))
                        {
                            this.txtSealData.Value = dt.Rows[i]["SignData"].ToString();
                            this.liCurUserSealButton.Text += "<input type=\"button\" onclick=\"PopupIFrame('/App_Ctrl/ShowSignData.aspx?Id=" + Convert.ToString(dt.Rows[i]["Id"]) + "','查看手写签章','','',250,250);\" value=\"查看手写签章\" />";
                        }
                    }
                    else
                    {
                        this.liSignList.Text += " <tr>\n";
                        this.liSignList.Text += " <td>\n";
                        this.liSignList.Text += "     <fieldset>\n";
                        this.liSignList.Text += "    <!--<b> -->\n";
                        this.liSignList.Text += "      <legend>\n";
                        this.liSignList.Text += "      	第" + dt.Rows[i]["StepNo"] + "步 " + dt.Rows[i]["runname"] + "      <u style='cursor:hand'>" + dt.Rows[i]["username"] + "</u>\n";
                        this.liSignList.Text += "      <!--</b> -->\n";
                        this.liSignList.Text += "      </legend>\n";
                        /*
                        if (Convert.ToString(dt.Rows[i]["UserId"]) == WX.Main.CurUser.UserID)
                        {
                            sign.Text += "                      	<img src='/images/edit.gif' style='cursor:pointer' align='absmiddle' alt='编辑意见' onClick='edit_sign('6');'>&nbsp;\n";
                            sign.Text += "      	<img src='/images/delete.gif' style='cursor:pointer' align='absmiddle' alt='删除意见' onClick='delete_sign('6');'>&nbsp;\n";
                            sign.Text += "      	      <img src='/images/edit.gif' style='cursor:pointer' align='absmiddle' alt='回复意见' onClick='reply_sign('6');'>\n";
                        }*/
                        this.liSignList.Text += "      <div class='content'>\n";
                        this.liSignList.Text += "         <div style='width:85%;float:left;'>";
                        this.liSignList.Text += "         <i>" + dt.Rows[i]["EditTime"] + "</i><br/>";
                        this.liSignList.Text += "         " + dt.Rows[i]["Content"];
                        this.liSignList.Text += "         </div>\n";
                        this.liSignList.Text += "         <div style='width:14%;float:left; text-align:left;'>";
                        if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["SignData"])))
                        {
                            this.liSignList.Text += "           <input type=\"button\" onclick=\"PopupIFrame('/App_Ctrl/ShowSignData.aspx?Id=" + Convert.ToString(dt.Rows[i]["Id"]) + "','查看手写签章','','',250,250);\" value=\"查看手写签章\" />";
                        }
                        this.liSignList.Text += "         </div>\n";
                        this.liSignList.Text += "      </div>\n";
                        this.liSignList.Text += "      </fieldset>\n";
                        this.liSignList.Text += "       </td>\n";
                        this.liSignList.Text += "     </tr>";
                    }
                }
                #endregion
                qz.Visible = flag;
                form_control.Visible = flag;
                FileUpload1.Visible = flag;
            }
        }
        /// <summary>
        /// 保存表单及其它功能按钮
        /// </summary>
        private int Save()
        {
            //1.获取模型
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + this.rRunId);
            
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
                               , rRunId, runmodel.StepNo.ToString(), newPath, oldFileName, uploadUserId, DateTime.Now, uploadIp);
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
            string sealData = this.txtSealData.Value;
            WX.Flow.Model.Process.MODEL proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + runmodel.StepNo.ToInt32());

            WX.Flow.Model.Process.MODEL process = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + (runmodel.StepNo.ToInt32() + 1));

            if (process == null)
            {
                runmodel.Deal_Flag.value = WX.Flow.DealFlag.HasOperated;
            }
            else
            {
                runmodel.Deal_Flag.value = 1;
                runmodel.StepNo.value = runmodel.StepNo.ToInt32() + 1;
                if (process.Next_Nodes.ToString() != "")
                    runmodel.Next_Nodes.value = process.Next_Nodes.value;
                if (proc.Next_Nodes.ToString() == "")
                {
                    WX.Flow.Model.Process.MODEL proc2 = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID=" + runmodel.FlowId.ToString() + " and StepNo=" + (runmodel.Next_Nodes.ToInt32() + 1));
                    if (proc2 == null)
                        runmodel.Deal_Flag.value = WX.Flow.DealFlag.HasOperated;
                }
            }
            int iR = runmodel.Save(proc.Next_Nodes.ToString() == "" ? runmodel.Next_Nodes.ToInt32() : proc.StepNo.ToInt32(), ffc, attache_idlist, attach_nameList, this.FORM_CONTENT.Text, sealData, 1);
            if (process != null)
            {
                WX.Model.User.MODEL squser = WX.Model.User.NewDataModel(runmodel.BeginUser.ToString());
                if (process.Auto_Type.ToString() == "1")//经办人为流程发起人的
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + runmodel.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", runmodel.BeginUser.ToString(), WX.Main.CurUser.UserID, 12, 0);
                else if (process.Auto_Type.ToString() == "2")//经办人为部门主管的
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + runmodel.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", squser.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 12, 0);
                else if (process.Auto_Type.ToString() == "4")
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + runmodel.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "Host"), WX.Main.CurUser.UserID, 12, 0);
                else if (process.Auto_Type.ToString() == "5")
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + runmodel.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetParentDeptHost(squser.DepartmentID.ToInt32(), "SubHosts"), WX.Main.CurUser.UserID, 12, 0);
                else
                {
                    System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select UserID from Tu_Users where 1=1" + (process.Priv_UserList.ToString() != "" ? " and UserID in('" + process.Priv_UserList.ToString().Replace(",", "','") + "')" : "") + (process.Priv_DutyList.ToString() != "" ? " and DutyId in(select ID from TE_DutyDetail where DutyID in(" + process.Priv_DutyList.ToString() + "))" : "") + (process.Priv_DeptList.ToString() != "" ? " and Priv_DeptList in(" + process.Priv_DeptList.ToString() + ")" : ""));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        WX.Main.MessageSend("<a href=/Manage/Work/Work_MyCheck.aspx?flag=0&mes=1>" + runmodel.Name.ToString() + "(" + WX.CommonUtils.GetRealNameListByUserIdList(runmodel.BeginUser.ToString()) + ")——请尽快审批！</a>", "/Manage/Main/messagelist.aspx", dt.Rows[i][0].ToString(), WX.Main.CurUser.UserID, 12, 0);
                    }
                }
            }
            return iR;
        }
        protected void btn_EnterPass(object sender, EventArgs e)
        {
            this.Save();
            Response.Redirect("Work_MyCheck.aspx");
        }
        protected void btn_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("Work_MyCheck.aspx");
        }
        /// <summary>
        /// 清空手写签章信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearSignData(object sender, EventArgs e)
        {
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.GetModel("select * from FL_Run where Id=" + this.rRunId);
            
            string sSql = String.Format("update FL_RunFeedBack set SignData=NULL where RunId={0} and StepNo={1} and UserId='{2}'", rRunId, runmodel.StepNo.ToString(), WX.Main.CurUser.UserID);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            if (iR > 0)
            {
                Response.Redirect(this.Request.RawUrl);
            }
        }
    }
}
