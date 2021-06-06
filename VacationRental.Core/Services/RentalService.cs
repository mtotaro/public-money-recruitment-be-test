using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.Models;
using VacationRental.Core.Services.Interfaces;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services
{
    public class RentalService : IRentalService
    {
        private readonly IDataProvider<RentalDTO> _rentalDataProvider;
        private readonly IDataProvider<BookingDTO> _bookingDataProvider;
        private readonly IMapper _mapper;

        public RentalService(IDataProvider<RentalDTO> rentalDataProvider, IMapper mapper, IDataProvider<BookingDTO> bookingDataProvider)
        {
            _rentalDataProvider = rentalDataProvider;
            _mapper = mapper;
            _bookingDataProvider = bookingDataProvider;
        }

        public RentalViewModel GetRentalById(int id)
        {
            var rentalDTO = _rentalDataProvider.Get(id);
            var _rentalViewModel = _mapper.Map<RentalViewModel>(rentalDTO);
            return _rentalViewModel;
        }

        public int InsertNewRental(RentalBindingModel rentalEntity)
        {
            var rentalDto = new RentalDTO
            {
                Units = rentalEntity.Units,
                PreparationTimeInDays = rentalEntity.PreparationTimeInDays
            };

            var rentalId = _rentalDataProvider.Insert(rentalDto);

            if (rentalId <= 0)
                throw new ApplicationException("There was an error");

            return rentalId;
        }

        public RentalViewModel UpdateRental(RentalDTO rentalDTO)
        {
            var rentalEntity = _rentalDataProvider.Get(rentalDTO.Id);
            if (rentalEntity is null)
                throw new ArgumentException("dont Exists");

            var bookings = _bookingDataProvider.Get().Where(x=>x.RentalId== rentalDTO.Id);

            if (CheckUnitsQuantityBookedAlready(bookings, rentalEntity, rentalDTO.Units, out int currentUnitsBooked))
                throw new ArgumentException("Can't change units quantity, they had been already booked" + currentUnitsBooked + " units");

            if (CheckBookingDatesOverlap(bookings, rentalEntity))
                throw new ArgumentException("Dates overlapping on booked units");

            var rental =  _rentalDataProvider.Update(rentalDTO);

            var _rentalViewModel = _mapper.Map<RentalViewModel>(rental);
            return _rentalViewModel;
        }

        private bool CheckUnitsQuantityBookedAlready(IEnumerable<BookingDTO> bookings, RentalDTO rentalEntity, int newUnitsToUpdate, out int currentUnitsBooked)
        {
            var currentUnitsBookedResult = 0;
            foreach (var booking in bookings)
            {
                var lastDateTimeBooking = booking.Start.AddDays(booking.Nights + rentalEntity.PreparationTimeInDays);

                if (lastDateTimeBooking >= DateTime.Now.Date)
                    currentUnitsBookedResult++;
            }

            currentUnitsBooked = currentUnitsBookedResult;

            return currentUnitsBooked > newUnitsToUpdate;
        }

        private bool CheckBookingDatesOverlap(IEnumerable<BookingDTO> bookings, RentalDTO rentalEntity)
        {
            var listDates = new List<BookingDateStartEndModel>();
            foreach (var booking in bookings)
            {
                listDates.Add(new BookingDateStartEndModel
                {
                    Start = booking.Start,
                    End = booking.Start.AddDays(booking.Nights + rentalEntity.PreparationTimeInDays)
                });
            }

            var datesOverlapping = false;
            for (int date = 0; date < listDates.Count; date++)
            {
                if (datesOverlapping)
                    break;

                for (int dateToCheck = 0; dateToCheck < listDates.Count; dateToCheck++)
                {
                    if (date == dateToCheck)
                        continue;

                    if (listDates[date].Start <= listDates[dateToCheck].End && listDates[dateToCheck].Start <= listDates[date].End)
                    {
                        datesOverlapping = true;
                        break;
                    }
                }
            }

            return datesOverlapping;
        }
    }
}
