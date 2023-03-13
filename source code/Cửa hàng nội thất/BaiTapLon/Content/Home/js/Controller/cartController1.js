var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function()
    {
        $('.btnTiepTuc').off('click').on('click', function () {
            window.location.href = "/"
        });

       
        $('.btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cardList = [];
            $.each(listProduct, function (i, item) {
                cardList.push({
                    Quantity: $(item).val(),
                    Product: {
                        IDContent: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cardList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if(res.status==true)
                    {
                        notificationService().displaySuccess("Cập nhật sản phẩm thành công.");
                        setTimeout(function () {
                            window.location.reload(1);
                        }, 2000);
                        //window.location.href="/gio-hang"
                    }
                }
            });
        });

        $('.btnDeleteAll').off('click').on('click', function () {
     
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        notificationService().displayError("Đã xóa toàn bộ giỏ hàng.");
                        setTimeout(function () {
                            window.location.reload(1);
                        }, 2000);
                        //window.location.href = "/gio-hang"
                    }
                }
            });
        });
        //dùng dấu chấm nếu là class
        //do a href có dấu thăng nó k nhận nên phải truyền e, e.eventDefaul
        $('.remove_Item').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: {id: $(this).data('id')},
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        notificationService().displayError("Đã xóa sản phẩm.");
                        setTimeout(function () {
                            window.location.reload(1);
                        }, 2000);
                       
                    }
                }
            });
        });


        $('#btnPaymentOff').off('click').on('click', function () {
            window.location.href = "/thanh-toan-nhan-hang"
        });

        $('#btnPaymentTrucTiep').off('click').on('click', function () {
            window.location.href = "/thanh-toan-nhan-hang"
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
cart.init();