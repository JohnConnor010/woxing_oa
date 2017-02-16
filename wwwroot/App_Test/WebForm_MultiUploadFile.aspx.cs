﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Test
{
    public partial class WebForm_MultiUploadFile : System.Web.UI.Page
    {
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        hpf.SaveAs(Server.MapPath("/UploadFiles/test") + "\\" +
                          System.IO.Path.GetFileName(hpf.FileName));
                        Response.Write("<b>File: </b>" + hpf.FileName + " <b>Size:</b> " +
                            hpf.ContentLength + " <b>Type:</b> " + hpf.ContentType + " Uploaded Successfully <br/>");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}