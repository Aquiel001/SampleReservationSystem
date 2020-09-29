using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.DataHolders
{
    /// <summary>
    /// Data holder to mark as favorite some reservation
    /// </summary>
    public class ReservationMarkAsFavoriteRequest
    {
        public bool IsFav { get; set; }
        public Guid Id { get; set; }
    }
    /// <summary>
    /// Data Holder to udate ranking of some reservation
    /// </summary>
    public class RateRequest
    {
        public int Rate { get; set; }
    }
}
