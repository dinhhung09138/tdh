$(document).ready(function () {
    $('#GroupID').focus();
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

var beginSubmit = function () {
    loading($('.content-wrapper'), 'show');
};

var onSuccess = function (response, status, e) {
    loadPage('/administrator/admmoney/category', 'Danh mục thu chi');
};

var OnFailure = function (response) {
};
