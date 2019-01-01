var table;
var id = '';

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    //
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        responsive: true,
        paging: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        dom: dom,
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
        },
        language: language,
        order: [[1, "asc"]],
        ajax: {
            url: '/website/wsetting/configuration',
            type: 'post',
            data: function (d) {
                //d.ModuleCode = ""
            }
        },
        columns: [
            {
                orderable: false,
                width: '30px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: 'Key',
                orderable: true,
                searchable: true,
                width: '100px'
            },
            {
                data: 'Description',
                orderable: true,
                searchable: true
            },
            {
                data: 'Value',
                orderable: true,
                searchable: true
            },
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/website/wsetting/editconfiguration/' + data.Key + '\" title="Cập nhật thông tin thiết lập" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });

});
