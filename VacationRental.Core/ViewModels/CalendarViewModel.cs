using System.Collections.Generic;

namespace VacationRental.Core.ViewModels
{
    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewModel> Dates { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
