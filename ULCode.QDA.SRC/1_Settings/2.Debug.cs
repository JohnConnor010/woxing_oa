namespace ULCode.QDA
{
    using System;
    using System.Web;
    using System.Configuration;
    /// <summary>
    /// 此类主要用来调试与输出
    /// </summary>
    public enum DebugType
    {
        NoDebug = 0,     //不调试状态
        HiddenDebug = 1, //无论错误与正确，都没有隐藏信息
        Debug = 2,       //错误有调试信息，而正确没有
        DebugAll = 3     //无论错误与正确，都有调试信息
    }
    public class Debug
    {
        //检测DEBUG模式
        private static DebugType _DEBUG = DebugType.NoDebug;
        public static DebugType DEBUG
        {
            get
            {
                if (_DEBUG == DebugType.NoDebug)
                {
                    if (ConfigurationManager.AppSettings["SqlDebug"] != null)
                    {
                        _DEBUG = (DebugType)Convert.ToInt32(ConfigurationManager.AppSettings["SqlDebug"]);
                    }
                    else
                    {
                        _DEBUG = DebugType.Debug;
                    }
                }
                return _DEBUG; 
            }
            set 
            {
                _DEBUG = value;
            }
        }
        //输出功能
        //1.根据Form或Web进行不同输出
        //2.不同格式的输出
        //3.可以指定输出文件
        public static bool DebugToFile
        {
            get
            {
                if (ConfigurationManager.AppSettings["DebugToFile"] != null)
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["DebugToFile"]);
                }
                else
                {
                    return false;
                }
            }
        }
        public static void PRINT(string sLogs)
        {
            PRINT(sLogs, false);
        }

        public static void PRINT(string format, object[] args)
        {
            PRINT(string.Format(format, args));
        }
        
        public static void PRINT(string slogs, bool endResponse)
        {
            string sLogs = String.Format("\r\n({0:MM-dd HH:mm}){1}", DateTime.Now, slogs);
            if (DebugToFile)
            {
                if (ConfigurationManager.AppSettings["DebugToFilePath"] != null)
                {
                    string sPath = Convert.ToString(ConfigurationManager.AppSettings["DebugToFilePath"]);
                    TextFile tf = new TextFile(sPath);
                    tf.Save(sLogs, true);
                    tf = null;
                }
            }
            if (IS_WEB_CLIENT)
            {
                HttpContext.Current.Response.Write("<br/>"+slogs);
                if (endResponse)
                {
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                System.Windows.Forms.Clipboard.SetText(slogs);
                System.Windows.Forms.MessageBox.Show(slogs);
                try
                {
                    if (endResponse)
                    {
                        Console.Clear();
                    }
                    Console.WriteLine(slogs);
                }
                catch
                {
                    ;
                }
            }
        }

        public static void ClearDebugToFile()
        {
            if (ConfigurationManager.AppSettings["DebugToFilePath"] != null)
            {
                string sPath = Convert.ToString(ConfigurationManager.AppSettings["DebugToFilePath"]);
                TextFile tf = new TextFile(sPath);
                tf.Save(String.Empty, false);
                tf = null;
            }
        }
        //环境检测
        public static bool IS_FORM_CLIENT
        {
            get
            {
                return (HttpContext.Current == null);
            }
        }

        public static bool IS_WEB_CLIENT
        {
            get
            {
                return (HttpContext.Current != null);
            }
        }
    }
}

