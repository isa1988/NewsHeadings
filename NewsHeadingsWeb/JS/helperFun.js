
$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $(".linkToPage").on("click", function (e) {
        e.preventDefault();
        var $url = $(this).attr("data-dialog-url");
        $("<div id='dialogContent'></div>")
            .addClass("dialog")
            .appendTo("body")
            .load(this.href)
            .dialog({
                maxWidth: 550,
                minWidth: 50,
                width: 550,
                title: $(this).attr("data-dialog-title"),
                close: function () { $(this).remove() },
                modal: true,
                buttons: [
                    {
                        text: "Ok",
                        click: function() {
                                $.ajax({
                                    url: $url,

                                    type: "POST",
                                    data: $('form').serialize(),
                                    datatype: "json",
                                    success: function(result) {

                                        $("#dialogContent").html(result);
                                    }
                                });
                        },
                        class: "btn, btn-success",
                        width: "100"
                    },
                    {
                        text: "Отмена",
                        click: function () {
                            //def.reject();
                            $(this).dialog("close");
                        },
                        class: "btn, btn-success",
                        width: "100"
                    }
                ]
            });
    });
});