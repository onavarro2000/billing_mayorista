<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="generar_factura_frm.aspx.cs" Inherits="billing_mayorista.pages.generar_factura_frm" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style4 {
            text-align: center;
        }
        .auto-style5 {
            width: auto;
            border-style: solid;
            border-width: 4px;
        }
        .auto-style6 {
            width: 51px;
        }
        </style>
    <link href="../Content/facturacion.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <div>
            <table align="center" class="auto-style5">
                <tr>
                    <td class="auto-style4" colspan="11">
                        <h1>Generación de factura.</h1>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td>Operador:</td>
                    <td>
                        <asp:DropDownList ID="lstOperador" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstOperador_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>Año</td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td>
                        <asp:DropDownList ID="lstAnio" runat="server">
                            <asp:ListItem>2017</asp:ListItem>
                            <asp:ListItem>2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                            <asp:ListItem>2027</asp:ListItem>
                            <asp:ListItem>2028</asp:ListItem>
                            <asp:ListItem>2029</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Mes:</td>
                    <td>
                        <asp:DropDownList ID="lstMes" runat="server">
                            <asp:ListItem Value="1">Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Setiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">Moneda</td>
                    <td>
                        <asp:DropDownList ID="lstMoneda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstMoneda_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td>Servicios a Facturar:</td>
                    <td>
                        <asp:CheckBox ID="chkIPTV" runat="server" Text="FTTH-IPTV" Visible="False" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkVoIP" runat="server" Text="FTTH-VoIP" Visible="False" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkData" runat="server" Text="FTTH-DATA" Visible="False" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style4" colspan="3">
                        <h4>Opciones.</h4>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkRF" runat="server" Text="FTTH-RF" Visible="False" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style8">
                        <asp:ImageButton ID="btnFacturar" runat="server" ImageUrl="~/imagenes/facturar_50x50.png" OnClick="btnFacturar_Click" ToolTip="Generar Factura" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">
                        <asp:ImageButton ID="btnExit" runat="server" ImageUrl="~/imagenes/exit_50x50.png" ToolTip="Retornar" PostBackUrl="~/Inicio_frm.aspx" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="11">
                        <div class="auto-style4">
                            <telerik:RadProgressManager ID="RadProgressManager1" Runat="server" />
                        </div>
                        <div class="auto-style4">
                        <telerik:RadProgressArea ID="RadProgressArea1" Runat="server"
                            Width="500px" RenderMode="Lightweight"
                            ProgressIndicators="TotalProgressPercent, RequestSize, FilesCountBar, FilesCount, FilesCountPercent, SelectedFilesCount, CurrentFileName, TimeElapsed" BackColor="#009900">
                        </telerik:RadProgressArea>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <p>
            </p>
        <asp:Panel ID="Panel1" runat="server">
            <table align="center" class="auto-style5">
                <tr>
                    <td class="auto-style4" colspan="7">
                        <h3>Exportar Factura.</h3>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td># Factura</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtNumeroFactura" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="validatorNumFactura" runat="server" ControlToValidate="txtNumeroFactura" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td># Fac. Coubicación</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtFactCoubicacion" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="validaFactCoubicacion" runat="server" ControlToValidate="txtFactCoubicacion" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Emitida el:</td>
                    <td>&nbsp;</td>
                    <td>
                        <telerik:RadDatePicker ID="txtEmitidaEl" Runat="server" Culture="en-US" FocusedDate="2017-01-01" MinDate="2017-01-01">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="validatorFechaEmision" runat="server" ControlToValidate="txtEmitidaEl" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>Vence el:</td>
                    <td>&nbsp;</td>
                    <td>
                        <telerik:RadDatePicker ID="txtVenceEl" Runat="server" Culture="en-US" FocusedDate="2017-01-01" MinDate="2017-01-01">
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="validatorFechaVencimiento" runat="server" ControlToValidate="txtVenceEl" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Periódo:</td>
                    <td>&nbsp;</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtPeriodo" runat="server" Width="330px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="validatorPeriodo" runat="server" ControlToValidate="txtPeriodo" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style4">
                        <h3>Opciones</h3>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="RadUploadProgressArea_rtl">
                        <asp:ImageButton ID="toXLS" runat="server" ImageUrl="~/imagenes/xls.png" OnClick="toXLS_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <p>
            <telerik:RadNotification  ID="popupNotificacion" runat="server" Text="Initial text" Position="Center"
                    AutoCloseDelay="0" Width="350" Title="Alerta" Animation="Slide" 
                    EnableRoundedCorners="true"  EnableShadow="true">
            </telerik:RadNotification>
            </p>
        <table align="center" class="auto-style5">
            <tr>
                <td colspan="3">
                    <h2>Mensajes del sistema.</h2>
                </td>
            </tr>
            <tr>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMensajeSistema" runat="server" ForeColor="#990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsjData" runat="server" ForeColor="#990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsjIPTV" runat="server" ForeColor="#990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsjVoIP" runat="server" ForeColor="#990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsjRF" runat="server" ForeColor="#990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsjONT" runat="server" ForeColor="#990000"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
