var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable()
{
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-25 btn-group" role="group">
                        <a href="/Admin/Product/Upsert?id=${data}"
                        class="btn btn-primary mx-1"><i class="bi bi-pen"></i> Edit</a>
                    </div>
                    <div class="w-25 btn-group" role="group">
                        <a onclick=Delete('/Admin/Product/Delete/${data}')
                        class="btn btn-danger mx-1"><i class="bi bi-trash"></i> Delete</a>
                    </div>
                                `
                }
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You can't take it back!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
            
        }
    })
}