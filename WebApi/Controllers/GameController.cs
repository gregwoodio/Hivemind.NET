using Hivemind.Contracts;
using Hivemind.Enums;
using Hivemind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            if (gameService == null)
            {
                throw new ArgumentNullException(nameof(gameService));
            }
            _gameService = gameService;
        }

        [HttpPost]
        [Route("pre")]
        public PreGameReport ProcessPreGame([FromBody] int id)
        {
            return _gameService.ProcessPreGame(id);
        }

        [HttpPost]
        [Route("post")]
        public PostGameReport ProcessPostGame(BattleReport battleReport)
        {
            return _gameService.ProcessPostGame(battleReport);
        }
    }
}
