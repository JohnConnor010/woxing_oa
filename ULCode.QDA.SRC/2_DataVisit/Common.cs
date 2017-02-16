namespace ULCode.QDA
{
    using System;
    public enum ProviderType
    {
        UnDefined,
        MsSql,
        OleDb,
        Odbc,
        Oracle,
        MySql,
        Db2,
        Sybase,
        Informix,
        INGRES
    }
    public enum CommandMode
    {
        Unknown = 0,
        Execute = 1,
        GetValue = 2,
        GetDataTable = 3,
        GetDataSet = 4,
        HasRow = 5,
        Count = 6,
        DataReader = 7,
        XmlReader = 8,
    }

}

