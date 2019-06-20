using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SprayNpray.api.Contracts;
using SprayNpray.api.Entities.Extensions;
using SprayNpray.api.Entities.Models;

namespace SprayNpray.api.Controllers
{
    [Route("api/score")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;

        public ScoreController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTopScores()
        {
            try
            {
                string[] weapons = new string[] { "Spitfire", "Devotion with turbocharger", "Devotion without turbocharger", "R99", "Alternator", "R301", "Flatline", "Havoc with turbocharger", "Havoc without turbocharger", "Re45" };
                ArrayList scoreList = new ArrayList();
                
                for(int i = 0; i < weapons.Length; i++)
                {
                    ArrayList weapon = new ArrayList();
                    
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().Weapon);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().TheScore);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().Time);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().BadReloads);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().GoodReloads);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().BarrelExtension);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().MagazineExtension);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().DateCreated);
                    weapon.Add(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().Missrate);
                    weapon.Add(_repository.Player.GetPlayerById(_repository.Score.GetAllScores().Where(x => x.Weapon == weapons[i]).OrderByDescending(x => x.TheScore).First().Player_PlayerId).Nickname);

                    scoreList.Add(weapon);
                    
                }
                string TopScoreList = JsonConvert.SerializeObject(scoreList);
                return Ok(TopScoreList);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTopScores action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "ScoreById")]
        public IActionResult GetScoreById(Guid id)
        {
            try
            {
                var score = _repository.Score.GetScoreById(id);
                if (score.IsEmptyObject())
                {
                    _logger.LogError($"Score with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned scored with id: {id}");
                    return Ok(score);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetScoreById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateScore([FromBody] Score score)
        {
            try
            {
                if (score.IsObjectNull())
                {
                    _logger.LogError("Score object sent from client is null.");
                    return BadRequest("Score object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid score object sent from client.");
                    return BadRequest("Invalid object model");
                }

                _repository.Score.CreateScore(score);
                _repository.Save();

                return CreatedAtRoute("ScoreById", new { id = score.Id }, score);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateScore action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateScore(Guid id, [FromBody]Score score)
        {
            try
            {
                if (score.IsObjectNull())
                {
                    _logger.LogError("Score object sent from client is null.");
                    return BadRequest("Score object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid score object sent from client.");
                    return BadRequest("Score object is invalid");
                }

                var dbScore = _repository.Score.GetScoreById(id);
                if (dbScore.IsEmptyObject())
                {
                    var returnedScore = _repository.Score.CreateScore(score);
                    return Ok(returnedScore);
                }

                _repository.Score.UpdateScore(dbScore, score);
                _repository.Save();
                return Ok(dbScore);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateScore action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]///this should be converted to DeleteAllOfPlayersScores
        public IActionResult DeleteScore(Guid id)
        {
            try
            {
                var score = _repository.Score.GetScoreById(id);
                if (score.IsEmptyObject())
                {
                    _logger.LogError($"Score with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Score.DeleteScore(score);
                _repository.Save();

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteScore action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}