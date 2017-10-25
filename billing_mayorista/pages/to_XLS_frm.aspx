<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="to_XLS_frm.aspx.cs" Inherits="billing_mayorista.pages.to_XLS_frm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>
                <asp:Button ID="btnServicios" runat="server" Text="Servicios" Width="180px" OnClick="btnServicios_Click" />
                <asp:Button ID="btnCoubicacion" runat="server" Text="Coubicación" Width="180px" />
                <asp:Button ID="btdDetalleSrv" runat="server" Text="Detalle Servicios Facturados" Width="180px" OnClick="btdDetalleSrv_Click" />
            </h1>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundField DataField="acceso" HeaderText="Numero de Referncia" />
                    <asp:BoundField DataField="contacto" HeaderText="Nombre del cliente" />
                    <asp:BoundField DataField="medidor" HeaderText="Numero de medidor" />
                    <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de servicio" />
                    <asp:BoundField DataField="fecha_instalacion" HeaderText="Fecha Instalacion" />
                    <asp:BoundField DataField="fecha_inicio_fact" HeaderText="Inicio Facturacion" />
                    <asp:BoundField DataField="fecha_desconexcion" HeaderText="Fecha de Retiro" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="monto_antes_iv" HeaderText="Precio AntesIV" />
                    <asp:BoundField DataField="monto_iv" HeaderText="Precio IV" />
                    <asp:BoundField DataField="monto_total" HeaderText="Precio Total" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridDesgloseFactura" runat="server" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundField DataField="servicio" HeaderText="Servicio" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="total_srv" HeaderText="Total de servicios" />
                    <asp:BoundField DataField="monto_antes_iv" HeaderText="Precio AntesIV" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
