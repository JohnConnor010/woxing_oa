using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace wwwroot.App_Demo
{
    public partial class FlowGraphic : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        protected string vmlString = string.Empty;
        protected string lineString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder vmlBuilder = new StringBuilder();
            StringBuilder lineBuilder = new StringBuilder();
            if (!IsPostBack)
            {
                DataTable dataTable = GetDataTable();
                int rowCount = dataTable.Rows.Count;
                if (rowCount == 1)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string[] steps = row["NextStep"].ToString().Split(',');
                        foreach (string step in steps)
                        {
                            lineBuilder.Append("<v:line mfrID='" + row["NodeId"] + "' title='' source='" + row["NodeId"] + "' object='" + step + "' from='0,0' to='0,0' style='position:absolute;display:none;z-index:2' arcsize='4321f' coordsize='21600,21600'><v:stroke endarrow='classic'></v:stroke><v:shadow on='T' type='single' color='#b3b3b3' offset='1px,1px'/></v:line>\r\n");
                        }
                        vmlBuilder.Append("<v:oval id='" + row["NodeId"] + "' table_id='" + row["ID"] + "' flowId='" + row["FlowID"] + "' flowType='start'  passCount='0'  flowTitle='<b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "' flowFlag='0'  readOnly='0'  receiverID=''  receiverName=''  fillcolor='#50A625' onDblClick='Edit_Process(104);' style='LEFT: " + row["Left"] + "; TOP:" + row["Top"] + "; WIDTH: 120; POSITION: absolute; HEIGHT: 60;vertical-align:middle;CURSOR:hand;TEXT-ALIGN:center;z-index:1' arcsize='4321f' coordsize='21600,21600' title='" + row["NodeDescription"].ToString() + "'><v:shadow on='T' type='single' color='#b3b3b3' offset='3px,3px'/><v:textbox inset='1pt,2pt,1pt,1pt' onselectstart='return false;'><b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "</v:textbox></v:oval>\r\n");

                    }
                }
                else
                {
                    foreach (DataRow row in dataTable.Rows)
                    {

                        string[] steps = row["NextStep"].ToString().Split(',');
                        foreach (string step in steps)
                        {
                            lineBuilder.Append("<v:line mfrID='" + row["NodeId"] + "' title='' source='" + row["NodeId"] + "' object='" + step + "' from='0,0' to='0,0' style='position:absolute;display:none;z-index:2' arcsize='4321f' coordsize='21600,21600'><v:stroke endarrow='classic'></v:stroke><v:shadow on='T' type='single' color='#b3b3b3' offset='1px,1px'/></v:line>\r\n");
                        }
                        if (Convert.ToInt32(row["NodeId"]) == 1)
                        {
                            vmlBuilder.Append("<v:oval id='" + row["NodeId"] + "' table_id='" + row["ID"] + "' flowId='" + row["FlowID"] + "' flowType='start'  passCount='0'  flowTitle='<b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "' flowFlag='0'  readOnly='0'  receiverID=''  receiverName=''  fillcolor='#50A625' onDblClick='Edit_Process(" + row["FlowID"].ToString() + "," + row["NodeId"].ToString() + ");' style='LEFT: " + row["Left"] + "; TOP:" + row["Top"] + "; WIDTH: 120; POSITION: absolute; HEIGHT: 60;vertical-align:middle;CURSOR:hand;TEXT-ALIGN:center;z-index:1' arcsize='4321f' coordsize='21600,21600' title='" + row["NodeDescription"].ToString() + "'><v:shadow on='T' type='single' color='#b3b3b3' offset='3px,3px'/><v:textbox inset='1pt,2pt,1pt,1pt' onselectstart='return false;'><b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "</v:textbox></v:oval>\r\n");
                        }
                        else if (Convert.ToInt32(row["NodeId"]) == rowCount)
                        {
                            vmlBuilder.Append("<v:oval id='" + row["NodeId"] + "' table_id='" + row["ID"] + "' flowId='" + row["FlowID"] + "' flowType='end'  passCount='0'  flowTitle='<b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "' flowFlag='0'  readOnly='0'  receiverID=''  receiverName=''  fillcolor='#F4A8BD' onDblClick='Edit_Process(" + row["FlowID"].ToString() + "," + row["NodeId"].ToString() + ");' style='LEFT: " + row["Left"] + "; TOP:" + row["Top"] + "; WIDTH: 120; POSITION: absolute; HEIGHT: 60;vertical-align:middle;CURSOR:hand;TEXT-ALIGN:center;z-index:1' arcsize='4321f' coordsize='21600,21600' title='" + row["NodeDescription"].ToString() + "'><v:shadow on='T' type='single' color='#b3b3b3' offset='3px,3px'/><v:textbox inset='1pt,2pt,1pt,1pt' onselectstart='return false;'><b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "</v:textbox></v:oval>\r\n");
                        }
                        else
                        {
                            vmlBuilder.Append("<v:roundrect inset='2pt,2pt,2pt,2pt' id='" + row["NodeId"] + "' table_id='" + row["ID"] + "' flowId='" + row["FlowID"] + "' flowType=''  passCount='0'  flowTitle='<b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "' flowFlag='0'  readOnly='0'  receiverID=''  receiverName=''  fillcolor='#EEEEEE' onDblClick='Edit_Process(" + row["FlowID"].ToString() + "," + row["NodeId"].ToString() + ");' style='LEFT: " + row["Left"] + "; TOP:" + row["Top"] + "; WIDTH: 100; POSITION: absolute; HEIGHT: 50;vertical-align:middle;CURSOR:hand;TEXT-ALIGN:center;z-index:1' arcsize='4321f' coordsize='21600,21600' title='" + row["NodeDescription"].ToString() + "'> <v:shadow on='T' type='single' color='#b3b3b3' offset='3px,3px'/><v:textbox inset='1pt,2pt,1pt,1pt' onselectstart='return false;'><b>" + row["NodeId"] + "</b><br>" + row["NodeName"] + "</v:textbox></v:roundrect>\r\n");
                        }

                    }
                }
                vmlString = vmlBuilder.ToString();
                lineString = lineBuilder.ToString();
            }
        }
        private DataTable GetDataTable()
        {
            string flowId = Request.QueryString["FlowID"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectCommandText = "SELECT * FROM FlowView WHERE FlowID=" + flowId;
                SqlDataAdapter sda = new SqlDataAdapter(selectCommandText, connection);
                DataTable table = new DataTable();
                sda.Fill(table);
                return table;
            }
        }
    }
}