using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using GoodsStore.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStore.Business.Services.Concrete
{
    public class JWTAuthManager : IAuthManager
    {
        private string _secret;
        private IServicesUnitOfWork _uow;

        public JWTAuthManager(IServicesUnitOfWork uow, string secret)
        {
            _uow = uow;
            _secret = secret;
        }

        public async Task<IPrincipal> Authenticate(string token)
        {
            if (ValidateToken(token, out var user))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                foreach (var role in user.Roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal userPrincipal = new ClaimsPrincipal(identity);

                return await Task.FromResult(userPrincipal);
            }

            return await Task.FromResult<IPrincipal>(null);
        }

        public async Task<UserDTO> Authenticate(string login, string password)
        {
            var user = _uow.Users.Get(i => i.Email == login && i.Password == password).FirstOrDefault();
            return await Task.FromResult(user);
        }

        public async Task<UserDTO> Register(UserDTO user)
        {
            var added = _uow.Users.Add(user);
            var token = JwtManager.GenerateToken(added.Id, added.Email, added.Roles, _secret);
            added.Token = token;
            _uow.Users.CreateOrUpdate(user);
            _uow.Save();

            return await Task.FromResult(added);
        }

        public async Task<bool> IsUserExists(UserDTO user)
        {
            return await Task.FromResult(_uow.Users.Get(i => i.Email == user.Email) != null);
        }

        private bool ValidateToken(string token, out UserDTO user)
        {
            user = null;

            var simplePrinciple = JwtManager.GetPrincipal(token, _secret);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            string userIdentifier = userIdClaim?.Value;

            if (string.IsNullOrEmpty(userIdentifier))
                return false;

            int userId = int.Parse(userIdentifier);

            user = _uow.Users.Get(userId);
            if (user == null)
                return false;

            return true;
        }
    }
}
