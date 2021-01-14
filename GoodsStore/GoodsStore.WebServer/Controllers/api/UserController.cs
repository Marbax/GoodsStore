using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GoodsStore.WebServer.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly IAuthManager _authManager;
        private readonly IServicesUnitOfWork _uow;

        public UserController(IAuthManager authManager, IServicesUnitOfWork uow)
        {
            _authManager = authManager;
            _uow = uow;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await Task.FromResult(_uow.Users.GetAll());

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var res = _uow.Users.Get(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] UserDTO user)
        {
            try
            {
                UserDTO added = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    added = _uow.Users.Add(user);// "add" method made save

                    trans.Complete();
                }
                return Ok(added);
            }
            catch (DbEntityValidationException ex)
            {
                string exMsg = "";

                foreach (var eve in ex.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        exMsg += $"{ve.ErrorMessage} \n";

                return BadRequest(exMsg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] UserDTO user)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    _uow.Users.CreateOrUpdate(user);
                    _uow.Save();

                    trans.Complete();
                }

                var updated = _uow.Users.Get(user.Id);
                return Ok(updated);
            }
            catch (DbEntityValidationException ex)
            {
                string exMsg = "";

                foreach (var eve in ex.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        exMsg += $"{ve.ErrorMessage} \n";

                return BadRequest(exMsg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UserDTO deleted = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    deleted = _uow.Users.Delete(id);
                    _uow.Save();

                    trans.Complete();
                }
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            catch (Exception)
            {
                return BadRequest("No such User or incorrect login data.");
            }

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
            catch (DbEntityValidationException ex)
            {
                string exMsg = "";

                foreach (var eve in ex.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        exMsg += $"{ve.ErrorMessage} \n";

                return BadRequest(exMsg);
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
