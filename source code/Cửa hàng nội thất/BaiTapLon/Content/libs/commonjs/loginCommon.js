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
var login = function () {
    var data = $('#loginForm').serialize();
    $.ajax({
        type: "POST",
        url: "/dang-nhap",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đăng nhập thành công.");
                setTimeout(function () {
                    window.location.href = "/Home/TrangChu"
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Tài khoản này không có quyền truy cập");
            }
            else if (result == -1) {
                notificationService().displayError("Tài khoản đã bị khóa");
            }
            else if (result == -2) {
                notificationService().displayError("Mật khẩu sai.");
            }
            else if (result == 0) {
                notificationService().displayError("Tài khoản không tồn tại.");
            }
            else {
                notificationService().displayError("Đăng nhập không thành công");
            }
        }
    })

}
var loginMobile = function () {
    var data = $('#loginFormMobile').serialize();
    $.ajax({
        type: "POST",
        url: "/dang-nhap",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đăng nhập thành công.");
                setTimeout(function () {
                    window.location.href = "/Home/TrangChu"
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Tài khoản này không có quyền truy cập.");
            }
            else if (result == -1) {
                notificationService().displayError("Tài khoản đã bị khóa.");
            }
            else if (result == -2) {
                notificationService().displayError("Mật khẩu sai.");
            }
            else if (result == 0) {
                notificationService().displayError("Tài khoản không tồn tại.");
            }
            else {
                notificationService().displayError("Đăng nhập không thành công.");
            }
        }
    })

}
var Sigup = function () {
    var data = $('#Sigup').serialize();
    $.ajax({
        type: "POST",
        url: "/Users/RegisterUser",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đăng ký thành công.");
                setTimeout(function () {
                    window.location.href = "/Home/TrangChu"
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Số điện thoại không hợp lệ.");
            }
            else if (result == -1) {
                notificationService().displayError("Email này đã có người đăng ký.");
            }
            else if (result == -2) {
                notificationService().displayError("Người dùng này đã tồn tại.");
            }
            else if (result == -4) {
                notificationService().displayError("Định dạng email sai.");
            }
            else if (result == -5) {
                notificationService().displayError("Mật khẩu không trùng khớp.");
            }
            else if (result == 0) {
                notificationService().displayError("Đăng ký thất bại.");
            }

        }
    })

}
var SigupMobile = function () {
    var data = $('#Sigup1').serialize();
    $.ajax({
        type: "POST",
        url: "/Users/RegisterUser",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đăng ký thành công.");
                setTimeout(function () {
                    window.location.href = "/Home/TrangChu"
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Số điện thoại không hợp lệ.");
            }
            else if (result == -1) {
                notificationService().displayError("Email này đã có người đăng ký.");
            }
            else if (result == -2) {
                notificationService().displayError("Người dùng này đã tồn tại.");
            }
            else if (result == -4) {
                notificationService().displayError("Định dạng email sai.");
            }
            else if (result == -5) {
                notificationService().displayError("Mật khẩu không trùng khớp.");
            }
            else if (result == 0) {
                notificationService().displayError("Đăng ký thất bại.");
            }

        }
    })

}
var retestPass = function () {
    var data = $('#retesPass').serialize();
    $.ajax({
        type: "POST",
        url: "/Users/RetestPassWord",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đã gửi mật khẩu mới đến Email.");
                setTimeout(function () {
                    window.location.reload(1)
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Email nhập không đúng định đạng.");
            }
            else if (result == -1) {
                notificationService().displayError("Không cấp được mật khẩu.");
            }
            else if (result == -2) {
                notificationService().displayError("Không tìm thấy tài khoản nào.");
            }

            else if (result == 0) {
                notificationService().displayError("Yêu cầu nhập đầy đủ các trường.");
            }

        }
    })

}
var retestPassMobile = function () {
    var data = $('#retesPass1').serialize();
    $.ajax({
        type: "POST",
        url: "/Users/RetestPassWord",
        data: data,
        success: function (result) {
            if (result == 1) {
                notificationService().displaySuccess("Đã gửi mật khẩu mới đến Email.");
                setTimeout(function () {
                    window.location.reload(1)
                }, 2000);
            }
            else if (result == -3) {
                notificationService().displayError("Email nhập không đúng định đạng.");
            }
            else if (result == -1) {
                notificationService().displayError("Không cấp được mật khẩu.");
            }
            else if (result == -2) {
                notificationService().displayError("Không tìm thấy tài khoản nào.");
            }

            else if (result == 0) {
                notificationService().displayError("Yêu cầu nhập đầy đủ các trường.");
            }

        }
    })

}