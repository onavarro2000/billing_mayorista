<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feriados_frm.aspx.cs" Inherits="billing_mayorista.pages.feriados_frm" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/facturacion.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style8 {
            width: auto;
        }
        .auto-style9 {
            text-align: center;
        }
        </style>

    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Desea eliminar día feriado?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" >


        <table align="center" cellpadding="4" class="auto-style8">
            <tr>
                <td colspan="6">
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
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" height="200px" width="300px">
                        <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server">
                        </telerik:RadAjaxManager>
                        <table align="center" cellpadding="5" class="auto-style8">
                            <tr>
                                <td class="auto-style9" colspan="6">
                                    <h2>Feriados</h2>
                                </td>
                            </tr>
                            <tr>
                                <td>Año:</td>
                                <td>
                                    <asp:DropDownList ID="cboFechas" runat="server" DataTextField="Num_Anno" DataValueField="Num_Anno" Width="180px">
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                        <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2019</asp:ListItem>
                                        <asp:ListItem>2020</asp:ListItem>
                                        <asp:ListItem>2021</asp:ListItem>
                                        <asp:ListItem>2022</asp:ListItem>
                                        <asp:ListItem Value="2023">2023</asp:ListItem>
                                        <asp:ListItem>2024</asp:ListItem>
                                        <asp:ListItem>2025</asp:ListItem>
                                        <asp:ListItem>2026</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar" Width="70px" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Fecha:</td>
                                <td>
                                    <telerik:RadDatePicker ID="dtFecha" runat="server" Width="180px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Motivo:</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtDescripcion" runat="server" Height="45px" TextMode="MultiLine" Width="264px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:ImageButton ID="btnAdd" runat="server" Height="32px" ImageUrl="~/imagenes/add.png" OnClick="btnAdd_Click" ToolTip="Guardar Cambios" Width="32px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnEliminar" runat="server" Height="32px" ImageUrl="~/Imagenes/DeleteRed.png" OnClick="btnEliminar_Click" OnClientClick="Confirm()" ToolTip="Eliminar Registro Seleccionado" Width="32px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFeriados" runat="server" Height="32px" ImageUrl="~/Imagenes/Salir2.png" PostBackUrl="~/Inicio_frm.aspx" ToolTip="Ir al formulario Principal" Width="32px" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="5">
                                    <asp:Label ID="Label8" runat="server" Font-Size="10pt" Text="Para Eliminar, primero tiene que seleccionar el registro y luego presionar el boton en forma de X"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <telerik:RadGrid ID="gvDatos" runat="server" AllowFilteringByColumn="True" AllowSorting="True" AutoGenerateColumns="False" Culture="es-ES">
                                        <ClientSettings EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                        <MasterTableView>
                                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="dia" DataFormatString="{0:d}" FilterControlAltText="Filter Fecha column" HeaderText="Fecha del Feriado" UniqueName="Fecha">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="dsc" FilterControlAltText="Filter Observaciones column" HeaderText="Observaciones" UniqueName="Observaciones">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <FilterMenu EnableImageSprites="False">
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadAjaxPanel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
&nbsp;<div >

        </div>
    </form>
</body>
</html>
