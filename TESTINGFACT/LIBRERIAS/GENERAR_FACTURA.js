function formatCurrency(val) {
    val = parseFloat(val.toString().replace(/,/g, ""))
                    .toFixed(2)
                    .toString()
                    .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return val;
}
function addRowsToContinue(element) {
    if ($(element).hasClass("inputnumbercurrency")) {
        element.value = formatCurrency(element.value);
    }
    if ($(element).hasClass("recalculate")) {
        var val1 = $(element).parent().parent().find("td").eq(2).find("input").val().replace(",", ""),
            val2 = $(element).parent().parent().find("td").eq(4).find("input").val().replace(",", ""),
            res = (val1 * val2);
        var subtotal = 0,
                iva = 0,
                ish = 0,
                ieps = 0,
                total = 0;
        var importe = 0;
        $("#tblFacturar tbody tr.productvalid").each(function () {
            var item = this;
            if ($(item).find("td").eq(0).find("input").val() != "") {
                importe = ($(item).find("td").eq(4).find("input").val().replace(",", "") * 1) * ($(item).find("td").eq(2).find("input").val() * 1);
                subtotal += importe;
                iva += importe * ($(item).attr("p_iva") * 1);
                ish += importe * ($(item).attr("p_ish") * 1);
                ieps += importe * ($(item).attr("p_ieps") * 1);
                total += (importe + iva + ish + ieps);
                $(item).find("td").eq(5).find("input").val(formatCurrency(importe));
            }
        });
        $("#MainContent_txtSubTotal").val(formatCurrency(subtotal));
        $("#MainContent_TextBox1").val(formatCurrency(iva));
        $("#MainContent_TextBox2").val(formatCurrency(ish));
        $("#MainContent_TextBox3").val(formatCurrency(ieps));
        $("#MainContent_TextBox4").val(formatCurrency(total));
    }
    if ($(element).hasClass("searchproduct")) {
        var codigo = $(element).val();
        if (codigo != "")
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ Codigo: codigo }),
                url: "GENERAR_FACTURA.aspx/ValidadProducto",
                dataType: "json",
                success: function (data) {
                    if (data.d == null) {
                        alert("El código no existe");
                    }
                    else {
                        $(element).parent().parent().find("td").eq(0).find("input").val(data.d.CODIGO);
                        $(element).parent().parent().find("td").eq(1).find("input").val(data.d.DESCRIPCION);
                        if ($(element).parent().parent().find("td").eq(2).find("input").val() == "")
                            $(element).parent().parent().find("td").eq(2).find("input").val(1);
                        $(element).parent().parent().find("td").eq(3).find("input").val(data.d.UNIDAD);
                        $(element).parent().parent().find("td").eq(4).find("input").val(formatCurrency(data.d.PRECIO_SIN_IVA));
                        $(element).parent().parent().find("td").eq(5).find("input").val(formatCurrency(data.d.PRECIO_SIN_IVA));
                        $(element).parent().parent().attr("p_ieps", data.d.P_IEPS);
                        $(element).parent().parent().attr("p_iva", data.d.P_IVA);
                        $(element).parent().parent().attr("p_ish", data.d.P_ISH);
                    }
                }
            });
    }
    if ($(element).hasClass("searchbycasesensitive")) {
        if ($(element).parent().parent().next(".searchedproduct").length)
            seleccionarProducto($(element).parent().parent().next());
    }
    if (!$(element).parent().parent().closest('tr').next(".productvalid").length) {
        var $tr = $(element).parent().parent().clone();
        $tr.find("td").each(function (item, i) { $(this).children("input").val(""); });
        $("#tblFacturar tbody").append($tr);
    }
    borrarUltimosVacios();
    return false;
}
function saveSale() {
    if (confirm("Se cerrará esta cuenta con los montos establecidos. ¿Seguro que desea continuar?")) {
        var arreglo = [];
        CrearModificarCliente();
        $("#tblFacturar tbody tr.productvalid").each(function () {
            var tr = this,
                elemento = {
                    Codigo: $(tr).find("td").eq(0).find("input").val(),
                    Descripcion: $(tr).find("td").eq(1).find("input").val(),
                    Cantidad: $(tr).find("td").eq(2).find("input").val(),
                    Unidad: $(tr).find("td").eq(3).find("input").val(),
                    Precio: $(tr).find("td").eq(4).find("input").val().replace(",", ""),
                    Importe: $(tr).find("td").eq(5).find("input").val().replace(",", ""),
                    P_IEPS: $(tr).attr("p_ieps"),
                    P_IVA: $(tr).attr("p_iva"),
                    P_ISH: $(tr).attr("p_ish")
                };
            if (elemento.Codigo != "") {
                if (elemento.Descripcion == "")
                    alert("Debe existir una descripción para continuar con la venta de " + elemento.Descripcion);
                else {
                    if (!$.isNumeric(elemento.Cantidad) | !$.isNumeric(elemento.Precio))
                        alert("La cantidad y el precio deben ser numéricos");
                    else
                        arreglo.push(elemento);
                }
            }
        });
        var rfc = "";
        if ($("#chkConFactura").prop("checked"))
            rfc = $("#MainContent_txtRfc").val();
        var selFormaDePago = $("#selFormaDePago option:selected").text(),
            txtDatosDePago = $("#txtDatosDePago").val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Ventas: arreglo, Rfc: rfc, FormaDePago: selFormaDePago, DatosDePago: txtDatosDePago }),
            url: "GENERAR_FACTURA.aspx/GenerarVenta",
            dataType: "json",
            success: function (data) {
                if (data.d.ERROR)
                    alert(data.d.RESULTADO);
                else {
                    while ($("#tblFacturar tbody tr").length > 1)
                        $("#tblFacturar tbody tr").eq(0).remove();
                    if (rfc != "")
                        $("#chkConFactura").click();
                    $("#MainContent_txtSubTotal").val("");
                    $("#MainContent_TextBox1").val("");
                    $("#MainContent_TextBox2").val("");
                    $("#MainContent_TextBox3").val("");
                    $("#MainContent_TextBox4").val("");
                    alert("Venta exitosa");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": Hubo un error en la operación, consulte al administrador");
            }
        });
    }
    return false;
}
function removeRow(element) {
    $(element).parent().parent().parent().find("tr.searchedproduct").remove();
    if ($(element).parent().parent().parent().find("tr").length > 1)
        $(element).parent().parent().remove();
    else
        $(element).parent().parent().find("td").each(function (i, item) {
            if ($(item).find("input").length)
                $(item).find("input").val("");
        });
    return false;
}
function buscarArticulo(element) {
    var descripcion = $(element).val();
    if (descripcion != "")
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Descripcion: descripcion }),
            url: "GENERAR_FACTURA.aspx/BuscarProducto",
            dataType: "json",
            success: function (data) {
                $(".searchedproduct").remove();
                $.each(data.d, function (i, item) {
                    $(element).parent().parent().after("<tr class='searchedproduct' style='cursor:pointer;' onclick='return seleccionarProducto(this);'>" +
                        "<td>" + item.CODIGO + "</td>" +
                        "<td>" + item.DESCRIPCION + "</td>" +
                        "<td>1</td>" +
                        "<td>" + item.UNIDAD + "</td>" +
                        "<td>" + item.PRECIO_SIN_IVA + "</td>" +
                        "<td></td>" +
                        "</tr>");
                });
            }
        });
    else
        $(".searchedproduct").remove();
    return false;
}
function seleccionarProducto(element) {
    var indexofelement = $(element).index();
    for (var i = indexofelement - 1; i >= 0; i--) {
        if ($("#tblFacturar tbody tr").eq(i).hasClass("productvalid")) {
            $("#tblFacturar tbody tr").eq(i).find("td").eq(0).find("input").val($(element).find("td").eq(0).html());
            $("#tblFacturar tbody tr").eq(i).find("td").eq(1).find("input").val($(element).find("td").eq(1).html());
            if ($("#tblFacturar tbody tr").eq(i).find("td").eq(2).find("input").val() == "")
                $("#tblFacturar tbody tr").eq(i).find("td").eq(2).find("input").val($(element).find("td").eq(2).html());
            $("#tblFacturar tbody tr").eq(i).find("td").eq(3).find("input").val($(element).find("td").eq(2).html());
            $("#tblFacturar tbody tr").eq(i).find("td").eq(4).find("input").val($(element).find("td").eq(4).html());
            $("#tblFacturar tbody tr").eq(i).find("td").eq(5).find("input").val($(element).find("td").eq(4).html());
            $("#tblFacturar tbody tr").eq(i).find("td").eq(2).find("input").focus();
            $("#tblFacturar tbody tr").eq(i).find("td").eq(2).find("input").select();
            $(".searchedproduct").remove();
            break;
        }
    }
    return false;
}

