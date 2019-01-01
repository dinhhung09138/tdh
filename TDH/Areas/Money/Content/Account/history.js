var table;
var id = 0;

$(document).ready(function () {
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        responsive: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        dom: dom,
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[2, "asc"]],
        ajax: {
            url: '/money/mnaccount/history',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#id').val(); //By account id
                d.Parameter2 = $('#ddlSelect').val(); //by type (income or payment)
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
                orderable: false,
                searchable: false,
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
                data: 'Title',
                orderable: false,
                searchable: false
            }
        ]
    });
    $(".dataTables_wrapper .toolbar").append(toolbarSearch);


});

$(document).on('change', '#ddlSelect', function (e) {
    table.ajax.reload();
});
