/// <reference path="angular/angular.js" />

var nhaphang = {
    init: function () {
        nhaphang.registerEvents();
    },
    registerEvents: function (e) {
        $('.active-Status').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);
            var step = $('#step1');
            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/NhapHang/DuyetDon",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.DuyetDon == 0) {
                            //gán text box thành kích hoạt
                            btn.text('Duyệt Đơn');
                            btn.removeClass("badge badge-warning text-white");
                            btn.addClass("badge badge-info text-white");
                            
                            //step.removeClass('step active');
                            //.addClass("step");
                            notificationService().displayWarning("Mã Đơn Nhập " + id + " đã bị hủy.")
                        }
                        else if(response.DuyetDon == 1){
                            btn.text('Đang Giao Dịch');
                            btn.removeClass("badge badge-info text-white");
                            //step.removeClass('step');
                            //step.addClass("step active");
                            btn.addClass("badge badge-warning text-white")
                            notificationService().displaySuccess("Mã Đơn Nhập " + id + " đang được tiến hành.")
                            
                        }
                           
                    }
                });
        });

    }
}

var exportNhapHang = {
    init: function () {
        exportNhapHang.registerEvents();
    },
    registerEvents: function (e) {
        $('.exportNhapHang').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
                var idNhapHang = $(this).data('id');
                e.preventDefault();
                $.ajax({
                    url: "/Admin/NhapHang/ExportExel",
                    data: { id: idNhapHang },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "GET",// trên controler dăt post,
                    success: function(res)
                    {
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
nhaphang.init();
exportNhapHang.init();
notificationService();
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