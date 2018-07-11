var table;
var id = 0;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();
    //
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        responsive: true,
        pageLength: 10,
        dom: dom,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[4, "desc"]],
        ajax: {
            url: '/administrator/admworking/report',
            type: 'post',
            data: function (d) {
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
                data: 'Title',
                orderable: true,
                searchable: true,
                render: function (obj, type, data, meta) {
                    return '<a href="javascript:;" data-url="/administrator/admworking/detailreport/' + data.ID + '\" data-title="Cập nhật báo cáo" class="pg_ld">' + data.Title + '</a>';
                }
            },
            {
                data: 'Count',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '90px'
            },
            {
                data: 'UserCreate',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '200px'
            },
            {
                data: 'CreateDateString',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '200px'
            },
            {
                orderable: false,
                width: '60px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="javascript:;" data-url="/administrator/admworking/editreport/' + data.ID + '\" data-title="Cập nhật báo cáo" class="mg-lr-2 pg_ld" title="Cập nhật"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');" class="mg-lr-2"><i class="fa fa-remove" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });


});

function confirmDelete(deletedId) {
    $.ajax({
        url: '/administrator/admworking/checkdeletereport',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: deletedId }),
        success: function (response) {
            if (response === 3) {
                id = deletedId;
                $('#deleteModal').modal('show');
            } else {
                id = '';
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}

function deleteItem() {
    $.ajax({
        url: '/administrator/admworking/deletereport',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: id }),
        success: function (response) {
            if (response === 0) {
                table.ajax.reload();
            }
            id = '';
            $('#deleteModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}
