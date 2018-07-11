

var Site = {

    init: function() {

        $('#bntLogoutSystem').on('click', function(e) {

            e.preventDefault();
            Site.notification.confirm("Aviso", "Deseja realmente sair?", function() {
                window.location.href="/Index?handler=logout"
            });
        });
    }
};


Site.notification = {
    alert: function(msg) {
        //
        swal(msg);
    },
    confirm: function(title, text, callbackOkResult, labelBtnOK, labelBtnCancel) {


        if (!labelBtnOK)
            labelBtnOK = "Sim";

        if (!labelBtnCancel)
         labelBtnCancel = "Cancelar"

        

        swal({
            title: title,
            text: text,
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#0CC27E',
            cancelButtonColor: '#FF586B',
            confirmButtonText: labelBtnOK,
            cancelButtonText: labelBtnCancel
        }).then(function(isConfirm) {
            if (isConfirm) {

                if (callbackOkResult && typeof (callbackOkResult) == "function")
                    callbackOkResult();
            }
        }).catch(swal.noop);
    }
}




$(document).ready(function() {
    Site.init();
})