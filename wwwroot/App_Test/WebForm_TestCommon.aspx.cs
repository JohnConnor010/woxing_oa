using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
namespace wwwroot.App_Test
{
    public partial class WebForm_TestCommon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Response.Write("xxx1");
            Response.End();
            ULCode.QDA.XSql.Execute("update Ass_Unit set UnitName='本2' where Id=18");
             */
            Response.Write(Bind.Flag.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /*
            string sSql = "select Name from te_companys where ID=11";
            DbResult dr = ULCode.QDA.XSql.GetData_R(null, sSql);
            if (dr.OK)
            {
                Response.Write(dr.ToDbValue().ToStr()); 
            }*/
            ULCode.KeyXmlString kxs0 = new ULCode.KeyXmlString();
            ULCode.KeyXmlString kxs = new ULCode.KeyXmlString();
            kxs.SetItemValue("姓名", "孙战平");
            kxs.SetItemValue("性别", "男");
            kxs.SetItemValue("政治面貌", "无");
            kxs0.AddItem("Node",kxs.GetSavedData());

            ULCode.KeyXmlString kxs1 = new ULCode.KeyXmlString();
            kxs1.SetItemValue("姓名", "孙战平1");
            kxs1.SetItemValue("性别", "男1");
            kxs1.SetItemValue("政治面貌", "无1");
            kxs0.AddItem("Node", kxs1.GetSavedData());
            
            Response.Write(kxs0.GetSavedData());


            ULCode.KeyXmlString kxs10 = new ULCode.KeyXmlString();
            kxs10.LoadData(kxs0.GetSavedData());
            foreach (String s in kxs10.GetItemValues("Node"))
            {
                ULCode.KeyXmlString kxs9 = new ULCode.KeyXmlString();
                kxs9.LoadData(s);
                Response.Write(String.Format("<hr/>姓名：{0} 性别：{1} 政治面貌：{2}"
                    ,kxs9.GetItemValue("姓名")
                    ,kxs9.GetItemValue("性别")
                    ,kxs9.GetItemValue("政治面貌")));
            }
        }
    }
}