using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace wwwroot.Manage
{
    public partial class add_message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(WX.Authentication.GetUserID());
            string fromUserId = WX.Authentication.GetUserID();
            string toUserId = Convert.ToString(Request.QueryString["UserId"]);
            string fromUser = WX.WXUser.GetRealNameByUserID(fromUserId);
            string toUser = WX.WXUser.GetRealNameByUserID(toUserId);
            this.lbSendTo.Text = toUser;
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            string fromUserId = WX.Authentication.GetUserID();
            string toUserId = Convert.ToString(Request.QueryString["UserId"]);
            string fromUser = WX.WXUser.GetRealNameByUserID(fromUserId);
            string toUser = WX.WXUser.GetRealNameByUserID(toUserId);
            //WX.Authentic
            WX.Model.Message.MODEL model =WX.Model.Message.NewDataModel();
            model.Title.value = txtContent.Text.Trim();
            model.ID.value = Guid.NewGuid();
            model.SendToUserId.value=toUserId;
            model.FromUserId.value = fromUserId;
            model.SendTime.value = DateTime.Now;
            model.RedirectToUrl.value = "/Manage/Main/messagelist.aspx";
            //model.State.value = 0;
            //model.Type.value = DropDownList1.SelectedValue;
            model.Type.value = "1";
            if (FileUpload1.HasFile)
            {
                string filepath = "/UploadFiles/Mess/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
            }        
            model.Insert();
        }
    }
}