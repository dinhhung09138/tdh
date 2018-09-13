var fbID = '';
var fbToken = '';
//-----------------------
// Check satus callback
// This is called with the results from from FB.getLoginStatus().
//-----------------------
function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        testAPI();
    } else {
        // The person is not logged into your app or we are unable to tell.
        document.getElementById('status').innerHTML = 'Please log ' +
            'into this app.';
    }
}

//-----------------------
// Check facebook login state
// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
//-----------------------
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

//-----------------------
// Login to facebook
//-----------------------
function Login() {
    FB.login(function (response) {
        if (response.status === 'connected') {
            // Logged into your app and Facebook.
            //console.log(response);
            fbID = response.authResponse.userID;
            fbToken = response.authResponse.accessToken;
            //console.log(response.authResponse.accessToken);
            longToken(clientID, clientSecrect, response.authResponse.accessToken);
            $('#fbLogin').modal('hide');
            //setFacebookUserInfo(response.authResponse.userID, '', response.authResponse.accessToken, response.authResponse.expiresIn);

        } else {
            // The person is not logged into this app or we are unable to tell.
        }
    });
}

//-----------------------
// Get facebook longtime token
//-----------------------
function longToken(clientID, clientSecrect, token) {
    FB.api('/oauth/access_token?grant_type=fb_exchange_token&client_id=' + clientID + '&client_secret=' + clientSecrect + '&fb_exchange_token=' + token, function (response) {
        //console.log(response);
    });
}

//-----------------------
// Logout to facebook
//-----------------------
function Logout() {
    FB.logout(function (response) {
        // Person is now logged out
    });
}

//-----------------------
// get fanpage infor
//-----------------------
function fanpagInfo(fanpageId, token) {
    var fields = 'name,username,link,picture,cover,category,about,impressum,mission';
    fields += ',description_html,general_info,contact_address,single_line_address';
    fields += ',emails,phone,website,hours';
    fields += ',fan_count,rating_count,ratings';
    FB.api(
        "/" + fanpageId,
        'GET',
        {
            fields: fields,
            access_token: token,
        },
        function (response) {
            console.log(response);
            if (response && !response.error) {
                console.log(response);
            }
        });
}

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
//-----------------------
// Get list of fanpage by user id
// To get list of page, we need "manage_pages" permision
//-----------------------
function getFanpageData(userUID, userToken, _callback) {
    FB.api(
        '/' + userUID,
        'GET',
        {
            fields: 'accounts{id,name,access_token}',
            access_token: userToken
        },
        _callback
    );
}

