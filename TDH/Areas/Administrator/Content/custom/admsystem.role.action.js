var table;

$(document).ready(function () {
    $('#Name').focus();
    //new AutoNumeric('#Level', { maximumValue: 10, minimumValue: 0, decimalPlaces: 0 });

    table = $('#tbList').DataTable({
        processing: false,
        serverSide: false,
        searching: false,
        ordering: false,
        paging: false,
        responsive: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            loading($('body'), 'hide');
        },
        stateSave: false,
        language: language
    });

    table.on('draw', function () {
        if ($('#tbList input[type="checkbox"]')[0]) {
            $('#tbList input[type="checkbox"]').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green',
                increaseArea: '20%'
            });
        }
    });

    $('input[type="checkbox"]').iCheck({
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
    loadPage('/administrator/admsystem/role', 'Nhóm quyền');
};

var OnFailure = function (response) {
};



