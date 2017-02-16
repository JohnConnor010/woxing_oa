namespace ULCode.QDA
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using ULCode;

    public class Bind
    {
        public static int Flag = 599;
        public static string FilteredHtml(string html)
        {
            if (String.IsNullOrEmpty(html)) 
                return String.Empty;
            Regex regex = new Regex("<[^>]+>|]+>");
            return regex.Replace(html, "");
        }

        public string g(string sSql, params object[] oValues)
        {
            return this.g(string.Empty, sSql, string.Empty, oValues);
        }

        public string g(string sCn, string sSql, params object[] oValues)
        {
            return this.g(sCn, sSql, string.Empty, oValues);
        }

        public virtual string g(string sCn, string sSql, string nullValue, params object[] oValues)
        {
            if (sSql == string.Empty)
            {
                return string.Empty;
            }
            if ((oValues.Length == 1) && ((oValues[0] == null) || (oValues[0] == Convert.DBNull)))
            {
                return nullValue;
            }
            for (int i = 0; i < oValues.Length; i++)
            {
                sSql = sSql.Replace("{" + i + "}", Convert.ToString(oValues[i]));
            }
            if (sCn == string.Empty)
            {
                return XSql.GetDB(sCn).GetData(sSql).ToStr();
            }
            return XSql.GetDB(sCn).GetData( sSql).ToStr();
        }

        public static string GetFixedLenOfString(string str, int len)
        {
            return GetFixedLenOfString(str, len, "..");
        }

        public static string GetFixedLenOfString(string str, int len, string sBack)
        {
            string result = string.Empty;
            int byteLen = Encoding.Default.GetByteCount(str);
            int charLen = str.Length;
            int byteCount = 0;
            int pos = 0;
            if (byteLen <= len)
            {
                return str;
            }
            for (int i = 0; i < charLen; i++)
            {
                if (Convert.ToInt32(str.ToCharArray()[i]) > 0xff)
                {
                    byteCount += 2;
                }
                else
                {
                    byteCount++;
                }
                if (byteCount > len)
                {
                    pos = i;
                    break;
                }
                if (byteCount == len)
                {
                    pos = i + 1;
                    break;
                }
            }
            if (pos >= 0)
            {
                result = str.Substring(0, pos) + sBack;
            }
            return result;
        }

        public string gl(string outputFormat, string sSql, params object[] oValues)
        {
            return this.gl(outputFormat, (object)string.Empty, string.Empty, sSql, oValues);
        }

        public string gl(string outputFormat, string sCn, string sSql, params object[] oValues)
        {
            return this.gl(outputFormat, (object)string.Empty, sCn, sSql, oValues);
        }

        public virtual string gl(string outputFormat, object nullValue, string sCn, string sSql, params object[] oValues)
        {
            int i;
            object[] oArr;
            if (sSql == string.Empty)
            {
                return Convert.ToString(nullValue);
            }
            if ((oValues.Length == 1) && ((oValues[0] == null) || (oValues[0] == Convert.DBNull)))
            {
                return Convert.ToString(nullValue);
            }
            for (i = 0; i < oValues.Length; i++)
            {
                sSql = sSql.Replace("{" + i + "}", Convert.ToString(oValues[i]));
            }
            oArr = XSql.GetDB(sCn).GetXDataTable(sSql).ToObjectArray();
            if ((oArr.Length == 0) || ((oArr.Length == 1) && ((oArr[0] == null) || (oArr[0] == Convert.DBNull))))
            {
                return Convert.ToString(nullValue);
            }
            for (i = 0; i < oArr.Length; i++)
            {
                string sSingleValue = string.Empty;
                if (outputFormat.Contains("{" + i + "}"))
                {
                    if ((oArr[i] == null) || (oArr[i] == Convert.DBNull))
                    {
                        sSingleValue = string.Empty;
                    }
                    else
                    {
                        sSingleValue = Convert.ToString(oArr[i]);
                    }
                    outputFormat = outputFormat.Replace("{" + i + "}", sSingleValue);
                }
            }
            return outputFormat;
        }
    }
}

