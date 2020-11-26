using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public IHttpActionResult Get()
        {
            try
            {
                var res = _uow.Categories.GetAll();

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
                    added = _uow.Categories.Add(cat);// "add" method made save

                    trans.Complete();
                }
                return Ok(added);
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
                CategoryDTO updated = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    _uow.Categories.CreateOrUpdate(cat);
                    _uow.Save();
                    updated = _uow.Categories.Get(cat.Id);

                    trans.Complete();
                }
                return Ok(updated);
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
