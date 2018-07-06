$(document).ready(function () {

    google.charts.load('current', { 'packages': ['line'] });
    google.charts.setOnLoadCallback(drawChart);


    $('#btnCalcularLimite').click(function () {

        $.ajax({
            type: 'POST',
            url: '/AreaRestrita/Limite/CalculoLimiteControle',
            success: function (lista) {
                drawChart(lista);
            },
            error: function (e) {
                alert(e.status);
            },
        });
    });
});

function drawChart(lista) {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Lote');
    data.addColumn('number', 'p');
    data.addColumn('number', 'LSC');
    data.addColumn('number', 'LC');
    data.addColumn('number', 'LIC');

    for (var i = 0; i < lista.ConsultaLoteAmostra.length; i++) {

        var lote = lista.ConsultaLoteAmostra[i].IdLote.toString();
        var p = lista.ConsultaLoteAmostra[i].PercentualReprovado;
        var LSC = lista.LimiteControle.LSC * 100;
        var LC = lista.LimiteControle.LC * 100;
        var LIC = lista.LimiteControle.LIC * 100 > 0 ? lista.LimiteControle.LIC * 100 : 0;

        data.addRow([lote, p, LSC, LC, LIC]);
    }

    $('#lblLimites').html("LIC: " + LIC.toFixed(2) + "% / LC: " + LC.toFixed(2) + "% / LSC: " + LSC.toFixed(2) + "%");

    var options = {
        fontSize: 12,
        legend: { position: 'top' },
        displayAnnotations: true,
        annotations: {
            textStyle: {
                fontSize: 12
            }
        },
        vAxis: {
            format: '#\'%\'' }
    };

    var chart = new google.charts.Line(document.getElementById('linechart_material'));
    chart.draw(data, google.charts.Line.convertOptions(options));
}