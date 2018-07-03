$(document).ready(function () {
    $('#Title').focus();
    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

    $(document).on('submit', "#form", function (e) {
        e.preventDefault();
        $(document).unbind('submit');
        return;
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
var beginSubmit = function () {
    loading($('.content-wrapper'), 'show');
};

var onSuccess = function (response, status, e) {
    loadPage('/administrator/admpost/navigation', 'Danh mục bài viết');
};

var OnFailure = function (response) {
};
