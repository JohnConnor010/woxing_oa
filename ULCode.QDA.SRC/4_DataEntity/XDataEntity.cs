namespace ULCode.QDA
{
    using System;
    using System.Data;
    using ULCode;

    public class XDataEntity : XEntity
    {
        public XDataEntity(string tableName) : base(tableName)
        {

        }

        public XDataEntity(string tableName, string keyField) : base(tableName, keyField)
        {
        }

        public XDataEntity(string cnName, string tableName, string keyField) : base(cnName, tableName, keyField)
        {
        }
        //实现
        protected override int Execute(string sSql)
        {
            int iR = 0;
            if (string.IsNullOrEmpty(CnName))
            {
                iR = XSql.Execute(sSql);
                return iR;
            }
            string[] cns = CnName.Split(new char[] { ',' });
            foreach (string cn in cns)
            {
                iR += XSql.Execute(cn, sSql);
            }
            return iR;
        }

        protected override DataTable GetDataTable(string sSql)
        {
            string cn = string.Empty;
            if (string.IsNullOrEmpty(CnName))
            {
                cn = string.Empty;
            }
            else
            {
                cn = CnName.Split(new char[] { ',' })[0];
            }
            return XSql.GetDataTable(cn, sSql);
        }

        protected override object GetValue(string sSql)
        {
            string cn = string.Empty;
            if (string.IsNullOrEmpty(CnName))
            {
                cn = string.Empty;
            }
            else
            {
                cn = CnName.Split(new char[] { ',' })[0];
            }
            return XSql.GetValue(cn, sSql) ;
        }
    }
}

