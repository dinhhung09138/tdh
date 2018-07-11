var table;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    //
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        responsive: true,
        paging: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        dom: dom,
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[3, "desc"]],
        ajax: {
            url: '/administrator/admsetting/navigation',
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
                orderable: false,
                searchable: false,
                className: 'ctn-center',
                width: '90px',
                render: function (obj, type, data, meta) {
                    if (allowEdit === 'True') {
                        if (data.Selected === true) {
                            return '<input type="checkbox" class="flat" name="Selected" checked  value="' + data.NavigationID + '" />';
                        } else {
                            return '<input type="checkbox" class="flat" name="Selected" value="' + data.NavigationID + '" />';
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
                data: 'NavigationTitle',
                orderable: true,
                searchable: true,
                width: '150px',
            },
            {
                data: 'Ordering',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '100px',
                render: function (obj, type, data, meta) {
                    if (data.Selected === true) {
                        return '<span class="ordering" data-nav="' + data.NavigationID + '" data-ordering="' + data.Ordering + '">' + data.Ordering + '</span>';
                    } else {
                        return data.Ordering;
                    }
                }
            }
        ]
    });

    table.on('draw', function () {
        if ($('#tbList input[name="Selected"]')[0]) {
            $('#tbList input[name="Selected"]').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green',
                increaseArea: '20%'
            });
            $('#tbList input[name="Selected"]').on('ifChecked', function () {
                saveSelected($(this).val(), true);
            });
            $('#tbList input[name="Selected"]').on('ifUnchecked', function () {
                saveSelected($(this).val(), false);
            });
        }
    });
});

function saveSelected(id, selected) {
    $.ajax({
        url: '/Administrator/AdmSetting/savenavigation',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ NavigationID: id, Selected: selected }),
        success: function (response) {
            if (response === 0) {
                table.ajax.reload();
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

$(document).on('click', '.ordering', function (e) {
    $('#orderingSelection').val($(this).attr('data-ordering'));
    $('#navID').val($(this).attr('data-nav'));
    $('#orderingModal').modal('show');
});

function updateOrder() {
    $.ajax({
        url: '/Administrator/AdmSetting/savenavigation',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ NavigationID: $('#navID').val(), Ordering: $('#orderingSelection').val(), Selected: true }),
        success: function (response) {
            if (response === 0) {
                table.ajax.reload();
            }
            $('#orderingModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}