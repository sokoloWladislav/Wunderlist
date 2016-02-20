using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace DAL.Identity
{
    public class ApplicationUserManager : UserManager<DalApplicationUser>
    {
        public ApplicationUserManager(IUserStore<DalApplicationUser> store) : base(store) { }
    }
}
