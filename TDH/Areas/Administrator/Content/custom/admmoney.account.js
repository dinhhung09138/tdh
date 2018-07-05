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
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[1, "asc"]],
        ajax: {
            url: '/administrator/admmoney/account',
            type: 'post',
            data: function (d) {
                //d.ModuleCode = ""
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
                data: 'AccountTypeName',
                orderable: true,
                searchable: true,
                width: '200px'
            },
            {
                data: 'Name',
                orderable: true,
                searchable: true,
                render: function (obj, type, data, meta) {
                    return data.Name;
                }
            },
            {
                data: 'StartString',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '150px',
                render: function (obj, type, data, meta) {
                    return data.StartString;
                }
            },
            {
                data: 'InputString',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '150px',
                render: function (obj, type, data, meta) {
                    return data.InputString;
                }
            },
            {
                data: 'OutputString',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '150px',
                render: function (obj, type, data, meta) {
                    return data.OutputString;
                }
            },
            {
                data: 'EndString',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '150px',
                render: function (obj, type, data, meta) {
                    return data.EndString;
                }
            },
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="javascript:;" data-url="/administrator/admmoney/editaccount/' + data.ID + '\" data-title="Cập nhật tài khoản" title="Cập nhật" class="pg_ld"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');"><i class="fa fa-remove" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });

    table.on('draw', function () {
        if ($('#tbList input[name="publish"]')[0]) {
            $('#tbList input[name="publish"]').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green',
                increaseArea: '20%'
            });

            $('#tbList input[name="publish"]').on('ifChecked', function () {
                savePublish($(this).val(), true);
            });

            $('#tbList input[name="publish"]').on('ifUnchecked', function () {
                savePublish($(this).val(), false);
            });

        }
    });
});

function savePublish(id, publish) {
    $.ajax({
        url: '/administrator/admmoney/publishaccount',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: id, Publish: publish }),
        success: function (response) {
            if (response === 0) {
                table.ajax.reload();
            }
            id = '';
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function confirmDelete(deletedId) {
    $.ajax({
        url: '/administrator/admmoney/checkdeleteaccount',
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
            $('#deleteModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}

function deleteItem() {
    $.ajax({
        url: '/administrator/admmoney/deleteaccount',
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