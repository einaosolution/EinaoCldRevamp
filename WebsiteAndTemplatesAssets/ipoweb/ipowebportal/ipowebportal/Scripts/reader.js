



$(document).ready(function () {
  
    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";

});





$("#submitClose").click(function (e) {

    document.getElementById("terms").style.display = "none";
});




$(document).ready(function () {

    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";

    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";





    $("#divLogOut").click(function (e) {

        // kill all the session and sign out 
        document.getElementById("successful").style.display = "none";
        document.getElementById("fail").style.display = "none";


        window.localStorage.clear();
        document.getElementById("divRegister").style.display = "block";
        document.getElementById("divLogOut").style.display = "none";
        document.getElementById("divLogin").style.display = "block";
        document.getElementById("AppPortal").style.display = "none";
    });

});

$(document).ready(function () {

    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";





    $("#divAppPortal").click(function (e) {

        // kill all the session and sign out 
        var url = apiUrl + "home";
        setTimeout(function () { window.location = url; }, 2000);

    });

});

$(document).ready(function () {

    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";





    $("#submitButton").click(function (e) {

        e.preventDefault();
        if ($("#FirstName").val() == "") {
            $("#FirstNameSpan").text("Please enter your first name");

        }
        else {
            $("#FirstNameSpan").text("");
        }
        if ($("#LastName").val() == "")
            $("#LastNameSpan").text("Please enter your last name");
        else
            $("#LastNameSpan").text("");


        if ($("#Email").val() == "") {
            $("#EmailSpan").text("Please enter your Email Address");
        }
        else {
            $("#EmailSpan").text("");
        }
     
        if ($("#CategoryType").val() == "")
        {
           
            $("#CategorySpan").text("Please select category type");
        }
        else {
            $("#CategorySpan").text("");
        }

        if (($("#Email").val() !== "") && ($("#FirstName").val() !== "") && ($("#LastName").val() !== "") && ($("#CategoryType").val() !== "")) {

            pageLoader.show();
            debugger
            $.ajax({
                type: "POST",
                url: APIBaseURL + "UserManagement/EmailVerification",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ "email": $("#Email").val(), "first_Name": $("#FirstName").val(), "last_Name": $("#LastName").val(), "category": $("#CategoryType").val() }),
                dataType: "json",
                success: function (response, status, xhr) {
                  
                    if (response.statusCode == "200") {
                        $("#messageSpan").text("Registration successful, kindly check your email for your activation link");


                        document.getElementById("successful").style.display = "block";
                        closeAlert();

                    }
                    else
                        document.getElementById("fail").style.display = "block";
                    $("#messageSpanError").text("Registration not successful, please try again");
                    closeAlert();

                },
                error: function (xhr, status, error) {
                    document.getElementById("fail").style.display = "block";
                    if (error == "Found") {
                        $("#messageSpanError").text("An account for the specified email address already exists. Try another email address.");
                    }
                    else {
                        $("#messageSpanError").text("Registration not successful, please try again");
                    }
                    closeAlert();
                }
            });
        }
        
    });

});



$(document).ready(function () {

    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";



    $("#btnForgetPassword").click(function (e) {
        e.preventDefault();
        pageLoader.show();
        debugger;
        if ($("#ForgetPasswordEmail").val() == "") {
            $("#ForgetPasswordSpan").text("Please enter your email address");

        }
        else {
            $("#ForgetPasswordSpan").text("");
        }


        if (($("#ForgetPasswordEmail").val() !== ""))

            pageLoader.show();

            $.ajax({
                type: "POST",
                url: APIBaseURL + "UserManagement/forgotPassword",

                contentType: "application/json; charset=utf-8",

                data: JSON.stringify({ "username": $("#ForgetPasswordEmail").val() }),

                dataType: "json",
                success: function (response, status, xhr) {

                    if (response.statusCode == "200") {

                        
                        $("#messageSpan").text(response.message);
                        document.getElementById("successful").style.display = "block";
                        closeAlert();
                    }
                    else {

                        $("#ForgetPasswordEmail").val();

                        document.getElementById("fail").style.display = "block";
                        $("#messageSpanError").text(response.message);
                        closeAlert();
                    }
                },
                error: function (xhr, status, error) {


                    document.getElementById("successful").style.display = "block";


                    $("#messageSpan").text("Forget password operation completed. Kindly check the email address provided");
                    closeAlert();
                }
            });
    });

});



