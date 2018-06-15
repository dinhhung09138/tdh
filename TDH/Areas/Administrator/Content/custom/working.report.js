﻿var table;
var id = 0;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();
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
        order: [[4, "desc"]],
        ajax: {
            url: document.URL,
            type: 'post',
            data: function (d) {
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
                data: 'Title',
                orderable: true,
                searchable: true,
                render: function (obj, type, data, meta) {
                    return '<a href="/administrator/admworking/detailreport/' + data.ID + '\">' + data.Title + '</a>';
                }
            },
            {
                data: 'Count',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '90px'
            },
            {
                data: 'UserCreate',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '200px'
            },
            {
                data: 'CreateDateString',
                orderable: true,
                searchable: true,
                className: 'ctn-center',
                width: '200px'
            },
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    var str = '';
                    if (allowEdit === "True") {
                        str = str + '<a href="/administrator/admworking/editreport/' + data.ID + '\" title="Cập nhật"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    }
                    if (allowDelete === "True") {
                        str = str + '<a href="javascript:;" title="Xóa" onclick="confirmDelete(\'' + data.ID + '\');"><i class="fa fa-remove" aria-hidden="true"></i></a>';
                    }
                    return str;
                }
            }
        ]
    });


});

function confirmDelete(deletedId) {
    $.ajax({
        url: '/administrator/admworking/checkdeletereport',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: deletedId }),
        success: function (response) {
            if (response.Status === 3) {
                id = deletedId;
                $('#deleteModal').modal('show');
            } else {
                notification(response.Message, 'error');
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}

function deleteItem() {
    $.ajax({
        url: '/administrator/admworking/deletereport',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ID: id }),
        success: function (response) {
            if (response.Status === 0) {
                notification(response.Message, 'success');
                table.ajax.reload();
            } else {
                notification(response.Message, 'error');
            }
            id = '';
            $('#deleteModal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#deleteModal').modal('hide');
        }
    });
}
