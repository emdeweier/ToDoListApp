// Login
function uLogin() {
    debugger;
    var login = new Object();
    login.UserName = $("#username").val();
    login.Password = $("#password").val();
    if ($("#username").val().trim() == "") {
        $("#login-alert").removeAttr('hidden');
        $("#login-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#login-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=uClearAlert();>&times;</button>Please fill username');
    }
    else if ($("#password").val().trim() == "") {
        $("#login-alert").removeAttr('hidden');
        $("#login-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#login-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=uClearAlert();>&times;</button>Please fill password');
    }
    else {
        $.ajax({
            "url": "/Users/Login",
            "type": "POST",
            "dataType": "json",
            "data": { UserName: login.UserName, Password: login.Password }
        }).then((result) => {
            debugger;
            if (result.statusCode == 200) {
                window.location.href = '/Questions/'
            }
            else {
                $("#login-alert").removeAttr('hidden');
                $("#login-alert").attr("class", "alert alert-danger alert-dismissible");
                $("#login-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
                    'onclick=uCount();>&times;</button>Username / password invalid');
            }
        })
    }
}