

(function ($) {


    var RegisterSourceScrap = {
        init: function () {

            //binds
            $('#btnSaveRegister').on('click', RegisterSourceScrap.SubmitRegisterForm);
          

        },

        Validate: function () {

            var errors = [];
            //Informar ao menos o alias
            var alias = $('#sourceAlias').val();
            if (!alias || alias == "" || alias.length == 0) {

                errors.push('Informe um alias para a fonte')
            }


            var arrSourceChecked = RegisterSourceScrap.getCategoriesIndustry();

            if (arrSourceChecked.length == 0)
                errors.push('Informe ao menos uma categoria')

         
            return errors;
        },

        getCategoriesIndustry: function () {

            var arrSourceChecked = [];
            $(".chkIndustry").each(function (index, checkbox) {
                var checked = $(this).is(":checked");
                if (checked) {
                    var industryId = $(checkbox).data('industryid');
                    arrSourceChecked.push(industryId);
                }
            });

            return arrSourceChecked;
        },
        SubmitRegisterForm: function (event) {
        
            var arrSourceChecked = [];
            

            var arrErros = RegisterSourceScrap.Validate();

            if (arrErros.length > 0) {
                Site.notification.error(arrErros.join("\n"));
                return;

            }

         

            //var oTable = $("#dtEntityNotRelated").dataTable();
            //percorrer as linhas do datatable, dessa forma se um valor for marcado em uma das paginas ele
            //será considerado, se não for assim, somente os valores visiveis serão considerados.
            arrSourceChecked = RegisterSourceScrap.getCategoriesIndustry();


            if (arrSourceChecked.length > 0) {
                $('#selectedIndustries').val(arrSourceChecked.join(";"));
            }

            //Padronização dos valores do campo tagException
            //remover o caracter de quebra de linha e colocar espaço em branco
            var tagException = $('#tagException').val().replace(/\n/g, ' ');
            $('#tagException').val(tagException);



          $('.formRegisterSource').submit();


        }
    }

    RegisterSourceScrap.init();

})(jQuery);