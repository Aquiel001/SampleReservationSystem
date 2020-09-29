using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public class BaseEntity
    {
        public Guid Id;
    }

    /// <summary>
    /// User's entity Model
    /// </summary>
    public class User
    {
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
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Mandatory Contact Type. This field must exist in the database
        /// </summary>
        [JsonProperty("contactType")]
        [Required]
        public UserType ContactType { get; set; } = new UserType();

    }

    /// <summary>
    /// Reservations entity
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Unique identifier from database
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Ranking
        /// </summary>
        public int Ranking { get; set; }
        /// <summary>
        /// Mark as favorite or not
        /// </summary>
        public bool IsFavorite { get; set; }
        /// <summary>
        /// Id from user who owns the reservation
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Reservation's details
        /// </summary>
        public string Details { get; set; }

    }

    /// <summary>
    /// User's Types
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// Unique identifier from database
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// user's type description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
