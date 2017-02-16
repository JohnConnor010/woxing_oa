function ZD(obj, hidname) {
    if (obj.src.indexOf("jia.jpg") > -1) {
        obj.src = "/images/jian.jpg";
    } else {
        obj.src = "/images/jia.jpg";
    }
    var trs = $("tr[class='" + hidname + "']");
    for (i = 0; i < trs.length; i++) {
        if (obj.src.indexOf("jia.jpg") > -1) {
            trs[i].style.display = "none";
        } else {
            trs[i].style.display = "";
        }
    }

}
