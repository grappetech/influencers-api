


(function ($) {


    var RegisterEntity = {
        init: function () {

            //binds
            //dtEntityScrapSource
            $('#btnSaveRegister').on('click', RegisterEntity.SubmitRegisterForm);
            $('#ddlScrapsource').on('change', RegisterEntity.SourceSelected);
            $('#ddlCategory').on('change', RegisterEntity.ChangeCategory);
            $('.btnScrapSource').on('click', RegisterEntity.GetScrapSourceInformation);


            RegisterEntity.FillForm();


        },
        GetScrapSourceInformation: function () {
            var scrapSourceId = $(this).data('scrapsourceid');


            //obter o detalhe
            var url = 'http://localhost:60294/Logged/Sources/SourceInfo/' + scrapSourceId;
            Site.ajax.get(url, function (response) {


                if (response && response.status) {
                    var data = response.data;
                    var sourceName = data.alias;
                    var html = "<small><strong>Informações sobre a fonte.</strong></small>";
                    //html += "<h2>" + data.alias + "</h2>";
                    html += "<br />";
                    html += "<br />";
                    

                    if (data.industries && data.industries.length > 0) {
                        var industries = data.industries;
                        html += "<table class='table table-bordered table-hover table-sm '>";
                        
                        html += "<tr'><theade><th>Indústria</th></thead></tr>";

                        html += "<tbody>";
                        for (var i = 0, total = industries.length; i < total; i++) {
                            var industry = industries[i].industry;

                           

                            if (industry)
                                html += "<tr><td>" + industry.name + "</td></tr>";

                          
                        }
                        html += "<tbody>";
                        html += "</table>";
                    } else {
                        html += "<small>Nenhuma indústria vinculada a esta fonte</small>";
                    }

                    Site.notification.htmlMessage(sourceName, html);

                }
            });


        },

        ChangeCategory: function () {


            /*
             * 2 == Pessoa
             * 0 == Marca
             * */

            $('#divIndustry').hide();
            var categoryId = $(this).val();

            RegisterEntity.SelectCategory(categoryId);


        },

        SelectCategory: function (categoryId) {
            if (categoryId == 0)
                RegisterEntity.ShowBrandCategoryOption();

            if (categoryId == 2)
                RegisterEntity.ShowPersonCategoryOption();
        },

        ShowBrandCategoryOption: function () {
            $('#divRoles').hide();
            $('.person-relation').hide()
            $('#divIndustry').fadeIn('normal');
        },
        ShowPersonCategoryOption: function () {

            $('#divIndustry').hide();
            $('#divRoles').fadeIn('normal');
            $('.person-relation').fadeIn('normal')

        },
        FillForm: function () {


            $('#divIndustry').hide();
            $('#divRoles').hide();



            //preencher as roles selecionadas
            var selectdRoles = $('#selectedRoles').val();

            var categoryId = $('#ddlCategory').val();

            if (categoryId && categoryId != "") {

                RegisterEntity.SelectCategory(categoryId);
            }


            if (selectdRoles && selectdRoles != null) {
                var arrRolesChecked = selectdRoles.split(";");

                if (Array.isArray(arrRolesChecked)) {

                    $.each(arrRolesChecked, function (index, roleId) {
                        $('#chkRole_' + roleId).prop('checked', true)
                    });
                }

            }

        },
        SourceSelected: function () {


            var scrapUrl = $(this).find(":selected").data("scrapurl");
            $('#labelScrapUrl').text("").text(scrapUrl);
        },

        Validate: function () {
            var errors = [];

            var entityName = $('#entityName').val();


            if (!entityName || entityName == "" || entityName.length == 0)
                errors.push('Informe um nome para entidade');


            return errors;

        },
        getEntityChecked: function () {
            var arrEntityChecked = [];
            var oTable = $("#dtEntityNotRelated").dataTable();
            //percorrer as linhas do datatable, dessa forma se um valor for marcado em uma das paginas ele
            //será considerado, se não for assim, somente os valores visiveis serão considerados.
            $(".chkUnRelatedEntity:checked", oTable.fnGetNodes()).each(function (index, checkbox) {
                var checked = $(this).is(":checked");
                if (checked) {
                    var entityCoreId = $(checkbox).data('entitycoreid');
                    arrEntityChecked.push(entityCoreId);
                }
            });

            return arrEntityChecked;
        },
        getRoles: function () {

            var arrRolesChecked = [];

            $('.chkRoles:checked').each(function (index, checkbox) {
                var checked = $(checkbox).is(":checked");
                var roleId = $(checkbox).val();
                if (checked) {

                    arrRolesChecked.push(roleId);
                }
            });

            return arrRolesChecked;

        },
        SubmitRegisterForm: function (event) {

            ///Obter todos as entitydaes marcadas com o checkbox
            var arrEntityChecked = [];
            var arrEntityUnChecked = [];
            var arrRolesChecked = [];
            var arrScrapSourceChecked = [];

            var arrErrors = RegisterEntity.Validate();

            if (arrErrors.length > 0) {

                Site.notification.error(arrErrors.join("\n"));
                return;
            }


          
            var dtTableEntityRelated = $("#dtEntityRelated").dataTable();
            var dtTableScrapSources = $("#dtEntityScrapSource").dataTable();


            arrEntityChecked = RegisterEntity.getEntityChecked();      

            arrRolesChecked = RegisterEntity.getRoles();

         

            //dtEntityScrapSource
            $(".chkScrapSource", dtTableScrapSources.fnGetNodes()).each(function (index, checkbox) {
                var checked = $(this).is(":checked");

                if (checked) {
                    var scrapSourceId = $(checkbox).val();
                    arrScrapSourceChecked.push(scrapSourceId);
                }
            })



            $(".chkRelatedEntity", dtTableEntityRelated.fnGetNodes()).each(function (index, checkbox) {
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


            if (arrRolesChecked.length > 0) {
                $('#selectedRoles').val(arrRolesChecked.join(";"));
            }


            if (arrScrapSourceChecked.length > 0) {
                $('#selectedSources').val(arrScrapSourceChecked.join(";"));
            }

            //tratamento da data no componente
            var birthDate = $("#birthDate").val();

            if (birthDate != "" && birthDate.length > 0) {
                var arrBirthDate = $("#birthDate").val().split("/");

                if (arrBirthDate != null && Array.isArray(arrBirthDate) && arrBirthDate.length == 3) {
                    var _birthDate = arrBirthDate.reverse().join("-");
                    $("#birthDate").val(_birthDate)

                }

                
            }
           



            $('.formRegisterEntity').submit();


        }
    }


    $(document).ready(function () {
        RegisterEntity.init();
    })

})(jQuery);