$(document).ready(function () {

    document.getElementById("successful").style.display = "none";
    document.getElementById("fail").style.display = "none";



    $("#submitLogin").click(function (e) {
        e.preventDefault();

        if ($("#UserName").val() == "") {
            $("#UserNameSpan").text("Please enter your username");

        }
        else {
            $("#UserNameSpan").text("");
        }
        if ($("#Password").val() == "")
            $("#PasswordSpan").text("Please enter you password");
        else
            $("#PasswordSpan").text("");




        if (($("#UserName").val() !== "") && ($("#Password").val() !== ""))

            var x = document.getElementById("successful");
        var f = document.getElementById("fail");
        f.style.display = "none";
        x.style.display = "none";

        pageLoader.show();
        $.ajax({
            type: "POST",
            url: APIBaseURL + "UserManagement/Authenticate",

            contentType: "application/json; charset=utf-8",

            data: JSON.stringify({ "username": $("#UserName").val(), "password": $("#Password").val(), "rememberMe": true }),

            dataType: "json",
            success: function (response, status, xhr) {

                if (response.statusCode == "200") {

                    localStorage.clear();

                    // add to local storage
                    var navigateByUrl = "";

                    if (response.content.category == "1" && response.content.registrationcomplete == false) {
                        localStorage.setItem('username2', $("#UserName").val());
                        localStorage.setItem('ChangePassword', "False");

                        navigateByUrl = apiUrl + "Individual";


                    }
                    else if (response.content.category == "2" && response.content.registrationcomplete == false) {
                        localStorage.setItem('username2', $("#UserName").val());
                        localStorage.setItem('ChangePassword', "False");

                        navigateByUrl = apiUrl + "Corporate";
                    }


                    else if (response.content.changepassword == false) {

                        localStorage.setItem('username', response.content.username);
                        localStorage.setItem('loggeduser', response.content.username);
                        localStorage.setItem('UserId', response.content.userId);


                        localStorage.setItem('access_tokenexpire', response.content.token);
                        localStorage.setItem('Roles', JSON.stringify(response.content.dynamicMenu));

                        localStorage.setItem('ExpiryTime', response.content.expiryTime);
                        localStorage.setItem('Roleid', response.content.roleId);

                        if (response.content.profilepic == null) {
                            localStorage.setItem('profilepic', "");
                        }

                        else {
                            localStorage.setItem('profilepic', response.content.profilepic);

                        }

                        localStorage.setItem('ChangePassword', "False");

                        navigateByUrl = apiUrl + "PasswordChange";
                    }

                    else {                            // last part

                        localStorage.setItem('username', response.content.username);

                        localStorage.setItem('access_tokenexpire', response.content.token);
                        localStorage.setItem('UserId', response.content.userId);
                        localStorage.setItem('ExpiryTime', response.content.expiryTime);
                        localStorage.setItem('Roleid', response.content.roleId);

                        localStorage.setItem('loggeduser', response.content.loggeduser);
                        localStorage.setItem('Roles', JSON.stringify(response.content.dynamicMenu));
                        if (response.content.profilepic == null) {
                            localStorage.setItem('profilepic', "");
                        }

                        else {
                            localStorage.setItem('profilepic', response.content.profilepic);
                        }
                       

                      //  registerapi.settoken(response.content.token);

                        //  localStorage.settoken(response.content.token);
                        localStorage.setItem('ChangePassword', "True");
                        localStorage.setItem('lastpasswordchange', response.content.lastpasswordchange);

                        localStorage.setItem('token', response.content.token);
                        localStorage.setItem('access_tokenexpire', response.content.token);



                        navigateByUrl = apiUrl + "home";
                        console.log(localStorage);
                    }

                    $("#messageSpan").text("You will be redirected to your page soon");
                    document.getElementById("successful").style.display = "block";

                    closeAlert();
                   
                    setTimeout(function () { window.location = navigateByUrl; }, 2000);
                }
                else {
                    document.getElementById("fail").style.display = "block";

                    $("#messageSpanError").text("Login failed, Please try again.");
                    closeAlert();
                }
            },
            error: function (xhr, status, error) {


                document.getElementById("fail").style.display = "block";

                $("#messageSpanError").text("Login failed, Please try again.");
                closeAlert();
            }
        });
    });

});

function checkLoginDetails() {

    document.getElementById("divRegister").style.display = "none";
    document.getElementById("divLogOut").style.display = "none";
    document.getElementById("divLogin").style.display = "none";
    document.getElementById("divAppPortal").style.display = "none";
    if (localStorage.getItem("username") == null || localStorage.getItem("username") == "") {

        document.getElementById("divRegister").style.display = "block";
        document.getElementById("divLogin").style.display = "block";

        pageLoader.hide();
    }

    else {
       
        document.getElementById("divLogOut").style.display = "block";
        document.getElementById("divAppPortal").style.display = "block";
        pageLoader.hide();
    }


}

$(window).load(function () {
    pageLoader.hide();
});


function clearTextMessages() {

    $('#UserName').val("");
    $("#Password").val("");
    $("#ForgetPasswordEmail").val("");
    $("#Email").val("");
    $("#FirstName").val("");
    $("#LastName").val("");
}

function closeAlert() {

    $("#successful").hide(10000);
    $("#fail").hide(10000);

    clearTextMessages();
    pageLoader.hide();

}
checkLoginDetails();
