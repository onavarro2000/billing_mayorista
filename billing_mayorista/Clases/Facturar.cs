using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace billing_mayorista.Clases
{
    public class Facturar
    {


        #region Variables Privadas
        private string _nomOperador;
        private Int32 _mesFactura;
        private Int32 _anioFactura;
        private string _monedaFactura;
        private string _cicloFactura;
        private string _mensaje;
        private string _servicioFactura;
        private string _tipoConsulta;
        private DataTable _tabla;

        #endregion Variables Privadas

        #region comandos_Oracle
        OracleConnection _connectionOra = new OracleConnection(StringConeccion.string_cnxOraJASEC); //new OracleConnection(Conectar.string_conexionOra);
        OracleCommand _command;
        OracleDataAdapter _adapOra;
        OracleTransaction _transaccionOra;
        OracleDataReader _reader;
        #endregion comandos_Oracle

        #region Encapsulacion

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

        public int MesFactura
        {
            get
            {
                return _mesFactura;
            }

            set
            {
                _mesFactura = value;
            }
        }

        public int AnioFactura
        {
            get
            {
                return _anioFactura;
            }

            set
            {
                _anioFactura = value;
            }
        }

        public string MonedaFactura
        {
            get
            {
                return _monedaFactura;
            }

            set
            {
                _monedaFactura = value;
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

        public string CicloFactura
        {
            get
            {
                return _cicloFactura;
            }

            set
            {
                _cicloFactura = value;
            }
        }

        public string ServicioFactura
        {
            get
            {
                return _servicioFactura;
            }

            set
            {
                _servicioFactura = value;
            }
        }

        public string TipoConsulta
        {
            get
            {
                return _tipoConsulta;
            }

            set
            {
                _tipoConsulta = value;
            }
        }

        public DataTable Tabla
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
        #endregion Encapsulacion

        #region Procedimientos_publicos



        //Limpia las tablas involucradas en prefacturacion.
        #region limipar_tablas

        public void limpiar_tablas()
        {
            string vResultado = null;

            try
            {
                //Abre conexion a Oracle
                _connectionOra.Open();

                //Se crea una nueva instancia.
                //comandOra = new OracleCommand();
                _command = _connectionOra.CreateCommand();


                //Le indico el nombre del procedimiento/fucion y/o paquete de base de datos.
                //_command.CommandText = "facturacion_mayorista_v2_pkg.limpiar_tablas_fnc";
                _command.CommandText = "facturacion_mayorista_v4_pkg.limpiar_tablas_fnc";

                //Se indica que es un procedimiento/fucion y/o paquete de base de datos.
                _command.CommandType = CommandType.StoredProcedure;

                _command.BindByName = true;
                _command.Parameters.Add("Return_Value", OracleDbType.Int16, ParameterDirection.ReturnValue);

                //Paso de parametros.
                _command.Parameters.Add("pmsg", OracleDbType.Varchar2, ParameterDirection.Output);
                _command.Parameters["pmsg"].Size = 900;

                //Ejecuta el procedimiento
                _command.ExecuteNonQuery();

                vResultado = Convert.ToString(_command.Parameters["return_value"].Value);

                //Se verifica el resultado
                if (vResultado == "0") //0 no dio error, de lo contrario da error.
                {
                    _mensaje = "Limpieza de tablas finalizó con éxito.";
                }
                else
                {
                    _mensaje = Convert.ToString(_command.Parameters["pmsg"].Value);
                }

            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                //connection.Close();
                _connectionOra.Close();
            }
        }

        #endregion limipar_tablas


        //Factura el servicio FTTH-DATA
        #region factura_FTTH_DATA

        public void facturar_FTTH_DATA()
        {
            string vResultado = null;

            try
            {
                //Abre conexion a Oracle
                _connectionOra.Open();

                //Se crea una nueva instancia.
                //comandOra = new OracleCommand();
                _command = _connectionOra.CreateCommand();


                //Le indico el nombre del procedimiento/fucion y/o paquete de base de datos.
                //_command.CommandText = "facturacion_mayorista_v2_pkg.main_facturar_fnc";
                _command.CommandText = "facturacion_mayorista_v4_pkg.main_facturar_fnc";

                //Se indica que es un procedimiento/fucion y/o paquete de base de datos.
                _command.CommandType = CommandType.StoredProcedure;

                _command.BindByName = true;
                _command.Parameters.Add("Return_Value", OracleDbType.Int16, ParameterDirection.ReturnValue);

                //Paso de parametros.
                _command.Parameters.Add("pOperador", OracleDbType.Varchar2, 100, _nomOperador, ParameterDirection.Input);
                _command.Parameters.Add("pMesFact", OracleDbType.Int16, _mesFactura, ParameterDirection.Input);
                _command.Parameters.Add("paniofact", OracleDbType.Int16, _anioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pproducto", OracleDbType.Varchar2, 15, _servicioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmoneda", OracleDbType.Varchar2, 3, _monedaFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmensaje", OracleDbType.Varchar2, ParameterDirection.Output);
                _command.Parameters["pmensaje"].Size = 900;

                //Ejecuta el procedimiento
                _command.ExecuteNonQuery();

                vResultado = Convert.ToString(_command.Parameters["return_value"].Value);

                //Se verifica el resultado
                if (vResultado == "0") //0 no dio error, de lo contrario da error.
                {
                    _mensaje = "Facturación FTTH-DATA finalizó con éxito.";
                }
                else
                {
                    _mensaje = Convert.ToString(_command.Parameters["pmensaje"].Value);
                }

            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                //connection.Close();
                _connectionOra.Close();
            }
        }

        #endregion factura_FTTH_DATA


        //Factura el servicio FTTH-VoIP
        #region factura_FTTH_VoIP

        public void facturar_FTTH_VoIP()
        {

            string vResultado = null;

            try
            {
                //Abre conexion a Oracle
                _connectionOra.Open();

                //Se crea una nueva instancia.
                //comandOra = new OracleCommand();
                _command = _connectionOra.CreateCommand();

                //Le indico el nombre del procedimiento/fucion y/o paquete de base de datos.
                //_command.CommandText = "facturacion_mayorista_v2_pkg.main_facturar_fnc";
                _command.CommandText = "facturacion_mayorista_v4_pkg.main_facturar_fnc";

                //Se indica que es un procedimiento/fucion y/o paquete de base de datos.
                _command.CommandType = CommandType.StoredProcedure;

                _command.BindByName = true;
                _command.Parameters.Add("Return_Value", OracleDbType.Int16, ParameterDirection.ReturnValue);

                //Paso de parametros.
                _command.Parameters.Add("pOperador", OracleDbType.Varchar2, 100, _nomOperador, ParameterDirection.Input);
                _command.Parameters.Add("pMesFact", OracleDbType.Int16, _mesFactura, ParameterDirection.Input);
                _command.Parameters.Add("paniofact", OracleDbType.Int16, _anioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pproducto", OracleDbType.Varchar2, 15, _servicioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmoneda", OracleDbType.Varchar2, 3, _monedaFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmensaje", OracleDbType.Varchar2, ParameterDirection.Output);
                _command.Parameters["pmensaje"].Size = 900;



                //Ejecuta el procedimiento
                _command.ExecuteNonQuery();

                vResultado = Convert.ToString(_command.Parameters["return_value"].Value);

                //Se verifica el resultado
                if (vResultado == "0") //0 no dio error, de lo contrario da error.
                {
                    _mensaje = "Facturación FTTH-VoIP finalizó con éxito.";
                }
                else
                {
                    _mensaje = Convert.ToString(_command.Parameters["pmensaje"].Value);
                }


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                //connection.Close();
                _connectionOra.Close();
            }
        }

        #endregion factura_FTTH_VoIP

        //Factura el servicio FTTH- IPTV
        #region factura_FTTH_IPTV

        public void facturar_FTTH_IPTV()
        {

            string vResultado = null;

            try
            {
                //Abre conexion a Oracle
                _connectionOra.Open();

                //Se crea una nueva instancia.
                //comandOra = new OracleCommand();
                _command = _connectionOra.CreateCommand();

                //Le indico el nombre del procedimiento/fucion y/o paquete de base de datos.
                //_command.CommandText = "facturacion_mayorista_v2_pkg.main_facturar_fnc";
                _command.CommandText = "facturacion_mayorista_v4_pkg.main_facturar_fnc";

                //Se indica que es un procedimiento/fucion y/o paquete de base de datos.
                _command.CommandType = CommandType.StoredProcedure;

                _command.BindByName = true;
                _command.Parameters.Add("Return_Value", OracleDbType.Int16, ParameterDirection.ReturnValue);

                //Paso de parametros.
                _command.Parameters.Add("pOperador", OracleDbType.Varchar2, 100, _nomOperador, ParameterDirection.Input);
                _command.Parameters.Add("pMesFact", OracleDbType.Int16, _mesFactura, ParameterDirection.Input);
                _command.Parameters.Add("paniofact", OracleDbType.Int16, _anioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pproducto", OracleDbType.Varchar2, 15, _servicioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmoneda", OracleDbType.Varchar2, 3, _monedaFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmensaje", OracleDbType.Varchar2, ParameterDirection.Output);
                _command.Parameters["pmensaje"].Size = 900;




                //Ejecuta el procedimiento
                _command.ExecuteNonQuery();

                vResultado = Convert.ToString(_command.Parameters["return_value"].Value);

                //Se verifica el resultado
                if (vResultado == "0") //0 no dio error, de lo contrario da error.
                {
                    _mensaje = "Facturación FTTH-IPTV finalizó con éxito.";
                }
                else
                {
                    _mensaje = Convert.ToString(_command.Parameters["pmensaje"].Value);
                }


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                //connection.Close();
                _connectionOra.Close();
            }
        }

        #endregion factura_FTTH_VoIP

        //Factura el servicio FTTH-RF
        #region factura_FTTH_RF
        public void facturar_FTTH_RF()
        {

            string vResultado = null;

            try
            {
                //Abre conexion a Oracle
                _connectionOra.Open();

                //Se crea una nueva instancia.
                //comandOra = new OracleCommand();
                _command = _connectionOra.CreateCommand();

                //Le indico el nombre del procedimiento/fucion y/o paquete de base de datos.
                //_command.CommandText = "facturacion_mayorista_v2_pkg.main_facturar_fnc";
                _command.CommandText = "facturacion_mayorista_v4_pkg.main_facturar_fnc";

                //Se indica que es un procedimiento/fucion y/o paquete de base de datos.
                _command.CommandType = CommandType.StoredProcedure;

                _command.BindByName = true;
                _command.Parameters.Add("Return_Value", OracleDbType.Int16, ParameterDirection.ReturnValue);

                //Paso de parametros.
                _command.Parameters.Add("pOperador", OracleDbType.Varchar2, 100, _nomOperador, ParameterDirection.Input);
                _command.Parameters.Add("pMesFact", OracleDbType.Int16, _mesFactura, ParameterDirection.Input);
                _command.Parameters.Add("paniofact", OracleDbType.Int16, _anioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pproducto", OracleDbType.Varchar2, 15, _servicioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmoneda", OracleDbType.Varchar2, 3, _monedaFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmensaje", OracleDbType.Varchar2, ParameterDirection.Output);
                _command.Parameters["pmensaje"].Size = 900;




                //Ejecuta el procedimiento
                _command.ExecuteNonQuery();

                vResultado = Convert.ToString(_command.Parameters["return_value"].Value);

                //Se verifica el resultado
                if (vResultado == "0") //0 no dio error, de lo contrario da error.
                {
                    _mensaje = "Facturación FTTH-RF finalizó con éxito.";
                }
                else
                {
                    _mensaje = Convert.ToString(_command.Parameters["pmensaje"].Value);
                }


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                //connection.Close();
                _connectionOra.Close();
            }
        }

        #endregion factura_FTTH_RF

        //Factura el alquiler de ONT

        #region factura_FTTH_ONT
        public void factura_ONT()
        {

            string vResultado = null;

            try
            {
                //Abre conexion a Oracle
                _connectionOra.Open();

                //Se crea una nueva instancia.
                //comandOra = new OracleCommand();
                _command = _connectionOra.CreateCommand();

                //Le indico el nombre del procedimiento/fucion y/o paquete de base de datos.
                //_command.CommandText = "facturacion_mayorista_v2_pkg.calc_ont_costo_fnc";
                _command.CommandText = "facturacion_mayorista_v4_pkg.calc_ont_costo_fnc";

                //Se indica que es un procedimiento/fucion y/o paquete de base de datos.
                _command.CommandType = CommandType.StoredProcedure;

                _command.BindByName = true;
                _command.Parameters.Add("Return_Value", OracleDbType.Int16, ParameterDirection.ReturnValue);

                //Paso de parametros.
                _command.Parameters.Add("pOperador", OracleDbType.Varchar2, 100, _nomOperador, ParameterDirection.Input);
                _command.Parameters.Add("pCicloFact", OracleDbType.Varchar2, 100, _cicloFactura, ParameterDirection.Input);
                _command.Parameters.Add("pMesFact", OracleDbType.Int16, _mesFactura, ParameterDirection.Input);
                _command.Parameters.Add("paniofact", OracleDbType.Int16, _anioFactura, ParameterDirection.Input);
                _command.Parameters.Add("pmoneda", OracleDbType.Varchar2, 3, _monedaFactura, ParameterDirection.Input);
                _command.Parameters.Add("pMsj", OracleDbType.Varchar2, ParameterDirection.Output);
                _command.Parameters["pMsj"].Size = 900;




                //Ejecuta el procedimiento
                _command.ExecuteNonQuery();

                vResultado = Convert.ToString(_command.Parameters["return_value"].Value);

                //Se verifica el resultado
                if (vResultado == "0") //0 no dio error, de lo contrario da error.
                {
                    _mensaje = "Facturación ONT finalizó con éxito.";
                }
                else
                {
                    _mensaje = Convert.ToString(_command.Parameters["pMsj"].Value);
                }


            }
            catch (Exception lu)
            {
                throw lu;
            }
            finally
            {
                //connection.Close();
                _connectionOra.Close();
            }
        }

        #endregion factura_FTTH_ONT


        public void CargarFacturacion()
        {
            string v_Consulta = null;
            if (_tipoConsulta == "desglose_x_cliente")
            {
                v_Consulta = "select acceso, contacto, medidor, tipo_producto, TRUNC(fecha_instalacion) fecha_instalacion, " +
                                    "TRUNC(fecha_inicio_fact) fecha_inicio_fact, TRUNC(fecha_desconexcion) fecha_desconexcion, cantidad, " +
                                    "monto_antes_iv, monto_iv, monto_total  " +
                               "from FACT_DETALLE_FACT t " +
                              "where operador =  '" + _nomOperador + "' " +
                                "AND t.ciclo_facturacion = '" + _cicloFactura + "' " +
                              "order by medidor,   orden_visual, acceso, FECHA_CREACION_DTE"; // acceso"; //fecha_instalacion ";

            }
            else if (_tipoConsulta == "factura_detallada")
            {
                v_Consulta = "select acceso, contacto, medidor, tipo_producto, TRUNC(fecha_instalacion) fecha_instalacion, " +
                                    "TRUNC(fecha_inicio_fact) fecha_inicio_fact, TRUNC(fecha_desconexcion) fecha_desconexcion, cantidad, " +
                                    "monto_antes_iv, monto_iv, monto_total " +
                               "from FACT_DETALLE_FACT t " +
                              "where operador = '" + _nomOperador + "' " +
                                "AND t.ciclo_facturacion = '" + _cicloFactura + "' " +
                                "and tipo_producto NOT IN ('ONT') " +
                            "order by  tipo_producto, cantidad, medidor ";

            }
            else if (_tipoConsulta == "desglose_factura")
            {
                v_Consulta = "select decode(t.TIPO_PRODUCTO, 'FTTH-DATA', 'DATA', " +
                                           "'FTTH-IPTV', 'IPTV', " +
                                           "'FTTH-VoIP', 'VoIP') servicio, " +
                                    "t.CANTIDAD, count(*) total_srv,sum(t.MONTO_ANTES_IV) monto_antes_iv " +
                               "from APEX_JASEC.FACT_DETALLE_FACT t " +
                               "where TIPO_PRODUCTO <> 'ONT' " +
                              "group by t.TIPO_PRODUCTO, t.CANTIDAD " +
                              "UNION " +
                              "select x.TIPO_PRODUCTO servicio, " +
                                     "x.cantidad, " +
                                     "count(*) total_srv, " +
                                     "sum(x.MONTO_ANTES_IV) monto_antes_iv " +
                                "from APEX_JASEC.FACT_DETALLE_FACT x " +
                               "where TIPO_PRODUCTO = 'ONT' AND x.MONTO_ANTES_IV > 0 " +
                               "group by x.TIPO_PRODUCTO, x.CANTIDAD " +
                              "order by 1, 2";
            }
            else if (_tipoConsulta == "factura1")
            {
                v_Consulta = "select decode(t.TIPO_PRODUCTO, 'FTTH-DATA', 'DATA', " +
                                                            "'FTTH-IPTV', 'IPTV', " +
                                                            "'FTTH-VoIP', 'VoIP') servicio, 0 cantidad, " +
                                     "count(*) total_srv,sum(t.MONTO_ANTES_IV) monto_antes_iv " +
                               "from APEX_JASEC.FACT_DETALLE_FACT t " +
                              "where TIPO_PRODUCTO <> 'ONT' " +
                              "group by t.TIPO_PRODUCTO " +
                             "UNION " +
                              "select decode (x.TIPO_PRODUCTO, 'ONT', 'Alquiler de Equipos (ONT, Otros)') servicio, " +
                                     "0 cantidad, " +
                                     "count(*) total_srv, " +
                                     "sum(x.MONTO_ANTES_IV) monto_antes_iv " +
                              "from APEX_JASEC.FACT_DETALLE_FACT x " +
                              "where TIPO_PRODUCTO = 'ONT' AND x.MONTO_ANTES_IV > 0 " +
                              "group by x.TIPO_PRODUCTO " +
                             "order by 1,2";
            }

            try
            {





                _command = new OracleCommand(v_Consulta, _connectionOra);

                _connectionOra.Open();



                //  OracleDataAdapter _adapterObj = new OracleDataAdapter(command);
                _reader = _command.ExecuteReader();


                this._tabla = new DataTable();
                this._tabla.Load(_reader);





                //return ds;

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


        #endregion Procedimientos_publicos
    }
}