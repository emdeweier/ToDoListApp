var tdl = null;

//$(function () {
//    var tdl = $("#tdlTable").DataTable({
//        "columnDefs": [
//            {
//                "searchable": false,
//                "orderable": false,
//                "targets": [0,6]
//            },
//            {
//                "orderable": false,
//                "targets": [1,2,3,4]
//            },
//            {
//                "visible": false,
//                "targets": [5]
//            }
//        ],
//        "order": [[5, 'asc']],
//        "initComplete": function () {
//            this.api().columns().every(function () {
//                var column = this;
//                var text = ["No.", "To Do List Name", "Date", "Completed Date", "Status", "Status", "Action"];
//                if (column.index() !== 0 && column.index() !== 6) {
//                    var select = $('<select style="width: 100%"><option value="">' + text[column.index()] + '</option></select>')
//                        .appendTo($(column.header()).empty())
//                        .on('change', function () {
//                            var val = $.fn.dataTable.util.escapeRegex(
//                                $(this).val()
//                            );
//                            column
//                                .search(val ? '^' + val + '$' : '', true, false)
//                                .draw();
//                        });
//                    column.data().unique().sort().each(function (d, j) {
//                        select.append('<option value="' + d + '">' + d + '</option>')
//                    });
//                    $('#filter' + column.index()).select2({
//                        theme: 'bootstrap4'
//                    })
//                }
//            });
//        }
//    });
//    tdl.on('order.dt search.dt', function () {
//        tdl.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
//            cell.innerHTML = i + 1 + ".";
//        });
//    }).draw();
//});

$(function () {
    tdl = $("#tdlTable").DataTable({
        "paging": true,
        "serverSide": true,
        "ajax": "/ToDoLists/Data/" + $('#status').val(),
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1 + ".";
                }
            },
            {
                data: "status",
                render: function (data, type, row) {
                    if (data) {
                        return '<center><i class="fa fa-times-circle fa-lg"></i></center>';
                    } else {
                        return '<center><a style="color:#28a745; cursor:pointer;" onclick=tdlUpdateStatus("' + row.id + '","' + row.userId + '");><i class="fa fa-check-circle fa-lg"></i></a></center>'
                    }
                }
            },
            { data: "name" },
            {
                data: "createDate",
                render: function (data) {
                    var createdate = moment(data).format('DD MMMM YYYY');
                    return createdate;
                }
            },
            {
                data: "completedDate",
                render: function (data) {
                    if (data != null) {
                        return moment(data).format('DD MMMM YYYY');
                    }
                    return "On Progress";
                }
            },
            {
                data: "status",
                render: function (data) {
                    if (data) {
                        return "Completed";
                    }
                    return "Active";
                }
            },
            {
                data: "status",
                visible: false,
                render: function (data) {
                    if (data) {
                        return "Completed";
                    }
                    return "Active";
                }
            },
            {
                data: "status",
                render: function (data, type, row) {
                    if (data) {
                        return "No Action";
                    } else {
                        return '<a style="color:#ffc107; cursor:pointer;" onclick=tdlGetById("' + row.id + '","' + row.userId +'");><i class="fa fa-edit fa-lg"></i></a>&nbsp' +
                            '<a style="color:#dc3545; cursor:pointer;" onclick=tdlDelete("' + row.id + '","' + row.userId +'");><i class="fa fa-trash fa-lg"></i></a>&nbsp'
                    }
                }
            }
        ],
        "columnDefs": [
            {
                "orderable": false,
                "targets": [0, 1, 2, 3, 4, 5, 7]
            }
        ],
        "order": [[6, 'asc']],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            debugger
            if (aData.status == 1) {
                $('td', nRow).css('background-color', '#959595');
                $('td', nRow).css('color', 'white');
            }
        }
    });
});

$('#status').change(function () {
    debugger;
    tdl.ajax.url('/ToDoLists/Data/' + $('#status').val()).load();
});

