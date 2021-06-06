using AutoMapper;
using System;
using System.Linq;
using VacationRental.Api.Services.Interfaces;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.Models;
using VacationRental.Core.ViewModels;


namespace VacationRental.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDataProvider<BookingDTO> _bookingDataProvider;
        private readonly IDataProvider<RentalDTO> _rentalDataProvider;
        private readonly IMapper _mapper;

        public BookingService(IDataProvider<BookingDTO> bookingDataProvider, IMapper mapper, IDataProvider<RentalDTO> rentalDataProvider)
        {
            _bookingDataProvider = bookingDataProvider;
            _mapper = mapper;
            _rentalDataProvider = rentalDataProvider;
        }

        public BookingViewModel GetBookingById(int id)
        {
            var bookingDTO = _bookingDataProvider.Get(id);
            //ACA VA EL AUTOMAPPER A BOOKINGVIEWMODEL
            var _bookingViewModel = _mapper.Map<BookingViewModel>(bookingDTO);
            return _bookingViewModel;
        }

        public int InsertBooking(BookingBindingModel bookinBindingModel)
        {
            var rental = _rentalDataProvider.Get(bookinBindingModel.RentalId);
            var rentalUnits = rental.Units;
            var preparationTimeInDays = rental.PreparationTimeInDays;
            var bookingDTO = new BookingDTO
            {
                RentalId = bookinBindingModel.RentalId,
                Nights = bookinBindingModel.Nights,
                Start = bookinBindingModel.Start

            };
            var bookingList = _bookingDataProvider.Get();
            var rentalBookings = bookingList.Where(x => x.RentalId == bookinBindingModel.RentalId);

            if (rentalUnits > rentalBookings.Count())
            {
                var id = _bookingDataProvider.Insert(bookingDTO);
                return id;
            }


            var occupiedRentalUnitsCount = 0;
            for (var i = 0; i < bookinBindingModel.Nights; i++)
            {
                foreach (var booking in rentalBookings)
                {
                    if (booking.RentalId == bookinBindingModel.RentalId
                        && (booking.Start <= bookinBindingModel.Start.Date && booking.Start.AddDays(booking.Nights) > bookinBindingModel.Start.Date)
                        || (booking.Start < bookinBindingModel.Start.AddDays(bookinBindingModel.Nights) && booking.Start.AddDays(booking.Nights) >= bookinBindingModel.Start.AddDays(booking.Nights))
                        || (booking.Start > bookinBindingModel.Start && booking.Start.AddDays(booking.Nights) < bookinBindingModel.Start.AddDays(bookinBindingModel.Nights)))
                    {
                        occupiedRentalUnitsCount++;
                    }
                }

                if (occupiedRentalUnitsCount >= rental.Units)
                    throw new ApplicationException("Not Available");
            }

            var lastUnit = rentalBookings.Max(x => x.Unit);
            lastUnit++;
            bookingDTO.Unit = lastUnit;

            var bookingId = _bookingDataProvider.Insert(bookingDTO);

            return bookingId;
        }

    }
}
