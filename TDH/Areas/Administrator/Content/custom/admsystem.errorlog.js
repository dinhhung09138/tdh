var table;
var id = 0;

$(document).ready(function () {
    var allowEdit = $('#edit').val();
    var allowDelete = $('#delete').val();
    //
    table = $('#tbList').DataTable({
        processing: true,
        serverSide: true,
        searching: false,
        ordering: false,
        paging: true,
        responsive: true,
        pageLength: 10,
        pagingType: 'full_numbers',
        info: true,
        autoWidth: false,
        initComplete: function (settings, json) {
            //Do something after finish
        },
        language: language,
        order: [[2, "asc"]],
        ajax: {
            url: '/administrator/admsystem/errorlog',
            type: 'post',
            data: function (d) {
                //d.ModuleCode = ""
            }
        },
        columns: [
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: 'DateString',
                className: 'ctn-center',
                width: '150px'
            },
            {
                data: 'FileName'
            },
            {
                data: 'MethodName',
                width: '120px'
            },
            {
                data: 'Message',
            },
            {
                orderable: false,
                width: '40px',
                className: 'ctn-center',
                render: function (obj, type, data, meta) {
                    return '<a href="javascript:;"  title="Chi tiết" onclick="detailItem(\'' + data.ID + '\');" class="mg-lr-2"><i class="fa fa-eye" aria-hidden="true"></i></a>';
                    
                    return str;
                }
            }
        ]
    });
    
});

function detailItem(id) {
    $.ajax({
        url: '/administrator/admsystem/detailerrolog',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ id: id }),
        success: function (response) {
            console.log(response);
            $('#date').html(response.DateString);
            $('#fileName').html(response.FileName);
            $('#methodName').html(response.MethodName);
            $('#innerException').html(response.InnerException);
            $('#stacktrack').html(response.StackTrace);
            $('#message').html(response.Message);
            id = '';
            $('#detailModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#detailModal').modal('hide');
        }
    });
}
