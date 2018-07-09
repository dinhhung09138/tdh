var table;
var id = 0;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();

    $('#cbIncomeCategory').attr('data-parsley-required', true);
    $('#cbIncomeCategory').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    $('#cbIncomeAccount').attr('data-parsley-required', true);
    $('#cbIncomeAccount').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    //
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        ordering: false,
        paging: true,
        responsive: true,
        pageLength: 25,
        pagingType: 'full_numbers',
        dom: '<"top"<"row"<"col-sm-4"l><"col-sm-4"<"toolbar">><"col-sm-4 text-right"f>>>rt<"bottom"ip><"clear">',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        createdRow: function (row, data, dataIndex) {
           
        },
        language: language,
        ajax: {
            url: '/administrator/admmoney/flowhistory',
            type: 'post',
            data: function (d) {
                d.Parameter1 = $('#ddlSelect').val()
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
                data: 'TypeName',
                width: '90px'
            },
            {
                data: 'CategoryName',
                width: '200px'
            },
            {
                data: 'FromName',
            },
            {
                data: 'ToName',
            },
            {
                data: 'Title',
            },
            {
                data: 'Money',
                width: '100px'
            },
            {
                data: 'Fee',
                width: '100px'
            },
            {
                data: 'DateString',
                width: '100px'
            },
            {
                width: '80px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="javascript:;" onclick="groupSetting(\'' + data.ID + '\')" title="Thiết lập" class="mg-lr-2"><i class="fa fa-cogs" aria-hidden="true"></i></a>';
                        str = str + '<a href="javascript:;" data-url="/administrator/admmoney/editgroup/' + data.ID + '\" data-title="Cập nhật quy tắc chi tiêu" title="Cập nhật" class="mg-lr-2"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });

    $(".dataTables_wrapper .toolbar").append(ddlSelect);

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

function income() {
    $('#txtIncomeTitle').val('');
    $('#txtIncomeMoney').val('');
    $('#cbIncomeCategory').val('');
    $('#cbIncomeAccount').val('');
    $('#txtIncomePurpose').val('');
    $('#incomeModel').modal('show');
}

function saveIncome() {
    $('#btnSaveIncome').attr('disabled', true);
    $('#frmIncome').parsley().validate();
    if ($('#frmIncome').parsley().isValid() === true) {
        $.ajax({
            url: '/administrator/admmoney/saveincome',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                AccountID: $('#cbIncomeAccount').val(),
                Title: $('#txtIncomeTitle').val(),
                CategoryID: $('#cbIncomeCategory').val(),
                Money: $('#txtIncomeMoney').val(),
                Purpose: $('#txtIncomePurpose').val()
            }),
            success: function (response) {
                table.ajax.reload();
                $('#incomeModel').modal('hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    } else {
        $('#btnSaveIncome').attr('disabled', false);
    }
}

$('#incomeModel').on('shown.bs.modal', function (e) {
    $('#txtIncomeTitle').focus();
});
