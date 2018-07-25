

(function ($) {


    var RegisterSourceScrap = {
        init: function () {

            //binds
            $('#btnSaveRegister').on('click', RegisterSourceScrap.SubmitRegisterForm);
          

        },
        SubmitRegisterForm: function (event) {
        
            var arrSourceChecked = [];
         

            //var oTable = $("#dtEntityNotRelated").dataTable();
            //percorrer as linhas do datatable, dessa forma se um valor for marcado em uma das paginas ele
            //será considerado, se não for assim, somente os valores visiveis serão considerados.
            $(".chkIndustry").each(function (index, checkbox) {
                var checked = $(this).is(":checked");
                if (checked) {
                    var industryId = $(checkbox).data('industryid');
                    arrSourceChecked.push(industryId);
                }
            });


            if (arrSourceChecked.length > 0) {
                $('#selectedIndustries').val(arrSourceChecked.join(";"));
            }



           $('.formRegisterSource').submit();


        }
    }

    RegisterSourceScrap.init();

})(jQuery);