function SetWinHeight(obj,methed) {
    var win = obj;
    if (document.getElementById) {
        if (win && !window.opera) {
            if (win.contentDocument && win.contentDocument.body.offsetHeight)
                win.height = win.contentDocument.body.offsetHeight;
            else if (win.Document && win.Document.body.scrollHeight)
                win.height = win.Document.body.scrollHeight;
        }
        if (methed == "1") {
            if (win.height < 260)
                win.height = 260;
        }
        else {
            if (win.height < 30)
                win.height = 30;
        }
   }
}
function getmonth(month) {
    var monthbody = "<table class=\"table3 stylese\"><tr>";
    for (var i = 1; i <= 12; i++) {
        monthbody += "<td  onclick='getmonth(" + i + ");'";
        if (month == i) {
            monthbody += " style='background-color:#ddd;'"
        }
        monthbody += ">" + i + "月</td>";
        if (i % 4 == 0 && i < 12) {
            monthbody += "</tr><tr>";
        }
    }
    monthbody += "</tr></table>";
    document.getElementById("table1").innerHTML = monthbody;
    document.getElementById("Hidmonth").value = month;
    getday("", "");
    document.getElementById("iframe1").src = "/Manage/Plan/Plan_PlanDetail.aspx?UserID="+userid+"&starttime=" + myDate.getFullYear() + "-" + month + "-01&type=3&rtype=1";
    //document.getElementById("iframe2").src = "/Manage/Plan/Plan_PlanDetail.aspx?UserID=" + deptuserid + "&starttime=" + myDate.getFullYear() + "-" + month + "-01&type=3&rtype=2";
    document.getElementById("iframe2").src = "/Manage/Plan/Plan_PlanDetail.aspx?deptid=" + deptid + "&starttime=" + myDate.getFullYear() + "-" + month + "-01&type=3&rtype=2";
}
function getday(week, day) {
    var no = 0;
    var monthbody = "<table class=\"table3 stylese\"><tr>";
    var monthbody2 = "<table class=\"table3 stylese\">";
    var datatime = new Date(myDate.getFullYear(), document.getElementById("Hidmonth").value, 0);
    var days = datatime.getDate();
    var color = "";
    var nowweekday = "1";
    document.getElementById("plantitle").innerHTML = document.getElementById("Hidmonth").value + "月份计划";
    for (var i = 1; i <= days; i++) {
        var date = new Date("2012/" + document.getElementById("Hidmonth").value + "/" + i);
        if (date.getDay() == 1) {
            no++;
            switch (no) {
                case 1: color = "FD7D7D"; break;
                case 2: color = "7D80FD"; break;
                case 3: color = "7DFD83"; break;
                case 4: color = "FDEF7D'"; break;
                case 5: color = "FB7DFD"; break;
                default: break;
            }
            if (no == week) {
                document.getElementById("Hidweek").value = no;
                document.getElementById("plantitle").innerHTML = document.getElementById("Hidmonth").value + "月份第" + no + "周计划";
                monthbody2 += "<tr><td style='background-color:#eee;' onclick=\"getday('" + no + "','');\">第" + no + "周(" + i + "-" + (i + 6) + ")</td></tr>";
                nowweekday = i;
            } else
                monthbody2 += "<tr><td style='background-color:#" + color + ";' onclick=\"getday('" + no + "','');\">第" + no + "周(" + i + "-" + (i + 6) + ")</td></tr>";

        }
        if (i == day) {

            document.getElementById("Hidday").value = i;
            document.getElementById("plantitle").innerHTML = document.getElementById("Hidmonth").value + "月" + i + "日计划";
            monthbody += "<td style='background-color:#eee;' onclick=\"getday('','" + i + "');\">" + i + "</td>";
        } else
            monthbody += "<td style='background-color:#" + color + ";' onclick=\"getday('','" + i + "');\">" + i + "</td>";
        if (i % 7 == 0 && i < days) {
            monthbody += "</tr><tr>";
        }
    }
    monthbody += "</tr></table>";
    monthbody2 += "</table>";
    document.getElementById("table3").innerHTML = monthbody;
    document.getElementById("table2").innerHTML = monthbody2;
    if (week != "") {
        document.getElementById("iframe1").src = "/Manage/Plan/Plan_PlanDetail.aspx?UserID=" + userid + "&starttime=" + myDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + nowweekday + "&type=2&rtype=1";
        document.getElementById("iframe2").src = "/Manage/Plan/Plan_PlanDetail.aspx?UserID=" + deptuserid + "&starttime=" + myDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + nowweekday + "&type=2&rtype=2";
    } else {
        document.getElementById("iframe1").src = "/Manage/Plan/Plan_PlanDetail.aspx?UserID=" + userid + "&starttime=" + myDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + day + "&type=1&rtype=1";
        document.getElementById("iframe2").src = "/Manage/Plan/Plan_PlanDetail.aspx?UserID=" + deptuserid + "&starttime=" + myDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + day + "&type=1&rtype=2";
    }
}


