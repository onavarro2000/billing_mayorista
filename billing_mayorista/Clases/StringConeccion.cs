using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace billing_mayorista.Clases
{
    public class StringConeccion
    {
        //Conexion a Oracle
        public static string string_conexionOra = "Data Source=(DESCRIPTION =" +
                                                  "(ADDRESS = (PROTOCOL = TCP)(HOST =  10.5.7.5)(PORT = 1521))" +
                                                  "(CONNECT_DATA =" +
                                                  "(SERVER = DEDICATED)" +
                                                  "(SERVICE_NAME = bssdb)));" +
                                                  "User Id= system;Password=oracle;";
        //Conexion a oracle jasec
        public static string string_cnxOraJASEC = "Data Source=(DESCRIPTION =" +
                                              "(ADDRESS = (PROTOCOL = TCP)(HOST =  10.1.4.35)(PORT = 1521))" +
                                              "(CONNECT_DATA =" +
                                              "(SERVER = DEDICATED)" +
                                              "(SERVICE_NAME = jasec10g)));" +
                                              "User Id= apex_jasec;Password=osvnavna;";



        //Conexion a MySQL
        public static string string_conexion = "Database=oss;Data Source=srv-webservice;User Id=facturacion;Password=Facturar2016";

    }
}