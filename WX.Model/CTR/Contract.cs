
namespace WX.CTR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Contract
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Contract : XDataEntity
    {
        public Contract(string tableName)
            : base(tableName)
        {
        }
        public Contract(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Contract(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Contract _entity;
        private static MODEL _model;

        public static MODEL NewDataModel()
        {
            return new MODEL(Entity);
        }
        public static MODEL NewDataModel(DataRow drCache)
        {
            return new MODEL(Entity, drCache);
        }
        public static MODEL NewDataModel(params object[] keyValues)
        {
            return new MODEL(Entity, keyValues);
        }
        public static MODEL NewDataModel(DataTable dtCache, params object[] keyValues)
        {
            return new MODEL(Entity, dtCache, keyValues);
        }
        public static Contract NewEntity()
        {
            return new Contract("CTR_Contracts", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Contract Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = NewEntity();
                }
                return _entity;
            }
        }
        public static MODEL Model
        {
            get
            {
                if (_model == null)
                {
                    _model = NewModel();
                }
                return _model;
            }
        }
        public static MODEL GetModel(string sSql)
        {
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return NewDataModel(dr);
        }
        public static List<MODEL> GetModels(string sSql)
        {
            List<MODEL> lm = new List<MODEL>();
            DataTable dt = XSql.GetDataTable(sSql);
            foreach (DataRow dr in dt.Rows)
            {
                lm.Add(NewDataModel(dr));
            }
            return lm;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField ContractID;
            public XDataField ContractName;
            public XDataField CategoryID;
            public XDataField ProjectID;
            public XDataField ContractAmount;
            public XDataField Currency;
            public XDataField SignedDate;
            public XDataField EmployeeID;
            public XDataField DepartmentID;
            public XDataField PaymentType;
            public XDataField StartDate;
            public XDataField EndDate;
            public XDataField ReceivablesPayment;
            public XDataField PartyA;
            public XDataField PartyAPerson;
            public XDataField PartyB;
            public XDataField PartyBPerson;
            public XDataField DigitPath;
            public XDataField Implementation;
            public XDataField InputDate;
            public XDataField Managers;
            public XDataField ContractContent;
            public XDataField ContractAbnormal;
            public XDataField Remark;

            public MODEL() { }
            public MODEL(Contract parentEntity) : base(parentEntity) { }
            public MODEL(Contract parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Contract parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Contract parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.ContractID=new XDataField("ContractID",DbType.String);
                this.ContractName=new XDataField("ContractName",DbType.String);
                this.CategoryID=new XDataField("CategoryID",DbType.Int32);
                this.ProjectID=new XDataField("ProjectID",DbType.Int32);
                this.ContractAmount=new XDataField("ContractAmount",DbType.Decimal);
                this.Currency=new XDataField("Currency",DbType.String);
                this.SignedDate=new XDataField("SignedDate",DbType.Date);
                this.EmployeeID=new XDataField("EmployeeID",DbType.String);
                this.DepartmentID=new XDataField("DepartmentID",DbType.Int32);
                this.PaymentType=new XDataField("PaymentType",DbType.String);
                this.StartDate=new XDataField("StartDate",DbType.Date);
                this.EndDate=new XDataField("EndDate",DbType.Date);
                this.ReceivablesPayment=new XDataField("ReceivablesPayment",DbType.String);
                this.PartyA=new XDataField("PartyA",DbType.String);
                this.PartyAPerson=new XDataField("PartyAPerson",DbType.String);
                this.PartyB=new XDataField("PartyB",DbType.String);
                this.PartyBPerson=new XDataField("PartyBPerson",DbType.String);
                this.DigitPath=new XDataField("DigitPath",DbType.String);
                this.Implementation=new XDataField("Implementation",DbType.String);
                this.InputDate=new XDataField("InputDate",DbType.Date);
                this.Managers=new XDataField("Managers",DbType.String);
                this.ContractContent=new XDataField("ContractContent",DbType.String);
                this.ContractAbnormal=new XDataField("ContractAbnormal",DbType.String);
                this.Remark=new XDataField("Remark",DbType.String);   
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.ContractID,this.ContractName,this.CategoryID,this.ProjectID,this.ContractAmount,this.Currency,this.SignedDate,this.EmployeeID,this.DepartmentID,this.PaymentType,this.StartDate,this.EndDate,this.ReceivablesPayment,this.PartyA,this.PartyAPerson,this.PartyB,this.PartyBPerson,this.DigitPath,this.Implementation,this.InputDate,this.Managers,this.ContractContent,this.ContractAbnormal,this.Remark });
            }
        }
    }
}
