function AtivarLimite(id) {
    if (confirm("Deseja ativar este limite controle e definí-lo como padrão?")) {
        $.ajax({
            type: 'POST',
            data: "id=" + id,
            url: '/AreaRestrita/Limite/DefinirLimiteAtivo',
            success: function (message) {
                if (message) {
                    confirm("Limite de Controle definido como ativo e padrão");
                    window.location.href = '/AreaRestrita/Limite/ConsultaLimiteControle';
                }
                else {
                    messageBox("Erro: " + message);
                }
            },
            error: function (e) {
                messageBox(e.status);
            }
        });
    }
}