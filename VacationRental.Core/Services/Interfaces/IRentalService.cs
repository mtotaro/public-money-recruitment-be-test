using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Models;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Services.Interfaces
{
    public interface IRentalService
    {
        /// <summary>
        /// Get booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>RetanlViewModel</returns>
        RentalViewModel GetRentalById(int id);
        int InsertNewRental(RentalBindingModel rentalEntity);

        RentalViewModel UpdateRental(RentalDTO rentalViewModel);

    }
}
