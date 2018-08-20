// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//RESPONSIVE FIRST AND SECOND ROW

//var h = $(window).height()-100;
//$('#firstRow').css('height', h);
$(document).ready(function () {
    //console.log("ready!");

//NAVBAR SCRIPT ON SCROLLING
$("nav ul a[href^='#']").on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({
        scrollTop: $(this.hash).offset().top
    },
        1500,
        function () { });
});
//$('.carousel').carousel({
//    interval: 1000 * 10
//});
var $item = $('.carousel-item');
var $wHeight = $(window).height()-70;
$item.eq(0).addClass('active');
$item.height($wHeight);
$item.addClass('full-screen');

$('.carousel img').each(function () {
    var $src = $(this).attr('src');

    $(this).parent().css({
        'background-image': 'url(' + $src + ')',
    });
    $(this).remove();
});

$(window).on('resize', function () {
    $wHeight = $(window).height()-70;
    $item.height($wHeight);
});

$('.carousel').carousel({
    interval: 6000,
    pause: "false"
    });
});