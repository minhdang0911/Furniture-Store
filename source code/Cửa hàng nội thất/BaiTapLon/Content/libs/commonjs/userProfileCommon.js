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
var ChangeUserProfile = function () {
    var data = $('#ChangeUserProfile').serialize();
    $.ajax({
        type: "POST",
        url: "/sua-thong-tin",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Cập nhật thông tin thành công.");
                setTimeout(function () {
                    window.location.reload(1)
                }, 2000);
            }

            else if (result == -1) {
                notificationService().displayError("Định dạng số điện thoại không hợp lệ");
            }
            else if (result == -2) {
                notificationService().displayError("Định dạng email không hợp lệ.");
            }
            else if (result == 0) {
                notificationService().displayError("Cập nhật thông tin không thành công.");
            }

        }
    })

}
var ChangePass = function () {
    var data = $('#changePass').serialize();
    $.ajax({
        type: "POST",
        url: "/doi-mat-khau",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đổi mật khẩu thành công.");
                setTimeout(function () {
                    window.location.reload(1)
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Mật khẩu hiện tại không chính xác");
            }
            else if (result == -4) {
                notificationService().displayError("Mật khẩu mới và xác nhận không được để trống");
            }
            else if (result == -1) {
                notificationService().displayError("Mật khẩu không được để trống");
            }
            else if (result == -2) {
                notificationService().displayError("Xác nhận mật khẩu không trùng khớp");
            }
            else if (result == 0) {
                notificationService().displayError("Lỗi không thể cập nhật mật khẩu.");
            }

        }
    })

}


var giaohanghoanthanh = {
    init: function () {
        giaohanghoanthanh.registerEvents();
    },
    registerEvents: function (e) {
        $('.success_order').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);

            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Users/ChangeSuccessOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        if (response.NhanHang == 1) {
                            notificationService().displaySuccess("Đã nhận hàng thành công.")

                            setTimeout(function () {
                                window.location.reload(1)
                            }, 2000);
                        }
                    }
                });
        });

    }
}
giaohanghoanthanh.init();