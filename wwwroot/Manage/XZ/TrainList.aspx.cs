using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class TrainList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            if (start)
            {
                int count = WX.XZ.Train.GeCount("");
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.XZ.Train.GetPageList("", -1, "order by Runtime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            WX.XZ.Train.MODEL trainmodel = WX.XZ.Train.NewDataModel(lb.CommandName);
            string title = trainmodel.Title.ToString();
            int iR = trainmodel.Del();
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除({0})成功！", title), "");
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                this.BindData(false);
            }
            else
            {
                ULCode.Debug.Alert(this, "删除失败！");
            }
        }
        //向参与人发送消息
        protected void Sendmes(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;


            WX.XZ.Train.MODEL trainmodel = WX.XZ.Train.NewDataModel(lb.CommandName);
            string[] users = trainmodel.UsersID.ToString().Split(',');
            for (int i = 0; i < users.Length; i++)
            {
                string url = "/Manage/XZ/TrainDetail.aspx?TrainID=" + lb.CommandName;
                WX.Main.MessageSend("<a href=" + url + ">" + trainmodel.Title.ToString() + "</a>", url, users[i], trainmodel.UserID.ToString(), 6, 1);
            }
            ULCode.Debug.Alert(this, "发送完毕！");
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataTable dt = WX.XZ.TrainUsers.GetUsersList(Convert.ToInt32(e.Row.Cells[3].Text.Trim()));
                string textstr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["RunID"].ToString()!="")
                        textstr += "<a style='font-weight:bold;text-decoration:underline;' title='已提交学习心得'";
                    else if (dt.Rows[i]["State"].ToString() == "2")
                        textstr += "<a style='font-weight:bold;' title='已参与'";
                    else if (dt.Rows[i]["State"].ToString() == "1")
                        textstr += "<a title='已接收'";
                    else
                        textstr += "<a style='color:#888;' title='未接收'";
                    textstr += " href='TrainDetail.aspx?TrainID=" + e.Row.Cells[3].Text.Trim() + "&UserID=" + dt.Rows[i]["UserID"] + "'>" + dt.Rows[i]["RealName"] + "</a>&nbsp;&nbsp;";
                }
                e.Row.Cells[3].Text = textstr;
            }
        }
        private void sendQQ()
        {
            System.Net.WebClient _client = new System.Net.WebClient(); 
            string postValues = "VER=1.0&amp;CMD=Query_Stat&amp;SEQ=540882930&amp;UIN=2560985023&amp;TN=50&amp;UN=0"; 
            Byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(postValues);
            Byte[] pageData = _client.UploadData("Host","POST",byteArray); 
        }
    }
}