// Clear Screen Input To Do List
function tdlClearScreen() {
    document.getElementById("tdlIdText").disabled = true;
    $("#tdlIdText").val('');
    $("#tdlNameText").val('');
    $("#tdlUpdate").hide();
    $("#tdlSave").show();
}

// Validation Input
function tdlValidation() {
    if ($("#tdlNameText").val().trim() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please Fill Name'
        })
    }
    else if ($("#tdlIdText").val() == "" || $("#tdlIdText").val() == " ") {
        tdlSave();
    }
    else {
        debugger
        tdlEdit($("#tdlIdText").val());
    }
}

// Save To Do List
function tdlSave() {
    debugger;
    var todolist = new Object();
    todolist.Name = $("#tdlNameText").val();
    todolist.UserId = $("#tdlUserText").val();
    debugger;
    $.ajax({
        "url": "/ToDoLists/Create",
        "type": "POST",
        "dataType": "json",
        "data": todolist
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Your data has been saved',
                text: 'Success!'
            }).then((hasil) => {
                location.reload();
            });
            $("#tdlModal").modal("hide");
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Your data not saved',
                text: 'Failed!'
            })
        }
    })
}

// Get To Do List Id
function tdlGetById(id, userid) {
    debugger
    $.ajax({
        "url": "/ToDoLists/Get/" + id,
        "type": "GET",
        "dataType": "json",
        "data": { Id: id, userId: userid }
    }).then((result) => {
        debugger
        if (result.data != null) {
            debugger
            document.getElementById("tdlIdText").disabled = true;
            debugger
            $("#tdlIdText").val(result.data.id);
            debugger
            $("#tdlNameText").val(result.data.name);
            $("#tdlUserText").val(result.data.userId);
            debugger
            $("#tdlModal").modal("show");
            $("#tdlUpdate").show();
            $("#tdlSave").hide();
        }
    })
}

// Edit To Do List
function tdlEdit(id) {
    var todolist = new Object();
    debugger
    todolist.Id = id;
    debugger
    todolist.Name = $("#tdlNameText").val();
    todolist.UserId = $("#tdlUserText").val();
    $.ajax({
        "url": "/ToDoLists/Edit/",
        "type": "POST",
        "dataType": "json",
        "data": { Id: todolist.Id, Name: todolist.Name, UserId: todolist.UserId }
    }).then((result) => {
        if (result.statusCode == 200) {
            $("#tdlModal").modal("hide");
            Swal.fire({
                icon: 'success',
                title: 'Your data has been updated',
                text: 'Success!'
            }).then((result) => {
                location.reload();
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Your data not updated',
                text: 'Failed!'
            })
        }
    })
}

// Delete To Do List
function tdlDelete(id, userid) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            debugger
            $.ajax({
                "url": "/ToDoLists/Delete/",
                "dataType": "json",
                "data": { Id: id, userId: userid }
            }).then((hasil) => {
                debugger
                if (hasil.data[0] != 0) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your data has been deleted',
                        text: 'Deleted!'
                    }).then((result) => {
                        location.reload();
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Your data not deleted',
                        text: 'Failed!'
                    })
                }
            })
        }
    })
}

// Update Status To Do List
function tdlUpdateStatus(id, userid) {
    var todolist = new Object();
    debugger
    todolist.Id = id;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, complete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                "url": "/ToDoLists/UpdateStatus/",
                "type": "POST",
                "dataType": "json",
                "data": { Id: todolist.Id, userId: userid }
            }).then((result) => {
                if (result.statusCode == 200) {
                    $("#tdlModal").modal("hide");
                    Swal.fire({
                        icon: 'success',
                        title: 'Your to do list has been completed',
                        text: 'Success!'
                    }).then((result) => {
                        location.reload();
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Your data not updated',
                        text: 'Failed!'
                    })
                }
            })
        }
    })
}