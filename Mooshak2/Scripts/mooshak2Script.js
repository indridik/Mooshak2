

$(document).ready(function () {
    $("#courses").change(function () {
        var id = $(this).children(":selected").attr('id');
        var form = $("#courseForm");
        $.ajax({
            url: "/Home/AssignmentJson/" + id,
            method: 'POST',
            data : form.serialize(),
            success: function(data) {
                var html = "<select multiple class='form-control' id='assignments'>";
                for(var i = 0; i < data.length; i++) {
                    html += "<option>" + data[i].Title + "</option>";
                }
                html += "</select";
                $(".assignmentDiv").html(html);
;            }
        });
    });
});