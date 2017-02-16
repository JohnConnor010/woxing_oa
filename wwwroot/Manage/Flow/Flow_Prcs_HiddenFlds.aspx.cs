using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_HiddenFlds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                //WX.Flow.Model.Flow.MODEL flowmodel = WX.Flow.Model.Flow.GetModel("select * from FL_Flows where ID=" + Request["flowid"]);
                //WX.Flow.Model.Form.MODEL formmodel = WX.Flow.Model.Form.GetModel("select * from FL_Forms where ID=" + flowmodel.FormId.value.ToString());
                WX.Flow.Model.Flow.MODEL flowmodel = WX.Flow.Model.Flow.GetCache(Convert.ToInt32(Request["flowid"]));
                flowmodel.LoadForm(false);
                WX.Flow.Model.Form.MODEL formmodel = flowmodel.Form;
                
                WX.Flow.FormFieldCollection ffc = formmodel.Items_FormFieldCollection;
                if (ffc != null)
                {
                    foreach (WX.Flow.FormField ff in ffc)
                    {
                        select1.Items.Add(new ListItem(ff.Text, ff.Id));
                    }
                }
                WX.Flow.Model.Process.MODEL model = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"])); //WX.Flow.Model.Process.MODEL model = WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
                Literal1.Text = model.StepNo.value + "：" + model.Name.value;
                WX.Flow.FormFieldCollection edit = model.Hidden_FormFieldCollection;
                if (edit != null)
                {
                    foreach (WX.Flow.FormField ff in edit)
                    {
                        select2.Items.Add(new ListItem(ff.Text, ff.Id));
                        select1.Items.Remove(select1.Items.FindByValue(ff.Id));
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Flow.Model.Process.MODEL model = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"])) ;//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
            WX.Flow.FormFieldCollection ffc = new WX.Flow.FormFieldCollection();
            WX.Flow.FormField ff = null;
            string[] ffstr = FLD_STR.Value.Split(',');
            for (int i = 0; i < ffstr.Length; i++)
            {
                if (ffstr[i] != "")
                {
                    ff = new WX.Flow.FormField();
                    ff.Id = ffstr[i].Split('|')[0];
                    ff.Text = ffstr[i].Split('|')[1];
                    ffc.Add(ff);
                }
            }
            model.Hidden_FormFieldCollection = ffc;
            model.Update();
            Response.Redirect("Flow_Prcs_List.aspx?id=" + Request["flowid"]);
        }
    }
}