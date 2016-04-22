using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Domain;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController
    {
        private TscApplication _application;

        public TeamsController()
        {
            _application = new TscApplication();
        }

        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return _application.GetAllTeams();
        }
    }
}
