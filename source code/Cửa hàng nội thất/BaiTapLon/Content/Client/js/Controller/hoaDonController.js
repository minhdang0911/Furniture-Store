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
                            btn.text('Chờ xác nhận');
                        }
                        else {

                            btn.text('Đã xác nhận');
                        }
                    }
                });
        });

    }
}

var giaohang = {
    init: function () {
        giaohang.registerEvents();
    },
    registerEvents: function (e) {
        $('.btn-active-giao').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);

            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/HoaDon/ChangeGiaoHangOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.GiaoHang == 1) {
                            //gán text box thành kích hoạt
                            btn.text('Đang giao hàng');
                        }
                        else {

                            btn.text('Chờ giao hàng');
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

                    success: function (response) {// trả về chuỗi JSON các thông tin
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

var hoanThanhGiao = {
    init: function () {
        hoanThanhGiao.registerEvents();
    },
    registerEvents: function (e) {
        var div = document.getElementById("btn-active-change");
        div.innerHTML = 'Đã hoàn tất';
    }
}
var thatBaiGiao = {
    init: function () {
        thatBaiGiao.registerEvents();
    },
    registerEvents: function (e) {
        var div = document.getElementById("btn-active-change");
        div.innerHTML = 'Đang giao hàng';
    }
}

hoadon.init();
giaohang.init();
hoanThanh.init();