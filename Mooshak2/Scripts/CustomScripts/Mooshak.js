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
                    html += '<a href="/Assignment/Details/' + tempID + '" class="list-group-item">' + data[i].Title + '</a>';
                }
                if (html == "") {
                    html = "<p>No Assignments in this course right now!</p>";
                }
                $(".list-group").html(html);
            }
        });
    });
});

function assignments(id) {
    window.location = "~/Assignment/Details/" + id;
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