// The function to open update passwor menu
function openUpdatePasswordMenu() {
    // Append update password menu into update password area
    $('.update-password-area').append(`
        <div class="update-backdrop" id="update-password-backdrop" onclick="closeUpdateTaskMenu()"></div>
        <div class="update-menu" id="update-password-menu">
            <form class="update-form" onsubmit="onUpdatePassword()">
                <h2>
                    Update password
                </h2>
                <label for="title">Old password:</label><br>
                <input type="text" id="old_password_password_update_field" name="title" class="field"><br>

                <label for="content">New password:</label><br>
                <input type="text" id="new_password_password_update_field" name="content" class="field"><br>
                
                <label for="content">New password confirm:</label><br>
                <input type="text" id="confirm_new_password_password_update_field" name="content" class="field"><br><br>

                <input type="submit" value="Submit" class="button">
            </form>
        </div>
    `)
}

// The function to close update password menu
function closeUpdatePasswordMenu() {
    // Remove the update backdrop and update menu from the view
    $("div").remove('#update-password-backdrop');
    $("div").remove('#update-password-menu');
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
            "currentPassword": oldPassword,
            "newPassword": newPassword,
            "newPasswordConfirm": newPasswordConfirm
        }),
        success: function (responseData) {
            console.log("Password has been updated");
        },
        error: function (responseData) {
            console.log(`There seem to be an error ${responseData}`);
        }
    })
}