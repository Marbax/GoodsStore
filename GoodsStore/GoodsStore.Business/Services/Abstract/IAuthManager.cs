using GoodsStore.Business.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStore.Business.Services.Abstract
{
    public interface IAuthManager
    {
        Task<IPrincipal> Authenticate(string token);
        Task<UserDTO> Authenticate(string login, string password);
        Task<bool> IsUserExists(string email);
        Task<UserDTO> Register(UserDTO user);
        Task<UserDTO> UpdateProfile(UserDTO user);
    }
}
