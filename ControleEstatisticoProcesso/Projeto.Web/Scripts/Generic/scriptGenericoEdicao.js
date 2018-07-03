$(document).ready(function () {

    $("#btnAtualizar").prop('value', 'Atualizar').prop('disabled', false);

    $("#formEdicao").submit(function () {
        $('#btnAtualizar').prop('value', 'Aguarde...').prop('disabled', true);
    });

});