using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Tsc.Application;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class TournamentsController : Controller
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

        [HttpGet("{id}", Name = "GetTournament")]
        public IActionResult Get(Guid id)
        {
            var tournament = _application.GetTournament(id);
            if (tournament == null)
            {
                return HttpNotFound(id);
            }

            return Ok(tournament);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Tournament tournament)
        {
            if (tournament == null)
            {
                return HttpBadRequest();
            }

            var newTournament = _application.AddTournament(tournament);
            return CreatedAtRoute("GetTournament", new { id = newTournament.Id }, newTournament);
        }

        [HttpPut("{tournamentId}/fixtures/{fixtureId}")]
        public IActionResult SetFixtureResult(Guid tournamentId, Guid fixtureId, [FromBody] Fixture fixture)
        {
            // it should handle NotFound cases!
            _application.SetFixtureResult(tournamentId, fixtureId, fixture.Results);
            return new NoContentResult();
        }

        /*
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // put delete logic here
            return new NoContentResult();
        }
        */
    }
}
