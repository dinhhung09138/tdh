google.charts.load('current', { 'packages': ['line', 'corechart'] });
//
google.charts.setOnLoadCallback(drawSummaryByYearChart);

//Report summary by year
function drawSummaryByYearChart() {

    var chartDiv = document.getElementById('divSummaryByYearChart');

    var data = new google.visualization.DataTable();
    data.addColumn('number', 'Tháng');
    data.addColumn('number', "Thu vào");
    data.addColumn('number', "Chi ra");
    data.addColumn('number', "Còn lại");

    function loadDataSummaryByYearChart(callback) {
        $.ajax({
            url: '/administrator/admmoney/summaryreportbyyear',
            type: 'POST',
            cache: false,
            async: true,
            xhrFields: {
                withCredentials: true,
            },
            dataType: 'json',
            data: ({ year: 2018 }),
            success: function (response) {
                $.each(response, function (idx, item) {
                    data.addRow([item.Month, item.Income, item.Payment, item.Total]);
                });
                console.log(data);
                callback();
            },
            error: function (xhr, status, error) {
            }
        });
    }

    var options = {
        legend: { position: 'bottom' },
        chart: {
            title: 'Báo cáo tình hình thu chi năm',
            subtitle: ''
        },
        chartArea: {
            width: '100%'
        },
        //width: 900,
        //height: 500,
        series: {
            // Gives each series an axis name that matches the Y-axis below.
            0: {
                axis: 'dvt', color: '28a745'
            },
            1: {
                color: 'dc3545'
            },
            2: {
                color: '007bff'
            }
        },
        axes: {
            // Adds labels to each axis; they don't have to match the axis names.
            y: {
                dvt: { label: '(vnd)' }
            }
        }
    };

    function drawChart() {
        var chart = new google.charts.Line(chartDiv);
        chart.draw(data, options);
    }

    loadDataSummaryByYearChart(drawChart);

    if (document.addEventListener) {
        window.addEventListener('resize', drawChart);
    }
    else if (document.attachEvent) {
        window.attachEvent('onresize', drawChart);
    }
    else {
        window.resize = drawChart;
    }
}