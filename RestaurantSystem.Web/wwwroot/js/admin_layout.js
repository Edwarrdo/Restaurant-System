$(document).ready(function () {

        $('#sidebarCollapse').on('click', function () {
            $('#sidebar').toggleClass('active');
            $('.footerParagraph').toggleClass('activeFooter');
    });

    $(".hoverLink").hover(function () {
        //$(".hoverLink").css({ "color:": " #69696A", "background": "#fff" })
        $(this).attr("src", function (index, attr) {
   
            return attr.replace(".png", "_hovered.png");
        });
        
    }, function () {
        $(this).attr("src", function (index, attr) {
            return attr.replace("_hovered.png", ".png");
        });
        });
    
    //$(".hoverLink0").hover(function () {
    //    var index = $(".hoverLink img").attr("src")
    //    $("img").attr("src", function (index, attr) {

    //        return attr.replace(".png", "_hovered.png");
    //    });
    //}, function () {
    //    $("img").attr("src", function (index, attr) {
    //        return attr.replace("_hovered.png", ".png");
    //    });
    //    });

    //$(".hoverLink1").hover(function () {
    //    var index = $(".hoverLink img").attr("src")
    //    $("img").attr("src", function (index, attr) {

    //        return attr.replace(".png", "_hovered.png");
    //    });
    //}, function () {
    //    $("img").attr("src", function (index, attr) {
    //        return attr.replace("_hovered.png", ".png");
    //    });
    //});
        $("select").attr('data-selected-text-format', 'count>3');
        $("select").attr('data-size', 5);
        $("select").attr('data-live-search', true);

});