
$(document).ready(function () {
    $('#Content').focus();
    CKEDITOR.replace('Content',
        {
            filebrowserBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images',
            filebrowserUploadUrl: '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=GetFiles&type=Images',
            filebrowserImageUploadUrl: '/Areas/Administrator/Content/ckfinder/connector?command=QuickUpload&type=Images'
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
        finder.selectActionFunction = function (fileUrl) {
            $('#MetaOgImage').val(fileUrl);
            $('#imgMetaOgImage').attr('src', fileUrl);
        };
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

    $(document).on('submit', "#form", function (e) {
        e.preventDefault();
        $(document).unbind('submit');
        return;
    });

});
var beginSubmit = function () {
    loading($('body'), 'show');
};

var onSuccess = function (response, status, e) {
    loadPage('/administrator/admpost/about', 'Giới thiệu');
};

var OnFailure = function (response) {
};
