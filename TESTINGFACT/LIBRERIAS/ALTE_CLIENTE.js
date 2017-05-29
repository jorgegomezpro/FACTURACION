function CrearModificarCliente() {
    var rfc = $("#MainContent_txtRfc").val(),
        razonsocial = $("#MainContent_txtRazonSocial").val(),
        calle = $("#MainContent_txtCalle").val(),
        numerointerior = $("#MainContent_txtNumeroInterior").val(),
        numeroexterior = $("#MainContent_txtNumeroExterior").val(),
        colonia = $("#MainContent_txtColonia").val(),
        delegacionmunicipio = $("#MainContent_txtDelegacionMunicipio").val(),
        localidad = $("#MainContent_txtLocalidad").val(),
        estado = $("#MainContent_txtEstado").val(),
        pais = $("#MainContent_txtPais").val(),
        codigopostal = $("#MainContent_txtCodigoPostal").val(),
        correoelectronico = $("#MainContent_txtCorreoElectronico").val();
    if (rfc == "" | razonsocial == "")
        alert("Los campos de RFC, y Razón Social son obligatorios de llenar");
    else
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Rfc: rfc, RazonSocial: razonsocial, Calle: calle, NumeroExterior: numeroexterior, NumeroInterior: numerointerior, Colonia: colonia,
                DelegacionMunicipio: delegacionmunicipio, Localidad: localidad, Estado: estado, Pais: pais, CodigoPostal: codigopostal, CorreoElectronico: correoelectronico
            }),
            url: "ALTA_CLIENTE.aspx/CrearModificarCliente",
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    alert("Registro exitoso");
                } else
                    alert("Hubo un error en la operación, intentelo de nuevo");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": Hubo un error en la operación, consulte al administrador");
            }
        });
}

function CrearCliente() {
    var rfc = $("#MainContent_txtRfc").val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Rfc: rfc }),
        url: "ALTA_CLIENTE.aspx/ExisteCliente",
        dataType: "json",
        success: function (data) {
            if (data.d != "False") {
                if (confirm("El Cliente ya existe, si continuas con la operación modificarás los datos capturados con anterioridad. ¿Deseas continuar?")) {
                    CrearModificarCliente()
                }
            } else CrearModificarCliente();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            debugger;
        }
    });
    return false;
}

$(document).ready(function () {
});
