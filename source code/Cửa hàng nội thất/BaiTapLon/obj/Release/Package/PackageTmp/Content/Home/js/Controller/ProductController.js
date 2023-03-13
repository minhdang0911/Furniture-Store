
var product_selected_list = {
    init: function () {
        product_selected_list.registerEvents();
    },
    registerEvents: function (e) {
        var selectedValue = $("select[name^='product_sort']")[0].getElementsByTagName("option")[0].getAttribute("value");
            $.ajax({
                data: {
                    
                    sort: selectedValue
                },
                url: '/Product/ProductCategory',
                success: function (returndata) {
                    //something to do
                }
            });
        

    }

}
product_selected_list.init();