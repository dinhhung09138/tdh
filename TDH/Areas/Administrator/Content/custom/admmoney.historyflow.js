var table;
var id = 0;
var incomeMoney;
var paymentMoney;
var transferMoney;
var transferFee;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();
    //
    incomeMoney = new AutoNumeric('#txtIncomeMoney', {
        minimumValue: '0',
        maximumValue: '99999999999',
        digitGroupSeparator: ',',
        decimalPlaces: 2,
        decimalCharacter: '.',
        selectNumberOnly: true,
        allowDecimalPadding: true
    });
    paymentMoney = new AutoNumeric('#txtPaymentMoney', {
        minimumValue: '0',
        maximumValue: '99999999999',
        digitGroupSeparator: ',',
        decimalPlaces: 2,
        decimalCharacter: '.',
        selectNumberOnly: true,
        allowDecimalPadding: true
    });
    transferMoney = new AutoNumeric('#txtTransferMoney', {
        minimumValue: '0',
        maximumValue: '99999999999',
        digitGroupSeparator: ',',
        decimalPlaces: 2,
        decimalCharacter: '.',
        selectNumberOnly: true,
        allowDecimalPadding: true
    });
    transferFee = new AutoNumeric('#txtTransferFee', {
        minimumValue: '0',
        maximumValue: '99999999',
        digitGroupSeparator: ',',
        decimalPlaces: 2,
        decimalCharacter: '.',
        selectNumberOnly: true,
        allowDecimalPadding: true
    });
    //
    $(".datePicker").datepicker({
        format: "dd/mm/yyyy"
    }).on('changeDate', function (ev) {
        $('.datePicker').datepicker('hide');
    });
    //
    $('#cbIncomeCategory').attr('data-parsley-required', true);
    $('#cbIncomeCategory').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    $('#cbIncomeAccount').attr('data-parsley-required', true);
    $('#cbIncomeAccount').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    $('#cbPaymentCategory').attr('data-parsley-required', true);
    $('#cbPaymentCategory').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    $('#cbPaymentAccount').attr('data-parsley-required', true);
    $('#cbPaymentAccount').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    $('#cbPaymentAccountFrom').attr('data-parsley-required', true);
    $('#cbPaymentAccountFrom').attr('data-parsley-required-message', 'Nội dung không được rỗng');
    $('#cbPaymentAccountTo').attr('data-parsley-required', true);
    $('#cbPaymentAccountTo').attr('data-parsley-required-message', 'Nội dung không được rỗng');
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
        dom: dom,
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

$(document).on('change', '#ddlSelect', function (e) {
    table.ajax.reload();
});

function income() {
    var currentTime = new Date()
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    $('#txtIncomeDate').val(day + '/' + (month < 10 ? '0' + month : month) + '/' + year);
    $('#txtIncomeTitle').val('');
    $('#txtIncomeMoney').val('');
    $('#cbIncomeCategory').val('');
    $('#cbIncomeAccount').val('');
    $('#txtIncomePurpose').val('');
    $('#incomeModel').modal('show');
}

function saveIncome() {
    loading($('#btnSaveIncome'), 'show');
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
                DateString: $('#txtIncomeDate').val(),
                CategoryID: $('#cbIncomeCategory').val(),
                Money: incomeMoney.getNumber(),
                Purpose: $('#txtIncomePurpose').val()
            }),
            success: function (response) {
                table.ajax.reload();
                $('#incomeModel').modal('hide');
                loading($('#btnSaveIncome'), 'hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
                loading($('#btnSaveIncome'), 'hide');
            }
        });
    } else {
        loading($('#btnSaveIncome'), 'hide');
    }
}

$('#incomeModel').on('shown.bs.modal', function (e) {
    $('#txtIncomeTitle').focus();
});

function payment() {
    var currentTime = new Date()
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    $('#txtPaymentDate').val(day + '/' + (month < 10 ? '0' + month : month) + '/' + year);
    $('#txtPaymentTitle').val('');
    $('#txtPaymentMoney').val('');
    $('#cbPaymentCategory').val('');
    $('#cbPaymentAccount').val('');
    $('#txtPaymentPurpose').val('');
    $('#paymentModel').modal('show');
}

function savePayment() {
    loading($('#btnSavePayment'), 'show');
    $('#frmPayment').parsley().validate();
    if ($('#frmPayment').parsley().isValid() === true) {
        $.ajax({
            url: '/administrator/admmoney/savepayment',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                AccountID: $('#cbPaymentAccount').val(),
                Title: $('#txtPaymentTitle').val(),
                DateString: $('#txtPaymentDate').val(),
                CategoryID: $('#cbPaymentCategory').val(),
                Money: paymentMoney.getNumber(),
                Purpose: $('#txtPaymentPurpose').val()
            }),
            success: function (response) {
                table.ajax.reload();
                $('#paymentModel').modal('hide');
                loading($('#btnSavePayment'), 'hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
                loading($('#btnSavePayment'), 'hide');
            }
        });
    } else {
        loading($('#btnSavePayment'), 'hide');
    }
}

$('#paymentModel').on('shown.bs.modal', function (e) {
    $('#txtPaymentTitle').focus();
});

function transfer() {
    var currentTime = new Date()
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    $('#txtTransferDate').val(day + '/' + (month < 10 ? '0' + month : month) + '/' + year);
    $('#txtTransferTitle').val('');
    $('#txtTransferMoney').val('');
    $('#txtTransferFee').val('');
    $('#cbTransferAccountFrom').val('');
    $('#cbTransferAccountTo').val('');
    $('#txtTransferPurpose').val('');
    $('#transferModel').modal('show');
}

function saveTransfer() {
    loading($('#btnSaveTransfer'), 'show');
    $('#frmTransfer').parsley().validate();
    if ($('#frmTransfer').parsley().isValid() === true) {
        $.ajax({
            url: '/administrator/admmoney/savetransfer',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                AccountFrom: $('#cbTransferAccountFrom').val(),
                AccountTo: $('#cbTransferAccountTo').val(),
                Title: $('#txtTransferTitle').val(),
                DateString: $('#txtTransferDate').val(),
                Money: transferMoney.getNumber(),
                Fee: transferFee.getNumber(),
                Purpose: $('#txtTransferPurpose').val()
            }),
            success: function (response) {
                table.ajax.reload();
                $('#transferModel').modal('hide');
                loading($('#btnSaveTransfer'), 'hide');
            },
            error: function (xhr, status, error) {
                console.log(error);
                loading($('#btnSaveTransfer'), 'hide');
            }
        });
    } else {
        loading($('#btnSaveTransfer'), 'hide');
    }
}

$('#transferModel').on('shown.bs.modal', function (e) {
    $('#txtTransferTitle').focus();
});
