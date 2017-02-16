//���������������ƶ�
function viewMenu(id,num)
{
	var menuObj = document.getElementById(id+"_menu");
	var MenuTop = menuObj.style.top;
	MenuTop = MenuTop.substr(0, MenuTop.length - 2);
	menuObj.style.top = MenuTop - 25;
	if(is_ie && is_ie < 7) {
		jsmenu['iframe'][0].style.top = menuObj.style.top;
	}
}
//�鿴�������ͼ
function view_graph(FLOW_ID)
{
    var myleft=(screen.availWidth-800)/2;
    window.open("/general/workflow/flow_view.php?FLOW_ID="+FLOW_ID,"","status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,width=800,height=500,left="+myleft+",top=50");
}

//�鿴��
function form_view(RUN_ID,FLOW_ID,PRCS_ID)
{
    window.open("/general/workflow/list/print?RUN_ID="+RUN_ID+"&FLOW_ID="+FLOW_ID+"&PRCS_ID="+PRCS_ID,"","status=0,toolbar=no,menubar=no,width="+(screen.availWidth-12)+",height="+(screen.availHeight-38)+",location=no,scrollbars=yes,resizable=yes,left=0,top=0");
}

//�鿴ʵ������ͼ
function flow_view(RUN_ID,FLOW_ID)
{
    var myleft=(screen.availWidth-800)/2;
    window.open("/general/workflow/list/flow_view?RUN_ID="+RUN_ID+"&FLOW_ID="+FLOW_ID,RUN_ID,"status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,width=800,height=400,left="+myleft+",top=100");
}

function edit_run(RUN_ID,FLOW_ID)
{
    var myleft=(screen.availWidth-800)/2;
    window.open("/general/workflow/list/input_form?RUN_ID="+RUN_ID+"&MENU_FLAG=<?=$MENU_FLAG?>&EDIT_MODE=1&FLOW_ID="+FLOW_ID,"edit_run","status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,width=800,height=600,left="+myleft+",top=50");
}

function restore_run(RUN_ID)
{
    var msg=td_lang.general.workflow.msg_1;//"ȷ��Ҫ���˹����ָ���ִ������"
    if(window.confirm(msg))
    {
        var url="restore.php?RUN_ID="+RUN_ID;
        jQuery.get(url,{},function(data){
            jQuery.showTip(data);
            jQuery("#flow_list").gridReload({colModel:""});   
        });
    }
}

function end_run(run_id_one)
{
    var msg=td_lang.general.workflow.msg_2;//"ȷ��Ҫǿ�ƽ�����ѡ������"
    if(window.confirm(msg))
    {
    	if(typeof run_id_one == "undefined")
	    	var run_str=get_run_str();
	    else
	    	var run_str=run_id_one;
	    if(run_str=="")
	    {
            alert(td_lang.general.workflow.msg_3);//"Ҫ����������������ѡ������һ�"
            return;
        }
        jQuery.get("end.php",{"RUN_ID_STR":run_str},function(data)
        {
      	    jQuery.showTip(data);
      	    jQuery("#flow_list").gridReload({colModel:""});             
      	});
    }
}

function focus_run(RUN_ID,OP)
{   
	var OP_DESC=OP==1?td_lang.general.workflow.msg_4:td_lang.general.workflow.msg_5;//"��ע":"ȡ����ע"
    var msg2 = sprintf(td_lang.inc.msg_126,OP_DESC);
    var msg=msg2;
    if(window.confirm(msg))
    {
        jQuery.get("../list/focus.php",{"RUN_ID":RUN_ID,"OP":OP},function(data)
        {
    	    jQuery.showTip(data);
    	    jQuery("#flow_list").gridReload({colModel:""});
        });
    }
}

function others(FLOW_ID,RUN_ID,PRCS_ID,FLOW_PRCS,FLOW_TYPE)
{
    var page;
    if(FLOW_TYPE==1)
        page="others";
    else
        page="others_free";
    var myleft=(screen.availWidth-700)/2;
    var mytop=(screen.availHeight-450)/2;
    window.open("/general/workflow/list/others/"+page+".php?RUN_ID="+RUN_ID+"&FLOW_ID="+FLOW_ID+"&PRCS_ID="+PRCS_ID+"&FLOW_PRCS="+FLOW_PRCS,"others","status=0,toolbar=no,menubar=no,width=700,height=450,location=no,scrollbars=yes,resizable=no,left="+myleft+",top="+mytop);
}



function user_view(USER_ID)
{
    var mytop=(screen.availHeight-500)/2-30;
    var myleft=(screen.availWidth-780)/2;
    window.open("/general/ipanel/user/user_info.php?WINDOW=1&USER_ID="+USER_ID,"user_view","status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,width=780,height=500,left="+myleft+",top="+mytop+"\"");
}

function delete_run(RUN_ID)
{
    var msg=td_lang.general.workflow.msg_6;//"ȷ��Ҫɾ����ѡ������"
    if(window.confirm(msg))
    {
        jQuery.get("/general/workflow/list/delete.php",{"RUN_ID_STR":RUN_ID},function(data)
        {
            if(data==RUN_ID)
            {
                jQuery.showTip(td_lang.general.workflow.msg_7);//�ù�����ɾ��
                jQuery("#flow_list").gridReload({colModel:""});
            }
            else
                jQuery.showTip(td_lang.general.workflow.msg_8);//�ù���δ��ɾ��������ϵϵͳ����Ա��
        });
    }
}

