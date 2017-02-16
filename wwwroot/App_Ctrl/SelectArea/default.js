$(function () {
    $('#ddlProvince').change(function () {
        var code = $('#ddlProvince').val();
        $('#hidden_province').val(code);
        if (code != "0") {
            $.ajax({
                url: "/App_Ctrl/SelectArea/GetRegionByCode.ashx?Region=Province&code=" + code,
                type: "get",
                dataType: "json",
                success: function (result) {
                    if (result != "") {
                        $('#ddlCity').removeAttr("disabled");
                        $('#ddlCity').empty();
                        $('#ddlCity').append("<option value='0'>--请选择--</option>");
                        $('#ddlArea').empty();
                        $('#ddlArea').append("<option value='0'>--请选择--</option>");
                        $('#ddlArea').attr("disabled", "disabled");
                        $.each(result, function (index, item) {
                            $("<option value='" + item.Code + "'>" + item.Name + "</option>").appendTo('#ddlCity');
                        });
                    }
                }
            });
        } else {
            $('#ddlCity').empty();
            $('#ddlCity').append("<option value='0'>--请选择--</option>");
            $('#ddlCity').attr("disabled", "disabled");
            
            $('#ddlArea').empty();
            $('#ddlArea').append("<option value='0'>--请选择--</option>");
            $('#ddlArea').attr("disabled", "disabled");
            
        }
        $("#hidden_city").val("0");
        $("#hidden_area").val("0");
    });
    $('#ddlCity').change(function () {
        var code = $('#ddlCity').val();
        $('#hidden_city').val(code);
        if (code != "0") {
            $.ajax({
                url: "/App_Ctrl/SelectArea/GetRegionByCode.ashx?Region=City&code=" + code,
                type: "get",
                dataType: "json",
                success: function (result) {
                    if (result != "") {
                        $('#ddlArea').removeAttr("disabled");
                        $('#ddlArea').empty();
                        $('#ddlArea').append("<option value='0'>--请选择--</option>");
                        $.each(result, function (index, item) {
                            $("<option value='" + item.Code + "'>" + item.Name + "</option>").appendTo('#ddlArea');
                        });
                    } else {
                        $('#ddlArea').empty();
                        $('#ddlArea').append("<option value='0'>--请选择--</option>");
                        $('#ddlArea').attr("disabled", "disabled");
                    }
                }
            });
        } else {
            $('#ddlArea').empty();
            $('#ddlArea').append("<option value='0'>--请选择--</option>");
            $('#ddlArea').attr("disabled", "disabled");

        }
        $("#hidden_area").val("0");
    });
    $('#ddlArea').change(function () {
        var code = $('#ddlArea').val();
        $('#hidden_area').val(code);
    });
});