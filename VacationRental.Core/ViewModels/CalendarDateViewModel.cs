using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VacationRental.Core.ViewModels
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
        [JsonProperty("free_units")]
        public int Unit { get; set; }
    }
}