function stop_run(RUN_ID,FLOW_ID,PRCS_ID,FLOW_PRCS,FLOW_TYPE,OP_FLAG)
{
    var msg=td_lang.general.workflow.msg_9;//"ȷ��Ҫ�����ù���������"
    if(typeof OP_FLAG == 'undefined')
        OP_FLAG = "";
    if(window.confirm(msg))
    {
        jQuery.get("/general/workflow/list/stop.php",{OP_FLAG:OP_FLAG,FLOW_ID:FLOW_ID,RUN_ID:RUN_ID,PRCS_ID:PRCS_ID,FLOW_PRCS:FLOW_PRCS,FLOW_TYPE:FLOW_TYPE,FLAG:1},function(data)
        {
            if(data=="")
            {
                jQuery.showTip(td_lang.general.workflow.msg_10);//�����ѳɹ�
                jQuery("#flow_list").gridReload({colModel:""});
            }
            else{
                jQuery.showTip(data);
            }    
            //jQuery("#flow_list").gridReload({colModel:""});
        });
    }
}

function call_back(RUN_ID,PRCS_ID,FLOW_PRCS)
{
    var msg=td_lang.general.workflow.msg_11;//"��һ������δ����ʱ���ջ������������°���ȷ��Ҫ�ջ���"
    if(window.confirm(msg))
    {
        var url="/general/workflow/list/call_back.php?RUN_ID="+RUN_ID+"&PRCS_ID="+PRCS_ID+"&FLOW_PRCS="+FLOW_PRCS;
        jQuery.get(url,{},function(data){
            if(data==1)
        	    jQuery.showTip(td_lang.general.workflow.msg_12);//��û��Ȩ�ޣ�
        	else if(data==2)
        	    jQuery.showTip(td_lang.general.workflow.msg_13);//�Է��ѽ��գ������ջ�
            else
            {
        	    jQuery.showTip(td_lang.general.workflow.msg_14);//�����ѻ���
        	    jQuery("#flow_list").gridReload({colModel:""});
            }
        });
    }
}

function call_back_other(RUN_ID,PRCS_ID,FLOW_PRCS,NEXT_USER)
{
    var msg=td_lang.general.workflow.msg_15;//"��ί������δ����ʱ���ջ����°���ȷ��Ҫ�ջ���"
    if(window.confirm(msg))
    {
        var url="/general/workflow/list/call_back_other.php?RUN_ID="+RUN_ID+"&PRCS_ID="+PRCS_ID+"&FLOW_PRCS="+FLOW_PRCS+"&NEXT_USER="+NEXT_USER;
        jQuery.get(url,{},function(data){
            if(data==1)
        	    jQuery.showTip(td_lang.general.workflow.msg_12);//��û��Ȩ�ޣ�
        	else if(data==2)
        	    jQuery.showTip(td_lang.general.workflow.msg_13);//�Է��ѽ��գ������ջ�
            else
            {
        	    jQuery.showTip(td_lang.general.workflow.msg_14);//�����ѻ���
        	    jQuery("#flow_list").gridReload({colModel:""});
            }
        });
    }
}

function check_one(el)
{
	if(!el.checked) {
		jQuery("#allbox_for").attr("checked", false); 
	}
}

function get_run_str()
{
    var run_str="";
    jQuery("input[name='run_select']").each(function(){
  	    if(jQuery(this).attr("checked")==true) {
  	        run_str+=jQuery(this).val()+",";
  	    }
  	});
    run_str=run_str.substr(0,run_str.length-1);
    return run_str;
}

function showMenuWorkflow(objId){
	hideMenu();
	var obj = jQuery("#"+objId);
	var parentObj = obj.parents("td");
	var objOffset = obj.offset();
	var parentObjOffset = parentObj.offset();
	var menuObj = jQuery("#"+objId+"_menu");
	var btnBarHeight = jQuery("#action_button").outerHeight();
	var vLeft	= ((jQuery(document).width()-objOffset.left) > (menuObj.outerWidth()+25)) ? (objOffset.left+jQuery("#flow_list").parent().scrollLeft()-6)
				: ((objOffset.left+jQuery("#flow_list").parent().scrollLeft())-(menuObj.outerWidth()-obj.width()-4));
	var vTop	= ((jQuery(document).height()-btnBarHeight) - (objOffset.top+obj.parent().outerHeight())) > menuObj.outerHeight() ? '' 
				: -(menuObj.outerHeight()+obj.parent().height());
	menuObj.css({
		left:vLeft,
		marginTop:vTop
	});
	initCtrl(obj[0], false, 2, 200, 0);
	ctrlobjclassName = obj[0].className;
	obj[0].className += ' hover';
	initMenu(objId, menuObj[0], 2, 200, 0, false);
	menuObj.show();
	jsmenu['active'][0] = menuObj[0];
	
}