var  nowDate= new Date();
function getdeptmonth(month, type, rtype) {
    var monthbody = "<table class=\"table3 stylese\"><tr>";
    for (var i = 1; i <= 12; i++) {
        var datestr = nowDate.getFullYear() + "-" + i + "-01";
        monthbody += "<td  onclick=\"posturl(3," + rtype + ", '" + datestr + "');\"";
        if (month == i) {
            monthbody += " style='background-color:#ddd;'"
        }
        monthbody += ">" + i + "月</td>";
        if (i % 4 == 0 && i < 12) {
            monthbody += "</tr><tr>";
        }
    }
    monthbody += "</tr></table>";
    document.getElementById("table1").innerHTML = monthbody;
    document.getElementById("Hidmonth").value = month;
    getdeptday(type,rtype, document.getElementById("Hidday").value);
}
function posturl(type, rtype, date) {
    document.location = "Plan_DeptSearch.aspx?type="+type+"&rtype="+rtype+"&date="+date;
}
function getdeptday(type,rtype, day) {
    var no = 0;
    var monthbody = "<table class=\"table3 stylese\"><tr>";
    var monthbody2 = "<table class=\"table3 stylese\">";
    var datatime = new Date(nowDate.getFullYear(), document.getElementById("Hidmonth").value, 0);
    var days = datatime.getDate();
    var color = "";
    var nowweekday = "1";
    for (var i = 1; i <= days; i++) {
        var date = new Date(nowDate.getFullYear()+"/" + document.getElementById("Hidmonth").value + "/" + i);
        if (date.getDay() == 1) {
            no++;
            switch (no) {
                case 1: color = "FD7D7D"; break;
                case 2: color = "7D80FD"; break;
                case 3: color = "7DFD83"; break;
                case 4: color = "FDEF7D"; break;
                case 5: color = "FB7DFD"; break;
                default: break;
            }
            if (type == 2 && date.getDay() == new Date(nowDate.getFullYear(), document.getElementById("Hidmonth").value, day).getDay()) {
                monthbody2 += "<tr><td style='background-color:#eee;' onclick=\"posturl(2,"+rtype+", '" + nowDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + i + "');\">第" + no + "周(" + i + "-" + (i + 6) + ")</td></tr>";
                nowweekday = i;
            } else
                monthbody2 += "<tr><td style='background-color:#" + color + ";' onclick=\"posturl(2," + rtype + ", '" + nowDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + i + "');\">第" + no + "周(" + i + "-" + (i + 6) + ")</td></tr>";

        }
        if (type == 1 && i == day) {
            document.getElementById("Hidday").value = i;
            monthbody += "<td style='background-color:#eee;' onclick=\"posturl(1," + rtype + ", '" + nowDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + i + "');\">" + i + "</td>";
        } else
            monthbody += "<td style='background-color:#" + color + ";' onclick=\"posturl(1," + rtype + ", '" + nowDate.getFullYear() + "-" + document.getElementById("Hidmonth").value + "-" + i + "');\">" + i + "</td>";
        if (i % 7 == 0 && i < days) {
            monthbody += "</tr><tr>";
        }
    }
    monthbody += "</tr></table>";
    monthbody2 += "</table>";
    document.getElementById("table3").innerHTML = monthbody;
    document.getElementById("table2").innerHTML = monthbody2;
}