using MySql.Data.MySqlClient;
using ReservationSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.Storage
{
    public class FullInfoStorageService : IFullInfoStorageServices
    {
        MySqlConnection _context;

        public FullInfoStorageService(MySqlConnection context)
        {
            _context = context;
        }

        public int Count()
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;

            cmd.CommandText = " CALL get_all_reservations_count(); ";
            var reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                 count = int.Parse(reader["counter"].ToString());
            }
            reader.Close();
            _context.Close();
            return count;
        }
        public int CountByUser(Guid userId)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;

            cmd.CommandText = "SELECT COUNT(r.`Id`)AS counter FROM  `reservations` r WHERE r.`Id`=?id; ";
            cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = userId;
            var reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = int.Parse(reader["counter"].ToString());
            }
            reader.Close();
            _context.Close();
            return count;
        }
        

        public List<FullInfoDTO> GetFullInfo(int currentPage = 0, int recordsPerPage = 0, OrderByCriteria orderByCriteria = OrderByCriteria.None)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand(); 
            cmd.Connection = _context;

            cmd.CommandText = "CALL get_all_reservations(?currentPage,?totalRecords,?orderCriteria) ";
            cmd.Parameters.Add("?currentPage", MySqlDbType.Int32).Value = (recordsPerPage*(currentPage-1));
            cmd.Parameters.Add("?totalRecords", MySqlDbType.Int32).Value = recordsPerPage;
            cmd.Parameters.Add("?orderCriteria", MySqlDbType.Int32).Value = (int)orderByCriteria;
            var reader = cmd.ExecuteReader();
            List<FullInfoDTO> users = new List<FullInfoDTO>();
            while (reader.Read())
            {
                users.Add(new FullInfoDTO() {
                    BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                    Id = Guid.Parse(reader["Id"].ToString()),
                    ContactName = reader["ContactName"].ToString(),
                    IsFavorite = bool.Parse(reader["IsFavorite"].ToString()),
                    Ranking = int.Parse(reader["Ranking"].ToString()),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Details = reader["Details"].ToString(),
                    ContactType = new UserType()
                    {
                        Description = reader["Description"].ToString(),
                        Id = int.Parse(reader["user_typeId"].ToString())
                    },
                    UserId = Guid.Parse(reader["UserId"].ToString())

                }); 
            }
            reader.Close();
            _context.Close();
            return users;
        }

        public FullInfoDTO GetFullInfoById(string id)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;

            cmd.CommandText = "CALL get_reservation_by_id(?id) ";
            cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            var reader = cmd.ExecuteReader();
            FullInfoDTO users = new FullInfoDTO();
            while (reader.Read())
            {
                users = new FullInfoDTO()
                {
                    BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                    Id = Guid.Parse(reader["Id"].ToString()),
                    ContactName = reader["ContactName"].ToString(),
                    IsFavorite = bool.Parse(reader["IsFavorite"].ToString()),
                    Ranking = int.Parse(reader["Ranking"].ToString()),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Details = reader["Details"].ToString(),
                    ContactType = new UserType()
                    {
                        Description = reader["Description"].ToString(),
                        Id = int.Parse(reader["user_typeId"].ToString())
                    },
                    UserId =Guid.Parse(reader["UserId"].ToString())
                };
            }
            reader.Close();
            _context.Close();
            return users;
        }
        public List<FullInfoDTO> GetFullInfoByUser(string id,int currentPage = 1, int recordsPerPage = 0, OrderByCriteria orderByCriteria = OrderByCriteria.None)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;

            cmd.CommandText = "CALL get_reservation_by_user(?currentPage,?totalRecords,?orderCriteria,?id) ";
            cmd.Parameters.Add("?currentPage", MySqlDbType.Int32).Value = currentPage;
            cmd.Parameters.Add("?totalRecords", MySqlDbType.Int32).Value = recordsPerPage;
            cmd.Parameters.Add("?orderCriteria", MySqlDbType.Int32).Value = (int)orderByCriteria;
            cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            var reader = cmd.ExecuteReader();
            List<FullInfoDTO> reservations = new List<FullInfoDTO>();
            while (reader.Read())
            {
                reservations.Add(new FullInfoDTO()
                {
                    BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                    Id = Guid.Parse(reader["Id"].ToString()),
                    ContactName = reader["ContactName"].ToString(),
                    IsFavorite = bool.Parse(reader["IsFavorite"].ToString()),
                    Ranking = int.Parse(reader["Ranking"].ToString()),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Details = reader["Details"].ToString()              
                });
            }
            reader.Close();
            _context.Close();
            return reservations;
        }
    }
}
