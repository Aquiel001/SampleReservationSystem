using MySql.Data.MySqlClient;
using ReservationSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.Storage
{
    public class UserTypesStorageService : IUserTypesStorageService
    {
        MySqlConnection _context;

        public UserTypesStorageService(MySqlConnection context)
        {
            _context = context;
        }

        
        public List<UserType> GetUserTypes()
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            cmd.CommandText = "SELECT   `Id`,  `Description` FROM  `usertypes`; ";
            var reader = cmd.ExecuteReader();
            List<UserType> types = new List<UserType>();
            while (reader.Read())
            {
                types.Add(new UserType() {Id=int.Parse(reader["id"].ToString()),Description=reader["Description"].ToString() });
            }
            reader.Close();
            _context.Close();
            return types;
        }
    }
}
