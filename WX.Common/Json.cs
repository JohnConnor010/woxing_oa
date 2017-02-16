using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace WX.Json
{
    public class JsonConvert
    {
        public static T GetJsonObject<T>(string jsonString, bool objectIsSerialized)
        {
            if (objectIsSerialized)
            {
                using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
                { return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms); }
            }
            else
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                try
                {
                    return jss.Deserialize<T>(jsonString);
                }
                catch
                {
                    return default(T);
                }
            }
        }
        public static string GetJsonString(object jsonObject, bool objectIsSerialized)
        {
            if (objectIsSerialized)
            {
                using (var ms = new MemoryStream())
                {
                    new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                    return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //try
                //{
                //    return jss.Serialize(jsonObject);
                //}
                //catch
                //{
                //    return String.Empty;
                //}
            }
        }
    }
    //用于Json组织菜单单元
    //[DataContract]
    //public class UserMenu
    //{
    //    [DataMember(Order = 0)]
    //    public string menuid { get; set; }
    //    [DataMember(Order = 1)]
    //    public string menuname { get; set; }
    //    [DataMember(Order = 2)]
    //    public string icon { get; set; }
    //    [DataMember(Order = 3)]
    //    public string url { get; set; }
    //    [DataMember(Order = 4)]
    //    public List<UserMenu> menus { get; set; }
    //}
    public class UserMenu
    {
        public string menuid { get; set; }
        public string menuname { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public List<UserMenu> menus { get; set; }
        public List<UserMenu> child { get; set; }
    }
    //生成用户Menu类
    public class BuildUserMenus
    {
        private int DutyId;
        
        private DataTable DTBase = null;
        public BuildUserMenus(int dutyId)
        {
            this.DutyId = dutyId;
        }
        public string GetUserMenu()
        {
            string sSql = "select Id as menuid,Name as menuname,ParentId as parentid,Url as url,Icon as icon,Degree as degree from TE_Menus where Id in (select MenuId from te_MenusInDuties where DutyId="+this.DutyId+" and Flag>0) order by OrderId;";
            this.DTBase = ULCode.QDA.XSql.GetDataTable(sSql);
            List<UserMenu> UserMenus = this.CreateMenuByParentId(0);
            return Json.JsonConvert.GetJsonString(UserMenus, false);
        }
        private List<UserMenu> CreateMenuByParentId(int id)
        {
            List<UserMenu> userMenus = new List<UserMenu>();
            DataRow[] drs = this.DTBase.Select("parentid=" + id);
            foreach (DataRow dr in drs)
            {
                List<UserMenu> userMenus_childs = this.CreateMenuByParentId(Convert.ToInt32(dr["menuid"]));
                UserMenu umNew = new UserMenu()
                 {
                     menuid = Convert.ToString(dr["menuid"])
                   ,
                     menuname = Convert.ToString(dr["menuname"])
                   ,
                     icon = Convert.ToString(dr["icon"])
                   ,
                     url = Convert.ToString(dr["url"])
                 };
                if (userMenus_childs.Count > 0)
                {
                    switch (Convert.ToInt32(dr["degree"]))
                    {
                        case 1: umNew.menus = userMenus_childs; break;
                        case 2: umNew.child = userMenus_childs; break;
                    }

                }
                userMenus.Add(umNew);

            }
            return userMenus;
        }
    }
}
