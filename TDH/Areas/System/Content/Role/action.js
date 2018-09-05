﻿var table;

$(document).ready(function () {
    $('#Name').focus();

    table = $('#tbList').DataTable({
        processing: false,
        serverSide: false,
        searching: false,
        ordering: false,
        paging: false,
        responsive: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        dom: dom,
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
        },
        stateSave: false,
        language: language,
        order: [[1, "asc"]]
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
    loading($('body'), 'show');
};

var onSuccess = function (response, status, e) {
    window.location.href = '/system/strole/index';
};

var OnFailure = function (response) {
};



