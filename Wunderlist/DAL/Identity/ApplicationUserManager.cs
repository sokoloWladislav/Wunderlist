using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUserEntity>
    {
        public ApplicationUserManager(IUserStore<ApplicationUserEntity> store) : base(store) { }
    }
}
