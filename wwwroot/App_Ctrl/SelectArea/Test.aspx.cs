using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Ctrl.SelectArea
{
    public partial class Test : System.Web.UI.Page
    {
        protected void TestR(object sender, EventArgs e)
        {
            string s = String.Format("ProvCode:{0}<br/>CityCode:{1}<br/>AreaCode:{2}",this.SelectAreaCtrl1.ProvCode,this.SelectAreaCtrl1.CityCode,this.SelectAreaCtrl1.AreaCode);
            Response.Write(s);
        }
    }
}