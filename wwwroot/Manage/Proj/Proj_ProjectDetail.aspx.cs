using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_ProjectDetail : System.Web.UI.Page
    {
        string hivalue = "";
        int rowcount = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    WX.PRO.Project.MODEL model = WX.Request.rProject;
                    if (model != null)
                    {
                        li_name.Text = model.ProjectName.ToString();
                        li_days.Text = model.Days.ToString();
                        if (Convert.ToInt32(model.Persons.ToString()) > 0)
                        {
                            ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select pu.UserID,te.RealName from PRO_User pu left join TU_Users te on pu.UserID=te.UserID where pu.type=1 and pid=" + model.ID.ToString());
                            li_persons.Text = xdt.ToColValueList("，", 1);
                        }
                        li_fee.Text = model.Fee.ToString();
                        li_content.Text = model.Content.ToString();
                        li_Imagine.Text = model.Imagine.ToString();
                        li_state.Text = WX.PRO.Project.statearray[Convert.ToInt32(model.State.ToString())];
                        if (model.Annex.ToString() != "")
                            li_annex.Text = "<a href='" + model.Annex.ToString() + "'>" + model.Annex.ToString() + "</a>";
                        hi_procid.Value = "0|" + DateTime.Now.ToString("yyyy-MM-dd") + "|0";
                        if (model.State.ToString() == "1" || model.State.ToString() == "0")
                        {
                            tr1.Visible = false;
                            tr2.Visible = false;
                        }
                        else
                        {
                            WX.PRO.State.MODEL statemodel = WX.PRO.State.GetModel("select * from PRO_State where ProjID=" + model.ID.ToString());
                            if (statemodel != null)
                            {
                                if (statemodel.ProcID.ToString() != "" && statemodel.ProcID.ToString() != "0")
                                {
                                    // WX.PRO.Process.MODEL procmodel = WX.PRO.Process.GetModel("select top 1 * from PRO_Process where ProjID=" + statemodel.ProjID.ToString() + " and NO=" + statemodel.ProcID.ToString());
                                    hivalue = statemodel.ProcID.ToString();
                                }
                                li_projmanage.Text = WX.CommonUtils.GetRealNameListByUserIdList(statemodel.Manage.ToString());
                                li_checkfee.Text = statemodel.Fee.ToString();
                                li_checkmanage.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Manage.ToString());
                                li_checkopinion.Text = model.Opinion.ToString();
                                li_checktime.Text = Convert.ToDateTime(model.Stime.value).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    }
                    ReBind();
            }
        }
        private void ReBind()
        {
            string sql = "SELECT * FROM PRO_Process where ProjID=" + WX.Request.rProjectId + " order by NO asc";
            var supplierData = ULCode.QDA.XSql.GetDataTable(sql);
            rowcount = supplierData.Rows.Count;
            this.SupplierRepeater.DataSource = supplierData;
            this.SupplierRepeater.DataBind();
        }
        public string getclass(object id)
        {
            if (hivalue == id.ToString())
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
        public string getbuttonname(object no)
        {
            if (no.ToString() == rowcount.ToString())
                return "结束";
            else
                return "转到下一步";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.PRO.Process.MODEL procmodel = WX.PRO.Process.GetModel("select top 1 * from PRO_Process where ProjID=" + WX.Request.rProjectId + " and NO=" + Convert.ToInt32(((Button)sender).ToolTip));
            WX.PRO.Process.MODEL procmodel2 = WX.PRO.Process.GetModel("select top 1 * from PRO_Process where ProjID=" + WX.Request.rProjectId + " and NO=" + (Convert.ToInt32(procmodel.NO.ToString()) + 1));
            if (procmodel2 != null)
            {
                ULCode.QDA.XSql.Execute("update PRO_State set ProcID=" + procmodel2.NO.ToString() + ",Percnt=Percnt+" + procmodel.Percnt.ToString() + ",Percnttime=Percnttime+" + procmodel.Percnttime.ToString() + " where ProjID=" + procmodel.ProjID.ToString());
                WX.PRO.Log.AddLog(6, Convert.ToInt32(procmodel.ProjID.ToString()), "从 第" + procmodel.NO.ToString() + "步 转至 第" + procmodel2.NO.ToString() + "步", Request.UserHostAddress);
                WX.PRO.Process.SetTime(DateTime.Now, Convert.ToInt32(procmodel.ProjID.ToString()), Convert.ToInt32(procmodel2.NO.ToString()));
            }
            else
            {
                procmodel.Stoptime.value = DateTime.Now;
                procmodel.Update();
                ULCode.QDA.XSql.Execute("update PRO_State set ProcID=0,Stoptime=getdate(),State=2,Percnt=Percnt+" + procmodel.Percnt.ToString() + ",Percnttime=Percnttime+" + procmodel.Percnttime.ToString() + " where ProjID=" + procmodel.ProjID.ToString());
                ULCode.QDA.XSql.Execute("update PRO_Projects set State=5 where ID=" + procmodel.ProjID.ToString());
                WX.PRO.Log.AddLog(7, Convert.ToInt32(procmodel.ProjID.ToString()), "第" + procmodel.NO.ToString() + "步结束", Request.UserHostAddress);
                WX.PRO.Log.AddLog(8, Convert.ToInt32(procmodel.ProjID.ToString()), "项目流程结束", Request.UserHostAddress);
            }
            Response.Redirect("Proj_ProjectDetail.aspx?ProjectId=" + WX.Request.rProjectId);
        }
    }
}