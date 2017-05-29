<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventanas.aspx.cs" Inherits="TESTINGFACT.Ventanas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width: 100%; background-color: #c3cdd4;">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/head_almacen1.png" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    <br />
                        <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Content/altag.png" OnClick="ImageButton14_Click" />
                        <div style="background-color: #5FAAD2; border-radius: 7px; height: 26px; width: 170px; padding: 6px;">
                            <img src="Content/ico_alta.png" style="" />
                            <span style="font-size:12px;font-family:Arial;"><i></i> DAR DE ALTA ARTÍCULOS</span>
                        </div>
                        &nbsp; &nbsp;&nbsp;&nbsp;
                    <br />
                        <br />
                        <asp:MultiView ID="PREGUNTAS" runat="server" ActiveViewIndex="0">
                            <asp:View ID="VER" runat="server">
                                <table style="width: 100%; border-style: solid; border-width: 2px; border-color: #122435;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Content/ico_alta.png" />
                                            <br />
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                    <td colspan="4">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">
                                                        <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="11pt" Text="CÓDIGO:"></asp:Label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txtcodigo" runat="server" Width="150px"></asp:TextBox>
                                                        &nbsp;<asp:ImageButton ID="ImageButton20" runat="server" Height="24px" ImageUrl="~/Content/lupa1.png" OnClick="ImageButton20_Click" Width="17px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">
                                                        <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="11pt" Text="CLASE:"></asp:Label>
                                                    </td>
                                                    <td class="auto-style12">
                                                        <asp:DropDownList ID="txtclass" runat="server" Font-Names="Arial" Height="23px" Width="150px">
                                                            <asp:ListItem>ARTICULO</asp:ListItem>
                                                            <asp:ListItem>SERVICIO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="auto-style14">&nbsp;</td>
                                                    <td class="auto-style15">&nbsp;</td>
                                                    <td>&nbsp; &nbsp; &nbsp; </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">
                                                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="11pt" Text="DESCRIPCIÓN:"></asp:Label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txtdescripcion" runat="server" Width="630px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                    <td colspan="4">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7" colspan="5">
                                                        <asp:GridView ID="GridView7" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="GridView7_SelectedIndexChanged" Width="1150px">
                                                            <Columns>
                                                                <asp:CommandField ControlStyle-ForeColor="Blue" SelectText="Ver" ShowSelectButton="True" />
                                                            </Columns>
                                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">&nbsp;</td>
                                                    <td class="auto-style1" colspan="4">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1" colspan="5">
                                                        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Arial" Font-Size="Small" GridLines="Horizontal" ShowFooter="True" Width="548px">
                                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="CODIGO">
                                                                    <ControlStyle Width="120" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CANT_OC">
                                                                    <ControlStyle Width="100" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CANT_FACT">
                                                                    <ControlStyle Width="100" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UNIDAD">
                                                                    <ControlStyle Width="100" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                                                    <ControlStyle Width="500" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PRECIO">
                                                                    <ControlStyle Width="110" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IMPORTE">
                                                                    <ControlStyle Width="110" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IVA">
                                                                    <ControlStyle Width="110" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ISH">
                                                                    <ControlStyle Width="100" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IEPS">
                                                                    <ControlStyle Width="110" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Agregar Fila" />
                                                                        <asp:Button ID="ButtonClear" runat="server" OnClick="ButtonClear_Click" Text="Limpiar Tabla" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1"></td>
                                                    <td class="auto-style1" colspan="4"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">
                                                        <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="11pt" Text="FECHA:"></asp:Label>
                                                    </td>
                                                    <td colspan="4" rowspan="6">
                                                        <asp:Calendar ID="Calendar3" runat="server" BackColor="#F0E5D7" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Arial" Font-Size="9pt" ForeColor="black" Height="180px" Width="200px">
                                                            <DayHeaderStyle BackColor="#4781A8" Font-Bold="True" Font-Size="7pt" />
                                                            <NextPrevStyle VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#F0E5D7" />
                                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                            <SelectorStyle BackColor="#CCCCCC" />
                                                            <TitleStyle BackColor="#01558C" BorderColor="Black" Font-Bold="True" ForeColor="#FFFFFF" />
                                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <WeekendDayStyle BackColor="#91AAC0" />
                                                        </asp:Calendar>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Content/btn_regisart.png" OnClick="ImageButton18_Click" Style="height: 47px" />
                                            &nbsp; &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                        <br />
                        &nbsp;&nbsp; &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
