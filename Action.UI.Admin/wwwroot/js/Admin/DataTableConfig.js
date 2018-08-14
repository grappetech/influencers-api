(function ($) {


    var jqDatatableLanguage =

        {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }


    $(document).ready(function() {


        //ocultar enquanto é carregado
        //$('#dtEntityNotRelatedContainer, #dtEntityRelatedContainer').hide();

        //definir jquery data table
        $('.dtscroll-vertical').DataTable({
            "scrollY": "350px",
            "scrollCollapse": true,
            "language": jqDatatableLanguage,
            "paging": true,
            "drawCallback": function(settings) {
                //var $dt = $(this);
                //var container = $dt.data('containerid');
                //console.log($dt)
                //$(container).show();
            }
        });


        $('.data-table').DataTable({          
            "paging": true,
            "language": jqDatatableLanguage,
            "order": [[0, "desc"]]
        });

     


    })

})(jQuery);