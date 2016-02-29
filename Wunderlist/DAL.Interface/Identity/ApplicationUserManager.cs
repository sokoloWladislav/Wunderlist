using Microsoft.AspNet.Identity;
using DAL.Interface.Entities;

namespace DAL.Interface.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUserEntity>
    {
        public ApplicationUserManager(IUserStore<ApplicationUserEntity> store) : base(store) { }
    }
}
