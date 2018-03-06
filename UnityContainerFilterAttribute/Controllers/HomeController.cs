using System.Web.Mvc;
using UnityContainerFilterAttribute.Infrastructure;
using UnityContainerFilterAttribute.ViewModels;

namespace UnityContainerFilterAttribute.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        [BaseLayout]
     
        public ActionResult Index()
        {


            return View(new HomeViewModel());
        }
    }
}