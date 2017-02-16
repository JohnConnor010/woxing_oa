using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Management;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_Assign_Show : System.Web.UI.Page
    {
        public int state = 0;
        private WX.WXUser CurUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CurUser = WX.Main.CurUser;
            if (!IsPostBack)
            {
                PageInit();
            }
        }
        private void PageInit()
        {
            if (Request["OrderID"] != null && Request["OrderID"] != "")
            {
                WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
                YJTime_my.Text = order.YJTime.ToString();
                FPTime_my.Text = WX.CommonUtils.GetRealNameListByUserIdList(order.AssignUserID.ToString()) + "&nbsp;&nbsp;" + order.FPTime.ToString();
                AddTime_my.Text = order.AddTime.ToString();
                YSTime_my.Text = order.YSTime.ToString();
                StopTime_my.Text = order.StopTime.ToString();
                Count_my.Text = order.Count.ToString();
                State_my.Text = WX.WorkOrder.Order.StateStr[order.State.ToInt32()];
                WX.WorkOrder.Order.MODEL porder = WX.WorkOrder.Order.NewDataModel(order.PID.ToString());
                Title_li.Text = porder.Title.ToString() + "-" + GetMacAddress();
                if (porder.UserID.ToString() == this.CurUser.UserID)
                    Title_li.Text = "<a style='font-weight:bold;' href='WorkOrder_Show.aspx?OrderID="+porder.ID.ToString()+"'>>> " + Title_li.Text + "</a>";
                Proj_li.Text = WX.WorkOrder.Order.ProjStr[porder.Proj.ToInt32()];
                Type_li.Text = WX.WorkOrder.Order.TypeStr[porder.Type.ToInt32()];
                YJTime_li.Text = porder.YJTime.ToString();
                StateTime_la.Text = WX.CommonUtils.GetRealNameListByUserIdList(porder.UserID.ToString()) + "&nbsp;&nbsp;" + porder.AddTime.ToString();
                SubTime_li.Text = porder.SubTime.ToString();
                FPTime_li.Text = porder.YSTime.ToString();
                StopTime_li.Text = porder.StopTime.ToString();
                State_li.Text = WX.WorkOrder.Order.StateStr[porder.State.ToInt32()];
                FS_drop.Items.Add(new ListItem("@" + WX.CommonUtils.GetRealNameListByUserIdList(porder.UserID.ToString()), porder.UserID.ToString()));
                if (porder.UserID.ToString() != order.ExecUserID.ToString())
                    FS_drop.Items.Add(new ListItem("@" + WX.CommonUtils.GetRealNameListByUserIdList(order.ExecUserID.ToString()), order.ExecUserID.ToString()));
                state = order.State.ToInt32();
                Remarks_li.Text = WX.WorkOrder.Order.EnCoding(porder.Remarks.ToString());
                if (order.State.ToInt32() > 3)
                {
                    MessBind(porder.ID.ToInt32());
                    mess.Visible = true;
                    if (order.State.ToInt32() == 6 || order.State.ToInt32() == 8)
                    {
                        messfs.Visible = false;
                        mess.Width = "418px";
                    }
                    if (order.State.ToInt32() == 8)
                        pingjia.Visible = true;
                    if (order.State.ToInt32() >= 8)
                    {
                        AppBind();
                        pingjiafs.Visible = true;
                        pingjiafs.Width = "418px";
                    }
                }
            }
        }
        public static string GetMacAddress()
        {
            string addr = "";
            try
            {
                int cb;
                ASTAT adapter;
                NCB Ncb = new NCB();
                char uRetCode;
                LANA_ENUM lenum;

                Ncb.ncb_command = (byte)NCBCONST.NCBENUM;
                cb = Marshal.SizeOf(typeof(LANA_ENUM));
                Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                Ncb.ncb_length = (ushort)cb;
                uRetCode = Win32API.Netbios(ref Ncb);
                lenum = (LANA_ENUM)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(LANA_ENUM));
                Marshal.FreeHGlobal(Ncb.ncb_buffer);
                if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                    return "";

                for (int i = 0; i < lenum.length; i++)
                {
                    Ncb.ncb_command = (byte)NCBCONST.NCBRESET;
                    Ncb.ncb_lana_num = lenum.lana[i];
                    uRetCode = Win32API.Netbios(ref Ncb);
                    if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                        return "";

                    Ncb.ncb_command = (byte)NCBCONST.NCBASTAT;
                    Ncb.ncb_lana_num = lenum.lana[i];
                    Ncb.ncb_callname[0] = (byte)'*';
                    cb = Marshal.SizeOf(typeof(ADAPTER_STATUS)) + Marshal.SizeOf(typeof(NAME_BUFFER)) * (int)NCBCONST.NUM_NAMEBUF;
                    Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                    Ncb.ncb_length = (ushort)cb;
                    uRetCode = Win32API.Netbios(ref Ncb);
                    adapter.adapt = (ADAPTER_STATUS)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(ADAPTER_STATUS));
                    Marshal.FreeHGlobal(Ncb.ncb_buffer);

                    if (uRetCode == (short)NCBCONST.NRC_GOODRET)
                    {
                        if (i > 0)
                            addr += ":";
                        addr = string.Format("{0,2:X}{1,2:X}{2,2:X}{3,2:X}{4,2:X}{5,2:X}",
                        adapter.adapt.adapter_address[0],
                        adapter.adapt.adapter_address[1],
                        adapter.adapt.adapter_address[2],
                        adapter.adapt.adapter_address[3],
                        adapter.adapt.adapter_address[4],
                        adapter.adapt.adapter_address[5]);
                    }
                }
            }
            catch
            {
            }
            return addr.Replace(' ', '0');
        }
        private void AppBind()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from WorkOrder_Appraisal where WID=" + Request["OrderID"] + " order by AddTime desc");
            DataList2.DataSource = dt;
            DataList2.DataBind();
            WX.Main.ExcuteUpdate("WorkOrder_Appraisal", "State=1", "WID=(select ID from WorkOrder_Orders where ID=" + Request["OrderID"] + " and ExecUserID='" + this.CurUser.UserID + "') and State=0");
        }
        private void MessBind(int porderid)
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from WorkOrder_Message where WID=" + Request["OrderID"] + " or WID=" + porderid + " order by AddTime desc");
            DataList1.DataSource = dt;
            DataList1.DataBind();
            WX.Main.ExcuteUpdate("WorkOrder_Message", "State=1", "WID=" + Request["OrderID"] + " and ToUserID='" + this.CurUser.UserID + "' and State=0");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.WorkOrder.Message.MODEL mess = WX.WorkOrder.Message.NewDataModel();
            mess.WID.value = Request["OrderID"];
            mess.FromUserID.value = this.CurUser.UserID;
            mess.ToUserID.value = FS_drop.SelectedValue;
            mess.Remarks.value = MessContent_txt.Text;
            mess.Insert();
            MessContent_txt.Text = "";
            WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            MessBind(order.PID.ToInt32());
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            LinkButton bu1 = (LinkButton)sender;
            if (FS_drop.Items.FindByValue(bu1.CommandArgument) == null)
                FS_drop.Items.Add(new ListItem("@" + WX.CommonUtils.GetRealNameListByUserIdList(bu1.CommandArgument), bu1.CommandArgument));
            FS_drop.SelectedValue = bu1.CommandArgument;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            WX.WorkOrder.Appraisal.MODEL app = WX.WorkOrder.Appraisal.NewDataModel();
            app.WID.value = Request["OrderID"];
            app.UserID.value = this.CurUser.UserID;
            app.Remarks.value = AppContent_txt.Text;
            app.Insert();
            MessContent_txt.Text = "";
            AppBind();
        }
    }

    public enum NCBCONST
    {
        NCBNAMSZ = 16, /**//**//**//* absolute length of a net name */
        MAX_LANA = 254, /**//**//**//* lana's in range 0 to MAX_LANA inclusive */
        NCBENUM = 0x37, /**//**//**//* NCB ENUMERATE LANA NUMBERS */
        NRC_GOODRET = 0x00, /**//**//**//* good return */
        NCBRESET = 0x32, /**//**//**//* NCB RESET */
        NCBASTAT = 0x33, /**//**//**//* NCB ADAPTER STATUS */
        NUM_NAMEBUF = 30, /**//**//**//* Number of NAME's BUFFER */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ADAPTER_STATUS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] adapter_address;
        public byte rev_major;
        public byte reserved0;
        public byte adapter_type;
        public byte rev_minor;
        public ushort duration;
        public ushort frmr_recv;
        public ushort frmr_xmit;
        public ushort iframe_recv_err;
        public ushort xmit_aborts;
        public uint xmit_success;
        public uint recv_success;
        public ushort iframe_xmit_err;
        public ushort recv_buff_unavail;
        public ushort t1_timeouts;
        public ushort ti_timeouts;
        public uint reserved1;
        public ushort free_ncbs;
        public ushort max_cfg_ncbs;
        public ushort max_ncbs;
        public ushort xmit_buf_unavail;
        public ushort max_dgram_size;
        public ushort pending_sess;
        public ushort max_cfg_sess;
        public ushort max_sess;
        public ushort max_sess_pkt_size;
        public ushort name_count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NAME_BUFFER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] name;
        public byte name_num;
        public byte name_flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NCB
    {
        public byte ncb_command;
        public byte ncb_retcode;
        public byte ncb_lsn;
        public byte ncb_num;
        public IntPtr ncb_buffer;
        public ushort ncb_length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] ncb_callname;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] ncb_name;
        public byte ncb_rto;
        public byte ncb_sto;
        public IntPtr ncb_post;
        public byte ncb_lana_num;
        public byte ncb_cmd_cplt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ncb_reserve;
        public IntPtr ncb_event;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LANA_ENUM
    {
        public byte length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.MAX_LANA)]
        public byte[] lana;
    }

    [StructLayout(LayoutKind.Auto)]
    public struct ASTAT
    {
        public ADAPTER_STATUS adapt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NUM_NAMEBUF)]
        public NAME_BUFFER[] NameBuff;
    }
    public class Win32API
    {
        [DllImport("NETAPI32.DLL")]
        public static extern char Netbios(ref NCB ncb);
    }
}