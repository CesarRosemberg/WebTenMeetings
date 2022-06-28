using System.Linq;

namespace WebTenMeetings.Controllers

{
    public interface IGenericRepository<T> where T : class
    {
        T Find(int id);
        IQueryable<T> List();
        void Add(T item);
        void Remove(T item);
        void Edit(T item);
    }
}
