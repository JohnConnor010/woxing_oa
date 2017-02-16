using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace wwwroot.Manage.CRM
{
    public partial class CRM_SingleM_EditTrack : System.Web.UI.Page
    {
        public string mess = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 0; i < WX.CRM.Track.ProcessState.Length; i++)
                {
                    ddlProcessState.Items.Add(new ListItem(WX.CRM.Track.ProcessState[i], i.ToString()));
                }
                WX.CRM.Customer.MODEL customer;
                if (Request["TrackID"] != null && Request["TrackID"] != "")
                {
                    WX.CRM.Track.MODEL track = WX.CRM.Track.NewDataModel(Request["TrackID"]);
                    customer = WX.CRM.Customer.NewDataModel(track.CustomerID.ToString());
                    ddlProcessState.SelectedValue = track.ProcessState.ToString();
                    ddlProcessState.Enabled = false;
                    txtTrackNo.Text = track.TrackNo.ToString();
                    txtFee.Text = Convert.ToDouble(track.Fee.value).ToString("#.00");
                    if (!track.Remarks.isEmpty)
                    {
                        string[] arr_track = track.Remarks.ToString().Split('|');
                        txtremark.Text = arr_track[0];
                        txtremark2.Text = arr_track[1];
                        txtremark3.Text = arr_track[2];
                        if (arr_track.Length > 3) txtremark4.Text = arr_track[3];
                    }
                    txtLogParaments.Text = track.LogParaments.ToString();
                    jhdate.Text = track.TrackTime.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                    tr1.Visible = true;
                    LinkButton2.Text = "计划时间";
                    rblstate.SelectedValue = "1";
                    if (ddlProcessState.SelectedValue == "4")
                    {
                        ccp.Visible = true;
                        WX.CRM.CustomerProgram.MODEL program = WX.CRM.CustomerProgram.GetModel("SELECT * FROM CRM_CustomerProgram where TrackID=" + Request["TrackID"]);
                        txtcustomername.Text = program.Title.ToString();
                        txtContent.Text = program.Content.ToString();
                        txtLK.Text = program.Remarks.ToString();
                        txtTime.Text = program.ProgramTime.ToDateTime().ToString("yyyy-MM-dd");
                        TablerowContent();
                    }
                    else if (ddlProcessState.SelectedValue == "5")
                        SetTr2();
                    else
                    {
                        ccp.Visible = false;
                    }
                }
                else
                {
                    customer = WX.CRM.Customer.NewDataModel(WX.Request.rCustomerID);
                }
                txtcustomername.Text = customer.CustomerName.ToString() + "合作方案";
                txtcustomername.Style.Value = "text-align:center;";
                txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (ddlProcessState.SelectedValue == "4")
            {
                ccp.Visible = true;
            }
            else
            {
                ccp.Visible = false;
            }
            WX.CRM.Track.MODEL track;
            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                track = WX.CRM.Track.NewDataModel(Request["TrackID"]);
            }
            else
            {
                track = WX.CRM.Track.NewDataModel();
            track.UserID.value = WX.Main.CurUser.UserID;
            }
            track.ProcessState.value = ddlProcessState.SelectedValue;
            track.CustomerID.value = WX.Request.rCustomerID;
            track.TrackNo.value = txtTrackNo.Text;
            track.Fee.value = txtFee.Text;
            track.Remarks.value = txtremark.Text + "|" + txtremark2.Text + "|" + txtremark3.Text + "|" + txtremark4.Text;
            track.IP.value = System.Web.HttpContext.Current.Request.UserHostAddress;
            track.LogParaments.value = txtLogParaments.Text;
            track.Type.value = 0;
            if (txtTrackTime.Text.Trim() == "")
            {
                mess = "Messages('跟踪时间必填！')";
                return;
            }
            track.TrackTime.value = txtTrackTime.Text;
            track.State.value = rblstate.SelectedValue; //track.TrackTime.ToDateTime() < DateTime.Now ? 1 : 0;

            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                track.Update();
                if (track.ProcessState.ToInt32() == 4)
                {
                    WX.CRM.CustomerProgram.MODEL programmodel = WX.CRM.CustomerProgram.GetModel("SELECT * FROM CRM_CustomerProgram where TrackID=" + Request["TrackID"]);
                    if (programmodel != null)
                    {
                        SetProgram(programmodel, track.id.ToInt32());
                    }
                }
                else if (track.ProcessState.ToInt32() == 5)
                {

                    WX.CRM.CustomerAgreement.MODEL agreementmodel = WX.CRM.CustomerAgreement.GetModel("SELECT * FROM CRM_CustomerAgreement where TrackID=" + Request["TrackID"]);
                    SetAgreement(agreementmodel, track.id.ToInt32());
                }
            }
            else
            {
                int trackid = track.Insert(true);
                if (track.ProcessState.ToInt32() == 4)
                {
                    WX.CRM.CustomerProgram.MODEL programmodel = WX.CRM.CustomerProgram.NewDataModel();
                    SetProgram(programmodel, trackid);
                }
                else if (track.ProcessState.ToInt32() == 5)
                {
                    WX.CRM.CustomerAgreement.MODEL agreementmodel = WX.CRM.CustomerAgreement.NewDataModel();
                    SetAgreement(agreementmodel, trackid);
                }
            }
            //当签订协议后更改促成方案的状态
            if (track.ProcessState.ToInt32() == 5 && track.State.ToInt32() == 1 && DropProgram.SelectedValue != "0")
            {
                WX.CRM.CustomerProgram.MODEL cprogram = WX.CRM.CustomerProgram.NewDataModel(DropProgram.SelectedValue);
                cprogram.State.value = 1;
                cprogram.Updatetime.value = DateTime.Now;
                cprogram.Update();
            }
            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(track.CustomerID.value);
            if (customer.StageID.ToInt32() == 0 && (track.ProcessState.ToInt32() == 0 || track.ProcessState.ToInt32() == 1))
            {
                customer.StageID.value = 1;
            }
            else if (customer.StageID.ToInt32() == 1 && (track.ProcessState.ToInt32() == 2 || track.ProcessState.ToInt32() == 3 || track.ProcessState.ToInt32() == 4))
            {
                customer.StageID.value = 2;
            }
            else if (customer.StageID.ToInt32() == 2 && (track.ProcessState.ToInt32() == 5 || track.ProcessState.ToInt32() == 6 || track.ProcessState.ToInt32() == 7 || track.ProcessState.ToInt32() == 8))
            {
                customer.StageID.value = 3;
            }
            else if (customer.StageID.ToInt32() == 3 && track.ProcessState.ToInt32() > 8)
            {
                customer.StageID.value = 4;
            }
            if (rblstate.SelectedValue == "1" && Convert.ToDouble(track.Fee.value) > 0)
            {
                WX.CRM.CustomerTemp.MODEL temp = WX.Request.rCustomerTempToCID;
                if (temp != null)
                {
                    temp.LastMaintainMoney.value = track.Fee.value;
                    temp.MaintainMoney.value = Convert.ToDouble(temp.MaintainMoney.value) + Convert.ToDouble(track.Fee.value);
                    temp.Update();
                    customer.LastMaintainMoney.value = temp.LastMaintainMoney.value;
                    customer.MaintainMoney.value = temp.MaintainMoney.value;
                }
                else
                {
                    customer.LastMaintainMoney.value = track.Fee.value;
                    customer.MaintainMoney.value = Convert.ToDouble(customer.MaintainMoney.value) + Convert.ToDouble(track.Fee.value);
                }
            }
            customer.Update();

            WX.CRM.Customer.AddLog(customer.ID.ToInt32(), customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 11, ddlProcessState.SelectedItem.Text);
            if (track.ProcessState.ToInt32() == 5)
            {
                Response.Redirect("CRM_SingleM_EditAgreement.aspx?TrackID="+track.id.ToString());
            }
            else
            {
                WX.Main.CloseDialog_In_EasyUIDialog(this, "提交成功！");
            }
        }
        public void SetProgram(WX.CRM.CustomerProgram.MODEL programmodel, int pid)
        {
            decimal allfee = 0;
            for (int i = 0; i < CustomerRepeater.Items.Count; i++)
            {
                if (((CheckBox)CustomerRepeater.Items[i].FindControl("CheckBox1")).Checked)
                {
                    try
                    {
                        allfee += Convert.ToDecimal(((TextBox)CustomerRepeater.Items[i].FindControl("zdfee1")).Text);
                    }
                    catch { }
                }
            }
            programmodel.CustomerID.value = WX.Request.rCustomerID;
            programmodel.TrackID.value = pid;
            programmodel.ZDFee.value = allfee;
            programmodel.Title.value = txtcustomername.Text;
            programmodel.Content.value = txtContent.Text;
            programmodel.ProgramTime.value = txtTime.Text;
            programmodel.Remarks.value = txtLK.Text;
            if (Request.Files["tfile"].FileName != "")
            {
                DateTime nowtime = DateTime.Now;
                Random rd = new Random();
                HttpPostedFile postfile = Request.Files["tfile"];
                string filepath = "/UploadFiles/crm/" + nowtime.ToString("yyyyMMddHHmmss") + rd.Next(10000, 999999) + System.IO.Path.GetExtension(postfile.FileName);
                postfile.SaveAs(Server.MapPath(filepath));
                programmodel.Program.value = filepath;
            } int programid = 0;
            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                if (programmodel.UserID.ToString() == "")
                    programmodel.UserID.value = WX.Main.CurUser.UserID;
                programmodel.Update();
                programid = programmodel.id.ToInt32();
                WX.Main.ExecuteDelete("CRM_CustomerProducts", "PID=" + programmodel.id.ToString() + " and Type", "1");
            }
            else
            {
                programmodel.UserID.value = WX.Main.CurUser.UserID;
                programid = programmodel.Insert(true);
            }
            SetProduct(programid, 1);
        }
        public void SetAgreement(WX.CRM.CustomerAgreement.MODEL agreementmodel, int pid)
        {
            decimal allfee = 0;
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                if (((CheckBox)Repeater2.Items[i].FindControl("CheckBox2")).Checked)
                {
                    try
                    {
                        allfee += Convert.ToDecimal(((TextBox)Repeater2.Items[i].FindControl("zdfee2")).Text);
                    }
                    catch { }
                }
            }
            agreementmodel.CustomerID.value = WX.Request.rCustomerID;
            agreementmodel.TrackID.value = pid;
            agreementmodel.TempID.value = 0;
            agreementmodel.AllFee.value = allfee;
            agreementmodel.OverFee.value = txtOverFee.Text;
            agreementmodel.Fee.value=Convert.ToDecimal(agreementmodel.AllFee.value)-Convert.ToDecimal(agreementmodel.OverFee.value);
            agreementmodel.OverTime.value=txtOverTime.Text;
            agreementmodel.Invoice.value=txtInvoice.Text;
            agreementmodel.OverInvoice.value = Convert.ToDecimal(agreementmodel.AllFee.value) - Convert.ToDecimal(agreementmodel.Invoice.value);
            agreementmodel.Addtime.value = txtAddtime.Text;
            agreementmodel.StartTime.value = txtStartTime.Text;
            agreementmodel.StopTime.value = txtStopTime.Text;
            if (DropProgram.SelectedValue != "0")
                agreementmodel.ProgramID.value = DropProgram.SelectedValue;
            int agreementid = 0;
            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                agreementmodel.Update();
                agreementid = agreementmodel.id.ToInt32();
                WX.Main.ExecuteDelete("CRM_CustomerProducts", "PID=" + agreementid + " and Type", "2");
            }
            else
            {
                agreementmodel.UserID.value = WX.Main.CurUser.UserID;
                agreementid = agreementmodel.Insert(true);
            }
            SetProduct(agreementid, 2);
        }
        public void SetProduct(int pid, int type)
        {
            int count = type == 1 ? CustomerRepeater.Items.Count : Repeater2.Items.Count;
            Repeater rep = type == 1 ? CustomerRepeater : Repeater2;
            if (type == 2)
                WX.Main.ExecuteDelete("CRM_CustomerDept", "AgreementID", pid.ToString());
            for (int i = 0; i < count; i++)
            {
                CheckBox checkboxitem = (CheckBox)rep.Items[i].FindControl("CheckBox" + type);
                if (checkboxitem.Checked)
                {
                    WX.CRM.CustomerProducts.MODEL product = WX.CRM.CustomerProducts.NewDataModel();
                    product.CustomerID.value = WX.Request.rCustomerID;
                    product.PID.value = pid;
                    product.Type.value = type;
                    product.ProductID.value = checkboxitem.ToolTip;
                    product.Remarks.value = ((TextBox)rep.Items[i].FindControl("Remarks" + type)).Text;
                    product.ZDFee.value = ((TextBox)rep.Items[i].FindControl("zdfee" + type)).Text;
                    WX.PDT.Product.MODEL productmodel = WX.PDT.Product.NewDataModel(product.ProductID.value);
                    product.ProductName.value = productmodel.ProductName.value;
                    product.Specification.value = productmodel.Specification.value;
                    product.Units.value = productmodel.Units.value;
                    product.SalesPrice.value = productmodel.SalesPrice.value;
                    product.DiscountedPrice.value = productmodel.DiscountedPrice.value;
                    product.CostPrice.value = productmodel.CostPrice.value;
                    product.Remark.value = productmodel.Remark.value;
                    product.Services.value = productmodel.Services.value;
                    product.Insert();
                    if (type == 2)
                        ULCode.QDA.XSql.Execute("insert into CRM_CustomerDept select *," + pid + " from PDT_ProductDept where ProductID = " + product.ProductID.ToString());
                }
            }
        }
        private void TablerowContent()
        {
            string sql = "SELECT *,null ccpID,null ZDFee,null Remarks FROM [PDT_Products] where IsEnable=1";
            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                WX.CRM.CustomerProgram.MODEL program = WX.CRM.CustomerProgram.GetModel("SELECT * FROM CRM_CustomerProgram where TrackID=" + Request["TrackID"]);

                sql = "SELECT pp.*,ccp.id ccpID,ccp.ZDFee,ccp.Remarks FROM [PDT_Products] pp left join CRM_CustomerProducts ccp on pp.id=ccp.ProductID and PID=" + program.id.ToString() + " and  ccp.Type=1 where pp.IsEnable=1";
            }
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sql);
            CustomerRepeater.DataSource = dt;
            CustomerRepeater.DataBind();
        }
        private void SetTr2()
        {
            Tr2.Visible = true;
            if (DropProgram.Items.Count == 0)
            {
                DropProgram.DataSource = ULCode.QDA.XSql.GetDataTable("select Title,id from CRM_CustomerProgram where CustomerID=" + WX.Request.rCustomerID);
                DropProgram.DataTextField = "Title";
                DropProgram.DataValueField = "id";
                DropProgram.DataBind();
                DropProgram.Items.Add(new ListItem("无", "0"));
                DropProgram.SelectedValue = "0";
            }
            string sql = "SELECT *,null ccpID,null ZDFee,null Remarks FROM [PDT_Products] where IsEnable=1";
            if (Request["TrackID"] != null && Request["TrackID"] != "")
            {
                WX.CRM.CustomerAgreement.MODEL agreement = WX.CRM.CustomerAgreement.GetModel("SELECT * FROM CRM_CustomerAgreement where TrackID=" + Request["TrackID"]);
                if (agreement != null)
                {
                    DropProgram.SelectedValue = agreement.ProgramID.ToString();

                    txtOverFee.Text = agreement.OverFee.ToString();
                    txtOverTime.Text =agreement.OverTime.ToString()!=""? agreement.OverTime.ToDateTime().ToString("yyyy-MM-dd"):"";
                    txtInvoice.Text = agreement.Invoice.ToString();
                    txtAddtime.Text = agreement.Addtime.ToString()!=""? agreement.Addtime.ToDateTime().ToString("yyyy-MM-dd"):"";
                    txtStartTime.Text = agreement.StartTime.ToString()!=""? agreement.StartTime.ToDateTime().ToString("yyyy-MM-dd"):"";
                    txtStopTime.Text = agreement.StopTime.ToString() != "" ? agreement.StopTime.ToDateTime().ToString("yyyy-MM-dd") : "";
                }
                sql = "SELECT pp.*,ccp.id ccpID,ccp.ZDFee,ccp.Remarks FROM [PDT_Products] pp left join CRM_CustomerProducts ccp on pp.id=ccp.ProductID and PID=" + agreement.id.ToString() + " and ccp.Type=2 where pp.IsEnable=1";
            }
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sql);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        protected void ddlProcessState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ccp.Visible = false; Tr2.Visible = false;
            switch (ddlProcessState.SelectedValue)
            {
                case "4": ccp.Visible = true; TablerowContent(); break;
                case "5": SetTr2(); break;
                default: ccp.Visible = false; Tr2.Visible = false; break;
            }
        }
        private string GetDateStr(int no)
        {
            DateTime jndate;
            try
            {
                jndate = Convert.ToDateTime(txtTrackTime.Text.Trim() == "" ? jhdate.Text : txtTrackTime.Text.Trim());
            }
            catch { jndate = DateTime.Now; }
            return jndate.AddDays(no).ToString("yyyy-MM-dd HH:mm:ss");

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            txtTrackTime.Text = GetDateStr(-1);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (Request["TrackID"] != null && Request["TrackID"] != "")
                txtTrackTime.Text = jhdate.Text;
            else
                txtTrackTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            txtTrackTime.Text = GetDateStr(1);
        }
    }
}