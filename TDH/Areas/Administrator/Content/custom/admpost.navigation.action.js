var active = false;
$(document).ready(function () {
    $('#Title').focus();
    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

});
$(document).on('click', '#selectImg', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl) {
        $('#Image').val(fileUrl);
        $('#imgImage').attr('src', fileUrl);
    };
    finder.popup();
});
$(document).on('click', '#selectImgG', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl, data) {
        $('#MetaOgImage').val(fileUrl);
        $('#imgMetaOgImage').attr('src', fileUrl);
    }
    finder.popup();
});
$(document).on('click', '#selectImgTwitter', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl) {
        $('#MetaTwitterImage').val(fileUrl);
        $('#imgMetaTwitterImage').attr('src', fileUrl);
    };
    finder.popup();
});
var onSuccess = function (response, status, e) {
    if (active === true) {
        return;
    }
    if (response.Status === 0) {
        notification(response.Message, 'success');
        loadPage('/administrator/admpost/navigation', 'Danh mục bài viết');
        active = true;
    } else {
        notification(response.Message, 'error');
    }
    loading($('#submitForm'), 'hide');
}   