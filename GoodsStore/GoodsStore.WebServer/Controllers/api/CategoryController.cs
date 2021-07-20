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
    //[Authorize(Roles = "superadmin,admin,manager")]
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        private readonly IServicesUnitOfWork _uow;

        public CategoryController(IServicesUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                //some how it get's items in different states after item edited , mb some problems with context again
                // thats happens on lowest level , with dbset (in generic repo)
                var res = await Task.FromResult(_uow.Categories.GetAll());

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
                var res = _uow.Categories.Get(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] CategoryDTO cat)
        {
            try
            {
                CategoryDTO added = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    added = _uow.Categories.Add(cat);// "add" method made save to return an item with real id

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
        public IHttpActionResult Update([FromBody] CategoryDTO cat)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    _uow.Categories.CreateOrUpdate(cat);
                    _uow.Save();

                    trans.Complete();
                }
                CategoryDTO updated = _uow.Categories.Get(cat.Id);
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
                CategoryDTO deleted = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    deleted = _uow.Categories.Delete(id);
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
