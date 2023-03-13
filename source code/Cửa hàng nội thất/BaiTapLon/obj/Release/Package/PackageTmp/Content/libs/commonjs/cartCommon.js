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
function addcartHome(productID) {
    $.ajax({
        url: '/Cart/Additem?productID=' + productID + '&quantity=1',
        type: 'GET',
        success: function (data) {
            notificationService().displaySuccess(" Đã thêm vào  GIỎ HÀNG(" + data.cartCount + ")");
            setTimeout(function () {
                window.location.reload(1);
            }, 2000);
        }

    });

}
function thanhtoan() {
    $.ajax({
        url: '/thanh-toan-truc-tuyen',
        type: 'GET',
        success: function (data) {
            notificationService().displayError("Bạn cần đăng nhập để thanh toán.");

        }
    })
}