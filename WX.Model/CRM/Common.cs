using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Model.CRM
{
    public enum LogType
    {
        /// <summary>
        /// 添加客户
        /// </summary>
        AddCustomer=1,
        /// <summary>
        /// 删除客户
        /// </summary>
        DeleteCustomer=2,
        /// <summary>
        /// 客户审核，业务员录入客户后，上级部门有审核权限的人审核后，不再可以修改客户信息
        /// </summary>
        CheckCustomer=3,
        /// <summary>
        /// 具有修改客户权限的人进行修改客户信息
        /// </summary>
        EditCustomer=4,
        /// <summary>
        /// 维护客户，维护过程中会发生费用
        /// </summary>
        MaintainCustomer=5,
        /// <summary>
        /// 为客户添加联系人
        /// </summary>
        AddContact=6,
        /// <summary>
        /// 修改联系人
        /// </summary>
        EditContact=7,
        /// <summary>
        /// 跟踪客户，往往是客户没有成交以前的行为
        /// </summary>
        TracertCustomer=8,
        /// <summary>
        /// 其它设置
        /// </summary>
        CRMSet=9
    }
    public class Public
    { 

    }
}
