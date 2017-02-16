using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.Manage.CRM
{
    public partial class CRM_SingleM_EditAgreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
        }
        public void PageInit()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] buff = wc.DownloadData(Server.MapPath("AgreementTemp/Default.htm"));
            FORM_CONTENT.Value = System.Text.Encoding.GetEncoding("utf-8").GetString(buff);
            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.GetModel("select * from CRM_Customers where ID=(select CustomerID from CRM_Track where ID=" + Request["TrackID"] + ")");
            if (customer != null)
                FORM_CONTENT.Value = FORM_CONTENT.Value.Replace("$CustomerName", customer.CustomerName.ToString()).Replace("$jiafang", customer.CustomerName.ToString());
            WX.Main.CurUser.LoadMyCompany();
            FORM_CONTENT.Value = FORM_CONTENT.Value.Replace("$yifang", WX.Main.CurUser.MyCompany.Name.ToString());
            string product = "";
            string allfee = "";
            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                WX.CRM.CustomerAgreement.MODEL agreement = WX.CRM.CustomerAgreement.GetModel("SELECT * FROM CRM_CustomerAgreement where TrackID=" + Request["TrackID"]);
                if (agreement != null)
                {
                    allfee = agreement.AllFee.ToString();
                    string sql = "SELECT * FROM CRM_CustomerProducts where PID=" + agreement.id.ToString() + " and Type=2";
                    System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                        product = "<table border='1' cellpadding=\"0\" cellspacing=\"0\" style='width:100%;'>\n <tr style=\"text-align: center;height:30px; font-weight: bold;\">\n<td>\n 合作形式\n</td>\n<td>\n服务内容\n</td>\n<td>\n报价\n</td>\n<td>\n 其它补充\n</td>\n </tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        product += "<tr>\n<td height='28' align=\"left\">\n" + dt.Rows[i]["ProductName"] + "</td>\n<td width=\"200\">\n" + dt.Rows[i]["Services"] + "</td>\n<td>\n" + dt.Rows[i]["ZDFee"] + "</td>\n<td>\n" + dt.Rows[i]["Remarks"] + "</td>\n</tr>";
                    }
                    product = product == "" ? "" : product + "</table>";
                }
            }
            FORM_CONTENT.Value = FORM_CONTENT.Value.Replace("$Products", product).Replace("$AllFee", allfee.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.CRM.CustomerAgreement.MODEL agreement = WX.CRM.CustomerAgreement.GetModel("SELECT * FROM CRM_CustomerAgreement where TrackID=" + Request["TrackID"]);
            agreement.Content.value = FORM_CONTENT.Value;
            agreement.Update();
            Response.Redirect("CRM_SingleM_ShowAgreement.aspx?AgreementID="+agreement.id.ToString());
        }
    }
}