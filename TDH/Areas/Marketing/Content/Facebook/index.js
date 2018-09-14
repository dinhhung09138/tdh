'use trick'

var uid = '';

function getListUser() {
    $.ajax({
        url: '/marketing/facebook/getuser',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            $('#user-list').empty();
            $('#user-count').html(response.length + ' tài khoản');
            $.each(response, function (idx, item) {
                var li = $('<li/>');
                li.append('<img src="#" alt="' + item.Name + '">');
                li.append('<a class="users-list-name" href="#" target="_blank">' + item.Name + '</a>');
                li.append('<span class="users-list-date">UID: ' + item.UID + '</span>');
                li.append('<span class="users-list-date" onclick="confirmDelete(\'' + item.UID + '\');">X</span>');
                $('#user-list').append(li);
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveUserInfo(user) {
    $.ajax({
        url: '/marketing/facebook/saveuser',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(user),
        success: function (response) {
            if (response !== 0) {
                //error
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function DeleteUser() {
    $.ajax({
        url: '/marketing/facebook/deleteuser',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ UID: uid }),
        success: function (response) {
            if (response === 0) {
                getListUser();
                $('#deleteModal').modal('hide');
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    uid = '';
}

function getListFanpage() {
    $.ajax({
        url: '/marketing/facebook/getfanpage',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            $.each(response, function (idx, item) {
                $('#fanpage-list').empty();
                $('#fanpage-count').html(response.length + ' trang');
                $.each(response, function (item, idx) {
                    var li = $('<li/>').attr('class', 'item');
                    var divImg = $('<div/>').attr('class', 'product-img');
                    divImg.append('<img src="#" alt="' + item.DisplayName + '" class="img-size-50">');
                    li.append(divImg);
                    var divInfo = $('<div/>').attr('class', 'product-info');
                    divInfo.append('<a href="' + item.Link + '" class="product-title" target="_blank">&#64;' + item.UserName + '</a>');
                    divInfo.append('<a href="javascript:;" class="badge badge-danger float-right p-1 m-1" onclick="confirmDelete(\'' + item.UID + '\');"><i class="fa fa-times" aria-hidden="true"></i></a>');
                    divInfo.append('<span class="product-description">' + item.DisplayName + '</span>');
                    li.append(divInfo);
                    $('#fanpage-list').append(li);
                });
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveFanpageInfo(fanpage) {
    $.ajax({
        url: '/marketing/facebook/savefanpage',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(fanpage),
        success: function (response) {
            if (response !== 0) {
                //error
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function DeleteFanpage() {
    $.ajax({
        url: '/marketing/facebook/deletefanpage',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ UID: uid }),
        success: function (response) {
            if (response === 0) {
                getListFanpage();
                $('#deleteModal').modal('hide');
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    uid = '';
}

function getListGroup() {
    $.ajax({
        url: '/marketing/facebook/getgroup',
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            $.each(response, function (idx, item) {
                $('#group-list').empty();
                $('#group-count').html(response.length + ' nhóm');
                $.each(response, function (item, idx) {
                    var li = $('<li/>').attr('item');
                    var divImg = $('<div/>').attr('class', 'product-img');
                    divImg.append('<img src="#" alt="' + item.Name + '" class="img-size-50">');
                    li.append(divImg);
                    var divInfo = $('<div/>').attr('class', 'product-info');
                    divInfo.append('<a href="' + item.Link + '" class="product-title" target="_blank">&#64;' + item.Name + '</a>');
                    divInfo.append('<a href="javascript:;" class="badge badge-danger float-right p-1 m-1" onclick="confirmDelete(\'' + item.UID + '\');"><i class="fa fa-times" aria-hidden="true"></i></a>');
                    divInfo.append('<span class="product-description"></span>');
                    li.append(divInfo);
                    $('#group-list').append(li);
                });
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function saveGroupInfo(group) {
    $.ajax({
        url: '/marketing/facebook/savegroup',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(group),
        success: function (response) {
            if (response !== 0) {
                //error
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function DeleteGroup() {
    $.ajax({
        url: '/marketing/facebook/deletegroup',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ UID: uid }),
        success: function (response) {
            if (response === 0) {
                getListGroup();
                $('#deleteModal').modal('hide');
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    uid = '';
}

function confirmDelete(deletedId) {
    uid = deletedId;
    $('#deleteModal').modal('show');
}

$('#deleteModal').on('hidden.bs.modal', function () {
    uid = '';
});

$(document).on('click', '#btnLogin', function (e) {
    Facebook.login();
});

$(document).on('click', '#btnLogout', function (e) {

});

function renderLoginFBControl() {
    $('#toolboxbar').append('<a class="btn btn-box-tool" id="btnLogin" href="javascript:;"><i class="fa fa-facebook-official"></i> Đăng nhập</a>');
}

function renderLogoutFBControl(img) {
    $('#toolboxbar').append('<img src="' + img + '" class="fb-user-icon-login" />');
    $('#toolboxbar').append('<a class="btn btn-box-tool" id="btnLogout" href="javascript:;"><i class="fa fa-facebook-official"></i> Đăng xuất</a>');
}
