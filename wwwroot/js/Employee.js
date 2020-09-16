let flag;
let unassignedDepartmentFlag;
let onload = 0;

function listEmployees() {
    $.ajax({
        url: "/Employee/List",
        type: "GET",
        success: function (result) {
           //List Employees in table
            let tableRows = 
                `<table id="employeeList" class="table table-hover"><thead class="thead-light">
                    <tr>
                     <th scope="col">Name</th>
                     <th scope="col">Email</th>
                     <th scope="col">Age</th>
                     <th scope="col">Department</th>
                     <th scope="col">Manager</th>
                    </tr>
                </thead>
                <tbody>`
            $.each(result, function (key,item){
                tableRows+=
                    `<tr onclick="getEmployee(${item.id})">
                        <td>${item.firstName} ${item.lastName}</td>
                        <td>${item.email}</td>
                        <td>${item.age}</td>
                        <td>${item.departmentName}</td>
                        <td>${item.manager}</td>
                    </tr>`
            })
            tableRows+= '</tbody></table>'
            $('#employeeListDiv').html(tableRows);
            $('#employeeList').DataTable({"pagingType":"numbers"});
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getDepartments() {
    $.ajax({
        url: "/Department/List",
        type: "GET",

        success: function (result) {
            let options = '<option selected value="0">Choose Department...</option>';
            $.each(result, function (key, item) {
                options+= `<option value="${item.id}" id="${item.managerId}">${item.departmentName}</option>`
            });

            $('#departmentEdit').html(options);
            $('#departmentCreate').html(options);
            $('#departmentCurrentName').html(options);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function createEmployee() {
    if($('#firstNameCreate').val()!='' && 
       $('#lastNameCreate').val()!='' &&
       $('#emailCreate').val()!=''&&
       $('#ageCreate').val()!=''&&
       $('#departmentCreate').val()!=0) {
        $.ajax({
            url: "/Employee/Create",
            type: "POST",
            data: {
                FirstName: $('#firstNameCreate').val(),
                LastName: $('#lastNameCreate').val(),
                Email: $('#emailCreate').val(),
                Age: $('#ageCreate').val(),
                DepartmentId: $('#departmentCreate').val()

            },

            success: function (result) {
                alert('Employee is added successfully.');
                $('#createEmployeeModal').modal('hide');
                listEmployees();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        alert("Kindly check your inputs!");
    }
}
function deleteEmployee() {
    if(isManager($('#idEdit').val())){
        alert("Cannot delete manager before assigning department to another employee.")
    }
    else {
        const message = confirm("Are you sure you want to delete this employee?");

        if (message) {

            $.ajax({
                url: "/Employee/Delete/",
                data:{
                    Id: $('#idEdit').val(),
                    FirstName: $('#firstNameEdit').val(),
                    LastName: $('#lastNameEdit').val(),
                    Email: $('#emailEdit').val(),
                    Age: $('#ageEdit').val(),
                    DepartmentId: $('#departmentEdit').val()
                },
                type: "POST",

                success: function (result) {
                    alert('Employee was deleted successfully.');
                    $('#editEmployeeModal').modal('hide');
                    listEmployees();
                },
                error: function (errormessage) {
                    alert("Employee doesn't exist!");
                }
            });
        }
    }
}
function getEmployee(id){
    $.ajax({
        url: "/Employee/GetOne/"+id,
        type: "GET",
        success: function (result) {
            $('#idEdit').val(id);
            $('#firstNameEdit').val(result.firstName);
            $('#lastNameEdit').val(result.lastName);
            $('#emailEdit').val(result.email);
            $('#ageEdit').val(result.age);
            $('#departmentEdit').val(result.departmentId);

            isManager(id, () => {
                if(flag){
                    $('#isManager').prop('checked', true)
                    $('#isManager').prop('disabled', true)
                    $('#departmentEdit').prop('disabled', true)
                }
                else {
                    $('#isManager').prop('checked', false)
                    $('#isManager').prop('disabled', false)
                    $('#departmentEdit').prop('disabled', false)
                }
                $('#editEmployeeModal').modal('show');
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function editEmployee(){

    if($('#firstNameEdit').val()!='' &&
        $('#lastNameEdit').val()!='' &&
        $('#emailEdit').val()!=''&&
        $('#ageEdit').val()!=''&&
        $('#departmentEdit').val()!='') {
        $.ajax({
            url: "/Employee/Edit",
            type: "POST",
            data: {
                Id: $('#idEdit').val(),
                FirstName: $('#firstNameEdit').val(),
                LastName: $('#lastNameEdit').val(),
                Email: $('#emailEdit').val(),
                Age: $('#ageEdit').val(),
                DepartmentId: $('#departmentEdit').val()
            },

            success: function (result) {
                if($('#isManager').is(':checked')){
                    updateManager($('#idEdit').val());
                }
                $('#editEmployeeModal').modal('hide');
                alert('Employee is Edited successfully.');
                getDepartments();
                listEmployees();

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        alert("Kindly check your input.")
    }
}
function updateManager(id){
    $.ajax({
        url: "/Department/Edit",
        type: "POST",
        data: {
            Id: $('#departmentEdit').val(),
            DepartmentName: $('#departmentEdit').find(':selected').html(),
            ManagerId: id
        },
        success: function (result) {
            listEmployees();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function isManager(id, callback){
    $.ajax({
        url: "/Department/List",
        type: "GET",

        success: function (result) {
            flag = 0;
            $.each(result, function (key, item) {
                if(id === item.managerId) {
                    flag = 1;
                }
            });
            callback();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    
}
function editDepartmentName() {
    getDepartments();
    if($('#departmentNameEdit').val()!='' && $('#departmentCurrentName').val() != ''){
        $.ajax({
            url: "/Department/Edit",
            type: "POST",
            data: {
                Id: $('#departmentCurrentName').val(),
                DepartmentName: $('#departmentNameEdit').val(),
                ManagerId: $('#departmentCurrentName').children(':selected').attr('id')
            },

            success: function (result) {
                getDepartments();
                listEmployees();
                alert('Department name is updated successfully.');
                $('#editDepartmentModal').modal('hide');

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        alert("Kindly check your input.")
    }
   
}
function createDepartment() {
    if($('#departmentNameCreate').val()!='') {
        $.ajax({
            url: "/Department/Create",
            type: "POST",
            data: {
                DepartmentName: $('#departmentNameCreate').val(),
                ManagerId: 0

            },

            success: function (result) {
                alert('Department is added successfully.');
                $('#createDepartmentModal').modal('hide');
                getDepartments();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        alert("Kindly check your input.")
    }
    
}



$(document).ready( function(){
    listEmployees();
    getDepartments();
})

