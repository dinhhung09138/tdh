
$(document).ready(function () {
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

});