using System;
using System.Collections.Generic;
using System.Text;

namespace ULCode.QDA
{
    public abstract class DynamicSqlStatement
    {        
        public abstract String GetSql(ProviderType providerType, string name);
    }
}
