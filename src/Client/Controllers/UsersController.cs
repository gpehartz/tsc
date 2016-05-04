using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class UsersController
    {
        private TscApplication _application;

        public UsersController()
        {
            _application = new TscApplication();
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _application.GetAllUsers();
        }
    }
}
