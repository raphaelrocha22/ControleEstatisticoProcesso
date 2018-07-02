function messageBox(mensagem) {

    $('#message').html(mensagem);
    $("#messageBox").dialog();
}

function messageBox_OK(mensagem) {

    $('#message').html(mensagem);
    $("#messageBox").dialog({
        dialogClass: "no-close",
        buttons: [
            {
                text: "OK",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ]
    });
}

function messageBox_SimNao(mensagem) {

    $('#message').html(mensagem);
    $("#messageBox").dialog({
        dialogClass: "no-close",
        buttons: [
            {
                text: "NÃO",
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            },
            {
                text: "SIM",
                click: function () {
                    $(this).dialog("close");
                    return true;
                }
            }
        ]
    });
}