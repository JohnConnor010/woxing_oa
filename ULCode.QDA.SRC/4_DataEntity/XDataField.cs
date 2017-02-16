namespace ULCode.QDA
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ULCode.QDA.SqlStatement;
    /// <summary>
    /// 此类的作用在于：
    /// 1、为模型类提供基础字段数据。
    /// 2、为实体类提供Sql表达式语句。
    /// </summary>
    public class XDataField
    {
        #region 1.基本变量部分
        public XModel parentMode = null; 
        private string _cacheValue;
        public string CacheValue
        {
            get
            {
                if (this._cacheValue == null)
                {
                    return this.NameStr();
                }
                return this._cacheValue;
            }
            set
            {
                this._cacheValue = value;
            }
        }

        private string _name;
        private DbType _type=DbType.String;
        private object _value = null;
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public DbType type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        public object value
        {
            get
            {
                object oValue = this._value;
                if ((oValue == null) || (oValue == Convert.DBNull))
                {
                    oValue = this.defaultValue;
                }
                return oValue;
            }
            set
            {
                if (value == null)
                {
                    this.set(Convert.DBNull);
                }
                else
                {
                    this.set(value);
                }
            }
        }
        public object value_Initial = null;
        private object _defaultValue=null;
        private bool _desc=false;
        private bool _readOnly=false;
        private bool _selected=false;
        private bool _updated=false;  //说明与数据库存储同步

        public object defaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                this._defaultValue = value;
            }
        }
        public bool desc
        {
            get
            {
                return this._desc;
            }
            set
            {
                this._desc = value;
                this._selected = true;
            }
        }
        public bool readOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
            }
        }
        public bool selected
        {
            get
            {
                return this._selected;
            }
            set
            {
                this._selected = value;
            }
        }
        public bool updated
        {
            get
            {
                return this._updated;
            }
            set
            {
                this._updated = value;
            }
        }

        public bool addWith=false;
        public bool isIdentity=false;
        public bool isKeyField=false;

        public bool isBool
        {
            get
            {
                return (this.type == DbType.Boolean);
            }
        }
        public bool isDateTime
        {
            get
            {
                switch (this.type)
                {
                    case DbType.Date:
                    case DbType.DateTime:
                    case DbType.UInt64:
                    case DbType.DateTime2:
                        return true;
                }
                return false;
            }
        }
        public bool isNull
        {
            get
            {
                return ((this.value == null) || (this.value == Convert.DBNull));
            }
        }
        public bool isDbNull
        {
            get
            {
                return this.value == Convert.DBNull;
            }
        }
        public bool isEmpty
        {
            get
            {
                return ((this.value == null) || (this.value == Convert.DBNull) || (String.IsNullOrEmpty(Convert.ToString(this.value))));
            }
        }
        public bool isDecimal
        {
            get
            {
                switch (this.type)
                {
                    case DbType.Currency:
                    case DbType.Decimal:
                    case DbType.Double:
                        return true;
                }
                return false;
            }
        }
        public bool isNumberic
        {
            get
            {
                switch (this.type)
                {
                    case DbType.Byte:
                    case DbType.Int16:
                    case DbType.Int32:
                    case DbType.Int64:
                    case DbType.SByte:
                    case DbType.Single:
                    case DbType.UInt16:
                    case DbType.UInt32:
                    case DbType.UInt64:
                        return true;
                }
                return false;
            }
        }
        public int CompareTo(object var, string format, bool useInitialValue)
        {
            if (String.IsNullOrEmpty(format)) format = "{0}";
            string var1 = String.Format(format, useInitialValue ? this.value_Initial : this.value);
            string var2 = String.Format(format, var);
            return var1.CompareTo(var2);
        }
        public int CompareTo(object var, bool useInitialValue)
        {
            object var1 = useInitialValue ? this.value_Initial : this.value;
            object var2 = var;
            if ((var1 == Convert.DBNull || var1 == null) && (var2 == Convert.DBNull || var2 == null))
                return 0;
            else if ((var1 == Convert.DBNull || var1 == null) || (var2 == Convert.DBNull || var2 == null))
                return 1;
            else if (this.isDateTime)
                return Convert.ToDateTime(var1).CompareTo(Convert.ToDateTime(var2));
            else if (this.isDecimal)
                return Convert.ToDouble(var1).CompareTo(Convert.ToDouble(var2));
            else if (this.isNumberic)
                return Convert.ToInt64(var1).CompareTo(Convert.ToInt64(var2));
            else if (this.isBool)
                return Convert.ToBoolean(var1).CompareTo(Convert.ToBoolean(var2));
            else
                return Convert.ToString(var1).CompareTo(Convert.ToString(var2));
        }
        public void AddWith(object value)  //添加默认值
        {
            this.addWith = true;
            this.defaultValue = value;
        }
        public void AddWith(DefaultValueMode dvm)
        {
            this.addWith = true;
            this.defaultValue = this.GetDefaultValue(dvm);
        }
        public string GetDefaultValue(DefaultValueMode dvm)
        {
            string sdvalue = string.Empty;
            if (dvm == DefaultValueMode.Guid)
            {
                return Guid.NewGuid().ToString();
            }
            if (dvm == DefaultValueMode.UserIP)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            if (dvm == DefaultValueMode.UserName)
            {
                return HttpContext.Current.User.Identity.Name;
            }
            if (dvm == DefaultValueMode.UserID)
            {
                return Convert.ToString(HttpContext.Current.Session["UserID"]);
            }
            if (dvm == DefaultValueMode.RawUrl)
            {
                return HttpContext.Current.Request.RawUrl;
            }
            if (dvm == DefaultValueMode.ServerName)
            {
                return HttpContext.Current.Request.ServerVariables["Server_Name"];
            }
            if (dvm == DefaultValueMode.Url)
            {
                return HttpContext.Current.Request.Url.ToString();
            }
            if (dvm == DefaultValueMode.VirtualPath)
            {
                sdvalue = HttpContext.Current.Request.Path;
            }
            return sdvalue;
        }
        public void update()
        {
            this._updated = true;
        }
        public void select()
        {
            this._selected = true;
        }
        #endregion

        #region 2.Value Set
        public void set(object value)
        {
            if (this.CompareTo(value, true) != 0)
            {
                this._value = value;
                this._updated = true;
            }
        }
        public void set(XDataField xdfValue)
        {
            if (this._value != xdfValue.value)
            {
                this._value = xdfValue.value;
                this.updated = true;
            }
        }
        public void set_Int_Add(int iAdd)
        {
            if (isNull)
            {
                set(iAdd);
            }
            else
            {
                int iBase = Convert.ToInt32(value);
                iBase += iAdd;
                set(iBase);
            }
        }
        public void set_Int_Reduce(int iAdd)
        {
            if (isNull)
            {
                set(iAdd);
            }
            else
            {

                int iBase = Convert.ToInt32(value);
                iBase -= iAdd;
                set(iBase);
            }
        }
        public void set_Int_Increase()
        {
            set_Int_Add(1);
        }
        public void set_Int_Decrease()
        {
            set_Int_Reduce(1);
        }
        public void set_DateTime_Add(TimeSpan ts)
        {
            set(Convert.ToDateTime(value).Add(ts));
        }
        public void set_DateTime_Now()
        {
            set(DateTime.Now);
        }
        public void set_String_Append(string appendStr)
        {
            set(ULCode.Bind.isEmpty(Convert.ToString(value), "") + appendStr);
        }
        public void set_String_AppendLine(string appendStr)
        {
            string sV = Convert.ToString(value);
            if (ULCode.Bind.isEmpty(sV))
                sV = appendStr;
            else
                sV = sV + "\r\n" + appendStr;
            set(sV);
        }
        public void set_String_AppendFormat(string format, params object[] values)
        {
            set(ULCode.Bind.isEmpty(Convert.ToString(value), "") + String.Format(format, values));
        }
        public void set_String_Format(string format, params object[] values)
        {
            set(String.Format(format, values));
        }
        public void set_String_Replace(string oldChar, string newChar)
        {
            set(Convert.ToString(value).Replace(oldChar, newChar));
        }
        public void set_String_RegReplace(string pattern, string replacement)
        {
            set(System.Text.RegularExpressions.Regex.Replace(Convert.ToString(value), pattern, replacement));
        }
        public void set_noHTML()
        {
            set(ULCode.ULString.FilteredHtml(this.ToString()));
        }
        public void set_Trim()
        {
            string s = this.ToString().Trim();
            s = System.Text.RegularExpressions.Regex.Replace(s, @"(^\s*)|(\s*$)", String.Empty);
            set(s);
        }
        #endregion

        public XDataField(string name)
        {
            this._name = name;
        }
        public XDataField(string name, DbType dbType)
        {
            this._name = name;
            this._type = dbType;
        }
        public XDataField(string name, object Type, object value)
        {
            this._name = name;
            this._value = value;
        }
        public XDataField(string name,DbType dbType,object value)
        {
            this._name = name;
            this._type = dbType;
            this._value = value;
        }

        #region 3.Format Name String
        public string AvgNameStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AVG([");
            sb.Append(this.name);
            sb.Append("])");
            return sb.ToString();
        }
        public string CountNameStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Count([");
            sb.Append(this.name);
            sb.Append("])");
            return sb.ToString();
        }
        public string SumNameStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Sum([");
            sb.Append(this.name);
            sb.Append("])");
            return sb.ToString();
        }        
        public string MaxNameStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Max([");
            sb.Append(this.name);
            sb.Append("])");
            return sb.ToString();
        }
        public string MinNameStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Min([");
            sb.Append(this.name);
            sb.Append("])");
            return sb.ToString();
        }
        public string NameStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("["); //delete it for access
            sb.Append(this.name);
            sb.Append("]"); //delete it for access
            return sb.ToString();
        }
        public string ValueStr()
        {
            object objValue = this.value;
            return this.ValueStr(objValue);
        }
        public string ValueStr(object objValue)
        {
            if (objValue == Convert.DBNull || objValue == null)
            {
                return "NULL";
            }
            //SqlDbType
            if (this.isBool)
            {
                if (objValue == null)
                {
                    objValue = false;
                }
                return (Convert.ToBoolean(objValue) ? "1" : "0");
            }
            else if (this.isNumberic)
            {
                if (objValue == null || objValue == Convert.DBNull || String.IsNullOrEmpty(Convert.ToString(objValue)))
                {
                    objValue = 0;
                }
                return Convert.ToInt32(objValue).ToString();
            }
            string sv = Convert.ToString(objValue);
            if (sv.Contains("'"))
            {
                sv = sv.Replace("'", "''");
            }
            return String.Format("'{0}'", sv);
        }
        public string Expression
        {
            get
            {
                return (this.NameStr() + "=" + this.ValueStr());
            }
        }
        public string Condition
        {
            get
            {
                string v = this.ValueStr(this.value_Initial);
                if (v == "NULL")
                    return String.Format("{0} is NULL", this.NameStr());
                else
                    return (this.NameStr() + "=" + v);
            }
        }
        public string OrderStr() { return OrderStr(false); }
        public string OrderStr(bool withOrderBy)
        {
            string sOrder = String.Format("{0} {1}", NameStr(), desc ? "Desc" : "Asc");
            if (!String.IsNullOrEmpty(sOrder) && withOrderBy)
            {
                sOrder = " Order by " + sOrder;
            }
            return sOrder;
        }
        public string GetExpression(string operatorStr)
        {
            return (this.NameStr() + operatorStr + this.ValueStr());
        }
        public static string GetValueStr(object v)
        {
            if ((v == null) || (v == Convert.DBNull))
            {
                return String.Empty;
            }
            if (v is bool)
            {
                return (Convert.ToBoolean(v) ? "1" : "0");
            }
            int iTemp = 0;
            string sv = Convert.ToString(v);
            if (int.TryParse(sv, out iTemp))
            {
                return v.ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("'");
            if (sv.Contains("'"))
            {
                sv = sv.Replace("'", "''");
            }
            sb.Append(sv);
            sb.Append("'");
            return sb.ToString();
        }
        #endregion

        #region 4.Format Value String
        public DateTime ToDateTime()
        {
            if (this.isEmpty)
            {
                return DateTime.MinValue;
            }
            try
            {
                return Convert.ToDateTime(this.value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public int ToInt32()
        {
            if (this.isEmpty)
            {
                return 0;
            }
            return Convert.ToInt32(this.value);
        }
        public Boolean ToBoolean()
        {
            if (this.isEmpty)
            {
                return false;
            }
            if (this.isNumberic)
            {
                return Convert.ToInt32(this.value) == 1;
            }
            else
            {
                return Convert.ToBoolean(this.value);
            }
        }
        public override string ToString()
        {
            return this.ToString(string.Empty);
        }
        public string ToString(string nullValue)
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return nullValue;
            }
            return Convert.ToString(this.value);
        }
        public ULCode.QDA.DbValue DbValue
        {
            get
            {
                return new ULCode.QDA.DbValue(this.value);
            }
        }
        public string f(string format)
        {
            return Format(format);
        }
        public string Format(string format)
        {
            if (this.isEmpty)
            {
                return String.Empty;
            }
            if (this.type == DbType.DateTime)
                return String.Format(format, this.ToDateTime());
            else if (this.type == DbType.DateTime2)
                return String.Format(format, this.ToDateTime());
            else if (this.type == DbType.Date)
                return String.Format(format, this.ToDateTime());
            else if (this.type == DbType.Int16)
                return String.Format(format, Convert.ToInt16(this.value));
            else if (this.type == DbType.Int32)
                return String.Format(format, this.ToInt32());
            else if (this.type == DbType.Int64)
                return String.Format(format, Convert.ToInt64(this.value));
            else if (this.type == DbType.Byte)
                return String.Format(format, Convert.ToByte(this.value));
            else if (this.type == DbType.Decimal)
                return String.Format(format, Convert.ToDecimal(this.value));
            else if (this.type == DbType.Double)
                return String.Format(format, Convert.ToDouble(this.value));
            else if (this.type == DbType.Currency)
                return String.Format(format, Convert.ToDecimal(this.value));
            else
                return String.Format(format, this.value);
        }
        //其它
        public override bool Equals(object obj)
        {
            XDataField x = (XDataField)obj;
            return ((this.name == x.name) && (this.value == x.value));
        }
        public override int GetHashCode()
        {
            return ((this.name.Length * 100) + this.value.GetHashCode());
        }
        #endregion

        #region 5.DataField与表达式
        public const string DESC_FORMAT = "{0} Desc";
        public const string ASC_FORMAT = "{0} Asc";
        public OrderUnit Desc
        {
            get
            {
                return new OrderUnit(String.Format(DESC_FORMAT, this.NameStr()));
            }
        }
        public OrderUnit Asc
        {
            get
            {
                return new OrderUnit(String.Format(ASC_FORMAT, this.NameStr()));
            }
        }

        public const string BETWEEN_FORMAT = "{0} between {1} and {2}";
        public ConditionUnit Between(DateTime minValue, DateTime maxValue)
        {
            return new ConditionUnit(String.Format(BETWEEN_FORMAT,this.NameStr(),this.ValueStr(minValue),this.ValueStr(maxValue)));
        }
        public ConditionUnit Between(int minValue, int maxValue)
        {
            return new ConditionUnit(String.Format(BETWEEN_FORMAT, this.NameStr(), this.ValueStr(minValue), this.ValueStr(maxValue)));
        }
        public ConditionUnit Between(object minValue, object maxValue)
        {
            return new ConditionUnit(String.Format(BETWEEN_FORMAT, this.NameStr(), this.ValueStr(minValue), this.ValueStr(maxValue)));
        }

        public ConditionUnit In(string strList)
        {
            return new ConditionUnit(this.NameStr() + " in (" + strList + ")");
        }
        public ConditionUnit In(params int[] arrList)
        {
            string sString = string.Empty;
            for (int i = 0; i < arrList.Length; i++)
            {
                if (i != 0)
                {
                    sString = sString + ",";
                }
                sString = sString + Convert.ToString(arrList[i]);
            }
            return new ConditionUnit(this.NameStr() + " in (" + sString + ")");
        }
        public ConditionUnit In(params object[] arrList)
        {
            string sString = string.Empty;
            for (int i = 0; i < arrList.Length; i++)
            {
                if (i != 0)
                {
                    sString = sString + ",";
                }
                sString = sString + this.ValueStr(arrList[i]);
            }
            return new ConditionUnit(this.NameStr() + " in (" + sString + ")");
        }

        public ConditionUnit InDate(DateTime value)
        {
            return new ConditionUnit("Convert(char(10)," + this.NameStr() + ",120)=Convert(char(10),'" + Convert.ToString(value) + "',120)");
        }
        public ConditionUnit InToday()
        {
            return new ConditionUnit("Convert(char(10)," + this.NameStr() + ",120)=Convert(char(10),GetDate(),120)");
        }
        public ConditionUnit Like(string format)
        {
            return new ConditionUnit("" + this.NameStr() + " like '" + format + "'");
        }

        public SetsUnit SetAs(object value)
        {
            return new SetsUnit(this.NameStr() + "=" + this.ValueStr(value));
        }
        public SetsUnit SetAs(XDataField xf)
        {
            return new SetsUnit(this.NameStr() + "=" + xf.CacheValue);
        }
        public SetsUnit SetAs(ConditionUnit cu)
        {
            return new SetsUnit(this.NameStr() + "=" + cu.Value);
        }
        public SetsUnit SetAs(ValueExpression ve)
        {
            return new SetsUnit(this.NameStr() + "=" + ve.ValueG);
        }
        public SetsUnit SetAs_alternate(params object[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            if (values.Length >= 2)
            {
                sb.Append("(case " + this.NameStr() + " ");
                for (int i = 0; i < values.Length; i++)
                {
                    int iNext = i + 1;
                    if (iNext > (values.Length - 1))
                    {
                        iNext = 0;
                    }
                    sb.AppendFormat("when {0} then {1} ", this.ValueStr(values[i]), this.ValueStr(values[iNext]));
                }
                sb.Append(" end)");
            }
            else
            {
                sb.Append(this.ValueStr(values[0]));
            }
            return new SetsUnit(this.NameStr() + " = " + sb.ToString());
        }
        public SetsUnit SetAs_AppendLine(string appendStr)
        {
            return new SetsUnit(this.NameStr() + " = isNull(" + this.NameStr() + ",'') + '\r\n' + " + appendStr);
        }
        public SetsUnit SetAs_Append(string appendStr)
        {
            return new SetsUnit(this.NameStr() + " = isNull(" + this.NameStr() + ",'') + " + appendStr);
        }
        public SetsUnit SetAs_AppendFormat(string format, params object[] values)
        {
            return new SetsUnit(this.NameStr() + " = isNull(" + this.NameStr() + ",'') + " + String.Format(format, values));
        }
        public SetsUnit SetAs_Format(string format, params object[] values)
        {
            return new SetsUnit(this.NameStr() + "=" + String.Format(format, values));
        }

        public SetsUnit SetAs_now()
        {
            return new SetsUnit(this.NameStr() + " = GetDate()");
        }
        public SetsUnit SetAs_CurrentTime()
        {
            return new SetsUnit(this.NameStr() + " = '"+DateTime.Now.ToString()+"'");
        }
        public SetsUnit SetAs_text(string value)
        {
            return new SetsUnit(this.NameStr() + " = " + value);
        }
        public SetsUnit SetAs_today()
        {
            return new SetsUnit(this.NameStr() + " = GetDate()");
        }
        public SetsUnit SetAs_Increase() { return SetAs_Increase(1); }
        public SetsUnit SetAs_Increase(int iIncrease)
        {
            return new SetsUnit(String.Format("{0} = {0} + {1}", this.NameStr(), iIncrease));
        }
        public SetsUnit SetAs_Decrease() { return SetAs_Decrease(1); }
        public SetsUnit SetAs_Decrease(int iDecrease)
        {
            return new SetsUnit(String.Format("{0} = {0} - {1}", this.NameStr(), iDecrease));
        }

        ///ValueExpression = object + xf
        ///ValueExpression = object - xf
        ///ValueExpression = object * xf
        ///ValueExpression = object / xf
        ///ValueExpression = object % xf
        ///ValueExpression = object & xf
        ///ValueExpression = object | xf
        public static ValueExpression operator +(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " + " + xf.NameStr());
        }
        public static ValueExpression operator -(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " - " + xf.NameStr());
        }
        public static ValueExpression operator *(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " * " + xf.NameStr());
        }
        public static ValueExpression operator /(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " / " + xf.NameStr());
        }
        public static ValueExpression operator %(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " % " + xf.NameStr());
        }
        public static ValueExpression operator &(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " & " + xf.NameStr());
        }
        public static ValueExpression operator |(object value, XDataField xf)
        {
            return new ValueExpression(xf.ValueStr(value) + " | " + xf.NameStr());
        }

        ///ValueExpression = xf + object
        ///ValueExpression = xf - object
        ///ValueExpression = xf * object
        ///ValueExpression = xf / object
        ///ValueExpression = xf % object        
        ///ValueExpression = xf & object        
        ///ValueExpression = xf | object        
        public static ValueExpression operator +(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " + " + xf.ValueStr(value));
        }
        public static ValueExpression operator -(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " - " + xf.ValueStr(value));
        }
        public static ValueExpression operator *(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " / " + xf.ValueStr(value));
        }
        public static ValueExpression operator /(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " / " + xf.ValueStr(value));
        }
        public static ValueExpression operator %(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " % " + xf.ValueStr(value));
        }
        public static ValueExpression operator &(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " & " + xf.ValueStr(value));
        }
        public static ValueExpression operator |(XDataField xf, object value)
        {
            return new ValueExpression(xf.NameStr() + " | " + xf.ValueStr(value));
        }
 
        ///ValueExpression = XDataField + XDataField (无)
        ///ValueExpression = XDataField - XDataField
        ///ValueExpression = XDataField * XDataField
        ///ValueExpression = XDataField / XDataField
        ///ValueExpression = XDataField % XDataField        
        ///ValueExpression = XDataField & XDataField        
        ///ValueExpression = XDataField | XDataField        
        public static ValueExpression operator -(XDataField xf1, XDataField xf2)
        {
            return new ValueExpression(xf1.NameStr() + "-" + xf2.NameStr());
        }
        public static ValueExpression operator *(XDataField xf1, XDataField xf2)
        {
            return new ValueExpression(xf1.NameStr() + "*" + xf2.NameStr());
        }
        public static ValueExpression operator /(XDataField xf1, XDataField xf2)
        {
            return new ValueExpression(xf1.NameStr() + "/" + xf2.NameStr());
        }
        public static ValueExpression operator %(XDataField xf1, XDataField xf2)
        {
            return new ValueExpression(xf1.NameStr() + "%" + xf2.NameStr());
        }
        public static ValueExpression operator &(XDataField xf1, XDataField xf2)
        {
            return new ValueExpression(xf1.NameStr() + "&" + xf2.NameStr());
        }
        public static ValueExpression operator |(XDataField xf1, XDataField xf2)
        {
            return new ValueExpression(xf1.NameStr() + "|" + xf2.NameStr());
        }

        ///ValueExpression = XDataField + FieldsExpression(冲突删除)
        ///ValueExpression = XDataField - FieldsExpression
        ///ValueExpression = XDataField * FieldsExpression
        ///ValueExpression = XDataField / FieldsExpression
        ///ValueExpression = XDataField % FieldsExpression        
        ///ValueExpression = XDataField & FieldsExpression        
        ///ValueExpression = XDataField | FieldsExpression        
        //public static ValueExpression operator +(XDataField xf, FieldsExpression fe)
        //{
        //    return new ValueExpression(xf.NameStr() + " + " + fe.ValueExpressionStr());
        //}
        public static ValueExpression operator -(XDataField xf, FieldListExpression fe)
        {
            return new ValueExpression(xf.NameStr() + " - " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator *(XDataField xf, FieldListExpression fe)
        {
            return new ValueExpression(xf.NameStr() + " / " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator /(XDataField xf, FieldListExpression fe)
        {
            return new ValueExpression(xf.NameStr() + " / " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator %(XDataField xf, FieldListExpression fe)
        {
            return new ValueExpression(xf.NameStr() + " % " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator &(XDataField xf, FieldListExpression fe)
        {
            return new ValueExpression(xf.NameStr() + " & " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator |(XDataField xf, FieldListExpression fe)
        {
            return new ValueExpression(xf.NameStr() + " | " + fe.ValueExpressionStr());
        }

        ///ValueExpression = XDataField + ValueExpression
        ///ValueExpression = XDataField - ValueExpression
        ///ValueExpression = XDataField * ValueExpression
        ///ValueExpression = XDataField / ValueExpression
        ///ValueExpression = XDataField % ValueExpression        
        ///ValueExpression = XDataField & ValueExpression        
        ///ValueExpression = XDataField | ValueExpression        
        public static ValueExpression operator +(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " + " + ve.ValueG);
        }
        public static ValueExpression operator -(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " - " + ve.ValueG);
        }
        public static ValueExpression operator *(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " / " + ve.ValueG);
        }
        public static ValueExpression operator /(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " / " + ve.ValueG);
        }
        public static ValueExpression operator %(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " % " + ve.ValueG);
        }
        public static ValueExpression operator &(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " & " + ve.ValueG);
        }
        public static ValueExpression operator |(XDataField xf, ValueExpression ve)
        {
            return new ValueExpression(xf.NameStr() + " | " + ve.Value);
        }

        /// object == XDataField  ConditionUnit        
        /// object > XDataField  ConditionUnit        
        /// object >= XDataField  ConditionUnit        
        /// object != XDataField  ConditionUnit        
        /// object < XDataField  ConditionUnit        
        /// object <= XDataField  ConditionUnit        
        public static ConditionUnit operator ==(object value, XDataField dataField)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(Convert.ToString(value) + "=" + dataField.NameStr());
        }
        public static ConditionUnit operator >(object value, XDataField dataField)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(Convert.ToString(value) + ">" + dataField.NameStr());
        }
        public static ConditionUnit operator >=(object value, XDataField dataField)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(Convert.ToString(value) + ">=" + dataField.NameStr());
        }
        public static ConditionUnit operator !=(object value, XDataField dataField)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(Convert.ToString(value) + "!=" + dataField.NameStr());
        }
        public static ConditionUnit operator <(object value, XDataField dataField)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(Convert.ToString(value) + "<" + dataField.NameStr());
        }
        public static ConditionUnit operator <=(object value, XDataField dataField)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(Convert.ToString(value) + "<=" + dataField.NameStr());
        }

        /// XDataField == object  ConditionUnit
        /// XDataField > object  ConditionUnit
        /// XDataField >= object  ConditionUnit
        /// XDataField != object  ConditionUnit
        /// XDataField < object  ConditionUnit
        /// XDataField <= object  ConditionUnit
        public static ConditionUnit operator ==(XDataField dataField, object value)
        {
            //if (string.IsNullOrEmpty(Convert.ToString(value)))
            //{
            //    return null;
            //}
            if (value == Convert.DBNull)
                return new ConditionUnit(dataField.NameStr() + " IS NULL");
            else
                return new ConditionUnit(dataField.NameStr() + "=" + dataField.ValueStr(value));
        }
        public static ConditionUnit operator >(XDataField dataField, object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(dataField.NameStr() + ">" + dataField.ValueStr(value));
        }
        public static ConditionUnit operator >=(XDataField dataField, object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(dataField.NameStr() + ">=" + dataField.ValueStr(value));
        }
        public static ConditionUnit operator !=(XDataField dataField, object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(dataField.NameStr() + "<>" + dataField.ValueStr(value));
        }
        public static ConditionUnit operator <(XDataField dataField, object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(dataField.NameStr() + "<" + dataField.ValueStr(value));
        }
        public static ConditionUnit operator <=(XDataField dataField, object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return null;
            }
            return new ConditionUnit(dataField.NameStr() + "<=" + dataField.ValueStr(value));
        }

        /// XDataField == ValueExpression  ConditionUnit
        /// XDataField > ValueExpression  ConditionUnit
        /// XDataField >= ValueExpression  ConditionUnit
        /// XDataField != ValueExpression  ConditionUnit
        /// XDataField < ValueExpression  ConditionUnit
        /// XDataField <= ValueExpression  ConditionUnit
        public static ConditionUnit operator ==(XDataField dataField, ValueExpression ve)
        {
            return new ConditionUnit(dataField.NameStr() + "=" + ve.Value);
        }
        public static ConditionUnit operator >(XDataField dataField, ValueExpression ve)
        {
            return new ConditionUnit(dataField.NameStr() + ">" + ve.Value);
        }
        public static ConditionUnit operator >=(XDataField dataField, ValueExpression ve)
        {
            return new ConditionUnit(dataField.NameStr() + ">=" + ve.Value);
        }
        public static ConditionUnit operator !=(XDataField dataField, ValueExpression ve)
        {
            return new ConditionUnit(dataField.NameStr() + "<>" + ve.Value);
        }
        public static ConditionUnit operator <(XDataField dataField, ValueExpression ve)
        {
            return new ConditionUnit(dataField.NameStr() + "<" + ve.Value);
        }
        public static ConditionUnit operator <=(XDataField dataField, ValueExpression ve)
        {
            return new ConditionUnit(dataField.NameStr() + "<=" + ve.Value);
        }

        /// XDataField == XDataField  ConditionUnit
        /// XDataField > XDataField  ConditionUnit
        /// XDataField >= XDataField  ConditionUnit
        /// XDataField != XDataField  ConditionUnit
        /// XDataField < XDataField  ConditionUnit
        /// XDataField <= XDataField  ConditionUnit
        public static ConditionUnit operator ==(XDataField xf1, XDataField xf2)
        {
            return new ConditionUnit(xf1.NameStr() + "=" + xf2.NameStr());
        }
        public static ConditionUnit operator >(XDataField xf1, XDataField xf2)
        {
            return new ConditionUnit(xf1.NameStr() + ">" + xf2.NameStr());
        }
        public static ConditionUnit operator >=(XDataField xf1, XDataField xf2)
        {
            return new ConditionUnit(xf1.NameStr() + ">=" + xf2.NameStr());
        }
        public static ConditionUnit operator !=(XDataField xf1, XDataField xf2)
        {
            return new ConditionUnit(xf1.NameStr() + "<>" + xf2.NameStr());
        }
        public static ConditionUnit operator <(XDataField xf1, XDataField xf2)
        {
            return new ConditionUnit(xf1.NameStr() + "<" + xf2.NameStr());
        }
        public static ConditionUnit operator <=(XDataField xf1, XDataField xf2)
        {
            return new ConditionUnit(xf1.NameStr() + "<=" + xf2.NameStr());
        }

        ///XDataField + XDataField = FieldsExpression
        ///XDataField + FieldsExpression = FieldsExpression
        public static FieldListExpression operator +(XDataField xf1, XDataField xf2)      //+
        {
            FieldListExpression fe = new FieldListExpression(String.Empty);
            fe.Add(xf1);
            fe.Add(xf2);
            return fe;
        }
        public static FieldListExpression operator +(XDataField xf, FieldListExpression fe)  //+
        {
            fe.Add(xf);
            return fe;
        }      
        ///XDataField-- = ValueExpression
        ///XDataField++ = ValueExpression
        public static XDataField operator --(XDataField dataField)
        {
            dataField.set_Int_Decrease();
            return dataField;
        }
        public static XDataField operator ++(XDataField dataField)
        {
            dataField.set_Int_Increase();
            return dataField;
        }        
        #endregion

        #region 6.UI部分
        public void Read(HtmlInputText input)
        {
            this.value = input.Value;
        }
        public void Read(HtmlTextArea area)
        {
            this.value = area.Value;
        }
        /// <summary>
        /// 0-以逗号为分隔符的字符串
        /// 1-位运算整数
        /// </summary>
        /// <param name="cbl">CheckBoxList控件</param>
        /// <param name="Mode">读取模式</param>
        public void Read(CheckBoxList cbl, int Mode)
        {
            if (Mode == 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListItem li in cbl.Items)
                {
                    if (li.Selected)
                    {
                        if (sb.Length > 0) sb.Append(",");
                        sb.Append(li.Value);
                    }
                    this.value = sb.ToString();
                }
            }
            else if (Mode == 1)
            {
                int iF = 0;
                foreach (ListItem li in cbl.Items)
                {
                    if (li.Selected)
                    {
                        iF |= Convert.ToInt32(li.Value);
                    }
                    this.value = iF;
                }
            }
        }
        public void Read(Label lb)
        {
            this.value = lb.Text;
        }
        public void Read(Literal lt)
        {
            this.value = lt.Text;
        }
        public void Read(TextBox tb)
        {
            this.value = tb.Text;
        }
        public void Write(HtmlInputText input)
        {
            input.Value = this.f("{0}");
        }
        public void Write(HtmlTextArea area)
        {
            area.Value = this.f("{0}");
        }
        public void Write(Label lb)
        {
            lb.Text = this.f("{0}");
        }
        public void Write(Literal lt)
        {
            lt.Text = this.f("{0}");
        }
        public void Write(TextBox tb)
        {
            tb.Text = this.f("{0}");
        }
        #endregion

    }
}

