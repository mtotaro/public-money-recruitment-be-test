using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Interfaces;

namespace VacationRental.Core.Data.Repositories
{
    public class RentalRepository : IDataProvider<RentalDTO>
    {
        private readonly IDictionary<int, RentalDTO> _rentals;

        public RentalRepository(IDictionary<int, RentalDTO> rentals)
        {
            _rentals = rentals;
        }

        public void Delete(RentalDTO entity)
        {
            _rentals.Remove(entity.Id);
        }

        public IEnumerable<RentalDTO> Get()
        {
            return _rentals.Values;
        }

        public RentalDTO Get(int id)
        {
            return _rentals.TryGetValue(id, out var value) ? value : null;
        }

        public int Insert(RentalDTO entity)
        {
            entity.Id = _rentals.Values.Count()+1;
            _rentals.Add(entity.Id, entity);

            return entity.Id;
        }

        public RentalDTO Update(RentalDTO entity)
        {
            _rentals[entity.Id] = entity;
            return entity;
        }
    }
}
