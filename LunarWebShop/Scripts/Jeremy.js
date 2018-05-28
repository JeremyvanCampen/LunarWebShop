window.onload = function() {
    $('#PreviewProduct').click(function (event) {
        // 
        var url = "Producten/AsyncDetails/" + $('#PreviewProduct').data('productid');
        $.get(url, function (data) {
            $(".modal-body").html(data);
        });
    });
};