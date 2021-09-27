using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;

namespace GoodsStore.WebServer.Controllers.api
{
    /// <summary>
    /// 
    /// </summary>
    public class ManufacturerController : ApiController
    {

        private readonly IServicesUnitOfWork _uow;

        /// <summary>
        /// 
        /// </summary>
        public ManufacturerController(IServicesUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// GET: api/Manufacturer
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await Task.FromResult(_uow.Manufacturers.GetAll());

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// GET: api/Manufacturer/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var res = await Task.FromResult(_uow.Manufacturers.Get(id));

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// POST: api/Manufacturer
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        public IHttpActionResult Create([FromBody] ManufacturerDTO dto)
        {
            try
            {
                ManufacturerDTO added = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    added = _uow.Manufacturers.Add(dto);// "add" method made save to return an item with real id

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
        /// Update info
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update([FromBody] ManufacturerDTO dto)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    _uow.Manufacturers.CreateOrUpdate(dto);
                    _uow.Save();

                    trans.Complete();
                }
                ManufacturerDTO updated = _uow.Manufacturers.Get(dto.Id);
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
        /// DELETE: api/Manufacturer/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ManufacturerDTO deleted = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    deleted = _uow.Manufacturers.Delete(id);
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
