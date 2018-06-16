$(document).ready(function () {
    $(function () {
        $("#txtDate").datepicker();
    });
    $('input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green',
        increaseArea: '20%'
    });
});
$(document).on('click', '#addChild', function (e) {
    var level = parseInt($(this).attr('data-level'));
    $('#hdLevel').val(level + 1);
    $('#hdInsert').val(true);
    $('#txtTargetName').val('');
    $('#txtDate').datepicker("setDate", 'doday');
    $('#targetModal').modal('show');
});

function saveTarget() {
    $.ajax({
        url: '/administrator/admtarget/savetarget',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({
            Level: $('#hdLevel').val(),
            Title: $('#txtTargetName').val(),
            EstimateDateString: $('#txtDate').val(),
            Insert: $('#hdInsert').val()
        }),
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