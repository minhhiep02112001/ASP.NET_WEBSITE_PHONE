function AddCart(id, quatity) {
    $.ajax({
        url: '/gio-hang/AddCart/' + id + '/' + quatity,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (data.status == false) {
                alert(data.text);
            } else {
                location.href = "/gio-hang";
            }
        }
    });
}