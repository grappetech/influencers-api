(function ($) {
    $.fn.serializeFormJSON = function () {

        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);




var Site = {

    utils: {
        validateEmail: function (email) {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$/.test(email)) {
                return (true)
            }

            return (false)
        },
        isStrongPassword: function (value) {
            var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            return strongRegex.test(value);

        }

    },
    init: function () {

        console.log('Init site.')
        $('#bntLogoutSystem').on('click', function (e) {
            e.preventDefault();
            Site.notification.confirm("Aviso", "Deseja realmente sair?", function () {
                window.location.href = "/Logged/Index?handler=logout"
            });
        });



        //DatePicker
        try {
            $('.pickadate-dropdown').pickadate({
                format: 'dd/mm/yyyy',
                formatSubmit: 'yyyy-mm-dd',
                hiddenPrefix: 'prefix__',
                hiddenSuffix: '__suffix',
                selectMonths: true,
                selectYears: true,
                selectYears: 238


            });
        } catch (e) {

        }
    },
    ajax: {
        get: function (url, callback) {
            //$.get(url, function (data, status) {
            //    callback(data, status);
            //});

            $.ajax({

                type: "GET",
                //data: dataParameter,
                //contentType: contentType,
                url: url,
                //dataType: "html",
                success: function (data, status, jqXHR) {
                    callback(data, status);
                },
                beforeSend: function () {
                    Site.page.loading();
                },
                complete: function (msg) {
                    Site.page.unloading();
                }
            });

        },
        post: function (url, dataParameter, callback, contentType) {

            if (!contentType)
                contentType = 'application/x-www-form-urlencoded; charset=UTF-8';

            $.ajax({

                type: "POST",
                data: dataParameter,
                contentType: contentType,
                url: url,
                dataType: "html",
                success: function (data, status, jqXHR) {
                    callback(data, status);
                },
                beforeSend: function () {
                    Site.page.loading();
                },
                complete: function (msg) {
                    Site.page.unloading();
                }
            });
        }
    },
};


Site.page = {
    loading: function (msg) {

        if (!msg) {
            msg = "Aguarde um momento...";
        }
        $.blockUI({ message: '<h5>' + msg + '</h5>' });
    },
    unloading: function () {
        $.unblockUI();
    }
}

Site.notification = {
    alert: function (msg) {
        swal(msg);

    },
    error: function (msg) {
        swal("Ops!", msg, "error");
    },
    success: function (msg, callback) {



        if (callback && typeof (callback) == "function") {
            Site.notification.confirm("Sucesso", msg, callback, null, null, "success", false);
        } else {
            swal("Aviso", msg, "success");
        }

    },
    confirm: function (title, text, callbackOkResult, labelBtnOK, labelBtnCancel, type, showCancelButton) {

        if (showCancelButton == undefined || showCancelButton == null)
            showCancelButton = true;

        if (!labelBtnOK)
            labelBtnOK = "Sim";

        if (!labelBtnCancel)
            labelBtnCancel = "Cancelar"


        if (!type)
            type = "warning";


        swal({
            title: title,
            text: text,
            type: type,
            showCancelButton: showCancelButton,
            confirmButtonColor: '#0CC27E',
            cancelButtonColor: '#FF586B',
            confirmButtonText: labelBtnOK,
            cancelButtonText: labelBtnCancel
        }).then(function (isConfirm) {
            if (isConfirm) {

                if (callbackOkResult && typeof (callbackOkResult) == "function")
                    callbackOkResult();
            }
        }).catch(swal.noop);
    },
    htmlMessage: function (titlelHtml, htmlBody) {

        if (!titlelHtml)
            titlelHtml = "Informe um título";

        if (!htmlBody)
            htmlBody = "";

        swal({
            title: titlelHtml,
            text: htmlBody,
            html: false
        });
    }
}




$(document).ready(function () {
    Site.init();
})

function validatedate(inputText) {
    var dateformat = /^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$/;
    // Match the date format through regular expression
    if (inputText.match(dateformat)) {
        
        //Test which seperator is used '/' or '-'
        var opera1 = inputText.split('/');
        var opera2 = inputText.split('-');
        lopera1 = opera1.length;
        lopera2 = opera2.length;
        // Extract the string into month, date and year
        if (lopera1 > 1) {
            var pdate = inputText.split('/');
        }
        else if (lopera2 > 1) {
            var pdate = inputText.split('-');
        }
        var mm = parseInt(pdate[0]);
        var dd = parseInt(pdate[1]);
        var yy = parseInt(pdate[2]);
        // Create list of days of a month [assume there is no leap year by default]
        var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        if (mm == 1 || mm > 2) {
            if (dd > ListofDays[mm - 1]) {
               
                return false;
            }
        }
        if (mm == 2) {
            var lyear = false;
            if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                lyear = true;
            }
            if ((lyear == false) && (dd >= 29)) {
               
                return false;
            }
            if ((lyear == true) && (dd > 29)) {
                
                return false;
            }
        }
    }
    else {
      
        return false;
    }
}
