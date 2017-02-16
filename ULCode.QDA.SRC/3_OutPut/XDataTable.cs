namespace ULCode.QDA
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.IO;
    public class XDataTable
    {
        private DataTable oDataTable;
        #region //输入参数(DataSet,DataTable,String[],XmlString)
        public XDataTable(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                this.oDataTable = ds.Tables[0];
            }
        }
        public XDataTable(DataTable dt)
        {
            this.oDataTable = dt;
        }
        public XDataTable(string[] aData)
        {
            this.LoadDataArr(aData, ",");
        }
        public XDataTable(string[] aData, string sSplitter)
        {
            this.LoadDataArr(aData, sSplitter);
        }
        public XDataTable(string[] aData, string sSplitter, string[] aHeader, string[] aHeaderType)
        {
            this.LoadDataArr(aData, sSplitter, aHeader, aHeaderType);
        }
        private void LoadDataArr(string[] aData, string sSplitter)
        {
            int i;
            DataTable dt = new DataTable();
            string[] aHeader = aData[0].Split(new string[] { sSplitter }, StringSplitOptions.None);
            for (i = 0; i < aHeader.Length; i++)
            {
                dt.Columns.Add(aHeader[i]);
            }
            for (i = 1; i < aData.Length; i++)
            {
                string[] aHeader1 = aData[i].Split(new string[] { sSplitter }, StringSplitOptions.None);
                DataRow dr = dt.Rows.Add(aHeader1);
            }
            this.oDataTable = dt;
        }
        private void LoadDataArr(string[] aData, string sSplitter, string sHeaderList, string sHeaderTypeList)
        {
            int i;
            string[] aHeader = sHeaderList.Split(new char[] { ',' });
            string[] aHeaderType = sHeaderTypeList.Split(new char[] { ',' });
            DataTable dt = new DataTable();
            for (i = 0; i < aHeader.Length; i++)
            {
                DataColumn dc = dt.Columns.Add(aHeader[i], Type.GetType(aHeaderType[i]));
            }
            for (i = 0; i < aData.Length; i++)
            {
                string[] aHeader1 = aData[i].Split(new string[] { sSplitter }, StringSplitOptions.None);
                DataRow dr = dt.Rows.Add(aHeader1);
            }
            this.oDataTable = dt;
        }
        private void LoadDataArr(string[] aData, string sSplitter, string[] aHeader, string[] aHeaderType)
        {
            int i;
            DataTable dt = new DataTable();
            for (i = 0; i < aHeader.Length; i++)
            {
                DataColumn dc = dt.Columns.Add(aHeader[i], Type.GetType(aHeaderType[i]));
            }
            for (i = 0; i < aData.Length; i++)
            {
                string[] aHeader1 = aData[i].Split(new string[] { sSplitter }, StringSplitOptions.None);
                DataRow dr = dt.Rows.Add(aHeader1);
            }
            this.oDataTable = dt;
        }
        public XDataTable(String xmlString) : this(xmlString, null) { ; }
        public XDataTable(String xmlString, Encoding encode)
        {
            if (encode == null) encode = new UTF8Encoding();
            byte[] bytes = encode.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(bytes);
            DataTable dt = new DataTable();
            dt.ReadXml(ms);
            this.oDataTable = dt;
        }

        #endregion

        #region //列设置
        /// <summary>
        /// Add Identity Column:添加新标识列。添加新列后，值依次设置为1到N。
        /// 如：xdt.ai("id");  添加新列id,并赋值1到N
        /// </summary>
        /// <param name="newColumnName">新列名</param>
        /// <returns></returns>
        public int ai(string newColumnName)
        {
            if (this.oDataTable != null)
            {
                if (this.oDataTable.Columns.Contains(newColumnName))
                {
                    return -1;
                }
                this.oDataTable.Columns.Add(newColumnName);
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    this.oDataTable.Rows[i][newColumnName] = i + 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// Set Names:设置列名与标题
        /// </summary>
        /// <param name="sNames"></param>
        /// <param name="sCaptions"></param>
        /// <returns></returns>
        public int sn(string sNames, string sCaptions)
        {
            if (this.oDataTable != null)
            {
                int i;
                DataTable dt = this.oDataTable;
                if (dt == null)
                {
                    return 1;
                }
                string[] aName = sNames.Split(new char[] { ',' });
                string[] aCaption = sCaptions.Split(new char[] { ',' });
                for (i = 0; i < aName.Length; i++)
                {
                    if (aName[i].Contains("="))
                    {
                        string[] aNameList = aName[i].Split(new char[] { '=' });
                        string sOName = aNameList[0];
                        dt.Columns[sOName].ColumnName = aNameList[1];
                    }
                    else if (aName[i] != "")
                    {
                        dt.Columns[i].ColumnName = aName[i];
                    }
                }
                for (i = 0; i < aCaption.Length; i++)
                {
                    if ((aCaption.Length > (i + 1)) && aCaption[i].Contains("="))
                    {
                        string[] aCaptionList = aCaption[i].Split(new char[] { '=' });
                        string sOCaption = aCaptionList[0];
                        dt.Columns[sOCaption].Caption = aCaptionList[1];
                    }
                    else if ((aCaption.Length > (i + 1)) && (aCaption[i] != ""))
                    {
                        dt.Columns[i].Caption = aCaption[i];
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// Set Columns : 添加或删除列
        /// .sc("+Title")   //添加列Title
        /// .sc("-Title")   //删除列Title
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public int sc(string colName)
        {
            if (this.oDataTable != null)
            {
                if (!(colName.Contains(",") || (!colName.StartsWith("+") && !colName.StartsWith("-"))))
                {
                    if (colName.StartsWith("+"))
                    {
                        this.oDataTable.Columns.Add(colName.Substring(1));
                    }
                    else
                    {
                        this.oDataTable.Columns.Remove(colName.Substring(1));
                    }
                }
                else
                {
                    int i;
                    string[] aNames = colName.Split(new char[] { ',' });
                    for (i = 0; i < aNames.Length; i++)
                    {
                        if (!this.oDataTable.Columns.Contains(aNames[i]))
                        {
                            this.oDataTable.Columns.Add(aNames[i]);
                        }
                    }
                    colName = "," + colName + ",";
                    for (i = this.oDataTable.Columns.Count - 1; i >= 1; i--)
                    {
                        if (!colName.Contains("," + this.oDataTable.Columns[i].ColumnName + ","))
                        {
                            this.oDataTable.Columns.Remove(this.oDataTable.Columns[i].ColumnName);
                        }
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 添加多列
        /// </summary>
        /// <param name="colNames"></param>
        /// <returns></returns>
        public int AddColumns(String colNames)
        {
            int i;
            string[] aNames = colNames.Split(new char[] { ',', '|', ';' });
            for (i = 0; i < aNames.Length; i++)
            {
                if (!this.oDataTable.Columns.Contains(aNames[i]))
                {
                    this.oDataTable.Columns.Add(aNames[i]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 移除多列
        /// </summary>
        /// <param name="colNames"></param>
        /// <returns></returns>
        public int RemoveColumns(String colNames)
        {
            int i;
            string[] aNames = colNames.Split(new char[] { ',', '|', ';' });
            for (i = 0; i < aNames.Length; i++)
            {
                if (!this.oDataTable.Columns.Contains(aNames[i]))
                {
                    this.oDataTable.Columns.Remove(aNames[i]);
                }
            }
            return 0;
        }
        #endregion

        #region //整列值设置
        /// <summary>
        /// Copy Column:复制新列
        /// </summary>
        /// <param name="srColumnName">复制源列,此列若没有，则会返回-1.</param>
        /// <param name="deColumnName">复制目标列，此列可以为新列，也可以是已有的列</param>
        /// <returns></returns>
        public int cc(string srColumnName, string deColumnName)
        {
            if (this.oDataTable != null)
            {
                if (!this.oDataTable.Columns.Contains(srColumnName))
                {
                    return -1;
                }
                if (!this.oDataTable.Columns.Contains(deColumnName))
                {
                    this.oDataTable.Columns.Add(deColumnName);
                }
                
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    this.oDataTable.Rows[i][deColumnName] = this.oDataTable.Rows[i][srColumnName];
                }
            }
            return 0;
        }
        /// <summary>
        /// 计算列值
        /// </summary>
        /// <param name="columnName">列名，不存在则返回-1</param>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public int compute(string columnName, string sValue)
        {
            return this.compute(columnName, sValue, null);
        }
        /// <summary>
        /// 计算列值
        /// </summary>
        /// <param name="columnName">列名，，不存在则返回-1</param>
        /// <param name="expression">表达式</param>
        /// <param name="filter">筛选器</param>
        /// <returns></returns>
        public int compute(string columnName, string expression, string filter)
        {
            if (this.oDataTable != null)
            {
                if (!this.oDataTable.Columns.Contains(columnName))
                {
                    return -1;
                }
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    string sF;
                    if (String.IsNullOrEmpty(filter))
                    {
                        sF = this.oDataTable.Columns[0].ColumnName + "=" + this.oDataTable.Rows[i][0];
                    }
                    else
                    {
                        sF = filter;
                    }
                    this.oDataTable.Rows[i][columnName] = this.oDataTable.Compute(expression, sF);
                }
            }
            return 0;
        }
        /// <summary>
        /// Filter: 过滤标签
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public int f(string columnName)
        {
            if (this.oDataTable != null)
            {
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    object oF = this.oDataTable.Rows[i][columnName];
                    if ((oF != null) && (oF != Convert.DBNull))
                    {
                        string sF = Convert.ToString(oF);
                        this.oDataTable.Rows[i][columnName] = this.FilteredHtml(sF);
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// --过滤Html标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string FilteredHtml(string html)
        {
            Regex regex = new Regex("<[^>]+>|]+>");
            return regex.Replace(html, "");
        }
        /// <summary>
        /// GetValue:取值
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="sList"></param>
        /// <returns></returns>
        public int g(string columnName, string sList)
        {
            return this.g(columnName, sList, string.Empty, string.Empty);
        }
        public int g(string columnName, string sList, object nullValue)
        {
            return this.g(columnName, sList, nullValue, nullValue);
        }
        public int g(string columnName, string sList, object nullValue, object dbNullValue)
        {
            if (this.oDataTable != null)
            {
                string[] aList = sList.Split(new String[] { ",", ";", "|" }, StringSplitOptions.None);
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    object oF = this.oDataTable.Rows[i][columnName];
                    object oNew = oF;
                    if (oF == null)
                    {
                        oNew = nullValue;
                    }
                    else if (oF == Convert.DBNull)
                    {
                        oNew = dbNullValue;
                    }
                    else
                    {
                        string sF = Convert.ToString(oF);
                        for (int j = 0; j < aList.Length; j += 2)
                        {
                            if (aList[j] == sF)
                            {
                                oNew = aList[j + 1];
                                break;
                            }
                        }
                    }
                    this.oDataTable.Rows[i][columnName] = oNew;
                }
            }
            return 0;
        }
        /// <summary>
        /// --获取文字的真正长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="sBack"></param>
        /// <returns></returns>
        private string GetFixedLenOfString(string str, int len, string sBack)
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
        /// <summary>
        /// 空值处理
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="isNullValue"></param>
        /// <returns></returns>
        public int IsNull(string columnNameList, object isNullValue)
        {
            return this.IsNull(columnNameList, isNullValue, isNullValue, null);
        }
        /// <summary>
        /// 空值处理
        /// </summary>
        /// <param name="columnNameList"></param>
        /// <param name="isNullValue"></param>
        /// <param name="isDbNullValue"></param>
        /// <returns></returns>
        public int IsNull(string columnNameList, object isNullValue, object isDbNullValue)
        {
            return this.IsNull(columnNameList, isNullValue, isDbNullValue, null);
        }
        /// <summary>
        /// 空值处理
        /// </summary>
        /// <param name="columnNameList"></param>
        /// <param name="isNullValue"></param>
        /// <param name="isDbNullValue"></param>
        /// <param name="isEmptyValue"></param>
        /// <returns></returns>
        public int IsNull(string columnNameList, object isNullValue, object isDbNullValue, object isEmptyValue)
        {
            if (this.oDataTable != null)
            {
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < this.oDataTable.Columns.Count; j++)
                    {
                        if ((columnNameList == "*") || ("," + columnNameList.ToLower() + ",").Contains("," + this.oDataTable.Columns[j].ColumnName.ToLower() + ","))
                        {
                            object o = this.oDataTable.Rows[i][j];
                            if ((isNullValue != null) && (o == null))
                            {
                                this.oDataTable.Rows[i][j] = isNullValue;
                            }
                            else if ((isDbNullValue != null) && (o == Convert.DBNull))
                            {
                                this.oDataTable.Rows[i][j] = isDbNullValue;
                            }
                            else if ((isEmptyValue != null) && string.IsNullOrEmpty(Convert.ToString(o)))
                            {
                                this.oDataTable.Rows[i][j] = isEmptyValue;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// Limit Length:限制长度
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="iDataLen"></param>
        /// <returns></returns>
        public int l(string columnName, int iDataLen)
        {
            return this.l(columnName, iDataLen, "..");
        }
        /// <summary>
        /// Limit Length:限制列的长度
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="iDataLen"></param>
        /// <param name="sBack"></param>
        /// <returns></returns>
        public int l(string columnName, int iDataLen, string sBack)
        {
            if (this.oDataTable == null)
            {
                return 1;
            }
            if (this.oDataTable.Columns.IndexOf(columnName) == -1)
            {
                this.oDataTable.Columns.Add(columnName);
            }
            for (int i = 0; i < this.oDataTable.Rows.Count; i++)
            {
                string sValueTemp = Convert.ToString(this.oDataTable.Rows[i][columnName]);
                if (!string.IsNullOrEmpty(sValueTemp))
                {
                    this.oDataTable.Rows[i][columnName] = this.GetFixedLenOfString(sValueTemp, iDataLen, sBack);
                }
            }
            return 0;
        }

        /// <summary>
        /// Format:整列格式化
        /// 如：.format("NewDate","{3:yyyy-MM-dd}");        将第3列的值格式化赋给列NewDate列
        /// 如：.format("NewDate","{NewsDate:yyyy-MM-dd}"); 将列Title的值格式化赋值给列NewDate列
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public int format(string columnName, string format)
        {
            if (this.oDataTable != null)
            {
                for (int j = 0; j < this.oDataTable.Columns.Count; j++)
                {
                    string sf = format;
                    if (sf.Contains("{"+this.oDataTable.Columns[j].ColumnName))
                    {
                        sf = sf.Replace("{" + this.oDataTable.Columns[j].ColumnName, "{" + Convert.ToString(j));
                    }
                }
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    this.oDataTable.Rows[i][columnName] = String.Format(format,this.oDataTable.Rows[i].ItemArray);
                }
            }
            return 0;
        }
        /// <summary>
        /// Set Format:整列格式化
        /// 如：.sf("FormatNewsTitle","{3}");        
        /// 如：.sf("FormatNewsTitle","{NewsTitle}");
        /// 如：.sf("FormatNewsTitle","@NewsTitle");
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public int sf(string columnName, string format)
        {
            if (this.oDataTable != null)
            {
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    string sf = format;
                    for (int j = 0; j < this.oDataTable.Columns.Count; j++)
                    {
                        string sn = this.oDataTable.Columns[j].ColumnName;
                        if (sf.Contains("{" + j + "}")
                            || sf.Contains("@" + sn)
                            || sf.Contains("{" + sn + "}"))
                        {
                            string sv;
                            object ov = this.oDataTable.Rows[i][j];
                            if ((ov == null) || (ov == Convert.DBNull))
                            {
                                sv = Convert.ToString(ov);
                            }
                            else
                            {
                                sv = string.Empty;
                            }
                            sf = sf.Replace("@" + sn, sv)
                                   .Replace("{" + j + "}", sv)
                                   .Replace("{" + sn + "}", sv);
                        }
                    }
                    this.oDataTable.Rows[i][columnName] = sf;
                }
            }
            return 0;
        }
        /// <summary>
        /// 设置列值
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public int sv(string columnName, string format,string filter)
        {
            if (this.oDataTable != null)
            {
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    string sf = format;
                    for (int j = 0; j < this.oDataTable.Columns.Count; j++)
                    {
                        string sn = this.oDataTable.Columns[j].ColumnName;
                        if (sf.Contains("{" + j + "}")
                            || sf.Contains("@" + sn)
                            || sf.Contains("{" + sn + "}"))
                        {
                            string sv;
                            object ov = this.oDataTable.Rows[i][j];
                            if ((ov == null) || (ov == Convert.DBNull))
                            {
                                sv = Convert.ToString(ov);
                            }
                            else
                            {
                                sv = string.Empty;
                            }
                            sf = sf.Replace("@" + sn, sv)
                                   .Replace("{" + j + "}", sv)
                                   .Replace("{" + sn + "}", sv);
                        }
                    }
                    this.oDataTable.Rows[i][columnName] = this.oDataTable.Compute(sf, filter).ToString();
                }
            }
            return 0;
        }
        public int TrimInner(string columnName)
        {
            if (this.oDataTable != null)
            {
                if (this.oDataTable.Columns.Contains(columnName))
                {
                    return -1;
                }
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    this.oDataTable.Rows[i][columnName] = Convert.ToString(this.oDataTable.Rows[i][columnName]).Replace(" ", string.Empty);
                }
            }
            return 0;
        }
        public int TrimSide(string columnName)
        {
            if (this.oDataTable != null)
            {
                if (this.oDataTable.Columns.Contains(columnName))
                {
                    return -1;
                }
                for (int i = 0; i < this.oDataTable.Rows.Count; i++)
                {
                    this.oDataTable.Rows[i][columnName] = Convert.ToString(this.oDataTable.Rows[i][columnName]).Trim();
                }
            }
            return 0;
        }        
        /// <summary>
        /// 将所有DBNull换成Null值
        /// </summary>
        public void FilterDbNull()
        {
            DataTable dt = this.oDataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] == Convert.DBNull)
                    {
                        dt.Rows[i][j] = null;
                    }
                }
            }
        }
        #endregion

        #region //输出其它集合
        /// <summary>
        /// 两个表进行迭加
        /// </summary>
        /// <param name="table1">表1</param>
        /// <param name="table2">表2</param>
        /// <returns></returns>
        public static XDataTable operator +(XDataTable table1, XDataTable table2)
        {
            int i;
            DataTable dt = table1.ToDataTable().Clone();
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            DataTable dt1 = table1.ToDataTable();
            DataTable dt2 = table2.ToDataTable();
            for (i = 0; i < dt1.Rows.Count; i++)
            {
                dt.Rows.Add(dt1.Rows[i].ItemArray);
            }
            for (i = 0; i < dt2.Rows.Count; i++)
            {
                try
                {
                    dt.Rows.Add(dt2.Rows[i].ItemArray);
                }
                catch
                {
                }
            }
            return new XDataTable(dt);
        }
        /// <summary>
        /// 生成HtmlTable
        /// </summary>
        /// <returns></returns>
        public string HtmlTable()
        {
            return this.HtmlTable(string.Empty, string.Empty);
        }
        /// <summary>
        /// 生成HtmlTable
        /// </summary>
        /// <param name="sColumnTitleList">标题列表</param>
        /// <param name="sColumnWidthList">宽度列表</param>
        /// <returns></returns>
        public string HtmlTable(string sColumnTitleList, string sColumnWidthList)
        {
            int i;
            DataTable dt = this.oDataTable;
            string[] aTitleList = sColumnTitleList.Split(new char[] { ',', ';', '|' });
            string[] aWidthList = sColumnWidthList.Split(new char[] { ',', ';', '|' });
            string strHtml = "<table border='1' style='width:95%' cellspacing='0'>";
            strHtml = strHtml + "<tr>";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                string sCaption = dt.Columns[i].Caption;
                if ((i < aTitleList.Length) && (sColumnTitleList != string.Empty))
                {
                    sCaption = aTitleList[i];
                }
                string sWidth = "";
                if ((i < aWidthList.Length) && (sColumnWidthList != string.Empty))
                {
                    sWidth = aWidthList[i];
                }
                if (sWidth != string.Empty)
                {
                    sWidth = "width:" + sWidth;
                }
                string sT = strHtml;
                strHtml = sT + "<th style='" + sWidth + "'>" + sCaption + "</th>";
            }
            strHtml = strHtml + "</tr>";
            for (i = 0; i < dt.Rows.Count; i++)
            {
                strHtml = strHtml + "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    object o = dt.Rows[i][j];
                    if (o == Convert.DBNull)
                    {
                        o = null;
                    }
                    string s = Convert.ToString(o);
                    if (s.Length > 50)
                    {
                        s = s.Substring(0, 20) + "..";
                    }
                    strHtml = strHtml + "<td>&nbsp;" + s + "</td>";
                }
                strHtml = strHtml + "</tr>";
            }
            return (strHtml + "</table>");
        }
        /// <summary>
        /// 输出范围集合
        /// </summary>
        /// <param name="iBegin"></param>
        /// <returns></returns>
        public DataTable ScopeToDataTable(int iBegin)
        {
            if (this.oDataTable == null)
            {
                return null;
            }
            return this.ScopeToDataTable(iBegin, this.oDataTable.Rows.Count);
        }
        public DataTable ScopeToDataTable(int iBegin, int iEnd)
        {
            DataTable dt1;
            int i;
            if (this.oDataTable == null)
            {
                return null;
            }
            if ((iBegin > iEnd) || (this.oDataTable == null))
            {
                return null;
            }
            if ((iBegin <= 0) || (iBegin > this.oDataTable.Rows.Count))
            {
                iBegin = 1;
            }
            if ((iEnd <= 0) || (iEnd > this.oDataTable.Rows.Count))
            {
                iEnd = this.oDataTable.Rows.Count;
            }
            if ((((iEnd - iBegin) + 1) <= 10) || ((this.oDataTable.Rows.Count / ((iEnd - iBegin) + 1)) > 2))
            {
                dt1 = this.oDataTable.Clone();
                for (i = iBegin - 1; (i < iEnd) && (i < this.oDataTable.Rows.Count); i++)
                {
                    dt1.Rows.Add(this.oDataTable.Rows[i].ItemArray);
                }
                return dt1;
            }
            dt1 = this.oDataTable.Copy();
            for (i = 0; i < iBegin; i++)
            {
                dt1.Rows[i].Delete();
            }
            for (i = this.oDataTable.Rows.Count; i > (iEnd - 1); i--)
            {
                dt1.Rows[i].Delete();
            }
            for (i = iBegin - 2; i >= 0; i--)
            {
                dt1.Rows[i].Delete();
            }
            return dt1;
        }
        public DataRow[] Select(string selectString)
        {
            if (this.oDataTable == null)
            {
                return null;
            }
            return this.oDataTable.Select(selectString);
        }
        public DataTable SelectToDataTable(string selectString)
        {
            int i;
            if (this.oDataTable == null)
            {
                return null;
            }
            if (selectString == string.Empty)
            {
                return this.oDataTable;
            }
            if (this.oDataTable == null)
            {
                return null;
            }
            DataRow[] dr = this.oDataTable.Select(selectString);
            DataTable dt1 = new DataTable();
            for (i = 0; i < this.oDataTable.Columns.Count; i++)
            {
                DataColumn dc = new DataColumn(this.oDataTable.Columns[i].ColumnName);
                dc.Caption = this.oDataTable.Columns[i].Caption;
                dt1.Columns.Add(dc);
            }
            for (i = 0; i < dr.Length; i++)
            {
                dt1.ImportRow(dr[i]);
            }
            return dt1;
        }
        public DataSet ToDataSet()
        {
            if (this.oDataTable == null)
            {
                return null;
            }
            DataTable dt = this.oDataTable;
            if (dt.DataSet == null)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
            }
            return dt.DataSet;
        }
        public DataTable ToDataTable()
        {
            return this.oDataTable;
        }
        public DataTable TopToDataTable(int iTop)
        {
            int i;
            if (this.oDataTable == null)
            {
                return null;
            }
            DataTable dt1 = new DataTable();
            for (i = 0; i < this.oDataTable.Columns.Count; i++)
            {
                DataColumn dc = new DataColumn(this.oDataTable.Columns[i].ColumnName);
                dc.Caption = this.oDataTable.Columns[i].Caption;
                dt1.Columns.Add(dc);
            }
            for (i = 0; (i < iTop) && (i < this.oDataTable.Rows.Count); i++)
            {
                dt1.Rows.Add(this.oDataTable.Rows[i].ItemArray);
            }
            return dt1;
        }
        /// <summary>
        /// 输出ArrayList
        /// </summary>
        /// <returns></returns>
        public ArrayList ToArrayList()
        {
            ArrayList oArr = new ArrayList();
            DataTable dt = this.oDataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    oArr.Add(dt.Rows[i][j]);
                }
            }
            return oArr;
        }
        /// <summary>
        /// 形成数据库
        /// </summary>
        /// <returns></returns>
        public string ToCellValueList()
        {
            return this.ToCellValueList(",", "|");
        }
        public string ToCellValueList(string sSpliter_Col, string sSpliter_Row)
        {
            string sList = string.Empty;
            DataTable dt = this.oDataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sListPerRow = string.Empty;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sListPerRow = sListPerRow + ((j == 0) ? string.Empty : sSpliter_Col) + Convert.ToString(dt.Rows[i][j]);
                }
                sList = sList + ((i == 0) ? string.Empty : sSpliter_Row) + sListPerRow;
            }
            return sList;
        }
        public Dictionary<string, object> ToColDict()
        {
            return this.ToColDict(0, 1);
        }
        public Dictionary<string, object> ToColDict(int iNameCol, int iValueCol)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DataTable dt = this.oDataTable;
            if ((((iValueCol < 0) || (iValueCol > (dt.Columns.Count - 1))) || (iNameCol < 0)) || (iNameCol > (dt.Columns.Count - 1)))
            {
                return null;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dic.Add(Convert.ToString(dt.Rows[i][iNameCol]), dt.Rows[i][iValueCol]);
            }
            return dic;
        }
        public NameValueCollection ToColNameValueCollection()
        {
            return this.ToColNameValueCollection(0, 1);
        }
        public NameValueCollection ToColNameValueCollection(int iNameCol, int iValueCol)
        {
            NameValueCollection dic = new NameValueCollection();
            DataTable dt = this.oDataTable;
            if ((((iValueCol < 0) || (iValueCol > (dt.Columns.Count - 1))) || (iNameCol < 0)) || (iNameCol > (dt.Columns.Count - 1)))
            {
                return null;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dic.Add(Convert.ToString(dt.Rows[i][iNameCol]), Convert.ToString(dt.Rows[i][iValueCol]));
            }
            return dic;
        }
        public StringCollection ToColStringCollection()
        {
            return this.ToColStringCollection(0);
        }
        public StringCollection ToColStringCollection(int iCol)
        {
            StringCollection dic = new StringCollection();
            DataTable dt = this.oDataTable;
            if ((iCol < 0) || (iCol > (dt.Columns.Count - 1)))
            {
                return null;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dic.Add(Convert.ToString(dt.Rows[i][iCol]));
            }
            return dic;
        }
        public string ToColValueList()
        {
            return this.ToColValueList(",", 0);
        }
        public string ToColValueList(string sSpliter)
        {
            return this.ToColValueList(sSpliter, 0);
        }
        public string ToColValueList(string sSpliter, int iCol)
        {
            string sList = string.Empty;
            DataTable dt = this.oDataTable;
            if (dt == null) return null;
            if ((iCol < 0) || (iCol > (dt.Columns.Count - 1)))
            {
                return string.Empty;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sList = sList + sSpliter + Convert.ToString(dt.Rows[i][iCol]);
            }
            if (sList != string.Empty)
            {
                sList = sList.Substring(sSpliter.Length);
            }
            return sList;
        }
        public string ToColValueList(string sSpliter, string sCol)
        {
            string sList = string.Empty;
            DataTable dt = this.oDataTable;
            if (dt == null) return null;
            if (!dt.Columns.Contains(sCol))
            {
                return string.Empty;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sList = sList + sSpliter + Convert.ToString(dt.Rows[i][sCol]);
            }
            if (sList != string.Empty)
            {
                sList = sList.Substring(sSpliter.Length);
            }
            return sList;
        }
        public DataRow ToDataRow()
        {
            DataTable dt = this.oDataTable;
            if (dt == null) return null;
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return dt.Rows[0];
        }
        public DataRow ToDataRow(int iRow)
        {
            DataTable dt = this.oDataTable;
            if (dt == null) return null;
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            if ((iRow < 0) || (iRow > (dt.Rows.Count - 1)))
            {
                return null;
            }
            return dt.Rows[iRow];
        }
        public DataRow[] ToDataRows()
        {
            return this.oDataTable.Select();
        }
        public DataRow[] ToDataRows(string filter)
        {
            return this.oDataTable.Select(filter);
        }
        public DataRow[] ToDataRows(string filter, string sort)
        {
            return this.oDataTable.Select(filter, sort);
        }
        public DataView ToDataView()
        {
            return this.oDataTable.DefaultView;
        }
        public DbValues ToDbValues()
        {
            return new DbValues(this.oDataTable);
        }
        public List<object> ToListObject()
        {
            List<object> oArr = new List<object>();
            DataTable dt = this.oDataTable;
            if (dt == null) return null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    oArr.Add(dt.Rows[i][j]);
                }
            }
            return oArr;
        }
        public object[] ToObjectArray()
        {
            if (this.ToListObject() == null)
            {
                return null;
            }
            return this.ToListObject().ToArray();
        }
        public Dictionary<string, object> ToRowDict()
        {
            return this.ToRowDict(0);
        }
        public Dictionary<string, object> ToRowDict(int iRow)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DataTable dt = this.oDataTable;
            if ((iRow < 0) || (iRow > (dt.Rows.Count - 1)))
            {
                return null;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dic.Add(dt.Columns[i].ColumnName, dt.Rows[iRow][i]);
            }
            return dic;
        }
        public NameValueCollection ToRowNameValueCollection()
        {
            return this.ToRowNameValueCollection(0);
        }
        public NameValueCollection ToRowNameValueCollection(int iRow)
        {
            NameValueCollection dic = new NameValueCollection();
            DataTable dt = this.oDataTable;
            if ((iRow < 0) || (iRow > (dt.Rows.Count - 1)))
            {
                return null;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dic.Add(dt.Columns[i].ColumnName, Convert.ToString(dt.Rows[iRow][i]));
            }
            return dic;
        }
        public StringCollection ToRowStringCollection()
        {
            return this.ToRowStringCollection(0);
        }
        public StringCollection ToRowStringCollection(int iRow)
        {
            StringCollection dic = new StringCollection();
            DataTable dt = this.oDataTable;
            if ((iRow < 0) || (iRow > (dt.Rows.Count - 1)))
            {
                return null;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dic.Add(Convert.ToString(dt.Rows[iRow][i]));
            }
            return dic;
        }
        public string ToRowValueList()
        {
            return this.ToRowValueList(",", 0);
        }
        public string ToRowValueList(string sSpliter)
        {
            return this.ToRowValueList(sSpliter, 0);
        }
        public string ToRowValueList(string sSpliter, int iRow)
        {
            string sList = string.Empty;
            DataTable dt = this.oDataTable;
            if ((iRow < 0) || (iRow > (dt.Rows.Count - 1)))
            {
                return string.Empty;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sList = sList + sSpliter + Convert.ToString(dt.Rows[i][iRow]);
            }
            if (sList != string.Empty)
            {
                sList = sList.Substring(sSpliter.Length);
            }
            return sList;
        }
        #endregion
    }
}

