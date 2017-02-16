﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.App_Demo
{
    public partial class DefaultListPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {                
                //UI专用测试数据
                string[] datas = new String[]
                {
                    "id,name,tel,address",
                    "1,szp,85071163,",
                    "2,zdx,12343211233,",
                    "3,yjy,838402349,",
                    "4,wxd,9234e2329,"
                };
                GridView1.DataSource = WX.Main.GetTestDataTable(datas);
                GridView1.DataBind();
            }
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            //获取id
            //*******************************************************
            //1.验证用户权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            ULCode.Debug.we(String.Format("已经收到id:{0}", id));
            return;
            
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
                 //填写主要业务逻辑代码

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(LogType.Default, "删除用户信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据
                //

                ULCode.Debug.Alert(this,"删除用户成功！");
            }
            else
            {
                ULCode.Debug.Alert(this,"添加用户失败！");
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }
}