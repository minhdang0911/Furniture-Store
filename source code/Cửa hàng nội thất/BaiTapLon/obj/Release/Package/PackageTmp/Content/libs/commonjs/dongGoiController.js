/// <reference path="angular/angular.js" />

var donggoi = {
    init: function () {
        donggoi.registerEvents();
    },
    registerEvents: function (e) {
        $('#spanDongGoi').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var donggoi = $(this);
            var id = $(this).data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/HoaDon/ChangeGiaoHangOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.GiaoHang == 0) {
                            notificationService().displayWarning("Hóa Đơn" + id + " đã hủy giao hàng.")
                            donggoi.remove(donggoi);
                        }
                        else if(response.GiaoHang == 1){
                           
                            notificationService().displaySuccess("Hóa đơn " + id + " đang chờ xuất kho.")
                           
                        }
                            /*
                        else {
                            btn.text('Hoàn Thành');
                            btn.removeClass("badge badge-warning text-white");
                            //step.removeClass('step');
                            //step.addClass("step active");
                            btn.addClass("badge badge-success text-white")
                            notificationService().displaySuccess("Mã Đơn Nhập "+id+" đã hoàn thành.")
                        }*/
                    }
                });
        });

    }
}

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
                            btn.removeClass("btn-active-Status badge badge-success")
                            btn.addClass("btn-active-Status badge badge-info");
                            notificationService().displayWarning("Đơn hàng "+id+" không được duyệt")

                        }
                        else {

                            btn.text('Đã Duyệt');
                            btn.removeClass("btn-active-Status badge badge-info")
                            btn.addClass("btn-active-Status badge badge-success");
                            notificationService().displaySuccess("Đơn hàng " + id + " được duyệt chờ đóng gói.")
                        }
                    }
                });
        });

    }
}


var xuatkho = {
    init: function () {
        xuatkho.registerEvents();
    },
    registerEvents: function (e) {
        $('#spanXuatKho').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/HoaDon/ChangeXuatKhoOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.XuatKho == 1) {
                            notificationService().displayWarning("Đơn xuất kho " + id + " đã giữ lại.")
                            
                        }
                        else if (response.XuatKho == 2) {

                            notificationService().displaySuccess("Đơn xuất kho " + id + " đã được giao cho vận chuyển.")

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
                        } else if (response.GiaoHang == 2) {
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


exportFile.init();

hoadon.init();

hoanThanh.init();
donggoi.init();
xuatkho.init();
notificationService();
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

/*
(function (app) {
    app.factory('notificationService', notificationService);

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
})(angular.module('app'));*/