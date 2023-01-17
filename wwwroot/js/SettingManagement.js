var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "Structure/GetAll/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nom", "width": "20%" },
            { "data": "responsable", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "role", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Books/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            <i class="fa-solid fa-pencil"></i>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/books/Delete?id='+${data})>
                            <i class="fa-sharp fa-solid fa-xmark"></i>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}
