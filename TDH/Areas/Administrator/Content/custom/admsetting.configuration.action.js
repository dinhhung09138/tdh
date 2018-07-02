var active = false;
$(document).ready(function () {
    $('#Description').focus();
    $(document).on('click', '#selectImg', function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $('#Value').val(fileUrl);
            $('#imgValue').attr('src', fileUrl);
        };
        finder.popup();
    });
    $('#submitForm').click(function (e) {
        loading($(this), 'show');
        if ($('.form').validate().checkForm() === false) {
            loading($(this), 'hide');
        }
    });
});
var onSuccess = function (response, status, e) {
    if (active === true) {
        return;
    }
    if (response.Status === 0) {
        notification(response.Message, 'success');
        loadPage('/administrator/admsetting/configuration', 'Cấu hình chung');
        active = true;
    } else {
        notification(response.Message, 'error');
    }
    loading($('#submitForm'), 'hide');
}    