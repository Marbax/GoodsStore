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
    /// <summary>
    /// Category controller
    /// </summary>
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        private readonly IServicesUnitOfWork _uow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow"></param>
        public CategoryController(IServicesUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get Category by Category Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates new category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Title": "Car",
        ///        "Description": "Solid good",
        ///        "Goods": [
        ///             1,
        ///             2,
        ///             3
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="cat"></param>
        /// <returns>Ok on successfully created category</returns>
        /// <response code="201">Ok on successfully created category</response>
        /// <response code="400">Bad request on wrong model or errors during adding to DataBase</response>
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

        /// <summary>
        /// Updates information about category
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove Category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
