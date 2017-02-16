namespace ULCode.QDA
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Data;
    public abstract class XModel
    {
        public List<XDataField> Fields;
        private Dictionary<string, XDataField> _DFields = null;
        public Dictionary<string, XDataField> DFields
        {
            get
            {
                if (_DFields == null)
                {
                    _DFields = new Dictionary<string, XDataField>();
                    foreach (XDataField xdf in this.Fields)
                    {
                        _DFields.Add(xdf.name, xdf);
                    }

                }
                return _DFields;
            }
        }
        public bool LoadSucceed = false;
        public XModel()
        {
            this.LoadFields();
            if (this.Fields == null)
            {
                throw new ApplicationException("数据模型，初始化数据字段失败！");
            }
            else 
            {
                foreach (XDataField xdf in this.Fields)
                {
                    xdf.parentMode = this;
                }
            }
        }
        ~XModel()
        {
            if (this.Fields != null)
            {
                this.Fields.Clear();
                this.Fields = null;
            }
        }        
        protected abstract void LoadFields();
        protected void AddFields(params XDataField[] fields)
        {
            if (this.Fields == null)
            {
                this.Fields = new List<XDataField>();
            }
            for (int i = 0; i < fields.Length; i++)
            {
                this.Fields.Add(fields[i]);
            }
        }
        //更新所有Fields的updated,selected,readOnly
        public void ClearAllUpdated() { SetAllFlag("Updated",false); }
        public void ClearAllSelected() { SetAllFlag("Selected", false); }
        public void SelectAll() { SetAllFlag("Selected", true); }
        private void SetAllFlag(string flagName, bool value)
        {
            if (this.Fields != null)
            {
                for (int i = 0; i < this.Fields.Count; i++)
                {
                    if (flagName == "Updated")
                    {
                        if (!value)
                        {
                            this.Fields[i].value_Initial = this.Fields[i].value;
                        }
                        this.Fields[i].updated = value;
                    }
                    else if (flagName == "Selected")
                    {
                        this.Fields[i].selected = value;
                    }
                    else if (flagName == "ReadOnly")
                    {
                        this.Fields[i].readOnly = value;
                    }
                }
            }
        }
        public void RestoreInitial()
        {
            if (this.Fields != null)
            {
                for (int i = 0; i < this.Fields.Count; i++)
                {
                    this.Fields[i].value = this.Fields[i].value_Initial;
                }
            }
        }
        //输出条件表达式
        public string ConditionsString() { return ConditionsString(false); }
        public string ConditionsString(bool addWhere)
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if (this.Fields[i].selected)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(" and ");
                    }
                    sb.Append(this.Fields[i].Condition);
                    //sb.Append(this.Fields[i].NameStr());
                    //sb.Append(" = ");
                    //sb.Append(this.Fields[i].ValueStr());
                }
            }
            string sCon = sb.ToString();
            if (!String.IsNullOrEmpty(sCon) && addWhere)
            {
                sCon = " where " + sCon;
            }
            return sCon;
        }
        public string DefaultConditionString(string keyFieldName) { return DefaultConditionString(keyFieldName, false); }
        public string DefaultConditionString(string keyFieldName,bool addWhere)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Fields.Count; i++)
            {
                bool sel = false;
                if (!string.IsNullOrEmpty(keyFieldName) && (","+keyFieldName+",").Contains(this.Fields[i].name))
                {
                    sel = true;
                }
                else if (this.Fields[i].isIdentity)
                {
                    sel = true;
                }
                else if (this.Fields[i].isKeyField)
                {
                    sel = true;
                }
                if (sel)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(" and ");
                    }
                    sb.Append(this.Fields[i].Condition);
                    //sb.Append(this.Fields[i].NameStr());
                    //sb.Append(" = ");
                    //sb.Append(this.Fields[i].ValueStr());
                }
            }
            string sCon=sb.ToString();
            if (!String.IsNullOrEmpty(sCon) && addWhere)
            {
                sCon = " where " + sCon;
            }
            return sCon;
        }
        //输出字段与值列表
        public string FieldsListString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if (this.Fields[i].selected)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(this.Fields[i].NameStr());
                }
            }
            return sb.ToString();
        }
        //updatedField = true是指插入updated标志为True的字段
        //             = false是指插入非空字段
        public string FieldsListStringForInsert() { return FieldsListStringForInsert(true); }
        public string FieldsListStringForInsert(bool updatedField)
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if ((!this.Fields[i].readOnly && !this.Fields[i].isIdentity)
                    && (!updatedField && !this.Fields[i].isNull || updatedField && this.Fields[i].updated || this.Fields[i].addWith))
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(this.Fields[i].NameStr());
                }
            }
            return sb.ToString();
        }
        public string ValuesListString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if (this.Fields[i].selected)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(this.Fields[i].ValueStr());
                }
            }
            return sb.ToString();
        }
        //updatedField = true是指插入updated标志为True的字段
        //             = false是指插入非空字段
        public string ValuesListStringForInsert() { return ValuesListStringForInsert(true); }
        public string ValuesListStringForInsert(bool updatedField)
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if ((!this.Fields[i].readOnly && !this.Fields[i].isIdentity)
                    && (!updatedField && !this.Fields[i].isNull || updatedField && this.Fields[i].updated || this.Fields[i].addWith))
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    if (this.Fields[i].value == null)
                    {
                        sb.Append(this.Fields[i].ValueStr(this.Fields[i].defaultValue));
                    }
                    else
                    {
                        sb.Append(this.Fields[i].ValueStr());
                    }
                }
                //if (this.Fields[i].isKeyField && String.IsNullOrEmpty(this.Fields[i].ToString()))
                //{   //当为关键字段，却为空值时，返回空值！数据库执行脚本一定出错！
                //    return String.Empty;
                //}
            }
            return sb.ToString();
        }
       //输出参数列表
        public string FieldsParamListString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                //(|| this.Fields[i].isIdentity)值得争议
                if (this.Fields[i].selected || this.Fields[i].isIdentity)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append("@");
                    sb.Append(this.Fields[i].name);
                }
            }
            return sb.ToString();
        }
        //格式化输出字符串
        public string Format(string sFormatString)
        {
            if ((this.Fields != null) && (this.Fields.Count != 0))
            {
                for (int i = 0; i < this.Fields.Count; i++)
                {
                    sFormatString = sFormatString.Replace("{$" + this.Fields[i].name + "}", Convert.ToString(this.Fields[i].value));
                }
            }
            return sFormatString;
        }
        public string ExportXmlString()
        {
            if (this.Fields == null) return null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<NewDataSet><NewDataTable></NewDataTable></NewDataSet>");
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("NewDataTable");
            foreach (XDataField xdf in this.Fields)
            {
                XmlElement x = xmlDoc.CreateElement(xdf.name);
                x.InnerText = xdf.ToString();
                xn.AppendChild(x);
            }
            string s = xmlDoc.InnerXml;
            xmlDoc = null;
            return s;
        }
        public bool ImportXmlString(string xmlStr)
        {
            if (String.IsNullOrEmpty(xmlStr)) return false;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            XmlNode xnSel = xmlDoc.DocumentElement.SelectSingleNode("NewDataTable");
            foreach (XmlNode xn in xnSel.ChildNodes)
            {
                this.DFields[xn.Name].set(xn.InnerText);
            }
            xmlDoc = null;
            return true;
        }
        //输出某个字段
        public XDataField GetField(string fieldName)
        {
            return DFields[fieldName];
        }
        public object GetFieldValue(string fieldName)
        {
            return GetField(fieldName).value;
        }
        public string GetFieldValueStr(string fieldName)
        {
            return GetField(fieldName).ValueStr();
        }
        //输出排序表达式
        public string OrderByString() { return OrderByString(false); }
        public string OrderByString(bool withOrderBy)
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if (this.Fields[i].selected)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(this.Fields[i].NameStr());
                    if (this.Fields[i].desc)
                    {
                        sb.Append(" desc");
                    }
                }
            }
            string sOrder = sb.ToString();
            if (!String.IsNullOrEmpty(sOrder) && withOrderBy)
                sOrder = " order by " + sOrder;
            return sOrder;
        }
        //输出更新字段列表
        public string SetsListString()
        {
            return this.SetsListString(false);
        }
        public string SetsListString(bool updateAllFields)
        {
            StringBuilder sb = new StringBuilder();
            if (this.Fields == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < this.Fields.Count; i++)
            {
                if (!this.Fields[i].readOnly && (updateAllFields || this.Fields[i].updated) && !this.Fields[i].isIdentity)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(this.Fields[i].Expression);
                    //sb.Append(this.Fields[i].NameStr());
                    //sb.Append(" = ");
                    //sb.Append(this.Fields[i].ValueStr());
                }
            }
            return sb.ToString();
        }
        //多个兄弟Model之间值传送
        public void ImportFrom(XModel otherModel)
        {
            List<XDataField> otherFields = otherModel.Fields;
            Dictionary<string, XDataField> thisFields = this.DFields;
            foreach (XDataField xdf in otherFields)
            {
                if (thisFields.ContainsKey(xdf.name))
                {
                    thisFields[xdf.name].set(xdf.value);
                }
            } 
        }
        public void ExportInto(XModel otherModel)
        {
            List<XDataField> thisFields = this.Fields;
            Dictionary<string, XDataField> otherFields = otherModel.DFields;
            foreach (XDataField xdf in thisFields)
            {
                if (otherFields.ContainsKey(xdf.name))
                {
                    otherFields[xdf.name].set(xdf.value);
                }
            }
        }
    }
}

