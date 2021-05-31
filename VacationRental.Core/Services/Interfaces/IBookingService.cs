using VacationRental.Core.ViewModels;

namespace VacationRental.Api.Services.Interfaces
{
    public interface IBookingService
    {
        /// <summary>
        /// Get booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BookingViewModel</returns>
        BookingViewModel GetBookingById(int id);
    }
}
