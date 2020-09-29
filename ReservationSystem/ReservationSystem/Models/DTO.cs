using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    /// <summary>
    /// Get all the related information in the database
    /// </summary>
    public class FullInfoDTO : User
    {
        /// <summary>
        /// Reservations list from user
        /// </summary>
        [JsonProperty("reservationos")]
        public virtual List<Reservation> Reservations { get; set; }

        /// <summary>
        /// It is favorite property
        /// </summary>
        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; } = false;
        /// <summary>
        /// Ranking counter
        /// </summary>
        [JsonProperty("ranking")]
        public int Ranking { get; set; } = 3;
        /// <summary>
        /// Reservation's details
        /// </summary>
        [JsonProperty("details")]
        public string Details { get; set; }
        /// <summary>
        /// Id of the user involved
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; } = Guid.NewGuid();
    }
    public class FullInfoDTORequest
    {
        /// <summary>
        /// List of user reservations
        /// </summary>
        [JsonProperty("reservations")]
        public virtual List<Reservation> Reservations { get; set; }

        /// <summary>
        /// It is favorite property 
        /// </summary>
        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; } = false;
        /// <summary>
        /// Rate counter
        /// </summary>
        [JsonProperty("ranking")]
        public int Ranking { get; set; } = 3;
        /// <summary>
        /// reservation's details
        /// </summary>
        [JsonProperty("details")]
        public string Details { get; set; }
        /// <summary>
        /// Id of the user involved
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Unique identifier from database
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Mandatory Contact Name
        /// </summary>
        [JsonProperty("contactName")]
        [Required]
        public string ContactName { get; set; }
        /// <summary>
        /// Mandatory Phone number
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Mandatory BithDate
        /// </summary>
        [JsonProperty("birthDate")]
        [Required]
        public string BirthDate { get; set; }
        /// <summary>
        /// Mandatory Contact Type. This field must exist in the database
        /// </summary>
        [JsonProperty("contactType")]
        [Required]
        public UserType ContactType { get; set; } = new UserType();
    }
}
