function selectface(ICQ) {
    document.getElementById("nowimg").src = ICQ.src;
    ICQ.style.border = "2px #aaaaaa solid";
    if (nowimg != null) {
        nowimg.style.border = "0px";
    }
    nowimg = ICQ;
    document.getElementById("ui_icon").value = ICQ.src;
}
function checkDegree(obj) {
    var str = obj.options[obj.selectedIndex].text; //命名一个变量放置给出的字符串
    getStr = str.substr(0, 1);
    switch (getStr) {
        case "│": document.getElementById("ui_degree").value = "3";
            setdisplay("table-row", 3); break;
        case "├": document.getElementById("ui_degree").value = "2";
        setdisplay("none",2); break;
        default: document.getElementById("ui_degree").value = "1";
         setdisplay("none",1); break;
    }
}
function setdisplay(value, flag) {
    document.getElementById("tr1").style.display = value;
    document.getElementById("tr2").style.display = value;
    document.getElementById("tr3").style.display = value;
   // document.getElementById("tr4").style.display = value;
    document.getElementById("ui_Url").datatype = (value == "table-row" ? "Require" : "");
    document.getElementById("ui_Urls").datatype = (value == "table-row" ? "Require" : "");
    if (value == "none" && flag == 2) {
        document.getElementById("tr2").style.display = "table-row";
        document.getElementById("ui_Url").datatype = "Require";
    }
}