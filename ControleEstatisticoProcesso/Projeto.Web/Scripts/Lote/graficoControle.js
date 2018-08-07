$(document).ready(function () {

    google.charts.load('current', { 'packages': ['line'] });
    google.charts.setOnLoadCallback(drawChart);

});

function drawChart(lista) {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Lote');
    data.addColumn('number', 'p');
    data.addColumn('number', 'LSC');
    data.addColumn('number', 'LC');
    data.addColumn('number', 'LIC');

    $.each(lista.Lotes, function (key, value) {

        var lote = value.IdLote.toString();
        var p = value.PercentualReprovado;
        var LSC = value.LimiteControle.LSC;
        var LC = value.LimiteControle.LC;
        var LIC = value.LimiteControle.LIC > 0 ? lista.Lotes[i].LimiteControle.LIC : 0;

        data.addRow([lote, p, LSC, LC, LIC]);

    });

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
            format: '#\'%\''
        }
    };

    var chart = new google.charts.Line(document.getElementById('linechart_material'));
    chart.draw(data, google.charts.Line.convertOptions(options));

};
