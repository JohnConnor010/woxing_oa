using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Sys
{
    public partial class Duty_DutyDetailEdit : System.Web.UI.Page
    {
        public string mes = "";
        public string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadProgressBarState();

                WX.Data.Dict.BindListCtrl_DeptList(this.ddlParentId, null, null, null);
                
                WX.Data.Dict.BindListCtrl_DutyCatagory(ddlDutyCatagory, null, null, null);
                System.Data.DataTable dtgrade = ULCode.QDA.XSql.GetDataTable("select * from HR_Grade order by Sort asc");
                for (int i = 0; i < dtgrade.Rows.Count; i++)
                {
                    ui_grade.Items.Add(new ListItem(dtgrade.Rows[i]["Name"].ToString(), dtgrade.Rows[i]["Sort"].ToString()));
                }
                if (Request["DutyDetailID"] != null)
                {
                    WX.Model.DutyDetail.MODEL model = WX.Request.rDutyDetail;
                    System.Data.DataTable DT_Duties = ULCode.QDA.XSql.GetDataTable("select ID,Name,DutyCatagoryID from te_duties where DutyCatagoryID=" + model.DutyCatagoryID.ToString());
                    ddlDuty.DataSource = DT_Duties;
                    ddlDuty.DataTextField = "Name";
                    ddlDuty.DataValueField = "ID";
                    ddlDuty.DataBind();
                    this.ddlParentId.SelectedValue = model.DepartentID.ToString();
                    this.ddlDuty.SelectedValue = model.DutyID.ToString();
                    this.ddlDutyCatagory.SelectedValue = model.DutyCatagoryID.ToString();
                    this.ui_grade.SelectedValue = model.GradeID.ToString();
                    this.ui_name.Text = model.Name.ToString();
                    this.ui_persons.Text = model.Persons.ToString();
                    if (model.UsersName.ToString() != "")
                    {
                        this.users.Visible = true;
                        this.ui_users.Text = model.UsersName.ToString();
                    }
                }
                else
                {
                    System.Data.DataTable DT_Duties = ULCode.QDA.XSql.GetDataTable("select ID,Name,DutyCatagoryID from te_duties where DutyCatagoryID=" + Request["DutyCatagory"]);
                    ddlDuty.DataSource = DT_Duties;
                    ddlDuty.DataTextField = "Name";
                    ddlDuty.DataValueField = "ID";
                    ddlDuty.DataBind();
                    this.ddlParentId.SelectedValue = Request["DepartentID"];
                    this.ddlDuty.SelectedValue = Request["ddl"];
                    this.ddlDutyCatagory.SelectedValue = Request["DutyCatagory"];
                    ui_name.Text = ddlParentId.SelectedItem.Text + ddlDuty.SelectedItem.Text;
                }
            }
        }
        private void LoadProgressBarState()
        { 
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            WX.Model.DutyDetail.MODEL model;
            if (Request["DutyDetailID"] != null)
            {
                model = WX.Request.rDutyDetail;
                model.Name.value = ui_name.Text; 
                model.Persons.value = ui_persons.Text;
                model.GradeID.value = ui_grade.SelectedValue;
                model.Update();
                this.id = WX.Request.rDutyDetailID.ToString();
                mes = "sub2();";
            }
            else
            {
                model = WX.Model.DutyDetail.NewDataModel(); 
                model.Name.value = ui_name.Text;
                model.DutyCatagoryID.value = ddlDutyCatagory.SelectedValue;
                model.DutyID.value = ddlDuty.SelectedValue;
                model.DepartentID.value = ddlParentId.SelectedValue;
                model.Persons.value = ui_persons.Text;
                model.GradeID.value = ui_grade.SelectedValue;
                this.id = model.Insert(true).ToString();
                if (Convert.ToInt32(this.id) > 0)
                {
                    model.SaveIntoCaches();
                }
                mes = "sub();";
            }
           // WX.Main.ExcuteUpdate("TU_Users", "Grade=" + model.GradeID.ToString(), "DutyId="+id);
        }
        public string getdutylist()
        {
            if (Request["DutyDetailID"] != null)
            {
                WX.Model.DutyDetail.MODEL model = WX.Request.rDutyDetail;
                System.Data.DataTable dt = WX.Model.DutyDetail.GetTable(model.DepartentID.ToString(), model.DutyCatagoryID.ToString());
                string str = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string color = "";
                    if (Request["type"] != null && Request["type"] == "1")
                    {
                        int persons = Convert.ToInt32(dt.Rows[j]["Persons"].ToString());
                        int users = dt.Rows[j]["UsersName"].ToString() == "" ? 0 : dt.Rows[j]["UsersName"].ToString().Split(',').Length - 1;
                        if (persons == 0 && users == 0)
                            color = " style='color:#aaaaaa;'";
                        else if (persons > users)
                            color = " style='color:red;'";
                    }
                    str += "<a" + color + " title='职务全称：" + dt.Rows[j]["Name"] + "--当前人员：" + dt.Rows[j]["UsersName"] + "' href=javascript:PopupIFrame('Duty_DutyDetailEdit.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + (Request["type"] != null ? "&type=" + Request["type"] : "") + "','编辑具体职务','sdf','sdf',468,240)>" + (dt.Rows[j]["Name"].ToString().Length > 4 ? dt.Rows[j]["Name"].ToString().Substring(0, 4) : dt.Rows[j]["Name"].ToString()) + (Request["type"] != null && Request["type"] == "1" ? "（" + dt.Rows[j]["Persons"].ToString() + "/" + (dt.Rows[j]["UsersName"].ToString() == "" ? 0 : dt.Rows[j]["UsersName"].ToString().Split(',').Length - 1) + "）</a>" : "</a><a title='删除' href='Duty_DutyGive.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + "'><img width='10' src='/Manage/icon/ico_false.gif'></a>") + "&nbsp;&nbsp;";
                    if (j > 0 && (j + 1) % 5 == 0)
                    {
                        str += "<br/>";
                    }
                }
                return str;
            }
            else
            {
                System.Data.DataTable dt = WX.Model.DutyDetail.GetTable(ddlParentId.SelectedValue, ddlDutyCatagory.SelectedValue);
                string str = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    str += "<a title='职务全称：" + dt.Rows[j]["Name"] + "--当前人员：" + dt.Rows[j]["UsersName"] + "' href=javascript:PopupIFrame('Duty_DutyDetailEdit.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + "','编辑具体职务','sdf','sdf',468,240)>" + (dt.Rows[j]["Name"].ToString().Length > 4 ? dt.Rows[j]["Name"].ToString().Substring(0, 4) : dt.Rows[j]["Name"].ToString()) + "</a><a title='删除' href='Duty_DutyGive.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + "'><img width='10' src='/Manage/icon/ico_false.gif'></a>&nbsp;&nbsp;";
                    if (j > 0 && (j + 1) % 5 == 0)
                    {
                        str += "<br/>";
                    }
                }
                return str;
            }
        }
    }
}