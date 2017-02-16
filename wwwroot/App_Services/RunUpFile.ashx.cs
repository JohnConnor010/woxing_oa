using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Diagnostics;


namespace wwwroot.App_Services
{
    /// <summary>
    /// RunUpFile 的摘要说明
    /// </summary>
    public class RunUpFile : IHttpHandler
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        private const int UploadFileLimit = 3;//上传文件数量限制

        private string _msg = "上传成功！";//返回信息
        private int _count = 0;
        private string _oldFileName;
        private string _newFileName;
        StringBuilder IDList = new StringBuilder();
        StringBuilder NameList = new StringBuilder();
        public void ProcessRequest(HttpContext context)
        {
            int iTotal = context.Request.Files.Count;

            if (iTotal == 0)
            {
                _msg = "没有数据";
            }
            else
            {
                int iCount = 0;
                for (int i = 0; i < iTotal; i++)
                {
                    HttpPostedFile file = context.Request.Files[i];
                    // 取文件后缀名
                    string houzui = Path.GetExtension(file.FileName);
                    //旧文件名
                    _oldFileName = Path.GetFileName(file.FileName);

                    // 服务器上保存的文件名称                
                    string filename = DateTime.Now.ToString("yyyyMMddhhmmss fff") + houzui;

                    if (file.ContentLength > 0 || !string.IsNullOrEmpty(file.FileName))
                    {
                        _count++;
                        //保存文件
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/UploadFiles/Run/" + filename));

                        //新文件名
                        _newFileName = "UploadFiles/Run/" + filename;
                        Guid uploadUserId = Guid.NewGuid();
                        DateTime uploadTime = DateTime.Now;
                        string uploadIp = context.Request.UserHostAddress;
                        string cmdText = "INSERT INTO FL_RunAttachs (RunId,StepNo,NewFileName,OldFileName,UploadUserID,UploadTime,UploadIP) VALUES (@RunId,@StepNo,@NewFileName,@OldFileName,@UploadUserID,@UploadTime,@UploadIP)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(cmdText, connection);
                            command.Parameters.Add("@RunId", SqlDbType.Int, 200).Value = context.Request["runid"];
                            command.Parameters.Add("@StepNo", SqlDbType.Int, 100).Value = context.Request["stepno"];
                            command.Parameters.Add("@NewFileName", SqlDbType.VarChar, 200).Value = _newFileName;
                            command.Parameters.Add("@OldFileName", SqlDbType.NVarChar, 100).Value = _oldFileName;
                            command.Parameters.Add("@UploadUserID", SqlDbType.UniqueIdentifier, 16).Value = uploadUserId;
                            command.Parameters.Add("@UploadTime", SqlDbType.DateTime, 8).Value = uploadTime;
                            command.Parameters.Add("@UploadIP", SqlDbType.VarChar, 20).Value = uploadIp;
                            try
                            {
                                int row = command.ExecuteNonQuery();
                                command.CommandText = "SELECT @@IDENTITY as  IdentityID";
                                int identityId = Convert.ToInt32(command.ExecuteScalar());
                                if (row > 0)
                                {
                                    IDList.Append(identityId + ",");
                                    NameList.Append(_oldFileName + ",");
                                }
                            }
                            catch (SqlException ex)
                            {
                                _msg = ex.Message;
                            }
                        }
                    }
                }
            }
            string idlist = IDList.ToString().TrimEnd(',');
            string namelist = NameList.ToString().TrimEnd(',');
            context.Response.Write("<script>window.parent.UploadCompleted('" + namelist + "','" + idlist + "','" + _count + "','" + _msg + "');</script>");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}