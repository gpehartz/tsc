using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class TournamentsController
    {
        private readonly ITscApplication _application;

        public TournamentsController(ITscApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public IEnumerable<Tournament> Get()
        {
            return _application.GetAllTournaments();
        }

        [HttpGet("{id}")]
        public Tournament Get(Guid id)
        {
            return _application.GetTournament(id);
        }

        [HttpPost]
        public void Post([FromBody]Tournament tournament)
        {
            _application.AddTournament(tournament);
        }

        [HttpPut("{tournamentId}/fixtures/{fixtureId}")]
        public void SetFixtureResult(Guid tournamentId, Guid fixtureId, [FromBody] Fixture fixture)
        {
            _application.SetFixtureResult(tournamentId, fixtureId, fixture.Results);
        }
    }
}
