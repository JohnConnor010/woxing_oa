//��ӡ�ɾ���ؼ����˵�����������FCKCutCopyCommand�������޸ģ� by dq 090522
var FCKDeleteCommand = function()
{
	this.Name = 'td_delete' ;
}

FCKDeleteCommand.prototype =
{
	Execute : function()
	{
		var enabled = false ;

        //ɾ���ؼ�ʱ������ʾ  add by lx 20090928 
        var cntrlSelected = FCKSelection.GetSelectedElement();
        if(cntrlSelected != null)
        {
            var cntrlClassName = cntrlSelected.className.toUpperCase();
            var cntrlTagName = cntrlSelected.tagName.toUpperCase();
            if(cntrlTagName == "INPUT" || cntrlTagName == "TEXTAREA" || cntrlTagName == "SELECT" || cntrlClassName == "LIST_VIEW" || cntrlClassName == "DATE" || cntrlClassName == "USER" || cntrlClassName == "DATA" || cntrlClassName == "FETCH")
            {
                var msg = "ȷ��Ҫɾ���ؼ���ɾ���ؼ����ܵ�����ʷ�����޷���ʾ��ȷ��Ҫ������";
                if(!window.confirm(msg))
                    return false;
            } 
        }
    
	    if ( FCKBrowserInfo.IsIE )
	    {
	    	var onEvent = function()
	    	{
	    		enabled = true ;
	    	} ;
    
	    	var eventName = 'on' + this.Name.toLowerCase() ;
    
	    	FCK.EditorDocument.body.attachEvent( eventName, onEvent ) ;
	    	FCK.ExecuteNamedCommand( 'Delete' ) ;
	    	FCK.EditorDocument.body.detachEvent( eventName, onEvent ) ;
	    }
	    else
	    {
	    	try
	    	{
	    		FCK.ExecuteNamedCommand( this.Name ) ;
	    		enabled = true ;
	    	}
	    	catch(e){}
	    }
/*  
	    	if ( !enabled )
	    		alert( FCKLang[ 'PasteError' + this.Name ] ) ;*/
	},

	GetState : function()
	{
		return FCK.EditMode != FCK_EDITMODE_WYSIWYG ?
				FCK_TRISTATE_DISABLED :
				FCK.GetNamedCommandState( 'Cut' ) ;
	}
};

FCKCommands.RegisterCommand( 'td_delete',new FCKDeleteCommand ( 'td_delete', 'ɾ���ؼ�') );