using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBock.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        T GetFirstOrDefault(Expression<Func<T,bool>> Filter,string? IncludeProperties = null);
        IEnumerable<T> GetAll(string? IncludeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
