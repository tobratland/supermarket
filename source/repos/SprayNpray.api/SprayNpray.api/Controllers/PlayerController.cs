using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SprayNpray.api.Contracts;
using SprayNpray.api.Entities.ExtendedModels;
using SprayNpray.api.Entities.Extensions;
using SprayNpray.api.Entities.Models;

namespace SprayNpray.api.Controllers
{
 
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public PlayerController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        
        [HttpGet("{id}", Name = "PlayerById")]
        public IActionResult GetPlayerById(Guid id)
        {
            try
            {
                var player = _repository.Player.GetPlayerById(id);

                if (player.IsEmptyObject())
                {
                    _logger.LogError($"Player with id: {id}, has NOT been found in db.");
                    return NotFound();
                }
                else
                {

                    _logger.LogInfo($"Returned player with id: {id}");
                    player.Password = null;
                    return Ok(player);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPlayerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("{id}/scores")]
        public IActionResult GetPlayerWithDetails(Guid id)
        {
            try
            {
                var player = _repository.Player.GetPlayerWithDetails(id);

                if (player.IsEmptyObject())
                {
                    _logger.LogError($"Player with id: {id}, has NOT been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned player with details for id: {id}");
                    player.Password = null;
                    return Ok(player);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPlayerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticatePlayer([FromBody]Player playerDetails)
        {
            try
            {
                var player = _repository.Player.AuthenticatePlayer(playerDetails);
                if (player.IsEmptyObject())
                {
                    _logger.LogError($"Wrong username or password");
                    return NotFound("Wrong username or password");
                }
                return Ok(player);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside AuthenticatePlayer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        
        [HttpPost("createplayer", Name = "createplayer")]
        public IActionResult CreatePlayer([FromBody]Player player)
        {
            try
            {
                if (player.IsObjectNull())
                {
                    _logger.LogError("Player object sent from client is null.");
                    return BadRequest("Player object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid player object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Player.CreatePlayer(player);
                _repository.Save();
                var DetailedPlayer = _repository.Player.GetPlayerWithDetails(player.Id);
                DetailedPlayer.Password = null;
                return CreatedAtRoute("Createplayer", DetailedPlayer);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside Create action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(Guid id, [FromBody]Player player)
        {
            try
            {
                if (player.IsObjectNull())
                {
                    _logger.LogError("Player object sent from client is null.");
                    return BadRequest("Player object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid player object sent from client.");
                    BadRequest("Invalid model object");
                }

                var dbPlayer = _repository.Player.GetPlayerById(id);
                if (dbPlayer.IsEmptyObject())
                {
                    _logger.LogError($"Player with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Player.UpdatePlayer(dbPlayer, player);
                _repository.Save();

                dbPlayer.Password = null;
                return Ok(dbPlayer);

            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePlayer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(Guid id)
        {
            try
            {
                var player = _repository.Player.GetPlayerById(id);
                if (player.IsEmptyObject())
                {
                    _logger.LogError($"Player with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Score.ScoresByPlayer(id).Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related Scores. Delete those Scores first");
                    return BadRequest("Cannot delete player. It has scores. Delete scores first MUST ADD AUTO");
                }

                _repository.Player.DeletePlayer(player);
                _repository.Save();
                return Ok("player deleted");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePlayer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
    }

}