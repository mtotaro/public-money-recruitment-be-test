using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.ViewModels;

namespace VacationRental.Core.Mapper
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<BookingDTO, BookingViewModel>().ReverseMap();

        
        }
    }
}
