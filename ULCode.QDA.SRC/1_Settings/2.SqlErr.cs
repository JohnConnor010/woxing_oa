using MySql.Data.MySqlClient;
using System;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ULCode.QDA
{
    /// <summary>
    /// 将被淘汰
    /// </summary>
    public class SqlErr
    {
        private static Dictionary<String, DbException> Errors = new Dictionary<string, DbException>();
        private static List<String> ErrorAccessList=new List<string>();  //列表中存在Key时，才可捕捉。
        //0.清空所有，应用程序系统管理应用
        private static void ClearAll()
        {
            Errors.Clear();
            ErrorAccessList.Clear();
        }
        //1.开始捕捉，用户应用
        public static void StartCapture(string key)
        {
            if (!ErrorAccessList.Contains(key))
                ErrorAccessList.Add(key);

            if (Errors.ContainsKey(key))
                Errors.Remove(key);
        }
        //2.在数据访问组件中应用
        public static void Capture(String key, DbException ex)
        {
            if (!ErrorAccessList.Contains(key)) return;
            if (ex == null) return;
            Errors.Add(key, ex);
        }
        //3.停止捕捉，用户应用
        public static void StopCapture(string key)
        {
            if (ErrorAccessList.Contains(key))
                ErrorAccessList.Remove(key);
        }
        //4.是否获取到错误
        public static bool Found(string key)
        {
            return Errors.ContainsKey(key);
        }
        //5.获取到错误
        public static DbException GetCapturedError(String key)
        {
            if (Errors.ContainsKey(key))
                return Errors[key];
            else
                return null;
        }
    }
}

