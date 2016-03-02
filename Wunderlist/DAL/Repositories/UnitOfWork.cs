using DAL.Interface.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _db;
        
        public UnitOfWork(IdentityDbContext dbContext)
        {
            _db = dbContext;
        }


        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
           
        }
        
        
    }
}
