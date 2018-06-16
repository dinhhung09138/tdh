

$(document).ready(function () {

    new AutoNumeric('#PriceString', { maximumValue: 9999999, minimumValue: 0, decimalPlaces: 1 });
    new AutoNumeric('#DiscountString', { maximumValue: 9999999, minimumValue: 0, decimalPlaces: 1 });
    new AutoNumeric('#PercentString', { maximumValue: 100, minimumValue: 0, decimalPlaces: 0 });

    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

    CKEDITOR.replace('ShortDescription',
        {
            filebrowserBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images',
            filebrowserUploadUrl: '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=GetFiles&type=Images',
            filebrowserImageUploadUrl: '/Areas/Administrator/Content/ckfinder/connector?command=QuickUpload&type=Images'
        });

    CKEDITOR.replace('Content',
        {
            filebrowserBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images',
            filebrowserUploadUrl: '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=GetFiles&type=Images',
            filebrowserImageUploadUrl: '/Areas/Administrator/Content/ckfinder/connector?command=QuickUpload&type=Images'
        });

});

$(document).on('click', '#selectImg1', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl, data) {
        $('#Image1').val(fileUrl);
        $('#imgImage1').attr('src', fileUrl);
    }
    finder.popup();
});

$(document).on('click', '#selectImg2', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl, data) {
        $('#Image2').val(fileUrl);
        $('#imgImage2').attr('src', fileUrl);
    }
    finder.popup();
});

$(document).on('click', '#selectImg3', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl, data) {
        $('#Image3').val(fileUrl);
        $('#imgImage3').attr('src', fileUrl);
    }
    finder.popup();
});

$(document).on('click', '#selectImg4', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl, data) {
        $('#Image4').val(fileUrl);
        $('#imgImage4').attr('src', fileUrl);
    }
    finder.popup();
});

$(document).on('click', '#selectImg5', function () {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl, data) {
        $('#Image5').val(fileUrl);
        $('#imgImage5').attr('src', fileUrl);
    }
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
