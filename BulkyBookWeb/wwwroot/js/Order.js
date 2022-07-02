var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        LoadDataTable("inprocess");
    }
    else {
        if (url.includes("completed")) {
            LoadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                LoadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    LoadDataTable("approved");
                }
                else {
                    LoadDataTable("all");
                }
            }
        }
    }
   
});

function LoadDataTable(status) {
    dataTable = $('#tblDataOrder').DataTable({
        "ajax": {
            "url": "/Admin/Order/GetAll?status=" + status
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "10%" },
            { "data": "orderTotal", "width": "5%" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                <a class="btn-info btn" href="./Order/Details?orderId=${data}"><i class="bi bi-pencil-square"></i></a>
                           `
                },
                "width": "5%"
            }
        ] 
    });
}
