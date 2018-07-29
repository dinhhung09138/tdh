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
        footerCallback: function (row, data, start, end, display) {
            var api = this.api(), data;
            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };
            var _str = '';
            //Month Income
            monthIncome = api.column(3, { page: 'current' }).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
            _str = '';
            if (monthIncome > 0) {
                _str = '<span style="color: #359746;">' + number_format(monthIncome, 2) + '</span>';
            }
            else {
                _str = '<span style="color: #dc3545;">' + number_format(monthIncome, 2) + '</span>';
            }
            $(api.column(3).footer()).html(_str);
            //Month Payment
            monthPayment = api.column(4, { page: 'current' }).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
            _str = '<span style="color: #dc3545;">' + number_format(monthPayment, 2) + '</span>';
            $(api.column(4).footer()).html(_str);
            //Month total
            monthTotal = api.column(5, { page: 'current' }).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
            _str = '';
            if (monthTotal > 0) {
                _str = '<span style="color: #359746;">' + number_format(monthTotal, 2) + '</span>';
            }
            else {
                _str = '<span style="color: #dc3545;">' + number_format(monthTotal, 2) + '</span>';
            }
            $(api.column(5).footer()).html(_str);
            // Total over
            total = api.column(6, { page: 'current' }).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
            _str = '';
            if (total > 0) {
                _str = '<span style="color: #359746;">' + number_format(total, 2) + '</span>';
            }
            else {
                _str = '<span style="color: #dc3545;">' + number_format(total, 2) + '</span>';
            }
            $(api.column(6).footer()).html(_str);
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
                data: 'MonthInput',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '110px',
                render: function (obj, type, data, meta) {
                    return data.MonthInputString;
                }
            },
            {
                data: 'MonthOutput',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '110px',
                render: function (obj, type, data, meta) {
                    return data.MonthOutputString;
                }
            },
            {
                data: 'MonthTotal',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '110px',
                render: function (obj, type, data, meta) {
                    if (data.MonthTotal > 0) {
                        return '<span style="color: #359746;">' + data.MonthTotalString + '</span>';
                    }
                    return '<span style="color: #dc3545;">' + data.MonthTotalString + '</span>'; 
                }
            },
            {
                data: 'Total',
                orderable: false,
                searchable: false,
                className: 'text-right',
                width: '110px',
                render: function (obj, type, data, meta) {
                    if (data.Total > 0) {
                        return '<span style="color: #359746;">' + data.TotalString + "</span>"; 
                    }
                    return '<span style="color: #dc3545;">' + data.TotalString + "</span>"; 
                }
            },
            {
                orderable: false,
                width: '70px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    str = str + '<a href="javascript:;" onclick="history(\'' + data.ID + '\',\'' + data.Name + '\');" title="Lịch sử giao dịch" class="mg-lr-2"><i class="fa fa-eye" aria-hidden="true"></i></a>';
                    if (allowEdit === "True") {
                        str = str + '<a href="javascript:;" data-url="/administrator/admmoney/editaccount/' + data.ID + '\" data-title="Cập nhật tài khoản" title="Cập nhật" class="mg-lr-2 pg_ld"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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

function history(id, name) {
    loading($('body'), 'show');
    $.ajax({
        url: '/administrator/admmoney/accounthistory/',
        type: 'get',
        async: false,
        dataType: 'html',
        data: { id: id, name: name, yearMonth: '' },
        success: function (response) {
            document.title = 'Lịch sử giao dịch: ' + name;
            $('#main_layout').empty();
            $('#main_layout').append(response);
            setTimeout(function () { loading($('body'), 'hide') }, 700);      
        }
    }); 
}
