<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ALTA_ARTICULO.aspx.cs" Inherits="TESTINGFACT.ALTA_ARTICULO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .tblForm tr td {
            padding: 5px;
        }
    .auto-style1 {
        width: 20px;
    }
    </style>
    <script src="LIBRERIAS/ALTA_ARTICULO.js"></script>
    <table class="tblForm" style="width: 100%; border-style: solid; border-width: 2px; border-color: #122435;">
        <tr>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Content/ico_alta.png" />
                <br />
                <br />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
            <td rowspan="10">Área de búsqueda
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">CÓDIGO
            </td>
            <td>
                <asp:TextBox ID="txtCodigor" runat="server" Width="280"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td style="text-align: right;">CLASE
            </td>
            <td>
                <asp:DropDownList ID="txtTipo" runat="server" Font-Names="Arial" Height="23px" Width="150px">
                    <asp:ListItem>ARTICULO</asp:ListItem>
                    <asp:ListItem>SERVICIO</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td style="text-align: right;">DESCRIPCIÓN
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtDescripcionr" runat="server" Width="93%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">UNIDAD
            </td>
            <td>
                <asp:TextBox ID="txtUnidad" runat="server" Width="280"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td style="text-align: right;">PRECIO SIN IVA
            </td>
            <td>
                <asp:TextBox ID="txtPrecio" runat="server" Width="280"></asp:TextBox>
            </td>
            <td>
                <asp:RadioButtonList ID="rdListGeneraIVA" runat="server">
                    <asp:ListItem Text="Genera IVA" Value="true"></asp:ListItem>
                    <asp:ListItem Text="No Genera IVA" Value="false"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>% IEPS
            </td>
            <td>
                <asp:TextBox ID="txtPorcentajeIeps" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RadioButtonList ID="rdListGeneraISH" runat="server">
                    <asp:ListItem Text="Genera ISH" Value="true"></asp:ListItem>
                    <asp:ListItem Text="No Genera ISH" Value="false"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="auto-style1">
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">STOCK MÍNIMO
            </td>
            <td>
                <asp:TextBox ID="txtStockMinimo" runat="server" Width="280"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td style="text-align: right;">STOCK MÁXIMO
            </td>
            <td>
                <asp:TextBox ID="txtStockMaximo" runat="server" Width="280"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td style="text-align: right;">ANAQUEL
            </td>
            <td>
                <asp:TextBox ID="txtAnaquel" runat="server" Width="280"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td colspan="7" style="text-align: center;">
                <br />
                <br />
                <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Content/btn_regisart.png" Style="height: 47px" OnClientClick="return CrearArticuloServicio();" />
            </td>
        </tr>
    </table>
</asp:Content>
