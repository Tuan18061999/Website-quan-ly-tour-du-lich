$(document).ready(function () {
    LoadData();
});
function LoadData() {
    $.ajax({
        url: "/ajax/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MaSoTour + '</td>';
                html += '<td>' + item.TenTour + '</td>';
                html += '<td>' + item.NgayBatDau + '</td>';
                html += '<td>' + item.NgayKetThuc + '</td>';
                html += '<td>' + item.MoTaTour + '</td>';
                html += '<td>' + item.Gia + '</td>';
                html += '<td>' + item.MaLoaiTour + '</td>';
                html += '<td>' + item.MaSoTinh + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a>|'
                    + '<a href="#" onclick="Delete(' + item.id + ')">Delete</a></td>';
                html += '</tr>';

            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Add() {
    var empObj =
        {
            MaSoTour: $('#MaTour').val(),
            TenTour: $('#TenTour').val(),
            NgayBatDau: $('#NgayBD').val(),
            NgayKetThuc: $('#NgayKT').val(),
            MoTaTour: $('#MaTour').val(),
            Gia: $('#Gia').val(),
            MaLoaiTour: $('#MaLoai').val(),
            MaSoTinh: $('#SoTinh').val(),
        };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}