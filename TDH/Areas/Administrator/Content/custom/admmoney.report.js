
google.charts.load('current', { 'packages': ['corechart'] })
google.charts.load('current', { 'packages': ['line', 'corechart'] });

google.charts.setOnLoadCallback(drawSummaryChart);
google.charts.setOnLoadCallback(drawSummaryByYearChart);

//Report summary by year
function drawSummaryChart() {

    var chartDiv = document.getElementById('divSummary');

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Năm');
    data.addColumn('number', "Thu nhập");
    data.addColumn('number', "Tiêu dùng");
    data.addColumn('number', "Còn lại");
    //data.addColumn({ 'type': 'string', 'role': 'tooltip', 'p': { 'html': 'true' }});

    function loadDataSummaryChart(callback) {
        $.ajax({
            url: '/administrator/admmoney/summaryreport',
            type: 'POST',
            cache: false,
            async: true,
            xhrFields: {
                withCredentials: true,
            },
            dataType: 'json',
            success: function (response) {
                $.each(response, function (idx, item) {
                    data.addRow([item.Year.toString(), item.Income, item.Payment, item.Total]);
                });
                callback();
            },
            error: function (xhr, status, error) {
            }
        });
    }
    
    var options = {
        legend: { position: 'bottom' },
        title: 'Báo cáo tình hình thu chi các năm',
        titlePosition: 'out',
        hAxis: {
            title: 'Năm'
        },
        vAxis: {
            title: 'Tổng số tiền (vnd)',
            format: 'short',
            gridlines: { count: 8 },
            scareType: 'mirrorLog'
        },
        series: {
            0: {
                color: '28a745'
            },
            1: {
                color: 'dc3545'
            },
            2: {
                color: '007bff'
            }
        },
        //Makes the entire category's tooltip active
        focusTarget: 'category',
        //Use a html tooltip
        tooltip: { isHtml: true },
        asex: {

        },
        pointSize: 5,
        backgroundColor: 'none'
    };

    function drawChart() {
        var chart = new google.visualization.LineChart(chartDiv);
        chart.draw(data, options);
    }

    loadDataSummaryChart(drawChart);

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

//Report summary in a year
function drawSummaryByYearChart() {

    var chartDiv = document.getElementById('divSummaryByYear');

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Tháng');
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
                    data.addRow([item.Month.toString(), item.Income, item.Payment, item.Total]);
                });
                callback();
            },
            error: function (xhr, status, error) {
            }
        });
    }

    var options = {
        legend: { position: 'bottom' },
        title: 'Báo cáo tình hình thu chi các năm',
        titlePosition: 'out',
        hAxis: {
            title: 'Tháng'
        },
        vAxis: {
            title: 'Tổng số tiền (vnd)',
            format: 'short',
            gridlines: { count: 8 },
            scareType: 'mirrorLog'
        },
        series: {
            0: {
                color: '28a745'
            },
            1: {
                color: 'dc3545'
            },
            2: {
                color: '007bff'
            }
        },
        //Makes the entire category's tooltip active
        focusTarget: 'category',
        //Use a html tooltip
        tooltip: { isHtml: true },
        asex: {

        },
        pointSize: 5,
        backgroundColor: 'none'
    };

    function drawChart() {
        var chart = new google.visualization.LineChart(chartDiv);
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