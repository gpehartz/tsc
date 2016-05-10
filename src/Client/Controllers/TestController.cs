using Microsoft.AspNet.Mvc;

namespace Client.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public string About()
        {
            return "Szevasz!";
        }
    }
}
