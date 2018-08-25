$(document).ready(function () {

        $('#sidebarCollapse').on('click', function () {
            $('#sidebar').toggleClass('active');
        });

        $("select").attr('data-selected-text-format', 'count>3');
        $("select").attr('data-size', 5);
        $("select").attr('data-live-search', true);

});