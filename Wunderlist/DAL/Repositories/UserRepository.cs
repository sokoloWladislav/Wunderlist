using System;
using System.Collections.Generic;
using DAL.Interface.Entities;
using DAL.Interface.Identity;
using DAL.Interface.Repositories;
using Microsoft.AspNet.Identity;

namespace DAL.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationUserManager _userManager;

        public UserRepository(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public void CreateUserEntity(ApplicationUserEntity userEntity,string password)
        {
            _userManager.Create(userEntity, password);
        }

        public ApplicationUserEntity FindByName(string name)
        {
            return _userManager.FindByName(name);
        }

        public ApplicationUserEntity FindById(string id)
        {
            return _userManager.FindById(id);
        }

        public ApplicationUserEntity Find(string name, string password)
        {
            return _userManager.Find(name, password);
        }

        public void Delete(ApplicationUserEntity user)
        {
            var currentUser = FindById(user.Id);
            _userManager.Delete(currentUser);
        }

        public IEnumerable<ApplicationUserEntity> GetUsers()
        {
            return _userManager.Users;
        }

        public bool Update(ApplicationUserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
