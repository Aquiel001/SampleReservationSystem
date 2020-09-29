using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.DataHolders
{
    /// <summary>
    /// Data Holder  for paged
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paging<T>
    {
        public int RecordsPerPage { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public List<T> Outcome { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
