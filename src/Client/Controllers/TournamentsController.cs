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
        public IActionResult Post()
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            Request.Form.Files[0].SaveAsAsync(Path.Combine(@"c:\Work\tsc\src\Client\wwwroot\images\", fileName));

            var tournament = JsonConvert.DeserializeObject<Tournament>(Request.Form["tournament"]);
            tournament.LogoUrl = Path.Combine(@"http://localhost:8000/images/", fileName);

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
