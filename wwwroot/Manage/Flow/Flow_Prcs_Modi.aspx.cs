using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using System.Drawing;
using System.Text;
namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_Modi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ULCode.Validation.IsNumber(Convert.ToString(Request.QueryString["flowid"]))
                    || !ULCode.Validation.IsNumber(Convert.ToString(Request.QueryString["id"])))
                {
                    ULCode.Debug.we("你没有权限访问此功能！");
                    return;
                }
                WX.Data.Dict.BindListCtrl_enum_NodeType(this.ddlNodeType, null, "#--请选择节点类型--", null);
                this.LoadData();
            }
        }
        private void LoadData()
        {
            int flowId = Convert.ToInt32(Request.QueryString["flowid"]);
            int Id = Convert.ToInt32(Request.QueryString["id"]);

            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(flowId) ;//WX.Flow.Model.Flow.NewDataModel(flowId);
            flow.LoadProcessList(true);

            Process.MODEL process = WX.Flow.Model.Process.GetCache(Id) ;//Process.NewDataModel(Id);
            process.LoadNextProcessList(true);
            process.LoadNotbyProcessList(true);
            this.txtNodeId.Text = process.StepNo.ToString();
            this.txtNodeName.Text = process.Name.ToString();
            this.ddlNodeType.SelectedValue = process.NodeType.ToString();
            
            if (process.NextProcessList != null)
                foreach (Process.MODEL prcs in process.NextProcessList)
                {
                    this.ListBox1.Items.Add(new ListItem(String.Format("{0},{1}", prcs.StepNo, prcs.Name), prcs.StepNo.ToString()));

                }
                foreach (Process.MODEL prcs in process.NotbyProcessList)
                {
                    this.ListBox3.Items.Add(new ListItem(String.Format("{0},{1}", prcs.StepNo, prcs.Name), prcs.StepNo.ToString()));
                }
            if (flow.ProcessList != null)
                foreach (Process.MODEL prcs in flow.ProcessList)
                {
                    if (process.NextProcessList == null || !process.NextProcessList.Exists(delegate(Process.MODEL prcs_dele) { return prcs.Id.ToInt32() == prcs_dele.Id.ToInt32(); }))
                        this.ListBox2.Items.Add(new ListItem(String.Format("{0},{1}", prcs.StepNo, prcs.Name), prcs.StepNo.ToString()));
                    if (process.NotbyProcessList == null || !process.NotbyProcessList.Exists(delegate(Process.MODEL prcs_dele) { return prcs.Id.ToInt32() == prcs_dele.Id.ToInt32(); }))
                    this.ListBox4.Items.Add(new ListItem(String.Format("{0},{1}", prcs.StepNo, prcs.Name), prcs.StepNo.ToString()));
                }
        }
        protected void btnAddList_Click(object sender, EventArgs e)
        {
            int selectIndex = this.ListBox1.SelectedIndex;
            if (selectIndex != -1)
            {
                this.ListBox2.Items.Add(this.ListBox1.SelectedItem);
                this.ListBox1.Items.RemoveAt(selectIndex);
            }
        }
        protected void btnRemoveList_Click(object sender, EventArgs e)
        {
            int selectIndex = this.ListBox2.SelectedIndex;
            if (selectIndex != -1)
            {
                this.ListBox1.Items.Add(this.ListBox2.SelectedItem);
                this.ListBox2.Items.RemoveAt(selectIndex);
            }
        }

        protected void btnAddList2_Click(object sender, EventArgs e)
        {
            int selectIndex = this.ListBox3.SelectedIndex;
            if (selectIndex != -1)
            {
                this.ListBox4.Items.Add(this.ListBox3.SelectedItem);
                this.ListBox3.Items.RemoveAt(selectIndex);
            }
        }
        protected void btnRemoveList2_Click(object sender, EventArgs e)
        {
            int selectIndex = this.ListBox4.SelectedIndex;
            if (selectIndex != -1)
            {
                this.ListBox3.Items.Add(this.ListBox4.SelectedItem);
                this.ListBox4.Items.RemoveAt(selectIndex);
            }
        }
        private Point GetDefault_VMLPoint(int stepNo)
        {
            int x = (stepNo - 1) / 2 * 180 + (stepNo == 1 ? 11 : 20);
            int y = (stepNo - 1) % 2 == 0 ? 50 : 230;
            return new Point(x, y);
        }
        private string GetNextNodeIdList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in ListBox1.Items)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(item.Value);
            }
            return sb.ToString();
        }

        private string GetNextNodeIdList2()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in ListBox3.Items)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(item.Value);
            }
            return sb.ToString();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //输入用户变量
            int Id = Convert.ToInt32(Request.QueryString["id"]);
            int flowId = Convert.ToInt32(Request.QueryString["flowId"]);
            int stepNo = Convert.ToInt32(this.txtNodeId.Text);
            string stepName = this.txtNodeName.Text;
            string nodeType = this.ddlNodeType.SelectedValue;
            //验证
            //处理
            Process.MODEL prcsModi = Process.GetCache(Id);//Process.NewDataModel(Id);
            prcsModi.StepNo.set(stepNo);
            prcsModi.Name.set(stepName);
            prcsModi.NodeType.set(nodeType);
            prcsModi.Next_Nodes.set(this.GetNextNodeIdList());
            prcsModi.Notby.set(this.GetNextNodeIdList2());
            int iR = prcsModi.Update();
            //if (stepNo > 1)
            //{
            //    string sSql = String.Format("Update FL_Process set Next_Nodes=(case when Next_Nodes is null then '{2}' when Next_Nodes='' then '{2}' else Next_Nodes+','+'{2}' end) where FlowId={0} and StepNo={1}", flowId, stepNo - 1, stepNo);
            //    ULCode.QDA.XSql.Execute(sSql);
            //}
            //错误
            if (iR != 0)
            {
                string listUrl = this.MenuBar1.CurIndex == 4 ? String.Format("Flow_Prcs_VmlList.aspx?Id={0}", flowId) : String.Format("Flow_Prcs_List.aspx?Id={0}", flowId);
                ULCode.Debug.Confirm(this, "修改步骤成功，是否回到流程设计页？", listUrl, this.Request.Url.ToString());
            }
            else
            {
                ULCode.Debug.Alert(this, "修改步骤失败！");
            }
        }
    }
}