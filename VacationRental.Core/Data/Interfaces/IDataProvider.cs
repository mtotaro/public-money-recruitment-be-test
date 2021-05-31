using System.Collections.Generic;

namespace VacationRental.Core.Data.Interfaces
{
    public interface IDataProvider<T> where T : class
    {
        IEnumerable<T> Get();

        T Get(int id);

        int Insert(T entity);

        T Update(T entity);

        void Delete(T entity);
    }
}
