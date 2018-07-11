(function($) {

    $(document).ready(function() {


        //ocultar enquanto é carregado
        //$('#dtEntityNotRelatedContainer, #dtEntityRelatedContainer').hide();

        //definir jquery data table
        $('.dtscroll-vertical').DataTable({
            "scrollY": "350px",
            "scrollCollapse": true,
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
            "order": [[0, "desc"]]
        });

     


    })

})(jQuery);