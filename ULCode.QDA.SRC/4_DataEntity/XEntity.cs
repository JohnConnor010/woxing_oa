namespace ULCode.QDA
{
    using System;
    using System.Data;
    using ULCode.QDA.SqlStatement;
    using System.Text;
    /// <summary>
    /// XEntity的功能有：
    /// 一、组织CnName,Table,KeyField与(KeyValue、Model、XSqlStatementExpression)进行数据访问
    /// 二、获取数据Model、Models、Object、DataSet、DataTable、SqlStatement语句
    /// </summary>
    public abstract class XEntity
    {
        public string CnName;         //可以有多个,以逗号分隔
        public string KeyField;       //可以有多个,以逗号分隔
        public string TableName;      //只能有一个，且不能为空
        public string[] KeyFields
        {
            get 
            {
                return KeyField.Split(',');
            }
        }
        public string PrimaryKeyField
        {
            get
            {
                _CheckKeyField();
                return KeyFields[0];
            }
        }
        private string _viewName;     
        public string ViewName        //只能有一个
        {
            get
            {
                if (string.IsNullOrEmpty(this._viewName))
                {
                    return this.TableName;
                }
                return this._viewName;
            }
            set
            {
                this._viewName = value;
            }
        }
        ///*************************************************************
        /// <summary>
        /// 输入共有三种：
        /// 一、只有TableName。
        /// 二、只有TableName与KeyField
        /// 三、CnName与TableName与KeyField
        /// 其它还有一种，当ViewName用来作为读取操作。
        /// 检索条件有四种：
        /// 1.XModel
        /// 2.XSqlStatementExpression
        /// 3.String
        /// 4.KeyValue，此类操作必须用来KeyField，如果KeyField为空，则会出错！
        /// </summary>
        /// <param name="tableName"></param>
        public XEntity(string tableName)
        {
            this.CnName = null;
            this.TableName = tableName;
            this.KeyField = null;
            _CheckTableName();
        }
        public XEntity(string tableName, string keyField)
        {
            this.CnName = null;
            this.TableName = tableName;
            this.KeyField = keyField;
            _CheckTableName();
        }
        public XEntity(string cnName, string tableName, string keyField)
        {
            this.CnName = cnName;
            this.TableName = tableName;
            this.KeyField = keyField;
            _CheckTableName();
        }
        //表名验证
        private void _CheckTableName()
        {
            if (String.IsNullOrEmpty(TableName))
            {
                throw new ApplicationException("表名不能为空！");
            }
        }
        //关键字验证
        private void _CheckKeyField()
        {
            if (String.IsNullOrEmpty(KeyField))
            {
                throw new ApplicationException("此功能函数要求关键字不为空！");
            }
        }        
        ///*************************************************************
        ///需要实现
        protected abstract int Execute(string sSql);
        protected abstract DataTable GetDataTable(string sSql);
        protected abstract object GetValue(string sSql);
        ///*************************************************************
        private string GetFieldListStr(object objFieldList)
        {
            string strFieldsList = String.Empty;
            //get strFieldList
            if (objFieldList == null || objFieldList == Convert.DBNull)
                strFieldsList = "*";
            else if (objFieldList is String)
                strFieldsList = Convert.ToString(objFieldList);
            else if (objFieldList is XDataField)
                strFieldsList = ((XDataField)objFieldList).NameStr();
            else if (objFieldList is XModel)
                strFieldsList = ((XModel)objFieldList).FieldsListString();
            else if (objFieldList is FieldListExpression)
                strFieldsList = ((FieldListExpression)objFieldList).FieldsListString();
            //
            return strFieldsList;
        }
        private string GetWhereStr(object objWhere)
        {
            string strWhere = String.Empty;
            //get strWhere
            if (objWhere == null || objWhere == Convert.DBNull)
                strWhere = String.Empty;
            else if (objWhere is String)
                strWhere = new ConditionExpression(Convert.ToString(objWhere)).ConditionStr();
            else if (objWhere is XDataField)
                strWhere = ((XDataField)objWhere).Condition;
            else if (objWhere is XModel)
                strWhere = ((XModel)objWhere).ConditionsString();
            else if (objWhere is ConditionUnit)
                strWhere = ((ConditionUnit)objWhere).ConditionStr();
            else if (objWhere is ConditionExpression)
                strWhere = ((ConditionExpression)objWhere).ConditionStr();

            return strWhere;

        }
        private string GetOrderStr(object objOrder)
        {
            string strOrder = String.Empty;
            //get strOrder
            if (objOrder == null || objOrder == Convert.DBNull)
                strOrder = String.Empty;
            else if (objOrder is String)
                strOrder = new OrderExpression(Convert.ToString(objOrder)).OrderBy();
            else if (objOrder is XDataField)
                strOrder = ((XDataField)objOrder).OrderStr(true);
            else if (objOrder is XModel)
                strOrder = ((XModel)objOrder).OrderByString(true);
            else if (objOrder is OrderUnit)
                strOrder = ((OrderUnit)objOrder).OrderBy();
            else if (objOrder is OrderExpression)
                strOrder = ((OrderExpression)objOrder).OrderBy();
            else if (objOrder is FieldListExpression)
                strOrder = ((FieldListExpression)objOrder).OrderByString();
            //
            return strOrder;
        }
        private string GetSetsStr(object objSets)
        {
            string strSets = String.Empty;
            //get strSets
            if (objSets is Nullable || objSets == Convert.DBNull)
                strSets = String.Empty;
            else if (objSets is String)
                strSets = Convert.ToString(objSets);
            else if (objSets is XDataField)
                strSets = ((XDataField)objSets).Expression;
            else if (objSets is ConditionUnit)    //当DataName==Object时
                strSets = ((ConditionUnit)objSets).Value;
            else if (objSets is XModel)
                strSets = ((XModel)objSets).SetsListString();
            else if (objSets is SetsUnit)
                strSets = ((SetsUnit)objSets).Value;
            else if (objSets is SetsExpression)
                strSets = ((SetsExpression)objSets).Value;
            //
            return strSets;
        }
        public string GetKeyConditionString(bool addWhere, DataRow drCache)
        {
            StringBuilder sb = new StringBuilder();
            String[] keyFields = KeyFields;
            for (int i = 0; i < keyFields.Length; i++)
            {
                string kf = keyFields[i];
                if (sb.Length != 0) sb.Append(" and ");
                if (drCache[kf] == Convert.DBNull)
                {
                    sb.AppendFormat("{0} IS NULL", kf);
                }
                else
                    sb.AppendFormat("{0}='{1}'", keyFields[i], drCache[kf]);
            }
            if (sb.Length != 0 && addWhere)
                sb.Insert(0, " where ");
            return sb.ToString();
        }
        public string GetKeyConditionString(bool addWhere, XModel xm)
        {
            StringBuilder sb = new StringBuilder();
            String[] keyFields = KeyFields;
            for (int i = 0; i < keyFields.Length; i++)
            {
                string kf = keyFields[i];
                //object kv = xm.DFields[kf].ValueStr();
                if (sb.Length != 0) sb.Append(" and ");
                sb.Append(xm.DFields[kf].Condition);
                //if (kv == Convert.DBNull)
                //    sb.AppendFormat("{0} IS NULL", kf);
                //else if (xm.DFields[kf].type == DbType.Guid)
                //{
                //    //sb.AppendFormat("{0} = {1}", kf, ULCode.XSql.Sql.GetQueryGuidFieldValue(this.CnName, xm.DFields[kf].value));
                //    sb.AppendFormat("{0} = '{1}'", kf, kv);
                //}
                //else
                //    sb.AppendFormat("{0} = '{1}'", kf, kv);
            }
            if (sb.Length != 0 && addWhere)
                sb.Insert(0, " where ");
            return sb.ToString();
        }
        public string GetKeyConditionString(bool addWhere, params object[] keyValues)
        {
            if (keyValues == null)
                return null;

            StringBuilder sb = new StringBuilder();
            String[] keyFields = KeyFields;
            for (int i = 0; i < keyValues.Length; i++)
            {
                if (sb.Length != 0) sb.Append(" and ");
                if (keyValues[i] == Convert.DBNull)
                    sb.AppendFormat("{0} IS NULL", keyFields[i]);
                else
                    sb.AppendFormat("{0}='{1}'", keyFields[i], keyValues[i]);
            }
            if (sb.Length != 0 && addWhere)
                sb.Insert(0, " where ");
            return sb.ToString();
        }
        ///*************************************************************
        ///删除操作
        public int Delete(object objWhere)
        {
            string sSql = "Delete " + this.TableName + " " + GetWhereStr(objWhere);
            return this.Execute(sSql);
        }
        public string DeleteOneSql(params object[] keyValues)
        {
            _CheckKeyField();  //验证
            return ("Delete from " + this.TableName + " where " + GetKeyConditionString(false, keyValues));
        }
        public int DeleteOne(params object[] keyValues)
        {
            string sSql = this.DeleteOneSql(keyValues);
            if (string.IsNullOrEmpty(sSql))
            {
                return -1;
            }
            return this.Execute(sSql);
        }
        public int DeleteOne(XModel xm)
        {
            string sSql = "Delete from " + this.TableName + " " + GetKeyConditionString(true, xm);
            return this.Execute(sSql);
        }
        public int DeleteOneFromDataTable(DataTable dtCache, params object[] keyValues)
        {
            DataRow[] drs = dtCache.Select(GetKeyConditionString(false, keyValues));
            int iR = drs.Length;
            foreach (DataRow dr in drs)
                dr.Delete();
            return iR;
        }
        ///GetMaxKey
        public object GetMaxKey()
        {
            return this.GetMaxKey(null);
        }
        public object GetMaxKey(object objWhere)
        {
            _CheckKeyField();
            string sSql = "Select Max(" + this.KeyField + ") from " + this.ViewName + GetWhereStr(objWhere);
            return this.GetValue(sSql);
        }
        public object GetMaxKey(XDataField xf,object objWhere)
        {
            string sSql = "Select Max(" + xf.NameStr() + ") from " + this.ViewName + GetWhereStr(objWhere);
            return this.GetValue(sSql);
        }
        public object GetMinKey()
        {
            return this.GetMinKey(null);
        }
        public object GetMinKey(object objWhere)
        {
            _CheckKeyField();
            string sSql = "Select Min(" + this.KeyField + ") from " + this.ViewName + GetWhereStr(objWhere);
            return this.GetValue(sSql);
        }
        public object GetMinKey(XDataField xf, object objWhere)
        {
            string sSql = "Select Min(" + xf.NameStr() + ") from " + this.ViewName + GetWhereStr(objWhere);
            return this.GetValue(sSql);
        }
        ///**************************************************************
        public int GetRowsCount()
        {
            string sSql = "Select count(*) from " + this.ViewName;
            return Convert.ToInt32(this.GetValue(sSql));
        }
        public int GetRowsCount(object objWhere)
        {
            string strWhere = GetWhereStr(objWhere);
            string sSql = "Select count(*) from " + this.ViewName + " " + strWhere;
            return Convert.ToInt32(this.GetValue(sSql));
        }
        ///**************************************************************
        public object GetValue(string fieldName, params object[] keyValues)
        {
            _CheckKeyField();
            string strWhere = GetKeyConditionString(true, keyValues);
            string sSql = "Select " + fieldName + " from " + this.ViewName + strWhere;
            return this.GetValue(sSql);
        }
        public object GetValue(XDataField xf,params object[] keyValues)
        {
            _CheckKeyField();
            string strWhere = GetKeyConditionString(true, keyValues);
            string sSql = "Select " + xf.NameStr() + " from " + this.ViewName + strWhere;
            return this.GetValue(sSql);
        }
        public object GetValueInDataTable(string fieldName,DataTable dtCache, params object[] keyValues)
        {
            _CheckKeyField();
            DataRow[] drs = dtCache.Select(GetKeyConditionString(false, keyValues));
            if (drs.Length > 0)
                return drs[0][fieldName];
            else
                return null;
        }
        public object GetValueInDataTable(XDataField xf, DataTable dtCache, params object[] keyValues)
        {
            return GetValueInDataTable(xf.name, dtCache, keyValues);
        }
        ///**************************************************************
        public int Insert(XModel insertModel)
        {
            return Insert(insertModel, false);
        }
        public int Insert(XModel insertModel, bool returnIdentityID)
        {
            string sSql = this.InsertSql(insertModel);
            int iR;
            if (string.IsNullOrEmpty(sSql))
            {
                return -1;
            }            
            if (returnIdentityID)
            {
                sSql = sSql + ";SELECT @@IDENTITY";
            }
            if (!returnIdentityID)
                iR = this.Execute(sSql);
            else
                iR = Convert.ToInt32(this.GetValue(sSql));

            if (iR > 0)
            {
                insertModel.ClearAllUpdated();
            }
            return iR;
        }
        public string InsertSql(XModel insertModel)
        {
            string f1 = insertModel.FieldsListStringForInsert(false);
            string f2 = insertModel.ValuesListStringForInsert(false);
            if (String.IsNullOrEmpty(f1) || String.IsNullOrEmpty(f2))
                return null;
            else
                return ("Insert into " + this.TableName + "(" + f1 + ") values(" + f2 + ")");
        }
        ///**************************************************************
        public DataTable Select(object objFieldList, object objWhere, object objOrder)
        {
            string strFieldsList = GetFieldListStr(objFieldList);
            string strWhere = GetWhereStr(objWhere);
            string strOrder = GetOrderStr(objOrder);
            //
            string strSql = "Select {0} from {1} {2} {3}".TrimEnd();
            return GetDataTable(String.Format(strSql, strFieldsList, this.ViewName, strWhere, strOrder));
        }
        public DataTable SelectAll()
        {
            return this.Select(null,null,null);
        }
        public DataTable SelectOne(params object[] keyValues)
        {
            string strWhere = GetKeyConditionString(false, keyValues);
            return Select(null, strWhere, null);
        }
        ///**************************************************************
        public int Update(object objSets, object objWhere)
        {
            string strSets = GetSetsStr(objSets);
            string strWhere = GetWhereStr(objWhere);
            if (String.IsNullOrEmpty(strSets))
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ReturnValueWhenUpdateEmpty"] != null)
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ReturnValueWhenUpdateEmpty"]);
                else
                    return 0;
            }
            else
            {
                string sSql = "Update " + this.TableName + " Set " + strSets + " " + strWhere;
                return this.Execute(sSql);
            }
        }
        public int UpdateOne(XModel setsModel)
        {
            string sSql = UpdateOneSql(setsModel);
            if (String.IsNullOrEmpty(sSql))
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ReturnValueWhenUpdateEmpty"] != null)
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ReturnValueWhenUpdateEmpty"]);
                else
                    return 0;
            }
            else
            {
                int iR = this.Execute(sSql);
                if (iR == 1)
                {
                    setsModel.ClearAllUpdated();
                }
                return iR;
            }
        }
        public string UpdateOneSql(XModel setsModel)
        {
            _CheckKeyField();
            string sets = setsModel.SetsListString();
            if (string.IsNullOrEmpty(sets))
            {
                return null;
            }
            else
            {
                return ("Update " + this.TableName + " set " + sets + GetKeyConditionString(true, setsModel));
            }
        }
        ///**************************************************************
        ///outputModel
        ///关键值未赋，称为状态A=未初始化状态;
        ///关键值已赋，但其它值未赋，称为状态B=已初始化状态;
        ///所有值已经赋为为C=完整状态
        ///当keyValues为空时，则outputModel不能为未初始化状态。
        public void LoadFromDataRow(XModel outputModel, DataRow drCache)
        {
            if (drCache != null)
            {
                XModel xdm = outputModel;
                for (int i = 0; i < xdm.Fields.Count; i++)
                {
                    string fname = xdm.Fields[i].name;
                    if (drCache.Table.Columns.Contains(fname))
                    {
                        xdm.Fields[i].value = drCache[fname];
                    }
                }
                xdm.LoadSucceed = true;
                outputModel.ClearAllUpdated();
            }
        }
        public int LoadFromDataTable(XModel outputModel, DataTable dtCache, params object[] keyValues)
        {
            _CheckKeyField();
            if (dtCache == null || dtCache.Rows.Count == 0)
                return -2;
            string sCon = null;
            if (keyValues != null)
            {
                LoadKeyValues(outputModel, keyValues);
                sCon = GetKeyConditionString(false, keyValues);
            }
            else
            {
                sCon = GetKeyConditionString(false, outputModel);
            }
            DataTable dt = dtCache;
            DataRow[] drs = dtCache.Select(sCon);
            if (drs.Length > 0)
            {
                this.LoadFromDataRow(outputModel, drs[0]); return drs.Length;
            }
            else
            {
                return -1;
            }
        }
        public int LoadFromDB(XModel outputModel, params object[] keyValues)
        {
            _CheckKeyField();
            string sCon = null;
            if (keyValues != null)
            {
                LoadKeyValues(outputModel, keyValues);
                sCon = GetKeyConditionString(true, keyValues);
            }
            else
            {
                sCon = GetKeyConditionString(true, outputModel);
            }
            string sSql = "Select * from " + this.ViewName + sCon;
            DataTable dt = this.GetDataTable(sSql);
            if (dt!=null&&dt.Rows.Count != 0)
            {
                DataRow dr = dt.Rows[0];
                this.LoadFromDataRow(outputModel, dr);
                return 1;
            }
            else
                return 0;
        }
        //
        public void LoadKeyValues(XModel outputModel,params object[] keyValues)
        {
            for (int i = 0; i < KeyFields.Length; i++)
            {
                string key = KeyFields[i];
                object value = keyValues[i];
                outputModel.DFields[key].set(value);
            }
        }
        ///**************************************************************
        public int SaveToDb(XModel updateModel)
        {
            _CheckKeyField();
            string sets = updateModel.SetsListString();
            string fieldLists = updateModel.FieldsListStringForInsert(false);
            string valueLists = updateModel.ValuesListStringForInsert(false);
            //string whereStr=updateModel.DefaultConditionString(this.KeyField);
            string whereStr = GetKeyConditionString(false, updateModel);
            //if (string.IsNullOrEmpty(sets))
            //{
            //    return 0;
            //}
            string sSql_Exists = "Select * from  " + this.TableName + " where " + whereStr;
            string sSql_Update = String.IsNullOrEmpty(sets) ? "Select 1;" : "Update " + this.TableName + " set " + sets + " where " + whereStr + ";";
            string sSql_Insert = String.IsNullOrEmpty(fieldLists)||String.IsNullOrEmpty(valueLists) ? "Select 1;" : "Insert into " + this.TableName + "(" + fieldLists + ") values(" + valueLists + ");";
            string sSql = String.Format("if exists({0}) {1} else {2}", sSql_Exists, sSql_Update, sSql_Insert);
            int iR = 0;
            ULCode.QDA.ProviderType ct = XSql.GetDB(this.CnName).GetProviderType();
            if (ct == ProviderType.MsSql)
            {
                iR = this.Execute(sSql);
            }
            else// if (ct == ProviderType.OleDb)
            {
                if (XSql.IsHasRow(this.CnName,sSql_Exists))
                {
                    iR = this.Execute(sSql_Update);
                }
                else
                {
                    iR = this.Execute(sSql_Insert);
                }
            }
            if (iR > 0)
            {
                updateModel.ClearAllUpdated();
            }
            return iR;
        }
        public int SaveToDataRow(XModel updateModel, DataRow dataRowToUpdate)
        {
            if (dataRowToUpdate != null)
            {
                XModel xdm = updateModel;
                for (int i = 0; i < xdm.Fields.Count; i++)
                {
                    string fname = xdm.Fields[i].name;
                    if (dataRowToUpdate.Table.Columns.Contains(fname))
                    {
                        object o = xdm.Fields[i].value;
                        if (o == null) o = Convert.DBNull;
                        dataRowToUpdate[fname] = o;
                    }
                }
                return 1;
            }
            else
                return 0;
        }
        public int SaveToDataTable(XModel updateModel, DataTable dataTableToUpdate)
        {
            if (dataTableToUpdate != null)
            {
                DataRow dr;
                DataRow[] drs = dataTableToUpdate.Select(GetKeyConditionString(false, updateModel));
                if (drs.Length == 0)
                {
                    dr = dataTableToUpdate.Rows.Add(new object[0]);
                }
                else
                {
                    dr = drs[0];
                }
                return this.SaveToDataRow(updateModel, dr);
            }
            else
                return 0;
        }
        //***************************************************************
    }
}

