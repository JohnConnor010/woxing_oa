
namespace WX.Flow.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;
    using System.Text.RegularExpressions;
    using HtmlAgilityPack;

    public partial class Form
    {
        private static List<MODEL> _Caches = null;
        public static List<MODEL> Caches
        {
            get
            {
                if (_Caches == null)
                {
                    _Caches = GetModels("Select * from FL_Forms order by Id");
                }
                return _Caches;
            }
        }
        public static MODEL GetCache(int id)
        {
            return Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == id; });
        }
        public partial class MODEL
        {
            public int SaveIntoCaches()
            {
                if (Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == this.Id.ToInt32(); }) == null)
                {
                    Caches.Add(this);
                    return 1;
                }
                else
                    return 0;
            }
            public int RemoveFromCaches()
            {
                MODEL m = Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == this.Id.ToInt32(); });
                if (m != null)
                {
                    Caches.Remove(m);
                    return 1;
                }
                else
                    return 0;
            }
            //以下为模型开发部分
            public FormFieldCollection Items_FormFieldCollection
            {
                get { return new FormFieldCollection(this.Items.ToString(), FormFieldDataType.Item); }
                set { this.Items.set(value.GetSavedDatas(FormFieldDataType.Item)); }
            }
            public int Items_GetFormFieldCount
            {
                get { return new FormFieldCollection(this.Items.ToString(), FormFieldDataType.Item).GetFormFieldCount(); }
            }
            /// <summary>
            /// 从表单模板生成Htmls
            /// 1.主要在用户工作过程中展示表单而用
            /// </summary>
            /// <param name="Datas">存储的数据</param>
            /// <param name="EditableFlds">可编辑字段</param>
            /// <param name="HiddenFlds">不可见字段</param>
            /// <returns></returns>
            public String GenerateHtmls(String Datas, String editableFlds, String hiddenFlds)
            {
                return this.GenerateHtmls(new FormFieldCollection(Datas, FormFieldDataType.Data)
                , new FormFieldCollection(editableFlds, FormFieldDataType.Item)
                , new FormFieldCollection(hiddenFlds, FormFieldDataType.Item), "");
            }
            public String GenerateHtmls(FormFieldCollection datas, FormFieldCollection editableFlds, FormFieldCollection hiddenFlds, string BeginuserID)
            {
                String htmls = null;
                String module = this.Module.ToString();
                //需要开发
                htmls = module;
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(module);
                htmls = this.Module_Short.ToString();
                HtmlNode rootNode = document.DocumentNode;
                string newnode = "";
                HtmlNodeCollection hnc = rootNode.SelectNodes("//input");//单行文本框
                if (hnc != null)
                {
                    foreach (HtmlNode hn in hnc)
                    {
                        newnode = "";
                        if (hn.OuterHtml.Trim() != "")
                        {
                            if (editableFlds.Contains(hn.Attributes["name"].Value))
                            {
                                hn.Attributes.Add("readonly", "readonly");
                            }
                            if (hiddenFlds.Contains(hn.Attributes["name"].Value))
                            {
                                if (hn.Attributes["style"] != null)
                                {
                                    hn.Attributes["style"].Value = hn.Attributes["style"].Value + ";display:none;";
                                }
                                else
                                {
                                    hn.Attributes.Add("style", "display:none;");
                                }
                            }
                            if (hn.Attributes["value"] != null)
                            {
                                hn.Attributes["value"].Value = datas[hn.Attributes["name"].Value].Value;
                            }
                            else
                            {
                                hn.Attributes.Add("value", datas[hn.Attributes["name"].Value].Value);
                            }
                            if (hn.Attributes["class"] != null && hn.Attributes["class"].Value == "AUTO")
                            {
                                switch (hn.Attributes["datafld"].Value)
                                {
                                    case "SYS_DATE": hn.Attributes["value"].Value = DateTime.Now.ToString("yyyy-MM-dd"); break;
                                    case "SYS_DATE_CN": hn.Attributes["value"].Value = DateTime.Now.ToString("yyyy年M月d日"); break;
                                    case "SYS_DATE_CN_SHORT3": hn.Attributes["value"].Value = DateTime.Now.ToString("yyyy年"); break;
                                    case "SYS_DATE_CN_SHORT4": hn.Attributes["value"].Value = DateTime.Now.ToString("yyyy"); break;
                                    case "SYS_DATE_CN_SHORT1": hn.Attributes["value"].Value = DateTime.Now.ToString("yyyy年M月"); break;
                                    case "SYS_DATE_CN_SHORT2": hn.Attributes["value"].Value = DateTime.Now.ToString("M月d日"); break;
                                    case "SYS_TIME": hn.Attributes["value"].Value = DateTime.Now.ToString("H:m:s"); break;
                                    case "SYS_DATETIM": hn.Attributes["value"].Value = DateTime.Now.ToString("yyyy-MM-dd H:m:s"); break;
                                    case "SYS_WEEK": hn.Attributes["value"].Value = DateTime.Now.DayOfWeek.ToString(); break;
                                    case "SYS_USERID": hn.Attributes["value"].Value = WX.Authentication.GetUserID(); break;
                                    case "SYS_USERNAME":
                                        {
                                            if (BeginuserID == "")
                                            {
                                                WX.Public.CurUser.LoadUserModel(false);
                                                hn.Attributes["value"].Value = WX.Public.CurUser.UserModel.RealName.ToString();
                                            }
                                            else
                                            {
                                                hn.Attributes["value"].Value = WX.WXUser.GetRealNameByUserID(BeginuserID);
                                            }
                                        } break;
                                    case "SYS_DEPTNAME":
                                        {
                                            if (BeginuserID == "")
                                            {
                                                WX.Public.CurUser.LoadEmployeeUser();
                                                WX.Public.CurUser.LoadMyDepartment();
                                                hn.Attributes["value"].Value = WX.Public.CurUser.MyDepartMent.Name.ToString();
                                            }
                                            else
                                            {
                                                hn.Attributes["value"].Value = WX.WXUser.GetDeptNameByUserID(BeginuserID);
                                            }
                                        }
                                        break;
                                    case "SYS_USERPRIV":
                                        {
                                            WX.Public.CurUser.LoadEmployeeUser();
                                            WX.Public.CurUser.LoadDutyUser();
                                            hn.Attributes["value"].Value = WX.Public.CurUser.DutyUser.Name.ToString();
                                        } break;
                                    case "SYS_USERPRIVOTHER":
                                        {
                                            WX.Public.CurUser.LoadEmployeeUser();
                                            WX.Public.CurUser.LoadDutyUser();
                                            hn.Attributes["value"].Value = WX.Public.CurUser.DutyUser.Name.ToString();
                                        } break;
                                    case "SYS_USERNAME_DATE":
                                        {
                                            WX.Public.CurUser.LoadUserModel(false);
                                            hn.Attributes["value"].Value = WX.Public.CurUser.UserModel.RealName.ToString() + " " + DateTime.Now.ToString("yyyy-MM-dd");
                                        } break;
                                    case "SYS_USERNAME_DATETIME":
                                        {
                                            WX.Public.CurUser.LoadUserModel(false);
                                            hn.Attributes["value"].Value = WX.Public.CurUser.UserModel.RealName.ToString() + " " + DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                                        } break;
                                    case "SYS_FORMNAME":
                                        {
                                            hn.Attributes["value"].Value = "表单名称";
                                        } break;
                                    case "SYS_RUNDATE": hn.Attributes["value"].Value = ""; break;
                                    case "SYS_RUNDATETIME": hn.Attributes["value"].Value = ""; break;
                                    case "SYS_RUNID": hn.Attributes["value"].Value = ""; break;
                                    case "SYS_AUTONUM": hn.Attributes["value"].Value = ""; break;
                                    case "SYS_IP": hn.Attributes["value"].Value = "-SYS_IP-"; break;//getIp();
                                    case "SYS_MANAGER1":
                                        {
                                            WX.Public.CurUser.LoadUserModel(false);
                                            WX.Model.User.MODEL zgmodel = WX.Model.User.Caches.Find(delegate(WX.Model.User.MODEL dele) { return dele.DepartmentID.ToInt32() == WX.Public.CurUser.UserModel.DepartmentID.ToInt32() && dele.DutyId.ToInt32() == 500; });
                                            //WX.Model.Employee.GetModel("select top 1 * from [TU_Employees] where DepartmentID='" + WX.Main.CurUser.EmployeeUser.DepartmentID.ToString() + "' and DutyID=500");
                                            hn.Attributes["value"].Value = (zgmodel != null ? zgmodel.RealName.value.ToString() : "");
                                        } break;
                                    case "SYS_MANAGER2":
                                        {
                                            WX.Public.CurUser.LoadUserModel(false);
                                            WX.Public.CurUser.LoadMyDepartment();
                                            //WX.Model.Department.MODEL model = WX.Model.Department.GetModel("select * from TE_Departments where ParentID='" + usermodel.EmployeeUser.DepartmentID.value.ToString() + "'");
                                            //WX.Model.Employee.MODEL zgmodel = WX.Model.Employee.GetModel("select top 1 * from [TU_Employees] where DepartmentID='" + WX.Main.CurUser.EmployeeUser.DepartmentID.ToString() + "' and DutyID=500");
                                            WX.Model.User.MODEL zgmodel = WX.Model.User.Caches.Find(delegate(WX.Model.User.MODEL dele) { return dele.DepartmentID.ToInt32() == WX.Public.CurUser.UserModel.DepartmentID.ToInt32() && dele.DutyId.ToInt32() == 500; });
                                            hn.Attributes["value"].Value = (zgmodel != null ? zgmodel.RealName.value.ToString() : "");
                                        } break;
                                    case "SYS_SQL": { hn.Attributes["value"].Value = ULCode.QDA.XSql.GetValue(hn.Attributes["datasrc"].Value.ToString()).ToString(); } break;
                                    default: break;
                                }
                                if (hn.Attributes["hidden"] != null)
                                {
                                    hn.Attributes["type"].Value = "hidden";
                                    hn.Attributes["hidden"].Remove();
                                }
                                if (datas[hn.Attributes["name"].Value].Value != null)
                                {
                                    hn.Attributes["value"].Value = datas[hn.Attributes["name"].Value].Value;
                                }
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                            }
                            else if (hn.Attributes["class"] != null && hn.Attributes["class"].Value == "CALC")
                            {
                                newnode = "<script>function calc_" + hn.Attributes["name"].Value.Split('_')[1] + "(){var myvalue=eval(\"" + (hn.Attributes["value"].Value.IndexOf('(') > -1 ? "calc_" : "") + hn.Attributes["value"].Value.ToLower() + "\");if(myvalue==Infinity) document.form1." + hn.Attributes["name"].Value + ".value=\"无效结果\";else if(!isNaN(myvalue)) {var prec=document.form1." + hn.Attributes["name"].Value + ".getAttribute('prec');var vPrec;if(!prec) vPrec=10000;else vPrec=Math.pow(10,prec);var result = new Number(parseFloat(Math.round(myvalue*vPrec)/vPrec));document.form1." + hn.Attributes["name"].Value + ".value=result.toFixed(prec);}else document.form1." + hn.Attributes["name"].Value + ".value=myvalue;setTimeout(calc_" + hn.Attributes["name"].Value.Split('_')[1] + ",1000);}setTimeout(calc_" + hn.Attributes["name"].Value.Split('_')[1] + ",3000);</script>";
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml + newnode);
                            }
                            else if (hn.Attributes["type"].Value == "text")
                            {
                                if (hn.Attributes["hidden"] != null)
                                {
                                    hn.Attributes["type"].Value = "hidden";
                                    hn.Attributes["hidden"].Remove();
                                }
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                            }
                            else if (hn.Attributes["type"].Value == "checkbox")
                            {
                                hn.Attributes["value"].Value = hn.Attributes["name"].Value;
                                if (datas[hn.Attributes["name"].Value].Value == hn.Attributes["name"].Value)
                                {
                                    if (hn.Attributes["checked"] != null)
                                    { hn.Attributes["checked"].Value = "checked"; }
                                    else
                                    {
                                        hn.Attributes.Add("checked", "checked");
                                    }
                                }
                                else if (datas[hn.Attributes["name"].Value].Value != null)
                                {
                                    try
                                    {
                                        hn.Attributes.Remove("checked");
                                    }
                                    catch { }
                                }
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                            }
                            else
                            {
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                            }
                        }
                    }
                }
                hnc = rootNode.SelectNodes("//select");//下拉
                if (hnc != null)
                {
                    string child = "";
                    foreach (HtmlNode hn in hnc)
                    {
                        if (editableFlds.Contains(hn.Attributes["name"].Value))
                        {
                            hn.Attributes.Add("readonly", "readonly");
                        }
                        if (hiddenFlds.Contains(hn.Attributes["name"].Value))
                        {
                            if (hn.Attributes["style"] != null)
                            {
                                hn.Attributes["style"].Value = hn.Attributes["style"].Value + ";display:none;";
                            }
                            else
                            {
                                hn.Attributes.Add("style", "display:none;");
                            }
                        }
                        newnode = "";
                        if (hn.Attributes["class"] != null && hn.Attributes["class"].Value == "AUTO")
                        {
                            switch (hn.Attributes["datafld"].Value)
                            {
                                case "SYS_LIST_DEPT":
                                    {
                                        newnode = "";
                                        string sSql = "exec [dbo].[sp_get_tree_multi_table] 'TE_Departments','ID','Name','ParentID','Sort',0,0,5";
                                        DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);

                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            newnode += "<option value=\"" + dr["id"].ToString() + "\" " + (datas[hn.Attributes["name"].Value].Value == dr["id"].ToString() ? " selected=\"selected\"" : "") + ">" + dr["node_name"].ToString() + "</option>";
                                        }
                                        if (hn.Attributes["child"] != null)
                                        {
                                            child = hn.Attributes["child"].Value + "|DepartmentID";
                                        }
                                        hn.InnerHtml = newnode;
                                    } break;
                                case "SYS_LIST_USER":
                                    {
                                        newnode = "";
                                        DataTable dt = ULCode.QDA.XSql.GetDataTable("SELECT * FROM vw_employees");
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            newnode += "<option value=\"" + (child != "" && child.Split('|')[0] == hn.Attributes["id"].Value ? dr["RealName"].ToString() + "|" + dr[child.Split('|')[1]] + "|" : "") + dr["UserID"].ToString() + "\" " + (datas[hn.Attributes["name"].Value].Value == (child != "" && child.Split('|')[0] == hn.Attributes["id"].Value ? dr["RealName"].ToString() + "|" + dr[child.Split('|')[1]] + "|" : "") + dr["UserID"].ToString() ? " selected=\"selected\"" : "") + ">" + dr["RealName"].ToString() + "</option>";
                                        }
                                        hn.InnerHtml = newnode;
                                    } break;
                                case "SYS_LIST_PRIV":
                                    {
                                        newnode = "";
                                        DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from TE_Duties");
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            newnode += "<option value=\"" + dr["ID"].ToString() + "\" " + (datas[hn.Attributes["name"].Value].Value == dr["ID"].ToString() ? " selected=\"selected\"" : "") + ">" + dr["Name"].ToString() + "</option>";
                                        }
                                        if (hn.Attributes["child"] != null)
                                        {
                                            child = hn.Attributes["child"].Value + "|DutyID";
                                        }
                                        hn.InnerHtml = newnode;
                                    } break;
                                case "SYS_LIST_SQL":
                                    {
                                        newnode = "";
                                        DataTable dt = ULCode.QDA.XSql.GetDataTable(hn.Attributes["datasrc"].Value.ToString());
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            newnode += "<option value=\"" + dr["ID"].ToString() + "\" " + (datas[hn.Attributes["name"].Value].Value == dr["ID"].ToString() ? " selected=\"selected\"" : "") + ">" + (dr["Name"] == null || dr["Name"].ToString() == "" ? dr["ID"].ToString() : dr["Name"].ToString()) + "</option>";
                                        }
                                        hn.InnerHtml = newnode;
                                    } break;
                                default: break;
                            }
                            htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                        }
                        else
                        {
                            if (datas[hn.Attributes["name"].Value].Value != null && datas[hn.Attributes["name"].Value].Value != "")
                            {
                                hn.InnerHtml = hn.InnerHtml.Replace(" selected=\"selected\"", "").Replace(datas[hn.Attributes["name"].Value].Value + "\">", datas[hn.Attributes["name"].Value].Value + "\" selected=\"selected\">");
                            }
                            if (hn.OuterHtml.Trim() != "")
                            {
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                            }
                        }
                    }
                }
                hnc = rootNode.SelectNodes("//textarea");//单行文本框
                if (hnc != null)
                {
                    foreach (HtmlNode hn in hnc)
                    {
                        if (editableFlds.Contains(hn.Attributes["name"].Value))
                        {
                            hn.Attributes.Add("readonly", "readonly");
                        }
                        if (hiddenFlds.Contains(hn.Attributes["name"].Value))
                        {
                            if (hn.Attributes["style"] != null)
                            {
                                hn.Attributes["style"].Value = hn.Attributes["style"].Value + ";display:none;";
                            }
                            else
                            {
                                hn.Attributes.Add("style", "display:none;");
                            }
                        }
                        if (datas[hn.Attributes["name"].Value].Value != null)
                        {
                            hn.InnerHtml = datas[hn.Attributes["name"].Value].Value; ;
                        }
                        newnode = "";
                        if (hn.OuterHtml.Trim() != "")
                        {
                            if (hn.Attributes["rich"] != null && hn.Attributes["rich"].Value == "1")
                            {
                                newnode = "<div><input type=\"hidden\" id=\"" + hn.Attributes["name"].Value + "\" name=\"" + hn.Attributes["name"].Value + "\" value=\"" + hn.InnerHtml + "\" /><input type=\"hidden\" id=\"" + hn.Attributes["name"].Value + "___Config\" value=\"HtmlEncodeOutput=true\" /><iframe " + (hn.Attributes["style"] != null ? " style=\"" + hn.Attributes["style"].Value + "\"" : "") + (hn.Attributes["readonly"] != null ? " readonly=\"" + hn.Attributes["readonly"].Value + "\"" : "") + " id=\"" + hn.Attributes["name"].Value + "___Frame\" src=\"/fckeditor/editor/fckeditor.html?InstanceName=" + hn.Attributes["name"].Value + "&amp;Toolbar=\"Defaul\" width=\"" + (hn.Attributes["rich_width"] != null ? hn.Attributes["rich_width"].Value : "100%") + "\" height=\"" + (hn.Attributes["rich_width"] != null ? hn.Attributes["rich_height"].Value : "300px") + "\" frameborder=\"no\" scrolling=\"no\"></iframe></div>";
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", newnode);
                            }
                            else
                            {
                                htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", hn.OuterHtml);
                            }
                        }
                    }
                }
                hnc = rootNode.SelectNodes("//img");//单选按钮    
                if (hnc != null)
                {
                    foreach (HtmlNode hn in hnc)
                    {
                        newnode = "";
                        if (hn.OuterHtml.Trim() != "")
                        {
                            string oldnode = hn.OuterHtml;
                            switch (hn.Attributes["class"].Value)
                            {
                                case "RADIO":
                                    {
                                        string[] items = hn.Attributes["radio_field"].Value.Split('`');
                                        string checkstr = "";
                                        for (int j = 0; j < items.Length; j++)
                                        {
                                            checkstr = "";
                                            if (items[j] != "")
                                            {
                                                if (datas[hn.Attributes["name"].Value].Value == "" && hn.Attributes["radio_check"] != null && hn.Attributes["radio_check"].Value == items[j])
                                                {
                                                    checkstr = "checked";
                                                }
                                                else
                                                    if (datas[hn.Attributes["name"].Value].Value == items[j])
                                                    {
                                                        checkstr = "checked";
                                                    }
                                                newnode += "<input name=\"" + hn.Attributes["name"].Value + "\" type=\"radio\" title=\"" + hn.Attributes["title"].Value + "\" " + checkstr + " " + (editableFlds.Contains(hn.Attributes["name"].Value) ? "readonly=\"readonly\"" : "") + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? " style='display:none;'" : "") + " value=\"" + items[j] + "\" />" + items[j] + "&nbsp;&nbsp;";
                                            }
                                        }
                                        htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", newnode);

                                    } break;
                                case "DATE":
                                    {
                                        newnode = "<input name=\"" + hn.Attributes["name"].Value + "\" class=\"" + (hn.Attributes["date_format"].Value == "yyyy-MM-dd" ? "easyui-datebox" : "easyui-datetimebox") + "\" value=\"" + (datas[hn.Attributes["name"].Value].Value == "" ? DateTime.Now.ToString(hn.Attributes["date_format"].Value) : datas[hn.Attributes["name"].Value].Value) + "\" type=\"text\"  style=\"" + (hn.Attributes["tsize"] != null ? "width:" + hn.Attributes["tsize"].Value.ToString() + "px;" : "") + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? "display:none;" : "") + "\" " + (editableFlds.Contains(hn.Attributes["name"].Value) ? " readonly=\"readonly\"" : "") + " title=\"" + hn.Attributes["title"].Value + "\" />";
                                        htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", newnode);

                                    } break;
                                case "LIST_VIEW":
                                    {
                                        string[] titles = hn.Attributes["lv_title"].Value.Split('`');
                                        newnode = "<TABLE  id='LV_" + hn.Attributes["name"].Value.Split('_')[1] + "' lv_sum=\"" + hn.Attributes["lv_sum"].Value + "\" lv_cal=\"" + hn.Attributes["lv_cal"].Value + "\" lv_coltype=\"" + hn.Attributes["lv_coltype"].Value + "\" lv_colvalue=\"" + hn.Attributes["lv_colvalue"].Value + "\" data_table=\"\" data_field=\"\" data_query=\"\" data_fld_name=\"\" class='LIST_VIEW' style='border-collapse:collapse;" + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? "display:none;" : "") + "' border=1 cellspacing=0 cellpadding=2 FormData='" + hn.Attributes["lv_size"].Value + "'><TR style='font-weight:bold;font-size:14px;' class='LIST_VIEW_HEADER'>\n";
                                        newnode += "<TD nowrap>序号</TD>\n";
                                        for (int i = 0; i < titles.Length - 1; i++)
                                        {
                                            newnode += "<TD nowrap>" + titles[i] + "</TD>\n";
                                        }
                                        newnode += "<TD>操作</TD></TR></TABLE>\n";
                                        newnode += "<input type=button value=新增 " + (editableFlds.Contains(hn.Attributes["name"].Value) ? " readonly=\"readonly\"" : "") + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? "style=\"display:none;\"" : "") + " onclick=\"tb_addnew('LV_" + hn.Attributes["name"].Value.Split('_')[1] + "','0','" + hn.Attributes["lv_colvalue"].Value + "','1')\">\n";
                                        newnode += "<input type=button value=计算 " + (editableFlds.Contains(hn.Attributes["name"].Value) ? " readonly=\"readonly\"" : "") + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? "style=\"display:none;\"" : "") + " onclick=\"tb_cal('LV_" + hn.Attributes["name"].Value.Split('_')[1] + "','" + hn.Attributes["lv_cal"].Value + "')\">\n";
                                        newnode += "<input type=hidden class=\"LIST_VIEW\" name=" + hn.Attributes["name"].Value + ">\n";
                                        newnode += "<br />\n";
                                        newnode += "<br />\n";
                                        htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", newnode);
                                    }
                                    break;
                                case "SIGN":
                                    {
                                        string[] types = hn.Attributes["sign_type"].Value.Split(',');
                                        if (types[0] == "1")
                                        {
                                            newnode = "<input id=\"Button1\" type=\"button\" value=\" 盖 章 \" onclick=\"addseal()\" " + (editableFlds.Contains(hn.Attributes["name"].Value) ? "readonly=\"readonly\"" : "") + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? " style='display:none;'" : "") + " />";
                                        }
                                        if (types[1] == "1")
                                        {
                                            newnode += "<input id=\"Button2\" type=\"button\" value=\" 手 写 \" onclick=\"handwrite()\" " + (editableFlds.Contains(hn.Attributes["name"].Value) ? "readonly=\"readonly\"" : "") + (hiddenFlds.Contains(hn.Attributes["name"].Value) ? " style='display:none;'" : "") + " />";
                                        }
                                        if (newnode != "")
                                        {
                                            newnode += "<input type=\"hidden\" name=\"" + hn.Attributes["name"].Value + "\" value=\"" + datas[hn.Attributes["name"].Value].Value + "\" id=\"txtSealData\" />";
                                        }
                                        htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", newnode);
                                    }
                                    break;
                                case "USER":
                                    {
                                        newnode = "<input type=\"hidden\" name=\"" + hn.Attributes["name"].Value + "\" value=\"" + datas[hn.Attributes["name"].Value].Value + "\" id=\"" + hn.Attributes["name"].Value + "\" />";
                                        string txtvalue = "";
                                        try
                                        {
                                            if (datas[hn.Attributes["name"].Value].Value.Trim() != "")
                                            {
                                                DataTable dduname = ULCode.QDA.XSql.GetDataTable("select RealName from TU_Users where UserID='" + datas[hn.Attributes["name"].Value].Value + "'");
                                                if (dduname.Rows.Count > 0)
                                                    txtvalue = dduname.Rows[0][0].ToString();
                                            }
                                        }
                                        catch { }
                                        newnode += "<input type=\"text\" name=\"txt" + hn.Attributes["name"].Value + "\" value=\"" + txtvalue + "\" size=\"12\" readonly=\"readonly\" id=\"txt" + hn.Attributes["name"].Value + "\"/>";
                                        newnode += "&nbsp; ╋<a href=\"javascript:void(0)\" onclick=\"PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','" + hn.Attributes["name"].Value + "','txt" + hn.Attributes["name"].Value + "',468,395);\">选择</a>";
                                        htmls = htmls.Replace("{" + hn.Attributes["name"].Value + "}", newnode);
                                    } break;
                                default: break;
                            }

                        }
                    }
                }
                return htmls;
                //return htmls;
            }
            /// <summary>
            /// 从表单模板生成精简模板
            /// 1.保存表单模板数据前调用!
            /// </summary>
            /// <returns></returns>
            public String GetShortModule()
            {
                string resolvestr = this.Module.value.ToString();
                string[] regstr = new string[] {
                "<input(.*?)name=\"(.*?)\"(.*?)/>",//input表单
                "<textarea(.*?)name=\"(.*?)\"(.*?)>([\\s\\S]*?)</textarea>",//多行文本框
                "<select(.*?)name=\"(.*?)\"([\\s\\S]*?)</select>",//下拉列表
                "<img(.*?)name=\"(.*?)\"(.*?)/>",//图片
                "<button(.*?)name=\"(.*?)\"([\\s\\S]*?)</button>"//按钮
                };
                for (int i = 0; i < regstr.Length; i++)
                {
                    resolvestr = Regex.Replace(resolvestr, regstr[i], "{$2}", RegexOptions.Compiled);
                }
                return resolvestr;
            }
            /// <summary>
            /// 从表单模板更新精简模板
            /// </summary>
            public void UpdateShortModule()
            {
                this.Module_Short.set(this.GetShortModule());
            }
            /// <summary>
            /// 从表单模板生成表单字段
            /// 1.保存表单模板数据前调用!
            /// </summary>
            /// <returns></returns>
            public FormFieldCollection FetchItems()
            {
                //需要开发
                FormFieldCollection ffc = new FormFieldCollection();
                string resolvestr = this.Module.ToString();
                string[] regstr = new string[] {
                "<input(.*?)title=\"(.*?)\"(.*?)name=\"(.*?)\"(.*?)/>",//input表单
                "<input(.*?)name=\"(.*?)\"(.*?)title=\"(.*?)\"(.*?)/>",//input表单
                "<textarea(.*?)title=\"(.*?)\"(.*?)name=\"(.*?)\"(.*?)>([\\s\\S]*?)</textarea>",//多行文本框
                "<textarea(.*?)name=\"(.*?)\"(.*?)title=\"(.*?)\"(.*?)>([\\s\\S]*?)</textarea>",//多行文本框
                "<select(.*?)title=\"(.*?)\"(.*?)name=\"(.*?)\"([\\s\\S]*?)</select>",//下拉列表
                "<select(.*?)name=\"(.*?)\"(.*?)title=\"(.*?)\"([\\s\\S]*?)</select>",//下拉列表
                "<img(.*?)title=\"(.*?)\"(.*?)name=\"(.*?)\"(.*?)/>",//图片
                "<img(.*?)name=\"(.*?)\"(.*?)title=\"(.*?)\"(.*?)/>",//图片
                "<button(.*?)title=\"(.*?)\"(.*?)name=\"(.*?)\"([\\s\\S]*?)</button>",//按钮
                "<button(.*?)name=\"(.*?)\"(.*?)title=\"(.*?)\"([\\s\\S]*?)</button>"//按钮
                };
                for (int i = 0; i < regstr.Length; i++)
                {
                    MatchCollection mc = Regex.Matches(resolvestr, regstr[i]);

                    for (int j = 0; j < mc.Count; j++)
                    {
                        FormField ff = new FormField();
                        if (i % 2 == 0)
                        {
                            ff.Id = mc[j].Result("$4");
                            ff.Text = mc[j].Result("$2");
                        }
                        else
                        {
                            ff.Id = mc[j].Result("$2");
                            ff.Text = mc[j].Result("$4");
                        }
                        if (mc[j].Result("$3").IndexOf("datafld") > -1)
                        {
                            ff.Type = Regex.Match(mc[j].Result("$3"), "datafld=\"(.*?)\"").Result("$1");
                        }
                        ffc.Add(ff);
                    }
                }
                return ffc;
            }
            /// <summary>
            /// 从表单模板更新表单字段列表
            /// </summary>
            public void UpdateItems()
            {
                this.Items_FormFieldCollection = this.FetchItems();
            }
            /// <summary>
            /// 从表单项列表与Request.Forms生成字段数据
            /// 1.主要是用户在工作过程中流转使用
            /// </summary>
            /// <returns></returns>
            public FormFieldCollection GetPostedDatas()
            {
                FormFieldCollection ffc = this.Items_FormFieldCollection;
                foreach (FormField ff in ffc)
                {
                    ff.Value = HttpContext.Current.Request.Form[ff.Id] == null ? "" : HttpContext.Current.Request.Form[ff.Id];
                    //ff.Value = HttpContext.Current.Request.Form[ff.Id];
                }
                //需要开发
                return ffc;
            }
        }
    }
    public partial class Form : XDataEntity
    {
        public Form(string tableName)
            : base(tableName)
        {
        }
        public Form(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Form(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Form _entity;
        private static MODEL _model;

        public static MODEL NewDataModel()
        {
            return new MODEL(Entity);
        }
        public static MODEL NewDataModel(DataRow drCache)
        {
            return new MODEL(Entity, drCache);
        }
        public static MODEL NewDataModel(params object[] keyValues)
        {
            return new MODEL(Entity, keyValues);
        }
        public static MODEL NewDataModel(DataTable dtCache, params object[] keyValues)
        {
            return new MODEL(Entity, dtCache, keyValues);
        }
        public static Form NewEntity()
        {
            return new Form("FL_Forms", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Form Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = NewEntity();
                }
                return _entity;
            }
        }
        public static MODEL Model
        {
            get
            {
                if (_model == null)
                {
                    _model = NewModel();
                }
                return _model;
            }
        }
        public static MODEL GetModel(string sSql)
        {
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return NewDataModel(dr);
        }
        public static List<MODEL> GetModels(string sSql)
        {
            List<MODEL> lm = new List<MODEL>();
            DataTable dt = XSql.GetDataTable(sSql);
            foreach (DataRow dr in dt.Rows)
            {
                lm.Add(NewDataModel(dr));
            }
            return lm;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField Id;
            public XDataField CatagoryId;
            public XDataField Name;
            public XDataField Module;
            public XDataField Module_Short;
            public XDataField Script;
            public XDataField Css;
            public XDataField Items;
            public XDataField Sort;
            public XDataField DepartmentId;

            public MODEL() { }
            public MODEL(Form parentEntity) : base(parentEntity) { }
            public MODEL(Form parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Form parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Form parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int16);
                this.CatagoryId = new XDataField("CatagoryId", DbType.Int16);
                this.Name = new XDataField("Name", DbType.String);
                this.Module = new XDataField("Module", DbType.String);
                this.Module_Short = new XDataField("Module_Short", DbType.String);
                this.Script = new XDataField("Script", DbType.String);
                this.Css = new XDataField("Css", DbType.String);
                this.Items = new XDataField("Items", DbType.String);
                this.Sort = new XDataField("Sort", DbType.Int16);
                this.DepartmentId = new XDataField("DepartmentId", DbType.Int32);
                //

                this.Id.isIdentity = true;
                this.Id.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.CatagoryId, this.Name, this.Module, this.Module_Short, this.Script, this.Css, this.Items, this.Sort, this.DepartmentId });
            }
        }
    }
}
