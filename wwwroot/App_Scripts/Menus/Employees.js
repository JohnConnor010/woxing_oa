var _menus = {
    'menus': [{
        'menuid': '1',
        'icon': 'icon-img',
        'menuname': '我的快捷导航',
        'menus': [{
            'menuid': '11',
            'menuname': '我的工作台',
            'icon': 'icon-home',
            'url': 'DeskTop.aspx'
        },
	{
	    'menuid': '12',
	    'menuname': '所有资讯',
	    'icon': 'icon-paste',
	    'url': '/manage/News/News_List.aspx'
	},
	{
	    'menuid': '13',
	    'menuname': '收件箱(0/0)',
	    'icon': 'icon-data3',
	    'url': '/manage/Common/Mail_List.aspx?fid=0'
	},
	{
	    'menuid': '14',
	    'menuname': '我的日程',
	    'icon': 'icon-calendar',
	    'url': '/manage/CalendarMemo/CalendarMemo.aspx'
	},
	{
	    'menuid': '15',
	    'menuname': '我的流程',
	    'icon': 'icon-html',
	    'url': '/manage/flow/Flow_List.aspx?action=verify'
	},
	{
	    'menuid': '16',
	    'menuname': '我的文档',
	    'icon': 'icon-folder',
	    'url': '/manage/doc/Doc_List.aspx?action=mydoc'
	},
	{
	    'menuid': '17',
	    'menuname': '我的公文',
	    'icon': 'icon-et',
	    'url': '/Manage/gov/Gov_Recipient.aspx?action=verify'
	},

	{
	    'menuid': '18',
	    'menuname': '即时通讯',
	    'icon': 'icon-phone',
	    'url': '/Lesktop/Default.aspx?auto=true'
	},
	{
	    'menuid': '17511',
	    'menuname': '我的论坛',
	    'icon': 'icon-color',
	    'url': '/bbs/index.aspx'
	},

	{
	    'menuid': '19',
	    'menuname': '个人资料',
	    'icon': 'icon-user2',
	    'url': '/manage/Common/User_InfoEdit.aspx'
	},
	{
	    'menuid': '100',
	    'menuname': '安全退出',
	    'icon': 'icon-pc',
	    'url': '/manage/LoginOut.aspx?action=menu'
	}
	]
    },
{
    'menuid': '2',
    'icon': 'icon-paste',
    'menuname': '我的资讯',
    'menus': [{
        'menuid': '21',
        'menuname': '所有资讯',
        'icon': 'icon-paste',
        'url': '/manage/News/News_List.aspx'
    }
,
{
    'menuid': '220',
    'menuname': '通知公告',
    'icon': 'icon-jingpin',
    'url': '/Manage/News/News_List.aspx?tid=3'
}
,
{
    'menuid': '221',
    'menuname': '企业新闻',
    'icon': 'icon-jingpin',
    'url': '/Manage/News/News_List.aspx?tid=6'
}
,
{
    'menuid': '222',
    'menuname': '规章制度',
    'icon': 'icon-jingpin',
    'url': '/Manage/News/News_List.aspx?tid=4'
}
,
{
    'menuid': '223',
    'menuname': '公共资源',
    'icon': 'icon-jingpin',
    'url': '/Manage/News/News_List.aspx?tid=5'
}


	]
}
,
{
    'menuid': '3',
    'icon': 'icon-mail',
    'menuname': '我的邮件',
    'menus': [{
        'menuid': '31',
        'menuname': '发送新邮件',
        'icon': 'icon-addnew',
        'url': '/manage/Common/Mail_Manage.aspx'
    }
	,
	{
	    'menuid': '32',
	    'menuname': '收件箱(0/0)',
	    'icon': 'icon-data3',
	    'url': '/manage/Common/Mail_List.aspx?fid=0'
	}
	,
	{
	    'menuid': '33',
	    'menuname': '草稿箱(0)',
	    'icon': 'icon-guestbook',
	    'url': '/manage/Common/Mail_List.aspx?fid=1'
	}
	,
	{
	    'menuid': '34',
	    'menuname': '发件箱(0)',
	    'icon': 'icon-data1',
	    'url': '/manage/Common/Mail_List.aspx?fid=2'
	}
	,
	{
	    'menuid': '35',
	    'menuname': '垃圾箱(0)',
	    'icon': 'icon-data6',
	    'url': '/manage/Common/Mail_List.aspx?fid=3'
	}

	]
}
,
{
    'menuid': '4',
    'icon': 'icon-html',
    'menuname': '工作流程',
    'menus': [{
        'menuid': '41',
        'menuname': '新建工作流程',
        'icon': 'icon-addnew',
        'url': '/Manage/flow/Flow_Manage.aspx'
    }
	,
	{
	    'menuid': '42',
	    'menuname': '我的批阅',
	    'icon': 'icon-html',
	    'url': '/Manage/flow/Flow_List.aspx?action=verify'
	}
	,
	{
	    'menuid': '43',
	    'menuname': '已经批阅',
	    'icon': 'icon-filesave',
	    'url': '/Manage/flow/Flow_List.aspx?action=verified'
	}
	,
	{
	    'menuid': '44',
	    'menuname': '我的申请',
	    'icon': 'icon-all',
	    'url': '/Manage/flow/Flow_List.aspx?action=apply'
	}
	,
	{
	    'menuid': '45',
	    'menuname': '抄送呈报',
	    'icon': 'icon-template',
	    'url': '/Manage/flow/Flow_List.aspx?action=view'
	}

	]
}
,
{
    'menuid': '5',
    'icon': 'icon-calendar',
    'menuname': '我的日程',
    'menus': [{
        'menuid': '51',
        'menuname': '我的日程',
        'icon': 'icon-calendar',
        'url': '/manage/CalendarMemo/CalendarMemo.aspx'
    },
	{
	    'menuid': '52',
	    'menuname': '下属日程',
	    'icon': 'icon-calendar1',
	    'url': '/Manage/Common/MyMemo.aspx'
	},
	{
	    'menuid': '53',
	    'menuname': '导出日程',
	    'icon': 'icon-do',
	    'url': '/Manage/Common/Memo_General.aspx'
	}

	]
}
,
{
    'menuid': '6',
    'icon': 'icon-ett',
    'menuname': '我的公文',
    'menus': [{
        'menuid': '61',
        'menuname': '收文管理',
        'icon': 'icon-task',
        'child': [{
            'menuid': '611',
            'menuname': '公文签收',
            'icon': 'icon-et',
            'url': '/Manage/gov/Gov_Recipient.aspx?action=verify'
        },
		{
		    'menuid': '612',
		    'menuname': '已签收',
		    'icon': 'icon-filesave',
		    'url': '/Manage/gov/Gov_Recipient.aspx?action=verified'
		},
		{
		    'menuid': '613',
		    'menuname': '已归档',
		    'icon': 'icon-download',
		    'url': '/Manage/gov/Gov_Recipient.aspx?action=archived'
		}]
    },
	{
	    'menuid': '62',
	    'menuname': '发文管理',
	    'icon': 'icon-template',
	    'child': [{
	        'menuid': '621',
	        'menuname': '发文拟稿',
	        'icon': 'icon-addnew',
	        'url': '/Manage/gov/gov_Manage.aspx'
	    },
		{
		    'menuid': '622',
		    'menuname': '我的审核',
		    'icon': 'icon-password',
		    'url': '/Manage/gov/Gov_List.aspx?action=verify'
		},
		{
		    'menuid': '623',
		    'menuname': '已经审核',
		    'icon': 'icon-filesave',
		    'url': '/Manage/gov/Gov_List.aspx?action=verified'
		},
		{
		    'menuid': '624',
		    'menuname': '我的发文',
		    'icon': 'icon-all',
		    'url': '/Manage/gov/Gov_List.aspx?action=apply'
		}
		]
	}]
}
,
{
    'menuid': '7',
    'icon': 'icon-vss',
    'menuname': '沟通与分享',
    'menus': [{
        'menuid': '71',
        'menuname': '部门快速导航',
        'icon': 'icon-import',
        'url': '/Manage/Common/DepGuide.aspx'
    },
	{
	    'menuid': '72',
	    'menuname': '在线用户',
	    'icon': 'icon-usergroup',
	    'url': '/Manage/Common/User_OnLine.aspx'
	},
	{
	    'menuid': '73',
	    'menuname': '我的文档',
	    'icon': 'icon-sys',
	    'child': [{
	        'menuid': '731',
	        'menuname': '新建上传',
	        'icon': 'icon-upload',
	        'url': '/Manage/doc/Doc_Manage.aspx'
	    },
		{
		    'menuid': '732',
		    'menuname': '我的文档',
		    'icon': 'icon-folder',
		    'url': '/Manage/doc/Doc_List.aspx?action=mydoc'
		},
		{
		    'menuid': '733',
		    'menuname': '同事共享',
		    'icon': 'icon-sharedir',
		    'url': '/Manage/doc/Doc_List.aspx?action=shared'
		},
		{
		    'menuid': '734',
		    'menuname': '文件分类',
		    'icon': 'icon-template',
		    'url': '/Manage/doc/DocType_List.aspx'
		}]
	},
	{
	    'menuid': '74',
	    'menuname': '我的投票',
	    'icon': 'icon-app',
	    'url': '/Manage/Common/Vote_List.aspx'
	},

	{
	    'menuid': '75',
	    'menuname': '我的论坛',
	    'icon': 'icon-color',
	    'url': '/bbs/index.aspx'
	},

	{
	    'menuid': '76',
	    'menuname': '即时通讯',
	    'icon': 'icon-phone',
	    'url': '/Lesktop/Default.aspx?auto=true'
	}]
}
,
{
    'menuid': '8',
    'icon': 'icon-app',
    'menuname': '我的工具',
    'menus': [{
        'menuid': '81',
        'menuname': '会议管理',
        'icon': 'icon-task',
        'url': '/Manage/Common/Meeting_List.aspx'
    },
	{
	    'menuid': '82',
	    'menuname': '记事便笺',
	    'icon': 'icon-pen',
	    'url': '/Manage/Common/NoteBook_List.aspx'
	},
	{
	    'menuid': '83',
	    'menuname': '我的通讯录',
	    'icon': 'icon-pub',
	    'child': [{
	        'menuid': '831',
	        'menuname': '员工通讯录',
	        'icon': 'icon-table',
	        'url': '/Manage/Common/PublicAddrBook.aspx'
	    },
		{
		    'menuid': '832',
		    'menuname': '组织通讯录',
		    'icon': 'icon-theme',
		    'url': '/Manage/Common/PublicAddrBook_Dep.aspx'
		},
		{
		    'menuid': '833',
		    'menuname': '个人通讯录',
		    'icon': 'icon-template',
		    'url': '/Manage/Common/PrivateAddrBook.aspx'
		}]
	},
	{
	    'menuid': '84',
	    'menuname': '我的客户',
	    'icon': 'icon-users',
	    'child': [{
	        'menuid': '841',
	        'menuname': '新建客户',
	        'icon': 'icon-addnew',
	        'url': '/Manage/crm/CRM_Manage.aspx'
	    },
		{
		    'menuid': '842',
		    'menuname': '我的客户列表',
		    'icon': 'icon-users',
		    'url': '/Manage/crm/CRM_List.aspx?action=mycrm'
		},
		{
		    'menuid': '843',
		    'menuname': '协同共享客户',
		    'icon': 'icon-users2',
		    'url': '/Manage/crm/CRM_List.aspx?action=shared'
		},
		{
		    'menuid': '844',
		    'menuname': '所有客户接触',
		    'icon': 'icon-sitemap',
		    'url': '/Manage/crm/CRM_AllContact.aspx'
		},
		{
		    'menuid': '845',
		    'menuname': '供应商管理',
		    'icon': 'icon-data4',
		    'url': '/Manage/crm/CRM_Sup_List.aspx'
		}]
	},
	{
	    'menuid': '85',
	    'menuname': '便捷小工具',
	    'icon': 'icon-globe',
	    'child': [{
	        'menuid': '851',
	        'menuname': '网址大全',
	        'icon': 'icon-ppt',
	        'url': '/DK_Css/WZ/index.htm'
	    },
		{
		    'menuid': '852',
		    'menuname': '科学计算器',
		    'icon': 'icon-part',
		    'url': '/manage/utils/calar/jsq.htm'
		},
		{
		    'menuid': '853',
		    'menuname': '多功能万年历',
		    'icon': 'icon-calendar1',
		    'url': '/manage/utils/calar/Calendar.htm'
		},
		{
		    'menuid': '854',
		    'menuname': '历史上的今天',
		    'icon': 'icon-com',
		    'url': '/dk_css/history/history.aspx'
		}]
	},
	{
	    'menuid': '86',
	    'menuname': '个人资料',
	    'icon': 'icon-user2',
	    'url': '/manage/Common/User_InfoEdit.aspx'
	}]
}

,
{
    'menuid': '1000',
    'icon': 'icon-usergrade',
    'menuname': '系统管理',
    'menus': [

{
    'menuid': '10001',
    'menuname': '系统设置',
    'icon': 'icon-settings',
    'child': [
{
    'menuid': '100011',
    'menuname': '基本信息',
    'icon': 'icon-log',
    'url': '/Manage/CompanyInfo.aspx'
},
{
    'menuid': '100012',
    'menuname': '数据库备份',
    'icon': 'icon-db',
    'url': '/Manage/sys/DbBackup.aspx'
}
]
},

{
    'menuid': '10002',
    'menuname': '组织机构管理',
    'icon': 'icon-exam',
    'child': [
{
    'menuid': '100021',
    'menuname': '组织机构列表',
    'icon': 'icon-template',
    'url': '/Manage/sys/Dep_List.aspx'
},
{
    'menuid': '100022',
    'menuname': '新增组织机构',
    'icon': 'icon-addnew',
    'url': '/Manage/Department/AddDepartment.aspx'
}
]
},

{
    'menuid': '10003',
    'menuname': '用户管理',
    'icon': 'icon-user',
    'child': [
{
    'menuid': '100031',
    'menuname': '用户列表',
    'icon': 'icon-usergroup',
    'url': '/Manage/sys/User_List.aspx'
},
{
    'menuid': '100032',
    'menuname': '新增用户',
    'icon': 'icon-addnew',
    'url': '/Manage/sys/User_Manage.aspx'
},
{
    'menuid': '100033',
    'menuname': '角色列表',
    'icon': 'icon-userclass',
    'url': '/Manage/sys/Role_List.aspx'
},
{
    'menuid': '100034',
    'menuname': '新增角色',
    'icon': 'icon-addnew',
    'url': '/Manage/sys/Role_Manage.aspx'
}
]
},

{
    'menuid': '10005',
    'menuname': '资讯管理',
    'icon': 'icon-paste',
    'child': [
{
    'menuid': '100051',
    'menuname': '所有资讯列表',
    'icon': 'icon-guestbook',
    'url': '/manage/news/News_AllList.aspx'
},
{
    'menuid': '100052',
    'menuname': '发布资讯',
    'icon': 'icon-addnew',
    'url': '/manage/news/News_Manage.aspx'
},
{
    'menuid': '100053',
    'menuname': '资讯分类列表',
    'icon': 'icon-jingpin',
    'url': '/manage/news/NewsType_List.aspx'
},
{
    'menuid': '100054',
    'menuname': '添加资讯分类',
    'icon': 'icon-addnew',
    'url': '/manage/news/NewsType_Manage.aspx'
},
{
    'menuid': '100055',
    'menuname': '滚动公告列表',
    'icon': 'icon-other',
    'url': '/manage/news/Tips_List.aspx'
},
{
    'menuid': '100056',
    'menuname': '添加滚动公告',
    'icon': 'icon-addnew',
    'url': '/manage/news/Tips_Manage.aspx'
}
]
},

{
    'menuid': '10006',
    'menuname': '流程管理',
    'icon': 'icon-html',
    'child': [
{
    'menuid': '100061',
    'menuname': '流程模型列表',
    'icon': 'icon-mdb',
    'url': '/Manage/Flow/Flow_ModelList.aspx'
},
{
    'menuid': '100062',
    'menuname': '新增流程模型',
    'icon': 'icon-addnew',
    'url': '/Manage/Flow/Flow_ModelManage.aspx'
},
{
    'menuid': '100063',
    'menuname': '模板表单列表',
    'icon': 'icon-mde',
    'url': '/Manage/Flow/Flow_ModelFileList.aspx'
},
{
    'menuid': '100064',
    'menuname': '新增模板表单',
    'icon': 'icon-addnew',
    'url': '/Manage/Flow/Flow_ModelFileManage.aspx'
},
{
    'menuid': '100065',
    'menuname': '所有流程监控',
    'icon': 'icon-new2',
    'url': '/Manage/flow/Flow_ListAll.aspx'
}
]
},

{
    'menuid': '10007',
    'menuname': '公文管理',
    'icon': 'icon-ett',
    'child': [
{
    'menuid': '100071',
    'menuname': '发文拟稿',
    'icon': 'icon-addnew',
    'url': '/manage/gov/Gov_Manage.aspx'
},
{
    'menuid': '100072',
    'menuname': '公文模型列表',
    'icon': 'icon-pub',
    'url': '/manage/gov/gov_ModelList.aspx'
},
{
    'menuid': '100073',
    'menuname': '新增公文模型',
    'icon': 'icon-addnew',
    'url': '/manage/gov/gov_ModelManage.aspx'
},
{
    'menuid': '100074',
    'menuname': '公文表单列表',
    'icon': 'icon-txt',
    'url': '/manage/gov/gov_ModelFileList.aspx'
},
{
    'menuid': '100075',
    'menuname': '新增公文表单',
    'icon': 'icon-addnew',
    'url': '/manage/gov/gov_ModelFileManage.aspx'
},
{
    'menuid': '100076',
    'menuname': '所有公文监控',
    'icon': 'icon-new2',
    'url': '/manage/gov/gov_ListAll.aspx'
}
]
},

{
    'menuid': '10008',
    'menuname': '印章/签名管理',
    'icon': 'icon-sitemap',
    'child': [
{
    'menuid': '100081',
    'menuname': '印章/签名列表',
    'icon': 'icon-img',
    'url': '/Manage/Sys/Seal_List.aspx'
},
{
    'menuid': '100082',
    'menuname': '新增印章/签名',
    'icon': 'icon-addnew',
    'url': '/manage/sys/Seal_Manage.aspx'
}
]
},

{
    'menuid': '11000',
    'menuname': '投票管理',
    'icon': 'icon-app',
    'url': '/Manage/Common/Vote_AllList.aspx'
}
,

{
    'menuid': '10009',
    'menuname': '致信管理员',
    'icon': 'icon-mail1',
    'url': '/Manage/Common/Mail_Manage.aspx?userlist=31'
}]
}


]
};
