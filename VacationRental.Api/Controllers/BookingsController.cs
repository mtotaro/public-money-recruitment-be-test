using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Core.ViewModels;
using VacationRental.Core.Models;
using VacationRental.Api.Services.Interfaces;
using Error = VacationRental.Api.ErrorMessages;
using VacationRental.Core.Services.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IRentalService _rentalService;
      
        public BookingsController(IBookingService bookingService, IRentalService rentalService)
        {
            _bookingService = bookingService;
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            var bookingViewModel = _bookingService.GetBookingById(bookingId);

            if (bookingViewModel == null)
                throw new ApplicationException("Booking not found");

            return bookingViewModel;
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model )
        {
            if (model==null)
                throw new ApplicationException(Error.BookingNotFound);
            if (model.Nights <= 0)
                throw new ApplicationException(Error.BookingsPositie);
            if (model.RentalId <= 0)
                throw new ApplicationException(Error.RentalIdLessOrZero);
            if (model.Start < DateTime.Now.AddDays(-1))
                throw new ApplicationException(Error.DateAlreadyPassed);

            var rental = _rentalService.GetRentalById(model.RentalId);

            if (rental is null)
                throw new ApplicationException(Error.RentalNotFound);

            var bookingId = _bookingService.InsertBooking(model);

            return new ResourceIdViewModel { Id = bookingId };

        }
    }
}
