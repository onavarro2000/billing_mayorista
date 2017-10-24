using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace billing_mayorista.pages
{
    public partial class generar_factura_frm : System.Web.UI.Page
    {

        string vSrvGrl = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //Do not display SelectedFilesCount progress indicator.
                //RadProgressArea1.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;

                carga_operador();

                carga_combo_monedas();
                show_servicios_x_op_moneda();

                //RadProgressArea1.Localization.Uploaded = "Progreso Total";
                //RadProgressArea1.Localization.UploadedFiles = "Progreso";
                //RadProgressArea1.Localization.CurrentFileName = "Proceso en acción: ";
            }

        }

        private void carga_combo_monedas()
        {
            DataSet _dtOperador = new DataSet();
            try
            {
                this.lstMoneda.Text = null;

                Clases.Operador operador = new Clases.Operador();


                operador.NomOperador = this.lstOperador.Text;
                _dtOperador = operador.getMonedas_x_Operador();

                this.lstMoneda.DataSource = _dtOperador;
                this.lstMoneda.DataTextField = "moneda_contrato";
                this.lstMoneda.DataValueField = "moneda_contrato";
                this.lstMoneda.DataBind();
            }
            catch (Exception ex)
            {
                this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                this.lblMensajeSistema.Text = ex.Message.ToString();

            }
        }

        private void carga_operador()
        {
            DataSet _dtOperador = new DataSet();
            try
            {
                Clases.Operador operador = new Clases.Operador();

                _dtOperador = operador.getNombreAllOperador();

                this.lstOperador.DataSource = _dtOperador;
                this.lstOperador.DataTextField = "nom_operador";
                this.lstOperador.DataValueField = "nom_operador";
                this.lstOperador.DataBind();
            }
            catch (Exception ex)
            {
                this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                this.lblMensajeSistema.Text = ex.Message.ToString();

            }
        }

        protected void lstOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkData.Visible = false;
            this.chkIPTV.Visible = false;
            this.chkVoIP.Visible = false;
            this.chkRF.Visible = false;
            vSrvGrl = null;

            carga_combo_monedas();
            show_servicios_x_op_moneda();

        }

        private void show_servicios_x_op_moneda()
        {
            vSrvGrl = null;
            string vMoneda = null;
            string vSimbolo = null;

            try
            {

                Clases.Operador operador = new Clases.Operador();

                operador.NomOperador = this.lstOperador.Text;
                operador.Moneda = this.lstMoneda.Text;

                operador.get_srv_contratados_x_op();


                foreach (DataRow row in operador.DataTable.Rows)
                {

                    vSrvGrl = row["servicio"].ToString().Trim();
                    vMoneda = row["moneda_contrato"].ToString().Trim();
                    vSimbolo = " (" + row["simbolo_moneda"].ToString().Trim() + ")";

                    if (vSrvGrl == "FTTH-DATA")
                    {
                        this.chkData.Visible = true;
                        this.chkData.Checked = true;
                        this.chkData.Enabled = false;
                    }

                    if (vSrvGrl == "FTTH-IPTV")
                    {
                        this.chkIPTV.Visible = true;
                        this.chkIPTV.Checked = true;
                        this.chkIPTV.Enabled = false;
                    }

                    if (vSrvGrl == "FTTH-VoIP")
                    {
                        this.chkVoIP.Visible = true;
                        this.chkVoIP.Checked = true;
                        this.chkVoIP.Enabled = false;
                    }

                    if (vSrvGrl == "FTTH-RF")
                    {
                        this.chkRF.Visible = true;
                        this.chkRF.Checked = true;
                        this.chkRF.Enabled = false;
                    }
                }


            }
            catch (Exception ex)
            {
                this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                this.lblMensajeSistema.Text = ex.Message.ToString();

            }
        }

        protected void lstMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkData.Visible = false;
            this.chkIPTV.Visible = false;
            this.chkRF.Visible = false;
            this.chkVoIP.Visible = false;

            show_servicios_x_op_moneda();
        }

        protected void btnFacturar_Click(object sender, ImageClickEventArgs e)
        {
            const int Total = 100;
            string vContinuar = "SI";

            this.lblMsjData.Text = null;
            this.lblMsjIPTV.Text = null;
            this.lblMsjVoIP.Text = null;
            this.lblMsjRF.Text = null;
            this.lblMsjONT.Text = null;
            this.lblMensajeSistema.Text = null;
            string vResultado = "OK";

            RadProgressContext progress = RadProgressContext.Current;
            progress.Speed = "N/A";

            progress.PrimaryTotal = 1;
            progress.PrimaryValue = 1;
            progress.PrimaryPercent = 100;

            progress.SecondaryTotal = Total;
            progress.SecondaryValue = 2;
            progress.SecondaryPercent = 2;
            progress.CurrentOperationText = "Iniciando proceso de Facturación.";



            progress.TimeEstimated = (Total - 2) * 100;
            //Stall the current thread for 0.1 seconds
            System.Threading.Thread.Sleep(2000);

            Clases.Facturar facturar = new Clases.Facturar();  //Instancio la clase Facturar

            //Asigno valores a facturar.
            facturar.NomOperador = this.lstOperador.Text;
            facturar.MonedaFactura = this.lstMoneda.Text;
            facturar.MesFactura = Convert.ToInt32(this.lstMes.Text);
            facturar.AnioFactura = Convert.ToInt32(this.lstAnio.Text);
            //Armo el ciclo de facturacion  1  2016  01-2016
            facturar.CicloFactura = string.Concat(this.lstMes.Text.Trim().PadLeft(2, '0'), "-", this.lstAnio.Text.Trim());


            //--------------------------------------------------------------------
            //-----   Limpia las tablas involucradas en la prefacturacion     ----
            //--------------------------------------------------------------------
            #region limpia_tablas
            try
            {

                progress.SecondaryTotal = Total;
                progress.SecondaryValue = 1;

                progress.SecondaryPercent = 1;
                progress.CurrentOperationText = " Paso 0: Limpiando Tablas.";


                facturar.limpiar_tablas();

                progress.TimeEstimated = (Total - 1) * 100;
                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(1000);

                vResultado = facturar.Mensaje;

                if (vResultado.Trim() == "Limpieza de tablas finalizó con éxito.")
                {
                    vContinuar = "SI";
                    lblMsjData.Text = "\u221A" + " " + facturar.Mensaje;
                    lblMsjData.ForeColor = System.Drawing.Color.Green;

                }
                else
                {

                    lblMsjData.Text = "X" + " " + facturar.Mensaje;
                    lblMsjData.ForeColor = System.Drawing.Color.Red;
                    vContinuar = "NO";
                }


            }
            catch (Exception lu)
            {
                vContinuar = "NO";
                this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                this.lblMensajeSistema.Text = lu.Message;
            }
            #endregion limpia_tablas


            //-----------------------------------
            //-----   Facturar FTTH-DATA     ----
            //-----------------------------------
            #region factura_data
            if (vContinuar == "SI")
            {
                try
                {

                    facturar.ServicioFactura = "FTTH-DATA";

                    progress.SecondaryTotal = Total;
                    progress.SecondaryValue = 25;

                    progress.SecondaryPercent = 25;
                    progress.CurrentOperationText = " Paso 1: Facturando FTTH-DATA.";


                    facturar.facturar_FTTH_DATA();

                    progress.TimeEstimated = (Total - 25) * 100;
                    //Stall the current thread for 0.1 seconds
                    System.Threading.Thread.Sleep(2000);

                    vResultado = facturar.Mensaje;

                    if (vResultado.Trim() == "Facturación FTTH-DATA finalizó con éxito.")
                    {
                        vContinuar = "SI";
                        lblMsjData.Text = "\u221A" + " " + facturar.Mensaje;
                        lblMsjData.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {

                        lblMsjData.Text = "X" + " " + facturar.Mensaje;
                        lblMsjData.ForeColor = System.Drawing.Color.Red;
                        vContinuar = "NO";
                    }


                }
                catch (Exception lu)
                {
                    vContinuar = "NO";
                    this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                    this.lblMensajeSistema.Text = lu.Message;
                }
            }

            #endregion factura_data

            //-----------------------------------
            //-----   Facturar FTTH-VoIP     ----
            //-----------------------------------
            #region Factura_VoIP

            if (vContinuar == "SI")
            {
                facturar.ServicioFactura = "FTTH-VoIP";

                try
                {
                    progress.SecondaryTotal = Total;
                    progress.SecondaryValue = 45;
                    progress.SecondaryPercent = 45;
                    progress.CurrentOperationText = " Paso 2: Facturando FTTH-VoIP.";
                    facturar.facturar_FTTH_VoIP();
                    progress.TimeEstimated = (Total - 45) * 100;
                    //Stall the current thread for 0.1 seconds
                    System.Threading.Thread.Sleep(2000);

                    vResultado = facturar.Mensaje;

                    if (vResultado.Trim() == "Facturación FTTH-VoIP finalizó con éxito.")
                    {
                        vContinuar = "SI";
                        lblMsjVoIP.Text = "\u221A" + " " + facturar.Mensaje;
                        lblMsjVoIP.ForeColor = System.Drawing.Color.Green;

                        //lblEstado.Text = "<br/>" + lblEstado.Text + Environment.NewLine + "\u221A" + " " + facturar.Mensaje;
                    }
                    else
                    {
                        vContinuar = "NO";
                        // lblEstado.Text = "<br/>" + lblEstado.Text + "X" + " " + facturar.Mensaje;
                        lblMsjVoIP.Text = "X" + " " + facturar.Mensaje;
                        lblMsjVoIP.ForeColor = System.Drawing.Color.Red;

                    }

                }
                catch (Exception lu)
                {
                    vContinuar = "NO";
                    this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                    this.lblMensajeSistema.Text = lu.Message;
                }

            }
            #endregion Factura_VoIP

            //-----------------------------------
            //-----   Facturar FTTH-IPTV     ----
            //-----------------------------------
            #region Factura_IPTV

            if (vContinuar == "SI")
            {
                facturar.ServicioFactura = "FTTH-IPTV";
                try
                {
                    //Facturando FTTH-IPTV.
                    progress.SecondaryTotal = Total;
                    progress.SecondaryValue = 65;
                    progress.SecondaryPercent = 65;
                    progress.CurrentOperationText = " Paso 3: Facturando FTTH-IPTV.";
                    facturar.facturar_FTTH_IPTV();
                    progress.TimeEstimated = (Total - 65) * 100;
                    //Stall the current thread for 0.1 seconds
                    System.Threading.Thread.Sleep(2000);

                    vResultado = facturar.Mensaje;

                    if (vResultado.Trim() == "Facturación FTTH-IPTV finalizó con éxito.")
                    {
                        vContinuar = "SI";
                        lblMsjIPTV.Text = "\u221A" + " " + facturar.Mensaje;
                        lblMsjIPTV.ForeColor = System.Drawing.Color.Green;

                        //lblEstado.Text = "<br/>" + lblEstado.Text + Environment.NewLine + "\u221A" + " " + facturar.Mensaje;
                    }
                    else
                    {
                        vContinuar = "NO";
                        // lblEstado.Text = "<br/>" + lblEstado.Text + "X" + " " + facturar.Mensaje;
                        lblMsjIPTV.Text = "X" + " " + facturar.Mensaje;
                        lblMsjIPTV.ForeColor = System.Drawing.Color.Red;

                    }

                }
                catch (Exception lu)
                {
                    vContinuar = "NO";
                    this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                    this.lblMensajeSistema.Text = lu.Message;
                }

            }
            #endregion Factura_IPTV

            //-----------------------------------
            //-----   Facturar FTTH-RF     ----
            //-----------------------------------
            #region Factura_RF

            if (vContinuar == "SI")
            {
                facturar.ServicioFactura = "FTTH-RF";
                try
                {
                    //Facturando FTTH-IPTV.
                    progress.SecondaryTotal = Total;
                    progress.SecondaryValue = 85;
                    progress.SecondaryPercent = 85;
                    progress.CurrentOperationText = " Paso 4: Facturando FTTH-RF.";
                    facturar.facturar_FTTH_RF();
                    progress.TimeEstimated = (Total - 85) * 100;
                    //Stall the current thread for 0.1 seconds
                    System.Threading.Thread.Sleep(2000);

                    vResultado = facturar.Mensaje;

                    if (vResultado.Trim() == "Facturación FTTH-RF finalizó con éxito.")
                    {
                        vContinuar = "SI";
                        lblMsjRF.Text = "\u221A" + " " + facturar.Mensaje;
                        lblMsjRF.ForeColor = System.Drawing.Color.Green;

                        //lblEstado.Text = "<br/>" + lblEstado.Text + Environment.NewLine + "\u221A" + " " + facturar.Mensaje;
                    }
                    else
                    {
                        vContinuar = "NO";
                        // lblEstado.Text = "<br/>" + lblEstado.Text + "X" + " " + facturar.Mensaje;
                        lblMsjRF.Text = "X" + " " + facturar.Mensaje;
                        lblMsjRF.ForeColor = System.Drawing.Color.Red;

                    }

                }
                catch (Exception lu)
                {
                    vContinuar = "NO";
                    this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                    this.lblMensajeSistema.Text = lu.Message;
                }

            }
            #endregion Factura_RF

            //-----------------------------------
            //-----   Facturar alquiler ONT  ----
            //-----------------------------------
            #region Factura_ONT

            if (vContinuar == "SI")
            {
                try
                {

                    //Facturando FTTH-IPTV.
                    progress.SecondaryTotal = Total;
                    progress.SecondaryValue = 96;
                    progress.SecondaryPercent = 96;
                    progress.CurrentOperationText = " Paso 5: Facturando ONTs.";

                    facturar.factura_ONT();

                    progress.TimeEstimated = (Total - 96) * 100;
                    //Stall the current thread for 0.1 seconds
                    System.Threading.Thread.Sleep(2000);

                    vResultado = facturar.Mensaje;

                    if (vResultado.Trim() == "Facturación ONT finalizó con éxito.")
                    {
                        vContinuar = "SI";
                        lblMsjONT.Text = "\u221A" + " " + facturar.Mensaje;
                        lblMsjONT.ForeColor = System.Drawing.Color.Green;
                        //lblEstado.Text = "<br/>" + lblEstado.Text + Environment.NewLine + "\u221A" + " " + facturar.Mensaje;
                    }
                    else
                    {
                        vContinuar = "NO";
                        lblMsjONT.Text = "X" + " " + facturar.Mensaje;
                        lblMsjONT.ForeColor = System.Drawing.Color.Red;
                        //lblEstado.Text = "<br/>" + lblEstado.Text + "X" + " " + facturar.Mensaje;

                    }

                }
                catch (Exception lu)
                {
                    vContinuar = "NO";
                    this.lblMensajeSistema.ForeColor = System.Drawing.Color.Red;
                    this.lblMensajeSistema.Text = lu.Message;
                }

            }

            #endregion Factura_ONT

            if (vContinuar == "SI")
            {
                progress.SecondaryTotal = Total;
                progress.SecondaryValue = 100;
                progress.SecondaryPercent = 100;
                progress.CurrentOperationText = " Paso 6: Proceso de facturación finalizado con EXITO.";
                progress.TimeEstimated = (Total - 100) * 100;

            }
            else
            {
                progress.SecondaryTotal = Total;
                progress.SecondaryValue = 100;
                progress.SecondaryPercent = 100;
                progress.CurrentOperationText = " Paso 6: Proceso de facturación finalizado con ERRORES.";
                progress.TimeEstimated = (Total - 100) * 100;

            }


            //Stall the current thread for 0.1 seconds
            System.Threading.Thread.Sleep(2000);

        }

        
    }
}