namespace ULCode.QDA
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Runtime.InteropServices;

    public class WebConfigCnSetting : DbSetting
    {
        private string GetCnSettingName(string cnName)
        {
            if (string.IsNullOrEmpty(cnName))
                return this.DefaultCnSettingName;
            else
                return cnName;
        }
        private string DefaultCnSettingName
        {
            get
            {
                if (ConfigurationManager.AppSettings["DefaultCnSettingName"] != null)
                {
                    return ConfigurationManager.AppSettings["DefaultCnSettingName"].ToString();
                }
                if (ConfigurationManager.ConnectionStrings["DefaultCnSettingName"] != null)
                {
                    return "DefaultCnSettingName";
                }
                if (ConfigurationManager.ConnectionStrings["LocalSqlServer"] != null)
                {
                    return "LocalSqlServer";
                }
                if (ConfigurationManager.ConnectionStrings.Count > 0)
                {
                    return ConfigurationManager.ConnectionStrings[0].Name;
                }
                return string.Empty;
            }
        }
        //实现函数
        public override ConnectionStringSettings Find(string name)
        {
            name = this.GetCnSettingName(name);
            if (String.IsNullOrEmpty(name))
                return null;
            else if (ConfigurationManager.ConnectionStrings[name] == null)
            {
                return null;
            }
            else
                return ConfigurationManager.ConnectionStrings[name];
        }
        public override string GetDefaultName()
        {
            return this.DefaultCnSettingName;
        }
        public override ProviderType GetProviderType(string name)
        {
            switch (name)
            {
                case "System.Data.SqlClient": return ProviderType.MsSql;
                case "System.Data.OleDb": return ProviderType.OleDb;
                case "System.Data.Odbc": return ProviderType.Odbc;
                case "System.Data.OracleClient": return ProviderType.Oracle;
                case "MySql.Data.MySqlClient": return ProviderType.MySql;
            }
            return ProviderType.UnDefined;
        }
        public override ConnectionStringSettingsCollection GetAllConnections()
        {
            return ConfigurationManager.ConnectionStrings;
        }
    }
}

