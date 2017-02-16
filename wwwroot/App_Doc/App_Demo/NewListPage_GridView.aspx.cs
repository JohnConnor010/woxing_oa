using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot
{
    public partial class NewListPage_GridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] datas = new String[]
                {
                    "id,name,tel",
                    "1,szp,85071163",
                    "2,zdx,12343211233",
                    "3,yjy,838402349",
                    "4,wxd,9234e2329"
                };
                GridView2.DataSource = WX.Main.GetTestDataTable(datas);
                GridView2.DataBind();

                DropDownList1.Items.AddRange(
                    new ListItem[] { 
                       new ListItem("T1","1"),
                       new ListItem("T2","2"),
                       new ListItem("T3","3"),
                       new ListItem("T4","4"),
                       new ListItem("T5","5"),
                    });
            }
        }
    }
}