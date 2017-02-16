using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
namespace wwwroot.Manage.Sys
{
    public partial class Set_Cache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Update(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string cmd = btn.CommandName;
            int iEffect = 0;
            if (cmd == "All" || cmd == "Company")
            {
                Company.Caches.Clear(); Company.Caches = null; iEffect++;
            }
            if (cmd == "All" || cmd == "Department")
            {
                Department.Caches.Clear(); Department.Caches = null; iEffect++;
            }
            if (cmd == "All" || cmd == "Duty")
            {
                Duty.Caches.Clear(); Duty.Caches = null;
                DutyDetail.Caches.Clear(); DutyDetail.Caches = null; iEffect++;
            }
            if (cmd == "All" || cmd == "Function")
            {
                Function.Caches.Clear(); Function.Caches = null; iEffect++;
            }
            if (cmd == "All" || cmd == "Menu")
            {
                WX.Model.Menu.Caches.Clear(); WX.Model.Menu.Caches = null; iEffect++;
            }
            if (cmd == "All" || cmd == "Grade")
            {
                WX.Model.Grade.Caches.Clear(); WX.Model.Grade.Caches = null; iEffect++;
            } 
            if (cmd == "All" || cmd == "User")
            {
                WX.Model.User.Caches.Clear(); WX.Model.User.Caches = null; iEffect++;
            }
            ULCode.Debug.Alert(this, String.Format("更新完毕，共更新{0}个信息库", iEffect));
        }
    }
}