var user = {
    init: function(){
        user.registerEvents();
    },
    registerEvents : function(e){
        $('.btn-active').off('click').on('click', function (e)// off tất cả rồi on khi ấn click 1 hoạt động nó chỉ on cái đó thôi
        {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id'); // lấy được id vì nút btn mình nhấn vào , lấy ra thuộc tính data và đăng sau là id data-id
            $.ajax(
                {
                    url: "/Admin/User/ChangeStatus",
                    data: { id: id },// truyền vào tham số của cái id đấy id chính là id truyền vào
                    dataType: "json",// có cái datatype truyền lên rồi ko cần contentType nữa
                    type: "POST",// trên controler dăt post,
   
                    success: function (response)
                    {
                        console.log(response)
                        if (response.status == true)
                        {
                            //gán text box thành kích hoạt
                            btn.text('Hoạt động');
                        }
                        else
                        {
                            
                            btn.text('Đã khóa');
                        }
                    }
                });
        });

    }
}
user.init();