// The function to open update password menu
function openUpdatePasswordMenu(event) {
    // Prevent any default
    event.preventDefault();

    // Append update password menu into update password area
    $('.update-user-info-area').append(`
        <div class="update-backdrop" id="update-user-info-backdrop" onclick="closeUpdateUserInfoMenu()"></div>
        <div class="update-menu" id="update-user-info-menu">
            <form class="update-form" onsubmit="onUpdatePassword()">
                <h2>
                    Update password
                </h2>
                <label>Old password:</label><br>
                <input type="password" id="old_password_password_update_field" class="field" placeholder="Old password"><br>

                <label>New password:</label><br>
                <input type="password" id="new_password_password_update_field" class="field" placeholder="New password"><br>
                
                <label>New password confirm:</label><br>
                <input type="password" id="confirm_new_password_password_update_field" class="field" placeholder="New password confirm"><br><br>

                <input type="submit" value="Submit" class="button">
            </form>
        </div>
    `)
}

// The function to open update email menu
function openUpdateEmailMenu(event) {
    // Prevent any default
    event.preventDefault();

    // Append update email menu into update menu area
    $('.update-user-info-area').append(`
        <div class="update-backdrop" id="update-user-info-backdrop" onclick="closeUpdateUserInfoMenu()"></div>
        <div class="update-menu" id="update-user-info-menu">
            <form class="update-form" onsubmit="onUpdateEmail()">
                <h2>
                    Update email
                </h2>
                <label>New email:</label><br>
                <input type="text" id="new_email_email_update_field" class="field" placeholder="New email"><br><br>

                <input type="submit" value="Submit" class="button">
            </form>
        </div>
    `)
}

// The function to close update password menu
function closeUpdateUserInfoMenu() {
    console.log("Is closing");

    // Remove the update backdrop and update menu from the view
    $("div").remove('#update-user-info-backdrop');
    $("div").remove('#update-user-info-menu');
}

// The function to update password for the currently logged in user
function onUpdatePassword() {
    // Get value of the entered old password
    const oldPassword = document.getElementById("old_password_password_update_field").value;

    // Get value of the entered new password
    const newPassword = document.getElementById("new_password_password_update_field").value;

    // Get value of the entered new password confirm
    const newPasswordConfirm = document.getElementById("confirm_new_password_password_update_field").value;

    // Use Ajax to start updating password for the user
    $.ajax({
        url: "https://localhost:5001/api/v1/user/changePassword",
        type: "PATCH",
        dataType: "json",
        cache: false,
        data: JSON.stringify({
            "CurrentPassword": oldPassword,
            "NewPassword": newPassword,
            "NewPasswordConfirm": newPasswordConfirm
        }),
        success: function (responseData) {
            // Call the function to dismiss the menu
            closeUpdateUserInfoMenu();
        },
        error: function (responseData) {
            console.log(`There seem to be an error ${responseData}`);
        }
    })
}

// The function to update email for the currently logged in user
function onUpdateEmail() {
    // Get value of the entered new email
    const newEmail = document.getElementById("new_email_email_update_field").value;

    // Use Ajax to start updating email for the user
    $.ajax({
        url: "https://localhost:5001/api/v1/user/changeEmail",
        type: "PATCH",
        dataType: JSON.stringify({
            "NewEmail": newEmail
        }),
        success: function (responseData) {
            // Call the function to dismiss the menu
            closeUpdateUserInfoMenu();
        },
        error: function (responseData) {
            console.log(`There seem to be an error ${responseData}`);
        }
    })
}