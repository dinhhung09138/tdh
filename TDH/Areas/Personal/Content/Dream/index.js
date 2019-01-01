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
        initComplete: function (settings, json) {
            //Do something after finish
        },
        rowCallback: function (row, data) {
            console.log(data);
            if (data.Finish === true) {
                $(row).css('background-color', '#ffc107');
            }
        },
        language: language,
        order: [[3, "desc"]],
        ajax: {
            url: '/personal/pndream/index',
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
                searchable: true
            },
            {
                data: 'Ordering',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '80px'
            },
            {
                data: 'FinishTimeString',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '130px'
            },
            {
                orderable: false,
                width: '60px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/personal/pndream/edit/' + data.ID + '\" title="Cập nhật mục tiêu" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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
        url: '/personal/pndream/delete',
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
