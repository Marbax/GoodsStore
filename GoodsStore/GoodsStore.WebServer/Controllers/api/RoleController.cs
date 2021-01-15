using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GoodsStore.WebServer.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private readonly IServicesUnitOfWork _uow;

        public RoleController(IServicesUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await Task.FromResult(_uow.Roles.GetAll());

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
                var res = _uow.Roles.Get(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] RoleDTO item)
        {
            try
            {
                RoleDTO added = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    added = _uow.Roles.Add(item);// "add" method made save

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
        public IHttpActionResult Update([FromBody] RoleDTO item)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    _uow.Roles.CreateOrUpdate(item);
                    _uow.Save();

                    trans.Complete();
                }

                var updated = _uow.Roles.Get(item.Id);
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
                RoleDTO deleted = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    deleted = _uow.Roles.Delete(id);
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


    }
}