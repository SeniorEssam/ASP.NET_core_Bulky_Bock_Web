using BulkyBock.DataAccess.Data;
using BulkyBock.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBock.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.Products.Include(u => u.Category).Include(u => u.CoverType);
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(IncludeProperties != null)
            {
                foreach (var IncludeProp in IncludeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string? IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query= query.Where(Filter);
            if (IncludeProperties != null)
            {
                foreach (var IncludeProp in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
