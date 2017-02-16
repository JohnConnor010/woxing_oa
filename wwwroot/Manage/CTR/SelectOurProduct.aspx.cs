using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CTR
{
    public partial class SelectOurProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            DataTable productData = XSql.GetDataTable("SELECT P.*,C.Name FROM PDT_Products AS P LEFT JOIN PDT_ProductCategory AS C ON P.CategoryID=C.ID");
            this.ProductRepeater.DataSource = productData;
            this.ProductRepeater.DataBind();
        }
    }
}