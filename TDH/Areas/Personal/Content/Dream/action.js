$(document).ready(function () {
    $('#Title').focus();
    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

    $(".datePicker").datepicker({
        format: "dd/mm/yyyy"
    }).on('changeDate', function (ev) {
        $('.datePicker').datepicker('hide');
        });

    new AutoNumeric('#Ordering', {
        minimumValue: '0',
        maximumValue: '99',
        decimalPlaces: 0,
        selectNumberOnly: true,
        allowDecimalPadding: false
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
    window.location.href = '/personal/pndream/index';
};

var OnFailure = function (response) {
};
