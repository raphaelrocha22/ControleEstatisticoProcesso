$(document).ready(function () {

    if ($('#txtLote').val() == "0")
        $('#txtLote').attr('value', "");

    if ($('#txtQtdReprovada').val() == "0")
        $('#txtQtdReprovada').attr('value', "");

    if ($('#txtQtdReprovada').val() != '' && $('#txtQtdTotal').val() != '')
        VerificarStatus(CalcularPercent() / 100);

    $('#txtQtdReprovada').keyup(function () {
        if ($(this).val() != "") {
            VerificarStatus(CalcularPercent() / 100);
        }
    });

    $('#txtQtdTotal').keyup(function () {
        if ($(this).val() != "") {
            VerificarStatus(CalcularPercent() / 100);
        };
    });

    $('#formCadastro').submit(function (e) {
        if (parseInt($('#txtQtdReprovada').val()) > parseInt($('#txtQtdTotal').val())) {
            messageBox_OK("A quantidade de itens reprovados não pode ser maior que a quantidade total de itens");
            e.preventDefault();
            return false;
        }

        if ($('#btnReprovarLote').data('reprovar')) {
            if (confirm("Deseja realmente reprovar este lote ?")) {
                $('#txtUsuarioAprovacaoLogin').val('');
                $('#txtUsuarioAprovacaoSenha').val('');
            }
            else {
                e.preventDefault();
            }
        }
        else if (!VerificarStatus(CalcularPercent() / 100) && ($('#txtUsuarioAprovacaoLogin').val() == '' || $('#txtUsuarioAprovacaoSenha').val() == '')) {
            e.preventDefault();
            $('#modalAprovacao').modal('show')
        }
    });

});

function CalcularPercent() {
    var qtdReprovado = $('#txtQtdReprovada').val();
    var qtdTotal = $('#txtQtdTotal').val();

    var p = ((qtdReprovado / qtdTotal) * 100).toFixed(1);
    $('#txtPercentReprovacao').val(p + "%");

    return p
}

function VerificarStatus(p) {
    var lsc = parseFloat($('#lblLSC').val().replace(",", "."));
    var lic = parseFloat($('#lblLIC').val().replace(",", "."));

    if (p >= lic && p <= lsc) {
        $("#txtStatus").val('Aprovado');
        return true;
    }
    else {
        $("#txtStatus").val("Reprovado");
        return false;
    }
}