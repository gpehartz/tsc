using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class TournamentsController
    {
        private TscApplication _application;

        public TournamentsController()
        {
            _application = new TscApplication();
        }

        [HttpGet]
        public IEnumerable<Tournament> Get()
        {
            return _application.GetAllTournaments();
        }

        [HttpPost]
        public void Post([FromBody]Tournament tournament)
        {
            _application.AddTournament(tournament);
        }
    }
}