function buscarCliente() {
    var descripcion = $("#MainContent_txtRazonSocial").val();
    if (descripcion != "")
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Nombre: descripcion }),
            url: "GENERAR_FACTURA.aspx/BuscarCliente",
            dataType: "json",
            success: function (data) {
                $("#tdClientes").html("");
                $.each(data.d, function (i, item) {
                    $("#tdClientes").append("<div class='separar-clientes' rfc='" + item.RFC + "' onclick='return seleccionarCliente(\"" + item.RFC + "\");'>" +
                        "<span class='cliente-titulo'>RFC:</span><span class='cliente-descripcion'> " + item.RFC + " </span>" +
                        "<span class='cliente-titulo'>RAZÓN SOCIAL:</span><span class='cliente-descripcion'> " + item.RAZON_SOCIAL + " </span>" +
                        "<span class='cliente-titulo'>CALLE:</span><span class='cliente-descripcion'> " + item.CALLE + " </span>" +
                        "<span class='cliente-titulo'>NÚMERO INTERIOR:</span><span class='cliente-descripcion'> " + item.NUMERO_INTERIOR + " </span>" +
                        "<span class='cliente-titulo'>NÚMERO EXTERIOR:</span><span class='cliente-descripcion'> " + item.NUMERO_EXTERIOR + " </span>" +
                        "<span class='cliente-titulo'>COLONIA:</span><span class='cliente-descripcion'> " + item.COLONIA + " </span>" +
                        "<span class='cliente-titulo'>DELEGACIÓN/MUNICIPIO:</span><span class='cliente-descripcion'> " + item.DELEGACION_MUNICIPIO + " </span>" +
                        "<span class='cliente-titulo'>LOCALIDAD:</span><span class='cliente-descripcion'> " + item.LOCALIDAD + " </span>" +
                        "<span class='cliente-titulo'>ESTADO:</span><span class='cliente-descripcion'> " + item.ESTADO + " </span>" +
                        "<span class='cliente-titulo'>PAÍS:</span><span class='cliente-descripcion'> " + item.PAIS + " </span>" +
                        "<span class='cliente-titulo'>CÓDIGO POSTAL:</span><span class='cliente-descripcion'> " + item.CODIGO_POSTAL + " </span>" +
                        "<span class='cliente-titulo'>CORREO ELECTRÓNICO:</span><span class='cliente-descripcion'> " + item.CORREO_ELECTRONICO + " </span></div>");
                });
            }
        });
    else
        $("#tdClientes").html("");
    return false;
}
function seleccionarCliente(rfc) {
    if (rfc == "") {
        if ($("#tdClientes div").length > 0) {
            var localizado = $("#tdClientes div").eq(0).attr("rfc");
            llenarDatosDeCliente(localizado);
        }
    }
    else
        llenarDatosDeCliente(rfc);
    $("#tdClientes").html("");
    return false;
}

