
$(document).ready(function () {
    $('#CategoryID').focus();
    if ($('#IsNavigation').val() === 'True') {
        $('#CategoryID').hide();
        $('#NavigationID').show();
        $('#navChecked').iCheck('check');
    } else {
        $('#CategoryID').show();
        $('#NavigationID').hide();
        $('#cateChecked').iCheck('check');
    }

    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

    $('#navChecked').on('ifChecked', function () {
        console.log('nav checked');
        $('#IsNavigation').val(true);
        $('#CategoryID').hide();
        $('#NavigationID').show();
    });

    $('#cateChecked').on('ifChecked', function () {
        console.log('cate checked');
        $('#IsNavigation').val(false);
        $('#CategoryID').show();
        $('#NavigationID').hide();
    });

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
    window.location.href = '/administrator/admpost/news';
};

var OnFailure = function (response) {
};
