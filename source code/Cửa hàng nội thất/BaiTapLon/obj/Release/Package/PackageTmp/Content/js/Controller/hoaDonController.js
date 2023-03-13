var hoadon = {
    init: function () {
        hoadon.registerEvents();
    },
    registerEvents: function (e) {
        $('.btn-active-Status').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/HoaDon/ChangeStatusOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.status == 0) {
                            //gán text box thành kích hoạt
                            btn.text('Duyệt Đơn');

                           
                        }
                        else {

                            btn.text('Đã Duyệt');
                            
                        }
                    }
                });
        });

    }
}




var giaohangChange = {
    init: function () {
        giaohangChange.registerEvents();
    },
    registerEvents: function (e) {
        $('.btn-active-change').click('on').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn2 = $('.btn-active-change');
            var id = btn2.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/HoaDon/ChangeGiaoHangThatBai",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.GiaoHang == 1) {
                            //gán text box thành kích hoạt
                            btn2.text('Đã giao hàng');
                        }else if(response.GiaoHang == 2)
                        {
                            btn2.text('Đã hoàn tất');
                        }
                        else {

                            btn2.text('Trả lại hàng');
                        }
                    }
                });
        });

    }
}
var hoanThanh = {
    init: function () {
        hoanThanh.registerEvents();
    },
    registerEvents: function (e) {
        $('.btn-active-success').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);
            var btn2 = $('.btn-active-change');
            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/HoaDon/ChangeSuccessOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.NhanHang == 1) {
                            //gán text box thành kích hoạt
                            btn.text('Đã nhận hàng');
                            
                            //window.location.href = "/Admin/HoaDon/HoanThanh";
                            
                        } else if (response.NhanHang == 2) {
                            btn.text('Trả hàng lại');
                            
                            //window.location.href = "/Admin/HoaDon/HoanThanh";
                        }
                        else {

                            btn.text('Chưa nhận hàng');
                            
                            //window.location.href = "/Admin/HoaDon/HoanThanh";
                        }
                    }
                });
        });

    }
}



var exportFile = {
    init: function () {
        exportFile.registerEvents();
    },
    registerEvents: function (e) {
        $('.buttonExport').off('click').on('click', function (e) {
            var oderID = $(this).data('id');
            e.preventDefault();
            $.ajax({
                url: "/Admin/HoaDon/ExportExel",
                data: { id: oderID },// truyền vào tham số của cái id đấy id chính là id truyền vào
                dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                type: "GET",// trên controler dăt post,
                success: function(res)
                {
                    if (res !== '') {
                        location.href = "/Resource/ExportFile/" + res;
                    }
                }
            });
        });
    }
}

var exportDoanhSo = {
    init: function () {
        exportDoanhSo.registerEvents();
    },
    registerEvents: function (e) {
        $('.btnExportDoanhSo').off('click').on('click', function (e) {
            var formDate = $(this).data('fromdate');
            var toDate = $(this).data('todate');
            e.preventDefault();
            $.ajax({
                url: "/Admin/ThongKe/ExportExel",
                data: {
                    fromDate: formDate,
                    toDate: toDate
                },// truyền vào tham số của cái id đấy id chính là id truyền vào
                dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                type: "GET",// trên controler dăt post,
                success: function (res) {
                    if (res !== '') {
                        notificationService().displaySuccess("Xuất dữ liệu sang file excel thành công.");
                        location.href = "/Resource/ExportFile/" + res;
                    }
                }
            });
        });
    }
}
function notificationService() {
    toastr.options = {
        "debug": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timOut": 3000,
        "extendedTimOut": 1000
    }

    function displaySuccess(message) {
        toastr.success(message);
    }
    function displayError(error) {
        if (Array.isArray(error)) {
            error.each(function (err) {
                toastr.error(err);
            });
        } else {
            toastr.error(error);
        }
    }

    function displayWarning(message) {
        toastr.warning(message);
    }
    function displayInfo(message) {
        toastr.info(message);
    }
    return {
        displaySuccess: displaySuccess,
        displayError: displayError,
        displayWarning: displayError,
        displayInfo: displayInfo
    }
};
exportFile.init();
exportDoanhSo.init();
hoanThanh.init();
notificationService();