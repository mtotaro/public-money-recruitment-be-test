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
    }
}
