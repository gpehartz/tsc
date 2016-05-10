using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController
    {
        private readonly ITscApplication _application;

        public TeamsController(ITscApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return _application.GetAllTeams();
        }

        [HttpPost]
        public void Post([FromBody]Team team)
        {
            _application.AddTeam(team);
        }
    }
}
