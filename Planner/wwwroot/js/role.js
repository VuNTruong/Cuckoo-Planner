// The function to add new role
function onAddRole() {
    // Get value of the new role name
    const roleName = document.getElementById("new_role_name_text_field").value;

    // Get value of the new role description
    const roleDescription = document.getElementById("new_role_description_text_field").value;

    // Use Ajax to create new role in the database
    $.ajax({
        url: "https://localhost:5001/api/v1/Role/createNewRole",
        type: "POST",
        data: JSON.stringify({
            "NewRoleName": roleName,
            "NewRoleDescription": roleDescription
        }),
        contentType: "application/json",
        dataType: "json",
        cache: false,
        success: function (responseData) {
            // New role is created at this point, add a new role to current list
            $(".list-of-work-item").append(`
                <div class="work-item" id="${responseData.data.roleDetailId}">
                    <p class="title">${responseData.data.name}</p>
                    <div class="options">
                        <button class="button" id="${responseData.data.roleDetailId}">Remove</button>
                    </div>
                </div>
            `)
        }
    })
}

// The function to assign role to a user
function onAssignRole() {
    // Get user id of the user who will get assigned to a role
    const userId = document.getElementById("role_assign_user_id_text_field").value;

    // Get role id which will be assigned to the user
    const roleId = document.getElementById("role_assign_role_id_text_field").value;

    // Use Ajax to create new role assignment in the database
    $.ajax({
        url: "https://localhost:5001/api/v1/Role/addRoleToAUser",
        type: "POST",
        data: JSON.stringify({
            "RoleId": roleId,
            "UserId": userId
        }),
        contentType: "application/json",
        dataType: "json",
        cache: false,
        success: function (responseData) {
            $('.list-of-work-item').append(`
                <div class="work-item" id="">
                    <p class="title" id=""></p>
                    <p class="detail">User: ${responseData.user.fullName}</p>
                    <p class="detail">Role: ${responseData.role.roleName}</p>
                    <div class="options">
                        <button class="button" id="">Remove</button>
                    </div>
                </div>
            `)
        }
    })
}

// The function to remove role from a user
function onRoleRemove(roleDetailUserProfileIdToBeRemoved) {
    // Use Ajax to remove role from a user
    $.ajax({
        url: "https://localhost:5001/api/v1/Role/removeRoleFromAUser",
        type: "POST",
        data: JSON.stringify({
            "Id": roleDetailUserProfileIdToBeRemoved
        }),
        contentType: "application/json",
        dataType: "json",
        cache: false,
        success: function (responseData) {
            // At this point, role assignment is already removed from the database, we will now need
            // to remove it from list of roles here
            $("div").remove(`#${roleDetailUserProfileIdToBeRemoved}`);
        }
    })
}