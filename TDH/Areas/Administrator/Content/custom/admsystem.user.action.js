var active = false;
$(document).ready(function () {
    $('#RoleID').focus();
    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    
});
var onSuccess = function (response, status, e) {
    if (active === true) {
        return;
    }
    if (response.Status === 0) {
        notification(response.Message, 'success');
        loadPage('/administrator/admsystem/user', 'Tài khoản người dùng');
        active = true;
    } else {
        notification(response.Message, 'error');
    }
    loading($('#submitForm'), 'hide');
}  