var regexp = /pageindex=([^&]+)/i;
var regexp2 = /s=/i;
var GetValue = '';
var GetValue2 = '';
if (!!regexp.exec(document.location.search)) {
    GetValue = regexp.exec(document.location.search)[1];
}
if (!!regexp2.exec(document.location.search)) {
    GetValue2 = "true";
}
if ((GetValue != '')||(GetValue2 != '')) {
    var elem = document.getElementById('aboutBlog');
    elem.style.display = 'none'; // hide
}