<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ALTA_CLIENTE.aspx.cs" Inherits="TESTINGFACT.ALTA_CLIENTE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .tblForm tr td {
            padding: 5px;
        }
    </style>
    <script src="LIBRERIAS/ALTE_CLIENTE.js"></script>
    <table class="tblForm" style="width: 100%; border-style: solid; border-width: 2px; border-color: #122435;">
        <tr>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Content/ico_alta.png" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">RFC
            </td>
            <td>
                <asp:TextBox ID="txtRfc" runat="server" Width="280"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">RAZÓN SOCIAL
            </td>
            <td>
                <asp:TextBox ID="txtRazonSocial" runat="server" Width="280"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">CALLE
            </td>
            <td>
                <asp:TextBox ID="txtCalle" runat="server" Width="280"></asp:TextBox>
            </td>
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
        <tr>
            <td colspan="2" style="text-align: center;">
                <br />
                <br />
                <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Content/btn_regisart.png" OnClientClick="return CrearCliente();" Style="height: 47px" />
            </td>
        </tr>
    </table>
</asp:Content>
