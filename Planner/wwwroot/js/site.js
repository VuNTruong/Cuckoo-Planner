// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    console.log("Called");
    $.ajax({
        url: 'https://localhost:5001/api/v1/WorkItem',
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (responseData) {
            for (i = 0; i < responseData.data.length; i++) {
                $('.list-of-work-item').append(`
                <div class="work-item">
                    <p class="title">${responseData.data[i].title}</p>
                    <p class="detail">${responseData.data[i].content}</p>
                    <div class="options">
                        <button class="button">Remove</a>
                        <button class="button">Mark as done</button>
                    </div>
                </div>
            `)
            }
        }
    });
})();

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

    // Use Ajax to create new post in the
    $.ajax({
        url: 'https://localhost:5001/api/v1/WorkItem',
        type: 'POST',
        data: JSON.stringify({
            "title": newTitle,
            "content": newContent,
            "dateCreated": dateString
        }),
        dataType: 'json',
        cache: false,
        success: function (responseData) {
            // New work item is created at this point, add a new post to current list
            $('.list-of-work-item').append(`
                <div class="work-item">
                    <p class="title">${responseData.data.title}</p>
                    <p class="detail">${responseData.data.content}</p>
                    <div class="options">
                        <button class="button">Remove</a>
                        <button class="button">Mark as done</button>
                    </div>
                </div>
            `)
        }
    })
}