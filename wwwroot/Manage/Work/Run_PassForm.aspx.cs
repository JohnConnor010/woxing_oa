using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using System.Text;

namespace wwwroot.Manage.Work
{
    public partial class Run_PassForm : System.Web.UI.Page
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
        public int rStepId
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["Step_Id"]);
            }
        }
        public int SelNextStep = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //0.获取模型
                Run.MODEL run = Run.NewDataModel(rRunId);
                //1.头部流程与工作信息
                WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(rFlowId);//WX.Flow.Model.Flow.NewDataModel(rFlowId);
                liFlowName.Text = liFlowName1.Text = flow.Name.ToString();
                liStepId.Text = rStepId.ToString();
                liRunId.Text = rRunId.ToString();
                liRunName.Text = run.Name.ToString();
                //2.获取下一节点列表，并生成CheckList,并获取默认下步号selOne
                string nextStepsStr = run.GetNextPots(rStepId);
                //Response.Write(nextStepsStr);
                //Response.End(); return;
                string[] nextSteps = nextStepsStr.Split('|');
                var items = nextSteps.Select(step => new ListItem()
                    {
                        Text = String.Format("<img src=\"../images/arrow_down.gif\" /><a href='#' title='序号：{1}'>{0}</a>", step.Split(',')[1].ToString(),step.Split(',')[0].ToString()),
                        Value = step.Split(',')[0].ToString(),
                        Enabled = Convert.ToBoolean(step.Split(',')[2]),
                    });
                foreach (var item in items)
                {
                    item.Attributes.Add("onclick", "CheckSelect(this);");
                    if (this.SelNextStep == 0 && item.Enabled)
                    {
                        this.SelNextStep = Convert.ToInt32(item.Value);
                        item.Selected = true;
                    }
                    this.RadioButtonList1.Items.Add(item);
                }
                if (this.SelNextStep > 0)
                {
                    //根据选择下步 3.获取主办人，并写到页面
                    //根据选择下步 4.获取经办人，并写到页面 
                    this.FillNextStepInfo(run, flow, this.SelNextStep);
                }
                else
                {
                    ULCode.Debug.Alert(this, "没有默认选择,请认真制定要步骤权限与规则");
                }
                //5.信息提示
                this.txtTipContent.Text = String.Format("工作流转交提醒：{0}", run.Name);
            }
        }
        private void FillNextStepInfo(Run.MODEL run,WX.Flow.Model.Flow.MODEL flow,int nextStep)
        { 
                //3.获取主办人，并写到页面
                string auto_op_host=run.GetAutoOpHost(nextStep);
                if (!String.IsNullOrEmpty(auto_op_host))
                {
                    string strValue = auto_op_host.Split(',')[0];
                    string strText = auto_op_host.Split(',')[1];
                    this.hidden_organizer.Value = strValue;
                    string innerHtml1 = "<span id='a" + strValue + "'>" + strText + "<a href=\"javascript:RemoveItem1('" + strValue + "');\"><img src='../images/remove.png'/></a></span>";
                    this.organizer.InnerHtml = innerHtml1;
                }
                else
                {
                    this.hidden_organizer.Value = String.Empty;
                    this.organizer.InnerHtml = String.Empty;
                }
                //4.获取经办人，并写到页面
                string auto_op_list=run.GetAutoOpList(nextStep);
                if (!String.IsNullOrEmpty(auto_op_list))
                {
                    string[] tempTransactor = auto_op_list.Split(new String[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder builder = new StringBuilder();
                    StringBuilder builder1 = new StringBuilder();
                    for (int i = 0; i < tempTransactor.Length; i++)
                    {
                        if (i > 0)
                        {
                            builder1.Append(",");
                        }
                        string selectedText = tempTransactor[i].Split(',')[1];
                        string selectedValue = tempTransactor[i].Split(',')[0];
                        builder1.Append(selectedValue);
                        string innerHtml2 = "<span id='b" + selectedValue + "'>" + selectedText + "<a href=\"javascript:RemoveItem2('" + selectedValue + "');\"><img src='../images/remove.png'/></a></span>";
                        builder.Append(innerHtml2);
                    }
                    this.hidden_transactor.Value = builder1.ToString();
                    this.transactor.InnerHtml = builder.ToString();
                }
                else
                {
                    this.hidden_transactor.Value = String.Empty;
                    this.transactor.InnerHtml = String.Empty;
                }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Run.MODEL run = Run.NewDataModel(rRunId);
            int iR=0;
            try
            {
                iR = run.Pass(rStepId, Convert.ToInt32(this.RadioButtonList1.SelectedItem.Value), this.hidden_organizer.Value, this.hidden_transactor.Value, this.chkNextTip.Checked, this.chkSponsor.Checked, this.chkAllTransactor.Checked, this.txtTipContent.Text.Trim());
            }
            catch { iR = -1; }
            if (iR > 0)
            {
                ULCode.Debug.Alert(this, "工作转交成功！");
                Response.Redirect(String.Format("Run_NewForm.aspx?Flow_Id={0}", rFlowId));
            }
            else if(iR==-1)
            {
                ULCode.Debug.Alert(this, "资料不全提交失败！");
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取三个参数
            Run.MODEL run = Run.NewDataModel(rRunId);
            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(rFlowId);
            this.SelNextStep = Convert.ToInt32(this.RadioButtonList1.SelectedValue); 
            //更新经办人
            this.FillNextStepInfo(run, flow, this.SelNextStep);
        }
    }
}