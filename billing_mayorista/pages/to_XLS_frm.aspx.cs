using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace billing_mayorista.pages
{
    public partial class to_XLS_frm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            factura_a_xls();
        }

        private void factura_a_xls()
        {
            string vMedidorNext = "0";
            string vMedidorAct = "0";
            int i = 0;
            int numFil = 1;
            int tot_registros = 0;
            int vTotRegXMed = 1;
            double vMtoAntesIV = 0;
            double vIV = 0.13;
            double vMtoIV = 0;
            double vMtoTot = 0;
            double vMtoONT = 0;
            double vMtoSubtotal = 0;

            Clases.Facturar facturar = new Clases.Facturar();
            facturar.NomOperador = Session["OPERADOR"].ToString();
            facturar.CicloFactura = Session["CICLOFACT"].ToString();

            facturar.TipoConsulta = "desglose_x_cliente";
            facturar.CargarFacturacion();

            this.GridView1.DataSource = facturar.Tabla;

            this.GridView1.DataBind();

            tot_registros = GridView1.Rows.Count;

            //Crear un nuevo WorkBook
            XLWorkbook wb = new XLWorkbook();


            #region desglose_x_cliente
            //Agrega workSeet del Desglose por cliente
            var mWSheet1 = wb.Worksheets.Add("Desglose por cliente").SetTabColor(XLColor.Red);



            //Encabezado de la factura
            numFil = numFil + 1;
            mWSheet1.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet1.Cell("C" + numFil).Value = "Junta Administrativa del Servicio Eléctrico de Cartago";
            mWSheet1.Range("C" + numFil + ":I" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet1.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet1.Cell("C" + numFil).Value = "Cédula Jurídica 3-007-045087";
            mWSheet1.Range("C" + numFil + ":I" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet1.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet1.Cell("C" + numFil).Value = "Teléfono: 2550-6800  Apdo.179-7050 Cartago - Costa Rica";
            mWSheet1.Range("C" + numFil + ":I" + numFil).Row(1).Merge();
            numFil = numFil + 1;

            mWSheet1.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet1.Cell("C" + numFil).Value = "Gestor de Infraestructura de Telecomunicaciones";
            mWSheet1.Range("C" + numFil + ":I" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet1.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet1.Cell("C" + numFil).Value = "Informe de Facturación Desglosado";
            mWSheet1.Range("C" + numFil + ":I" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet1.Cell("B" + numFil).Value = "Operador:";
            mWSheet1.Cell("C" + numFil).Value = "ICE";
            mWSheet1.Cell("E" + numFil).Value = "Código Operador: 100014017";
            numFil = numFil + 1;
            mWSheet1.Cell("B" + numFil).Value = "ID Operador:";
            mWSheet1.Cell("C" + numFil).Value = "100014017";
            mWSheet1.Cell("E" + numFil).Value = "Teléfono: 1115";
            numFil = numFil + 1;
            mWSheet1.Cell("B" + numFil).Value = "Dirección: Sabana Sur, Contiguo a Contraloria";
            numFil = numFil + 1;
            mWSheet1.Cell("B" + numFil).Value = "San José, Costa Rica.";
            numFil = numFil + 1;
            mWSheet1.Cell("B" + numFil).Value = "Periodo de Facturación: " + Session["PERIODOFACTURACION"].ToString();

            //Detalle
            numFil = numFil + 2;
            mWSheet1.Cell("B" + numFil).Value = "ITEM";
            mWSheet1.Cell("C" + numFil).Value = "Referencia";
            mWSheet1.Cell("D" + numFil).Value = "Nombre Cliente";
            mWSheet1.Cell("E" + numFil).Value = "Medidor";
            mWSheet1.Cell("F" + numFil).Value = "Servicio Contratado";
            mWSheet1.Cell("G" + numFil).Value = "Fecha Instalación";
            mWSheet1.Cell("H" + numFil).Value = "Inicio Facturación";
            mWSheet1.Cell("I" + numFil).Value = "Fecha Retiro";
            mWSheet1.Cell("J" + numFil).Value = "Cantidad";
            mWSheet1.Cell("K" + numFil).Value = "Precio Total";
            numFil = numFil + 1;
            while (i < tot_registros)
            {
                /* if (vMedidorAct == "01-00022112")
                 {
                   numFil = numFil + 1 ;
                   numFil = numFil - 1;
                 }*/
                if (vMedidorAct != vMedidorNext)
                {


                    mWSheet1.Cell("J" + numFil).Value = "Sub-Total";
                    mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                    mWSheet1.Cell("K" + numFil).Value = vMtoAntesIV;

                    numFil = numFil + 1;
                    mWSheet1.Cell("J" + numFil).Value = "Impuesto 13%";
                    vMtoIV = (vMtoAntesIV - vMtoONT) * vIV;
                    vMtoIV = Math.Round(vMtoIV, 2);
                    mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                    mWSheet1.Cell("K" + numFil).Value = vMtoIV;

                    numFil = numFil + 1;
                    mWSheet1.Cell("J" + numFil).Value = "Total";
                    vMtoTot = vMtoAntesIV + vMtoIV;
                    vMtoTot = Math.Round(vMtoTot, 2);
                    mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                    mWSheet1.Cell("K" + numFil).Value = vMtoTot;

                    numFil = numFil + 2;


                    mWSheet1.Cell("C" + numFil).Value = "Referencia";
                    mWSheet1.Cell("D" + numFil).Value = "Nombre Cliente";
                    mWSheet1.Cell("E" + numFil).Value = "Medidor";
                    mWSheet1.Cell("F" + numFil).Value = "Servicio Contratado";
                    mWSheet1.Cell("G" + numFil).Value = "Fecha Instalación";
                    mWSheet1.Cell("H" + numFil).Value = "Fecha Facturación";
                    mWSheet1.Cell("I" + numFil).Value = "Fecha Retiro";
                    mWSheet1.Cell("J" + numFil).Value = "Cantidad";
                    mWSheet1.Cell("K" + numFil).Value = "Precio Total";
                    numFil = numFil + 1;
                    vMedidorAct = vMedidorNext;

                    //Incializa totales
                    vTotRegXMed = 1;
                    vMtoAntesIV = 0;

                    vMtoIV = 0;
                    vMtoTot = 0;
                    vMtoONT = 0;

                }

                mWSheet1.Cell("B" + numFil).Value = vTotRegXMed;
                mWSheet1.Cell("C" + numFil).Value = GridView1.Rows[i].Cells[0].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("D" + numFil).Value = GridView1.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("E" + numFil).Value = GridView1.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("F" + numFil).Value = GridView1.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("G" + numFil).Value = GridView1.Rows[i].Cells[4].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("H" + numFil).Value = GridView1.Rows[i].Cells[5].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("I" + numFil).Value = GridView1.Rows[i].Cells[6].Text.ToString().Replace("&nbsp;", "");
                mWSheet1.Cell("J" + numFil).Value = GridView1.Rows[i].Cells[7].Text.ToString().Replace("&nbsp;", "");

                mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet1.Cell("K" + numFil).Value = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.ToString().Replace("&nbsp;", "0"));

                vTotRegXMed = vTotRegXMed + 1;
                vMtoAntesIV = vMtoAntesIV + Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.ToString().Replace("&nbsp;", "0"));

                if (GridView1.Rows[i].Cells[3].Text.ToString() == "ONT")
                {
                    vMtoONT = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.ToString());

                }


                vMedidorAct = GridView1.Rows[i].Cells[2].Text.ToString();

                i = i + 1;
                numFil = numFil + 1;

                if (i < tot_registros)
                {
                    vMedidorNext = GridView1.Rows[i].Cells[2].Text.ToString();

                }



            }
            if (tot_registros > 1)
            {
                mWSheet1.Cell("J" + numFil).Value = "Sub-Total";
                mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet1.Cell("K" + numFil).Value = vMtoAntesIV;

                numFil = numFil + 1;
                mWSheet1.Cell("J" + numFil).Value = "Impuesto 13%";
                vMtoIV = (vMtoAntesIV - vMtoONT) * vIV;
                vMtoIV = Math.Round(vMtoIV, 2);
                mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet1.Cell("K" + numFil).Value = vMtoIV;

                numFil = numFil + 1;
                mWSheet1.Cell("J" + numFil).Value = "Total";
                vMtoTot = vMtoAntesIV + vMtoIV;
                vMtoTot = Math.Round(vMtoTot, 2);
                mWSheet1.Cell("K" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet1.Cell("K" + numFil).Value = vMtoTot;

            }

            //Repinto con lineas mas gruesas un rango de datos
            mWSheet1.Range("B1:K4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet1.Range("B5:K11").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet1.Range("B12:K" + numFil).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            mWSheet1.Columns().AdjustToContents();

            #endregion desglose_x_cliente

            #region factura_detallada

            int vCantidadAnt = 0;
            int vCantidadAct = 0;
            int rowIniProd = 0;
            int rowFinProd = 0;
            string vProductoAnt = null;
            string vProductoAct = null;

            vTotRegXMed = 0;
            vMtoAntesIV = 0;
            vMtoIV = 0;
            vMtoTot = 0;


            //Agrega workSeet del Desglose por cliente
            var mWSheet2 = wb.Worksheets.Add("Factura Detallada").SetTabColor(XLColor.Blue);




            facturar.TipoConsulta = "factura_detallada";
            facturar.CargarFacturacion();

            this.GridView1.DataSource = facturar.Tabla;



            this.GridView1.DataBind();

            tot_registros = GridView1.Rows.Count;


            //Encabezado de la factura
            numFil = 1;
            numFil = numFil + 1;
            mWSheet2.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet2.Cell("C" + numFil).Value = "Junta Administrativa del Servicio Eléctrico de Cartago";
            mWSheet2.Range("C" + numFil + ":G" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet2.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet2.Cell("C" + numFil).Value = "Cédula Jurídica 3-007-045087";
            mWSheet2.Range("C" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet2.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet2.Cell("C" + numFil).Value = "Teléfono: 2550-6800  Apdo.179-7050 Cartago - Costa Rica";
            mWSheet2.Range("C" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;

            mWSheet2.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet2.Cell("C" + numFil).Value = "Gestor de Infraestructura de Telecomunicaciones";
            mWSheet2.Range("C" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet2.Cell("C" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet2.Cell("C" + numFil).Value = "Informe de Facturación Detallado";
            mWSheet2.Range("C" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet2.Cell("B" + numFil).Value = "Operador:";
            mWSheet2.Cell("C" + numFil).Value = "ICE";
            mWSheet2.Cell("E" + numFil).Value = "Código Operador: 100014017";
            numFil = numFil + 1;
            mWSheet2.Cell("B" + numFil).Value = "ID Operador:";
            mWSheet2.Cell("C" + numFil).Value = "100014017";
            mWSheet2.Cell("E" + numFil).Value = "Teléfono: 1115";
            numFil = numFil + 1;
            mWSheet2.Cell("B" + numFil).Value = "Dirección: Sabana Sur, Contiguo a Contraloria";
            numFil = numFil + 1;
            mWSheet2.Cell("B" + numFil).Value = "San José, Costa Rica.";
            numFil = numFil + 1;
            mWSheet2.Cell("B" + numFil).Value = "Periodo de Facturación: " + Session["PERIODOFACTURACION"].ToString();


            //Detalle
            numFil = numFil + 2;
            //mWSheet1.Cell("A" + numFil).Value = "Datos";
            mWSheet2.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet2.Cell("B" + numFil).Value = "Nombre Cliente";
            mWSheet2.Cell("C" + numFil).Style.Font.Bold = true;
            mWSheet2.Cell("C" + numFil).Value = "Referencia";
            mWSheet2.Cell("D" + numFil).Style.Font.Bold = true;
            mWSheet2.Cell("D" + numFil).Value = "Medidor";
            mWSheet2.Cell("E" + numFil).Style.Font.Bold = true;
            mWSheet2.Cell("E" + numFil).Value = "Precio";
            mWSheet2.Cell("F" + numFil).Style.Font.Bold = true;
            mWSheet2.Cell("F" + numFil).Value = "Impuesto";
            mWSheet2.Cell("G" + numFil).Style.Font.Bold = true;
            mWSheet2.Cell("G" + numFil).Value = "Monto";

            i = 0;
            while (i < tot_registros)
            {

                vProductoAct = GridView1.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "");
                vCantidadAct = Convert.ToInt32(GridView1.Rows[i].Cells[7].Text.ToString().Replace("&nbsp;", "0"));

                if (vProductoAnt != vProductoAct)
                {
                    mWSheet2.Cell("A" + numFil).Style.Font.Bold = true;
                    mWSheet2.Cell("A" + numFil).Value = vProductoAct;
                    vProductoAnt = vProductoAct;

                    if (vMtoAntesIV > 0)
                    {
                        mWSheet2.Cell("E" + numFil).Style.NumberFormat.Format = "$###,###.00";
                        mWSheet2.Cell("E" + numFil).Style.Font.FontColor = XLColor.Red;
                        mWSheet2.Cell("E" + numFil).Value = vMtoAntesIV;
                        mWSheet2.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
                        mWSheet2.Cell("G" + numFil).Style.Font.FontColor = XLColor.Red;
                        mWSheet2.Cell("G" + numFil).Value = vMtoTot;
                    }
                    rowIniProd = numFil;
                    numFil = numFil + 1;
                    vTotRegXMed = 1;
                    vMtoAntesIV = 0;
                    vMtoTot = 0;
                    if (rowFinProd > 0)
                    {
                        mWSheet2.Range("A" + rowIniProd + ":G" + rowFinProd).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; ;
                        rowIniProd = rowFinProd;
                    }
                }

                if (vCantidadAnt != vCantidadAct)
                {
                    if (vMtoAntesIV > 0)
                    {
                        mWSheet2.Cell("E" + numFil).Style.NumberFormat.Format = "$###,###.00";
                        mWSheet2.Cell("E" + numFil).Style.Font.FontColor = XLColor.Red;
                        mWSheet2.Cell("E" + numFil).Value = vMtoAntesIV;
                        mWSheet2.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
                        mWSheet2.Cell("G" + numFil).Style.Font.FontColor = XLColor.Red;
                        mWSheet2.Cell("G" + numFil).Value = vMtoTot;
                    }



                    if (vProductoAct == "FTTH-DATA")
                    {
                        mWSheet2.Cell("A" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        mWSheet2.Cell("A" + numFil).Style.Font.Bold = true;
                        mWSheet2.Cell("A" + numFil).Value = vCantidadAct + " Mbs";
                    }

                    if (vProductoAct == "FTTH-IPTV")
                    {
                        mWSheet2.Cell("A" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        mWSheet2.Cell("A" + numFil).Style.Font.Bold = true;
                        mWSheet2.Cell("A" + numFil).Value = vCantidadAct + " TV";
                    }

                    if (vProductoAct == "FTTH-VoIP")
                    {
                        mWSheet2.Cell("A" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        mWSheet2.Cell("A" + numFil).Style.Font.Bold = true;
                        mWSheet2.Cell("A" + numFil).Value = vCantidadAct + " TEL";
                    }

                    vCantidadAnt = vCantidadAct;
                    numFil = numFil + 1;
                    vTotRegXMed = 1;
                    vMtoAntesIV = 0;
                    vMtoTot = 0;
                }

                mWSheet2.Cell("A" + numFil).Value = vTotRegXMed;
                mWSheet2.Cell("B" + numFil).Value = GridView1.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "");
                mWSheet2.Cell("C" + numFil).Value = GridView1.Rows[i].Cells[0].Text.ToString().Replace("&nbsp;", "");
                mWSheet2.Cell("D" + numFil).Value = GridView1.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "");

                mWSheet2.Cell("E" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet2.Cell("E" + numFil).Value = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.ToString().Replace("&nbsp;", "0"));

                mWSheet2.Cell("F" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet2.Cell("F" + numFil).Value = Convert.ToDouble(GridView1.Rows[i].Cells[9].Text.ToString().Replace("&nbsp;", "0"));

                mWSheet2.Cell("G" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet2.Cell("G" + numFil).Value = Convert.ToDouble(GridView1.Rows[i].Cells[10].Text.ToString().Replace("&nbsp;", "0"));

                vMtoAntesIV = vMtoAntesIV + Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.ToString().Replace("&nbsp;", "0"));
                vMtoTot = vMtoTot + Convert.ToDouble(GridView1.Rows[i].Cells[10].Text.ToString().Replace("&nbsp;", "0"));
                numFil = numFil + 1;
                rowFinProd = numFil;


                vTotRegXMed = vTotRegXMed + 1;

                i = i + 1;

            }

            if (vMtoAntesIV > 0)
            {
                mWSheet2.Cell("E" + numFil).Style.NumberFormat.Format = "$###,###.00";
                mWSheet2.Cell("E" + numFil).Style.Font.FontColor = XLColor.Red;
                mWSheet2.Cell("E" + numFil).Value = vMtoAntesIV;

                mWSheet2.Cell("G" + numFil).Style.NumberFormat.Format = "$0.00";
                mWSheet2.Cell("G" + numFil).Style.Font.FontColor = XLColor.Red;
                mWSheet2.Cell("G" + numFil).Value = vMtoTot;
            }

            numFil = numFil + 1;

            //Repinto con lineas mas gruesas un rango de datos
            mWSheet2.Range("A1:G4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet2.Range("A5:G11").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet2.Range("A12:G" + numFil).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            mWSheet2.Columns().AdjustToContents();
            #endregion factura_detallada

            #region desglose_factura

            //Agrega workSeet del desglose de la factura
            var mWSheet3 = wb.Worksheets.Add("Desglose de Factura").SetTabColor(XLColor.Green);
            double vMtoONT2 = 0;
            int vTotONT = 0;

            facturar.TipoConsulta = "desglose_factura";
            facturar.CargarFacturacion();



            this.GridDesgloseFactura.DataSource = facturar.Tabla;

            this.GridDesgloseFactura.DataBind();

            tot_registros = GridDesgloseFactura.Rows.Count;

            //Encabezado de la factura
            numFil = 1;
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet3.Cell("B" + numFil).Value = "Junta Administrativa del Servicio Eléctrico de Cartago";
            mWSheet3.Range("B" + numFil + ":G" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet3.Cell("B" + numFil).Value = "Cédula Jurídica 3-007-045087";
            mWSheet3.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet3.Cell("B" + numFil).Value = "Teléfono: 2550-6800  Apdo.179-7050 Cartago - Costa Rica";
            mWSheet3.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;

            mWSheet3.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet3.Cell("B" + numFil).Value = "Gestor de Infraestructura de Telecomunicaciones";
            mWSheet3.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet3.Cell("B" + numFil).Value = "Informe de Facturación Desglosado";
            mWSheet3.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "Operador:";
            mWSheet3.Cell("C" + numFil).Value = "ICE";
            mWSheet3.Cell("E" + numFil).Value = "Código Operador: 100014017";
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "ID Operador:";
            mWSheet3.Cell("C" + numFil).Value = "100014017";
            mWSheet3.Cell("E" + numFil).Value = "Teléfono: 1115";
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "Dirección: Sabana Sur, Contiguo a Contraloria";
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "San José, Costa Rica.";
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "Periodo de Facturación: " + Session["PERIODOFACTURACION"].ToString();

            //Detalle
            numFil = numFil + 2;
            //mWSheet1.Cell("A" + numFil).Value = "Datos";
            mWSheet3.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("B" + numFil).Value = "Detalle";
            mWSheet3.Cell("E" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("E" + numFil).Value = "Cantidad de Usuarios";
            mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("G" + numFil).Value = "Costo $";

            i = 0;
            vMtoAntesIV = 0;
            vProductoAnt = null;

            numFil = numFil + +1;

            while (i < tot_registros)
            {
                vProductoAct = GridDesgloseFactura.Rows[i].Cells[0].Text.ToString().Replace("&nbsp;", "");
                if (vProductoAct != "ONT")
                {
                    if (vProductoAnt != vProductoAct)
                    {
                        if (vMtoAntesIV > 0)
                        {

                            mWSheet3.Cell("F" + numFil).Style.Font.Bold = true;
                            mWSheet3.Cell("F" + numFil).Value = "Total";
                            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
                            mWSheet3.Cell("G" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
                            mWSheet3.Cell("G" + numFil).Value = vMtoAntesIV;
                            vMtoSubtotal = vMtoSubtotal + vMtoAntesIV;
                            vMtoSubtotal = Math.Round(vMtoSubtotal, 2);
                            numFil = numFil + 2;


                        }
                        mWSheet3.Cell("A" + numFil).Style.Font.Bold = true;
                        mWSheet3.Cell("A" + numFil).Value = vProductoAct;
                        vProductoAnt = vProductoAct;
                        numFil = numFil + 2;
                        vMtoAntesIV = 0;
                    }

                    if (vProductoAct == "DATA")
                    {
                        mWSheet3.Cell("A" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "") + " Mega";

                    }
                    if (vProductoAct == "IPTV")
                    {
                        mWSheet3.Cell("A" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "") + " TV";

                    }
                    if (vProductoAct == "VoIP")
                    {
                        mWSheet3.Cell("A" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "") + " TELF";

                    }
                    //mWSheet3.Cell("A" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "");
                    mWSheet3.Cell("E" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "");
                    mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
                    mWSheet3.Cell("G" + numFil).Value = Convert.ToDouble(GridDesgloseFactura.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "0"));

                    vMtoAntesIV = vMtoAntesIV + Convert.ToDouble(mWSheet3.Cell("G" + numFil).Value = Convert.ToDouble(GridDesgloseFactura.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "0")));
                    vMtoAntesIV = Math.Round(vMtoAntesIV, 2);

                    numFil = numFil + 1;

                }
                else
                {
                    vMtoONT2 = Convert.ToDouble(mWSheet3.Cell("G" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "0"));
                    vMtoONT2 = Math.Round(vMtoONT2, 2);
                    vTotONT = Convert.ToInt32(GridDesgloseFactura.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "0"));
                    

                }


                i = i + 1;
            }

            if (vMtoAntesIV > 0)
            {

                mWSheet3.Cell("F" + numFil).Style.Font.Bold = true;
                mWSheet3.Cell("F" + numFil).Value = "Total";
                mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
                mWSheet3.Cell("G" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
                mWSheet3.Cell("G" + numFil).Value = vMtoAntesIV;
                vMtoSubtotal = vMtoSubtotal + vMtoAntesIV;
                vMtoSubtotal = Math.Round(vMtoSubtotal, 2);
                numFil = numFil + 1;

            }
            vMtoAntesIV = 0;
            mWSheet3.Cell("A" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("A" + numFil).Value = "Servicios de Interconexión";
            numFil = numFil + 1;
            mWSheet3.Cell("A" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("A" + numFil).Value = "Servicio de Co-ubicación";
            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "Alquiler de Rack";
            mWSheet3.Cell("E" + numFil).Value = 1;
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet3.Cell("G" + numFil).Value = 743.97;
            vMtoAntesIV = vMtoAntesIV + 743.97;
            vMtoAntesIV = Math.Round(vMtoAntesIV, 2);

            numFil = numFil + 1;
            mWSheet3.Cell("B" + numFil).Value = "Energía Eléctrica";
            mWSheet3.Cell("E" + numFil).Value = 0;
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet3.Cell("G" + numFil).Value = 0;
            vMtoAntesIV = vMtoAntesIV + 0;
            vMtoAntesIV = Math.Round(vMtoAntesIV, 2);

            numFil = numFil + 1;
            mWSheet3.Cell("A" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("A" + numFil).Value = "Alquiler ONT";
            mWSheet3.Cell("E" + numFil).Value = vTotONT;
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet3.Cell("G" + numFil).Value = vMtoONT2;
            vMtoAntesIV = vMtoAntesIV + vMtoONT2;
            vMtoAntesIV = Math.Round(vMtoAntesIV, 2);

            numFil = numFil + 1;
            mWSheet3.Cell("F" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("F" + numFil).Value = "Total";
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet3.Cell("G" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("G" + numFil).Value = vMtoAntesIV;

            vMtoSubtotal = vMtoSubtotal + vMtoAntesIV;
            vMtoSubtotal = Math.Round(vMtoSubtotal, 2);
            mWSheet3.Range("A12:G" + numFil).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            numFil = numFil + 2;
            //mWSheet3.Cell("F" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("E" + numFil).Value = "Sub Total";
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            //mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("G" + numFil).Value = vMtoSubtotal;

            numFil = numFil + 1;
            //mWSheet3.Cell("F" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("E" + numFil).Value = "IV (13%)";
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            //mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
            vMtoIV = (vMtoSubtotal - vMtoONT2) * 0.13;
            vMtoIV = Math.Round(vMtoIV, 2);
            mWSheet3.Cell("G" + numFil).Value = vMtoIV;

            numFil = numFil + 1;
            //mWSheet3.Cell("F" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("E" + numFil).Value = "Total";
            mWSheet3.Cell("G" + numFil).Style.Font.Bold = true;
            mWSheet3.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet3.Cell("G" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            vMtoTot = (vMtoSubtotal - vMtoONT2);
            vMtoTot = Math.Round(vMtoTot, 2);
            mWSheet3.Cell("G" + numFil).Value = vMtoSubtotal + vMtoIV;


            mWSheet3.Range("A1:G4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet3.Range("A5:G11").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            mWSheet3.Columns().AdjustToContents();

            #endregion desglose_factura

            #region factura2
            var mWSheet4 = wb.Worksheets.Add("Factura 2").SetTabColor(XLColor.Purple);
            //Encabezado de la factura
            numFil = 1;
            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet4.Cell("B" + numFil).Value = "Junta Administrativa del Servicio Eléctrico de Cartago";
            mWSheet4.Range("B" + numFil + ":G" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet4.Cell("B" + numFil).Value = "Cédula Jurídica 3-007-045087";
            mWSheet4.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet4.Cell("B" + numFil).Value = "Teléfono: 2550-6800  Apdo.179-7050 Cartago - Costa Rica";
            mWSheet4.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;

            mWSheet4.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet4.Cell("B" + numFil).Value = "Gestor de Infraestructura de Telecomunicaciones";
            mWSheet4.Range("B" + numFil + ":G" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet4.Cell("B" + numFil).Value = "Numero de Factura";
            mWSheet4.Cell("C" + numFil).Value = Session["NUMFACTURACOUBICACION"].ToString(); //Session["NUMFACTURA"].ToString();

            mWSheet4.Cell("E" + numFil).Value = "Código Operador:";
            mWSheet4.Cell("F" + numFil).Value = "100014017";


            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Value = "Operador:";
            mWSheet4.Cell("C" + numFil).Value = "ICE";

            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Value = "ID Operador:";
            mWSheet4.Cell("C" + numFil).Value = "100014017";

            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Value = "Dirección: Sabana Sur, Contiguo a Contraloria";
            mWSheet4.Cell("F" + numFil).Value = "Fecha de Emision";
            mWSheet4.Cell("H" + numFil).Value = Session["FECHAEMISION"].ToString();

            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Value = "San José, Costa Rica.";
            mWSheet4.Cell("F" + numFil).Value = "Fecha de Vencimiento";
            mWSheet4.Cell("H" + numFil).Value = Session["FECHAVENCIMIENTO"].ToString();

            numFil = numFil + 1;
            mWSheet4.Cell("B" + numFil).Value = "Periodo de Facturación: " + Session["PERIODOFACTURACION"].ToString();


            //Detalle
            numFil = numFil + 2;
            //mWSheet1.Cell("A" + numFil).Value = "Datos";
            mWSheet4.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet4.Cell("B" + numFil).Value = "Detalle";
            mWSheet4.Cell("E" + numFil).Style.Font.Bold = true;
            mWSheet4.Cell("E" + numFil).Value = "Cantidad Unit";
            mWSheet4.Cell("G" + numFil).Style.Font.Bold = true;
            mWSheet4.Cell("G" + numFil).Value = "Costo $";

            numFil = numFil + 2;
            mWSheet4.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet4.Cell("B" + numFil).Value = "Servicios de Interconexión";
            mWSheet4.Cell("E" + numFil).Value = 0;
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Value = 0;

            numFil = numFil + 2;

            mWSheet4.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet4.Cell("B" + numFil).Value = "Servicio de Co-ubicación";
            mWSheet4.Cell("E" + numFil).Value = 1;
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Value = 743.97;

            numFil = numFil + 2;

            mWSheet4.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet4.Cell("B" + numFil).Value = "Otros Servicios";

            numFil = numFil + 1;


            vMtoAntesIV = 743.97;
            vMtoAntesIV = Math.Round(vMtoAntesIV, 2);
            mWSheet4.Cell("F" + numFil).Value = "SubTotal";
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Value = vMtoAntesIV;

            numFil = numFil + 1;

            vMtoIV = vMtoAntesIV * 0.13;
            vMtoIV = Math.Round(vMtoIV, 2);
            mWSheet4.Cell("F" + numFil).Value = "IV (13%)";
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Value = vMtoIV;

            numFil = numFil + 1;
            vMtoSubtotal = vMtoAntesIV + vMtoIV;
            vMtoSubtotal = Math.Round(vMtoSubtotal, 2);
            mWSheet4.Cell("F" + numFil).Value = "SubTotal";
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            mWSheet4.Cell("G" + numFil).Value = vMtoSubtotal;

            numFil = numFil + 1;
            mWSheet4.Cell("F" + numFil).Value = "Multas";
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Value = 0;

            numFil = numFil + 2;
            mWSheet4.Cell("F" + numFil).Value = "Total";
            mWSheet4.Cell("G" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet4.Cell("G" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            mWSheet4.Cell("G" + numFil).Value = vMtoSubtotal;
            numFil = numFil + 1;


            mWSheet4.Range("B1:H4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet4.Range("B5:H11").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet4.Range("B6:H19").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet4.Range("B20:H25").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            mWSheet4.Columns().AdjustToContents();



            #endregion factura2

            #region factura1

            facturar.TipoConsulta = "factura1";
            facturar.CargarFacturacion();

            this.GridDesgloseFactura.DataSource = facturar.Tabla;

            this.GridDesgloseFactura.DataBind();

            tot_registros = GridDesgloseFactura.Rows.Count;

            var mWSheet5 = wb.Worksheets.Add("Factura 1").SetTabColor(XLColor.PurplePizzazz);

            
            //Encabezado de la factura
            numFil = 1;
            numFil = numFil + 1;




            mWSheet5.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet5.Cell("B" + numFil).Value = "Junta Administrativa del Servicio Eléctrico de Cartago";
            mWSheet5.Range("B" + numFil + ":G" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet5.Cell("B" + numFil).Value = "Cédula Jurídica 3-007-045087";
            mWSheet5.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet5.Cell("B" + numFil).Value = "Teléfono: 2550-6800  Apdo.179-7050 Cartago - Costa Rica";
            mWSheet5.Range("B" + numFil + ":G" + numFil).Row(1).Merge();
            numFil = numFil + 1;

            mWSheet5.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet5.Cell("B" + numFil).Value = "Gestor de Infraestructura de Telecomunicaciones";
            mWSheet5.Range("B" + numFil + ":G" + numFil).Row(1).Merge();

            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            mWSheet5.Cell("B" + numFil).Value = "Numero de Factura";
            mWSheet5.Cell("C" + numFil).Value = Session["NUMFACTURA"].ToString();

            mWSheet5.Cell("E" + numFil).Value = "Código Operador:";
            mWSheet5.Cell("F" + numFil).Value = "100014017";


            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Value = "Operador:";
            mWSheet5.Cell("C" + numFil).Value = "ICE";

            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Value = "ID Operador:";
            mWSheet5.Cell("C" + numFil).Value = "100014017";

            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Value = "Dirección: Sabana Sur, Contiguo a Contraloria";
            mWSheet5.Cell("F" + numFil).Value = "Fecha de Emision";
            mWSheet5.Cell("H" + numFil).Value = Session["FECHAEMISION"].ToString();

            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Value = "San José, Costa Rica.";
            mWSheet5.Cell("F" + numFil).Value = "Fecha de Vencimiento";
            mWSheet5.Cell("H" + numFil).Value = Session["FECHAVENCIMIENTO"].ToString();

            numFil = numFil + 1;
            mWSheet5.Cell("B" + numFil).Value = "Periodo de Facturación: " + Session["PERIODOFACTURACION"].ToString();


            //Detalle
            numFil = numFil + 2;
            //mWSheet1.Cell("A" + numFil).Value = "Datos";
            mWSheet5.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet5.Cell("B" + numFil).Value = "Detalle";
            mWSheet5.Cell("E" + numFil).Style.Font.Bold = true;
            mWSheet5.Cell("E" + numFil).Value = "Cantidad Unit";
            mWSheet5.Cell("H" + numFil).Style.Font.Bold = true;
            mWSheet5.Cell("H" + numFil).Value = "Costo $";

            i = 0;
            vMtoAntesIV = 0;
            numFil = numFil + 1;

            while (i < tot_registros)
            {
                vProductoAct = GridDesgloseFactura.Rows[i].Cells[0].Text.ToString().Replace("&nbsp;", "");
                if (vProductoAct != "Alquiler de Equipos (ONT, Otros)")
                {
                    mWSheet5.Cell("B" + numFil).Style.Font.Bold = true;
                    mWSheet5.Cell("B" + numFil).Value = vProductoAct;
                    mWSheet5.Cell("E" + numFil).Style.NumberFormat.Format = "###,###";
                    mWSheet5.Cell("E" + numFil).Value = GridDesgloseFactura.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "");
                    mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
                    mWSheet5.Cell("H" + numFil).Value = Convert.ToDouble(GridDesgloseFactura.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "0"));
                    vMtoAntesIV = vMtoAntesIV + Convert.ToDouble(GridDesgloseFactura.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "0"));
                    vMtoAntesIV = Math.Round(vMtoAntesIV, 2);
                    numFil = numFil + 2;
                }
                else
                {
                    vMtoONT2 = Convert.ToDouble(GridDesgloseFactura.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "0"));
                    vMtoONT2 = Math.Round(vMtoONT2, 2);
                    vTotONT = Convert.ToInt32(GridDesgloseFactura.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "0"));
                    
                }

                i = i + 1;
            }
            mWSheet5.Cell("B" + numFil).Style.Font.Bold = true;
            mWSheet5.Cell("B" + numFil).Value = "Alquiler de Equipos (ONT, Otros)";
            mWSheet5.Cell("E" + numFil).Style.NumberFormat.Format = "###,###";
            mWSheet5.Cell("E" + numFil).Value = vTotONT;
            mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet5.Cell("H" + numFil).Value = vMtoONT2;

            numFil = numFil + 2;
            vMtoSubtotal = vMtoAntesIV + vMtoONT2;
            vMtoSubtotal = Math.Round(vMtoSubtotal, 2);
            mWSheet5.Cell("F" + numFil).Value = "Sub Total";
            mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet5.Cell("H" + numFil).Value = vMtoSubtotal;

            numFil = numFil + 1;
            vMtoIV = vMtoAntesIV * 0.13;
            vMtoIV = Math.Round(vMtoIV, 2);
            mWSheet5.Cell("F" + numFil).Value = "IV(13%)";
            mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet5.Cell("H" + numFil).Value = vMtoIV;

            numFil = numFil + 1;
            vMtoSubtotal = vMtoSubtotal + vMtoIV;
            vMtoSubtotal = Math.Round(vMtoSubtotal, 2);
            mWSheet5.Cell("F" + numFil).Value = "SubTotal";
            mWSheet5.Cell("H" + numFil).Style.Font.Bold = true;
            mWSheet5.Cell("H" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet5.Cell("H" + numFil).Value = vMtoSubtotal;

            numFil = numFil + 1;
            mWSheet5.Cell("F" + numFil).Value = "Multas";
            mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet5.Cell("H" + numFil).Value = 0;

            numFil = numFil + 2;
            vMtoTot = vMtoSubtotal;
            vMtoTot = Math.Round(vMtoTot, 2);
            mWSheet5.Cell("F" + numFil).Value = "Total";
            mWSheet5.Cell("H" + numFil).Style.Font.Bold = true;
            mWSheet5.Cell("H" + numFil).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            mWSheet5.Cell("H" + numFil).Style.NumberFormat.Format = "$###,###.00";
            mWSheet5.Cell("H" + numFil).Value = vMtoSubtotal;


            mWSheet5.Range("B1:H4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet5.Range("B5:H11").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet5.Range("B12:H20").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            mWSheet5.Range("B21:H27").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            mWSheet5.Columns().AdjustToContents();

            #endregion factura1

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=FacturacionICE" + facturar.CicloFactura + ".xlsx");

            try
                {
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream, false);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

            }catch (Exception e)
            {
                throw e;
            }
           
        }

        protected void btnServicios_Click(object sender, EventArgs e)
        {
            string url = "frm_facturaSRV_rpt.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=650,height=400,left=100,top=100,resizable=yes,scrollbars=yes');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), s, true);
        }

        protected void btdDetalleSrv_Click(object sender, EventArgs e)
        {
            factura_a_xls();
        }
    }
}