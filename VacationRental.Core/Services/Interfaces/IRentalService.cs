using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
