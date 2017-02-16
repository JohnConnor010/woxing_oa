//ajax postback
(function ($) {
    $.doPostback = function (funcName, paras, callback, opts) {
        var defaultOpts = {
            url: window.location.pathname,
            callback: function (data) { },
            show_loading: true,
            loading_icon: "http://minsheng.wsjn.cn/images/ajax-loader.gif",
            loading_html: "",
            error: function (xhr, text, err) {
                try {
                    var response = JSON.parse(xhr.responseText);
                    alert(response.Message + "\r\r" + response.StackTrace);
                }
                catch (ex) {
                    $("body").html(xhr.responseText);
                }
            }
        };

        if (typeof JSON == "undefined")
            JSON = new Object();
        JSON.stringify = JSON.stringify || function (obj) {
            var t = typeof (obj);
            if (t != "object" || obj === null) {
                // simple data type
                if (t == "string") obj = '"' + obj + '"';
                return String(obj);
            }
            else {
                // recurse array or object
                var n, v, json = [], arr = (obj && obj.constructor == Array);
                for (n in obj) {
                    v = obj[n]; t = typeof (v);
                    if (t == "string") v = '"' + v + '"';
                    else if (t == "object" && v !== null) v = JSON.stringify(v);
                    json.push((arr ? "" : '"' + n + '":') + String(v));
                }
                return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
            }
        };

        var attemptToAbort = false;

        if (typeof callback == "function") {
            opts = jQuery.extend(defaultOpts, opts || {});
            opts.callback = callback;
        }

        else {
            opts = jQuery.extend(defaultOpts, callback || {});
        }
        opts.ajax = jQuery.extend({
            type: "POST",
            url: funcName.indexOf("/") != -1 ? funcName : opts.url + "/" + funcName,
            data: JSON.stringify(paras),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (data) {
                opts.callback(data);
            },
            error: function (xhr, text, err) {
                if (!attemptToAbort || xhr.statusText != 'abort')
                    opts.error(xhr, text, err);
                else
                    attemptToAbort = false;
            }
        }, opts.ajax || {});

        if (opts.show_loading) {
            var progressBar = $("#clientsiderepearter_divLoadingProgressBar");

            if (progressBar.length < 1) {
                $("body").append('<div id="clientsiderepearter_divLoadingProgressBar" style="position:fixed;z-index:222;display:none">' +
                  (opts.loading_html == "" ? '<img src="' + opts.loading_icon + '" />' : opts.loading_html) +
                    '</div>');
                progressBar = $("#clientsiderepearter_divLoadingProgressBar");
            }

            progressBar.css({
                "top": (($(window).height() - progressBar.outerHeight()) / 2) + $(window).scrollTop() + "px",
                "left": (($(window).width() - progressBar.outerWidth()) / 2) + $(window).scrollLeft() + "px"
            });

            progressBar.show();
        }

        var xhr = $.ajax(opts.ajax).always(function (e) {
            if (opts.show_loading)
                progressBar.hide();
        });
        xhr.cancel = function () {
            attemptToAbort = true;
            xhr.abort();
        };

        return xhr;
    };
})(jQuery);
  