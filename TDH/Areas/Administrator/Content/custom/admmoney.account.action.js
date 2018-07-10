$(document).ready(function () {
    $('#AccountTypeID').focus();
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
    loadPage('/administrator/admmoney/account', 'Tài khoản');
};

var OnFailure = function (response) {
};

function history(id, name, yearMonth) {
    loading($('.content-wrapper'), 'show');
    $.ajax({
        url: '/administrator/admmoney/accounthistory/',
        type: 'get',
        async: false,
        dataType: 'html',
        data: { id: id, name: name, yearMonth: yearMonth },
        success: function (response) {
            document.title = 'Lịch sử giao dịch: ' + name;
            $('#main_layout').empty();
            $('#main_layout').append(response);
            setTimeout(function () { loading($('.content-wrapper'), 'hide') }, 700);      
        }
    }); 
}