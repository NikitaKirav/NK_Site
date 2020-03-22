CKEDITOR.replace('Text', {
    on: {
        save: function (evt) {
            // Do something here, for example:
            //console.log(evt.editor.getData());
            var textNew = CKEDITOR.instances.Text.getData();
            $('#Text').val(textNew);
            $.ajax({
                url: "/Articles/Edit/@Model.Id",
                method: "post",
                contentType: "application/x-www-form-urlencoded",
                data: $("#editForm").serialize()
            }).done(function () {
            });
            return false;
        }
    }
});
$("#saveButton").click(function () {
    var textNew = CKEDITOR.instances.Text.getData();    
    $('#Text').val(textNew);
    $.ajax({
        url: "/Articles/Create/@Model.Id",
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        data: $("#editForm").serialize(),
        success: function (response) {
            //debugger;
            document.getElementById('Id').value = response.id;
            document.getElementById('Number').value = response.number;
            document.getElementById('DateCreate').value = new Date(response.dateCreate).toLocaleString();
            document.getElementById('DateChange').value = new Date(response.dateChange).toLocaleString();
        }
    });
});

$("#saveAndExitButton").click(function () {
    var textNew = CKEDITOR.instances.Text.getData();
    $('#Text').val(textNew);
    $.ajax({
        url: "/Articles/Create/@Model.Id",
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        data: $("#editForm").serialize()
    }).done(function () {
        location.href = "/Articles/List";
    });
});