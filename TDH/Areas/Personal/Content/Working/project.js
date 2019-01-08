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
            url: '/personal/pnworking/project',
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
                orderable: false,
                width: '110px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return '<img src="' + data.Image + '" style="width: 126px; height: auto;" />';
                }
            },
            {
                data: 'Name',
                orderable: false,
                searchable: true,
                width: '250px'
            },
            {
                data: 'DuringTime',
                orderable: false,
                searchable: true,
                width: '250px'
            },
            {
                data: 'Description',
                orderable: false,
                searchable: true
            },
            {
                data: 'Ordering',
                orderable: false,
                searchable: true,
                width: '70px'
            },
            {
                orderable: false,
                width: '60px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/personal/pnworking/editproject/' + data.ID + '\" title="Cập nhật thông tin dự án" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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
    id = deletedId;
    $('#deleteModal').modal('show');
}

function deleteItem() {
    $.ajax({
        url: '/personal/pnworking/deleteproject',
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
