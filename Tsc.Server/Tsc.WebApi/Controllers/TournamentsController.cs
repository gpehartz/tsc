using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tsc.Application;
using Tsc.WebApi.Mapping;
using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TournamentsController : Controller
    {
        private readonly ITscApplication _application;
        private readonly ITranslator _translator;

        public TournamentsController(ITscApplication application, ITranslator translator)
        {
            _application = application;
            _translator = translator;
        }

        [HttpGet]
        public IEnumerable<Tournament> Get()
        {
            var items = _application.GetAllTournaments();
            var serviceItems = items.Select(_translator.TranslateToService).ToList();
            return serviceItems;
        }

        [HttpGet("{id}", Name = "GetTournament")]
        public IActionResult Get(Guid id)
        {
            var tournament = _application.GetTournament(id);
            if (tournament == null)
            {
                return NotFound(id);
            }

            var serviceTournament = _translator.TranslateToService(tournament);
            return Ok(serviceTournament);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Tournament tournament)
        {
            if (tournament == null)
            {
                return BadRequest();
            }

            var domainTournament = _translator.TranslateToDomain(tournament);
            var newTournament = _application.AddTournament(domainTournament);
            return CreatedAtRoute("GetTournament", new { id = newTournament.Id }, newTournament);
        }

        [HttpPut("{tournamentId}/fixtures/{fixtureId}")]
        public IActionResult SetFixtureResult(Guid tournamentId, Guid fixtureId, [FromBody] Fixture fixture)
        {
            var fixtureItems = fixture.Results.Select(_translator.TranslateToDomain).ToList();

            // it should handle NotFound cases!
            _application.SetFixtureResult(tournamentId, fixtureId, fixtureItems);
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
