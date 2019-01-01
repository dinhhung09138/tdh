'use trick'
var Facebook = {
    //-----------------------
    // Facebook user info model
    //-----------------------
    user: {
        UID: '',
        Name: '',
        Mobile: '',
        Email: '',
        AuthToken: '',
        StartOn: new Date(),
        LastExecute: new Date(),
        ExpiresOn: new Date()
    },
    //-----------------------
    // Facebook fanpage info model
    //-----------------------
    fanpage: {
        UID: '',
        Link: '',
        UserName: '',
        DisplayName: '',
        Phone: '',
        Email: '',
        Website: '',
        AuthToken: '',
        StartOn: new Date(),
        LastExecute: new Date(),
        ExpiresOn: new Date()
    },
    //-----------------------
    // Facebook group info model
    //-----------------------
    group: {
        UID: '',
        Link: '',
        Name: ''
    },
    //-----------------------
    // Check facebook login state
    // This function is called when someone finishes with the Login
    // Button.  See the onlogin handler attached to it in the sample
    // code below.
    //-----------------------
    checkLoginState: function () {
        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });
    },
    //-----------------------
    // Check satus callback
    // This is called with the results from from FB.getLoginStatus().
    //-----------------------
    statusChangeCallback: function (response) {
        // The response object is returned with a status field that lets the
        // app know the current login status of the person.
        // Full docs on the response object can be found in the documentation
        // for FB.getLoginStatus().
        if (response.status === 'connected') {
            // Logged into your app and Facebook.
            // testAPI();
        } else {
            // The person is not logged into your app or we are unable to tell.
            $('.fbLoginMessage').html('Bạn chưa đăng nhập. Vui lòng đăng nhập để tiếp tục');
        }
    },
    //-----------------------
    // Login to facebook
    //-----------------------
    login: function () {
        FB.login(function (response) {
            if (response.status === 'connected') {
                // Logged into your app and Facebook.
                console.log(response);
                this.user.UID = response.authResponse.userID;
                this.user.AuthToken = response.authResponse.accessToken;
                this.user.UID = response.authResponse.userID;
                //console.log(response.authResponse.accessToken);
                var token = this.longToken(clientID, clientSecrect, response.authResponse.accessToken);
                console.log(token);
                this.user.AuthToken = new Date();
                this.user.LastExecute = new Date();
                this.user.ExpiresOn = new Date();
                saveuser(this.user);
                $('#fbLogin').modal('hide');
                //setFacebookUserInfo(response.authResponse.userID, '', response.authResponse.accessToken, response.authResponse.expiresIn);

            } else {
                $('.fbLoginMessage').empty();
                $('.fbLoginMessage').html('Không thể kết nối đến facebook');
            }
        });
    },
    //-----------------------
    // Logout to facebook
    //-----------------------
    logout: function () {
        FB.logout(function (response) {
            renderLoginFBControl();
        });
    },
    //-----------------------
    // Get facebook longtime token
    //-----------------------
    longToken: function (clientID, clientSecrect, token) {
        FB.api('/oauth/access_token?grant_type=fb_exchange_token&client_id=' + clientID + '&client_secret=' + clientSecrect + '&fb_exchange_token=' + token, function (response) {
            console.log(response);
            return response;
        });
    },
    //-----------------------
    // get user infor
    //-----------------------
    getUserInfo: function (userUid, userToken) {
        var fields = 'name,username,link,picture,cover,emails,phone';
        FB.api(
            "/" + userUid,
            'GET',
            {
                fields: fields,
                access_token: userToken
            },
            function (response) {
                console.log(response);
                if (response && !response.error) {
                    console.log(response);
                } else {
                    console.log(response);
                }
            });
    },
    //-----------------------
    // get fanpage infor
    //-----------------------
    getFanpagInfo: function (fanpageUid, pageToken) {
        var fields = 'name,username,link,picture,cover,emails,phone,website';
        FB.api(
            "/" + fanpageUid,
            'GET',
            {
                fields: fields,
                access_token: pageToken
            },
            function (response) {
                console.log(response);
                if (response && !response.error) {
                    console.log(response);
                } else {
                    console.log(response);
                }
            });
    },
    //-----------------------
    // Get list of fanpage by user id
    // To get list of page, we need "manage_pages" permision
    //-----------------------
    getFanpageListDataByUser: function (userUid, userToken) {
        FB.api(
            '/' + userUid,
            'GET',
            {
                fields: 'accounts{id,name,access_token}',
                access_token: userToken
            },
            function (response) {
                console.log(response);
                if (response && !response.error) {
                    console.log(response);
                } else {
                    console.log(response);
                }
                var pages = response.accounts.data;
                var dRow = '';
                $.each(pages, function (idx, item) {
                    dRow += '<li class="list-group-item">';
                    dRow += '	<div class="form-check">';
                    dRow += '		<input type="checkbox" class="form-check-input selectFanpage" data-id="' + item.id + '" data-name="' + item.name + '" data-token="' + item.access_token + '">';
                    dRow += '		<label class="form-check-label">' + item.name + '</label>';
                    dRow += '	</div>';
                    dRow += '</li>';
                });
                $('#listFanpage').empty();
                $('#listFanpage').append(dRow);
            }
        );
    },
    //-----------------------
    // Get list of group by user id
    // To get list of group, we need "manage_group" permision
    //-----------------------
    getGroupListDataByUser: function (userUid, userToken) {
        FB.api(
            '/' + userUid,
            'GET',
            {
                fields: 'group{id,name,link}',
                access_token: userToken
            },
            function (response) {
                console.log(response);
                if (response && !response.error) {
                    console.log(response);
                } else {
                    console.log(response);
                }
            }
        );
    }
};












//-----------------------
// Get list of fanpage by user id
// To get list of page, we need "manage_pages" permision
//-----------------------
function getFanpageData(userUID, userToken) {
    FB.api(
        '/' + userUID,
        'GET',
        {
            fields: 'accounts{id,name,access_token}',
            access_token: userToken
        },
        function (response) {
            console.log(response);
            var pages = response.accounts.data;
            var dRow = '';
            $.each(pages, function (idx, item) {
                dRow += '<li class="list-group-item">';
                dRow += '	<div class="form-check">';
                dRow += '		<input type="checkbox" class="form-check-input selectFanpage" data-id="' + item.id + '" data-name="' + item.name + '" data-token="' + item.access_token + '">';
                dRow += '		<label class="form-check-label">' + item.name + '</label>';
                dRow += '	</div>';
                dRow += '</li>';
            });
            $('#listFanpage').empty();
            $('#listFanpage').append(dRow);
        }
    );
}

