using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IDataProvider<CalendarDTO> _calendarDataProvider;

        public CalendarService(IDataProvider<CalendarDTO> calendarDataProvider)
        {
            _calendarDataProvider = calendarDataProvider;
        }

        public CalendarViewModel GetCalendarById(int id)
        {
            var calendarDTO = _calendarDataProvider.Get(id);
            //ACA VA EL AUTOMAPPER A BOOKINGVIEWMODEL
            throw new NotImplementedException();
        }
    }
}
