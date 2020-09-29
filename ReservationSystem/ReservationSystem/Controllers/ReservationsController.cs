using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ReservationSystem.Contracts;
using ReservationSystem.Models;
using ReservationSystem.Models.DataHolders;
using ReservationSystem.Models.Storage;

namespace ReservationSystem.Controllers
{
    /// <summary>
    /// Main controller from reservation operations
    /// </summary>
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {

        private readonly ILogger<ReservationsController> _logger;
        private readonly IUserStorageServices _userStorage;
        private readonly IReservationStorageService _reservationStorage;
        private readonly IFullInfoStorageServices _fullInfoStorageServices;
        public ReservationsController(ILogger<ReservationsController> logger, IUserStorageServices userStorage, IFullInfoStorageServices fullInfoStorageServices, IReservationStorageService reservationStorageService)
        {
            _logger = logger;
            _userStorage = userStorage;
            _fullInfoStorageServices = fullInfoStorageServices;
            _reservationStorage = reservationStorageService;

        }

        /// <summary>
        /// Returns all the related information in the database by paging and filtering
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="totalRecords"></param>
        /// <param name="orderByCriteria"></param>
        /// <returns>Returns all reservations according to the chosen params.</returns>
        /// <response code="200"><seealso cref="FullInfoDTO"/>Returnsa list according to the chosen params </response>
        /// <response code="400">Unexpected error</response>
        [HttpGet]
        public ActionResult<Paging<FullInfoDTO>> Get(int currentPage = 1, int totalRecordsByPage = 10, OrderByCriteria orderByCriteria = 0)
        {

            try
            {
                var response = _fullInfoStorageServices.GetFullInfo(currentPage, totalRecordsByPage, orderByCriteria);
                var totalRecords = _fullInfoStorageServices.Count();

                var totalPages = (int)Math.Ceiling((double)totalRecords / totalRecordsByPage);

                var paginated = new Paging<FullInfoDTO>()
                {
                    RecordsPerPage = totalRecordsByPage,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    CurrentPage = currentPage,
                    Outcome = response
                };
                return Ok(paginated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Mark as favorite one reservation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalRecordsByPage"></param>
        /// <param name="orderByCriteria"></param>
        /// <returns>An updated lis of reservations</returns>
        /// <response code="200"><seealso cref="Paging{FullInfoDTO}"/>}Ok</response>
        /// <response code="400">Unexpected error</response>
        [Route("favorite")]
        [HttpPut]
        public ActionResult<Paging<FullInfoDTO>> MarkAsFavorite([FromBody]ReservationMarkAsFavoriteRequest request, int currentPage = 1, int totalRecordsByPage = 10, OrderByCriteria orderByCriteria = 0)
        {
            try
            {
                _reservationStorage.MarkReservationAsFav(request.IsFav, request.Id);
                var response = _fullInfoStorageServices.GetFullInfo(currentPage, totalRecordsByPage, orderByCriteria);
                var totalRecords = _fullInfoStorageServices.Count();

                var totalPages = (int)Math.Ceiling((double)totalRecords / totalRecordsByPage);

                var paginated = new Paging<FullInfoDTO>()
                {
                    RecordsPerPage = totalRecordsByPage,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    CurrentPage = currentPage,
                    Outcome = response
                };
                return Ok(paginated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Update the ranking from one reservation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="reservationId"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalRecordsByPage"></param>
        /// <param name="orderByCriteria"></param>
        /// <returns> Returns an updated list of reservations</returns>
        /// <response code="200"><seealso cref="Paging{FullInfoDTO}"/>}Ok</response>
        /// <response code="400">Unexpected error</response>
        [Route("rate/{reservationId}")]
        [HttpPut]
        public ActionResult<Paging<FullInfoDTO>> UpdateRanking([FromBody]RateRequest request, Guid reservationId, int currentPage = 1, int totalRecordsByPage = 10, OrderByCriteria orderByCriteria = 0)
        {
            try
            {
                _reservationStorage.UpdatekReservationRate(request.Rate, reservationId);
                var response = _fullInfoStorageServices.GetFullInfo(currentPage, totalRecordsByPage, orderByCriteria);
                var totalRecords = _fullInfoStorageServices.Count();

                var totalPages = (int)Math.Ceiling((double)totalRecords / totalRecordsByPage);

                var paginated = new Paging<FullInfoDTO>()
                {
                    RecordsPerPage = totalRecordsByPage,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    CurrentPage = currentPage,
                    Outcome = response
                };
                return Ok(paginated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Returns the completeinformations fby reservation
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns>all details from some reservation</returns>
        /// <response code="200"><seealso cref="FullInfoDTO"/>}Ok</response>
        /// <response code="400">Unexpected error</response>
        [Route("{reservationId}")]
        [HttpGet]
        public ActionResult<Paging<FullInfoDTO>> GetFullInfoById(string reservationId)
        {
            try
            {
                var response = _fullInfoStorageServices.GetFullInfoById(reservationId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                    return BadRequest(ex.Message);

                throw;
            }
        }


        /// <summary>
        ///Update a reservation into the system
        /// </summary>
        /// <param name="request"></param>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Unexpected error</response>
        [Route("add/{reservationId}")]
        [HttpPut]
        public ActionResult<User> UpdateReservation([FromBody]FullInfoDTORequest request,string reservationId)
        {
            try
            {
                
                var user = new User()
                {
                    BirthDate = DateTime.Parse(request.BirthDate),
                    ContactName = request.ContactName,
                    ContactType = request.ContactType,
                    PhoneNumber = request.PhoneNumber,
                    Id= request.UserId
                };
                var reservation = new Reservation() {
                    Details = request.Details,
                    IsFavorite = request.IsFavorite,
                    Ranking = request.Ranking,
                    UserId = user.Id                    
               };
                if(!string.IsNullOrEmpty(reservationId))
                {
                    reservation.Id = Guid.Parse(reservationId);
                }

                var response = _userStorage.AddUser(user,reservation);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
            
        }
        /// <summary>
        /// Create or update a reservation into system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Unexpected error</response>
        [Route("add")]
        [HttpPut]
        public ActionResult<User> AddReservation([FromBody]FullInfoDTORequest request)
        {
            try
            {

                var user = new User()
                {
                    BirthDate = DateTime.Parse(request.BirthDate),
                    ContactName = request.ContactName,
                    ContactType = request.ContactType,
                    PhoneNumber = request.PhoneNumber,
                    Id = request.UserId
                };
                var reservation = new Reservation()
                {
                    Details = request.Details,
                    IsFavorite = request.IsFavorite,
                    Ranking = request.Ranking,
                    UserId = user.Id
                };


                var response = _userStorage.AddUser(user, reservation);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Returns user's info
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalRecordsByPage"></param>
        /// <param name="orderByCriteria"></param>
        /// <returns></returns>
        /// <response code="200"><seealso cref="Paging{FullInfoDTO}"/>Ok</response>
        /// <response code="400">Unexpected error</response>
        [Route("user/{userId}")]
        [HttpGet]
        public ActionResult<Paging<FullInfoDTO>> GetByUser(Guid userId, int currentPage = 0, int totalRecordsByPage = 10, OrderByCriteria orderByCriteria = 0)
        {


            try
            {
                var response = _fullInfoStorageServices.GetFullInfoByUser(userId.ToString(), currentPage, totalRecordsByPage, orderByCriteria);
                var totalRecords = _fullInfoStorageServices.Count();

                var totalPages = (int)Math.Ceiling((double)totalRecords / totalRecordsByPage);

                var paginated = new Paging<FullInfoDTO>()
                {
                    RecordsPerPage = totalRecordsByPage,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    CurrentPage = currentPage,
                    Outcome = response
                };
                return Ok(paginated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
