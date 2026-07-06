using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Punto
{  
   
    internal class Conexion
    {
        private readonly string cadena =
        "Server=localhost;" +
        "Database=puntodb;" +
        "User ID=root;" +
        "Password=;" +
        "Port=3306;";

        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}