using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace wwwroot.NewFolder
{
    public partial class Delfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShosFolder();
            }
        }
        private void ShosFolder()
        {
           // SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString());

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FolderName"]));
            //return;

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.Text;
            int id = 1;
            foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}
                //cmd.CommandText = "SELECT  *  FROM [huanews].[dbo].[Class] where   ClassDir='" + dChild.Name + "'";
                //SqlDataReader sdr = cmd.ExecuteReader();
                //if (!sdr.Read())
                //{
                //    id++;
                //    //如果用GetDirectories("ab*"),那么全部以ab开头的目录会被显示
                //    Response.Write(id + "---" + dChild.FullName + "-----" + cmd.CommandText + "<BR>");//打印路径和目录名

                //}
                //else 
                    if (dChild.GetFiles().Length == 1 && dChild.GetFiles()[0].Name == "index.html")
                {
                    id++;
                    Response.Write(id + "---" + dChild.FullName +"<BR>");//打印路径和目录名
                    foreach (string d in Directory.GetFileSystemEntries(dChild.FullName))
                    {
                        Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + d+ "<BR>");
                    }
                }
                    //con.Close();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteFolder(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FolderName"]));
            ShosFolder();
        }
        private void DeleteFolder(string path)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString());

            DirectoryInfo dir = new DirectoryInfo(path);
            //return;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            int id = 1;
            foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
            {


                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}
                //cmd.CommandText = "SELECT  *  FROM [huanews].[dbo].[Class] where   ClassDir='" + dChild.Name + "'";
                //SqlDataReader sdr = cmd.ExecuteReader();
                //if (!sdr.Read())
                //{
                //    id++;

                //    foreach (string d in Directory.GetFileSystemEntries(dChild.FullName))
                //    {
                //        if (File.Exists(d))
                //            File.Delete(d); //直接删除其中的文件                          
                //        else
                //            DeleteFolder(d); //递归删除子文件夹   
                //    }
                //    Directory.Delete(dChild.FullName, true); //删除已空文件夹  

                //    //如果用GetDirectories("ab*"),那么全部以ab开头的目录会被显示
                //    Response.Write(id + "---" + dChild.FullName + "-----已删除！<BR>");//打印路径和目录名

                //}
                //else
                    if (dChild.GetFiles().Length==1 && dChild.GetFiles()[0].Name == "index.html")
                {
                    id++;
                    foreach (string d in Directory.GetFileSystemEntries(dChild.FullName))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件                          
                        else
                            DeleteFolder(d); //递归删除子文件夹   
                    }
                    Directory.Delete(dChild.FullName, true); //删除已空文件夹  
                }
                //con.Close();
            }
            Response.Write("删除完毕！共删除文件夹数："+id);
        }
    }
}