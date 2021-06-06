using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services.Interfaces
{
    public interface ICalendarService
    {
        /// <summary>
        /// Get booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CalendarViewModel</returns>
        CalendarViewModel GetCalendarById(int id);
        /// <summary>
        /// Get Rental Calendar by nights
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CalendarViewModel</returns>
        CalendarViewModel GetRentalCalendarByNights(int rentalId, DateTime start, int nights);
    }
}
