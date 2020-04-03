// Login
function uLogin() {
    debugger;
    var login = new Object();
    login.UserName = $("#username").val();
    login.PasswordHash = $("#password").val();
    if ($("#username").val().trim() == "") {
        $("#login-alert").removeAttr('hidden');
        $("#login-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#login-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=lClearAlert();>&times;</button>Please fill username');
    }
    else if ($("#password").val().trim() == "") {
        $("#login-alert").removeAttr('hidden');
        $("#login-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#login-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=lClearAlert();>&times;</button>Please fill password');
    }
    else {
        $.ajax({
            "url": "/Users/Login",
            "type": "POST",
            "dataType": "json",
            "data": { UserName: login.UserName, PasswordHash: login.PasswordHash }
        }).then((result) => {
            debugger;
            if (result.statusCode == 200) {
                window.location.href = '/'
            }
            else {
                $("#login-alert").removeAttr('hidden');
                $("#login-alert").attr("class", "alert alert-danger alert-dismissible");
                $("#login-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
                    'onclick=lClearAlert();>&times;</button>Username / password invalid');
            }
        })
    }
}

// Clear Alert Login
function lClearAlert() {
    $("#login-alert").removeAttr('class');
    $("#login-alert").html('');
    $("#login-alert").attr('hidden');
}

var testEmail = /^([a-zA-Z0-9_.+-])+\@@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

// Register
function uRegister() {
    debugger;
    var register = new Object();
    register.UserName = $("#username").val();
    register.PasswordHash = $("#password").val();
    register.Email = $("#email").val();
    register.Name = $("#name").val();
    if ($("#username").val().trim() == "") {
        $("#register-alert").removeAttr('hidden');
        $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=rClearAlert();>&times;</button>Please fill username');
    }
    else if ($("#email").val().trim() == "") {
        $("#register-alert").removeAttr('hidden');
        $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=rClearAlert();>&times;</button>Please fill email');
    }
    else if ($("#name").val().trim() == "") {
        $("#register-alert").removeAttr('hidden');
        $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=rClearAlert();>&times;</button>Please fill name');
    }
    else if ($("#password").val().trim() == "") {
        $("#register-alert").removeAttr('hidden');
        $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
        $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
            'onclick=rClearAlert();>&times;</button>Please fill password');
    }
    else {
        $.ajax({
            "url": "/Users/Create",
            "type": "POST",
            "dataType": "json",
            "data": { UserName: register.UserName, PasswordHash: register.PasswordHash, Email: register.Email, Name: register.Name }
        }).then((result) => {
            debugger;
            if (result.statusCode == 1) {
                $("#register-alert").removeAttr('hidden');
                $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
                $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
                    'onclick=rClearAlert();>&times;</button>Username Registered');
            }
            else if (result.statusCode == 2) {
                $("#register-alert").removeAttr('hidden');
                $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
                $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
                    'onclick=rClearAlert();>&times;</button>Email Registered');
            }
            else if (result.statusCode == 3) {
                $("#register-alert").removeAttr('hidden');
                $("#register-alert").attr("class", "alert alert-danger alert-dismissible");
                $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
                    'onclick=rClearAlert();>&times;</button>Register Failed');
            }
            else {
                $("#register-alert").removeAttr('hidden');
                $("#register-alert").attr("class", "alert alert-success alert-dismissible");
                $("#register-alert").html('<button type="button" class="close" data-dismiss="alert" aria-hidden="true"' +
                    'onclick=rClearAlert();>&times;</button>Register Success');
            }
        })
    }
}

// Clear Alert Register
function rClearAlert() {
    $("#register-alert").removeAttr('class');
    $("#register-alert").html('');
    $("#register-alert").attr('hidden');
}