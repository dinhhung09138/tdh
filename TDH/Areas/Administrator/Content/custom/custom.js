var language = {
    lengthMenu: 'Hiển thị _MENU_',
    zeroRecords: 'Dữ liệu không tồn tại',
    info: 'Trang _PAGE_/_PAGES_',
    infoEmpty: 'Không tìm thấy kết quả',
    infoFiltered: '(Tìm kiếm trên _MAX_ dòng)',
    search: 'Tìm kiếm',
    processing: 'Đang xử lý',
    paginate: {
        first: '<<',
        previous: '<',
        next: '>',
        last: '>>'
    }
};
/**
 * l: length changing input control
 * f: filtering input
 * t: The table
 * i: Table informatin summary
 * p: pagination control
 * r: Processing display element
 * < and > - div element
 * <"class" and > - div with a class
 * <"#id" and > - div with an ID
 * <"#id.class" and > - div with an ID and a class
 * https://datatables.net/reference/option/dom
 */
var dom = '<"top"<"row"<"col-md-3 col-sm-4 col-xs-12"l><"col-md-6 col-sm-4 col-xs-12"<"toolbar">><"col-md-3 col-sm-4 col-xs-12 text-right"f>>>rt<"bottom"<"row"<"col-md-5 col-sm-6 col-xs-12"i><"col-md-7 col-sm-6 col-xs-12"p>>><"clear">';

$(document).ready(function () {
    //------------------------------------------------------
    //Set modal center screen
    //-----------------
    var modalVerticalCenterClass = ".modal";
    function centerModals($element) {
        var $modals;
        if ($element.length) {
            $modals = $element;
        } else {
            $modals = $(modalVerticalCenterClass + ':visible');
        }
        $modals.each(function (i) {
            var $clone = $(this).clone().css('display', 'block').appendTo('body');
            var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
            top = top > 0 ? top : 0;
            $clone.remove();
            $(this).find('.modal-content').css("margin-top", top);
        });
    }
    $(modalVerticalCenterClass).on('show.bs.modal', function (e) {
        centerModals($(this));
    });
    $(window).on('resize', centerModals);
    //Show second modal above first modal
    $(document).on('show.bs.modal', '.modal', function (event) {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
    });
    //Prevent scrollbar visible when show second modal
    $(document).on('hidden.bs.modal', '.modal', function (event) {
        var count = $('.modal:visible').length;
        if (count > 0) {
            $('body').addClass('modal-open');
        }
    });

    $("*").dblclick(function (e) {
        e.preventDefault();
    });

    if ($('#msg').val().length > 0) {
        notification($('#msg').val(), $('#msgT').val());
        //$('#msg').val('');
        //$('#msgT').val('');
    }

});


function formatMoney(number, decimals, decPoint, thousandsSep) {
    decimals = isNaN(decimals = Math.abs(decimals)) ? 2 : decimals;
    decPoint = decPoint === undefined ? "." : decPoint;
    thousandsSep = thousandsSep === undefined ? "," : thousandsSep;
    s = number < 0 ? "-" : "";
    i = String(parseInt(number = Math.abs(Number(number) || 0).toFixed(decimals)));
    j = (j = i.length) > 3 ? j % 3 : 0;

    return s + (j ? i.substr(0, j) + thousandsSep : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousandsSep) + (decimals ? decPoint + Math.abs(number - i).toFixed(decimals).slice(2) : "");
}

function notification(message, type) {
    console.log(message);
    console.log(type);
    if (message === 'undefined' | message.length === 0) {
        return;
    }
    if (type.toLowerCase() === "error") {
        Lobibox.notify('error', {
            sound: false,
            delay: false,
            size: 'mini',
            icon: false,
            position: 'bottom right',
            msg: message
        });
        return;
    }
    if (type.toLowerCase() === "warning") {
        Lobibox.notify('warning', {
            sound: false,
            delay: false,
            size: 'mini',
            icon: false,
            position: 'bottom right',
            msg: message
        });
        return;
    }
    if (type.toLowerCase() === "success") {
        Lobibox.notify('success', {
            sound: false,
            delay: 3000,
            size: 'mini',
            icon: false,
            position: 'bottom right',
            msg: message
        });
        return;
    }
}

function loadPage(url, title) {
    loading($('body'), 'show');
    $('#main_layout').load(url, function (response, status, xhr) { loading($('body'), 'hide'); });
    //setTimeout(function () { loading($('.content-wrapper'), 'hide') }, 700);
}

function loading(el, type) {
    if (type === 'show') {
        el.waitMe({
            effect: 'facebook',
            text: '',
            maxSize: '',
            waitTime: -1,
            source: 'img.svg',
            textPos: 'vertical',
            fontSize: '',
            onClose: function (el) { }
        });
    }
    else {
        el.waitMe('hide');
    }
}

$(document).on('click', '.pg_ld', function (e) {
    var url = $(this).attr('data-url');
    var title = '';//$(this).attr('data-title');
    loadPage(url, title);
});
$('#main_layout').load(function () {
    loading($('body'), 'hide');
});