using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
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
        public PreGameReport ProcessPreGame([FromForm] Gang gang)
        {
            return _gameService.ProcessPreGame(gang.GangId);
        }

        [Authorize]
        [HttpPost]
        [Route("post")]
        public PostGameReport ProcessPostGame([FromForm] BattleReport battleReport)
        {
            return _gameService.ProcessPostGame(battleReport);
        }

        [Authorize]
        [HttpPost]
        [Route("post/skills")]
        public IEnumerable<GangerSkill> LearnSkills([FromForm] GangSkillUpRequest skillUpRequest)
        {
            if (skillUpRequest == null)
            {
                return null;
            }

            return _gameService.SkillUpGangers(skillUpRequest);
        }
    }
}
