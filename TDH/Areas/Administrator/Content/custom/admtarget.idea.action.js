$(document).ready(function () {
    $('#Title').focus();
    CKEDITOR.replace('Content',
        {
            filebrowserBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images',
            filebrowserUploadUrl: '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=GetFiles&type=Images',
            filebrowserImageUploadUrl: '/Areas/Administrator/Content/ckfinder/connector?command=QuickUpload&type=Images'
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
    loadPage('/administrator/admtarget/idea', 'Khu vườn ý tưởng');
};

var OnFailure = function (response) {
};
