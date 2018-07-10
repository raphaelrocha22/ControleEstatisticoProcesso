$(document).ready(function () {

    $('#btnCadastrarLimite').click(function () {

        if (confirm("Deseja cadastrar um novo Limite de Controle?")) {

            $.ajax({
                type: 'POST',
                url: '/AreaRestrita/Limite/CadastroLimiteControle',
                success: function (message) {
                    if (message) {
                        confirm("Limite de Controle cadastrado e definido como ativo");
                        window.location.href = '/AreaRestrita/CEP/Index';
                    }
                    else {
                        messageBox("Erro: " + message);
                    }
                },
                error: function (e) {
                    messageBox(e.status);
                }
            });
        };
    });
});
