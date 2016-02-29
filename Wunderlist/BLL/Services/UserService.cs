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

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager _userManager;

        public UserService(IUnitOfWork uow,IUserRepository userRepository, ApplicationUserManager manager)
        {
            _uow = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplicationUserEntity, ApplicationUserDTO>(); });
            _mapper = config.CreateMapper();
            _userRepository = userRepository;
            _userManager = manager;
        }

        public OperationDetails CreateUser(ApplicationUserDTO user)
        {
            var appUser = _userRepository.FindByName(user.UserName);
            if (appUser == null)
            {
                appUser = new ApplicationUserEntity { UserName = user.UserName, UserProfileName = user.UserProfileName };
                _userRepository.CreateUserEntity(appUser,user.Password);
                _uow.Commit();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            return new OperationDetails(false, "Пользователь с таким email уже существует", "");
        }

        public OperationDetails DeleteUser(ApplicationUserDTO user)
        {
            ApplicationUserEntity appUser = _userRepository.FindByName(user.UserName);
            if (appUser != null)
            {
                _userRepository.Delete(appUser);
                _uow.Commit();
                return new OperationDetails(true, "Удаление прошло успешно", "");
            }
            return new OperationDetails(false, "Пользователь, который должен быть удален не существует", "");
        }

        public IEnumerable<ApplicationUserDTO> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<ApplicationUserEntity>, IEnumerable<ApplicationUserDTO>>(_userRepository.GetUsers());
        }

        public ApplicationUserDTO GetUserById(string id)
        {

            return _mapper.Map<ApplicationUserEntity, ApplicationUserDTO>(_userRepository.FindById(id));
        }

        public ClaimsIdentity Authenticate(ApplicationUserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUserEntity user = _userRepository.Find(userDto.UserName, userDto.Password);
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