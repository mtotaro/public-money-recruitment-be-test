using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;

namespace VacationRental.Core.Data.Repositories
{
    public class BookingRepository : IDataProvider<BookingDTO>
    {
        private readonly IDictionary<int, BookingDTO> _bookings;

        public BookingRepository(IDictionary<int, BookingDTO> bookings)
        {
            _bookings = bookings;
        }

        public void Delete(BookingDTO entity)
        {
            _bookings.Remove(entity.Id);
        }

        public IEnumerable<BookingDTO> Get()
        {
            return _bookings.Values;
        }

        public BookingDTO Get(int id)
        {
            return _bookings.TryGetValue(id, out var value) ? value : null;
        }

        public int Insert(BookingDTO entity)
        {
            entity.Id = _bookings.Values.Count() + 1;
            _bookings.Add(entity.Id, entity);

            return entity.Id;
        }

        public BookingDTO Update(BookingDTO entity)
        {
            _bookings[entity.Id] = entity;
            return entity;
        }
    }
}
