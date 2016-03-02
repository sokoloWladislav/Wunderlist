using System;
using System.Collections.Generic;
using System.Security.Claims;
using BLL.Interface.DTO;
using BLL.Interface.Infrastructure;

namespace BLL.Interface.Interfaces
{
    public interface IUserService : IDisposable
    {
        OperationDetails CreateUser(ApplicationUserDTO user);
        OperationDetails DeleteUser(ApplicationUserDTO user);
        ApplicationUserDTO GetUserById(string id);
        IEnumerable<ApplicationUserDTO> GetAllUsers();
        ClaimsIdentity Authenticate(ApplicationUserDTO user);
    }
}
