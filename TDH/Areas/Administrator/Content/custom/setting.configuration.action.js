
$(document).ready(function () {
    
    $(document).on('click', '#selectImg', function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $('#Value').val(fileUrl);
            $('#imgValue').attr('src', fileUrl);
        };
        finder.popup();
    });
    
});