// The function to send password reset token to user with specified email address
function sendPasswordResetTokenToUserWithEmail(event, emailParam) {
    // Prevent any default
    event.preventDefault();

    // Get value of the email field
    const email = document.getElementById("email_password_reset_field").value;

    // Use Ajax to perform the call and send password reset token to a user
    $.ajax({
        url: "https://localhost:5001/api/v1/Auth/sendPasswordResetEmail",
        type: "POST",
        dataType: "json",
        cache: false,
        data: JSON.stringify({
            userEmail: email
        }),
        success: function (responseData) {
            // Check status from the response data to see if correct email address was entered or not
            if (responseData.status == "Done") {
                // If status is "Done", take user to the view where user can enter the obtained password reset token and start resetting password
                window.location.href = `/auth/createnewpassword?userEmail=${email}`
            } // Otherwise, take user to the view where user is informed that entered email was not correct
            else {
                window.location.href = "/auth/incorrectemailresetpassword"
            }
        }
    })
}

// The function to send password reset token to user with specified user id
function sendPasswordResetTokenToUserWithId(userId) {
    // Use Ajax to perform the call and send password reset token to the user
    $.ajax({
        ur: "https://localhost:5001/api/v1/Auth/sendPasswordResetEmailBasedOnId",
        type: "POST",
        dataType: "json",
        cache: false,
        data: JSON.stringify({
            "userId": userId
        }),
        success: function (responseData) {
            console.log(responseData);
        },
        error: function (responseData) {
            console.log(responseData);
        }
    })
}

// The function to reset password for the user with specified email address and password reset token
function resetUserPasswordBasedOnTokenAndEmail(event) {
    // Prevent any default
    event.preventDefault();

    // Get value of the new password field
    const newPassword = document.getElementById("new_password_password_reset_field").value;

    // Get value of the new password confirm field
    const newPasswordConfirm = document.getElementById("confirm_new_password_password_reset_field").value;

    // Get value of the password reset token
    const passwordResetToken = document.getElementById("token_password_reset_field").value;

    // Get value of email of the user that needs to get password reset
    const userEmail = document.getElementById("user_email_for_password_reset").textContent;

    // Use Ajax to perform the call and reset password for the user
    $.ajax({
        url: "https://localhost:5001/api/v1/Auth/resetPassword",
        type: "POST",
        dataType: "json",
        cache: false,
        data: JSON.stringify({
            userEmail: userEmail,
            passwordResetToken: passwordResetToken,
            newPassword: newPassword,
            newPasswordConfirm: newPasswordConfirm
        }),
        success: function (responseData) {
            // if the function executed successfully, user has done with resetting password
            // take user to the view where user is informed that password has been reset
            window.location.href ="/auth/passwordresetdone"
        },
        error: function (responseData) {
            // If the function did not execute successfully, there is something wrong
            console.log(responseData);
        }
    })
}

// The function to sign the user in
function signIn(event) {
    // Prevent any default
    event.preventDefault();

    // Get entered email
    const email = document.getElementById("email_login_field").value;

    // Get entered password
    const password = document.getElementById("password_login_field").value;

    // Use Ajax to perform login procedure
    $.ajax({
        url: "https://localhost:5001/api/v1/Auth/signIn",
        type: "POST",
        cache: false,
        data: JSON.stringify({
            "email": email,
            "password": password
        }),
        success: function (responseData) {
            // Go to the main page
            window.location.href = "/dashboard/main"
        }
    })
}

// The function to sign the user up
function signUp(event) {
    // Prevent any default
    event.preventDefault();

    // Get entered full name
    const fullName = document.getElementById("fullname_signup_field").value;

    // Get entered email
    const email = document.getElementById("email_signup_field").value;

    // Get entered password
    const password = document.getElementById("password_signup_field").value;

    // Get entered password confirm
    const passwordConfirm = document.getElementById("password_confirm_signup_field").value;

    // Use Ajax to perform sign up procedure
    $.ajax({
        url: "https://localhost:5001/api/v1/Auth/signUp",
        type: "POST",
        cache: false,
        data: JSON.stringify({
            "email": email,
            "password": password,
            "passwordConfirm": passwordConfirm,
            "fullName": fullName
        }),
        success: function (responseData) {
            // Take user to the view where user is informed that account has been created
            window.location.href = "/auth/signupdone"
        }
    })
}

// The function to sign the user out
function signOut(event) {
    // Prevent any default
    event.preventDefault();

    // Use Ajax to perform sign out procedure
    $.ajax({
        url: "https://localhost:5001/api/v1/Auth/signOut",
        type: "POST",
        cache: false,
        success: function (responseData) {
            // Go to the welcome page
            window.location.href = "/"
        },
        contentType: "application/json"
    })
}

// The function to get info of the currently logged in user
function getInfoOfCurrentUser() {
    // Use Ajax to get info of the currently logged in user
}

// The function to reset password for the currently logged in user
function resetPasswordForCurrentUser () {

}