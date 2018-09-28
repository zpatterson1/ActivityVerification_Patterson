
$(function () {
    $(".detailsItem").click(function () {
        var $Item = $(this);
        id = $Item.attr('data-id');
        var options = { backdrop: true, keyboard: false, show: true, focus: false };
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/Home/_details/' + id,
            datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $(".close").click(function () {
        $('#myModal').modal('hide');
    });
});