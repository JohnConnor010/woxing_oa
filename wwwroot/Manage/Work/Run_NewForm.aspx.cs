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
    public partial class Run_NewForm : System.Web.UI.Page
    {
        public int rFlowId
        {
            get
            {
                //return 1;
                return Convert.ToInt32(Request.QueryString["Flow_Id"]);
            }
        }
        public int FormId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //进入条件
                WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(rFlowId); //WX.Flow.Model.Flow.NewDataModel(rFlowId);
                this.FormId = flow.FormId.ToInt32();
                MenuBar1.Param1 = this.rFlowId.ToString();
                bool b = flow.GetProcessByStep(1).GetInAccess(null);             
                if (b == false)
                {
                    this.btnSubmit.Enabled = false;
                    this.tooltip.InnerText = flow.GetProcessByStep(1).GetInMsg(null);
                }
                //填充流程信息及新工作流水号
                flow.LoadNumberRule(false);
                string name = flow.Name.value.ToString();
                string number = flow.NumberRule.GetValue();
                this.txtSerialNumber.Text = number; //String.Format("{0}({1})", name, number);
                this.txtDescription.Text = flow.Description.value.ToString();
                //填充流程步骤列表
                flow.LoadProcessList(false);
                var process = flow.ProcessList;
                //var process = Process.Caches.FindAll(delegate(Process.MODEL dele) { return dele.Id.ToInt32() == rFlowId; });//WX.Flow.Model.Process.GetModels("SELECT * FROM FL_Process WHERE FlowId=" + rFlowId);
                var query = process.Select(p => new
                    {
                        StepNo = p.GetFieldValue("StepNo").ToString(),
                        Name = p.GetFieldValue("Name").ToString(),
                        NextNode = ShowNextNode(p.GetFieldValue("Next_Nodes").ToString())
                    });
                this.ProcessRepeater.DataSource = query;
                this.ProcessRepeater.DataBind();
            }
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
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Process.MODEL process = new Process.MODEL();
            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(rFlowId); //WX.Flow.Model.Flow.NewDataModel(rFlowId);
            flow.LoadProcessList(false);
            if (flow.GetProcessByStep(1).ExecIn(null) == 0)
            {
                ULCode.Debug.Alert(this,"程序出错，请联系管理员！");
                return;
            }
            int newRunId = flow.NewWork(this.txtSerialNumber.Text);
            if (newRunId > 0)
            {
                //转到下一页
                Response.Redirect(String.Format("Run_SignForm.aspx?Run_ID={0}&Flow_Id={1}&Step_Id={2}", newRunId, flow.Id, 1), true);
            }
        }
    }
}