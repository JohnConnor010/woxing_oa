using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULCode.QDA;

namespace WX
{
    public class CommonUtils
    {
        /// <summary>
        /// 根据用户ID获取用户姓名
        /// </summary>
        /// <param name="guidList">用户ID字符串，中间用,分割</param>
        /// <returns>用户姓名</returns>
        public static string GetRealNameListByUserIdList(string guidList)
        {
            if (guidList == "*") return "所有人员";
            string con = !string.IsNullOrEmpty(guidList)?" where UserID in ('" + guidList.Replace(",", "','") + "')":" where 1<>1";
            return XSql.GetXDataTable("SELECT RealName FROM TU_Users" + con).ToColValueList();
        }
        /// <summary>
        /// 根据部门ID获取部门名称，部门ID用,分割
        /// </summary>
        /// <param name="deptIdList">部门ID字符串</param>
        /// <returns>部门名称</returns>
        public static string GetDeptNameListByDeptIdList(string deptIdList)
        {
            if (deptIdList == "*") return "所有部门";
            string con = !string.IsNullOrEmpty(deptIdList) ? " where ID in (" + deptIdList + ")" : " where 1<>1";
            return XSql.GetXDataTable("SELECT Name FROM TE_Departments" + con + "").ToColValueList();
        }

        /// <summary>
        /// 根据职务ID获取职务名称，职务用,分割
        /// </summary>
        /// <param name="dutyIdList">职务ID字符串</param>
        /// <returns>职务名称</returns>
        public static string GetDutyNameListByDutyIdList(string dutyIdList)
        {
            if (dutyIdList == "*") return "所有职务";
            string con = !string.IsNullOrEmpty(dutyIdList) ? " where ID in (" + dutyIdList + ")" : " where 1<>1";
            return XSql.GetXDataTable("SELECT Name FROM TE_Duties" + con + "").ToColValueList();
        }
        /// <summary>
        /// 根据条件获取用户编号多个编号用,分割
        /// </summary>
        /// <param name="Wherestr">null或“”将查询全部</param>
        /// <returns></returns>
        public static string GetUserIDListByWhereStr(int topN ,string Wherestr)
        {
            string con = !string.IsNullOrEmpty(Wherestr) ? " where " + Wherestr : "";
            return XSql.GetXDataTable("SELECT"+(topN>0?" top "+topN:"")+" UserID FROM TU_Users" + con + "").ToColValueList("','");
            
        }
        /// <summary>
        /// 根据条件获取部门负责人用户编号多个编号用,分割
        /// </summary>
        /// <param name="Wherestr">null或“”将查询全部</param>
        /// <returns></returns>
        public static string GetUserIDListByDeptID(int topN,string clom,int id)
        {
                string users =XSql.GetDataTable("SELECT " + clom + " FROM TE_Departments where ID="+id).Rows[0][0].ToString();
            if(users!="")
                users = XSql.GetXDataTable("SELECT" + (topN > 0 ? " top " + topN : "") + " UserID FROM TU_Users where UserID in('" + users.Replace(",","','") + "') and State in(10,20) order by Grade desc").ToColValueList("','");
            return users;
        }
        /// <summary>
        /// 获取某部门人员编号
        /// </summary>
        /// <param name="topN">人员数量</param>
        /// <param name="dept">部门编号</param>
        public static string GetDeptUserID(int topN,string clom,int dept)
        {
            string users = GetUserIDListByDeptID(topN,clom, dept);
            if(users=="")
                users=GetUserIDListByWhereStr(topN, "DepartmentID="+dept+" and State in(10,20) order by Grade desc");
            return users;
        }
        /// <summary>
        /// 获取人力资源部部门主管编号
        /// </summary>
        /// <returns></returns>
        public static string GetHRUserID
        {
            get
            {
                return GetDeptUserID(1, "[Host]",Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["Dept_HR"]));
            }
        }
        /// <summary>
        /// 获取行综合管理中心部门主管编号
        /// </summary>
        /// <returns></returns>
        public static string GetCAUserID
        {
            get
            {
                return GetDeptUserID(1, "[Host]", Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Dept_CA"]));
            }
        }
        /// <summary>
        /// 获取行政部部门主管编号
        /// </summary>
        /// <returns></returns>
        public static string GetAdminUserID
        {
            get
            {
                return GetDeptUserID(1, "[Host]", Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Dept_Admin"]));
            }
        }
        /// <summary>
        /// 获取财务部部门主管编号
        /// </summary>
        /// <returns></returns>
        public static string GetFDUserID
        {
            get
            {
                return GetDeptUserID(1, "[Host]", Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Dept_FD"]));
            }
        }
        /// <summary>
        /// 获取总经理办公室最高职位编号
        /// </summary>
        /// <returns></returns>
        public static string GetBossUserID
        {
            get
            {
                return GetDeptUserID(1, "[SubHosts]", Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Dept_Boss"]));
            }
        }
        /// <summary>
        /// 获取上级部门信息主管/副主管/助理
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="HostName">[Host]主管,[SubHosts]副经理，[Assistants]助理</param>
        /// <returns></returns>
        public static string GetParentDeptHost(int DeptID,string HostName)
        {
            string userid="";
            WX.Model.Department.MODEL dept = WX.Model.Department.NewDataModel(DeptID);
            if (dept.ParentID.ToInt32() > 0)
                userid = ULCode.QDA.XSql.GetValue( "select "+HostName+" from TE_Departments where ID=" + dept.ParentID.ToString()).ToString();
            WX.Model.User.MODEL user = WX.Model.User.NewDataModel(userid);
            if (userid == "" || user == null || user.State.ToInt32() < 10 || user.State.ToInt32() >= 40)
            {
                userid = ULCode.QDA.XSql.GetValue("UserID", "select top 1 * from TU_Users where DepartmentID=" + dept.ParentID.ToString() + " and State>=10 and State<40 order by Grade desc").ToString();
                user = WX.Model.User.NewDataModel(userid);
                if (user != null)
                    return user.UserID.ToString();
            }
            else
                return user.UserID.ToString();
            return "";

        }
    }
}
