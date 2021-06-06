using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Core.ViewModels;
using VacationRental.Core.Models;
using VacationRental.Core.Services.Interfaces;
using Error = VacationRental.Api.ErrorMessages;
using VacationRental.Core.Data.DTO;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        //private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            if (_rentalService.GetRentalById(rentalId) is null)
                throw new ApplicationException("Rental not found");

            var rental = _rentalService.GetRentalById(rentalId);

            return rental;
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            if (model.Units <= 0)
                throw new ApplicationException(Error.RentalUnitsZero);
            if (model.PreparationTimeInDays < 0)
                throw new ApplicationException(Error.PreparationTimeLessZero);

            var rentalId = _rentalService.InsertNewRental(model);

            var key = new ResourceIdViewModel { Id = rentalId };

            return key;
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public RentalViewModel UpdateRentals(int rentalId, [FromBody] RentalBindingModel rentalBindingModel)
        {

            var rentalUpdated = _rentalService.UpdateRental(
                    new RentalDTO
                    {
                        PreparationTimeInDays = rentalBindingModel.PreparationTimeInDays,
                        Units = rentalBindingModel.Units,
                        Id = rentalId,
                    });

            return rentalUpdated;
        }

    }
}
