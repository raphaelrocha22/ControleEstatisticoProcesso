$(document).ready(function () {

    $("#btnConsultar").prop('value', 'Consultar').prop('disabled', false);

    $("#formConsulta").submit(function () {
        $('#btnConsultar').prop('value', 'Aguarde...').prop('disabled', true);
    });

    DataModelo();
    $("input[name='TipoPesquisa']").click(function () {
        DataModelo();
    });

});

function DataModelo() {

    $('input[name=TipoPesquisa]:checked').val() == "Diario" ?
        $(".data").prop("type", "date") :
        $(".data").prop("type", "month");
}