var active = false;
$(document).ready(function () {
    $('#Title').focus();
    CKEDITOR.replace('Content',
        {
            filebrowserBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images',
            filebrowserUploadUrl: '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=GetFiles&type=Images',
            filebrowserImageUploadUrl: '/Areas/Administrator/Content/ckfinder/connector?command=QuickUpload&type=Images'
        });

});
var onSuccess = function (response, status, e) {
    if (active === true) {
        return;
    }
    if (response.Status === 0) {
        notification(response.Message, 'success');
        loadPage('/administrator/admworking/report', 'Báo cáo');
        active = true;
    } else {
        notification(response.Message, 'error');
    }
    loading($('#submitForm'), 'hide');
}  