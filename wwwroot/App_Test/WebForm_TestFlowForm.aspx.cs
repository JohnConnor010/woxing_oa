using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow;
namespace wwwroot.App_Test
{
    public partial class WebForm_TestFlowForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region  //1.测试FormField
            //FormField ff = new FormField("Data_1{$||}当前用户名{$||}SYSCurUSER", FormFieldDataType.Item);
            //ff.Id = "Data_1";
            //ff.Text = "当前用户名";
            //ff.Type = "SYSCurUSER";
            //ff.Value = "杨金勇";
            //Response.Write(ff.GetSavedData(FormFieldDataType.Data));
            #endregion

            #region //2.测试FormFieldCollection
            /*
            WX.Flow.Model.Form.MODEL form=WX.Flow.Model.Form.NewDataModel();
            FormFieldCollection ffc = new FormFieldCollection();
            foreach(sdf)
            {
                FormField fNew=new FormField();
                fNew.Id="Data_1";
                fNew.Text="当前部门";
                fNew.Type="SYSCurDept";
                ffc.Add(fNew);
            }
            form.Items_FormFieldCollection=ffc;
            
            //form.Items.set(ffc.GetSavedDatas(FormFieldDataType.Item));

            FormFieldCollection ffcGet=form.Items_FormFieldCollection;
            //FormFieldCollection ffcGet=new FormFieldCollection(form.Items.ToString(), FormFieldDataType.Item);
            foreach(FormField ff in ffcGet)
            {
                ff.Value=Request.Form[ff.Id];
            }
            ss.Datas_fasd=ffcGet;

            ffc.Add(new FormField("Data_1{$||}当前用户名{$||}SYSCurUSER", FormFieldDataType.Item));
            ffc.Add(new FormField("Data_2{$||}当前部门{$||}SYSCurDept", FormFieldDataType.Item));
            ffc.Add(new FormField("Data_3{$||}当前时间{$||}SYSCurTime", FormFieldDataType.Item));
            foreach (FormField ff in ffc)
                Response.Write("<br/>" + ff.GetSavedData(FormFieldDataType.Item));
            Response.Write(ffc.GetSavedDatas(FormFieldDataType.Item));
            */
            #endregion

            //WX.Data.Dict.BindListCtrl_DeptList(DropDownList1, true);
            //WX.Data.Dict.BindListCtrl_FormCatagory(DropDownList1,"0#--请选择--",null);

        }
    }
}