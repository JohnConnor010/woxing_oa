using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace wwwroot.Manage.CRM
{
    public partial class Crm_SingleM_ShowTrack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            WX.CRM.Track.MODEL track = WX.CRM.Track.NewDataModel(Request["TrackID"]);
            LiProcessState.Text = WX.CRM.Track.ProcessState[track.ProcessState.ToInt32()];
            LiTrackTime.Text = track.TrackTime.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            LiTrackNo.Text = track.TrackNo.ToString();
            LiFee.Text = track.Fee.ToString();
            if (!track.Remarks.isEmpty)
            {
                StringBuilder sbRemarks = new StringBuilder();
                string[] arr_remark=track.Remarks.ToString().Split('|');

                sbRemarks.Append("目标预测：" + arr_remark[0] + "<br/>" +
                    "难&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;点：" + arr_remark[1] + "<br/>" +
                    "解决方法：" + arr_remark[2]);
                if(arr_remark.Length>3)
                    sbRemarks.Append("<br/>目标达成："+arr_remark[3]);
                Liremark.Text = sbRemarks.ToString();
            }
            LiLogParaments.Text = track.LogParaments.ToString();
            if (track.ProcessState.ToInt32() == 4)
            {
                ccp.Visible = true;
                TablerowContent();
            }
            else
            {
                ccp.Visible = false;
            }

        }
        private void TablerowContent()
        {
            WX.CRM.CustomerProgram.MODEL program = WX.CRM.CustomerProgram.GetModel("SELECT * FROM CRM_CustomerProgram where TrackID=" + Request["TrackID"]);
            if (program != null)
            {
                liProgramContent.Text = program.Content.ToString();
                liProgramTitle.Text = program.Title.ToString();
                liProgramLK.Text = program.Remarks.ToString();
                liProgramTime.Text = program.ProgramTime.ToDateTime().ToString("yyyy-MM-dd");
                liProgramPath.Text = "<a href='" + program.Program.ToString() + "'>" + program.Program.ToString() + "</a>";
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("SELECT pp.ProductName,pp.Remark PRemark,pp.SalesPrice,ccp.ZDFee,ccp.Remarks FROM CRM_CustomerProducts ccp left join PDT_Products pp on ccp.ProductID=pp.ID where Type=1 and PID=" + program.id.ToString());
                CustomerRepeater.DataSource = dt;
                CustomerRepeater.DataBind();
            }
        }
    }
}