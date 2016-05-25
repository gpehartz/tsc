using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Post()
        {
            const int defaultBufferSize = 81920;

            var formFile = Request.Form.Files[0];
            if (formFile == null)
            {
                return BadRequest();
            }

            var fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            fileName = Path.Combine(@"c:\Work\tsc\src\Client\wwwroot\images\", fileName);

            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            try
            {
                await formFile.OpenReadStream().CopyToAsync(fileStream, defaultBufferSize);
            }
            finally
            {
                fileStream.Dispose();
            }

            var serviceTournament = JsonConvert.DeserializeObject<Tournament>(Request.Form["tournament"]);
            serviceTournament.LogoUrl = Path.Combine(@"http://localhost:8000/images/", fileName);

            var tournament = _translator.TranslateToDomain(serviceTournament);
            var newTournament = _application.AddTournament(tournament);
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
