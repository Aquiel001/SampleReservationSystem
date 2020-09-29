using MySql.Data.MySqlClient;
using ReservationSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.Storage
{
    public class ReservationsStorageService:IReservationStorageService
    {
        MySqlConnection _context;
        public ReservationsStorageService(MySqlConnection context)
        {
            _context = context;
        }
        public void MarkReservationAsFav(bool isFav, Guid id, int currentPage, int recordsPerPage)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            //cmd.CommandText = "CALL mark_as_favorite(?id,?isFav)";
            //UPDATE   `reservations`  SET  `reservations`.`IsFavorite` = isFav WHERE  `reservations`.`Id` = reservationId;
            cmd.CommandText = "UPDATE   `reservations`  SET  `reservations`.`IsFavorite` = ?isFav WHERE  `reservations`.`Id` = ?id; COMMIT;";
            cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("?isFav", MySqlDbType.Int32).Value = isFav;
           var inte= cmd.ExecuteNonQuery();
            _context.Close();
            //cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = 4;
        }
      

        public void UpdatekReservationRate(int rate, Guid id, int currentPage = 1, int recordsPerPage = 10)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            //cmd.CommandText = "CALL mark_as_favorite(?id,?isFav)";
            //UPDATE   `reservations`  SET  `reservations`.`IsFavorite` = isFav WHERE  `reservations`.`Id` = reservationId;
            cmd.CommandText = "UPDATE   `reservations`  SET  `reservations`.`Ranking` = ?rate WHERE  `reservations`.`Id` = ?id; COMMIT;";
            cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("?rate", MySqlDbType.Int32).Value = rate;
            var index = cmd.ExecuteNonQuery();
            _context.Close();
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            //cmd.CommandText = "CALL mark_as_favorite(?id,?isFav)";
            //UPDATE   `reservations`  SET  `reservations`.`IsFavorite` = isFav WHERE  `reservations`.`Id` = reservationId;
            cmd.CommandText = "INSERT INTO   `reservations`(  `Id`,  `UserId`,  `Details`,  `Ranking`,  `IsFavorite`)VALUE(  ?Id,  ?UserId,  ?Details,  ?Ranking,  ?IsFavorite); ";
            cmd.Parameters.Add("?Id", MySqlDbType.VarChar).Value = reservation.Id;
            cmd.Parameters.Add("?Ranking", MySqlDbType.Int32).Value = reservation.Ranking;
            cmd.Parameters.Add("?UserId", MySqlDbType.VarChar).Value = reservation.UserId;
            cmd.Parameters.Add("?Details", MySqlDbType.VarChar).Value = reservation.Details;
            cmd.Parameters.Add("?IsFavorite", MySqlDbType.Binary).Value = reservation.IsFavorite;
            var inte = cmd.ExecuteNonQuery();
            _context.Close();

        }

        public void UpdatekReservation(Reservation reservation)
        {
            _context.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _context;
            //cmd.CommandText = "CALL mark_as_favorite(?id,?isFav)";
            //UPDATE   `reservations`  SET  `reservations`.`IsFavorite` = isFav WHERE  `reservations`.`Id` = reservationId;
            cmd.CommandText = "UPDATE   `reservations`  SET   `reservations`.`Details`=?details  `reservations`.`Ranking`=?rate  `reservations`.`IsFavorite`=?isFavorite WHERE  `reservations`.`Id` = ?id; COMMIT;";
            cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = reservation.Id;
            cmd.Parameters.Add("?rate", MySqlDbType.Int32).Value = reservation.Ranking;
            cmd.Parameters.Add("?details", MySqlDbType.VarChar).Value = reservation.Details;
            cmd.Parameters.Add("?isFavorite", MySqlDbType.Binary).Value = reservation.IsFavorite;
            var index = cmd.ExecuteNonQuery();
            _context.Close();
        }
    }
}
