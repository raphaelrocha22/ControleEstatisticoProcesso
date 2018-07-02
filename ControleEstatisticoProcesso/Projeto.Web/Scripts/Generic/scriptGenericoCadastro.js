$(document).ready(function () {

    $("#btnCadastrar").prop('value', 'Cadastrar').prop('disabled', false);

    $("#formCadastro").submit(function () {
        $('#btnCadastrar').prop('value', 'Aguarde...').prop('disabled', true);
    });

});