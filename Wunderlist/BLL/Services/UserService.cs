using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using BLL.Interface.DTO;
using BLL.Interface.Infrastructure;
using BLL.Interface.Interfaces;
using DAL.Interface.Entities;
using DAL.Interface.Identity;
using DAL.Interface.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        //move to repo!!!
        private readonly ApplicationUserManager _userManager;

        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow, ApplicationUserManager userManager)
        {
            _uow = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplicationUserEntity, ApplicationUserDTO>(); });
            mapper = config.CreateMapper();
            _userManager = userManager;
        }

        public OperationDetails CreateUser(ApplicationUserDTO user)
        {
            var appUser = _userManager.FindByName(user.UserName);
            if (appUser == null)
            {
                appUser = new ApplicationUserEntity { UserName = user.UserName, UserProfileName = user.UserProfileName };
                _userManager.Create(appUser, user.Password);
                _uow.Commit();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            return new OperationDetails(false, "Пользователь с таким email уже существует", "");
        }

        public OperationDetails DeleteUser(ApplicationUserDTO user)
        {
            ApplicationUserEntity appUser = _userManager.FindByName(user.UserName);
            if (appUser != null)
            {
                _userManager.Delete(appUser);
                _uow.Commit();
                return new OperationDetails(true, "Удаление прошло успешно", "");
            }
            return new OperationDetails(false, "Пользователь, который должен быть удален не существует", "");
        }

        public IEnumerable<ApplicationUserDTO> GetAllUsers()
        {
            return mapper.Map<IEnumerable<ApplicationUserEntity>, IEnumerable<ApplicationUserDTO>>(_userManager.Users);
        }

        public ApplicationUserDTO GetUserById(string id)
        {

            return mapper.Map<ApplicationUserEntity, ApplicationUserDTO>(_userManager.FindById(id));
        }

        public ClaimsIdentity Authenticate(ApplicationUserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUserEntity user = _userManager.Find(userDto.UserName, userDto.Password);
            if (user != null)
                claim = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}