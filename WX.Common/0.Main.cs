using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Model;
using ULCode.QDA;
using System.Web.UI;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace WX
{
    public class Main
    {
        public static int Whours = 7;
        public static int DefaultCompanyId
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultCompanyId"]); }
        }
        public static WX.Model.Company.MODEL DefaultCompany
        {
            get
            {
                return WX.Model.Company.GetCache(DefaultCompanyId);
            }
        }
        public static bool Priv_IsTemp
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Priv-IsTemp"].ToString() == "1";
            }
        }
        public static string GetConfigItem(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }
        public static void SaveConfig(string ConnenctionString, string strKey, string url)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            doc.Load(url);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == strKey)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    att.Value = ConnenctionString;
                    break;
                }
            }
            doc.Save(url);
        }
        public static string SuperPwd
        {
            get
            {
                if (ConfigurationManager.AppSettings["SuperPwd"] == null)
                    return null;
                else
                    return Convert.ToString(ConfigurationManager.AppSettings["SuperPwd"]);
            }
        }
        #region //当前用户 Main.CurUser,Main.NewUser,Main.GetUser(object user),Main.PageUser
        public static WXUser CurUser
        {
            get
            {
                return PageUser;
                //return new WX.CurUser();
                //if (HttpContext.Current.Session["Main_CurUser"] == null)
                //{
                //    WX.CurUser cu = new WX.CurUser();
                //    HttpContext.Current.Session["Main_CurUser"] = cu;
                //}
                //return (WX.CurUser)HttpContext.Current.Session["Main_CurUser"];
                //#if DEFAULT
                //#else                
                //#endif
            }
        }
        public static WXUser NewCurUser()
        {
            return new WX.WXUser();
        }
        public static WXUser GetUser(object user)
        {
            return new WX.WXUser(user);
        }
        public static WXUser PageUser
        {
            get
            {
                if (LegalClient())
                {
                    return WX.Public.GetUser(Convert.ToString(HttpContext.Current.Request.QueryString["xUser"]));
                }
                //if (HttpContext.Current.Session["Main_CurUser"] == null)
                //{
                //    WX.WXUser cu = new WX.WXUser();
                //    HttpContext.Current.Session["Main_CurUser"] = cu;
                //}
                //return (WX.WXUser)HttpContext.Current.Session["Main_CurUser"];
                return WX.Public.PageUser;
            }
        }
        public static void ClearCurUser()
        {
            WX.WXUser cu = PageUser;
            cu = null;
            HttpContext.Current.Session["Main_CurUser"] = null;
        }
        public static bool LegalClient()
        {
            return (HttpContext.Current.Request.UserHostAddress == "112.230.205.194"
                       || HttpContext.Current.Request.UserHostAddress == "127.0.0.1")
                              && !String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Request.QueryString["xUser"]));
        }
        #endregion

        #region //简易执行Sql语句 ExecuteDelete,ExecuteDeleteAll,ExecuteUpdate
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="key">唯一列名</param>
        /// <param name="id">值</param>
        /// <returns></returns>
        public static int ExecuteDelete(string tablename, string key, string id)
        {
            return ULCode.QDA.XSql.Execute("delete from " + tablename + " where " + key + " = '" + id + "'");
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="key">唯一列名</param>
        /// <param name="id">多个值，中间用逗号隔开</param>
        /// <returns></returns>
        public static int ExecuteDeleteAll(string tablename, string key, string idlist)
        {
            return ULCode.QDA.XSql.Execute("delete from " + tablename + " where " + key + " in('" + idlist.Replace(",", "','") + "')");
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="setclum">修改列</param>
        /// <param name="wherestr">条件</param>
        /// <returns></returns>
        public static int ExcuteUpdate(string tablename, string setclum, string wherestr)
        {
            return ULCode.QDA.XSql.Execute("update " + tablename + " set " + setclum + " where " + wherestr);
        }
        public static int DeleteFiles(string sSql)
        {
            int iR = 0;
            object[] arr = ULCode.QDA.XSql.GetXDataTable(sSql).ToObjectArray();
            if (arr != null)
            {
                foreach (object o in arr)
                {
                    try
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(Convert.ToString(o)));
                        iR++;
                    }
                    catch
                    {
                        ;
                    }
                }
            }
            return iR;
        }
        #endregion
        public static bool SendEmail(string toemail, string title, string body)
        {
            try
            {
                System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
                mail.From = "SDJNWX123@163.com";
                mail.To = toemail;
                mail.Subject = title;
                mail.Priority = System.Web.Mail.MailPriority.High;
                mail.Body = body;
                mail.BodyFormat = System.Web.Mail.MailFormat.Html;
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //用户名 
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SDJNWX123@163.com");
                //密码 
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "wx123456");
                //SMTP地址 
                System.Web.Mail.SmtpMail.SmtpServer = "smtp.163.com";
                //开始发送邮件 
                System.Web.Mail.SmtpMail.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
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
                WX.ServiceReference1.ServiceSoapClient smsClient = new WX.ServiceReference1.ServiceSoapClient();
                result = smsClient.SendSMS("gongshanglian", "123456", mobile, m.Value);
            }
            return result + "," + count;
        }

        public static string DealWithUrlForClient(string url)
        {
            if (LegalClient())
            {
                if (!url.ToLower().Contains("xuser="))
                {
                    if (url.Contains("?"))
                        url = String.Format("{0}&xUser={1}", url, HttpContext.Current.Request["xUser"]);
                    else
                        url = String.Format("{0}?xUser={1}", url, HttpContext.Current.Request["xUser"]);
                }
            }
            return url;
        }
        #region //日志公共接口 AddLog(LogType.Default,"用户添加成功！","");
        /// <summary>
        /// 添加当前用户的工作日志
        /// </summary>
        /// <param name="LogTypeID">LogTypeID</param>
        /// <param name="title"></param>
        /// <param name="parameters"></param>
        public static void AddLog(LogType logType, string title, string parameters)
        {
            AddLog(Convert.ToInt32(logType), title, parameters);
        }
        public static void AddLog(int logTypeID, string title, string parameters)
        {
            string tableName = (string)XSql.GetValue("SELECT TableName FROM TL_LogType WHERE ID=" + logTypeID);
            string param = parameters;
            string userId = Authentication.GetUserID();
            if (string.IsNullOrEmpty(param))
            {
                param = "NULL";
            }
            else
            {
                param = String.Format("'{0}'", param);
            }
            if (!string.IsNullOrEmpty(tableName))
            {
                string cmdText = String.Format("INSERT INTO " + tableName + " (Title,UserID,LogType,LogTime,LogIP,LogParaments) VALUES ('{0}','{1}',{2},'{3}','{4}',{5})", title, userId, logTypeID, DateTime.Now, HttpContext.Current.Request.UserHostAddress, param);
                XSql.Execute(cmdText);
            }
        }
        #endregion
        // <summary>
        /// 加密
        /// </summary>
        /// <param name="CryptText">要加密的文件</param>
        /// <param name="CryptKey">密匙</param>
        /// <returns></returns>
        public static string Encrypt(string CryptText, string CryptKey)
        {
            if (CryptKey.Length < 8)
            {
                CryptKey = CryptKey.PadRight(8, 'O');
            }
            else
            {
                CryptKey = CryptKey.Substring(0, 8);
            }

            return MyEncrypt.DESEncrypt(CryptText, CryptKey, CryptKey);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="CryptText">加密的字符串</param>
        /// <param name="CryptKey">密匙</param>
        /// <returns></returns>
        public static string Decrypt(string CryptText, string CryptKey)
        {
            if (CryptKey.Length < 8)
            {
                CryptKey = CryptKey.PadRight(8, 'O');
            }
            else
            {
                CryptKey = CryptKey.Substring(0, 8);
            }
            return MyEncrypt.DESDecrypt(CryptText, CryptKey, CryptKey);
        }

        #region //用户权限公共接口 GetPermission()
        /// <summary>
        /// 获取当前用户的功能权限
        /// </summary>
        /// <param name="functionId">使用功能ID</param>
        /// <returns></returns>
        //public static int GetPermission() { return GetPermission(false); }
        public static int GetPermission(bool IsServiceInterFace)
        {
            int flag = 0;
            string pageurl = HttpContext.Current.Request.Url.AbsolutePath;
            //个人信息直接允许
            //if (pageurl.ToLower().Contains("/private/")) return 3;

            WX.Model.Function.MODEL funcM = WX.Model.Function.GetCache(pageurl);
            if (funcM != null&&funcM.TypeID.ToInt32()>1&&funcM.State.ToInt32()==1)
            {
                WX.Main.CurUser.LoadMyCompany();
                object authcode = System.Configuration.ConfigurationManager.AppSettings["AuthCode"];
                if (authcode !=null)
                {
                    try
                    {
                        string[] array = authcode.ToString().Split('-');
                        if (Decrypt(array[0], WX.Main.CurUser.MyCompany.ID.ToString() + "-" + array[1]) == System.Configuration.ConfigurationManager.AppSettings["SalesCode"].ToString())
                        {
                            if (Convert.ToInt32(array[1]) >= funcM.TypeID.ToInt32())
                                return 3;
                        }
                    }
                    catch { }
                }
                return -2;//返回-2说明此功能为付费功能，且没有有效授权，不可使用
            }
            WX.WXUser user;
            if (IsServiceInterFace)
                user = WX.Main.NewCurUser();
            else
                user = WX.Main.CurUser;

            //管理员直接允许
            if (user.IsAdministratorUser) return 3;

            user.LoadUserModel(false);
            //系统管理员直接返回
            if (user.UserModel.DutyId.ToInt32() == 0) return 3;
            user.LoadDutyUser(false);
            //从页找到功能功能
            // WX.Model.Function.MODEL funcM = WX.Model.Function.GetCurFunciton(pageurl);
            if (funcM != null)
            {
                if (GetFuncState(funcM) > 0)
                {   //其它父目录是否打开
                    flag = GetPermission(user.DutyUser, funcM);
                }
                else
                {
                    flag = -1;
                }

            }
            else
            {
                //找不到此页暂为3,正式运营后请改为0
                flag = 3;
            }
            if (IsServiceInterFace)
                user = null;
            return flag;
        }
        //此函数暂未用到(可删除)
        public static int GetPermission(int funcID)
        {
            int flag = 0;
            WX.WXUser user = WX.Main.NewCurUser();
            user.LoadDutyUser(false);
            WX.Model.Function.MODEL funcM = WX.Model.Function.GetCache(funcID);
            if (funcM != null)
            {

                if (GetFuncState(funcM) > 0)
                {
                    flag = GetPermission(user.DutyUser, funcM);
                }
                else
                {
                    flag = 0;
                }
            }
            else
                flag = 3;
            return flag;
        }
        //功能的父目录
        private static int GetFuncState(WX.Model.Function.MODEL funcM)
        {
            int perm = int.Parse(funcM.State.value.ToString());
            if (perm == 0) return 0;

            int upId = int.Parse(funcM.ParentID.value.ToString());
            while (perm > 0 && upId > 0)
            {
                WX.Model.Function.MODEL pmodel = WX.Model.Function.GetCache(upId);//WX.Model.Function.NewDataModel(pid);
                if (pmodel != null)
                {
                    perm = int.Parse(pmodel.State.value.ToString());
                    upId = int.Parse(pmodel.ParentID.value.ToString());
                }
                else
                {
                    perm = 0;
                }
            }
            return perm;
        }
        public static int GetPermission(Duty.MODEL duty, Function.MODEL func)
        {
            if (func.State.ToInt32() == 0)
                return -1;
            else
                return ULCode.QDA.XSql.GetData("select Flag from TE_FunctionsInDuties where FunctionID=" + func.ID.value.ToString() + " and DutyID=" + duty.ID.value.ToString()).ToInt32();
        }
        /// <summary>
        /// 简捷使用
        /// </summary>
        /// <param name="functionId">使用功能ID</param>
        /// <returns></returns>
        //public static bool ExecPermission(int functionId)
        //{
        //    if (!GetPermission(functionId))
        //    {
        //        HttpContext.Current.Response.Write("你没有权限访问此功能！");
        //        HttpContext.Current.Response.End();
        //        return false;
        //    }
        //    else
        //        return true;
        //}
        #endregion

        #region //根据职务和功能动态生成左侧菜单 CreateMenu(int DutyID)

        public static string GetMenu()
        {
            WX.WXUser user = WX.Main.CurUser;
            DataTable dt = ULCode.QDA.XSql.GetDataTable("exec Get_MaxRole '" + user.UserID + "'");
            Duty.MODEL duty = Duty.GetCache(0);
            if (user.IsEmployeeUser)
            {
                user.LoadDutyUser();
                duty = user.DutyUser;
            }
            if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > duty.GradeID.ToInt32())
            {
                return dt.Rows[0][1].ToString();
            }
            return duty.Menus.ToString();
        }
        public static string CreateMenu(int DutyID)
        {
            string sSql = "exec [dbo].[sp_get_tree_multi_dutyinfontction] 'TE_Functions','ID','Name','ParentID',0,1," + DutyID;
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "-1";
            }
            int level = 3;
            string path = "-1";
            string menus = "'menus': [";
            int pflag = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                level = int.Parse(dt.Rows[i]["node_level"].ToString());
                if (level == 1)
                {
                    pflag = Convert.ToInt32(dt.Rows[i]["Flag"].ToString().Trim());
                }
                bool b = dt.Rows[i]["State"].ToString().Trim() == "1" && Convert.ToInt32(dt.Rows[i]["Flag"].ToString().Trim()) > 0;
                if (level == 2)
                {
                    b = dt.Rows[i]["State"].ToString().Trim() == "1" && Convert.ToInt32(dt.Rows[i]["Flag"].ToString().Trim()) > 0 && pflag > 0;
                }
                if (b)
                {
                    if (level == 1 && i > 0)
                    {
                        menus = menus.Substring(0, menus.Length - 1);
                        if (!(i + 1 == dt.Rows.Count || dt.Rows[i]["node_path"].ToString().IndexOf(path) == -1))
                        {
                            menus += "]";
                        }
                        else if (int.Parse(dt.Rows[i - 1]["node_level"].ToString()) > 1)
                        {
                            menus += "]";
                        }

                        menus += "},";
                    }
                    if (level == 1 && path.IndexOf(dt.Rows[i]["ID"].ToString()) == -1)
                    {
                        path = dt.Rows[i]["ID"].ToString();
                    }
                    menus += "{\n";
                    menus += "'menuid': '" + dt.Rows[i]["ID"].ToString() + "',\n'icon': '" + dt.Rows[i]["Icon"].ToString() + "',\n'menuname': '" + dt.Rows[i]["Name"].ToString() + "',\n";
                    if (level == 1)
                    {
                        if (!(i + 1 == dt.Rows.Count || dt.Rows[i + 1]["node_path"].ToString().IndexOf(path) == -1))
                        {
                            menus += "'menus': [";
                        }
                    }
                    if (level == 2)
                    {
                        menus += "'url': '" + dt.Rows[i]["Url"].ToString() + "'\n},";
                    }

                }
            }
            menus = menus.Substring(0, menus.Length - 1) + (int.Parse(dt.Rows[dt.Rows.Count - 1]["node_level"].ToString()) == 2 ? "\n]\n" : "") + "}\n]\n";
            return menus;
        }
        #endregion

        #region //穿过代理服务器取远程用户真实IP地址 getIp(Page page),getIp(httpContext),getIp(null)
        /// <summary>
        /// 穿过代理服务器取远程用户真实
        /// </summary>
        /// <param name="httpContext">可以是Page,也可以是HttpContext,也可以null</param>
        /// <returns></returns>
        public static string getIp(object httpContext)
        {
            HttpRequest hr;
            if (httpContext is Page)
                hr = ((Page)httpContext).Request;
            else if (httpContext is HttpContext)
                hr = ((HttpContext)httpContext).Request;
            else
                hr = HttpContext.Current.Request;
            string Ip = string.Empty;
            if (hr.ServerVariables["HTTP_VIA"] != null)
            {
                if (hr.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (hr.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = hr.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (hr.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = hr.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            Ip = "202.96.134.133";
                }
                else
                    Ip = hr.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (hr.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = hr.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                Ip = hr.UserHostAddress;
            }
            return Ip;
        }
        #endregion
        /// <summary>
        /// 快速生成DataTable
        /// </summary>
        /// <param name="datas">字符串数组</param>
        /// <returns></returns>
        public static DataTable GetTestDataTable(String[] datas)
        {
            return new ULCode.QDA.XDataTable(datas).ToDataTable();
        }
        /// <summary>
        /// 从数据库中获取分页记录集
        /// </summary>
        /// <param name="sql">Sql语句,(1-不能为Top,2-不能有Order By条件串)</param>
        /// <param name="top">前N条，大于0时起作用</param>
        /// <param name="orderBy">排序字符串Order by FieldName</param>
        /// <param name="pageSize">每页N条</param>
        /// <param name="pageIndex">第N页</param>
        /// <returns></returns>
        public static DataTable GetPagedRows(string sql, int top, string orderBy, int pageSize, int pageIndex)
        {
            string sSql = String.Format("exec [dbo].[SYST_pGetPageRows] '{0}',{1},'{2}',{3},{4}", sql.Replace("'", "''"), top, orderBy, pageSize, pageIndex);
            return ULCode.QDA.XSql.GetDataTable(sSql);
        }
        /// <summary>
        /// 获取数据的记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int GetPagedRowsCount(string sql)
        {
            sql = "Select Count(*) from (" + sql + ") as A";
            return ULCode.QDA.XSql.GetData(sql).ToInt32();
        }
        /// <summary>
        /// 直接下载而不是打开
        /// </summary>
        public static void DownloadFile(string downloadUrl, string renameFile)
        {
            string path = HttpContext.Current.Server.MapPath(downloadUrl);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //  添加头信息，为"文件下载/另存为"对话框指定默认文件名
            if (String.IsNullOrEmpty(renameFile)) renameFile = file.Name;
            renameFile = HttpContext.Current.Server.UrlPathEncode(renameFile);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + renameFile);
            //  添加头信息，指定文件大小，让浏览器能够显示下载进度
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            //  指定返回的是一个不能被客户端读取的流，必须被下载
            //if (String.IsNullOrEmpty(contentType))
            //    contentType = "application/ms-excel";
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //  把文件流发送到客户端
            //HttpContext.Current.Response.TransmitFile(file.FullName);
            HttpContext.Current.Response.WriteFile(file.FullName);
            //  停止页面的执行
            HttpContext.Current.Response.End();
            file = null;
        }
        /// <summary>
        /// 得到时间与现在的时间差字符串
        /// 例如：GetTimeEslapseStr(myDate,"还有{0}{1}","{0}{1}前")
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="comingFormat">还不到的时间格式：还有{0}{1}</param>
        /// <param name="pastFormat">已过的时间格式：{0}{1}前</param>
        /// <returns></returns>
        /// 
        public static string GetTimeEslapseStr(DateTime dt, string comingFormat, string pastFormat)
        {
            return GetTimeEslapseStr(dt, comingFormat, pastFormat, false);
        }

        public static string GetTimeEslapseStr(DateTime dt, string comingFormat, string pastFormat, bool onlyDay)
        {
            if (dt == null)
                return "";
            if (String.IsNullOrEmpty(comingFormat)) comingFormat = "还有{0}{1}";
            if (String.IsNullOrEmpty(pastFormat)) pastFormat = "{0}{1}前";
            TimeSpan ts = DateTime.Now - dt;
            if (ts.Days != 0)
            {
                if (ts.Days > 0)
                    return String.Format(pastFormat, ts.Days, "天");
                else
                    return String.Format(comingFormat, -ts.Days, "天");
            }
            else if (ts.Hours != 0 && !onlyDay)
            {
                if (ts.Hours > 0)
                    return String.Format(pastFormat, ts.Hours, "小时");
                else
                    return String.Format(comingFormat, -ts.Hours, "小时");
            }
            else if (ts.Minutes != 0 && !onlyDay)
            {
                if (ts.Minutes > 0)
                    return String.Format(pastFormat, ts.Minutes, "分钟");
                else
                    return String.Format(comingFormat, -ts.Minutes, "分钟");
            }
            else if (!onlyDay)
            {
                if (ts.Seconds > 0)
                    return String.Format(pastFormat, ts.Seconds, "秒");
                else
                    return String.Format(comingFormat, -ts.Seconds, "秒");
            }
            else
            {
                return "今天";
            }
        }
        /// <summary>
        /// 得到两个时间的时间差字符串
        /// 例如：GetTimeEslapseStr(myDate,"还有{0}{1}","{0}{1}")
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="comingFormat">还不到的时间格式：还有{0}{1}</param>
        /// <param name="pastFormat">已过的时间格式：{0}{1}前</param>
        /// <returns></returns>
        public static string GetTime_X_EslapseStr(DateTime dt, DateTime dt2, string comingFormat, string pastFormat)
        {
            if (dt == null)
                return "";
            if (String.IsNullOrEmpty(comingFormat)) comingFormat = "已过{0}{1}";
            if (String.IsNullOrEmpty(pastFormat)) pastFormat = "用了{0}{1}";
            TimeSpan ts = dt - dt2;
            if (ts.Days != 0)
            {
                if (ts.Days > 0)
                    return String.Format(pastFormat, ts.Days, "天");
                else
                    return String.Format(comingFormat, -ts.Days, "天");
            }
            else if (ts.Hours != 0)
            {
                if (ts.Hours > 0)
                    return String.Format(pastFormat, ts.Hours, "小时");
                else
                    return String.Format(comingFormat, -ts.Hours, "小时");
            }
            else if (ts.Minutes != 0)
            {
                if (ts.Minutes > 0)
                    return String.Format(pastFormat, ts.Minutes, "分钟");
                else
                    return String.Format(comingFormat, -ts.Minutes, "分钟");
            }
            else
            {
                if (ts.Seconds > 0)
                    return String.Format(pastFormat, ts.Seconds, "秒");
                else
                    return String.Format(comingFormat, -ts.Seconds, "秒");
            }
        }
        public static string GetIPStr(object evalIp)
        {
            string ip = Convert.ToString(evalIp);
            if (String.IsNullOrEmpty(ip))
                return "开发测试";
            else if (ip == "::1")
                return "开发测试";
            else if (ip == "127.0.0.1")
                return "开发测试";
            else
                return ip;
        }
        public static void CloseDialog_In_EasyUIDialog(Page page, String msg)
        {
            string js = String.Empty;
            if (!String.IsNullOrEmpty(msg))
                js += String.Format("alert('{0}');", msg);
            js += "window.parent.document.getElementById('dialogCase').style.display = 'none';";
            //js += "window.parent.loation.reload();";//此语句不能有效更新
            js += "window.parent.location.href=window.parent.location.href;";
            ULCode.Debug.ServJs(page, js);
        }
        public static bool IsBestDuty(int deptid, string userid)
        {
            WX.Model.User.MODEL usermodel = WX.Model.User.GetModel("select top 1 * from TU_Users where DepartmentID=" + deptid + " and State<40 order by Grade desc");
            if (usermodel != null && usermodel.UserID.ToString() == userid)
                return true;
            return false;
        }
        /// <summary>
        /// 发送站内消息
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="RedirectToUrl"></param>
        /// <param name="SendToUserId"></param>
        /// <param name="FromUserId"></param>
        /// <param name="type"></param>
        public static void MessageSend(string Title, string RedirectToUrl, string SendToUserId, string FromUserId, int type, int Role)
        {
            ULCode.QDA.XSql.Execute(String.Format("insert into TM_Messages( [ID],[Title],[RedirectToUrl],[SendToUserId],[FromUserId],[SendTime],[Type],[State],[Role]) values(newid(),'{0}','{1}','{2}','{3}',getdate(),{4},0,{5})",Title,RedirectToUrl,SendToUserId,FromUserId,type,Role));
        }
        /// <summary>
        /// 未读消息转入已读消息
        /// </summary>
        /// <param name="MessagesIDs"></param>
        public static void MessageToHistory(string MessagesIDs)
        {
            MessageToHistory_where(String.Format("ID in({0})", MessagesIDs));
        }
        /// <summary>
        /// 未读消息转入已读消息
        /// </summary>
        /// <param name="MessagesIDs"></param>
        public static void MessageToHistory_where(string WhereStr)
        {
            String sSql = String.Format("insert into TM_HistoryMessages SELECT * FROM TM_Messages where {0};"
                       + "Update TM_HistoryMessages set State=1 where {0};"
                       + "Delete from TM_Messages where {0}", WhereStr);
            ULCode.QDA.XSql.Execute(sSql);
        }
        public static string ShowFormula(string Formulastr)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from FD_Variable where Type=0 or Type=2");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Formulastr = Formulastr.Replace(dt.Rows[i]["VarValue"].ToString(), dt.Rows[i]["Name"].ToString());
            }
            Formulastr = Formulastr.Replace("&{", "");
            Formulastr = Formulastr.Replace("}", "");
            return Formulastr;
        }
        public static System.Data.DataTable GetDeptUsersAll()
        {
            int deptid = GetParentDeptID();
            string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + deptid).ToColValueList(",", 0);
            return ULCode.QDA.XSql.GetDataTable("Select * from view_DeptUsersAll where  DepartmentID in(" + deptid + (ids != "" ? "," + ids : "") + ")");

        }
        public static int GetParentDeptID()
        {
            WX.Main.CurUser.LoadUserModel(true);
            WX.Main.CurUser.LoadMyDepartment();
            bool flag = true;
            int deptid = WX.Main.CurUser.MyDepartMent.ID.ToInt32();
            int parent = WX.Main.CurUser.MyDepartMent.ParentID.ToInt32();
            while (flag)
            {
                if (parent == 0)
                    flag = false;
                else
                {
                    WX.Model.Department.MODEL dmodel = WX.Model.Department.NewDataModel(parent);
                    deptid = dmodel.ID.ToInt32();
                    parent = dmodel.ParentID.ToInt32();
                }
            }
            return deptid;
        }
        public static int GetParentDeptID(int id)
        {
            WX.Model.Department.MODEL deptmodel = WX.Model.Department.NewDataModel(id);
            bool flag = true;
            int deptid = deptmodel.ID.ToInt32();
            int parent = deptmodel.ParentID.ToInt32();
            while (flag)
            {
                if (parent == 0)
                    flag = false;
                else
                {
                    WX.Model.Department.MODEL dmodel = WX.Model.Department.NewDataModel(parent);
                    deptid = dmodel.ID.ToInt32();
                    parent = dmodel.ParentID.ToInt32();
                }
            }
            return deptid;
        }
        public static bool IsFinanceRole()
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select *  from vw_FinanceRole where UserID='"+WX.Main.CurUser.UserID+"'");
            return dt.Rows.Count>0;
        }
        public static string GetUserDeptids(string userid)
        {
            string idssql = "select ID  from TE_Departments where Host='" + userid + "' or SubHosts='" + userid + "' or Assistants='" + userid+ "'";
            return  ULCode.QDA.XSql.GetXDataTable(idssql + " or ParentID in(" + idssql + ")").ToColValueList(",", 0);
                    
        }
    }
}
