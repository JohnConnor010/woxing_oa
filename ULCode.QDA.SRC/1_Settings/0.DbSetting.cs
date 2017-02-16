namespace ULCode.QDA
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Runtime.InteropServices;
    
    public abstract class DbSetting
    {
        public abstract ConnectionStringSettings Find(string name);
        public abstract String GetDefaultName();
        public abstract ProviderType GetProviderType(string name);
        public abstract ConnectionStringSettingsCollection GetAllConnections();
    }
}

