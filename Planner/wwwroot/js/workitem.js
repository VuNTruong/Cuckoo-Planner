// The function get all work items from the database
(function () {
    // Use Ajax to start getting tasks from the database
    $.ajax({
        url: 'https://localhost:5001/api/v1/WorkItem/getWorkItemsOfCurrentUser',
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (responseData) {
            for (i = 0; i < responseData.data.length; i++) {
                $('.list-of-work-item').append(`
                    <div class="work-item" id="${responseData.data[i].id}">
                        <p class="title" id="title-${responseData.data[i].id}">${responseData.data[i].title}</p>
                        <p class="detail" id="detail-${responseData.data[i].id}">${responseData.data[i].content}</p>
                        <div class="options">
                            <button class="button" id="${responseData.data[i].id}" onclick="onDelete(event.target.id)">Remove</a>
                            <button class="button" id="${responseData.data[i].id}" onclick="openUpdateTaskMenu(event.target.id)">Update</button>
                        </div>
                    </div>
                `)
            }
        }
    });

    getCurrentUserInfo(testFunction);
})()