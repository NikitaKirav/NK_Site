$(document).ready(function () {
    resize_info();
    $(window).resize(function () {
        resize_info();
    });
});
$(window).resize(function () {
    resize_info();
});

function resize_info() {
    $.ajax({
        type: 'Get',
        url: '/Art/ImagePaths',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            LoadImagesInCanvas(msg);
        }
    });

    function LoadImagesInCanvas(msg) {

        var width = document.body.clientWidth, // width 
            height = document.body.clientHeight; // height
        var cellSize = 60;
        let imagesCanvas = document.getElementById('canvasPicture');
        let imgCtx = imagesCanvas.getContext('2d');
        let images = [];
        let countCell = Math.ceil(((width + cellSize) / cellSize) * 3);
        var k = msg.length-1;
        for (var i = 0; i < countCell; i++) {
            images[i] = 'images/art/imageDrawing/' + msg[k];
            if (i >= msg.length) {
                    images[i] = 'images/art/imageDrawing/default/0.png';
            }
            k--;
        }
        let loaded = [];
        imagesCanvas.height = 3 * cellSize;
        imagesCanvas.width = width;
        let row = 0;
        load();// Load pictures

        function load() {
            let name = images.shift();
            let img = new Image();
            img.crossOrigin = "anonymous";
            // Call the next image to load, until they are all loaded
            // Then they are all loaded, drawing on the canvas
            img.onload = () => add() | images.length ? load() : requestAnimationFrame(draw);
            img.src = name;
                        
            // Add info about a picture in the array 
            function add() {
                row++;
                loaded.push({
                    name: name, img: img,
                    x: width - Math.ceil((loaded.length +1 ) / 3) * cellSize,
                    y: row == 3 ? 0 :
                        row == 2 ? cellSize : 2 * cellSize,
                    w: cellSize, h: cellSize
                });
                if (row == 3) { row = 0; }
            }
        }

        function draw() {
            loaded.forEach((img, i) => {
                // Drow pictures on the canvas
                imgCtx.drawImage(img.img, img.x, img.y, img.w, img.h);
            })
        }
    }

}

function resize_canvas() {
    canvas = document.getElementById("c");
    if (canvas.width < window.innerWidth) {
        canvas.width = window.innerWidth;
    }

    if (canvas.height < window.innerHeight) {
        canvas.height = window.innerHeight;
    }
}

