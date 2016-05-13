//Ready function
$(document).ready(function () {
    console.log("Mættur!");

    setTimeout(function () {
        $("#success").hide();
    }, 3000);


    //function that adds a color to output from user
    //depending on if it was accepted or not
    if ($('.output').text() == $('.userOutput').text()) {
        $('.userOutput').css("background", "#3EC150");
    }
    else {
        $('.userOutput').css("background", "#E84A23");
    }

    //function for the dropdown list to get all assignments
    //for a chosen course in teacher and student home views
    $("#courses").change(function () {
        var id = $(this).children(":selected").attr('id');
        var form = $("#courseForm");
        $.ajax({
            url: 'AssignmentJson/' + id,
            method: 'POST',
            data: form.serialize(),
            success: function(data) {
                /*var html = "<select multiple='multiple' class='form-control' id='assignments'>";
               for (var i = 0; i < data.length; i++) {
                   var tempID = data[i].ID.toString();
                   html += "<option onclick='assignments(" + tempID +");' class='assignmentSelect' id='" + tempID + "'>" + data[i].Title + "</option>";
               }
               html += "</select>";*/
                console.log(data);
                var html = '<h2>Assignments</h2>';
                for (var i = 0; i < data.length; i++) {
                    var tempID = data[i].ID.toString();
                    html += '<a href="/Assignment/Details/' + tempID + '" class="list-group-item">' + data[i].Title + '</a>';
                }
                if (html == "<h2>Assignments</h2>") {
                    html += "<p>No Assignments in this course right now!</p>";
                }
                $("#assignments").html(html);
            }
        });
    });
});


//function to navigate to assignments details page
function assignments(id) {
    window.location = "~/Assignment/Details/" + id;
}

//function to navigate to assignments result page
function submissionResults(id) {
    window.location = "/Assignment/Results/" + id;
}


//function to create a course
function CreateCourse() {
    var title = $("#courseName").val();
    console.log(title)
    var obj = new Object();
    obj.Name = title;

    obj.teachers = [];
    $(".teachers").each(function (a) {
        if ($(this).is(":checked")) {
            obj.teachers.push($(this).val());
        }
    });

    obj.students = [];
    $(".students").each(function (a) {
        if ($(this).is(":checked")) {
            obj.students.push($(this).val());
        }
    });
    if (obj.Name == "" || obj.students.length == 0 || obj.teachers.length == 0) {
        var html = '<div class="alert alert-danger" id="success " role="alert">';
        html += '<p>You must fill out all the fields</p></div>';
        $('#failed').html(html);
    }

    else {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/Course/CreateCourseJson',
            data: JSON.stringify(obj),
            cache: false,
            dataType: "json",
            success: function (result) {
                console.log(result);
                window.location = "?message=success";
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
}

function TestTeachers() {
    var teachers = [];
    $(".teachers").each(function (a) {
        if ($(this).is(":checked")) {
            teachers.push($(this).val());
        }
    });

    console.log(teachers);
}


//function to add milestones when a teacher is creating
//an assignment
function addMilestones() {
    var number = $('#noOfMilestones').val();
    var html = "";
    var no = 1;
    for (var i = 0; i < number; i++) {
        html += '<div class="panel panel-default">';
        html += '<div class="panel-heading">';
        html += '<h4 class="panel-title">';
        html += '<a data-toggle="collapse" data-parent="#accordion" href="#collapse' + no + '">'
        html +=  'Milestone' + no + '</a>';
        html += '</h4></div>';
        html += '<div id="collapse' + no+ '" class="panel-collapse collapse">';
        html += '<div class="panel-body">';
        html += '<label for="mName' + no + '">Name</label>';
        html += '<input type="text" class="form-control" required id="mName' + no + '" name="mName' + no + '"/>';
        html += '<label for="mWeight">Weight(%)</label>';
        html += '<input type="number" min="0" max="100" required class="form-control" name="mWeight' + no +'" id="mWeight' + no + '"/>';
        html += '<label>Input';
        html += '<input type="file" class="form-control" name="file" required id="file" accept=".txt"/></label>';
        html += '<label>Output';
        html += '<input type="file" class="form-control" name="file" required id="file" accept=".txt"/></label>';
        html += '</div>';
        html += '</div></div></div>';
        no += 1;
 
    }
    $(".milestones").html(html);
}
function AddTeacher() {
    var name = $("#TeacherName").val()
    var courseId = $("#EditSelect").val();
    var obj = new Object();
    obj.name = name;
    obj.courseId = courseId;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Course/AddTeacherToCourse',
        data: JSON.stringify(obj),
        cache: false,
        dataType: "json",
        success: function (result) {
            console.log(result);
            window.location = "?message=success";
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    }); 
}
function DeleteTeacher() {
    var name = $("#TeacherName").val()
    var courseId = $("#EditSelect").val();
        var obj = new Object();
        obj.name = name;
        obj.courseId = courseId;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/Course/RemoveTeacherFromCourse',
            data: JSON.stringify(obj),
            cache: false,
            dataType: "json",
            success: function (result) {
                console.log(result);
                window.location = "?message=success";
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
}
function AddStudent() {
    var name = $("#StudentName").val()
    var courseId = $("#EditSelect").val();
    var obj = new Object();
    obj.name = name;
    obj.courseId = courseId;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Course/AddStudentToCourse',
        data: JSON.stringify(obj),
        cache: false,
        dataType: "json",
        success: function (result) {
            console.log(result);
            window.location = "?message=success";
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}
function DeleteStudent() {
    var name = $("#StudentName").val()
    var courseId = $("#EditSelect").val();
    var obj = new Object();
    obj.name = name;
    obj.courseId = courseId;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Course/RemoveStudentFromCourse',
        data: JSON.stringify(obj),
        cache: false,
        dataType: "json",
        success: function (result) {
            console.log(result);
            window.location = "?message=success";
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}
function DeleteCourse() {
    var courseId = $("#EditSelect").val();
    console.log(courseId);
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: '/Course/RemoveCourse?id=' + courseId,
        cache: false,
        dataType: "json",
        success: function (result) {
            console.log(result);
            window.location = "?message=success";
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}