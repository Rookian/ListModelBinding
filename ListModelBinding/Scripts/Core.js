$(document).ready(function () {

    $("#btnSubmit").click(function (e) {
        e.preventDefault();
        var form = $(this).closest("form");

        
        if (form.valid()) {
            var data = form.serialize();
            var url = form.attr("action");

            $.ajax({
                url: url,
                data: data,
            });
        }
    });
    
    $("#simpleForm").submit(function (event) {
        alert(event.originalEvent.explicitOriginalTarget.id);
        event.preventDefault();
    });
});