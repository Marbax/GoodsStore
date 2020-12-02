using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using GoodsStore.JWTAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GoodsStore.Business.Services.Concrete
{
    public class JWTAuthManager : IAuthManager
    {
        private readonly string _secret;
        private readonly IServicesUnitOfWork _uow;

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
                var roles = _uow.Roles.Get(i => user.RoleIds.Contains(i.Id));
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.Title));

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal userPrincipal = new ClaimsPrincipal(identity);

                return await Task.FromResult(userPrincipal);
            }

            return await Task.FromResult<IPrincipal>(null);
        }

        public async Task<UserDTO> Authenticate(string login, string password)
        {
            var user = _uow.Users.Get(i => i.Email == login && i.Password == password).FirstOrDefault();

            if (user == null)
                throw new ApplicationException("There is no user with such a paswword and email.");

            if (string.IsNullOrEmpty(user.Token) || !ValidateToken(user.Token, out var _))
                UpdateToken(user);

            return await Task.FromResult(user);
        }

        public async Task<UserDTO> Register(UserDTO user)
        {
            var toAdd = user;
            var role = _uow.Roles.Get(i => i.Title == "cashier").FirstOrDefault();
            toAdd.RoleIds = new List<int>() { role.Id }; ;
            var added = _uow.Users.Add(toAdd);

            UpdateToken(added);

            return await Task.FromResult(added);
        }

        private void UpdateToken(UserDTO user)
        {
            var roles = _uow.Roles.Get(i => user.RoleIds.Contains(i.Id)).Select(i => i.Title);
            var token = JwtManager.GenerateToken(user.Id, user.Email, roles, _secret);
            user.Token = token;
            _uow.Users.CreateOrUpdate(user);
            _uow.Save();
        }

        public async Task<UserDTO> UpdateProfile(UserDTO user)
        {
            if (!ValidateToken(user.Token, out var exist))
                throw new ApplicationException("Invalid token.");

            if (exist == null)
                throw new ApplicationException("User doesn't exist.");

            if (exist.Id != user.Id)
                throw new ApplicationException("That's not your profile.");


            _uow.Users.CreateOrUpdate(user);
            _uow.Save();

            return await Task.FromResult(user);
        }

        public async Task<bool> IsUserExists(string email)
        {
            var found = await Task.FromResult(_uow.Users.Get(i => i.Email == email).FirstOrDefault());
            var res = found != null;
            return res;
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
