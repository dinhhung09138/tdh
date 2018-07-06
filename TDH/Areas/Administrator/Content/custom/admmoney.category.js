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
        dom: '<"top"<"row"<"col-sm-4"l><"col-sm-4"<"toolbar">><"col-sm-4 text-right"f>>>rt<"bottom"ip><"clear">',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[2, "asc"]],
        ajax: {
            url: '/administrator/admmoney/category',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#groupSelect').val()
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
                data: 'GroupName',
                orderable: true,
                searchable: true,
                width: '200px'
            },
            {
                data: 'Name',
                orderable: true,
                searchable: true
            },
            {
                data: 'Notes',
                orderable: true,
                searchable: true
            },
            {
                orderable: false,
                searchable: false,
                width: '100px',
                render: function (obj, type, data, meta) {
                    var _str = '<div class="row">\
                                    <div class="col-12">Thiết lập</div>\
                                    <div class="col-12">' + data.PercentSetting + '</div>\
                                    <div class="col-12">Thực tế</div>\
                                    <div class="col-12">' + data.PercentCurrent + '</div>\
                                </div>';
                    return _str;
                }
            },
            {
                orderable: false,
                searchable: false,
                width: '90px',
                render: function (obj, type, data, meta) {
                    var _str = '<div class="row">\
                                    <div class="col-12">Thiết lập</div>\
                                    <div class="col-12">' + data.MoneySettingString + '</div>\
                                    <div class="col-12">Thực tế</div>\
                                    <div class="col-12">' + data.MoneyCurrentString + '</div>\
                                </div>';
                    return _str;
                }
            },
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="javascript:;" data-url="/administrator/admmoney/editcategory/' + data.ID + '\" data-title="Cập nhật danh mục chi tiêu" title="Cập nhật" class="pg_ld"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');"><i class="fa fa-remove" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });

    $(".dataTables_wrapper .toolbar").append(groupSelect);
    
});

$(document).on('change', '#groupSelect', function (e) {
    table.ajax.reload();
});

function savePublish(id, publish) {
    $.ajax({
        url: '/administrator/admmoney/publishcategory',
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
        url: '/administrator/admmoney/checkdeletecategory',
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
        url: '/administrator/admmoney/deletecategory',
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
