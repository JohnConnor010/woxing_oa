using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Duty_DutyGive : System.Web.UI.Page
    {
        DataTable DT_Duties = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["DutyDetailID"] != null)
                {
                    WX.Model.DutyDetail.MODEL model = WX.Request.rDutyDetail; //WX.Model.DutyDetail.GetModel(WX.Request.rDutyDetailID);
                    if (model.UsersName.ToString() != "")
                    {
                        ULCode.Debug.Alert(this, "此职务有人员任职，不可删除！");
                    }
                    else
                    {
                        int iR = model.Delete();
                        if (iR > 0)
                        {
                            model.RemoveFromCaches();
                        }
                        //WX.Main.ExecuteDelete("TE_DutyDetail", "ID", Request["DutyDetailID"]);
                    }
                }
                this.FillData();
            }
        }
        private void FillData()
        {
            this.DT_Duties = ULCode.QDA.XSql.GetDataTable("select ID,Name,DutyCatagoryID from te_duties ");
            //添加职务分类行
            DataTable dtC = WX.Data.Dict.GetDataTable_DutyCatagory();
            foreach (DataRow dr in dtC.Rows)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = Convert.ToString(dr["Name"]);
                bf.DataField = "DC" + Convert.ToString(dr["ID"]);
                Gv_duty.Columns.Add(bf);
            }
            //添加部门列
            System.Data.DataTable dtR = WX.Model.DutyDetail.GetDeptList();
            Gv_duty.DataSource = dtR;
            Gv_duty.DataBind();
            Gv_duty.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_duty.HeaderStyle.Height = Unit.Pixel(40);
        }
        protected void Gv_duty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    //创建DropDownList
                    DataTable dt = WX.Model.DutyDetail.GetTable(e.Row.Cells[0].Text, e.Row.Cells[i].Text);
                    // WX.Model.DutyDetail.MODEL model = WX.Model.DutyDetail.GetModel(e.Row.Cells[0].Text, e.Row.Cells[i].Text);
                    if (dt.Rows.Count > 0)
                    {
                        Label li = new Label();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            li.Text += "<a title='职务全称：" + dt.Rows[j]["Name"] + "\n当前人员：" + dt.Rows[j]["UsersName"] + "\n职务级别:" + dt.Rows[j]["GradeID"].ToString() + "级（" + dt.Rows[j]["GradeName"].ToString() + "）\n限制人数：" + dt.Rows[j]["Persons"] + "' href=\"javascript:PopupIFrame('Duty_DutyDetailEdit.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + "','编辑具体职务','sdf','sdf',468,240)\">" + (dt.Rows[j]["Name"].ToString().Length > 4 ? dt.Rows[j]["Name"].ToString().Substring(0, 4) : dt.Rows[j]["Name"].ToString()) + "</a><a title='删除' onclick=\"return confirm('您确定要删除吗？');\" href=\"Duty_DutyGive.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + "\"><img width='10' src='/Manage/icon/ico_false.gif'></a>&nbsp;&nbsp;";
                            if (j > 0 && (j + 1) % 5 == 0)
                            {
                                li.Text += "<br/>";
                            }
                        }
                        li.Visible = true;
                        li.ID = "li_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                        e.Row.Cells[i].Controls.Add(li);
                        if (i == 6)
                        {
                            DropDownList ddl = new DropDownList();
                            ddl.ID = "ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                            ddl.Visible = true;
                            ddl.Width = 80;
                            this.DT_Duties.DefaultView.RowFilter = "DutyCatagoryID=" + e.Row.Cells[i].Text;
                            ddl.DataSource = this.DT_Duties.DefaultView.ToTable();
                            ddl.DataTextField = "Name";
                            ddl.DataValueField = "ID";
                            ddl.DataBind();
                            e.Row.Cells[i].Controls.Add(ddl);
                            if (ddl.Items.Count == 0)
                            {
                                ddl.Items.Add(new ListItem("无", ""));
                            }
                            else
                            {
                                ddl.Items.Add(new ListItem("无", ""));
                                //创建按钮
                                ImageButton bt = new ImageButton();
                                bt.OnClientClick = "if(document.getElementById('ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "').value!=''){PopupIFrame('Duty_DutyDetailEdit.aspx?DepartentID=" + e.Row.Cells[0].Text + "&DutyCatagory=" + e.Row.Cells[i].Text + "&ddl='+document.getElementById('ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "').value,'编辑具体职务','li_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "','ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "',468,240);}else{alert('请选择职务！');}return false;";
                                bt.ImageUrl = "/Manage/icon/icon2_004.png";
                                bt.Width = Unit.Pixel(10);
                                bt.ID = "but_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                                bt.Visible = true;
                                e.Row.Cells[i].Controls.Add(bt);
                            }

                            ddl.SelectedValue = "";
                        }
                    }
                    else
                    {
                        Label li = new Label();
                        li.Text = "";
                        li.Visible = true;
                        li.ID = "li_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                        e.Row.Cells[i].Controls.Add(li);
                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                        ddl.Visible = true;
                        ddl.Width = 80;
                        this.DT_Duties.DefaultView.RowFilter = "DutyCatagoryID=" + e.Row.Cells[i].Text;
                        ddl.DataSource = this.DT_Duties.DefaultView.ToTable();
                        ddl.DataTextField = "Name";
                        ddl.DataValueField = "ID";
                        ddl.DataBind();
                        e.Row.Cells[i].Controls.Add(ddl);
                        if (ddl.Items.Count == 0)
                        {
                            ddl.Items.Add(new ListItem("无", ""));
                        }
                        else
                        {
                            ddl.Items.Add(new ListItem("无", ""));
                            //创建按钮
                            ImageButton bt = new ImageButton();
                            bt.OnClientClick = "if(document.getElementById('ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "').value!=''){PopupIFrame('Duty_DutyDetailEdit.aspx?DepartentID=" + e.Row.Cells[0].Text + "&DutyCatagory=" + e.Row.Cells[i].Text + "&ddl='+document.getElementById('ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "').value,'编辑具体职务','li_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "','ddl_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text + "',468,240);}else{alert('请选择职务！');}return false;";
                            bt.ImageUrl = "/Manage/icon/icon2_004.png";
                            bt.Width = Unit.Pixel(10);
                            bt.ID = "but_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                            bt.Visible = true;
                            e.Row.Cells[i].Controls.Add(bt);
                        }
                        ddl.SelectedValue = "";
                    }
                }
            }
        }
    }
}