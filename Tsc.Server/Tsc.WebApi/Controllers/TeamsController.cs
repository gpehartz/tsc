using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsc.Application;
using Tsc.WebApi.Mapping;
using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITscApplication _application;
        private readonly ITranslator _translator;

        public TeamsController(ITscApplication application, ITranslator translator)
        {
            _application = application;
            _translator = translator;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Team> Get()
        {
            var items = _application.GetAllTeams();
            var serviceItems = items.Select(_translator.TranslateToService).ToList();
            return serviceItems;
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public IActionResult Get(Guid id)
        {
            var team = _application.GetTeam(id);
            if (team == null)
            {
                return NotFound(id);
            }

            var serviceTeam = _translator.TranslateToService(team);
            return Ok(serviceTeam);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Team team)
        {
            if (team == null)
            {
                return BadRequest();
            }

            var domainTeam = _translator.TranslateToDomainNew(team);
            var newTeam = _application.AddTeam(domainTeam);
            return CreatedAtRoute("GetTeam", new { id = newTeam.Id }, newTeam);
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
