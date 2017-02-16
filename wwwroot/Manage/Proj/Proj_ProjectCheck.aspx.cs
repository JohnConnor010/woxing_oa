using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_ProjectCheck : System.Web.UI.Page
    {
        string hivalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    WX.PRO.Project.MODEL model = WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" + WX.Request.rProjectId);
                    if (model != null)
                    {
                        li_name.Text = model.ProjectName.ToString();
                        ui_Name.Text = model.ProjectName.ToString();
                        li_days.Text = model.Days.ToString();
                        ui_days.Text = model.Days.ToString();
                        if (Convert.ToInt32(model.Persons.ToString()) > 0)
                        {
                            ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select pu.UserID,te.RealName from PRO_User pu left join TU_Users te on pu.UserID=te.UserID where pu.type=1 and pid=" + model.ID.ToString());
                            li_persons.Text = xdt.ToColValueList("，", 1);
                            ui_Persons.Value = xdt.ToColValueList(",", 0);
                            liu_Persons.Text = xdt.ToColValueList(",", 1);
                        }
                        li_fee.Text = model.Fee.ToString();
                        ui_fee.Text = model.Fee.ToString();
                        li_content.Text = model.Content.ToString();
                        li_Imagine.Text = model.Imagine.ToString();
                        li_state.Text = WX.PRO.Project.statearray[Convert.ToInt32(model.State.ToString())];
                        if (model.Annex.ToString() != "")
                            li_annex.Text = "<a href='" + model.Annex.ToString() + "'>" + model.Annex.ToString() + "</a>";
                        if (model.State.ToString() == "1" || model.State.ToString() == "0")
                        {
                            tr1.Visible = false;
                            tr2.Visible = false;
                            checkdata.Visible = model.State.ToString() != "0";
                        }
                        else
                        {
                            WX.PRO.State.MODEL statemodel = WX.PRO.State.GetModel("select * from PRO_State where ProjID=" + model.ID.ToString());
                            if (statemodel.ProcID.ToString() != ""&&statemodel.ProcID.ToString()!="0")
                            {
                                //WX.PRO.Process.MODEL procmodel = WX.PRO.Process.GetModel("select top 1 * from PRO_Process where ProjID=" + statemodel.ProjID.ToString() + " and NO="+statemodel.ProcID.ToString());
                                hivalue = statemodel.ProcID.ToString();
                            }
                            li_projmanage.Text = WX.CommonUtils.GetRealNameListByUserIdList(statemodel.Manage.ToString());
                            li_checkfee.Text = statemodel.Fee.ToString();
                            li_checkmanage.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Manage.ToString());
                            li_checkopinion.Text = model.Opinion.ToString();
                            li_checktime.Text = Convert.ToDateTime(model.Stime.value).ToString("yyyy-MM-dd HH:mm:ss");
                            checkdata.Visible = false;
                        }

                    }
                ReBind();
            }
        }
        private void ReBind()
        {
            string sql = "SELECT * FROM PRO_Process where ProjID=" + WX.Request.rProjectId + " order by NO asc";
            var supplierData = ULCode.QDA.XSql.GetDataTable(sql);
            this.SupplierRepeater.DataSource = supplierData;
            this.SupplierRepeater.DataBind();
        }
        public string getclass(object id)
        {
            if (hivalue==id.ToString())
                return "selectproc";
            else
            return "";
        }
        public bool getimg(object id)
        {
            if (id.ToString() == hivalue)
                return true;
            else
                return false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.PRO.Project.MODEL model = WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" + WX.Request.rProjectId);
            model.Manage.value = WX.Main.CurUser.UserID;
            model.Opinion.value = ui_Opinion.Text.Trim();
            model.Stime.value = DateTime.Now;
            if (((Button)sender).ID == "Button1")
            {
                model.ProjectName.value = ui_Name.Text.Trim();
                model.Days.value = ui_days.Text.Trim();
                //model.Fee.value = ui_fee.Text.Trim();
                string[] userarry = ui_Persons.Value.Trim().Split(',');
                model.Persons.value = ui_Persons.Value != null ? userarry.Length : 0;
                model.State.value =2;
                //4.业务处理过程
                    model.Update();
                if (ui_Persons.Value != null)
                {
                    ULCode.QDA.XSql.Execute("delete from PRO_User where type=1 and pid=" + model.ID.ToString());
                    for (int i = 0; i < userarry.Length; i++)
                    {
                        WX.PRO.User.MODEL usermodel = WX.PRO.User.NewDataModel();
                        usermodel.PID.value = model.ID.ToString();
                        usermodel.type.value = 1;
                        usermodel.UserID.value = userarry[i];
                        usermodel.Insert();
                    }
                }
                ULCode.QDA.XSql.Execute("delete from PRO_State where ProjID="+model.ID.ToString());
                WX.PRO.State.MODEL statemodel = WX.PRO.State.NewDataModel();
                statemodel.ProjID.value = model.ID.value;
                if(ui_manage.Value!="")
                statemodel.Manage.value = ui_manage.Value;
                statemodel.Fee.value = ui_fee.Text.Trim();
                statemodel.Percnt.value = 0;
                statemodel.Percnttime.value = 0;
                statemodel.State.value = 0;
                statemodel.YJStarttime.value = ui_yjstarttime.Text;
                statemodel.Insert();
                WX.PRO.Process.SetTime(Convert.ToDateTime(statemodel.YJStarttime.ToString()),Convert.ToInt32(statemodel.ProjID.value),1);
                //5.登记日志
                WX.PRO.Log.AddLog(1, Convert.ToInt32(model.ID.value), "审核通过。审批意见：" + model.Opinion.ToString(), Request.UserHostAddress);
            }
            else
            {
                model.State.value = 3;
                model.Update();
                WX.PRO.Log.AddLog(1, Convert.ToInt32(model.ID.ToString()), model.ProjectName.ToString() + "的申请被退回。原因：" + model.Opinion.ToString(), Request.UserHostAddress);
            }
            //6.返回处理结果或返回其它页面。
            Response.Redirect("Proj_ProjectManage.aspx");
        }
    }
}