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
        createRow: function (row, data, dataIndex) {
            if ((data.MoneyCurrent === 0 && data.MoneySetting === 0) || ((data.MoneyCurrent * 100) / data.MoneySetting) < 80) {
                //
            } else {
                var cls = '';
                if (data.MoneyCurrent > data.MoneySetting) {
                    $(row).css({ ' background-color': '#ffc107' });
                } else {
                    $(row).css({ 'background-color': '#dc3545' });
                }
            }
        },
        language: language,
        order: [[2, "asc"]],
        ajax: {
            url: '/administrator/admmoney/category',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#groupSelect').val(),
                d.Parameter2 = $('#monthSelectValue').val()
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
                width: '150px'
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
                    var cls = '';
                    if (((data.PercentCurrent * 100) / data.PercentSetting) <= 80) {
                        cls = 'success';
                    } else {
                        cls = 'info';
                    }
                    var _str = '<div class="row">\
                                    <div class="col-12">\
                                        Thiết lập:  <h6><span class="badge badge-' + cls + '">' + data.PercentSetting + '%</span></h6>\
                                    </div>\
                                    <div class="col-12">\
                                        Thực tế:  <h6><span class="badge badge-' + cls + '">' + data.PercentCurrent + '%</span></h6>\
                                    </div>\
                                </div>';
                    return _str;
                }
            },
            {
                orderable: false,
                searchable: false,
                width: '90px',
                render: function (obj, type, data, meta) {
                    var cls = '';
                    if (((data.PercentCurrent * 100) / data.PercentSetting) <= 80) {
                        cls = 'success';
                    } else {
                        cls = 'info';
                    }
                    var _str = '<div class="row">\
                                    <div class="col-12">\
                                        Thiết lập:  <h6><span class="badge badge-' + cls + '">' + data.MoneySettingString + '</span></h6>\
                                    </div>\
                                    <div class="col-12">\
                                        Thực tế:  <h6><span class="badge badge-' + cls + '">' + data.MoneyCurrentString + '</span></h6>\
                                    </div>\
                                </div>';
                    return _str;
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
                        str = str + '<a href="javascript:;" data-url="/administrator/admmoney/editcategory/' + data.ID + '\" data-title="Cập nhật danh mục thu chi" title="Cập nhật" class="mg-lr-2 pg_ld"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');" class="mg-lr-2"><i class="fa fa-remove" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });

    $(".dataTables_wrapper .toolbar").append(toolbarSearch);

    $("#monthSelect").datepicker({
        language: 'vi',
        format: "yyyy/mm",
        viewMode: "months",
        minViewMode: "months"
    }).on('changeDate', function (ev) {
        $('#monthSelect').datepicker('hide');
        var month = $(this).val().substring(5, 8);
        var year = $(this).val().substring(0, 4);
        $('#monthSelectValue').val(parseInt(year + month));
        table.ajax.reload();
    });

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

function history(id, name) {
    loading($('body'), 'show');
    $.ajax({
        url: '/administrator/admmoney/categoryhistory/',
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