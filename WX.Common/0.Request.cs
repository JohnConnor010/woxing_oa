using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Model;
using ULCode.QDA;
using System.Web.UI;
namespace WX
{
    public class Request
    {
        //1.Request Validate
        public void ValidateQuery()
        {
            if (!this.ValidateIntQ("CompanyId")) return;
            if (!this.ValidateIntQ("DepartmentId")) return;
            if (!this.ValidateIntQ("DeptId")) return;
            if (!this.ValidateIntQ("DutyId")) return;
            if (!this.ValidateIntQ("FunctionId")) return;
            if (!this.ValidateIntQ("MenuId")) return;
            if (!this.ValidateGuidQ("UserId")) return;
            if (!this.ValidateIntQ("ProjectId")) return;
            if (!this.ValidateIntQ("SupplierID")) return;
            if (!this.ValidateIntQ("ContractID")) return;
            if (!this.ValidateIntQ("CustomerID")) return;
            if (!this.ValidateIntQ("CustomerTempID")) return;
            if (!this.ValidateIntQ("ContactID")) return;
            if (!this.ValidateIntQ("WarehouseID")) return;
            if (!this.ValidateIntQ("FormId")) return;
            if (!this.ValidateIntQ("FlowID")) return;
            if (!this.ValidateIntQ("DutyDetailID")) return;
            if (!this.ValidateIntQ("NotifyID")) return;
            if (!this.ValidateIntQ("PlanID")) return;
            if (!this.ValidateIntQ("TaskID")) return;
            if (!this.ValidateIntQ("ProductID")) return;
        }        
        //2.Request key
        public static int rCompanyId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["CompanyID"]); } }
        public static int rDepartmentId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["DepartmentId"]); } }
        public static int rDeptId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["DeptId"]); } }
        public static int rDutyId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["DutyId"]); } }
        public static int rFunctionId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["FunctionId"]); } }
        public static int rMenuId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["MenuId"]); } }
        public static String rUserId { get { return Convert.ToString(HttpContext.Current.Request.QueryString["UserId"]); } }
        public static int rProjectId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectId"]); } }
        public static int rSupplierID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["SupplierID"]); } }
        public static int rContractID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["ContractID"]); } }
        public static int rCustomerID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["CustomerID"]); } }
        public static int rCustomerTempID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["CustomerTempID"]); } }
        public static int rContactID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["ContactID"]); } }
        public static int rWarehouseID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["WarehouseID"]); } }
        public static int rFormID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["FormId"]); } }
        public static int rFlowID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["FlowID"]); } }
        public static int rDutyDetailID { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["DutyDetailID"]); } }
        public static int rId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["ID"]); } }
        public static int rLicenseId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["LicenseID"]); } }
        public static int rNotifyId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["NotifyID"]); } }
        public static int rNotifyFileId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["NotifyFileId"]); } }
        public static int rPlanId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["PlanID"]); } }
        public static int rTaskId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["TaskID"]); } }
        public static int rProductId { get { return Convert.ToInt32(HttpContext.Current.Request.QueryString["ProductID"]); } }

        //3.Requested Model
        public static Company.MODEL rCompany
        {
            get { return Company.GetCache(rCompanyId); }
        }

        public static Department.MODEL rDepartment
        {
            get { return Department.GetCache(rDepartmentId); }
        }
        public static Department.MODEL rDept
        {
            get { return Department.GetCache(rDeptId); }
        }
        public static Duty.MODEL rDuty
        {
            get { return Duty.GetCache(rDutyId); }
        }
        public static Function.MODEL rFunction
        {
            get { return Function.GetCache(rFunctionId); }
        }
        public static Menu.MODEL rMenu
        {
            get { return Menu.NewDataModel(rMenuId); }
        }
        public static WX.WXUser rCurUser
        {
            get { return WX.Main.GetUser(rUserId); }
        }
        public static User.MODEL rUser
        {
            get { return User.GetCache(rUserId); }
        }
        public static Employee.MODEL rEmpolyee
        {
            get { return Employee.GetModelToID(rUserId); }
        }
        public static WX.PRO.Project.MODEL rProject
        {
            get { return WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" +rProjectId); }
        }
        public static WX.Ass.Supplier.MODEL rSupplier
        {
            get { return WX.Ass.Supplier.NewDataModel(rSupplierID); }
        }
        public static WX.CTR.Contract.MODEL rContract
        {
            get { return WX.CTR.Contract.NewDataModel(rContractID); }
        }
        public static WX.CRM.Customer.MODEL rCustomer
        {
            get { return WX.CRM.Customer.GetModel("select * from CRM_Customers where ID=" + rCustomerID); }
        }
        public static WX.CRM.Customer.MODEL rCustomerToTemp
        {
            get { return WX.CRM.Customer.GetModel("select * from CRM_CustomersTemp where ID=" + rCustomerID ); }
        }
        public static WX.CRM.Customer.MODEL rCustomerTemp
        {
            get { return  WX.CRM.Customer.GetModel("select * from CRM_Customers where ID=" + rCustomerTempID); }
        }
        public static WX.CRM.Customer.MODEL rCustomerTotempCID
        {
            get { return WX.CRM.Customer.GetModel("select * from CRM_CustomersTemp where CustomersID=" + rCustomerID); }
        }
        public static WX.CRM.CustomerTemp.MODEL rCustomerTempToRCID
        {
            get { return WX.CRM.CustomerTemp.GetModel("select * from CRM_CustomersTemp where ID=" + rCustomerID); }
        }
        public static WX.CRM.CustomerTemp.MODEL rCustomerTempToCID
        {
            get { return WX.CRM.CustomerTemp.GetModel("select * from CRM_CustomersTemp where CustomersID=" + rCustomerID ); }
        }
        public static WX.CRM.CustomerTemp.MODEL rCustomerTempToCustomer
        {
            get { return WX.CRM.CustomerTemp.GetModel("select * from CRM_Customers where ID=" + rCustomerID); }
        }
        public static WX.CRM.CustomerTemp.MODEL rCustomerTempToTempID
        {
            get { return WX.CRM.CustomerTemp.GetModel("select * from CRM_CustomersTemp where ID=" + rCustomerTempID); }
        }
        public static WX.CRM.Contact.MODEL rContact
        {
            get { return WX.CRM.Contact.NewDataModel(rContactID); }
        }
        public static WX.Ass.Warehouse.MODEL rWarehouse
        {
            get { return WX.Ass.Warehouse.NewDataModel(rWarehouseID); }
        }
        public static WX.Flow.Model.Form.MODEL rForm
        {
            get { return WX.Flow.Model.Form.NewDataModel(rFormID); }
        }
        public static WX.Flow.Model.Flow.MODEL rFlow
        {
            get { return WX.Flow.Model.Flow.GetCache(rFlowID); }
        }
        public static WX.Model.DutyDetail.MODEL rDutyDetail
        {
            get 
            {
                return WX.Model.DutyDetail.GetCache(rDutyDetailID);
                //return WX.Model.DutyDetail.GetModel(rDutyDetailID); 
            }
        }
        public static WX.XZ.Notify.MODEL rNotify
        {
            get { return WX.XZ.Notify.GetModel(rNotifyId); }
        }
        public static WX.XZ.NotifyFiles.MODEL rNotifyFile
        {
            get { return WX.XZ.NotifyFiles.GetModel(rNotifyFileId); }
        }
        public static WX.Model.Plan.MODEL rPlan
        {
            get { return WX.Model.Plan.GetModel(rPlanId); }
        }
        public static WX.Model.Task.MODEL rTask
        {
            get { return WX.Model.Task.GetModel(rTaskId); }
        }
        public static WX.PDT.Product.MODEL rProduct
        {
            get { return WX.PDT.Product.NewDataModel(rProductId); }
        }
        #region //Validate functions
        public static string ErrorMsg = "你没有权限访问此页面";
        public static bool IsNumber(string queryKey, bool printMsg_If_False)
        {
            string queryValue = Convert.ToString(HttpContext.Current.Request.QueryString[queryKey]);
            bool bR = String.IsNullOrEmpty(queryValue) ? false : ULCode.Validation.IsNumber(queryValue);
            if (!bR && printMsg_If_False) { ULCode.Debug.we(ErrorMsg); }
            return bR;
        }
        public static bool IsGuid(string queryKey, bool printMsg_If_False)
        {
            string queryValue = Convert.ToString(HttpContext.Current.Request.QueryString[queryKey]);
            bool bR = String.IsNullOrEmpty(queryValue) ? false : ULCode.Validation.IsGuid(queryValue);
            if (!bR && printMsg_If_False) { ULCode.Debug.we(ErrorMsg); }
            return bR;
        }
        public static bool IsNotNull(string queryKey, bool printMsg_If_False)
        {
            bool bR = HttpContext.Current.Request.QueryString[queryKey] != null;
            if (!bR && printMsg_If_False) { ULCode.Debug.we(ErrorMsg); }
            return bR;
        }
        private bool ValidateIntQ(string queryKey)
        {
            return WX.Request.IsNotNull(queryKey, false) && !WX.Request.IsNumber(queryKey, true);
        }
        private bool ValidateGuidQ(string queryKey)
        {
            return WX.Request.IsNotNull(queryKey, false) && !WX.Request.IsGuid(queryKey, true);
        }
        #endregion
    }
}
