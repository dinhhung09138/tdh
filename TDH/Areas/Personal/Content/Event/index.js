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
        ordering: false,
        paging: true,
        pageLength: 10,
        dom: dom,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        ajax: {
            url: '/personal/pnevent/index',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#type').val()
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
                data: 'TypeName',
                orderable: false,
                searchable: true,
                width: '200px'
            },
            {
                data: 'Title',
                orderable: false,
                searchable: true
            },
            {
                data: 'Duration',
                orderable: false,
                searchable: true,
                width: '150px'
            },
            {
                orderable: true,
                searchable: true,
                width: '100px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    if (data.IsCancel === true) {
                        return '<label class="btn-dange">Đã hủy</label>';
                    }
                    if (data.IsFinish === true) {
                        return '<label class="btn-primary">Hoàn thành</label>';
                    }
                    return '<label class="btn-warning">Dự kiến</label>';
                }
            },
            {
                orderable: false,
                width: '60px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/personal/pnevent/edit/' + data.ID + '\" title="Cập nhật thông tin sự kiện" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');" class="mg-lr-2"><i class="fa fa-remove" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });

    $(".dataTables_wrapper .toolbar").append(type);

});

function confirmDelete(deletedId) {
    id = deletedId;
    $('#deleteModal').modal('show');
}

function deleteItem() {
    $.ajax({
        url: '/personal/pnevent/delete',
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

$(document).on('change', '#type', function (e) {
    table.ajax.reload();
});