using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.Services.Interfaces;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services
{
    public class RentalService : IRentalService
    {
        private readonly IDataProvider<RentalDTO> _rentalDataProvider;

        public RentalService(IDataProvider<RentalDTO> rentalDataProvider)
        {
            _rentalDataProvider = rentalDataProvider;
        }

        public RentalViewModel GetRentalById(int id)
        {
            var rentalDTO = _rentalDataProvider.Get(id);
            //ACA VA EL AUTOMAPPER A BOOKINGVIEWMODEL
            throw new NotImplementedException();
        }
    }
}
