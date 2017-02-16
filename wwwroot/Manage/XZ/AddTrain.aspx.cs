using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class AddTrain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                pageinit();
        }
        private void pageinit()
        {
            drop_flow.DataSource = WX.Flow.Model.Flow.GetDataList("Type=1");
            drop_flow.DataTextField = "Name";
            drop_flow.DataValueField = "ID";
            drop_flow.DataBind();
            drop_flow.Items.Add(new ListItem("无", ""));
            drop_flow.SelectedValue = "";
            if (Request["TrainID"] != null && Request["TrainID"] != "")
            {
                WX.XZ.Train.MODEL trainmodel = WX.XZ.Train.NewDataModel(Request["trainID"]);
                ui_Title.Text = trainmodel.Title.ToString();
                drop_type.SelectedValue = trainmodel.Type.ToString();
                drop_flow.SelectedValue = trainmodel.FlowID.ToString();
                ui_RunTime.Text = trainmodel.RunTime.ToString();
                ui_Addr.Text = trainmodel.Addr.ToString();
                ui_Persons.Value = trainmodel.UsersID.ToString();
                li_Persons.Text = trainmodel.UsersName.ToString();
                ui_content.Value = trainmodel.Content.ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.XZ.Train.MODEL trainmodel;
            bool isinsert = true;
            if (Request["TrainID"] != null && Request["TrainID"] != "")
            {
                trainmodel = WX.XZ.Train.NewDataModel(Request["trainID"]);
                if (trainmodel.UsersID.ToString() != ui_Persons.Value)
                    WX.XZ.TrainUsers.DeleteToTrainID(trainmodel.ID.ToInt32());
                else
                    isinsert = false;
            }
            else
            {
                trainmodel = WX.XZ.Train.NewDataModel();
                trainmodel.UserID.value = WX.Main.CurUser.UserID;
            }
            trainmodel.Title.value = ui_Title.Text;
            trainmodel.Type.value = drop_type.SelectedValue;
            if (drop_flow.SelectedValue != "")
                trainmodel.FlowID.value = drop_flow.SelectedValue;
            trainmodel.RunTime.value = ui_RunTime.Text;
            trainmodel.Addr.value = ui_Addr.Text;
            trainmodel.UsersID.value = ui_Persons.Value;
            trainmodel.UsersName.value = li_Persons.Text;
            trainmodel.Content.value = ui_content.Value;
            int trainid;
            if (Request["TrainID"] != null && Request["TrainID"] != "")
            {
                trainmodel.Update();
                trainid = trainmodel.ID.ToInt32();
            }
            else
                trainid = trainmodel.Insert(true);
            if (trainmodel.UsersID.ToString() != "" && isinsert)
            {
                string[] users = trainmodel.UsersID.ToString().Split(',');
                for (int i = 0; i < users.Length; i++)
                {
                    WX.XZ.TrainUsers.MODEL trainuser = WX.XZ.TrainUsers.NewDataModel();
                    trainuser.TrainID.value = trainid;
                    trainuser.UserID.value = users[i];
                    trainuser.State.value = 0;
                    trainuser.Addtime.value = DateTime.Now;
                    trainuser.Insert();
                }
            }
            Response.Redirect("TrainList.aspx");

        }
    }
}