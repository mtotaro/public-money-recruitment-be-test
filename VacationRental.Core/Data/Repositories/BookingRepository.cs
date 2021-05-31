using System.Collections.Generic;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;

namespace VacationRental.Core.Data.Repositories
{
    public class BookingRepository : IDataProvider<BookingDTO>
    {
        //ACA VA LA LISTA QUE HACE DE BASE DE DATOS

        public void Delete(BookingDTO entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BookingDTO> Get()
        {
            throw new System.NotImplementedException();
        }

        public BookingDTO Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(BookingDTO entity)
        {
            throw new System.NotImplementedException();
        }

        public BookingDTO Update(BookingDTO entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
