using System.Collections.Generic;
using DAL.Interface.Entities;

namespace DAL.Interface.Repositories
{
    public interface IUserRepository
    {
        void CreateUserEntity(ApplicationUserEntity user,string password);
        ApplicationUserEntity FindByName(string name);
        ApplicationUserEntity FindById(string id);
        ApplicationUserEntity Find(string name, string password);
        IEnumerable<ApplicationUserEntity> GetUsers(); 
        bool Update(ApplicationUserEntity user);
        void Delete(ApplicationUserEntity user);
    }
}
