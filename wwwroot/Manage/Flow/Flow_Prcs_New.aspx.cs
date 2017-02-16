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
	public partial class Flow_Prcs_New : System.Web.UI.Page
	{
        //private string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ULCode.Validation.IsNumber(Convert.ToString(Request.QueryString["id"])))
                {
                    ULCode.Debug.we("你没有权限访问此功能！");
                    return;
                }
                //this.MenuBar1.CurIndex = Convert.ToInt32(Request.QueryString["Index"]);
                WX.Data.Dict.BindListCtrl_enum_NodeType(this.ddlNodeType, null, "#--请选择节点类型--", null);
                this.LoadData();
            }
        }
        private void LoadData()
        {
            int flowId = Convert.ToInt32(Request.QueryString["id"]);
            WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(flowId); //WX.Flow.Model.Flow.NewDataModel(flowId);
            flow.LoadProcessList(true);
            //初始化txtNodeId
            if (flow.ProcessList == null)
            {
                this.txtNodeId.Text = "1";
            }
            else
            {
                this.txtNodeId.Text = Convert.ToString(flow.ProcessList.Count + 1);
            }
            //填充
            if (flow.ProcessList != null)
                foreach (Process.MODEL prcs in flow.ProcessList)
                {
                    this.ListBox2.Items.Add(new ListItem(String.Format("{0},{1}", prcs.StepNo, prcs.Name), prcs.StepNo.ToString()));
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
            int y = (stepNo-1) % 2 == 0 ? 50 : 230;
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
            int stepNo = Convert.ToInt32(this.txtNodeId.Text); 
            string stepName=this.txtNodeName.Text;
            string nodeType=this.ddlNodeType.SelectedValue;
            int flowId = Convert.ToInt32(Request.QueryString["id"]);
            //验证
            //处理
            Process.MODEL prcsNew = Process.NewDataModel();
            prcsNew.StepNo.set(stepNo);
            prcsNew.Name.set(stepName);
            prcsNew.NodeType.set(nodeType);
            prcsNew.FlowId.set(flowId);
            Point point = this.GetDefault_VMLPoint(stepNo);
            prcsNew.VML_Left.set(point.X);
            prcsNew.VML_Top.set(point.Y);
            prcsNew.Next_Nodes.set(this.GetNextNodeIdList());
            prcsNew.Notby.set(this.GetNextNodeIdList2());
            int iR = prcsNew.Insert(true);
            if (stepNo > 1)
            {
                string sSql = String.Format("Update FL_Process set Next_Nodes=(case when Next_Nodes is null then '{2}' when Next_Nodes='' then '{2}' else Next_Nodes+','+'{2}' end) where FlowId={0} and StepNo={1}", flowId, stepNo - 1, stepNo);
                ULCode.QDA.XSql.Execute(sSql);
            }
            //错误
            if (iR > 0)
            {
                prcsNew.SaveIntoCaches();
                string listUrl = this.MenuBar1.CurIndex == 4 ? String.Format("Flow_Prcs_VmlList.aspx?Id={0}", flowId) :String.Format("Flow_Prcs_List.aspx?Id={0}", flowId);
                ULCode.Debug.Confirm(this, "添加步骤成功，是否继续添加？", this.Request.Url.ToString(), listUrl);
            }
            else
            {
                ULCode.Debug.Alert(this, "添加步骤失败！");
            }
        }
	}
}