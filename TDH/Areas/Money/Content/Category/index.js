var table;
var id = 0;
var janValue;
var febValue;
var marValue;
var aprValue;
var mayValue;
var junValue;
var julValue;
var augValue;
var septValue;
var octValue;
var novValue;
var decValue;
var options = { minimumValue: '0', maximumValue: '99999999999', digitGroupSeparator: ',', decimalPlaces: 2, decimalCharacter: '.', selectNumberOnly: true, allowDecimalPadding: true };

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();
    //
    janValue = new AutoNumeric('#janValue', options);
    febValue = new AutoNumeric('#febValue', options);
    marValue = new AutoNumeric('#marValue', options);
    aprValue = new AutoNumeric('#aprValue', options);
    mayValue = new AutoNumeric('#mayValue', options);
    junValue = new AutoNumeric('#junValue', options);
    julValue = new AutoNumeric('#julValue', options);
    augValue = new AutoNumeric('#augValue', options);
    septValue = new AutoNumeric('#septValue', options);
    octValue = new AutoNumeric('#octValue', options);
    novValue = new AutoNumeric('#novValue', options);
    decValue = new AutoNumeric('#decValue', options);
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
            url: '/money/mncategory/index',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#groupSelect').val();
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
                class: 'text-right',
                render: function (obj, type, data, meta) {
                    var _str = '<h6><span class="badge badge-info">' + data.MoneySettingString + '</span></h6>';
                    return _str;
                }
            },
            {
                orderable: false,
                searchable: false,
                width: '90px',
                class: 'text-right',
                render: function (obj, type, data, meta) {
                    var cls = 'success';
                    if ((data.MoneyCurrent > data.MoneySetting) && (data.IsIncome === false)) {
                        cls = 'danger';
                    }
                    var _str = '<h6><span class="badge badge-' + cls + '">' + data.MoneyCurrentString + '</span></h6>';
                    return _str;
                }
            },
            {
                orderable: false,
                width: '110px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    str = str + '<a href="/money/mncategory/history/' + data.ID + '\" title="Lịch sử giao dịch" class="mg-lr-2"><i class="fa fa-eye" aria-hidden="true"></i></a>';
                    str = str + '<a href="javascript:;" onclick="setting(\'' + data.ID + '\',\'' + data.Name + '\');\" title="Thiết lập" class="mg-lr-2"><i class="fa fa-cog" aria-hidden="true"></i></a>';
                    if (allowEdit === "True") {
                        str = str + '<a href="/money/mncategory/edit/' + data.ID + '\" title="Cập nhật danh mục thu chi" title="Cập nhật" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
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
        url: '/money/mncategory/publish',
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
        url: '/money/mncategory/checkdelete',
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

function setting(id, name) {

    var year = (new Date()).getFullYear();
    var month = (new Date()).getMonth() + 1;
    $.ajax({
        url: '/money/mncategory/setting/',
        type: 'get',
        contentType: 'application/json',
        data: { id: id, year: year },
        success: function (response) {
            $('#settingTitleItemName').html('Thiết lập: ' + name);
            $('#settingTitleItemID').val(id);
            var readOnly = false;
            $.each(response, function (idx, item) {
                readOnly = false;
                if (item.Year < year || (item.Year === year && item.Month < month)) {
                    readOnly = true;
                }
                if (item.Month === 1) {
                    $('#janID').val(item.ID);
                    janValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#janValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 2) {
                    $('#febID').val(item.ID);
                    febValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#febValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 3) {
                    $('#marID').val(item.ID);
                    marValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#marValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 4) {
                    $('#aprID').val(item.ID);
                    aprValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#aprValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 5) {
                    $('#mayID').val(item.ID);
                    mayValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#mayValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 6) {
                    $('#junID').val(item.ID);
                    junValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#junValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 7) {
                    $('#julID').val(item.ID);
                    julValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#julValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 8) {
                    $('#augID').val(item.ID);
                    augValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#augValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 9) {
                    $('#septID').val(item.ID);
                    septValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#septValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 10) {
                    $('#octID').val(item.ID);
                    octValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#octValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 11) {
                    $('#novID').val(item.ID);
                    novValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#novValue').attr('readonly', 'readonly');
                    }
                }
                if (item.Month === 12) {
                    $('#decID').val(item.ID);
                    decValue.set(item.MoneySetting);
                    if (readOnly === true) {
                        $('#decValue').attr('readonly', 'readonly');
                    }
                }
            });
            $('#settingModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveSettingItem() {

    var list = [];
    if ($('#janValue').is('[readonly]') === false) {
        list.push({
            ID: $('#janID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: janValue.getNumber()
        });
    }
    if ($('#febValue').is('[readonly]') === false) {
        list.push({
            ID: $('#febID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: febValue.getNumber()
        });
    }
    if ($('#marValue').is('[readonly]') === false) {
        list.push({
            ID: $('#marID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: marValue.getNumber()
        });
    }
    if ($('#aprValue').is('[readonly]') === false) {
        list.push({
            ID: $('#aprID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: aprValue.getNumber()
        });
    }
    if ($('#mayValue').is('[readonly]') === false) {
        list.push({
            ID: $('#mayID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: mayValue.getNumber()
        });
    }
    if ($('#junValue').is('[readonly]') === false) {
        list.push({
            ID: $('#junID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: junValue.getNumber()
        });
    }
    if ($('#julValue').is('[readonly]') === false) {
        list.push({
            ID: $('#julID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: julValue.getNumber()
        });
    }
    if ($('#augValue').is('[readonly]') === false) {
        list.push({
            ID: $('#augID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: augValue.getNumber()
        });
    }
    if ($('#septValue').is('[readonly]') === false) {
        list.push({
            ID: $('#septID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: septValue.getNumber()
        });
    }
    if ($('#octValue').is('[readonly]') === false) {
        list.push({
            ID: $('#octID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: octValue.getNumber()
        });
    }
    if ($('#novValue').is('[readonly]') === false) {
        list.push({
            ID: $('#novID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: novValue.getNumber()
        });
    }
    if ($('#decValue').is('[readonly]') === false) {
        list.push({
            ID: $('#decID').val(),
            CategoryID: $('#settingTitleItemID').val(),
            MoneySetting: decValue.getNumber()
        });
    }
    $.ajax({
        url: '/money/mncategory/savesetting/',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(list),
        success: function (response) {
            if (response === 0) {
                table.ajax.reload();
                $('#settingModal').modal('hide');
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function deleteItem() {
    $.ajax({
        url: '/money/mncategory/delete',
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
        url: '/money/mncategory/history/',
        type: 'get',
        async: false,
        dataType: 'html',
        data: { id: id, name: name, yearMonth: '' },
        success: function (response) {
            document.title = 'Lịch sử giao dịch: ' + name;
            $('#main_layout').empty();
            $('#main_layout').append(response);
            setTimeout(function () { loading($('body'), 'hide'); }, 700);
        }
    });
}

$('#settingModal').on('hidden.bs.modal', function () {
    $('#settingTitleItemName').html('');
    $('#settingTitleItemID').val('');
});