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
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: {
            lengthMenu: 'Hiển thị _MENU_ dòng mỗi trang',
            zeroRecords: 'Dữ liệu không tồn tại',
            info: 'Trang _PAGE_/_PAGES_',
            infoEmpty: '',//'Không tìm thấy kết quả',
            infoFiltered: '',//'(Tìm kiếm trên _MAX_ dòng)',
            search: 'Tìm kiếm',
            processing: 'Đang xử lý',
            paginate: {
                first: '<<',
                previous: '<',
                next: '>',
                last: '>>'
            }
        },
        order: [[4, "desc"]],
        ajax: {
            url: document.URL,
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
                orderable: false,
                searchable: false,
                className: 'ctn-center',
                width: '40px',
                render: function (obj, type, data, meta) {
                    if (allowEdit === 'True') {
                        if (data.Selected === true) {
                            return '<input type="checkbox" class="flat" name="select" checked  value="' + data.CategoryID + '" />';
                        } else {
                            return '<input type="checkbox" class="flat" name="select" value="' + data.CategoryID + '" />';
                        }
                    } else {
                        if (data.Selected === true) {
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
                data: 'CategoryName',
                orderable: true,
                searchable: true,
            },
            {
                data: 'Count',
                orderable: false,
                searchable: false,
                width: '100px',
                className: 'text-right',
            },
            {
                data: 'Ordering',
                orderable: false,
                searchable: false,
                width: '100px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    if (allowEdit === 'True') {
                        if (data.Selected === true) {
                            return '<span class="ordering" data-cate="' + data.CategoryID + '" data-ordering="' + data.Ordering + '">' + data.Ordering + '</span>';
                        } else {
                            return '';
                        }
                    } else {
                        return data.Ordering;
                    }                    
                }
            }
        ]
    });

    table.on('draw', function () {
        if ($('#tbList input[name="select"]')[0]) {
            $('#tbList input[name="select"]').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green',
                increaseArea: '20%'
            });

            $('#tbList input[name="select"]').on('ifChecked', function () {
                selectItem($(this).val(), true);
            });

            $('#tbList input[name="select"]').on('ifUnchecked', function () {
                selectItem($(this).val(), false);
            });

        }
    });
});

function selectItem(id, selected) {
    $.ajax({
        url: '/administrator/admsetting/savehomecate',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ CategoryID: id, Selected: selected }),
        success: function (response) {
            if (response.Status === 0) {
                notification(response.Message, 'success');
                table.ajax.reload();
            } else { 
                notification(response.Message, 'error');
            }
            id = '';
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function confirmDelete(deletedId) {
    id = deletedId;
    $('#deleteModal').modal('show');
}

$(document).on('click', '.ordering', function (e) {
    $('#cateID').val($(this).attr('data-cate'));
    $('#cbOrdering').val($(this).attr('data-ordering'));
    $('#orderModal').modal('show');
});

function saveOrder() {
    $.ajax({
        url: '/administrator/admsetting/savehomecate',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ CategoryID: $('#cateID').val(), Ordering: $('#cbOrdering').val(), Selected: true }),
        success: function (response) {
            if (response.Status === 0) {
                notification(response.Message, 'success');
                table.ajax.reload();
            } else { //error
                notification(response.Message, 'error');
            }
            $('#orderModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#orderModal').modal('hide');
        }
    });
}