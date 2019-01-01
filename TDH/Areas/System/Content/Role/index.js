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
        dom: dom,
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[2, "asc"]],
        ajax: {
            url: '/system/strole/index',
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
                data: 'Name',
                orderable: true,
                searchable: true,
                width: '200px'
            },
            {
                data: 'Description',
                orderable: true,
                searchable: true
            },
            {
                data: 'CountString',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '70px'
            },
            {
                data: 'Publish',
                orderable: false,
                searchable: false,
                className: 'ctn-center',
                width: '60px',
                render: function (obj, type, data, meta) {
                    if (allowEdit === 'True' && data.Count === 0) {
                        if (data.Publish === true) {
                            return '<input type="checkbox" class="flat" name="publish" checked  value="' + data.ID + '" />';
                        } else {
                            return '<input type="checkbox" class="flat" name="publish" value="' + data.ID + '" />';
                        }
                    } else {
                        if (data.Publish === true) {
                            return '<div class="icheckbox_flat-green checked" style="position: relative;">\
                                        <input type="checkbox" class="flat" name="table_records" checked="" \
                                               style="position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;">\
                                        <ins class="iCheck-helper" \
                                                style="position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;">\
                                        </ins>\
                                    </div>';
                        } else {
                            return '<div class="icheckbox_flat-green" style="position: relative;">\
                                        <input type="checkbox" class="flat" name="table_records" value="14" \
                                               style="position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;">\
                                        <ins class="iCheck-helper" \
                                               style="position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;">\
                                        </ins>\
                                    </div>';
                        }
                    }
                }
            },
            {
                orderable: false,
                width: '60px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/system/strole/edit/' + data.ID + '\" title="Cập nhật nhóm quyền" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');" class="mg-lr-2"><i class="fa fa-remove" aria-hidden="true"></i></a>';
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
        url: '/system/strole/publish',
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
        url: '/system/strole/checkdelete',
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
        url: '/system/strole/delete',
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