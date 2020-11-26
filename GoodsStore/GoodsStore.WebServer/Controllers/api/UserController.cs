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
        //[System.Web.Http.Route("login")]
        public async Task<IHttpActionResult> Login(UserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserDTO foundUser = await _authManager.Authenticate(user.Email, user.Password);
            //AUTH
            if (foundUser == null)
                return BadRequest("No such User.");

            return Ok(foundUser);
        }

        [AllowAnonymous]
        [HttpPost()]
        //[System.Web.Http.Route("register")]
        public async Task<IHttpActionResult> Register(UserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ModelState.AddModelError("Email", "This user already exists.");

            UserDTO newUser = null;
            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                newUser = await _authManager.Register(user);

                trans.Complete();
            }

            if (newUser == null)
                return BadRequest("No such User.");

            return Ok(newUser);
        }


    }
}
