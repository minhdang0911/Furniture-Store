var commom = {
    init: function () {
        commom.registerEvent()
    },
    registerEvent: function () {
        $("#txtKeyWord").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);

                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyWord").val(ui.item.Name);

                return false;
            },
            select: function (event, ui) {
                $("#txtKeyWord").val(ui.item.Name);
                //$("#txtKeyWord").val(ui.item.Images);

                return false;
            }
        })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li class='list-group-item list-group-item-action'>")
          .append("<div class='"+name+"'>" + item.Name+ "</br>" +"<img src="+item.Images+" width="+50+" height="+50+" "+ "</div>")
          .appendTo(ul);
    };

    }
}
commom.init();
