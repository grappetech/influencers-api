

(function ($) {


    var RegisterEntity = {
        init: function () {

            //binds
            $('#btnSaveRegister').on('click', RegisterEntity.SubmitRegisterForm);
            
        },
        SubmitRegisterForm: function (event) {
            console.log('oi')
          ///Obter todos as entitydaes marcadas com o checkbox
            var arrEntityChecked = [];
            var arrEntityUnChecked = [];


            var oTable = $("#dtEntityNotRelated").dataTable();

            var dtTableEntityRelated = $("#dtEntityRelated").dataTable();

     
            
            //percorrer as linhas do datatable, dessa forma se um valor for marcado em uma das paginas ele
            //será considerado, se não for assim, somente os valores visiveis serão considerados.
            $(".chkUnRelatedEntity:checked", oTable.fnGetNodes()).each(function(index, checkbox) {
                var checked = $(this).is(":checked");
                if (checked) {
                    var entityCoreId = $(checkbox).data('entitycoreid');
                    arrEntityChecked.push(entityCoreId);
                }
            });

            

            $(".chkRelatedEntity", dtTableEntityRelated.fnGetNodes()).each(function(index, checkbox) {
                var checked = $(this).is(":checked");
                

                if (checked == false) {
                    var entityCoreId = $(checkbox).data('entitycoreid');
                    arrEntityUnChecked.push(entityCoreId);
                }
            })



            if (arrEntityChecked.length > 0) {
                $('#selectedUnrelatedEntity').val(arrEntityChecked.join(";"));
            }

            if (arrEntityUnChecked.length > 0) {
                $('#unselectedEntity').val(arrEntityUnChecked.join(";"));
            }


            $('.formRegisterEntity').submit();

          
        }
    }

    RegisterEntity.init();

})(jQuery);