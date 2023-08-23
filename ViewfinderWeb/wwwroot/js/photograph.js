var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = new DataTable('#tblData',
        {
            "ajax": {
                "url": "https://localhost:44392/Admin/Photograph/GetAll", // Make sure this URL is correct
                "type": "GET", // Explicitly specify the HTTP method
                "dataType": "json" // Specify the expected data type
            },
            "columns": [
                { "data": "title", "width": "15%" },
                { "data": "author", "width": "15%" },
                { "data": "description", "width": "15%" },
                { "data": "price", "width": "15%" },
                { "data": "category.name", "width": "15%" },
                { "data": "composition.name", "width": "15%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="w-75 btn btn-group">
                                <a href="/Admin/Photograph/Upsert?id=${data}" class="btn btn-info mx-2"><i class="bi bi-pencil-square"></i></a>
                                <a onClick=Delete('/Admin/Photograph/Delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash"></i></a>
                                
                            </div>`;
                    },
                    "width": "15%"
                }
            ]
        });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url:url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success == true) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
