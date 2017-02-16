﻿
window.onload = function(){
	$('#loading-mask').fadeOut();
}

//初始化左侧
function InitLeftMenu() {
	$("#nav").accordion({animate:false,fit:true,border:false});
	var selectedPanelname = '';
	$.each(_menus.menus, function (i, n) {
	    var menulist = '';
	    menulist += '<ul class="navlist">';
	    $.each(n.menus, function (j, o) {
                 menulist += '<li><div ><a ref="' + o.menuid + '" href="#" rel="' + o.url + '" ><img border="0" src="/Manage/icon/' + o.icon + '" />&nbsp;<span class="nav">' + o.menuname + '</span></a></div> ';
                 //  menulist += '<li><div ><a ref="' + o.menuid + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div> ';
	        if (o.child && o.child.length > 0) {
	            //li.find('div').addClass('icon-arrow');
	            menulist += '<ul class="third_ul">';
	            $.each(o.child, function (k, p) {
	                menulist += '<li><div><a ref="' + p.menuid + '" href="#" rel="' + p.url + '" ><img border="0" src="/Manage/icon/' + o.icon + '" />&nbsp;<span class="nav">' + p.menuname + '</span></a></div> </li>'
	                // menulist += '<li><div><a ref="' + p.menuid + '" href="#" rel="' + p.url + '" ><span class="icon ' + p.icon + '" >&nbsp;</span><span class="nav">' + p.menuname + '</span></a></div> </li>'
	            });
	            menulist += '</ul>';
	        }

	        menulist += '</li>';
	    })
	    menulist += '</ul>';
	    $('#nav').accordion('add', {
	        title: n.menuname,
	        content: menulist,
	        border: false,
	        iconCls: 'icon ' + n.icon
	    });

	    if (i == 0)
	        selectedPanelname = n.menuname;

	});
	$('#nav').accordion('select',selectedPanelname);



	$('.navlist li a').click(function(){
		var tabTitle = $(this).children('.nav').text();

		var url = $(this).attr("rel");
		var menuid = $(this).attr("ref");
		var icon = $(this).find('.icon').attr('class');

		var third = find(menuid);
		if(third && third.child && third.child.length>0)
		{
			$('.third_ul').slideUp();

			var ul =$(this).parent().next();
			if(ul.is(":hidden"))
				ul.slideDown();
			else
				ul.slideUp();
		}
		else{
			addTab(tabTitle,url,icon);
			$('.navlist li div').removeClass("selected");
			$(this).parent().addClass("selected");
		}
	}).hover(function(){
		$(this).parent().addClass("hover");
	},function(){
		$(this).parent().removeClass("hover");
	});



function othermenu() {
    alert("sf");
    var tabTitle = $(this).children('.nav').text();

    var url = $(this).attr("rel");
    var menuid = $(this).attr("ref");
    var icon = $(this).find('.icon').attr('class');

    var third = find(menuid);
    if (third && third.child && third.child.length > 0) {
        $('.third_ul').slideUp();

        var ul = $(this).parent().next();
        if (ul.is(":hidden"))
            ul.slideDown();
        else
            ul.slideUp();



    }
    else {
        addTab(tabTitle, url, icon);
        $('.navlist li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }
}

	//选中第一个
	//var panels = $('#nav').accordion('panels');
	//var t = panels[0].panel('options').title;
    //$('#nav').accordion('select', t);
}
//获取左侧导航的图标
function getIcon(menuid){
	var icon = 'icon ';
	$.each(_menus.menus, function(i, n) {
		 $.each(n.menus, function(j, o) {
		 	if(o.menuid==menuid){
				icon += o.icon;
			}
		 })
	})

	return icon;
}

function find(menuid){
	var obj=null;
	$.each(_menus.menus, function(i, n) {
		 $.each(n.menus, function(j, o) {
		 	if(o.menuid==menuid){
				obj = o;
			}
		 });
	});

	return obj;
}

function addTab(subtitle, url, icon) {

    if (!$('#tabs').tabs('exists', subtitle)) {

		$('#tabs').tabs('add',{
			title:subtitle,
			content:createFrame(url),
			closable:true,
			icon:icon
		});
} else {
    $('#tabs').tabs('close', subtitle);
    $('#tabs').tabs('add', {
        title: subtitle,
        content: createFrame(url),
        closable: true,
        icon: icon
    });
		//$('#tabs').tabs('select',subtitle);
		//$('#mm-tabupdate').click();
	}
	tabClose();
}

function createFrame(url)
{
	var s = '<iframe scrolling="auto" frameborder="0"  src="'+url+'" style="width:100%;height:100%;"></iframe>';
	return s;
}

function tabClose()
{
	/*双击关闭TAB选项卡*/
	$(".tabs-inner").dblclick(function(){
		var subtitle = $(this).children(".tabs-closable").text();
		$('#tabs').tabs('close',subtitle);
	})
	/*为选项卡绑定右键*/
	$(".tabs-inner").bind('contextmenu',function(e){
		$('#mm').menu('show', {
			left: e.pageX,
			top: e.pageY
		});

		var subtitle =$(this).children(".tabs-closable").text();

		$('#mm').data("currtab",subtitle);
		$('#tabs').tabs('select',subtitle);
		return false;
	});
}
//绑定右键菜单事件
function tabCloseEven()
{
	//刷新
	$('#mm-tabupdate').click(function(){
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('src');
		$('#tabs').tabs('update',{
			tab:currTab,
			options:{
				content:createFrame(url)
			}
		})
	})
	//关闭当前
	$('#mm-tabclose').click(function(){
		var currtab_title = $('#mm').data("currtab");
		$('#tabs').tabs('close',currtab_title);
	})
	//全部关闭
	$('#mm-tabcloseall').click(function(){
		$('.tabs-inner span').each(function(i,n){
			var t = $(n).text();
			$('#tabs').tabs('close',t);
		});
	});
	//关闭除当前之外的TAB
	$('#mm-tabcloseother').click(function(){
		$('#mm-tabcloseright').click();
		$('#mm-tabcloseleft').click();
	});
	//关闭当前右侧的TAB
	$('#mm-tabcloseright').click(function(){
		var nextall = $('.tabs-selected').nextAll();
		if(nextall.length==0){
			//msgShow('系统提示','后边没有啦~~','error');
			//alert('后边没有啦~~');
			return false;
		}
		nextall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			$('#tabs').tabs('close',t);
		});
		return false;
	});
	//关闭当前左侧的TAB
	$('#mm-tabcloseleft').click(function(){
		var prevall = $('.tabs-selected').prevAll();
		if(prevall.length==0){
			//alert('到头了，前边没有啦~~');
			return false;
		}
		prevall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			$('#tabs').tabs('close',t);
		});
		return false;
	});

	//退出
	$("#mm-exit").click(function(){
		$('#mm').menu('hide');
	})
}

		function refresh(){
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('src');
		$('#tabs').tabs('update',{
			tab:currTab,
			options:{
				content:createFrame(url)
			}
		})
		}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
	$.messager.alert(title, msgString, msgType);
}