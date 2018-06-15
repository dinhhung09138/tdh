var table;
var tableLink;
var linkID = '';

$(document).ready(function () {

    $('input[type=checkbox],input[type=radio]').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

});
$(document).on('click', '#selectImg', function () {
    var finder = new CKFinder();
    //var target = $(this).attr('data-target');
    finder.selectActionFunction = function (fileUrl, data) {
        $('#Image').val(fileUrl);
        //console.log(fileUrl);
        $('#imgImage').attr('src', fileUrl).attr('width', '100px');
        //document.getElementById(target).value = fileUrl;
    }
    finder.popup();
});

tableLink = $('#table-link').DataTable({
    processing: true,
    serverSide: true,
    searching: true,
    ordering: true,
    paging: true,
    pageLength: 10,
    pagingType: 'full_numbers',
    info: true,
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
    order: [
        [1, "desc"]
    ],
    ajax: {
        url: '/administrator/admsetting/bannerlink',
        type: "POST",
        data: function (d) {
            d.Parameter1 = $("#Link").val()
        }
    },
    columns: [
        {
            //Col: Thu Tu
            visible: true,
            width: "20px;",
            className: 'ctn-center',
            orderable: false,
            render: function (obj, type, data, meta) {
                var str = "";
                if (data.IsSelected == true) {
                    str = '<input type="radio" class="flat" name="onlySelect" value="' + data.Code + '" checked />';
                }
                else {
                    str = '<input type="radio" name="onlySelect" class="flat" value="' + data.Code + '" />';
                }
                return str;
            }
        },
        {
            data: "GroupName",
            visible: true,
            width: "100px",
            orderable: true,
            searchable: true
        },
        {
            //Col: Tie de
            data: "Title",
            visible: true,
            orderable: true,
            searchable: true
        }
    ]
});
//After re-draw function. Re-render icheck
tableLink.on('draw', function () {
    if ($("#table-link input.flat")[0]) {
        $('#table-link input.flat').iCheck({
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green',
            increaseArea: '20%'
        });

        $('#table-link input').on('ifChecked', function () {
            var checkCount = $("#table-link input[name='onlySelect']:checked").length;
            if (checkCount) {
                $("#table-link input[name='onlySelect']").iCheck('uncheck');
            }
            linkID = $(this).val();
        });

        $('#table-link input').on('ifUnchecked', function () {
            linkID = '';
        });
    }
});

$(document).on('click', '#btnLink', function () {
    linkID = '';
    tableLink.ajax.reload();
    $('#select-link-modal').modal('show');
});

function updateSelectDestinationLink() {
    $.ajax({
        url: '/administrator/admsetting/selectlinkevent',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        data: { id: linkID },
        success: function (result) {
            $('#Link').val(linkID);
            $('#aLink').attr('href', $('#host').val() + result.Url);
            $('#aLink').html(result.Title);
            $('#select-link-modal').modal('hide');
        },
        error: function (xhr) {
            console.log(xhr);
            notification.showSystemErrorMessage('<li>' + xhr.statusText + '</li>');
        }
    });
}