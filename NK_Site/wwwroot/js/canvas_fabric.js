var canvas = new fabric.Canvas('c');

fabric.Object.prototype.transparentCorners = false;
canvas.backgroundColor = "#1E1E1E";

ctx = canvas.getContext('2d'), // Контекст
pic = new Image();              // "Создаём" изображение
pic.src = 'images/art/imageDrawing/default/default.png';  // Источник изображения, позаимствовано на хабре
pic.onload = function () {    // Событие onLoad, ждём момента пока загрузится изображение
    ctx.drawImage(pic, 0, 220);  // Рисуем изображение от точки с координатами 0, 0
}

canvas.renderAll();

var drawingModeEl = document.getElementById('drawing-mode'),
    drawingOptionsEl = document.getElementById('drawing-mode-options'),
    drawingColorEl = document.getElementById('drawing-color'),
    drawingShadowColorEl = document.getElementById('drawing-shadow-color'),
    drawingLineWidthEl = document.getElementById('drawing-line-width'),
    drawingShadowWidth = document.getElementById('drawing-shadow-width'),
    drawingShadowOffset = document.getElementById('drawing-shadow-offset'),
    saveImg = document.getElementById('saveImg'),
    clearEl = document.getElementById('clear-canvas');

clearEl.onclick = function () { canvas.clear() };

drawingModeEl.onclick = function () {
    canvas.isDrawingMode = !canvas.isDrawingMode;
    if (canvas.isDrawingMode) {
        drawingModeEl.innerHTML = 'Cancel drawing mode';
        drawingOptionsEl.style.display = '';
    }
    else {
        drawingModeEl.innerHTML = 'Enter drawing mode';
        drawingOptionsEl.style.display = 'none';
    }
};

if (fabric.PatternBrush) {
    var vLinePatternBrush = new fabric.PatternBrush(canvas);
    vLinePatternBrush.getPatternSrc = function () {

        var patternCanvas = fabric.document.createElement('canvas');
        patternCanvas.width = patternCanvas.height = 10;
        var ctx = patternCanvas.getContext('2d');

        ctx.strokeStyle = this.color;
        ctx.lineWidth = 5;
        ctx.beginPath();
        ctx.moveTo(0, 5);
        ctx.lineTo(10, 5);
        ctx.closePath();
        ctx.stroke();

        return patternCanvas;
    };

    var hLinePatternBrush = new fabric.PatternBrush(canvas);
    hLinePatternBrush.getPatternSrc = function () {

        var patternCanvas = fabric.document.createElement('canvas');
        patternCanvas.width = patternCanvas.height = 10;
        var ctx = patternCanvas.getContext('2d');

        ctx.strokeStyle = this.color;
        ctx.lineWidth = 5;
        ctx.beginPath();
        ctx.moveTo(5, 0);
        ctx.lineTo(5, 10);
        ctx.closePath();
        ctx.stroke();

        return patternCanvas;
    };

    var squarePatternBrush = new fabric.PatternBrush(canvas);
    squarePatternBrush.getPatternSrc = function () {

        var squareWidth = 10, squareDistance = 2;

        var patternCanvas = fabric.document.createElement('canvas');
        patternCanvas.width = patternCanvas.height = squareWidth + squareDistance;
        var ctx = patternCanvas.getContext('2d');

        ctx.fillStyle = this.color;
        ctx.fillRect(0, 0, squareWidth, squareWidth);

        return patternCanvas;
    };

    var diamondPatternBrush = new fabric.PatternBrush(canvas);
    diamondPatternBrush.getPatternSrc = function () {

        var squareWidth = 10, squareDistance = 5;
        var patternCanvas = fabric.document.createElement('canvas');
        var rect = new fabric.Rect({
            width: squareWidth,
            height: squareWidth,
            angle: 45,
            fill: this.color
        });

        var canvasWidth = rect.getBoundingRect().width;

        patternCanvas.width = patternCanvas.height = canvasWidth + squareDistance;
        rect.set({ left: canvasWidth / 2, top: canvasWidth / 2 });

        var ctx = patternCanvas.getContext('2d');
        rect.render(ctx);

        return patternCanvas;
    };


    var img = new Image();
    img.src = 'images/art/imageDrawing/default/0.png';

    var texturePatternBrush = new fabric.PatternBrush(canvas);
    texturePatternBrush.source = img;
}

