using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.Services.Interfaces;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IDataProvider<CalendarDTO> _calendarDataProvider;
        private readonly IDataProvider<BookingDTO> _bookingDataProvider;
        private readonly IDataProvider<RentalDTO> _rentalDataProvider;
        private readonly IMapper _mapper;

        public CalendarService(IDataProvider<CalendarDTO> calendarDataProvider, IMapper mapper, IDataProvider<RentalDTO> rentalDataProvider, IDataProvider<BookingDTO> bookingDataProvider)
        {
            _calendarDataProvider = calendarDataProvider;
            _mapper = mapper;
            _rentalDataProvider = rentalDataProvider;
            _bookingDataProvider = bookingDataProvider;
        }

        public CalendarViewModel GetCalendarById(int id)
        {
            var calendarDTO = _calendarDataProvider.Get(id);
            var _calendarViewModel = _mapper.Map<CalendarViewModel>(calendarDTO);
            return _calendarViewModel; 
        }

        public CalendarViewModel GetRentalCalendarByNights(int rentalId, DateTime start, int nights)
        {
            GetRentalCalendarByNightsValidations(rentalId, start, nights);

            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>()
            };


            var rentalBooking = _bookingDataProvider.Get().Where(x => x.RentalId == rentalId);
            var rentals = _rentalDataProvider.Get(rentalId);

            var units = rentals.Units;

            for (var i = 0; i < nights; i++)
            {
                int occupiedUnits = 0;
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>()
                };

                foreach (var booking in rentalBooking)
                {
                    DateTime bookingEnd = booking.Start.AddDays(booking.Nights);
                    DateTime bookingStart = booking.Start;

                    if (bookingStart <= date.Date && bookingEnd >= date.Date)
                    {
                        occupiedUnits += 1;
                        date.Bookings.Add(new CalendarBookingViewModel
                        {
                            Id = booking.Id,
                            Nights = booking.Nights
                        });
                    }
                }
                date.Unit = units - occupiedUnits;
                result.Dates.Add(date);
            }

            result.PreparationTimeInDays  = rentals.PreparationTimeInDays;

            return result;
        }

        private void GetRentalCalendarByNightsValidations(int rentalId, DateTime start, int nights)
        {
            if (rentalId == 0)
                throw new InvalidOperationException($"{nameof(rentalId)} can't be 0");

            if (start == DateTime.MinValue)
                throw new InvalidOperationException($"{nameof(start)} can't be {DateTime.MinValue}");

            if (nights == 0)
                throw new InvalidOperationException($"{nameof(nights)} can't be 0");
        }
    }
}
