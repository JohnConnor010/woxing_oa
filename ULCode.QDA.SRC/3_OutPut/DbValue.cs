namespace ULCode.QDA
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class DbValue
    {
        public object oValue = null;

        public DbValue(object value)
        {
            this.oValue = value;
        }

        public string f()
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return string.Empty;
            }
            return ULString.FilteredHtml(Convert.ToString(this.value));
        }
        public string f(string formatString)
        {
            return string.Format(formatString, this.value);
        }
        public string format(string formatString)
        {
            return string.Format(formatString, this.value);
        }

        public string g(string sDictionaryString)
        {
            return this.g(sDictionaryString, ",");
        }

        public string g(string[] aKeys, string[] aValues)
        {
            string sEval;
            if (this.value == null)
            {
                sEval = "$NULL";
            }
            else if (this.value == Convert.DBNull)
            {
                sEval = "$DBNULL";
            }
            else if (Convert.ToString(this.value) == string.Empty)
            {
                sEval = "$EMPTY";
            }
            else
            {
                sEval = Convert.ToString(this.value);
            }
            for (int i = 0; i < aKeys.Length; i++)
            {
                if (sEval == aKeys[i])
                {
                    return aValues[i];
                }
                if (aKeys[i] == "*")
                {
                    return aValues[i];
                }
                if (aKeys[i].ToLower().StartsWith("$REG:") && Regex.IsMatch(sEval, aKeys[i].Substring(6), RegexOptions.IgnoreCase))
                {
                    return aValues[i];
                }
            }
            return string.Empty;
        }

        public string g(string sDictionaryString, string sSplitter)
        {
            string sEval;
            if (this.value == null)
            {
                sEval = "$NULL";
            }
            else if (this.value == Convert.DBNull)
            {
                sEval = "$DBNULL";
            }
            else if (Convert.ToString(this.value) == string.Empty)
            {
                sEval = "$EMPTY";
            }
            else
            {
                sEval = Convert.ToString(this.value);
            }
            string[] aList = sDictionaryString.Split(new string[] { sSplitter }, StringSplitOptions.None);
            for (int i = 0; i < aList.Length; i += 2)
            {
                if ((aList[i] == "$END") || (aList[i] == "*"))
                {
                    return aList[i + 1];
                }
                if (aList[i] == sEval)
                {
                    return aList[i + 1];
                }
                if (aList[i].ToLower().StartsWith("$REG:") && Regex.IsMatch(sEval, aList[i].Substring(5), RegexOptions.IgnoreCase))
                {
                    return aList[i + 1];
                }
            }
            return string.Empty;
        }

        public string getAge()
        {
            return this.getAge(string.Empty);
        }

        public string getAge(string nullReturn)
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return nullReturn;
            }
            return Convert.ToString((int) (DateTime.Now.Year - Convert.ToDateTime(this.value).Year));
        }

        public string l(int iLen)
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return string.Empty;
            }
            return Bind.GetFixedLenOfString(Convert.ToString(this.value), iLen);
        }

        public string l(int iLen, string sBack)
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return "";
            }
            return Bind.GetFixedLenOfString(Convert.ToString(this.value), iLen, sBack);
        }
        public bool isNull()
        {
            return this.value == null || this.value == Convert.DBNull;
        }
        public bool isDbNull()
        {
            return this.value == Convert.DBNull;
        }
        public bool isEmpty()
        {
            return this.value == null || this.value == Convert.DBNull || String.IsNullOrEmpty(Convert.ToString(this.value));
        }
        public bool ToBool()
        {
            return Convert.ToBoolean(this.Value(false));
        }

        public bool ToBool(bool nullValue)
        {
            return Convert.ToBoolean(this.Value(false));
        }

        public bool ToBool(bool nullValue, bool dbNullValue)
        {
            return Convert.ToBoolean(this.Value(false));
        }

        public bool ToBoolean()
        {
            return Convert.ToBoolean(this.Value(false));
        }

        public bool ToBoolean(bool nullValue)
        {
            return Convert.ToBoolean(this.Value(false));
        }

        public bool ToBoolean(bool nullValue, bool dbNullValue)
        {
            return Convert.ToBoolean(this.Value(false));
        }

        public byte ToByte()
        {
            return Convert.ToByte(this.Value(0));
        }

        public byte ToByte(byte nullValue)
        {
            return Convert.ToByte(this.Value(nullValue));
        }

        public byte ToByte(byte nullValue, byte dbNullValue)
        {
            return Convert.ToByte(this.Value(nullValue, dbNullValue));
        }

        public char ToChar()
        {
            return Convert.ToChar(this.Value('\0'));
        }

        public char ToChar(char nullValue)
        {
            return Convert.ToChar(this.Value(nullValue));
        }

        public char ToChar(char nullValue, char dbNullValue)
        {
            return Convert.ToChar(this.Value(nullValue, dbNullValue));
        }

        public DateTime ToDateTime()
        {
            return Convert.ToDateTime(this.Value(DateTime.MinValue));
        }

        public DateTime ToDateTime(DateTime nullValue)
        {
            return Convert.ToDateTime(this.Value(nullValue));
        }

        public DateTime ToDateTime(DateTime nullValue, DateTime dbNullValue)
        {
            return Convert.ToDateTime(this.Value(nullValue, dbNullValue));
        }

        public decimal ToDecimal()
        {
            return Convert.ToDecimal(this.Value(0));
        }

        public decimal ToDecimal(decimal nullValue)
        {
            return Convert.ToDecimal(this.Value(nullValue));
        }

        public decimal ToDecimal(decimal nullValue, decimal dbNullValue)
        {
            return Convert.ToDecimal(this.Value(nullValue, dbNullValue));
        }

        public double ToDouble()
        {
            return Convert.ToDouble(this.Value(0));
        }

        public double ToDouble(double nullValue)
        {
            return Convert.ToDouble(this.Value(nullValue));
        }

        public double ToDouble(double nullValue, double dbNullValue)
        {
            return Convert.ToDouble(this.Value(nullValue, dbNullValue));
        }

        public int ToInt()
        {
            return Convert.ToInt32(this.Value(0));
        }

        public int ToInt(int nullValue)
        {
            return Convert.ToInt32(this.Value(nullValue));
        }

        public int ToInt(int nullValue, int dbNullValue)
        {
            return Convert.ToInt32(this.Value(nullValue, dbNullValue));
        }

        public short ToInt16()
        {
            return Convert.ToInt16(this.Value(0));
        }

        public short ToInt16(short nullValue)
        {
            return Convert.ToInt16(this.Value(nullValue));
        }

        public short ToInt16(short nullValue, short dbNullValue)
        {
            return Convert.ToInt16(this.Value(nullValue, dbNullValue));
        }

        public int ToInt32()
        {
            return Convert.ToInt32(this.Value(0));
        }

        public int ToInt32(int nullValue)
        {
            return Convert.ToInt32(this.Value(nullValue));
        }

        public int ToInt32(int nullValue, int dbNullValue)
        {
            return Convert.ToInt32(this.Value(nullValue, dbNullValue));
        }

        public long ToInt64()
        {
            return Convert.ToInt64(this.Value(0));
        }

        public long ToInt64(long nullValue)
        {
            return Convert.ToInt64(this.Value(nullValue));
        }

        public long ToInt64(long nullValue, long dbNullValue)
        {
            return Convert.ToInt64(this.Value(nullValue, dbNullValue));
        }

        public long ToLong()
        {
            return Convert.ToInt64(this.Value(0));
        }

        public long ToLong(long nullValue)
        {
            return Convert.ToInt64(this.Value(nullValue));
        }

        public long ToLong(long nullValue, long dbNullValue)
        {
            return Convert.ToInt64(this.Value(nullValue, dbNullValue));
        }

        public short ToShort()
        {
            return Convert.ToInt16(this.Value(0));
        }

        public short ToShort(short nullValue)
        {
            return Convert.ToInt16(this.Value(nullValue));
        }

        public short ToShort(short nullValue, short dbNullValue)
        {
            return Convert.ToInt16(this.Value(nullValue, dbNullValue));
        }

        public float ToSingle()
        {
            return Convert.ToSingle(this.Value(0));
        }

        public float ToSingle(float nullValue)
        {
            return Convert.ToSingle(this.Value(nullValue));
        }

        public float ToSingle(float nullValue, float dbNullValue)
        {
            return Convert.ToSingle(this.Value(nullValue, dbNullValue));
        }

        public string ToStr()
        {
            return Convert.ToString(this.Value(string.Empty));
        }

        public string ToStr(string nullValue)
        {
            return Convert.ToString(this.Value(nullValue));
        }

        public string ToStr(string nullValue, string dbNullValue)
        {
            return Convert.ToString(this.Value(nullValue, dbNullValue));
        }

        public override string ToString()
        {
            return Convert.ToString(this.Value(string.Empty));
        }

        public string ToString(string nullValue)
        {
            return Convert.ToString(this.Value(nullValue));
        }

        public string ToString(string nullValue, string dbNullValue)
        {
            return Convert.ToString(this.Value(nullValue, dbNullValue));
        }

        public ushort ToUInt16()
        {
            return Convert.ToUInt16(this.Value(0));
        }

        public ushort ToUInt16(ushort nullValue)
        {
            return Convert.ToUInt16(this.Value(nullValue));
        }

        public ushort ToUInt16(ushort nullValue, ushort dbNullValue)
        {
            return Convert.ToUInt16(this.Value(nullValue, dbNullValue));
        }

        public uint ToUInt32()
        {
            return Convert.ToUInt32(this.Value(0));
        }

        public uint ToUInt32(uint nullValue)
        {
            return Convert.ToUInt32(this.Value(nullValue));
        }

        public uint ToUInt32(uint nullValue, uint dbNullValue)
        {
            return Convert.ToUInt32(this.Value(nullValue, dbNullValue));
        }

        public ulong ToUInt64()
        {
            return Convert.ToUInt64(this.Value(0));
        }

        public ulong ToUInt64(ulong nullValue)
        {
            return Convert.ToUInt64(this.Value(nullValue));
        }

        public ulong ToUInt64(ulong nullValue, ulong dbNullValue)
        {
            return Convert.ToUInt64(this.Value(nullValue, dbNullValue));
        }

        public string TrimInner()
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return string.Empty;
            }
            return Convert.ToString(this.value).Replace(" ", string.Empty);
        }

        public string TrimSide()
        {
            if ((this.value == null) || (this.value == Convert.DBNull))
            {
                return string.Empty;
            }
            return Convert.ToString(this.value).Trim();
        }

        public object Value()
        {
            return this.oValue;
        }

        public object Value(object nullValue)
        {
            if ((this.oValue == null) || (this.oValue == Convert.DBNull))
            {
                return nullValue;
            }
            return this.oValue;
        }

        public object Value(object nullValue, object dbNullValue)
        {
            if (this.oValue == Convert.DBNull)
            {
                return dbNullValue;
            }
            if (this.oValue == null)
            {
                return nullValue;
            }
            return this.oValue;
        }

        public void Write(HtmlInputText input)
        {
            input.Value = this.ToStr();
        }

        public void Write(HtmlTextArea area)
        {
            area.Value = this.ToStr();
        }

        public void Write(Label lb)
        {
            lb.Text = this.ToStr();
        }

        public void Write(Literal lt)
        {
            lt.Text = this.ToStr();
        }

        public void Write(TextBox tb)
        {
            tb.Text = this.ToStr();
        }

        public void Write(Label lb, string format)
        {
            lb.Text = string.Format(format, this.value);
        }

        public void Write(Literal lt, string format)
        {
            lt.Text = string.Format(format, this.value);
        }

        public void Write(TextBox tb, string format)
        {
            tb.Text = string.Format(format, this.value);
        }

        public object value
        {
            get
            {
                return this.oValue;
            }
        }
    }
}

