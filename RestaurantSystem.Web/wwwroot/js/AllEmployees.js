$(document).ready(function () {
    $("#fire").click(function () {
        if (confirm('Are You Sure to Delete this Employee Record ?')) {
            $.ajax({
                type: "GET",
                url: '@Url.Action("Fire","Employees")/' + id,
                success: function (data) {
                    if (data.success) {
                        window.ajax.reload();

                        $.notify(data.message, {
                            globalPosition: "top center",
                            className: "success"
                        })

                    }

                });
        }
    })
})