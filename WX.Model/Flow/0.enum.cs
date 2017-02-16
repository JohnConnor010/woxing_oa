using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Flow
{    
    /// <summary>
    /// 流程类型
    /// </summary>
    public enum FlowType
    {
        /// <summary>
        /// 固定流程
        /// </summary>
        FixedFlow = 1, 
        /// <summary>
        /// 自由流程
        /// </summary>
        FreeFlow = 2   
    }
    /// <summary>
    /// 流程委托模式
    /// </summary>
    public enum FlowAuthorizeMode
    {
        /// <summary>
        /// 禁止委托
        /// </summary>
        NoAuthorize = 0,
        /// <summary>
        /// 自由委托
        /// </summary>
        FreeAuthorize = 1,
        /// <summary>
        /// 按步骤设置的经办权限委托
        /// </summary>
        PrivInStep = 2,    
        /// <summary>
        /// 仅允许委托当前步骤经办人
        /// </summary>
        OnlyCurStepOp = 3  
    }
    /// <summary>
    /// 管理类型
    /// </summary>
    public enum ManageType
    {
        /// <summary>
        /// 管理
        /// </summary>
        Manage=1,
        /// <summary>
        /// 监控
        /// </summary>
        Monitor=2,
        /// <summary>
        /// 查询
        /// </summary>
        Query=3,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit=4,
        /// <summary>
        /// 点评
        /// </summary>
        Remark=5
    }
    /// <summary>
    /// 管理范围
    /// </summary>
    public enum ManageScope
    {
        /// <summary>
        /// 本机构
        /// </summary>
        MyUnit = 1,
        /// <summary>
        /// 本部门
        /// </summary>
        MyDepartment = 2,
        /// <summary>
        /// 所有部门
        /// </summary>
        AllDepartment = 3,
        /// <summary>
        /// 自定义部门
        /// </summary>
        SelfDepartment = 4
    }
    /// <summary>
    /// 提醒类型
    /// </summary>
    public enum RemindType
    {
        /// <summary>
        /// 仅此一次
        /// </summary>
        Once = 1,
        /// <summary>
        /// 每天一次
        /// </summary>
        EveryDay = 2,
        /// <summary>
        /// 每周一次
        /// </summary>
        EveryWeek = 3,
        /// <summary>
        /// 每月一次
        /// </summary>
        EveryMonth = 4,
        /// <summary>
        /// 每年一次
        /// </summary>
        EveryYear = 5
    }
    /// <summary>
    /// 流程步骤节点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 步骤节点
        /// </summary>
        DefaultNode=1,
        /// <summary>
        /// 子节点
        /// </summary>
        ChildNode=2,
        /// <summary>
        /// 外部流程链接节点
        /// </summary>
        OuterNode=3
    }
    /// <summary>
    /// 步骤自动选人类型
    /// </summary>
    public enum AutoSelOpType
    {
        /// <summary>
        /// 不进行自动选择
        /// </summary>
        None=0,
        /// <summary>
        /// 自动选择流程发起人
        /// </summary>
        AutoSelBeginner=1,
        /// <summary>
        /// 自动选择本部门主管
        /// </summary>
        AutoSelMyHost=2,
        /// <summary>
        /// 自动选择本部门助理
        /// </summary>
        AutoSelMyAssistant=3,
        /// <summary>
        /// 自动选择上级部门主管领导
        /// </summary>
        AutoSelUpHost=4,
        /// <summary>
        /// 自动选择上级部门分管领导
        /// </summary>
        AutoSelUpLeader=5,
        /// <summary>
        /// 自动选择一级部门主管
        /// </summary>
        AutoSelTopHost=6,
        /// <summary>
        /// 指定自动选择默认人员
        /// </summary>
        SelfDefaultOp=7,
        /// <summary>
        /// 按表单字段选择
        /// </summary>
        SelfByField=8,
        /// <summary>
        /// 自动选择指定步骤主办人
        /// </summary>
        AutoSelStepOp=9,
        /// <summary>
        /// 自动选择本部门符合条件的所有人员
        /// </summary>
        AutoSelOpsInMyDept=10,
        /// <summary>
        /// 自动选择一级部门内符合条件的所有人员
        /// </summary>
        AutoSelOpsInTopDept=11
 
    }
    /// <summary>
    /// 步骤智能过滤人选项
    /// </summary>
    public enum AutoSelOpFilter
    {
        /// <summary>
        /// 允许选择全部的经办人
        /// </summary>
        AllowSelAllOp=1,
        /// <summary>
        /// 允许选择本部门的经办人
        /// </summary>
        AllowSelMyDept=2,
        /// <summary>
        /// 允许选择上级部门的经办人
        /// </summary>
        AllowSelUpDept=3,
        /// <summary>
        /// 允许选择下级部门的经办人
        /// </summary>
        AllowSelDownDept=4
    }
    /// <summary>
    /// 步骤主办人选项
    /// </summary>
    public enum AutoOpMode
    {
        /// <summary>
        /// 无主办人会签
        /// </summary>
        NoneOp = 0,
        /// <summary>
        /// 明确指定主办人
        /// </summary>
        AssignOp = 1,
        /// <summary>
        /// 先接收者为主办人
        /// </summary>
        FirstAsOp = 2
    }
    /// <summary>
    /// 步骤会签模式
    /// </summary>
    public enum SignMode
    {
        /// <summary>
        /// 禁止会签
        /// </summary>
        NoSign = 0,
        /// <summary>
        /// 允许会签
        /// </summary>
        AllowSign = 1,
        /// <summary>
        /// 强制会签
        /// </summary>
        ForceSign = 2
    }
    /// <summary>
    /// 步骤会签可见性
    /// </summary>
    public enum SignLookMode
    {
        /// <summary>
        /// 总是可见
        /// </summary>
        AlwaysVisible=1,
        /// <summary>
        /// 针对本步骤之间不可见
        /// </summary>
        HiddenAmongThisStep=2,
        /// <summary>
        /// 针对其它步骤不可见
        /// </summary>
        HiddenForOtherSteps=3 
    }
    /// <summary>
    /// 步骤回退模式
    /// </summary>
    public enum RollBackMode
    {
        /// <summary>
        /// 禁止回退
        /// </summary>
        NoRollBack=0,
        /// <summary>
        /// 允许回退一步
        /// </summary>
        AllowRollBackOneStep=1,
        /// <summary>
        /// 允许回退到之前
        /// </summary>
        AllowRollBack=2
    }
    /// <summary>
    /// 步骤并发选项
    /// </summary>
    public enum Sync_DealMode
    {
        /// <summary>
        /// 不允许并发
        /// </summary>
        NoSync = 0,
        /// <summary>
        /// 允许并发
        /// </summary>
        AllowSync = 1,
        /// <summary>
        /// 强制并发
        /// </summary>
        ForceSync = 2
    }
    /// <summary>
    /// 步骤并发时合并选项
    /// </summary>
    public enum Sync_CombineMode
    {
        /// <summary>
        /// 非强制合并
        /// </summary>
        NoForceCombine = 0,
        /// <summary>
        /// 强制合并
        /// </summary>
        ForceCombine = 1
    }
    /// <summary>
    /// 自动编号开始模式
    /// </summary>
    public enum NumberAutoMode
    { 
        /// <summary>
        /// 永远自动编号
        /// </summary>
        Always=0,
        /// <summary>
        /// 每年一次
        /// </summary>
        InYear=1,
        /// <summary>
        /// 每月一次
        /// </summary>
        InMonth=2,
        /// <summary>
        /// 每日一次
        /// </summary>
        InDate=3
    }
    /// <summary>
    /// 用户自动编号修改模式
    /// </summary>
    public enum NumberUserMode
    {
        /// <summary>
        /// 不允许修改
        /// </summary>
        NotModify=0,
        /// <summary>
        /// 允许修改
        /// </summary>
        AllowModify=1,
        /// <summary>
        /// 仅允许添加前缀
        /// </summary>
        AllowInsertIntoHead=2,
        /// <summary>
        /// 仅允许添加后缀
        /// </summary>
        AllowInsertIntoEnd=3,
        /// <summary>
        /// 仅允许添加前后缀
        /// </summary>
        AllowInsertHeadAndEnd=4 
    }
    /// <summary>
    /// 工作状态
    /// </summary>
    public enum DealFlag
    {
        /// <summary>
        /// 未接收
        /// </summary>
        NotReceived=0,
        /// <summary>
        /// 办理中
        /// </summary>
        Operating=1,
        /// <summary>
        /// 已办理
        /// </summary>
        Operated=2,
        /// <summary>
        /// 已办结
        /// </summary>
        HasOperated=3,
        /// <summary>
        /// 已挂起
        /// </summary>
        HungUp=5
    }
}
