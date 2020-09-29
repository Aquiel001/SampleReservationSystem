using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Contracts
{
    public interface IUserTypesStorageService
    {
        /// <summary>
        /// Returns  all user types from Data base
        /// </summary>
        /// <returns></returns>
        List<UserType> GetUserTypes();
    }
}
