using System.Web.Mvc;

namespace GoodsStore.WebServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult About() => View();
        public ActionResult Contact() => View();
    }
}