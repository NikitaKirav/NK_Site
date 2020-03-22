window.onload = function () {
    var level = document.getElementById("DifficultyLevel").value;
    if (level == '0') { level = '1'; }
    switch (level) {
        case '1':  level1();
            break;
        case '2':  level2();
            break;
        case '3':  level3();
            break;
        case '4':  level4();
            break;
        case '5':  level5();
            break;
    }
}

$("#level1").click(function () {    
    $('#DifficultyLevel').val(1);
    level1();
});

$("#level2").click(function () {
    $('#DifficultyLevel').val(2);
    level2();
});

$("#level3").click(function () {
    $('#DifficultyLevel').val(3);
    level3();
});

$("#level4").click(function () {
    $('#DifficultyLevel').val(4);
    level4();
});

$("#level5").click(function () {
    $('#DifficultyLevel').val(5);
    level5();
});

function level1() {
    document.querySelector('#level1').style.backgroundColor = '#47a64a';
    document.querySelector('#level1').style.border = "";
    document.querySelector('#level2').style.backgroundColor = "";
    document.querySelector('#level2').style.border = "1px inset #8f8f8f";
    document.querySelector('#level3').style.backgroundColor = "";
    document.querySelector('#level3').style.border = "1px inset #8f8f8f";
    document.querySelector('#level4').style.backgroundColor = "";
    document.querySelector('#level4').style.border = "1px inset #8f8f8f";
    document.querySelector('#level5').style.backgroundColor = "";
    document.querySelector('#level5').style.border = "1px inset #8f8f8f";
}

function level2() {
    document.querySelector('#level1').style.backgroundColor = '#47a64a';
    document.querySelector('#level1').style.border = "";
    document.querySelector('#level2').style.backgroundColor = '#aec20c';
    document.querySelector('#level2').style.border = "";
    document.querySelector('#level3').style.backgroundColor = "";
    document.querySelector('#level3').style.border = "1px inset #8f8f8f";
    document.querySelector('#level4').style.backgroundColor = "";
    document.querySelector('#level4').style.border = "1px inset #8f8f8f";
    document.querySelector('#level5').style.backgroundColor = "";
    document.querySelector('#level5').style.border = "1px inset #8f8f8f";
}

function level3() {
    document.querySelector('#level1').style.backgroundColor = '#47a64a';
    document.querySelector('#level1').style.border = "";
    document.querySelector('#level2').style.backgroundColor = '#aec20c';
    document.querySelector('#level2').style.border = "";
    document.querySelector('#level3').style.backgroundColor = '#f1c40f';
    document.querySelector('#level3').style.border = "";
    document.querySelector('#level4').style.backgroundColor = "";
    document.querySelector('#level4').style.border = "1px inset #8f8f8f";
    document.querySelector('#level5').style.backgroundColor = "";
    document.querySelector('#level5').style.border = "1px inset #8f8f8f";
}

function level4() {
    document.querySelector('#level1').style.backgroundColor = '#47a64a';
    document.querySelector('#level1').style.border = "";
    document.querySelector('#level2').style.backgroundColor = '#aec20c';
    document.querySelector('#level2').style.border = "";
    document.querySelector('#level3').style.backgroundColor = '#f1c40f';
    document.querySelector('#level3').style.border = "";
    document.querySelector('#level4').style.backgroundColor = '#f1650f';
    document.querySelector('#level4').style.border = "";
    document.querySelector('#level5').style.backgroundColor = "";
    document.querySelector('#level5').style.border = "1px inset #8f8f8f";
}

function level5() {
    document.querySelector('#level1').style.backgroundColor = '#47a64a';
    document.querySelector('#level1').style.border = "";
    document.querySelector('#level2').style.backgroundColor = '#aec20c';
    document.querySelector('#level2').style.border = "";
    document.querySelector('#level3').style.backgroundColor = '#f1c40f';
    document.querySelector('#level3').style.border = "";
    document.querySelector('#level4').style.backgroundColor = '#f1650f';
    document.querySelector('#level4').style.border = "";
    document.querySelector('#level5').style.backgroundColor = '#aa0505';
    document.querySelector('#level5').style.border = "";
}
