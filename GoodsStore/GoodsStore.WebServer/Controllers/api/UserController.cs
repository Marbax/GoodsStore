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
using Newtonsoft.Json.Linq;

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
        public async Task<IHttpActionResult> Login([FromBody] JObject authData)
        {
            string username = authData.Value<string>("username");
            string password = authData.Value<string>("password");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest("Some login data is absent.");

            UserDTO foundUser = null;

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    foundUser = await _authManager.Authenticate(username, password);
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
        public async Task<IHttpActionResult> Register([FromBody] JObject authData)
        {
            string username = authData.Value<string>("username");
            string password = authData.Value<string>("password");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest("Some login data is absent.");

            if (await _authManager.IsUserExists(username))
                return BadRequest("This user already exists.");

            UserDTO newUser = null;

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    newUser = await _authManager.Register(new UserDTO() { Email = username, Password = password });

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
