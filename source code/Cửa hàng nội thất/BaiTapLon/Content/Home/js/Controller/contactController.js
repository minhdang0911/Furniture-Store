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
var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#userName').val();
            var phone = $('#userPhone').val();
            var email = $('#userEmail').val();
            var address = $('#userAddress').val();
            var address = $('#userAddress').val();
            var tieude = $('#userTieuDe').val();
            var msg = $('#userMsg').val();
            

            $.ajax({
                url: '/Contact/Send',
                type: 'POST',
                dataType: 'JSON',
                data: {
                    name: name,
                    phone: phone,
                    email: email, 
                    address: address,
                    tieude: tieude,
                    msg: msg

                },
                success: function (res) {
                    if (res.status == true)
                    {
                        notificationService().displaySuccess("Chúng tôi đã nhận được câu hỏi của bạn.")
                        contac.retestForm();
                        window.location.href = "/Lien-He"
                       
                        
                    }
                }
            });
        });
    }, retestForm: function () {
        //trả hết nó về trang thái trống
        $('#userName').val(' ');
        $('#userPhone').val(' ');
        $('#userEmail').val(' ');
        $('#userAddress').val(' ');
        $('#userTieuDe').val(' ');
        $('#userMsg').val(' ');
    }
}
contact.init();