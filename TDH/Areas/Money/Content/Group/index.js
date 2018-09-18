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
        createdRow: function (row, data, dataIndex) {
            if (data.IsInput === true || (data.PercentCurrent === 0 && data.PercentSetting === 0) || ((data.PercentCurrent * 100) / data.PercentSetting) <= 80) {
                //
            } else {
                var cls = '';
                if (((data.PercentCurrent * 100) / data.PercentSetting) < 100) {
                    $(row).css({ 'background-color': '#ffc107' });
                } else {
                    $(row).css({ 'background-color': '#dc3545' });
                }
            }
        },
        language: language,
        order: [[3, "asc"]],
        ajax: {
            url: '/money/mngroup/index',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#ddlSelect').val();
                d.Parameter2 = $('#monthSelectValue').val();
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
                orderable: false,
                searchable: false,
                width: '80px',
                render: function (obj, type, data, meta) {
                    if (data.IsInput === true) {
                        return 'Thu nhập';
                    } else {
                        return 'Chi tiêu';
                    }
                }
            },
            {
                data: 'Name',
                orderable: true,
                searchable: true,
                width: '200px'
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
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return '<h6><span class="badge badge-info">' + data.PercentSetting + '%</span></h6>';
                }
            },
            {
                orderable: false,
                searchable: false,
                width: '90px',
                className: 'text-right',
                render: function (obj, type, data, meta) {
                    return '<h6><span class="badge badge-info">' + data.MoneyCurrentString + '</span></h6>';
                }
            },
            {
                data: 'CountString',
                orderable: false,
                searchable: false,
                className: 'ctn-center',
                width: '80px'
            },
            {
                orderable: false,
                width: '80px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/money/mngroup/edit/' + data.ID + '\" title="Cập nhật quy tắc chi tiêu" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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

    $("#monthSettingSelect").datepicker({
        language: 'vi',
        format: "yyyy/mm",
        viewMode: "months",
        minViewMode: "months"
    }).on('changeDate', function (ev) {
        $('#monthSettingSelect').datepicker('hide');
        var month = $(this).val().substring(5, 8);
        var year = $(this).val().substring(0, 4);
        getSpendSetting(parseInt(year + month));
    });

});

$(document).on('change', '#ddlSelect', function (e) {
    table.ajax.reload();
});

function confirmDelete(deletedId) {
    $.ajax({
        url: '/money/mngroup/checkdelete',
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
        url: '/money/mngroup/delete',
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

function spendSetting() {
    var currentTime = new Date();
    // returns the month (from 0 to 11)
    var month = currentTime.getMonth() + 1;
    // returns the day of the month (from 1 to 31)
    var day = currentTime.getDate();
    // returns the year (four digits)
    var year = currentTime.getFullYear();
    $('#monthSettingSelect').val(year + '/' + (month < 10 ? '0' + month : month));
    getSpendSetting(parseInt(year + (month < 10 ? '0' + month : month)));
}

function getSpendSetting(year) {
    $('#bodytbListFullGroupSetting').empty();
    $.ajax({
        url: '/money/mngroup/getgroupsettinginfo',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ year: year }),
        success: function (response) {
            $.each(response, function (idx, item) {
                var _str = '<tr>\
                                <td>' + (idx + 1) + '</td>\
                                <td>' + item.GroupName + '</td>\
                                <td class="text-right"><input type="text" data-id="' + item.ID + '" class="form-control text-right settingItem id' + idx + '" maxlength="2" min="0" value="' + item.PercentSetting + '" /></td>\
                                <td class="text-right">' + item.PercentCurrent + '</td>\
                                <td class="text-right">' + item.MoneySettingString + '</td>\
                                <td class="text-right">' + item.MoneyCurrentString + '</td>\
                            </tr>';
                $('#bodytbListFullGroupSetting').append(_str);
                new AutoNumeric('.id' + idx, {
                    minimumValue: '0',
                    maximumValue: '99',
                    selectNumberOnly: true,
                    allowDecimalPadding: false
                });
            });
            $('#settingModel').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveSpendSetting() {
    var list = [];
    $('.settingItem').each(function (idx) {
        var id = $(this).attr('data-id');
        var value = $(this).val();
        list.push({
            ID: id,
            PercentSetting: value
        });
    });
    if (list.length > 0) {
        $.ajax({
            url: '/money/mngroup/savegroupsettinginfo',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({ model: list }),
            success: function (response) {
                table.ajax.reload();
                $('#settingModel').modal('hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }
}

function groupSetting(id) {
    $.ajax({
        url: '/money/mngroup/groupsettinginfo',
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
    $('#groupSettingModel').modal('show');
}

$('#settingModel').on('shown.bs.modal', function () {
    loading($('body'), 'hide');
})