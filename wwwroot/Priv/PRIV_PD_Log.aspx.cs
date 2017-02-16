using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace wwwroot.Priv
{
    public partial class PRIV_PD_Log : System.Web.UI.Page
    {
        //模块接口
        private DateTime ThisDate
        {
            get
            {
                if (Request.QueryString["Date"] != null)
                {
                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(Convert.ToString(Request.QueryString["Date"]), out dt))
                    {
                        return dt;
                    }
                    else
                        return DateTime.Now.AddDays(this.DefaultDay);
                }
                else
                    return DateTime.Now.AddDays(this.DefaultDay);
            }
        }
        private int DefaultDay = -1;
        private String CurUserId
        {
            get
            {
                return WX.Main.CurUser.UserID;
            }
        }
        public String CurUserName
        {
            get
            {
                return WX.Main.CurUser.UserName;
            }
        }
        private bool IsLogined()
        {
            if (!WX.Authentication.IsAuthenticated)
            {
                Response.Redirect(String.Format("{0}?returnUrl={1}", this.LoginUrl, this.Request.Url), true);
                return false;
            }
            else
                return true;
        }
        private string LoginUrl
        {
            get
            {
                return "/Login.aspx";
            }
        }
        //****************************************************
        private System.Data.DataTable ProgramsDataTable = null;
        private System.Data.DataTable DetailsDataTable = null;
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Load_DP_Data();
            this.Load_Details_Data();
        }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.lblDate.Text = String.Format("{0:yyyy年MM月dd日 ddd}", this.ThisDate);
                this.Title = String.Format("{0:yyyy年MM月dd日}&nbsp;被动日志", this.ThisDate);
                if (String.Format("{0:yyyy-MM-dd}", this.ThisDate) == String.Format("{0:yyyy-MM-dd}", DateTime.Now))
                {
                    this.btnToday.ForeColor = System.Drawing.Color.Red;
                    this.btnToday.Font.Bold = true;
                }
                this.Bind_DP_Data();
            }
        }
        private void Load_DP_Data()
        {
            if (this.ProgramsDataTable != null)
            {
                //this.ProgramsDataTable.Clear();
                //this.ProgramsDataTable.Rows.Clear();
                this.ProgramsDataTable.Dispose();
            }
            String sSql = String.Format("select ROW_NUMBER() over (order by sort) as RowNo,* from Priv_pd_programs where state=1 and Userid='{0}' order by Sort", this.CurUserId);
            this.ProgramsDataTable = ULCode.QDA.XSql.GetDataTable(sSql);
        }
        private void Load_Details_Data()
        {
            if (this.DetailsDataTable != null)
            {
                //this.DetailsDataTable.Clear();
                //this.DetailsDataTable.Rows.Clear();
                this.DetailsDataTable.Dispose();
            } 
            String sSql = String.Format("select [pd_id],[desc],[value] from Priv_pd_details where Userid='{0}' and [DATE]='{1:yyyy-MM-dd}'", this.CurUserId, this.ThisDate);
            this.DetailsDataTable = ULCode.QDA.XSql.GetDataTable(sSql);
        }
        private void Bind_DP_Data()
        {
            this.rpt_PD.DataSource = this.ProgramsDataTable;
            this.rpt_PD.DataBind();
        }
        public string GetItemHtml(object eval_id, object eval_type, object eval_items, object eval_descFld)
        {
            int id = Convert.ToInt32(eval_id);
            int type = Convert.ToInt32(eval_type);
            string items = Convert.ToString(eval_items);
            string descFld = Convert.ToString(eval_descFld);
            if (type == 1) //普通文本
                return this.GetItemHtml1(id, items, descFld);
            else if (type == 2)
                return this.GetItemHtml2(id, items, descFld);
            else if (type == 3)
                return this.GetItemHtml3(id, items, descFld);
            else if (type == 4)
                return this.GetItemHtml4(id, items, descFld);
            else if (type == 5)
                return this.GetItemHtml5(id, items, descFld);
            else
                return null;
        }
        public string GetNotice(object eval_id,object eval_script,object eval_crition,object eval_format)
        {
            int id = Convert.ToInt32(eval_id);
            if(eval_script==Convert.DBNull||eval_crition== Convert.DBNull|| eval_format==Convert.DBNull)
                return null;
            string script = Convert.ToString(eval_script);
            string crition = Convert.ToString(eval_crition);
            string format = Convert.ToString(eval_format);
            if (String.IsNullOrEmpty(script) || String.IsNullOrEmpty(crition) || String.IsNullOrEmpty(format))
                return null;
            object oCri = ULCode.QDA.XSql.GetValue(String.Format(script, id, this.CurUserId, this.ThisDate));
            if (oCri == null || oCri == Convert.DBNull) return null;
            int iCri = Convert.ToInt32(oCri);
            int diff=Convert.ToInt32(crition) - iCri;
            string[] f_arr = format.Split('|');
            if (diff >= 0)
            {
                return String.Format("<span style='color:green;'>" + f_arr[0] + "</span>", iCri, crition, diff);
            }
            else
            {
                return String.Format("<span style='color:red;'>" + f_arr[1] + "</span>", iCri, crition, diff);
            }
        }
        private string GetDescFld(string descFld, int id)
        {
            string desc = null;
            if (this.DetailsDataTable != null)
            {
                System.Data.DataRow[] drs = this.DetailsDataTable.Select(String.Format("pd_id='{0}'", id));
                if (drs.Count() == 1)
                {
                    desc = Convert.ToString(drs[0]["desc"]);
                }
            }
            if (String.IsNullOrEmpty(descFld) || descFld.ToLower() == "text")
            {
                return String.Format("请输入内容：<input type='text' name='desc_{0}' style='width:500px' value='{1}' />", id, desc);
            }
            else if (descFld.ToLower() == "textarea")
            {
                return String.Format("请输入内容：<br/><textarea name='desc_{0}' style='width:500px;height:50px;' />{1}</textarea>", id, desc);
            }
            else
                return descFld;
        }
        private int GetValue(int id)
        {
            int value = 0;
            if (this.DetailsDataTable != null)
            {
                System.Data.DataRow[] drs = this.DetailsDataTable.Select(String.Format("pd_id='{0}'", id));
                if (drs.Count() == 1)
                {
                    value = Convert.ToInt32(drs[0]["value"]);
                }
            }
            return value;
        }
        private string GetItemHtml1(int id, string items,string descFld)
        {
            return String.Format(this.GetDescFld(descFld, id), id); 
        }
        private string GetItemHtml2(int id, string items,string descFld)
        {
            int value = this.GetValue(id);
            string sel = value == 1 ? "checked='checked'" : "";
            string display = value == 0 ? "display:none;" : "";
            return String.Format("<input type='checkbox' id='cb_{0}' value='1' {1}  name='cb_{0}' onclick=\"$('#div_{0}').css('display',$('#cb_{0}').attr('checked')?'block':'none')\" />是/否<div id='div_{0}' style='{2} vertical-align:top;'>" + this.GetDescFld(descFld, id) + "</div>", id, sel, display);
        }
        private string GetItemHtml3(int id, string items,string descFld)
        {
            int value = this.GetValue(id);
            string display = value == 0 ? "display:none;" : "";
            StringBuilder sb = new StringBuilder();
            sb.Append("<select id='select_{0}' name='select_{0}' style='width:200px;' onchange=\"$('#div_{0}').css('display',$('#select_{0}').val()!='0'?'block':'none')\" >");
            string[] items_arr = items.Split(new String[] { "=",";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= items_arr.Length - 1; i += 2)
            {
                string sel = Convert.ToInt32(items_arr[i]) == value ? "selected='selected'" : "";
                sb.AppendFormat("<option value='{0}' {2}>{1}</option>", items_arr[i], items_arr[i + 1], sel);
            }
             sb.Append("</select>");
             return String.Format(sb.ToString() + "<div id='div_{0}' style='{1} vertical-align:top;'>" + this.GetDescFld(descFld, id) + "</div>", id, display);
        }
        private string GetItemHtml4(int id, string items, string descFld)
        {
            int value = this.GetValue(id);
            string display = value == 0 ? "display:none;" : "";
            StringBuilder sb = new StringBuilder();
            string[] items_arr = items.Split(new String[] { "=", ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= items_arr.Length - 1; i += 2)
            {
                string sel = Convert.ToInt32(items_arr[i]) == value ? "checked='checked'" : "";
                sb.AppendFormat("<input type='radio' name='rb_{0}' value='{1}' {3} onclick=\"$('#div_{0}').css('display',$(this).val()!='-1'?'block':'none')\" />{2}", id, items_arr[i], items_arr[i + 1], sel);
            }
            return String.Format(sb.ToString() + "<div id='div_{0}' style='{1} vertical-align:top;'>" + this.GetDescFld(descFld, id) + "</div>", id, display);
        }
        private string GetItemHtml5(int id, string items, string descFld)
        {
            int value = this.GetValue(id);
            string display = value == 0 ? "display:none;" : "";
            StringBuilder sb = new StringBuilder();
            string[] items_arr = items.Split(new String[] { "=", ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= items_arr.Length - 1; i += 2)
            {
                string sel = (Convert.ToInt32(items_arr[i]) & value) == Convert.ToInt32(items_arr[i]) ? "checked='checked'" : "";
                sb.AppendFormat("<input type='checkbox' name='cb_{0}' {3} value='{1}' onclick=\"$('#div_{0}').css('display',$(this).val()!='-1'?'block':'none')\" />{2}", id, items_arr[i], items_arr[i + 1], sel);
            }
            return String.Format(sb.ToString() + "<div id='div_{0}' style='{1} vertical-align:top;'>" + this.GetDescFld(descFld, id) + "</div>", id, display);
        }
        //*****************************************************
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = this.ProgramsDataTable;
            StringBuilder sb = new StringBuilder();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr["id"]);
                int type = Convert.ToInt32(dr["Type"]);
                string items = Convert.ToString("Items");
                string descFld = Convert.ToString("descFld");
                string value = null;
                string desc = this.Request.Form[String.Format("desc_{0}", id)];
                if (type == 1)
                {
                }
                else if(type==2)
                {
                    value = Convert.ToString(this.Request.Form[String.Format("cb_{0}", id)]);
                    if (String.IsNullOrEmpty(value))
                        value = "0";
                }
                else if (type == 3)
                {
                    value = Convert.ToString(this.Request.Form[String.Format("select_{0}", id)]); 
                }
                else if (type == 4)
                {
                    value = Convert.ToString(this.Request.Form[String.Format("rb_{0}", id)]);
                }
                else if (type == 5)
                {
                    value = Convert.ToString(this.Request.Form[String.Format("cb_{0}", id)]);
                    if (!String.IsNullOrEmpty(value))
                    {
                        string[] value_arr = value.Split(new String[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                        int ivalue = 0;
                        foreach (string v in value_arr)
                        {
                            ivalue |= Convert.ToInt32(v);
                        }

                        value = Convert.ToString(ivalue);
                    }
                }
                if (String.IsNullOrEmpty(value))
                    value = "0";
                string sSql = "if exists(select * from PRIV_PD_Details where pd_id={0} and Date='{1:yyyy-MM-dd}')"
                        + " update PRIV_PD_Details set [value]='{2}',[desc]='{3}' where pd_id={0} and Date='{1:yyyy-MM-dd}';"
                        + " else "
                        + " insert PRIV_PD_Details(pd_id,[Date],[value],[desc],[userid]) values({0},'{1:yyyy-MM-dd}',{2},'{3}','{4}');";
                sb.AppendFormat(sSql, id, this.ThisDate, value, desc, this.CurUserId);
            }
            //Response.Write(sb.ToString());
            if (sb.Length > 0)
            {
                ULCode.QDA.XSql.Execute(sb.ToString());
                this.Load_Details_Data();
                this.Bind_DP_Data();
            }
        }   
        protected void btnNextDay_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PRIV_PD_Log.aspx?Date={0:yyyy-MM-dd}", this.ThisDate.AddDays(1)));
        }

        protected void btnPreviousDay_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PRIV_PD_Log.aspx?Date={0:yyyy-MM-dd}", this.ThisDate.AddDays(-1)));
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PRIV_PD_Log.aspx?Date={0:yyyy-MM-dd}", DateTime.Now));
        }

    }
}