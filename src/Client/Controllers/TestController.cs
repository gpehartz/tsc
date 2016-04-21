using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Client.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public string About()
        {
            return "Szevasz";
        }
    }
}
