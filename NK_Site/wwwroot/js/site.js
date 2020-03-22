// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function resize_info() {
    var canvasPicture = document.getElementById('canvasPicture');
    ctx = canvasPicture.getContext('2d'),
        cellSize = 20,
        pic = new Image(),
        width = document.body.clientWidth, // ширина  
        height = document.body.clientHeight; // высота

    $.ajax({
        type: 'Get',
        url: '/Art/ImagePaths',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {


            canvasPicture.height = 7 * cellSize;
            canvasPicture.width = width;
            for (var k = 0; k < msg.length; k++) {
                pic.src = 'images/art/imageDrawing/' + msg[k];
                pic.onload = function () {
                    for (var j = width; j > -cellSize; j = j - cellSize) {
                        for (var i = 0; i < 7; i++) {
                            ctx.drawImage(pic, j, i * cellSize, 20, 20);
                        }
                    }
                }
            }
        }
    });

}