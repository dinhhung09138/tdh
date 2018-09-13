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
        pageLength: 10,
        dom: dom,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        order: [[4, "desc"]],
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        ajax: {
            url: '/personal/pncetificate/index',
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
                data: 'Name',
                orderable: true,
                searchable: true
            },
            {
                data: 'School',
                orderable: true,
                searchable: true,
                width: '200px'
            },
            {
                data: 'Time',
                orderable: true,
                searchable: true,
                width: '200px'
            },
            {
                data: 'Ordering',
                orderable: true,
                searchable: true,
                width: '100px',
                className: 'ctn-center'
            },
            {
                orderable: false,
                width: '60px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/personal/pncetificate/edit/' + data.ID + '\" title="Cập nhật chứng chỉ đào tạo" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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
        url: '/personal/pncetificate/delete',
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
