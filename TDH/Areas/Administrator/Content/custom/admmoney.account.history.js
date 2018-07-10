﻿var table;
var id = 0;

$(document).ready(function () {

    $("#monthSelect").datepicker({
        language: 'vi',
        format: "yyyy/mm",
        viewMode: "months",
        minViewMode: "months"
    }).on('changeDate', function (ev) {
        $('#monthSelect').datepicker('hide');
        var month = $(this).val().substring(5, 8);
        var year = $(this).val().substring(0, 4);
        $('#yearMonth').val(parseInt(year + month));
        table.ajax.reload();
    });

    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: false,
        ordering: false,
        paging: true,
        responsive: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        dom: '<"top"<"row"<"col-md-3 col-sm-4 col-xs-12"l><"col-md-6 col-sm-4 col-xs-12"<"toolbar">><"col-md-3 col-sm-4 col-xs-12 text-right"f>>>rt<"bottom"ip><"clear">',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[1, "asc"]],
        ajax: {
            url: '/administrator/admmoney/accounthistory',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#yearMonth').val(), //By month
                d.Parameter2 = $('#id').val(), //By account id
                d.Parameter3 = $('#ddlSelect').val() //by type (income or payment)
            }
        },
        columns: [
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                className: 'ctn-center',
                width: '50px',
                render: function (obj, type, data, meta) {
                    if (data.Type === 1) {
                        return '<i class="fa fa-long-arrow-left" style="color: green;" aria-hidden="true"></i>';
                    }
                    return '<i class="fa fa-long-arrow-right" style="color: red;" aria-hidden="true"></i>';
                }
            },
            {
                data: 'DateString',
                className: 'ctn-center',
                width: '200px'
            },
            {
                data: 'MoneyString',
                className: 'text-right',
                width: '200px'
            },
            {
                data: 'Title'
            }
        ]
    });
    $(".dataTables_wrapper .toolbar").append(toolbarSearch);


});

$(document).on('change', '#ddlSelect', function (e) {
    table.ajax.reload();
});
