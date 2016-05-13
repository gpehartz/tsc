using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class UsersController
    {
        private ITscApplication _application;

        public UsersController(ITscApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _application.GetAllUsers();
        }
    }
}
