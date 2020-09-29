using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Contracts
{
    public interface IUserStorageServices
    {
        /// <summary>
        /// Add a new user into the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        long AddUser(User user, Reservation reservation);

        /// <summary>
        /// Returns a collection with a basic info by users
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns><seealso cref="List{User}"/></returns>
        List<User> GetSimpleUsers(string UserName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UserExist(string cad, bool exist = false);
    }
}
