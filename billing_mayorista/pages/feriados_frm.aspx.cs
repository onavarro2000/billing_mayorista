using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace billing_mayorista.pages
{
    public partial class feriados_frm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.Feriado feriado = new Clases.Feriado();
                feriado.Anio = Convert.ToInt32(cboFechas.SelectedValue.ToString());

                feriado.getFeriadosXAnio();

                this.gvDatos.DataSource = feriado.Tabla;
                this.gvDatos.DataBind();
            }
            catch (Exception lu)
            {
                RadAjaxManager2.Alert("Error: " + lu.Message);
            }
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Clases.Feriado feriado = new Clases.Feriado();

                //Asigna valores a variables de clase
                feriado.FechaFeriado = this.dtFecha.SelectedDate.Value;
                feriado.DscFeriado = this.txtDescripcion.Text;
                feriado.Anio = feriado.FechaFeriado.Date.Year; //Obtiene el año de la fecha del feriado.

                feriado.addFeriado();

                //Dispara la consulta para refrescar el grid.
                this.btnConsultar_Click(e, null);

                this.txtDescripcion.Text = "";

            }
            catch (Exception lu)
            {
                RadAjaxManager2.Alert("Registro NO insertado: " + lu.Message);
            }
        }

        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            string[] Value_Confirm = confirmValue.Split(',');

            //if (confirmValue == "Yes")
            if (Value_Confirm[Value_Confirm.Length - 1] == "Yes")

            {
                try
                {
                    Clases.Feriado feriado = new Clases.Feriado();

                    //Asigna valores a variables de clase
                    feriado.FechaFeriado = Convert.ToDateTime(gvDatos.Items[gvDatos.SelectedItems[0].ItemIndex]["Fecha"].Text);

                    feriado.delFeriado();

                    //Dispara la consulta para refrescar el grid.
                    this.btnConsultar_Click(e, null);

                    this.txtDescripcion.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Fecha Eliminada!')", true);

                }
                catch (Exception lu)
                {
                    RadAjaxManager2.Alert("Error: " + lu.Message);

                }
            }
        }

    }
}