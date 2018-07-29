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
    loading($('body'), 'show');
};

var onSuccess = function (response, status, e) {
    loadPage('/administrator/admmoney/category', 'Danh mục thu chi');
};

var OnFailure = function (response) {
};

function history(id, name, yearMonth) {
    loading($('body'), 'show');
    $.ajax({
        url: '/administrator/admmoney/categoryhistory/',
        type: 'get',
        async: false,
        dataType: 'html',
        data: { id: id, name: name, yearMonth: yearMonth },
        success: function (response) {
            document.title = 'Lịch sử giao dịch: ' + name;
            $('#main_layout').empty();
            $('#main_layout').append(response);
            setTimeout(function () { loading($('body'), 'hide') }, 700);      
        }
    }); 
}