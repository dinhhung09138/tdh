$(document).ready(function () {
    $('#Name').focus();

    $(".datePicker").datepicker({
        format: "dd/mm/yyyy"
    }).on('changeDate', function (ev) {
        $('.datePicker').datepicker('hide');
    });

    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

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
    window.location.href = '/personal/pneducation/index';
};

var OnFailure = function (response) {
};
