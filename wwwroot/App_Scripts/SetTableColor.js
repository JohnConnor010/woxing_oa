function SetTableColor(TableID) {
    var clickClass = "";        //点击样式名
    var moveClass = "";         //鼠标经过样式名
    var clickTR = null;         //点击的行
    var moveTR = null;          //鼠标经过行
    var Ptr = document.getElementById(TableID).getElementsByTagName("tr");
    for (i = 1; i < Ptr.length + 1; i++) {
        Ptr[i - 1].className = (i % 2 > 0) ? "Rep_Tab_EvenTr" : "Rep_Tab_OddTr";
    }
    //设置鼠标的动作事件
    for (var i = 1; i < Ptr.length; i++) {
        var Owner = Ptr[i].item;
        //鼠标经过事件
        Ptr[i].onmouseover = function Move() {
            if (clickTR != this) {
                if (moveTR != this) {
                    moveClass = this.className;
                    moveTR = this;
                    this.className = "Rep_Tr_Move";
                }
            }
        }
        //鼠标离开事件
        Ptr[i].onmouseout = function Out() {
            if (clickTR != this) {
                moveTR = null;
                this.className = moveClass;
            }
        }
        //鼠标单击事件
        Ptr[i].onclick = function Ck() {
            if (clickTR != this) {
                if (clickTR) {
                    clickTR.className = clickClass;
                }
                clickTR = this;
                clickClass = moveClass;
            }
            this.className = "Rep_Tr_Click";
        }
    }
}