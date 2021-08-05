using System.Web.Http;

namespace GoodsStore.WebServer.Controllers.api
{
    /// <summary>
    /// Default controller
    /// </summary>
    public class DefaultController : ApiController
    {
        /// <summary>
        /// Root action
        /// </summary>
        /// <response code="200">Redirects to swagger page</response>
        /// <returns>Redirect to swagger page</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Index()
        {
            string port = "";
            if (Request.RequestUri.Port != 443 || Request.RequestUri.Port != 80)
                port = ":" + Request.RequestUri.Port.ToString();
            var url = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}{port}/swagger";
            return Redirect(url);
        }
    }
}
