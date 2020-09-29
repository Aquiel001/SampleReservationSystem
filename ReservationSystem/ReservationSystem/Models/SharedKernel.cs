using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    /// <summary>
    /// Sort criterion for filtering
    /// </summary>
    public enum OrderByCriteria
    {
        /// <summary>
        /// Without criteria
        /// </summary>
        None = 0,
        /// <summary>
        /// By BirthDate Ascending
        /// </summary>
        ByDateAscending=1,
        /// <summary>
        /// By BirthDate Descending
        /// </summary>
        ByDateDescending = 2,
        /// <summary>
        /// By Alphabetic Ascending
        /// </summary>
        ByAlphabeticAscending = 3,
        /// <summary>
        /// By Alphabetic Descending
        /// </summary>
        ByAlphabeticDescending = 4,
        /// <summary>
        /// By Ranking Ascending
        /// </summary>
        ByRankingAscending = 5,
        /// <summary>
        /// By Raning Descending
        /// </summary>
        ByRankingDescending = 6
    }
}
