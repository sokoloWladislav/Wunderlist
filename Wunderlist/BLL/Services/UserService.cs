using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork db;

        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplicationUserEntity, ApplicationUserDTO>(); });
            mapper = config.CreateMapper();
        }

        public OperationDetails CreateUser(ApplicationUserDTO user)
        {
            ApplicationUserEntity appUser = db.UserManager.FindByName(user.UserName);
            if (appUser == null)
            {
                appUser = new ApplicationUserEntity { UserName = user.UserName, UserProfileName = user.UserProfileName };
                db.UserManager.Create(appUser, user.Password);
                db.Commit();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            return new OperationDetails(false, "Пользователь с таким email уже существует", "");
        }

        public OperationDetails DeleteUser(ApplicationUserDTO user)
        {
            ApplicationUserEntity appUser = db.UserManager.FindByName(user.UserName);
            if (appUser != null)
            {
                db.UserManager.Delete(appUser);
                db.Commit();
                return new OperationDetails(true, "Удаление прошло успешно", "");
            }
            return new OperationDetails(false, "Пользователь, который должен быть удален не существует", "");
        }

        public IEnumerable<ApplicationUserDTO> GetAllUsers()
        {
            return mapper.Map<IEnumerable<ApplicationUserEntity>, IEnumerable<ApplicationUserDTO>>(db.UserManager.Users);
        }

        public ApplicationUserDTO GetUserById(string id)
        {

            return mapper.Map<ApplicationUserEntity, ApplicationUserDTO>(db.UserManager.FindById(id));
        }

        public ClaimsIdentity Authenticate(ApplicationUserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUserEntity user = db.UserManager.Find(userDto.UserName, userDto.Password);
            if (user != null)
                claim = db.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}