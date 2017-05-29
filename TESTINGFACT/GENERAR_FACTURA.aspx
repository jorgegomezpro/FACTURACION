<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GENERAR_FACTURA.aspx.cs" Inherits="TESTINGFACT.GENERAR_FACTURA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #tblRegistrarCliente tr td {
            padding: 0px;
        }

        .busquedaservidor {
            background-color: khaki;
        }

        .cliente-titulo {
            font-weight: 900;
            font-size: 10px;
        }

        .cliente-descripcion {
            font-size: 10px;
        }

        .separar-clientes {
            border-style: solid;
            border-color: black;
            border-width: 2px;
            cursor: pointer;
        }

        #tblFacturar tbody tr td input {
            width: 100%;
            max-width: 100% !important;
        }

        span.btnRemoveHideRow {
            background-image: url('data:image/svg+xml;utf8,<svg style="enable-background:new 0 0 24 24;" version="1.1" viewBox="0 0 24 24" xml:space="preserve" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><g id="info"/><g id="icons"><g id="delete"><path d="M18.9,8H5.1c-0.6,0-1.1,0.5-1,1.1l1.6,13.1c0.1,1,1,1.7,2,1.7h8.5c1,0,1.9-0.7,2-1.7l1.6-13.1C19.9,8.5,19.5,8,18.9,8z"/><path d="M20,2h-5l0,0c0-1.1-0.9-2-2-2h-2C9.9,0,9,0.9,9,2l0,0H4C2.9,2,2,2.9,2,4v1c0,0.6,0.4,1,1,1h18c0.6,0,1-0.4,1-1V4    C22,2.9,21.1,2,20,2z"/></g></g></svg>');
            cursor: pointer;
            background-repeat: no-repeat;
            display: block;
            position: absolute;
            width: 18px;
            height: 18px;
            margin-top: 3px;
        }

        .busquedaservidor {
            padding-left: 20px;
        }
    </style>
    <script src="LIBRERIAS/GENERAR_FACTURA.js"></script>
    <table class="table" id="tblFacturar">
        <thead>
            <tr style="color: #F7F7F7; background-color: #4A3C8C; font-weight: bold;">
                <th style="width: 15%;">CÓDIGO
                </th>
                <th style="width: 40%;">DESCRIPCIÓN
                </th>
                <th style="width: 10%;">CANTIDAD
                </th>
                <th style="width: 15%;">UNIDAD
                </th>
                <th style="width: 10%;">PRECIO
                </th>
                <th style="width: 10%;">IMPORTE
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="productvalid" style="color: #4A3C8C; background-color: #E7E7FF;" p_ieps="0" p_iva="0" p_ish="0">
                <td>
                    <span title="Eliminar registro" onclick="return removeRow(this);" class="btnRemoveHideRow"></span>
                    <input type="text" class="searchproduct busquedaservidor" onblur="return addRowsToContinue(this);" />
                </td>
                <td>
                    <span title="Eliminar registro" onclick="return removeRow(this);" class="btnRemoveHideRow"></span>
                    <input type="text" class="searchbycasesensitive busquedaservidor" onblur="return addRowsToContinue(this);" onchange="return buscarArticulo(this);" onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();" /></td>
                <td>
                    <input type="number" class="recalculate" onblur="return addRowsToContinue(this);" /></td>
                <td>
                    <input type="text" onblur="return addRowsToContinue(this);" /></td>
                <td>
                    <input type="text" class="inputnumbercurrency" onblur="return addRowsToContinue(this);" /></td>
                <td>
                    <input type="text" class="inputnumbercurrency" disabled="disabled" onblur="return addRowsToContinue(this);" /></td>
            </tr>
        </tbody>
    </table>
    <table class="table">
        <tr>
            <td rowspan="11">
                <table id="tblRegistrarCliente">
                    <tbody>
                        <tr>
                            <td style="text-align: right;">RFC
                            </td>
                            <td>
                                <asp:TextBox ID="txtRfc" runat="server" class="busquedaservidor" onblur="return llenarDatosDeCliente('');" Width="280"></asp:TextBox>
                            </td>
                            <td>Forma de pago</td>
                            <td>
                                <select id="selFormaDePago">
                                    <option value="01">efectivo</option>
                                    <option value="02">Cheque nominativo</option>
                                    <option value="03">Transferencia electrónica de fondos</option>
                                    <option value="04">Tarjeta de crédito</option>
                                    <option value="05">Monedero electrónico</option>
                                    <option value="06">Dinero electrónico</option>
                                    <option value="08">Vales de despensa</option>
                                    <option value="28">Tarjeta de debito</option>
                                    <option value="29">Tarjeta de servicio</option>
                                    <option value="99">otros</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">RAZÓN SOCIAL
                            </td>
                            <td>
                                <asp:TextBox ID="txtRazonSocial" class="busquedaservidor" runat="server" onblur="return seleccionarCliente('');"
                                    onchange="return buscarCliente();" onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"
                                    Width="280"></asp:TextBox>
                            </td>
                            <td>Dígitos de la tarjeta</td>
                            <td>
                                <input type="text" id="txtDatosDePago" value="" placeholder="4 dígitos" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="tdClientes"></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">CALLE
                            </td>
                            <td>
                                <asp:TextBox ID="txtCalle" runat="server" Width="280"></asp:TextBox>
                            </td>
                            <td rowspan="12"></td>
                            <td rowspan="12"></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">NÚMERO INT.
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroInterior" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">NÚMERO EXT.
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroExterior" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">COLONIA
                            </td>
                            <td>
                                <asp:TextBox ID="txtColonia" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">DELEGACIÓN O MUNICIPIO
                            </td>
                            <td>
                                <asp:TextBox ID="txtDelegacionMunicipio" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">LOCALIDAD
                            </td>
                            <td>
                                <asp:TextBox ID="txtLocalidad" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">ESTADO
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstado" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">PAÍS
                            </td>
                            <td>
                                <asp:TextBox ID="txtPais" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">CÓDIGO POSTAL
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigoPostal" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">CORREO ELECTRÓNICO
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorreoElectronico" runat="server" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td></td>
            <td>
                <label>
                    <input type="checkbox" name="requierefactura" value="false" id="chkConFactura" onchange="return generarFactura(this);" />Requiere fáctura</label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">SUBTOTAL</td>
            <td style="text-align: right; width: 150px;">
                <asp:TextBox ID="txtSubTotal" Width="150px" disabled="disabled" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">IVA</td>
            <td style="text-align: right; width: 150px;">
                <asp:TextBox ID="TextBox1" Width="150px" disabled="disabled" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">ISH</td>
            <td style="text-align: right; width: 150px;">
                <asp:TextBox ID="TextBox2" Width="150px" disabled="disabled" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">IEPS</td>
            <td style="text-align: right; width: 150px;">
                <asp:TextBox ID="TextBox3" Width="150px" disabled="disabled" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">TOTAL</td>
            <td style="text-align: right; width: 150px;">
                <asp:TextBox ID="TextBox4" Width="150px" disabled="disabled" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: right; width: 150px;"></td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: right; width: 150px;"></td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: right; width: 150px;"></td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: right; width: 150px;"></td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: right; width: 150px;"></td>
        </tr>
        <tr>
            <td style="text-align: center;" colspan="2">
                <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Content/btn_regisart.png" OnClientClick="return saveSale();" Style="height: 47px" />
            </td>
        </tr>
    </table>
</asp:Content>
