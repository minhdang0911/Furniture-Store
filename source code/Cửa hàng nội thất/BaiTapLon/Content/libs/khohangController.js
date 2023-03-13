var exportKhoHang = {
    init: function () {
        exportKhoHang.registerEvents();
    },
    registerEvents: function (e) {
        $('.btnExportTonKho').off('click').on('click', function (e) {
            var formDate = $(this).data('fromdate');
            var toDate = $(this).data('todate');
            e.preventDefault();
            $.ajax({
                url: "/Admin/SanPham/ExportExelTonKho",
                data: {
                    fromDate: formDate,
                    toDate: toDate
                },// truyền vào tham số của cái id đấy id chính là id truyền vào
                dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                type: "GET",// trên controler dăt post,
                success: function (res) {
                    if (res !== '') {
                        notificationService().displaySuccess("Xuất dữ liệu sang file excel thành công.")
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
exportKhoHang.init();
notificationService();