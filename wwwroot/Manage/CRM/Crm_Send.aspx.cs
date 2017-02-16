using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Send : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_InnerCategory(this.ddlCustomerCategory, null, "#内部分类", null);
                WX.Data.Dict.BindListCtrl_CompanyNature(this.ddlCompanyNature, null, "#企业性质", null);
                WX.Data.Dict.BindListCtrl_Source(this.ddlSource, null, "#来源", null);
                WX.Data.Dict.BindListCtrl_Industry(this.ddlIndustry, null, "#行业", null);
                WX.Data.Dict.BindListCtrl_BusinessLevel(this.ddlBusinessLevel, null, "#合作分类", null);
                WX.Data.Dict.BindListCtrl_Stage(this.ddlStage, null, "#阶段", null);
                InitCustomerRepeater();
            }
        }
        public DataTable dataTable;
        private void InitCustomerRepeater()
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlCustomerCategory.SelectedValue != "")
                sqlBuilder.Append(" AND C.CategoryID=" + this.ddlCustomerCategory.SelectedValue);
            if (this.ddlCompanyNature.SelectedValue != "")
                sqlBuilder.Append(" AND C.NatureId=" + this.ddlCompanyNature.SelectedValue);
            if (this.ddlSource.SelectedValue != "")
                sqlBuilder.Append(" AND C.SourceID=" + this.ddlSource.SelectedValue);
            if (this.ddlIndustry.SelectedValue != "")
                sqlBuilder.Append(" AND C.IndustryID=" + this.ddlIndustry.SelectedValue);
            if (this.ddlBusinessLevel.SelectedValue != "")
                sqlBuilder.Append(" AND C.BusinessLevel=" + this.ddlBusinessLevel.SelectedValue);
            if (this.ddlStage.SelectedValue != "")
                sqlBuilder.Append(" AND C.StageID" + (this.ddlStage.SelectedValue == "1" ? "<2" : "=" + this.ddlStage.SelectedValue));
            string sql = "SELECT C.ID,C.CustomerName,cct.ContactName,cct.MobilePhone FROM CRM_Contact AS cct "
            + "inner join CRM_Customers C on C.ID=cct.CustomerID"
           + " where C.State>0 and C.EmployeeID='" + WX.Main.CurUser.UserID + "' and MobilePhone!=''"
                       + sqlBuilder.ToString();
            dataTable = ULCode.QDA.XSql.GetDataTable(sql + " ORDER BY C.ID desc");
            
            this.CustomerRepeater.DataSource = dataTable;
            this.CustomerRepeater.DataBind();
        }

        protected void ddlCustomerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCustomerRepeater();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string phones = "";
                string content = this.TextBox1.Text;
            if (CustomerRepeater.Items.Count > 0)
            {
                if (((CheckBox)CustomerRepeater.Items[0].FindControl("CheckBox1")).Checked)
                {
                    phones = ((HiddenField)CustomerRepeater.Items[0].FindControl("HiddenField1")).Value;
                    //        发送短信
                    SendSMS(content, phones);
                }
            }
            for (int i = 1; i < CustomerRepeater.Items.Count; i++)
            {
                if (((CheckBox)CustomerRepeater.Items[i].FindControl("CheckBox1")).Checked)
                {
                    phones += "," + ((HiddenField)CustomerRepeater.Items[i].FindControl("HiddenField1")).Value;
                }
            }
            if (phones != "")
            {
                string mobiles = "";
                StringBuilder mobileBuilder = new StringBuilder();

                var mobilePhone = phones.Trim().Split(',');
                Array.ForEach(mobilePhone,
                    r =>
                    {
                        if (IsCorrentMobile(r) == true)
                        {
                            mobileBuilder.AppendFormat("{0}|", r);
                        }
                    });
                mobiles = mobileBuilder.ToString().TrimEnd('|');
                var result = SendSMS(content, mobiles);
                Response.Write(result);
                //写系统日志
                //WX.CRM.Customer.AddLogSMS(phones, content, phones.Split(',').Length,WX.Main.CurUser.UserID);
            }
        }
        public static bool IsCorrentMobile(string input)
        {
            string pattern = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[1|2|3|5|6|7|8|9])\d{8}$";
            Regex regex = new Regex(pattern);
            return regex.Match(input).Success;
        }
        /// <summary>
        /// 调用WebService发送短信
        /// </summary>
        /// <param name="msg">短信内容70个字符以内</param>
        /// <param name="mobile">手机号码，多个手机号码用“|”隔开</param>
        /// <param name="userName">短信账户</param>
        /// <param name="userPwd">短信密码</param>
        /// <returns>短信返回值,短信内容分割数量</returns>
        /// 短信返回值
        /// 0 = 成功
        /// -1 = 失败
        /// -2 = 缺少目标号码
        /// -3 = 缺少用户名或密码
        /// -4 = 缺少短信内容
        /// -5 = 登陆失败
        /// -6 = 存在非法字符
        /// -7 = 存在错误号码
        /// -8 = 余额不足
        /// -9 = 服务器连接失败
        /// -10 = 用户名或密码格式不正确(只能为数字、字母、汉字)
        /// -11 = 短信内容存在系统保留关键词
        /// -12 = 号码条数超出限制
        /// -13 = 短信内容长度超出
        public static string SendSMS(string msg, string mobile)
        {
            int count = 0;
            Regex reg = new Regex(@".{1,69}");
            MatchCollection mc = reg.Matches(msg);
            string result = "";
            foreach (Match m in mc)
            {
                count++;
                SMSWebServiceReference.ServiceSoapClient smsClient = new SMSWebServiceReference.ServiceSoapClient();
                result = smsClient.SendSMS("gongshanglian", "123456", mobile, m.Value);
                if (result.Split(',')[0] == "0")
                {
                        //短信日志 群发短信成功    FE.Web.Ctrl.SMSSender.SMSUtility.LogSMS(m.Value, phone, CurrentUnitName, CurrentLoginChineseName, DateTime.Now, true);
                }
                else if (result.Split(',')[0] == "-7")
                {
                    //短信日志 群发短信失败
                }
            }
            return result + "," + count;
        }
    }
}