using GoodsStore.Business.Models;
using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Security.Claims;

namespace GoodsStore.WebServer.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly IAuthManager _authManager;

        public UserController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("api/user/login")]
        public async Task<IHttpActionResult> Login([FromBody] UserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user?.Email) || string.IsNullOrEmpty(user?.Password))
                return BadRequest("Password and email were empy.");

            UserDTO foundUser = null;

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    foundUser = await _authManager.Authenticate(user.Email, user.Password);
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (foundUser == null)
                return BadRequest("No such User. Or incorrect login data.");

            return Ok(foundUser);
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("api/user/register")]
        public async Task<IHttpActionResult> Register([FromBody] UserDTO user)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            if (user == null || string.IsNullOrEmpty(user?.Email) || string.IsNullOrEmpty(user?.Password))
                return BadRequest("Password and email were empy.");

            if (await _authManager.IsUserExists(user))
                return BadRequest("This user already exists.");

            UserDTO newUser = null;

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    newUser = await _authManager.Register(user);

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (newUser == null)
                return BadRequest("No such User.");

            return Ok(newUser);
        }

        [HttpPut()]
        [Route("api/user/update")]
        public async Task<IHttpActionResult> UpdateProfile([FromBody] UserDTO user)
        {
            // can't grap token from simple http method
            var token = Request.Headers?.Authorization?.Parameter;
            if (token != null)
                user.Token = token;

            if (user == null || string.IsNullOrEmpty(user?.Token))
                return BadRequest("Token is absent.");

            UserDTO updated = null;

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    updated = await _authManager.UpdateProfile(user);

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (updated == null)
                return BadRequest("Sended data were broken.");

            return Ok(updated);
        }

    }
}
