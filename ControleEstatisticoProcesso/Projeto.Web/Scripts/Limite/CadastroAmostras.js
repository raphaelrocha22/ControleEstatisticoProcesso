$(document).ready(function () {

    $('#txtLote').attr('value', "");
    $('#txtQtdReprovada').attr('value', "");

    if ($('#txtMaquina').val() != "" && $('#txtMaquina').val() != "0" && $('#txtQtdTotal').val() != "" && $('#txtQtdTotal').val() != "0") {
        desabilitar();
    }
    else {
        habilitar();
    }

    $('#formCadastro').submit(function (e) {
        if (parseInt($('#txtQtdReprovada').val()) > parseInt($('#txtQtdTotal').val())) {
            messageBox_OK("A quantidade de itens reprovados não pode ser maior que a quantidade total de itens");
            e.preventDefault();
        }
        else {
            habilitar();
        }
    });

    $('#txtQtdReprovada').keyup(function () {
        if ($(this).val() != "") {
            CalcularPercent();
        }
    });

    $('#txtQtdTotal').keyup(function () {
        if ($(this).val() != "") {
            CalcularPercent();
        };
    });
});

function desabilitar() {
    $('#txtMaquina').prop('disabled', true);
    $('#txtQtdTotal').prop('disabled', true);
}

function habilitar() {
    $('#txtMaquina').prop('disabled', false);
    $('#txtQtdTotal').prop('disabled', false);
}

function CalcularPercent() {
    var qtdReprovado = $('#txtQtdReprovada').val();
    var qtdTotal = $('#txtQtdTotal').val();

    $('#txtPercentReprovacao').val(((qtdReprovado / qtdTotal) * 100).toFixed(2) + "%");
}