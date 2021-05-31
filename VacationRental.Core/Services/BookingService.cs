using System;
using VacationRental.Api.Services.Interfaces;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDataProvider<BookingDTO> _bookingDataProvider;

        public BookingService(IDataProvider<BookingDTO> bookingDataProvider)
        {
            _bookingDataProvider = bookingDataProvider;   
        }

        public BookingViewModel GetBookingById(int id)
        {
           var bookingDTO = _bookingDataProvider.Get(id);
            //ACA VA EL AUTOMAPPER A BOOKINGVIEWMODEL
            throw new NotImplementedException();
        }
    }
}
