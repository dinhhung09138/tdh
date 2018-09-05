var active = false;
$(document).ready(function () {
    $('#Description').focus();
    $(document).on('click', '#selectImg', function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $('#Value').val(fileUrl);
            $('#imgValue').attr('src', fileUrl);
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
    window.location.href = '/website/wsetting/configuration';
};

var OnFailure = function (response) {
};