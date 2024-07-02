function ConsultarIdentificacion() {

    let cedula = $("#Cedula").val();

    if (cedula.length >= 9) {
        $.ajax({
            type: 'GET',
            url: 'https://apis.gometa.org/cedulas/' + cedula,
            dataType: 'json',
            success: function (data) {
                $("#Nombre").val(data.nombre);
            }
        });

    }
    else {
        $("#Nombre").val("");
    }
}