using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Configuration;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace wwwroot.App_Demo
{
    public partial class AddNewGraphic : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["FlowID"]))
                {
                    int flowId = Convert.ToInt32(Request.QueryString["FlowID"]);
                    using (WorkFlowContext context = new WorkFlowContext(connectionString))
                    {
                        var flowViews = context.FlowViews.Where(f => f.FlowID == flowId).Select(f => f);
                        if (flowViews.Count() == 0)
                        {
                            this.txtNodeId.Text = "1";
                        }
                        else
                        {
                            int nId = context.FlowViews.Where(f => f.FlowID == flowId).OrderByDescending(f => f.ID).FirstOrDefault().NodeId;
                            this.txtNodeId.Text = Convert.ToString(nId + 1);
                        }
                        var flowList = context.FlowViews.Where(f => f.FlowID == flowId).Select(f => new
                        {
                            FlowValue = f.NodeId,
                            FlowText = f.NodeId + "," + f.NodeName
                        });
                        this.ListBox1.DataSource = flowList;
                        this.ListBox1.DataTextField = "FlowText";
                        this.ListBox1.DataValueField = "FlowValue";
                        this.ListBox1.DataBind();
                    }
                }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["FlowID"]))
            {
                int left = 11;
                int top = 50;
                int flowId = Convert.ToInt32(Request.QueryString["FlowID"]);
                int lastLeft = 0;
                int lastTop = 0;
                using (WorkFlowContext context = new WorkFlowContext(connectionString))
                {
                    var flowViews = context.FlowViews.Where(f => f.FlowID == flowId).Select(f => f);
                    if (flowViews.Count() == 0)
                    {
                        FlowView flowView = new FlowView()
                        {
                            FlowID = flowId,
                            NodeId = Convert.ToInt32(this.txtNodeId.Text.Trim()),
                            NodeType = Convert.ToInt32(this.ddlNodeType.SelectedItem.Value),
                            NodeName = this.txtNodeName.Text.Trim(),
                            Left = left,
                            Top = top,
                            NextStep = Convert.ToString(Convert.ToInt32(this.txtNodeId.Text.Trim()) + 1),
                            NodeDescription = ""
                        };
                        context.FlowViews.InsertOnSubmit(flowView);
                        context.SubmitChanges();
                    }
                    else if (flowViews.Count() == 1)
                    {
                        StringBuilder builder = new StringBuilder();
                        if (ListBox2.Items.Count == 0)
                        {
                            builder.Append(Convert.ToInt32(this.txtNodeId.Text) + 1);
                        }
                        else
                        {
                            builder.Append((Convert.ToInt32(this.txtNodeId.Text) + 1) + ",");
                            foreach (ListItem item in ListBox2.Items)
                            {
                                builder.Append(item.Value + ",");
                            }
                            builder.ToString().TrimEnd(',');
                        }
                        FlowView flowView = new FlowView()
                        {
                            FlowID = flowId,
                            NodeId = Convert.ToInt32(this.txtNodeId.Text.Trim()),
                            NodeType = Convert.ToInt32(this.ddlNodeType.SelectedItem.Value),
                            NodeName = this.txtNodeName.Text.Trim(),
                            Left = 20,
                            Top = 230,
                            NextStep = builder.ToString().TrimEnd(','),
                            NodeDescription = ""
                        };
                        context.FlowViews.InsertOnSubmit(flowView);
                        context.SubmitChanges();
                    }
                    else if (flowViews.Count() + 1 >= 3)
                    {
                        var lastRow = context.FlowViews.Where(f => f.FlowID == flowId).OrderByDescending(f => f.ID).FirstOrDefault();
                        lastLeft = lastRow.Left;
                        lastTop = lastRow.Top;
                        if ((flowViews.Count() + 1) % 2 == 0)
                        {
                            StringBuilder builder = new StringBuilder();
                            if (ListBox2.Items.Count == 0)
                            {
                                builder.Append(Convert.ToInt32(this.txtNodeId.Text) + 1);
                            }
                            else
                            {
                                builder.Append((Convert.ToInt32(this.txtNodeId.Text) + 1) + ",");
                                foreach (ListItem item in ListBox2.Items)
                                {
                                    builder.Append(item.Value + ",");
                                }
                                builder.ToString().TrimEnd(',');
                            }
                            FlowView flowView = new FlowView()
                            {
                                FlowID = flowId,
                                NodeId = Convert.ToInt32(this.txtNodeId.Text.Trim()),
                                NodeType = Convert.ToInt32(this.ddlNodeType.SelectedItem.Value),
                                NodeName = this.txtNodeName.Text.Trim(),
                                Left = lastLeft,
                                Top = lastTop + 180,
                                NextStep = builder.ToString().TrimEnd(','),
                                NodeDescription = ""
                            };
                            context.FlowViews.InsertOnSubmit(flowView);
                            context.SubmitChanges();
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            if (ListBox2.Items.Count == 0)
                            {
                                builder.Append(Convert.ToInt32(this.txtNodeId.Text) + 1);
                            }
                            else
                            {
                                builder.Append((Convert.ToInt32(this.txtNodeId.Text) + 1) + ",");
                                foreach (ListItem item in ListBox2.Items)
                                {
                                    builder.Append(item.Value + ",");
                                }
                            }
                            FlowView flowView = new FlowView()
                            {
                                FlowID = flowId,
                                NodeId = Convert.ToInt32(this.txtNodeId.Text.Trim()),
                                NodeType = Convert.ToInt32(this.ddlNodeType.SelectedItem.Value),
                                NodeName = this.txtNodeName.Text.Trim(),
                                Left = lastLeft + 180,
                                Top = 50,
                                NextStep = builder.ToString().TrimEnd(','),
                                NodeDescription = ""
                            };
                            context.FlowViews.InsertOnSubmit(flowView);
                            context.SubmitChanges();
                        }
                    }
                }
                Response.Write("<script language='javascript'>alert('添加成功');window.opener.location.replace(opener.location);window.close();</script>");
            }
        }
    }
    [Table(Name = "FlowView")]
    public class FlowView
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public int FlowID { get; set; }

        [Column]
        public int NodeId { get; set; }

        [Column]
        public int NodeType { get; set; }

        [Column]
        public string NodeName { get; set; }

        [Column]
        public int Left { get; set; }

        [Column]
        public int Top { get; set; }

        [Column]
        public string NextStep { get; set; }

        [Column]
        public string NodeDescription { get; set; }     
    }
    public class WorkFlowContext : DataContext
    {
        public Table<FlowView> FlowViews;
        public WorkFlowContext(string connectionString) : base(connectionString) { }
    }
}