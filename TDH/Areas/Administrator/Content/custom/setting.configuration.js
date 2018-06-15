var table;
var id = '';

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    //
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
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
        },
        order: [[1, "asc"]],
        ajax: {
            url: document.URL,
            type: 'post',
            data: function (d) {
                //d.ModuleCode = ""
            }
        },
        columns: [
            {
                orderable: false,
                width: '30px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: 'Key',
                orderable: true,
                searchable: true,
                width: '100px'
            },
            {
                data: 'Description',
                orderable: true,
                searchable: true
            },
            {
                data: 'Value',
                orderable: true,
                searchable: true
            },
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/administrator/admsetting/editconfiguration/' + data.Key + '\" title="Cập nhật"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });
    
});
