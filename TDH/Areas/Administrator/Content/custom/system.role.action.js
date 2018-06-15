var table;

$(document).ready(function () {

    new AutoNumeric('#Level', { maximumValue: 10, minimumValue: 0, decimalPlaces: 0 });

    table = $('#tbList').DataTable({
        processing: false,
        serverSide: false,
        searching: false,
        ordering: false,
        paging: false,
        pageLength: 10,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        stateSave: false,
        language: {
            lengthMenu: 'Hiển thị _MENU_ dòng mỗi trang',
            zeroRecords: 'Dữ liệu không tồn tại',
            info: 'Trang _PAGE_/_PAGES_',
            infoEmpty: '',//'Không tìm thấy kết quả',
            infoFiltered: '',//'(Tìm kiếm trên _MAX_ dòng)',
            search: 'Tìm kiếm',
            processing: 'Đang xử lý',
            paginate: {
                first: '<<',
                previous: '<',
                next: '>',
                last: '>>'
            }
        }
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
});