using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.Services
{
    public class MySqlConnections
    {
        IConfiguration _config;
        /// <summary>
        /// Prepare a MySqL conection
        /// </summary>
        /// <param name="config"></param>
        public MySqlConnections(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Configure and start a MYSQL connection
        /// </summary>
        /// <returns></returns>
        public MySqlConnection Start()
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");// "database = reservation_system; server = localhost; port = 3306; user id = root";
            MySqlConnection conexion = new MySqlConnection(connectionString);
            conexion.Dispose();
            return conexion;
        }
    }
}
