﻿using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace GoodsStore.WebServer.Controllers.api
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodController : ApiController
    {
        private readonly IServicesUnitOfWork _uow;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        public GoodController(IServicesUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await Task.FromResult(_uow.Goods.GetAll());

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var res = await Task.FromResult(_uow.Goods.Get(id));

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Create new
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] GoodDTO dto)
        {
            try
            {
                GoodDTO added = null;
                PhotoDTO photoAdded = null;
                // tmp
                HttpPostedFileBase image = null;
                if (image != null)
                {
                    photoAdded = new PhotoDTO();
                    photoAdded.MimeType = image.ContentType;
                    photoAdded.PhotoData = new byte[image.ContentLength];
                    image.InputStream.Read(photoAdded.PhotoData, 0, image.ContentLength);
                    photoAdded = _uow.Photos.Add(photoAdded);
                    List<PhotoDTO> photos = dto.Photos.ToList();
                    photos.Add(photoAdded);
                    dto.Photos = photos;
                }
                //

                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {//TODO: on insert good in tryes to insert manufacturer to
                    added = await Task.FromResult(_uow.Goods.Add(dto));// "add" method made save to return an item with real id

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
                string exMsg = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    exMsg = ex.Message;
                }
                return BadRequest(exMsg);
            }
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody] GoodDTO dto)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    await Task.Factory.StartNew(() => _uow.Goods.CreateOrUpdate(dto));
                    _uow.Save();

                    trans.Complete();
                }
                var updated = _uow.Goods.Get(dto.Id);
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
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                GoodDTO deleted = null;
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    deleted = await Task.FromResult(_uow.Goods.Delete(id));
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
