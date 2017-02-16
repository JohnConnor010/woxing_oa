
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class CustomerTemp
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            public string CreateCustomerTempID(string CustomerTempId)
            {
                if (!string.IsNullOrEmpty(CustomerTempId))
                {
                    try
                    {
                        string strNumber = CustomerTempId.Replace(String.Format("KH{0}", DateTime.Now.ToString("yyyyMM")), "");
                        return String.Format("KH{1}{0:D4}", Convert.ToInt32(strNumber) + 1, DateTime.Now.ToString("yyyyMM"));
                    }
                    catch
                    {
                        return String.Format("KH{0}0001", DateTime.Now.ToString("yyyyMM"));
                    }
                }
                else
                {
                    return String.Format("KH{0}0001", DateTime.Now.ToString("yyyyMM"));
                }
            }

        }
    }
    public partial class CustomerTemp : XDataEntity
    {
        public CustomerTemp(string tableName)
            : base(tableName)
        {
        }
        public CustomerTemp(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CustomerTemp(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CustomerTemp _entity;
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
        public static CustomerTemp NewEntity()
        {
            return new CustomerTemp("CRM_CustomersTemp", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CustomerTemp Entity
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
            //1.客户基本信息
            public XDataField CustomerID;
            public XDataField CustomerName;
            public XDataField CustomerZJM;
            public XDataField NatureID;
            public XDataField IndustryID;
            public XDataField CategoryID;
            public XDataField SourceID;
            public XDataField Province;
            public XDataField City;
            public XDataField Area;
            public XDataField Address;
            public XDataField WebSite;
            public XDataField ImagePath;
            //2.客户营业信息
            public XDataField EstablishmentDate;
            public XDataField RealName;
            public XDataField BankName;
            public XDataField BankAccount;
            public XDataField BusinessCircles;
            public XDataField Products;
            //3.客户合作信息
            public XDataField BusinessLevel;
            public XDataField CoopFlag;
            public XDataField StageID;
            public XDataField BuyHabbit;
            public XDataField LastConsumptionMoney;
            public XDataField LastMaintainMoney;
            public XDataField PreMoney;
            public XDataField LastBegin;
            public XDataField LastEnd;
            public XDataField UrgerDate;
            public XDataField ConsumptionMoney;
            public XDataField MaintainMoney;
            public XDataField Mobile_D;
            public XDataField AttachFile_D;
            //4.其它客户信息
            public XDataField Remarks;
            public XDataField SpecialDesc;
            public XDataField Integral_D;
            //5.OA维护信息
            public XDataField CreateUserId;
            public XDataField CreateDate;
            public XDataField EmployeeID;
            public XDataField DeptId;
            public XDataField State;
            public XDataField CheckUserId;
            public XDataField CustomersID;

            public MODEL() { }
            public MODEL(CustomerTemp parentEntity) : base(parentEntity) { }
            public MODEL(CustomerTemp parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CustomerTemp parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CustomerTemp parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
                //1.客户基本信息
                this.ID=new XDataField("ID",DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.String);
                this.CustomerName=new XDataField("CustomerName",DbType.String);
                this.CustomerZJM=new XDataField("CustomerZJM",DbType.String);
                this.NatureID=new XDataField("NatureID",DbType.Int32);
                this.IndustryID=new XDataField("IndustryID",DbType.Int32);
                this.CategoryID=new XDataField("CategoryID",DbType.Int32);
                this.SourceID=new XDataField("SourceID",DbType.Int32);
                this.Province=new XDataField("Province",DbType.String);
                this.City=new XDataField("City",DbType.String);
                this.Area=new XDataField("Area",DbType.String);
                this.Address=new XDataField("Address",DbType.String);
                this.WebSite=new XDataField("WebSite",DbType.String);
                this.ImagePath=new XDataField("ImagePath",DbType.String);
                //2.客户营业信息
                this.EstablishmentDate=new XDataField("EstablishmentDate",DbType.String);
                this.RealName=new XDataField("RealName",DbType.String);
                this.BankName=new XDataField("BankName",DbType.String);
                this.BankAccount=new XDataField("BankAccount",DbType.String);
                this.BusinessCircles=new XDataField("BusinessCircles",DbType.String);
                this.Products=new XDataField("Products",DbType.String);
                //3.客户合作信息
                this.BusinessLevel=new XDataField("BusinessLevel",DbType.Byte);
                this.CoopFlag=new XDataField("CoopFlag",DbType.Int32);
                this.StageID=new XDataField("StageID",DbType.Int32);
                this.BuyHabbit=new XDataField("BuyHabbit",DbType.Int32);
                this.LastConsumptionMoney=new XDataField("LastConsumptionMoney",DbType.Decimal);
                this.LastMaintainMoney=new XDataField("LastMaintainMoney",DbType.Decimal);
                this.PreMoney=new XDataField("PreMoney",DbType.Decimal);
                this.LastBegin=new XDataField("LastBegin",DbType.DateTime);
                this.LastEnd=new XDataField("LastEnd",DbType.DateTime);
                this.UrgerDate=new XDataField("UrgerDate",DbType.DateTime);
                this.ConsumptionMoney=new XDataField("ConsumptionMoney",DbType.Decimal);
                this.MaintainMoney=new XDataField("MaintainMoney",DbType.Decimal);
                this.Mobile_D=new XDataField("Mobile_D",DbType.String);
                //4.其它客户信息
                this.AttachFile_D=new XDataField("AttachFile_D",DbType.String);
                this.Remarks=new XDataField("Remarks",DbType.String);
                this.SpecialDesc=new XDataField("SpecialDesc",DbType.String);
                this.Integral_D=new XDataField("Integral_D",DbType.Int32);
                //5.OA维护信息
                this.CreateUserId=new XDataField("CreateUserId",DbType.String);
                this.CreateDate=new XDataField("CreateDate",DbType.DateTime);
                this.EmployeeID=new XDataField("EmployeeID",DbType.String);
                this.DeptId=new XDataField("DeptId",DbType.Int32);
                this.State=new XDataField("State",DbType.Int32);
                this.CheckUserId=new XDataField("CheckUserId",DbType.String);
                this.CustomersID = new XDataField("CustomersID", DbType.Int32);   
                //
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.CustomerID, this.CustomerName, this.CustomerZJM, this.NatureID, this.IndustryID, this.CategoryID, this.SourceID, this.Province, this.City, this.Area, this.Address, this.WebSite, this.ImagePath, this.EstablishmentDate, this.RealName, this.BankName, this.BankAccount, this.BusinessCircles, this.Products, this.BusinessLevel, this.CoopFlag, this.StageID, this.BuyHabbit, this.LastConsumptionMoney, this.LastMaintainMoney, this.PreMoney, this.LastBegin, this.LastEnd, this.UrgerDate, this.ConsumptionMoney, this.MaintainMoney, this.Mobile_D, this.AttachFile_D, this.Remarks, this.SpecialDesc, this.Integral_D, this.CreateUserId, this.CreateDate, this.EmployeeID, this.DeptId, this.State, this.CheckUserId, this.CustomersID });
            }
        }
    }
}
