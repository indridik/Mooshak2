//Ready function
$(function () {

});


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
