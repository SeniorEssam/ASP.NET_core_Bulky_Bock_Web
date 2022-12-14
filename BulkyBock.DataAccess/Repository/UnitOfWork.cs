using BulkyBock.DataAccess.Data;
using BulkyBock.DataAccess.Repository.IRepository;

namespace BulkyBock.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category=new CategoryRepository(db);
            CoverType = new CoverTypeRepository(db);
            Product = new ProductRepository(db);
        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
