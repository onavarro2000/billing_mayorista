using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace billing_mayorista.Clases
{
    public class Feriado
    {

        #region atributos_feriado

        private int _anio;
        private DateTime _fechaFeriado;
        private string _dscFeriado;

        #endregion atributos_feriado

        #region variables_privadas
        private DataSet _tabla;
        private string _mensaje;

        #endregion variables

        #region comandos_Oracle
        OracleConnection _connectionOra = new OracleConnection(StringConeccion.string_cnxOraJASEC); //new OracleConnection(Conectar.string_conexionOra);
        OracleCommand _command;
        OracleDataAdapter _adapOra;
        OracleTransaction _transaccionOra;
        OracleDataReader _dt;
        #endregion comandos_Oracle

        #region encapsular_atributos
        public int Anio
        {
            get
            {
                return _anio;
            }

            set
            {
                _anio = value;
            }
        }

        public DateTime FechaFeriado
        {
            get
            {
                return _fechaFeriado;
            }

            set
            {
                _fechaFeriado = value;
            }
        }

        public string DscFeriado
        {
            get
            {
                return _dscFeriado;
            }

            set
            {
                _dscFeriado = value;
            }
        }

        public DataSet Tabla
        {
            get { return _tabla; }
            set { _tabla = value; }
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





        #endregion encapsular_atributos

        #region metodos_publicos

        public Feriado()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            Anio = 0;
            FechaFeriado = DateTime.MinValue; //Se inicializa el datetime con un valor minimo.
            DscFeriado = null;
            Tabla = null;
        }

        public void getFeriadosXAnio()
        {

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand();

                _command = _connectionOra.CreateCommand();

                _command.CommandText = "FERIADO_PKG.GET_FERIADOS_X_ANIO";
                _command.CommandType = CommandType.StoredProcedure;


                OracleParameter _panio = new OracleParameter();
                _panio.ParameterName = "panio";
                _panio.OracleDbType = OracleDbType.Int16;
                _panio.Direction = ParameterDirection.Input;
                _panio.Value = this._anio;
                _command.Parameters.Add(_panio);


                OracleParameter _pferiado_cursor = new OracleParameter("pferiado_cursor", OracleDbType.RefCursor);
                _pferiado_cursor.Direction = ParameterDirection.Output;
                _command.Parameters.Add(_pferiado_cursor);


                _adapOra = new OracleDataAdapter(_command);

                this._tabla = new DataSet();
                _adapOra.Fill(this._tabla);


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

        public void addFeriado()
        {

            string v_Consulta = "INSERT INTO fact_feriado (dia, dsc, anio) " +
                                      "VALUES (:dia, :dsc, :anio) ";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _command.Parameters.Add(":dia", OracleDbType.Date);
                _command.Parameters[":dia"].Value = _fechaFeriado;

                _command.Parameters.Add(":dsc", OracleDbType.Varchar2);
                _command.Parameters[":dsc"].Value = _dscFeriado;

                _command.Parameters.Add(":anio", OracleDbType.Int32);
                _command.Parameters[":anio"].Value = _anio;

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

        public void delFeriado()
        {

            string v_Consulta = "DELETE FROM fact_feriado " +
                                 "WHERE TRUNC(dia) = :dia ";

            try
            {

                _connectionOra.Open();

                _command = new OracleCommand(v_Consulta, _connectionOra);

                _command.Parameters.Add(":dia", OracleDbType.Date);
                _command.Parameters[":dia"].Value = _fechaFeriado;

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

        #endregion metodos_publicos
    }
}