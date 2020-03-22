$(".saveButton").click(function () {
    var id = $(this).attr("data-id");
    $.ajax({
        url: "/Constants/Save/",
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        data: {
            Id: $(this).attr("data-id"),
            Name: document.getElementById("Name" + id).value,
            Value: document.getElementById("Value" + id).value
        },
        success: function (result) {
            if (result)
                alert("Data was saved success!");
            else
                alert("Data wasn't saved!");
        }
    });
});

$(".addButton").click(function () {
    $.ajax({
        url: "/Constants/Create/",
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        data: {
            Id: 0,
            Name: document.getElementById("NameNew").value,
            Value: document.getElementById("ValueNew").value
        },
        success: function (result) {
            if (result) {
                alert("Data was added success!");
                location.href = "/Constants";
            }
            else
                alert("Data wasn't added!");
        }
    });
});