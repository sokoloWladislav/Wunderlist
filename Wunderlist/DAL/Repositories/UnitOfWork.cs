using System.Data.Entity;
using DAL.Interface.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;
        
        public UnitOfWork(DbContext dbContext)
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
