$(document).ready(function () {

        $('#sidebarCollapse').on('click', function () {
            $('#sidebar').toggleClass('active');
            $('.footerParagraph').toggleClass('activeFooter');
    });


    $('.hoverLink').mouseover(function (e) {
        
        $(this).find("img").attr("src", function (index, attr) {
            return attr.replace(".png", "_hovered.png");
        });
       
    });;
  

    $('.hoverLink').mouseout(function () {
       $(this).find("img").attr("src", function (index, attr) {
            return attr.replace("_hovered.png", ".png");
        });
       
    });


        $("select").attr('data-selected-text-format', 'count>3');
        $("select").attr('data-size', 5);
        $("select").attr('data-live-search', true);

});