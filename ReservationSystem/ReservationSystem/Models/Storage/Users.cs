using MySql.Data.MySqlClient;
using ReservationSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.Storage
{
    public class UserStorageService:IUserStorageServices
    {
        MySqlConnection _context;
        public UserStorageService(MySqlConnection context)
        {
            _context = context;
        }
        public long AddUser( User user, Reservation reservation)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            cmd.CommandText = "Call add_new_user(?Nombre, ?UserType,?PhoneNumber,?BirthDate,?Id,?Details,?ReservationId,?IsFavorite,?Ranking)";
            cmd.Parameters.Add("?Id", MySqlDbType.VarChar).Value = user.Id;
            cmd.Parameters.Add("?Nombre", MySqlDbType.VarChar).Value = user.ContactName;
            cmd.Parameters.Add("?PhoneNumber", MySqlDbType.VarChar).Value = user.PhoneNumber;
            cmd.Parameters.Add("?BirthDate", MySqlDbType.VarChar).Value = user.BirthDate.ToString();
            cmd.Parameters.Add("?UserType", MySqlDbType.VarChar).Value = user.ContactType.Id;
            cmd.Parameters.Add("?Details", MySqlDbType.VarChar).Value = reservation.Details;
            cmd.Parameters.Add("?ReservationId", MySqlDbType.VarChar).Value = reservation.Id;
            cmd.Parameters.Add("?IsFavorite", MySqlDbType.Int32).Value = reservation.IsFavorite;
            cmd.Parameters.Add("?Ranking", MySqlDbType.VarChar).Value = reservation.Ranking;
            cmd.ExecuteNonQuery();
            _context.Close();
            return cmd.LastInsertedId;
        }

        public List<User> GetSimpleUsers(string  username)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            cmd.CommandText = "Select *  FROM `users` WHERE `users`.`ContactName` = ?cad;  ";
            cmd.Parameters.Add("?cad", MySqlDbType.VarChar).Value =username;
            var reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User() { 
                    BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                    Id = Guid.Parse(reader["Id"].ToString()), 
                    ContactName = reader["ContactName"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString() });
            }
            reader.Close();
            _context.Close();
            return users;
        }

        public bool UserExist(string cad,bool exist)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            cmd.CommandText = "CALL user_exist(?cad); ";
            cmd.Parameters.Add("?cad", MySqlDbType.VarString).Value = cad;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var temp =  int.Parse(reader["user_exist"].ToString());
                if (temp == 1)
                    exist = true;
                else
                    exist = false;
            }
            reader.Close();
            _context.Close();
            return exist;
        }
    }

   
}
