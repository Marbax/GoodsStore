using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;

namespace GoodsStore.WebServer.Controllers.api
{
    /// <summary>
    /// Good's photos
    /// </summary>
    public class PhotoController : ApiController
    {
        private readonly IServicesUnitOfWork _uow;
        private static IList<string> _allowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        private static int _maxContentLength = 1024 * 1024 * 1; //Size = 1 MB

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        public PhotoController(IServicesUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Get photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int photoId)
        {
            var photo = await Task.FromResult(_uow.Photos.Get(photoId));

            byte[] ImageByte = photo.PhotoData;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            System.IO.MemoryStream oMemoryStream = new System.IO.MemoryStream(ImageByte);
            result.Content = new StreamContent(oMemoryStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(photo.MimeType);

            return result;
        }

        /// <summary>
        /// Post a list of images to the good
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post(int goodId)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var good = _uow.Goods.Get(goodId);
                var filesCount = httpRequest.Files.Count;
                var filesUploaded = 0;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!_allowedFileExtensions.Contains(extension))
                            throw new ApplicationException($"Please Upload image of type {_allowedFileExtensions.Aggregate((i, agg) => i + ',' + agg)}({postedFile.FileName}).");
                        else if (postedFile.ContentLength > _maxContentLength)
                            throw new ApplicationException($"Please Upload a file upto {_maxContentLength / 1024 / 1024} mb({postedFile.FileName}).");
                        else
                        {
                            var photo = new PhotoDTO
                            {
                                MimeType = postedFile.ContentType,
                                Title = postedFile.FileName
                            };
                            await postedFile.InputStream.ReadAsync(photo.PhotoData, 0, postedFile.ContentLength);

                            var addedPhoto = _uow.Photos.Add(photo);
                            var photos = new List<PhotoDTO>();
                            photos.Add(addedPhoto);
                            _uow.Goods.CreateOrUpdate(good);
                            ++filesUploaded;
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"{filesUploaded}/{filesCount} images uploaded successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
