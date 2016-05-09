function assignments(id) {
    window.location = "/Assignment/Submit/" + id;
}

$(document).ready(function () {
    $("#courses").change(function () {
        var id = $(this).children(":selected").attr('id');
        var form = $("#courseForm");
        $.ajax({
            url: "/Home/AssignmentJson/" + id,
            method: 'POST',
            data : form.serialize(),
            success: function (data) {
               /* var html = "<select multiple='multiple' class='form-control' id='assignments'>";
                for (var i = 0; i < data.length; i++) {
                    var tempID = data[i].ID.toString();
                    html += "<option class='ass' id='" + tempID + "'>" + data[i].Title + "</option>";
                }
                html += "</select>";*/
                var html = "";
                for (var i = 0; i < data.length; i++) {
                    var tempID = data[i].ID.toString();
                    html += "<button class='btn btn-primary ass' onclick='assignments("+ tempID + ");'>" + data[i].Title + "</button>";
                }
                $(".assignmentDiv").html(html);
;            }
        });
    });
});