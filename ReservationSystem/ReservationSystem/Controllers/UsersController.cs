using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservationSystem.Contracts;
using ReservationSystem.Models;
using ReservationSystem.Models.DataHolders;

namespace ReservationSystem.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly ILogger<ReservationsController> _logger;
        private readonly IUserStorageServices _userStorage;
        private readonly IReservationStorageService _reservationStorage;
        private readonly IFullInfoStorageServices _fullInfoStorageServices;
        public UsersController(ILogger<ReservationsController> logger, IUserStorageServices userStorage, IFullInfoStorageServices fullInfoStorageServices, IReservationStorageService reservationStorageService)
        {
            _logger = logger;
            _userStorage = userStorage;
            _fullInfoStorageServices = fullInfoStorageServices;
            _reservationStorage = reservationStorageService;

        }

        /// <summary>
        /// Get user information by user Contact
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Returns all information from user by name</returns>
        /// <response code="200"><seealso cref="Paging{FullInfoDTO"/>}Ok</response>
        /// <response code="404">Not found any user</response>
        /// <response code="400">Unexpected error</response>
        [Route("{userName}")]
        [HttpGet]
        public ActionResult<Paging<FullInfoDTO>> GetUserByName(string userName)
        {
            try
            {
                if (_userStorage.UserExist(userName))
                {
                    return Ok(_userStorage.GetSimpleUsers(userName).FirstOrDefault());
                }
                return NotFound("User not Found");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <response code="200"><seealso cref="Paging{FullInfoDTO"/>}Ok</response>
        /// <response code="404">Not found any user</response>
        /// <response code="400">Unexpected error</response>
        [Route("{userName}")]
        [HttpDelete]
        public IActionResult DeleteUserByName(string userName)
        {

            if (_userStorage.UserExist(userName))
            {
                try
                {
                    _userStorage.DeleteUser(userName);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
            return NotFound("User not Found");


        }
    }
}