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
        dom: '<"top"<"row"<"col-md-3 col-sm-4 col-xs-12"l><"col-md-6 col-sm-4 col-xs-12"<"toolbar">><"col-md-3 col-sm-4 col-xs-12 text-right"f>>>rt<"bottom"ip><"clear">',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        createdRow: function(row, data, dataIndex) {
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
            url: '/administrator/admmoney/group',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#ddlSelect').val(),
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
                data: 'CountString',
                orderable: false,
                searchable: false,
                className: 'ctn-center',
                width: '80px'
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
                width: '80px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="javascript:;" data-url="/administrator/admmoney/editgroup/' + data.ID + '\" data-title="Cập nhật quy tắc chi tiêu" title="Cập nhật" class="mg-lr-2 pg_ld"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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

function savePublish(id, publish) {
    $.ajax({
        url: '/administrator/admmoney/publishgroup',
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
        url: '/administrator/admmoney/checkdeletegroup',
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
        url: '/administrator/admmoney/deletegroup',
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
    var currentTime = new Date()
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
        url: '/administrator/admmoney/getgroupsettinginfo',
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
            url: '/administrator/admmoney/savegroupsettinginfo',
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
        url: '/administrator/admmoney/groupsettinginfo',
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
