using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Duty_Menu : System.Web.UI.Page
    {
        string offstr = "off";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            int companyId = WX.Request.rCompanyId;
            int dutyId = WX.Request.rDutyId;
            if (!IsPostBack)
            {
                WX.Model.Duty.MODEL model = WX.Request.rDuty; 
                this.DutyId.Text = model.ID.value.ToString();
                this.DutyName.Text = model.Name.value.ToString();
                
                string sSql = "exec [dbo].[sp_get_tree_multi_table] 'TE_Menus','ID','Name','ParentID','OrderId',0,1,3";
                DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
                this.Gv_duty.RowDataBound += new GridViewRowEventHandler(WX.Ctrl.GridView_DynamicColor_RowDataBound);
                Gv_duty.DataSource = dt;
                Gv_duty.DataBind();
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            //2.取得用户变量
            int companyId = WX.Request.rCompanyId;
            int Id =WX.Request.rDutyId;

            WX.Model.Duty.MODEL model = WX.Request.rDuty;
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (true)
            {
                DropDownList cbox;
                //WX.Main.ExecuteDelete("TE_MenusInDuties", "DutyID", Id.ToString());
                for (int i = 0; i < Gv_duty.Rows.Count; i++)
                {
                    cbox = (DropDownList)Gv_duty.Rows[i].Cells[1].FindControl("ddlFlag");
                    if (cbox == null)
                        throw new ApplicationException("没有发现控件(ddlFlag)！");
                    string sSql = String.Format("if exists(Select * from TE_MenusInDuties where MenuId={0} and DutyId={1})"
                                + " update TE_MenusInDuties set Flag={2} where MenuId={0} and DutyId={1}"
                                + " else "
                                + " insert into TE_MenusInDuties(MenuId,DutyId,Flag) values({0},{1},{2})",cbox.ToolTip,Id,cbox.SelectedValue);
                    ULCode.QDA.XSql.Execute(sSql);
                    //ULCode.QDA.XSql.Execute("insert into TE_MenusInDuties(MenuId,DutyId,Flag) values(" + cbox.ToolTip + "," + Id + "," + cbox.SelectedValue + ")");
                }
                bDeal = true;
            }
            else
            {
                model.RestoreInitial();
            }
            //5.（用户及业务对象）统计与状态
            if (bDeal)
            {
                WX.Main.CurUser.LoadDutyUser(true);
            }
            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "编辑职务信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Confirm(this, "已成功修改职务信息!是否返回职务列表页？", "Duty_List.aspx?CompanyID="+companyId, this.Request.RawUrl);
            }
            else
            {
                ULCode.Debug.Alert(this,"编辑职务失败,请重试！");
            }
        }
        protected void Gv_duty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] strarry = ((Label)e.Row.Cells[1].FindControl("lblState")).Text.Split('|');
                DropDownList drop1 = (DropDownList)e.Row.Cells[1].FindControl("ddlFlag");
                //drop1.Items.Add(new ListItem(strarry[1] == "3" ? "关闭" : "隐藏", "0"));
                int flag = ULCode.QDA.XSql.GetData("select Flag from TE_MenusInDuties where MenuID=" + strarry[0] + " and DutyID=" +WX.Request.rDutyId).ToInt32();
                drop1.SelectedValue = flag.ToString();
                #region //可删除
                /*
                if (strarry[1] == "3")
                {
                    drop1.Items.Add(new ListItem("打开普通功能", "1"));
                    drop1.Items.Add(new ListItem("打开读写", "2"));
                    drop1.Items.Add(new ListItem("打开删除功能", "3"));
                    object flag = ULCode.QDA.XSql.GetValue("select Flag from TE_MenusInDuties where MenuID=" + strarry[0] + " and DutyID=" + Request["id"]);
                    if (flag != null && flag != Convert.DBNull)
                    {
                        drop1.SelectedValue = flag.ToString();
                    }
                    else
                    {
                        drop1.SelectedValue = strarry[2];
                    }
                    if ((offstr != "off" && (strarry[3] + "-").IndexOf(offstr) > -1) || strarry[2] == "0")
                    {
                        drop1.Enabled = false;
                    }

                }
                else
                {
                    drop1.Items.Add(new ListItem("显示", "3"));
                    if (offstr != "off" && (strarry[3] + "-").IndexOf(offstr) > -1)
                    {
                        drop1.Enabled = false;
                    }
                    if (strarry[2] == "0")
                    {
                        offstr = strarry[0];
                        drop1.SelectedValue = strarry[2];
                        drop1.Enabled = false;
                    }
                    else
                    {
                        object flag = ULCode.QDA.XSql.GetValue("select Flag from TE_MenusInDuties where MenuID=" + strarry[0] + " and DutyID=" + Request["id"]);
                        if (flag != null && flag != Convert.DBNull)
                        {
                            drop1.SelectedValue = flag.ToString();
                        }
                        else
                        {
                            drop1.SelectedValue = "3";
                        }
                    }
                }
                drop1.Width = Unit.Pixel(120);
                 * */
                drop1.ToolTip = strarry[0];
                #endregion
                //e.Row.Cells[1].Controls.Add(drop1);
            }
        }
    }
}