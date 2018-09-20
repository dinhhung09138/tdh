
google.charts.load('current', { 'packages': ['corechart'] })
google.charts.load('current', { 'packages': ['line', 'corechart'] });

google.charts.setOnLoadCallback(drawSummaryChart);
google.charts.setOnLoadCallback(drawSummaryByYearChart);
google.charts.setOnLoadCallback(drawIncomeSummaryByYearChart);

/**
 * Summary dashboard
 *
 * Report summary by year
 */
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
            url: '/money/mnreport/summaryreport',
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
        //title: 'Báo cáo tình hình thu chi các năm',
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

/**
 * Summary dashboard
 *
 * Report summary by month in a year
 */
function drawSummaryByYearChart() {

    var chartDiv = document.getElementById('divSummaryByYear');

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Tháng');
    data.addColumn('number', "Thu vào");
    data.addColumn('number', "Chi ra");
    data.addColumn('number', "Còn lại");

    function loadDataSummaryByYearChart(callback) {
        $.ajax({
            url: '/money/mnreport/summaryreportbyyear',
            type: 'POST',
            cache: false,
            async: true,
            xhrFields: {
                withCredentials: true,
            },
            dataType: 'json',
            data: ({
                year: $('#summaryByYearSelector').val()
            }),
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
        title: 'Báo cáo tình hình thu chi',
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

/**
 * Income dashboard
 *
 * Report income summary by month in a year
 */
function drawIncomeSummaryByYearChart() {

    var chartDiv = document.getElementById('divIncomeSummaryByCategory');

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Tháng');

    function loadDataIncomeSummaryByYearChart(callback) {
        $.ajax({
            url: '/money/mnreport/incomebyyearreport',
            type: 'POST',
            cache: false,
            async: true,
            xhrFields: {
                withCredentials: true,
            },
            dataType: 'json',
            data: ({ year: $('#summaryByYearSelector').val() }),
            success: function (response) {
                var t1 = [];
                var t2 = [];
                var t3 = [];
                var t4 = [];
                var t5 = [];
                var t6 = [];
                var t7 = [];
                var t8 = [];
                var t9 = [];
                var t10 = [];
                var t11 = [];
                var t12 = [];
                $.each(response, function (idx, item) {
                    data.addColumn('number', item.Name);
                });
                $.each(response, function (idx, item) {
                    t1.push(item.T01);
                    t2.push(item.T02);
                    t3.push(item.T03);
                    t4.push(item.T04);
                    t5.push(item.T05);
                    t6.push(item.T06);
                    t7.push(item.T07);
                    t8.push(item.T08);
                    t9.push(item.T09);
                    t10.push(item.T10);
                    t11.push(item.T11);
                    t12.push(item.T12);
                });
                var arrayLength = t1.length;
                var row = [];
                row.push('01');
                for (var i = 0; i < arrayLength; i++) {
                    row.push(t1[i]);
                }
                data.addRow(row);
                row = [];
                row.push('02');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t2[i]);
                }
                data.addRow(row);
                row = [];
                row.push('03');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t3[i]);
                }
                data.addRow(row);
                row = [];
                row.push('04');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t4[i]);
                }
                data.addRow(row);
                row = [];
                row.push('05');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t5[i]);
                }
                data.addRow(row);
                row = [];
                row.push('06');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t6[i]);
                }
                data.addRow(row);
                row = [];
                row.push('07');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t7[i]);
                }
                data.addRow(row);
                row = [];
                row.push('08');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t8[i]);
                }
                data.addRow(row);
                row = [];
                row.push('09');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t9[i]);
                }
                data.addRow(row);
                row = [];
                row.push('10');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t10[i]);
                }
                data.addRow(row);
                row = [];
                row.push('11');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t11[i]);
                }
                data.addRow(row);
                row = [];
                row.push('12');
                for (i = 0; i < arrayLength; i++) {
                    row.push(t12[i]);
                }
                data.addRow(row);
                //data = google.visualization.arrayToDataTable(array);
                callback();
            },
            error: function (xhr, status, error) {
            }
        });
    }

    var options = {
        legend: { position: 'bottom' },
        title: 'Các nguồn thu nhập',
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
        var chart = new google.visualization.ComboChart(chartDiv);
        chart.draw(data, options);
    }

    loadDataIncomeSummaryByYearChart(drawChart);

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


