using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservationSystem.Contracts;
using ReservationSystem.Models;

namespace ReservationSystem.Controllers
{
    /// <summary>
    /// user Controller
    /// </summary>
    [Route("usertype")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly ILogger<ReservationsController> _logger;
        private readonly IUserTypesStorageService _typeStorage;
        public UserTypeController(ILogger<ReservationsController> logger, IUserTypesStorageService typeStorage)
        {
            _logger = logger;
            _typeStorage = typeStorage;

        }



        /// <summary>
        /// Get users types
        /// </summary>
        /// <returns> Returns all user types from database</returns>
        /// <response code="200"><seealso cref="List{User}"/>Unexpected error</response>
        [HttpGet]
        public ActionResult<List<UserType>> Get()
        {
            return Ok(_typeStorage.GetUserTypes());
        }
    }
}