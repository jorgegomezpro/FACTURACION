function CrearModificarArticulo() {
    var codigo = $("#MainContent_txtCodigor").val(),
        clase = $("#MainContent_txtTipo").val(),
        descripcion = $("#MainContent_txtDescripcionr").val(),
        unidad = $("#MainContent_txtUnidad").val(),
        generaish = $("input:radio[name='ctl00$MainContent$rdListGeneraISH']:checked").val(),
        preciosiniva = $("#MainContent_txtPrecio").val(),
        geeraiva = ($("input:radio[name='ctl00$MainContent$rdListGeneraIVA']:checked").val() == true ? true : false),
        p_ieps = $("#MainContent_txtPorcentajeIeps").val(),
        stockminimo = $("#MainContent_txtStockMinimo").val(),
        stockmaximo = $("#MainContent_txtStockMaximo").val(),
        anaquel = $("#MainContent_txtAnaquel").val();
    if (descripcion == "" | geeraiva == undefined | codigo == "" | generaish == undefined)
        alert("Los campos de Código, Descripción y la Opción de Genera IVA son obligatorias de marcar");
    else
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Codigo: codigo, Clase: clase, Descripcion: descripcion, Unidad: unidad, PrecioSinIVA: preciosiniva, GeneraIVA: geeraiva,
                PorcentajeIEPS: p_ieps, GeneraISH: generaish, StockMinimo: stockminimo, StockMaximo: stockmaximo, Anaquel: anaquel
            }),
            url: "ALTA_ARTICULO.aspx/CrearModificarArticuloServicio",
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

function CrearArticuloServicio() {
    var codigo = $("#MainContent_txtCodigor").val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Codigo: codigo }),
        url: "ALTA_ARTICULO.aspx/ExisteArticuloServicio",
        dataType: "json",
        success: function (data) {
            if (data.d != "False") {
                if (confirm("Este artículo ya existe, si continuas con la operación modificarás los datos capturados con anterioridad. ¿Deseas continuar?")) {
                    CrearModificarArticulo()
                }
            } else CrearModificarArticulo();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            debugger;
        }
    });
    return false;
}

$(document).ready(function () {
    $("input:radio[name='ctl00$MainContent$rdListGeneraISH']").click(function () {
        if ($("input:radio[name='ctl00$MainContent$rdListGeneraISH']:checked").val() == "true")
            $("#MainContent_txtPorcentejeDeISH").prop('disabled', false);
        else
            $("#MainContent_txtPorcentejeDeISH").prop('disabled', true);
    });
});
