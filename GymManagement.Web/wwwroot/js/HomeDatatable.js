

$(document).ready(function () {
    $("#dtAllUsers").DataTable({
        "processing": true, // for show progress bar    
        "serverSide": true, // for process server side    
        "filter": true, // this is for disable filter (search box)    
        "orderMulti": false, // for disable multiple column at once    
        "ajax": {
            "url": "/Home/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            //"visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "registrationNumber", "name": "RegistrationNumber", },
            { "data": "name", "name": "Name" },
            { "data": "mobile", "name": "Mobile" },
            { "data": "package", "name": "CurrentPackage" },
            { "data": "balanceAmt", "name": "BalanceAmount" },
            { "data": "startDate", "name": "StartDate" },
            { "data": "endDate", "name": "EndDate" },
            //{
            //    "render": function (data, type, full, meta) { return '<a href="/Attendance/ViewUserAttendance/' + full.RegistrationNumber + '">Attendace</a> | <a href="#">Edit</a> | <a href="#">Delete</a>'; }
            //},
            {
                //data: null,
                render: function (data, type, row) {
                    return '<a href="/Attendance/ViewUserAttendance?regNumber=' + row.registrationNumber + '">Attendace</a> | <a href="#">Edit</a> | <a href="#">Delete</a>';
                }
            },
        ]
    });
});


//function DeleteData(CustomerID) {
//    if (confirm("Are you sure you want to delete ...?")) {
//        Delete(CustomerID);
//    } else {
//        return false;
//    }
//}


//function Delete(CustomerID) {
//    var url = '@Url.Content("~/")' + "DemoGrid/Delete";

//    $.post(url, { ID: CustomerID }, function (data) {
//        if (data) {
//            oTable = $('#example').DataTable();
//            oTable.draw();
//        } else {
//            alert("Something Went Wrong!");
//        }
//    });
//}  