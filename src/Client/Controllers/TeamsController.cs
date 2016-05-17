using System;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Tsc.Application;
using Tsc.Application.ServiceModel;
using Microsoft.AspNet.Authorization;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITscApplication _application;

        public TeamsController(ITscApplication application)
        {
            _application = application;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Team> Get()
        {
            return _application.GetAllTeams();
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public IActionResult Get(Guid id)
        {
            var team = _application.GetTeam(id);
            if (team == null)
            {
                return HttpNotFound(id);
            }

            return Ok(team);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Team team)
        {
            if (team == null)
            {
                return HttpBadRequest();
            }

            var newTeam = _application.AddTeam(team);
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
