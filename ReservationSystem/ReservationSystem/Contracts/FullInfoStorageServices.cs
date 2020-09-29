using ReservationSystem.Models;
using ReservationSystem.Models.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Contracts
{
    public interface IFullInfoStorageServices
    {
        /// <summary>
        /// Resturns all values from reservations bay page
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="recordsPerPage"></param>
        /// <param name="orderByCriteria"></param>
        /// <returns></returns>
        List<FullInfoDTO> GetFullInfo(int currentPage = 1, int recordsPerPage=10, OrderByCriteria orderByCriteria = 0);
        /// <summary>
        ///  Count all reservation from database
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Counts all user's reservations
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int CountByUser(Guid userId);
        /// <summary>
        /// returns full reservation information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FullInfoDTO GetFullInfoById(string id);
        /// <summary>
        /// Returns all the information related to the reservations and the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentPage"></param>
        /// <param name="recordsPerPage"></param>
        /// <param name="orderByCriteria"></param>
        /// <returns></returns>
        List<FullInfoDTO> GetFullInfoByUser(string id, int currentPage = 1, int recordsPerPage = 0, OrderByCriteria orderByCriteria = OrderByCriteria.None);
    }
}
