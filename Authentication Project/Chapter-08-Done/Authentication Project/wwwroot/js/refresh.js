$(document).ready(function () {

    function pad2(n) { return n.toString().padStart(2, '0'); }

    setInterval(function () {
        $.getJSON('/TicketRenewal/getdata', function (data) {

            var now = new Date();
            var time = pad2(now.getHours()) + ":" + pad2(now.getMinutes()) + ":" + pad2(now.getSeconds());

            var text = data.message ?? "No message";
            var str = '<li>' + time + ' - ' + text + '</li>';

            $(str).appendTo('#Content');
        })
            .fail(function (xhr) {
                var now = new Date();
                var time = pad2(now.getHours()) + ":" + pad2(now.getMinutes()) + ":" + pad2(now.getSeconds());
                $(('<li>' + time + ' - ' + 'Request failed (' + xhr.status + ')' + '</li>')).appendTo('#Content');
            });

    }, 1000);
});
