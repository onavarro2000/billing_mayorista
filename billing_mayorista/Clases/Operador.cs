using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace billing_mayorista.Clases
{
    public class Operador
    {
        #region Variables Privadas
        private Int32 _codOperador;
        private string _dirOperador;
        private string _telOperador;
        private string _nomOperador;
        private string _moneda;
        private string _numMedidor;

        private DataSet _tabla;
        private DataTable _dataTable;
        private string _mensaje;

        private string _operaccion_dll;


        #endregion Variables Privadas

        #region comandos_Oracle
        OracleConnection _connectionOra = new OracleConnection(StringConeccion.string_cnxOraJASEC); //new OracleConnection(Conectar.string_conexionOra);
        OracleCommand _command;
        OracleDataAdapter _adapOra;
        OracleDataReader _reader;
        #endregion comandos_Oracle

        #region Encapsulacion
        public int CodOperador
        {
            get
            {
                return _codOperador;
            }

            set
            {
                _codOperador = value;
            }
        }

        public string DirOperador
        {
            get
            {
                return _dirOperador;
            }

            set
            {
                _dirOperador = value;
            }
        }

        public string TelOperador
        {
            get
            {
                return _telOperador;
            }

            set
            {
                _telOperador = value;
            }
        }

        public string NomOperador
        {
            get
            {
                return _nomOperador;
            }

            set
            {
                _nomOperador = value;
            }
        }

        public DataSet Tabla
        {
            get
            {
                return _tabla;
            }

            set
            {
                _tabla = value;
            }
        }

        public string Mensaje
        {
            get
            {
                return _mensaje;
            }

            set
            {
                _mensaje = value;
            }
        }

        public DataTable DataTable
        {
            get
            {
                return _dataTable;
            }

            set
            {
                _dataTable = value;
            }
        }

        public string Operaccion_dll
        {
            get
            {
                return _operaccion_dll;
            }

            set
            {
                _operaccion_dll = value;
            }
        }

        public string Moneda
        {
            get
            {
                return _moneda;
            }

            set
            {
                _moneda = value;
            }
        }

        public string NumMedidor
        {
            get
            {
                return _numMedidor;
            }

            set
            {
                _numMedidor = value;
            }
        }
        #endregion Encapsulacion

        #region metodos_publicos

        public DataSet getNombreAllOperador()
        {
            string v_Consulta = "SELECT nom_operador " +
                                  "FROM apex_jasec.fact_operadores " +
                                "ORDER BY nom_operador";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _adapOra = new OracleDataAdapter(_command);


                this._tabla = new DataSet();
                this._adapOra.Fill(_tabla);

                return _tabla;

            }

            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
            }
        }

        //Obtiene las diferentes monedas con las que se contrato el servicio.
        public DataSet getMonedas_x_Operador()
        {
            string v_Consulta = "SELECT  distinct moneda_contrato " +
                                "FROM fact_oper_srv_contratado " +
                                "WHERE operador = '" + _nomOperador + "' ";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _adapOra = new OracleDataAdapter(_command);


                this._tabla = new DataSet();
                this._adapOra.Fill(_tabla);

                return _tabla;

            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
            }
        }

        //Obtiene los servicios contratados por operador x moneda.
        public void get_srv_contratados_x_op()
        {
            string v_Consulta = "SELECT operador, servicio, moneda_contrato, simbolo_moneda " +
                                "FROM fact_oper_srv_contratado " +
                                "WHERE operador = '" + _nomOperador + "' " +
                                  "AND moneda_contrato = '" + _moneda + "'";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _reader = _command.ExecuteReader();


                this.DataTable = new DataTable();
                this.DataTable.Load(_reader);

            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
            }
        }

        public void getInfoAllOperador()
        {
            string v_Consulta = "SELECT nom_operador, cod_operador, telefono, direccion " +
                                  "FROM apex_jasec.fact_operadores " +
                                "ORDER BY nom_operador";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _reader = _command.ExecuteReader();


                this.DataTable = new DataTable();
                this.DataTable.Load(_reader);



            }

            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
            }
        }

        public void addOperador()
        {

            string v_Consulta = "INSERT INTO apex_jasec.fact_operadores " +
                                                 "(nom_operador, cod_operador, " +
                                                  "telefono, direccion) " +
                                          "VALUES (:pnom_operador, :pcod_operador, " +
                                                "  :ptelefono, :pdireccion)";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _command.Parameters.Add(":pnom_operador", OracleDbType.Varchar2);
                _command.Parameters[":pnom_operador"].Value = _nomOperador;

                _command.Parameters.Add(":pcod_operador", OracleDbType.Varchar2);
                _command.Parameters[":pcod_operador"].Value = _codOperador;

                _command.Parameters.Add(":ptelefono", OracleDbType.Varchar2);
                _command.Parameters[":ptelefono"].Value = _telOperador;

                _command.Parameters.Add(":pdireccion", OracleDbType.Varchar2);
                _command.Parameters[":pdireccion"].Value = _dirOperador;

                _command.ExecuteNonQuery();


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
                _connectionOra.Dispose();
                _connectionOra = null;
            }
        }

        public void updOperador()
        {

            string v_Consulta = "UPDATE apex_jasec.fact_operadores " +
                                   "SET nom_operador = :pnom_operador, " +
                                       "telefono =  :ptelefono, " +
                                       "direccion = :pdireccion " +
                                     "WHERE cod_operador = :pcod_operador";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _command.Parameters.Add(":pnom_operador", OracleDbType.Varchar2);
                _command.Parameters[":pnom_operador"].Value = _nomOperador;

                _command.Parameters.Add(":ptelefono", OracleDbType.Varchar2);
                _command.Parameters[":ptelefono"].Value = _telOperador;

                _command.Parameters.Add(":pdireccion", OracleDbType.Varchar2);
                _command.Parameters[":pdireccion"].Value = _dirOperador;

                _command.Parameters.Add(":pcod_operador", OracleDbType.Int64);
                _command.Parameters[":pcod_operador"].Value = _codOperador;





                _command.ExecuteNonQuery();


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
                _connectionOra.Dispose();
                _connectionOra = null;
            }
        }

        public void delOperador()
        {

            string v_Consulta = "DELETE FROM apex_jasec.fact_operadores " +
                                 "WHERE cod_operador = :pcod_operador ";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _command.Parameters.Add(":pcod_operador", OracleDbType.Int64);
                _command.Parameters[":pcod_operador"].Value = _codOperador;

                _command.ExecuteNonQuery();


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
                _connectionOra.Dispose();
                _connectionOra = null;
            }
        }


        //Obtiene los medidores facturados en un determinado mes
        public DataSet getMedidores_facturados()
        {
            string v_Consulta = "SELECT  DISTINCT medidor " +
                                "FROM fact_detalle_fact " +
                                "WHERE operador = '" + _nomOperador + "' " +
                                "ORDER BY medidor";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _adapOra = new OracleDataAdapter(_command);


                this._tabla = new DataSet();
                this._adapOra.Fill(_tabla);

                return _tabla;

            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
            }
        }

        //Obtiene los registro a facturar por medidor de un determinado operador
        public DataSet getInfo_facturacion()
        {
            string v_Consulta = "SELECT  id_registro, " +
                                " prod_inst_id, " +
                                " acceso, " +
                                " contacto, " +
                                " medidor, " +
                                " tipo_producto, " +
                                " ancho_banda, " +
                                " to_char(trunc(fecha_instalacion),'dd/mm/yyyy') fecha_instalacion, " +
                                " to_char(trunc(fecha_inicio_fact),'dd/mm/yyyy') fecha_inicio_fact, " +
                                " to_char(trunc(fecha_desconexcion),'dd/mm/yyyy') fecha_desconexcion, " +
                                " cantidad, " +
                                " tot_dias_facturar, " +
                                " trunc(monto_antes_iv,2) monto_antes_iv " +
                                "FROM fact_detalle_Fact " +
                                "WHERE operador = '" + _nomOperador + "' " +
                                " AND medidor = '" + NumMedidor + "' " +
                                "order by medidor, orden_visual, acceso, FECHA_CREACION_DTE";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _adapOra = new OracleDataAdapter(_command);


                this._tabla = new DataSet();
                this._adapOra.Fill(_tabla);

                return _tabla;

            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                _connectionOra.Close();
            }
        }





        #endregion metodos_publicos
    }
}