document.getElementById('drawing-mode-selector').onchange = function () {

    if (this.value === 'hline') {
        canvas.freeDrawingBrush = vLinePatternBrush;
    }
    else if (this.value === 'vline') {
        canvas.freeDrawingBrush = hLinePatternBrush;
    }
    else if (this.value === 'square') {
        canvas.freeDrawingBrush = squarePatternBrush;
    }
    else if (this.value === 'diamond') {
        canvas.freeDrawingBrush = diamondPatternBrush;
    }
    else if (this.value === 'texture') {
        canvas.freeDrawingBrush = texturePatternBrush;
    }
    else {
        canvas.freeDrawingBrush = new fabric[this.value + 'Brush'](canvas);
    }

    if (canvas.freeDrawingBrush) {
        canvas.freeDrawingBrush.color = drawingColorEl.value;
        canvas.freeDrawingBrush.width = parseInt(drawingLineWidthEl.value, 10) || 1;
        canvas.freeDrawingBrush.shadow = new fabric.Shadow({
            blur: parseInt(drawingShadowWidth.value, 10) || 0,
            offsetX: 0,
            offsetY: 0,
            affectStroke: true,
            color: drawingShadowColorEl.value,
        });
    }
};

drawingColorEl.onchange = function () {
    canvas.freeDrawingBrush.color = this.value;
};
drawingShadowColorEl.onchange = function () {
    canvas.freeDrawingBrush.shadow.color = this.value;
};
drawingLineWidthEl.onchange = function () {
    canvas.freeDrawingBrush.width = parseInt(this.value, 10) || 1;
    this.previousSibling.innerHTML = this.value;
};
drawingShadowWidth.onchange = function () {
    canvas.freeDrawingBrush.shadow.blur = parseInt(this.value, 10) || 0;
    this.previousSibling.innerHTML = this.value;
};
drawingShadowOffset.onchange = function () {
    canvas.freeDrawingBrush.shadow.offsetX = parseInt(this.value, 10) || 0;
    canvas.freeDrawingBrush.shadow.offsetY = parseInt(this.value, 10) || 0;
    this.previousSibling.innerHTML = this.value;
};

if (canvas.freeDrawingBrush) {
    canvas.freeDrawingBrush.color = drawingColorEl.value;
    canvas.freeDrawingBrush.width = parseInt(drawingLineWidthEl.value, 10) || 1;
    canvas.freeDrawingBrush.shadow = new fabric.Shadow({
        blur: parseInt(drawingShadowWidth.value, 10) || 0,
        offsetX: 0,
        offsetY: 0,
        affectStroke: true,
        color: drawingShadowColorEl.value,
    });
}
saveImg.onclick = function () {
    var canvas = document.getElementById('c');
    var context2D = canvas.getContext('2d');
    context2D.lineWidth = 5;
    context2D.beginPath();
    context2D.moveTo(1, 1);
    context2D.lineTo(499, 1);
    context2D.lineTo(499, 499);
    context2D.lineTo(1, 499);
    context2D.closePath();
    context2D.strokeStyle = "gray";
    context2D.stroke();

    var image = document.getElementById("c").toDataURL("image/png");
    image = image.replace('data:image/png;base64,', '');
    $.ajax({
        type: 'POST',
        url: '/Art/UploadImage',
        data: '{ "ImageData" :"' + image + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            if (msg.result == 'Saved') {
                alert('Image saved successfully !');
                resize_info();
            }
            else {
                let time = msg.result != '0' ? msg.result : '1';
                alert('The time from last changes isn\'t up. Try again in ' + time + ' minute(s)');
            }
            
        }
    });
}
