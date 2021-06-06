using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Core.ViewModels;
using VacationRental.Core.Models;
using VacationRental.Core.Services.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
        private readonly IRentalService _rentalService;

        public CalendarController(ICalendarService calendarService, IRentalService rentalService)
        {
            _calendarService = calendarService;
            _rentalService = rentalService;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            if (rentalId <= 0)
                throw new ApplicationException("Rental can't be with id 0");
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (_rentalService.GetRentalById(rentalId) is null  )
                throw new ApplicationException("Rental not found");


            var calendarModel =  _calendarService.GetRentalCalendarByNights(rentalId, start, nights);

            return calendarModel;

        }
    }
}
