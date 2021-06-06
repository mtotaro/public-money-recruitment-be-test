using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationRental.Api
{
    public static class ErrorMessages
    {
        public const string BookingNotFound = "Booking not found";
        public const string BookingsPositie = "Nights must be positive";

        public const string NoVacancy = "No Vacancy while requested period";
        public const string DateAlreadyPassed = "Date already passed. Please choose another date";
        public const string PreparationTimeLessZero = "Preparation Time should be equal zero or positive";

        public const string ValueCannotBeNull = "Value couldn't be NULL";
        public const string ValueCannotBeLessOrZero = "Value couldn't be zero or less";

        public const string RentalNotFound = "Rental not found";
        public const string RentalUnitsZero = "Rental Units Count should be positive";
        public const string RentalIdLessOrZero = "RentalId must be positive";
        public const string UnallowedRentalEditing = "Your current booking schedule doesn't allow decreasing units or increasing preparation days";

    }
}
