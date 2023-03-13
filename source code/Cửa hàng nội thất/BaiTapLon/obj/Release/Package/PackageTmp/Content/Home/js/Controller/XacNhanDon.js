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
                    url: "/Users/ChangeSuccessOrder",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,

                    success: function (response) {
                        console.log(response)
                        if (response.NhanHang == 1) {
                            //gán text box thành kích hoạt
                            btn.text('Đã nhận hàng');
                           

                        } else if (response.NhanHang == 2) {
                            btn.text('Trả hàng lại');
                            
                        }
                        else {
                           
                            btn.text('Chưa nhận hàng');
                           
                        }
                    }
                });
        });

    }
}
hoanThanh.init();