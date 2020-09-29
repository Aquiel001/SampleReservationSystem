using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Contracts
{
    public interface IReservationStorageService
    {
        /// <summary>
        /// Mark a reservation as favorite
        /// </summary>
        /// <param name="isFav"></param>
        /// <param name="id">reservation Id</param>
        /// <param name="currentPage"></param>
        /// <param name="recordsPerPage"></param>
        void MarkReservationAsFav(bool isFav, Guid id,int currentPage = 1, int recordsPerPage = 10);
        /// <summary>
        /// update ranking from reservation
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="id"></param>
        /// <param name="currentPage"></param>
        /// <param name="recordsPerPage"></param>
        void UpdatekReservationRate(int rate, Guid id, int currentPage = 1, int recordsPerPage = 10);
        /// <summary>
        /// Add a new reservatin into the system
        /// </summary>
        /// <param name="reservation"></param>
        void AddReservation(Reservation reservation);
        /// <summary>
        /// Update some reservation
        /// </summary>
        /// <param name="reservation"></param>
        void UpdatekReservation(Reservation reservation);
    }
}
