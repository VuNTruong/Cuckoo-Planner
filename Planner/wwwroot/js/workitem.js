// The function to open the update task menu
function openUpdateTaskMenu(item_id_to_update) {
    // Append update task menu into the Post view
    $('.update-task-area').append(`
        <div class="update-backdrop" id="update-backdrop" onclick="closeUpdateTaskMenu()"></div>
        <div class="update-menu" id="update-menu">
            <form class="update-form" action="OpenUpdateTaskMenu">
                <h2>
                    Update task
                </h2>
                <label for="title">Title:</label><br>
                <input type="text" id="title_update_field" name="title" class="field"><br>

                <label for="content">Content:</label><br>
                <input type="text" id="content_update_field" name="content" class="field"><br><br>

                <input type="submit" value="Submit" class="button">
            </form>
        </div>
    `);
}

// The function to show work item entry alert
function workItemEntryAlertShow() {
    // Append work item entry alert into the view
    $('.update-task-area').append(`
        <div class="update-backdrop" id="update-backdrop" onclick="closeUpdateTaskMenu()"></div>
        <div class="update-menu" id="update-menu">
            <p>Please check that you've filled out both fields</p>
        </div>
    `)
}

// The function to close the update task menu
function closeUpdateTaskMenu() {
    // Remove the update backdrop and update menu from the view
    $("div").remove('#update-backdrop');
    $("div").remove('#update-menu');
}

// The function to delete a work item from the database
function onDelete(work_item_id) {
    // Use Ajax to start deleting task from the database
    $.ajax({
        url: `https://localhost:5001/api/v1/WorkItem?workItemId=${work_item_id}`,
        type: 'DELETE',
        cache: false,
        data: JSON.stringify({
            "Title": "delete",
            "Content": "delete",
            "DateCreated": "delete"
        }),
        contentType: "application/json",
        success: function (responseData) {
            // At this point, task is deleted in the database, we will now need to remove it from
            // current list of tasks (work item)
            $("div").remove(`#${work_item_id}`);
        }
    })
}

// The function to add new work item
function onAdd() {
    // Get value of the new title
    const newTitle = document.getElementById("title_text_field").value;

    // Get value of the new content
    const newContent = document.getElementById("content_text_field").value;

    // Get current date
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;
    const dateString = today.toString();

    // Use Ajax to create new work item in the database
    $.ajax({
        url: 'https://localhost:5001/api/v1/WorkItem',
        type: 'POST',
        data: JSON.stringify({
            "Title": newTitle,
            "Content": newContent,
            "DateCreated": dateString
        }),
        contentType: "application/json",
        dataType: 'json',
        cache: false,
        success: function (responseData) {
            // New work item is created at this point, add a new post to current list
            $('.list-of-work-item').append(`
                <div class="work-item">
                    <p class="title">${responseData.data.title}</p>
                    <p class="detail">${responseData.data.content}</p>
                    <div class="options">
                        <button class="button" id="${responseData.data.id}" onclick="onDelete(event.target.id)">Remove</a>
                            <button class="button" id="${responseData.data.id}" onclick="openUpdateTaskMenu(event.target.id)">Update</button>
                    </div>
                </div>
            `)
        },
        error: function (responseData) {
            console.log("There seem to be an error");

            // Call the function to show the alert
            workItemEntryAlertShow();
        }
    })
}

// The function to update current record in the database
function onUpdate(work_item_id) {
    // Get value of the new title
    const newTitle = document.getElementById("title_update_field").value;

    // Get value of the new content
    const newContent = document.getElementById("content_update_field").value;

    // Use Ajax to start updating task in the database
    $.ajax({
        url: `https://localhost:5001/api/v1/WorkItem?workItemId=${work_item_id}`,
        type: 'PATCH',
        dataType: "json",
        cache: false,
        data: JSON.stringify({
            "Title": newTitle,
            "Content": newContent
        }),
        contentType: "application/json",
        success: function (responseData) {
            // Update title of the work item
            $(`title-${work_item_id}`).text(responseData.data.title)

            // Update content of the work item
            $(`content-${work_item_id}`).text(responseData.data.content)
        }
    })
}

// The function to send password reset email to user with specified email address
function sendPasswordResetEmailToUserWithId(userId) {
    // Use Ajax to send password reset email to the user
    $.ajax({
        url: "https://localhost:5001/api/v1/auth/sendPasswordResetEmailBasedOnId",
        type: "POST",
        data: JSON.stringify({
            "userId": userId
        }),
        contentType: "application/json",
        dataType: "json",
        cache: false,
        success: function (responseData) { }
    })
}