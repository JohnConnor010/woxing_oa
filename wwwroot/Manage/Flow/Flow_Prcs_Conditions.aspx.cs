using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_Conditions : System.Web.UI.Page
    {
        public string prcsinstr = "";
        public string prcsoutstr = "";
        WX.Flow.Model.Form.MODEL fm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                WX.Flow.Model.Flow.MODEL flow = WX.Flow.Model.Flow.GetCache(Convert.ToInt32(Request["flowId"]));//WX.Flow.Model.Flow.GetModel("select * from FL_Flows where ID=" + Request["flowId"]);
                if (flow != null)
                {
                    flow.LoadForm(false);
                    fm = flow.Form;// WX.Flow.Model.Form.GetModel("select * from FL_Forms where ID=" + flow.FormId.value);
                    ITEM_VALUE2.Items.Add(new ListItem("常量",""));
                    if (fm.Items_FormFieldCollection != null)
                    {
                        foreach (WX.Flow.FormField ff in fm.Items_FormFieldCollection)
                        {
                            ListItem li = new ListItem();
                            li.Text = ff.Text;
                            li.Value = "@" + ff.Text;
                            ITEM_NAME.Items.Add(li);
                            ITEM_VALUE2.Items.Add(li);
                        }
                    }
                    DataTable dt = ULCode.QDA.XSql.GetDataTable("SELECT [Name],[Title],[Value],[Type] FROM [TE_VarDefine]");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListItem li = new ListItem();
                        li.Text = "[" + dt.Rows[i]["title"].ToString() + "]";
                        li.Value = "[" + dt.Rows[i]["title"].ToString() + "]";
                        ITEM_VALUE2.Items.Add(li);
                        li.Value = "[" + dt.Rows[i]["title"].ToString() + "]";
                        ITEM_NAME.Items.Add(li);
                    }
                    WX.Flow.Model.Process.MODEL prcs = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"]));//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
                    if (prcs != null)
                    {
                        if (prcs.Condition_In.value != null && prcs.Condition_In.value.ToString() != "")
                        {

                            string[] inarray = prcs.Condition_In.value.ToString().Split('|');
                            string[] prcsinlist = inarray[0].Split(new string[] { "\n" }, StringSplitOptions.None);
                            for (int i = 0; i < prcsinlist.Length; i++)
                            {
                                if (prcsinlist[i].Trim() != "")
                                {
                                    prcsinstr += String.Format("<tr class=\"TableLine1\">\n" +
                                            "            <td align=\"center\">[{0}]</td>" +
                                            "            <td>{1}</td>" +
                                            "            <td align=\"center\">" +
                                            "              <image style=\"cursor:pointer\" src=\"/images/edit.gif\" align=\"absmiddle\" onclick=\"upedit(this,1);\">" +
                                            "              <image style=\"cursor:pointer\" src=\"/images/delete.gif\" align=\"absmiddle\" onclick=\"delRule(this,1)\">" +
                                            "            </td>" +
                                            "           </tr>     ", (i + 1).ToString(), prcsinlist[i].Replace("`", "'"));
                                }
                            }
                            PRCS_IN_SET.Value = inarray[1];
                            PRCS_IN_DESC.Value = inarray[2];
                        }
                        if (prcs.Condition_Out.value != null && prcs.Condition_Out.value.ToString() != "")
                        {
                            string[] outarray = prcs.Condition_Out.value.ToString().Split('|');
                            string[] prcsoutlist = outarray[0].Split(new string[] { "\n" }, StringSplitOptions.None);
                            for (int i = 0; i < prcsoutlist.Length; i++)
                            {
                                if (prcsoutlist[i].Trim() != "")
                                {
                                    prcsoutstr += String.Format("<tr class=\"TableLine1\">\n" +
                                            "            <td align=\"center\">[{0}]</td>" +
                                            "            <td>{1}</td>" +
                                            "            <td align=\"center\">" +
                                            "              <image style=\"cursor:pointer\" src=\"/images/edit.gif\" align=\"absmiddle\" onclick=\"upedit(this,0)\">" +
                                            "              <image style=\"cursor:pointer\" src=\"/images/delete.gif\" align=\"absmiddle\" onclick=\"delRule(this,0)\">" +
                                            "            </td>" +
                                            "           </tr>     ", (i + 1).ToString(), prcsoutlist[i].Replace( "`","'"));
                                }
                            }
                            PRCS_OUT_SET.Value = outarray[1];
                            PRCS_OUT_DESC.Value = outarray[2];
                        }
                    }
                }
                /*
                WX.Flow.Model.Process.MODEL prcsmodel = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"])) ;//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
                if (prcsmodel.Condition_In.value != null)
                {
                    Response.Write("转入：" + checkin(prcsmodel.Condition_In.value.ToString().Replace("`", "'")) + "<br/>");
                }
                if (prcsmodel.Condition_Out.value != null)
                {
                    Response.Write("转出：" + checkin(prcsmodel.Condition_Out.value.ToString().Replace("`", "'")));
                }*/
            }

           
            //if (prcsmodel != null)
            //{
            //    object obj = ULCode.QDA.XSql.GetValue("select top 1 ID from FL_Process where " + checkin(prcsmodel.Condition_In.value.ToString().Replace("`", "'")));
            //    if (obj != null)
            //    {
            //        Response.Write("正在进入流程。。。。。");
            //    }
            //    else
            //    {
            //        Response.Write(prcsmodel.Condition_In.value.ToString().Split('|')[2]);
            //    }
            //    object objout = ULCode.QDA.XSql.GetValue("select top 1 ID from FL_Process where " + checkin(prcsmodel.Condition_Out.value.ToString().Replace("`", "'")));
            //    if (objout != null)
            //    {
            //        Response.Write("正在转出流程。。。。。");
            //    }
            //    else
            //    {
            //        Response.Write(prcsmodel.Condition_In.value.ToString().Split('|')[2]);
            //    }
            //}
        }
        private string checkin(string instr)
        {
            string prcsin = instr;
            string[] prcslist = prcsin.Split('|');
            string[] wherelist = prcslist[0].Split(new string[] { "\n" }, StringSplitOptions.None);
            wherelist = Replacevalue(wherelist);
            string sqlstr = prcslist[1];
            for (int i = 0; i < wherelist.Length; i++)
            {
                sqlstr = sqlstr.Replace("[" + (i + 1) + "]", wherelist[i]);
            }
            return sqlstr;
        }
        private string[] Replacevalue(string[] wherelist)
        {
            WX.Flow.Model.Form.MODEL form = fm;
            WX.Flow.FormField ff = null;
            for (int i = 0; i < wherelist.Length; i++)
            {
                if (wherelist[i] != "")
                {
                    string[] tjlist = wherelist[i].Split('\'');
                    if (tjlist[1].Substring(0,1)=="@")
                    {
                        tjlist[1] = tjlist[1].Substring(1);
                        ff = form.Items_FormFieldCollection.FindItemByTitle(tjlist[1]);
                        if (ff != null)
                        {
                            tjlist[1] = ff.Value;
                        }
                    }
                    else if (tjlist[1].Substring(0, 1) == "[")
                    {
                        tjlist[1] = tjlist[1].Substring(1, tjlist[1].Length - 2);
                        tjlist[1] = GetSysVariable(tjlist[1]);
                    }
                    if (tjlist[3].Substring(0, 1) == "@")
                    {
                        tjlist[3] = tjlist[3].Substring(1);
                        ff = form.Items_FormFieldCollection.FindItemByTitle(tjlist[3]);
                        if (ff != null)
                        {
                            tjlist[3] = ff.Value;
                        }
                    }
                    else if (tjlist[3].Substring(0, 1) == "[")
                    {
                        tjlist[3] = tjlist[3].Substring(1, tjlist[3].Length - 2);
                        tjlist[3] = GetSysVariable(tjlist[3]);
                    }
                    wherelist[i] = "'" + tjlist[1] + "'" + tjlist[2] + "'" + tjlist[3] + "'";
                }
            }
            return wherelist;
        }
        private string GetSysVariable(string Vname)
        {
            string returnstr = "";
            WX.WXUser cu = WX.Main.CurUser;
            WX.Flow.Model.Process.MODEL pmodel = WX.Flow.Model.Process.GetModel("select top 1 * from TE_VarDefine where Title='" + Vname + "'");
            string name = "";
            if (pmodel != null && pmodel.Name.value != null)
            {
                name = pmodel.Name.value.ToString();
            }
            switch (name)
            {
                case "PRCS_Z_UserName":
                    cu.LoadUserModel(false);
                    returnstr = cu.UserModel.RealName.value.ToString();
                    break;
                case "PRCS_Z_UserDuty":
                    cu.LoadDutyUser();
                    returnstr = cu.DutyUser.Name.value.ToString();
                    break;
                case "PRCS_Z_UserDept":
                    cu.LoadUserModel(false);
                    WX.Model.Department.MODEL dept = WX.Model.Department.GetCache(cu.UserModel.DepartmentID.ToInt32());
                         //WX.Model.Department.GetModel("select * from TE_Departments where ID=" + cu.EmployeeUser.DepartmentID.value.ToString());
                    returnstr = dept.Name.value.ToString();
                    break;
                case "PRCS_Z_UserSupDept":
                    cu.LoadUserModel(false);
                    WX.Model.Department.MODEL dept2 = WX.Model.Department.GetCache(cu.UserModel.DepartmentID.ToInt32());
                        //WX.Model.Department.GetModel("select * from TE_Departments where ID=" + cu.EmployeeUser.DepartmentID.value.ToString());
                    dept2 = WX.Model.Department.GetCache(dept2.ParentID.ToInt32());
                        //WX.Model.Department.GetModel("select * from TE_Departments where ID=" + dept2.ParentID.value.ToString());
                    returnstr = dept2.Name.value.ToString();
                    break;
                case "PRCS_ID": returnstr = Request["id"]; break;
                case "Datetime_Now": returnstr = DateTime.Now.ToString("yyyy-MM-dd"); break;
                default: break;
            }
            return returnstr;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            //获取id
            //*******************************************************
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string prcsin = PRCS_IN.Value + "|" + PRCS_IN_SET.Value + "|" + PRCS_IN_DESC.Value;
            string prcsout = PRCS_OUT.Value + "|" + PRCS_OUT_SET.Value + "|" + PRCS_OUT_DESC.Value;
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。


            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            WX.Flow.Model.Process.MODEL prcs = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"])) ;//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
            if (prcs != null)
            {
                prcs.Condition_In.value = prcsin.Replace("'", "`");
                prcs.Condition_Out.value = prcsout.Replace("'", "`");

                // string sSql = String.Format("Update FL_Process set Condition_In='{0}',Condition_Out='{1}' where ID={2}", prcsin.Replace("'", "`"), prcsout.Replace("'", "`"), Request["id"]);
                if (prcs.Update() != 0)
                {
                    bDeal = true;
                }
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "流程条件设置成功！", "");
            }
            else
            {
                ULCode.Debug.Alert(this, "流程条件设置失败！");
            }
            Response.Redirect("Flow_Prcs_List.aspx?id=" + Request["flowId"]);
        }
    }
}