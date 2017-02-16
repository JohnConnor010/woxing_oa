using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Work
{
    public partial class Work_MyDuty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.Main.CurUser.LoadUserModel(true);
            WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(WX.Main.CurUser.UserModel.DutyId.ToInt32());
            WX.Model.Duty.MODEL dutymodel = WX.Model.Duty.GetModelToID(dutydetail.DutyID.ToInt32());
            Literal1.Text = dutymodel.Name.ToString();
            Literal2.Text = dutymodel.Description.ToString();
        }
    }
}