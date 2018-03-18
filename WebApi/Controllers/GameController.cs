using Hivemind.Contracts;
using Hivemind.Entities;
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

        [Authorize]
        [HttpPost]
        [Route("pre")]
        public PreGameReport ProcessPreGame([FromBody] Gang gang)
        {
            return _gameService.ProcessPreGame(gang.GangId);
        }

        [Authorize]
        [HttpPost]
        [Route("post")]
        public PostGameReport ProcessPostGame([FromBody] BattleReport battleReport)
        {
            return _gameService.ProcessPostGame(battleReport);
        }

        [Authorize]
        [HttpPost]
        [Route("post/skills")]
        public IEnumerable<GangerSkill> LearnSkills([FromBody] GangSkillUpRequest skillUpRequest)
        {
            if (skillUpRequest == null)
            {
                return null;
            }

            return _gameService.SkillUpGangers(skillUpRequest);
        }
    }
}
