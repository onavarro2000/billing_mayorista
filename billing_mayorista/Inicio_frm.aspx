<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio_frm.aspx.cs" Inherits="billing_mayorista.Inicio_frm" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style6 {
            width: auto;
            border: 2px solid #000000;
        }
        .auto-style7 {
            text-align: center;
        }
        </style>

    <link href="Content/facturacion.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">

 
        <div>

        </div>
        <table align="center" class="auto-style6">
            <tr>
                <td>
                    <asp:Menu ID="mnu_principal" runat="server" BackColor="#B5C7DE" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" Orientation="Horizontal" StaticSubMenuIndent="10px">
                        <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#B5C7DE" />
                        <DynamicSelectedStyle BackColor="#507CD1" />
                        <Items>
                            <asp:MenuItem Text="Catálogo" Value="Catalogo">
                                <asp:MenuItem Text="Feriados" Value="Feriado" NavigateUrl="~/pages/feriados_frm.aspx"></asp:MenuItem>
                                <asp:MenuItem Text="Operadores" Value="Operador"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Facturar" Value="Facturar">
                                <asp:MenuItem Text="Generar" Value="Generar" NavigateUrl="~/pages/generar_factura_frm.aspx" ToolTip="Generar la factura."></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Acerca de .." Value="Acerca " NavigateUrl="~/pages/acercaDe_frm.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Salir" Value="Salir" NavigateUrl="http://intranet/"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="#507CD1" />
                    </asp:Menu>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/jasec_info.png" />
                </td>
            </tr>
            <tr>
                <td class="auto-style7">
                    <h1>Bienvenido al Sistema de Facturación Mayorista.</h1></td>
            </tr>
        </table>
    </form>
</body>
</html>
