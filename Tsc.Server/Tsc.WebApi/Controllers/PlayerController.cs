using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tsc.Application;
using Tsc.WebApi.Mapping;
using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly ITscApplication _application;
        private readonly ITranslator _translator;

        public PlayersController(ITscApplication application, ITranslator translator)
        {
            _application = application;
            _translator = translator;
        }

        [HttpGet]
        public IEnumerable<Player> Get()
        {
            var items = _application.GetAllPlayers();
            var serviceItems = items.Select(_translator.TranslateToService).ToList();
            return serviceItems;
        }
        
        [HttpGet]
        [Route("{pageIndex:int}/{pageSize:int}")]
        public PagedResponse<Player> Get(int pageIndex, int pageSize)
        {
            var domainPlayers = _application.GetAllPlayers();
            var servicePlayers = domainPlayers.Select(_translator.TranslateToService).ToList();
            return new PagedResponse<Player>(servicePlayers, pageIndex, pageSize);
        }
    }
}