function CrearModificarCliente() {
    var rfc = $("#MainContent_txtRfc").val(), _rfc = $("#MainContent_txtRfc").attr("last"),
        razonsocial = $("#MainContent_txtRazonSocial").val(), _razonsocial = $("#MainContent_txtRazonSocial").attr("last"),
        calle = $("#MainContent_txtCalle").val(), _calle = $("#MainContent_txtCalle").attr("last"),
        numerointerior = $("#MainContent_txtNumeroInterior").val(), _numerointerior = $("#MainContent_txtNumeroInterior").attr("last"),
        numeroexterior = $("#MainContent_txtNumeroExterior").val(), _numeroexterior = $("#MainContent_txtNumeroExterior").attr("last"),
        colonia = $("#MainContent_txtColonia").val(), _colonia = $("#MainContent_txtColonia").attr("last"),
        delegacionmunicipio = $("#MainContent_txtDelegacionMunicipio").val(), _delegacionmunicipio = $("#MainContent_txtDelegacionMunicipio").attr("last"),
        localidad = $("#MainContent_txtLocalidad").val(), _localidad = $("#MainContent_txtLocalidad").attr("last"),
        estado = $("#MainContent_txtEstado").val(), _estado = $("#MainContent_txtEstado").attr("last"),
        pais = $("#MainContent_txtPais").val(), _pais = $("#MainContent_txtPais").attr("last"),
        codigopostal = $("#MainContent_txtCodigoPostal").val(), _codigopostal = $("#MainContent_txtCodigoPostal").attr("last"),
        correoelectronico = $("#MainContent_txtCorreoElectronico").val(), _correoelectronico = $("#MainContent_txtCorreoElectronico").attr("last");
    if (rfc == "" | razonsocial == "")
        alert("Los campos de RFC, y Razón Social son obligatorios de llenar");
    else {
        if (rfc != _rfc | razonsocial != _razonsocial | calle != _calle | numerointerior != _numerointerior | numeroexterior != _numeroexterior
            | colonia != _colonia | delegacionmunicipio != _delegacionmunicipio | localidad != _localidad | estado != _estado | pais != _pais
            | codigopostal != _codigopostal | correoelectronico != _correoelectronico) {
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
                        alert("Registro de cliente exitoso");
                    } else
                        alert("Hubo un error en la operación, intentelo de nuevo");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": Hubo un error en la operación, consulte al administrador");
                }
            });
        }
    }
}
function llenarDatosDeCliente(rfc) {
    if (rfc == "")
        rfc = $("#MainContent_txtRfc").val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Rfc: rfc }),
        url: "GENERAR_FACTURA.aspx/ObtenerCliente",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                $("#MainContent_txtRfc").val(data.d.RFC).attr("last", data.d.RFC);
                $("#MainContent_txtRazonSocial").val(data.d.RAZON_SOCIAL).attr("last", data.d.RAZON_SOCIAL);
                $("#MainContent_txtCalle").val(data.d.CALLE).attr("last", data.d.CALLE);
                $("#MainContent_txtNumeroInterior").val(data.d.NUMERO_INTERIOR).attr("last", data.d.NUMERO_INTERIOR);
                $("#MainContent_txtNumeroExterior").val(data.d.NUMERO_EXTERIOR).attr("last", data.d.NUMERO_EXTERIOR);
                $("#MainContent_txtColonia").val(data.d.COLONIA).attr("last", data.d.COLONIA);
                $("#MainContent_txtDelegacionMunicipio").val(data.d.DELEGACION_MUNICIPIO).attr("last", data.d.DELEGACION_MUNICIPIO);
                $("#MainContent_txtLocalidad").val(data.d.LOCALIDAD).attr("last", data.d.LOCALIDAD);
                $("#MainContent_txtEstado").val(data.d.ESTADO).attr("last", data.d.ESTADO);
                $("#MainContent_txtPais").val(data.d.PAIS).attr("last", data.d.PAIS);
                $("#MainContent_txtCodigoPostal").val(data.d.CODIGO_POSTAL).attr("last", data.d.CODIGO_POSTAL);
                $("#MainContent_txtCorreoElectronico").val(data.d.CORREO_ELECTRONICO).attr("last", data.d.CORREO_ELECTRONICO);
            }
        }
    });
}
function generarFactura(element) {
    if ($(element).prop("checked")) {
        var rfcinput = prompt("Ingresa el RFC de cliente, podrás modificar sus datos fiscales si eso deseas al generar la factura.", "");
        $("#MainContent_txtRazonSocial").focus();
        $("#MainContent_txtRazonSocial").select();
        llenarDatosDeCliente(rfcinput);
        $("#tblRegistrarCliente").show();
    }
    else
        $("#tblRegistrarCliente").hide();
    return false;
}
function borrarUltimosVacios() {
    var ultimo = $("#tblFacturar tbody tr").length - 1;
    var anteriovacio = false,
        estevacio = false;
    for (var i = ultimo; i > 0; i--) {
        if ($("#tblFacturar tbody tr").eq(i).hasClass("productvalid")) {
            if ($("#tblFacturar tbody tr").eq(i).find("td").eq(0).find("input").val() == ""
                & $("#tblFacturar tbody tr").eq(i).find("td").eq(1).find("input").val() == "")
                estevacio = true;
            else
                estevacio = false;
            if (anteriovacio & estevacio) {
                $("#tblFacturar tbody tr").eq(i + 1).remove();
                anteriovacio = false;
            } else
                anteriovacio = estevacio;
        }
    }
}

$(document).ready(function () {
    $("#tblRegistrarCliente").hide();
});
