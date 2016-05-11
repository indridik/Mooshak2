//Ready function
$(document).ready(function () {

    setTimeout(function () {
        $("#success").hide(); }, 3000);


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
                var html = "";
                for (var i = 0; i < data.length; i++) {
                    var tempID = data[i].ID.toString();
                    html += "<button class='btn btn-primary ass' onclick='assignments(" + tempID +");'>" + data[i].Title + "</button>";
                }
                $(".assignmentDiv").html(html);
            }
        });
    });
});

function assignments(id) {
    window.location = "/Assignment/Details/" + id;
}

function submissionResults(id) {
    window.location = "/Assignment/Results/" + id;
}

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
    

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Course/CreateCourseJson',
        data: JSON.stringify(obj) ,
        cache: false,
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            
        }
    });
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
