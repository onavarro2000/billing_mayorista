<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acercaDe_frm.aspx.cs" Inherits="billing_mayorista.pages.acercaDe_frm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/facturacion.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="40%" algin="center">
    <tr>
      <td style="background-color: royalblue;" colspan="2">
        <p style="color: white;">Acerca de</p>
      </td>
    </tr>
    <tr >
      <td>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/osvaldo.jpg" />
      </td>
       <td>
          &copy;Realizado por Osvaldo Navarro Navarro.
         <br/>
         Fecha de creación: Enero, 03, 2017.
         <br/>
         2550-6800 Ext 572.
         

      </td>
    </tr>
  </table>
        </div>
        <p>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/exit_50x50.png" PostBackUrl="~/Inicio_frm.aspx" />
        </p>
    </form>
</body>
</html>
