var hubHelper = {};
(function () {

    var unread = 0;

    $.connection.hub.url = "http://localhost:10430/Msg/hubs";

    var updateLabel = function (label) {
        $("#msgMenu #msgLabel").text(label).parent().css("color", "orangeRed");
    }

    var updateBrdge = function (count) {
        $("#msgMenu #unRead").text(Lang.UNREAD_MESSAGE.format(count));
    }

    var showMsg = function (msg) {
        $("<li class='new'><a href='#'>" + msg.Subject + "</li>").insertBefore("#msgMenu ul li:first");
        $("#msgMenu .divider").removeClass("hidden");
    }

    var imHub = $.connection.IMHub;
    var msgHub = $.connection.MsgHub;

    imHub.client.NewMsg = function (msg) {
        updateLabel("New Msg");
        updateBrdge(++unread);
        showMsg(msg);

        var news = $("#msgMenu .new");
        if (news.length > 5) {
            var m;
            for (var i = 5; m = news[i]; i++) {
                $(m).remove();
            }
        }
    };

    $.connection.hub.start()
    .done(function () {
        //获取未读条数
        msgHub.server.unReadCount().done(function (c) {
            updateBrdge(c);
            if (c > 0) {
                updateLabel(Lang.UNREAD_MESSAGE.format(c));
            }
            unread = c;
        });
        
        //加载最新5条未读
        msgHub.server.listMsgs({ page: 0, pageSize: 5 }, true).done(function (msgs) {
            //showMsg 是插入到第1条前面,所以这里应该是反转一下.
            msgs.reverse();
            var msg;
            for (var i = 0; msg = msgs[i]; i++) {
                showMsg(msg);
            }
        });
    });

})();