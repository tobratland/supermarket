using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Contracts
{
    public interface IScoreRepository
    {
        IEnumerable<Score> ScoresByPlayer(Guid playerId);
        IEnumerable<Score> GetAllScores();
        Score GetScoreById(Guid accountId);
        Score CreateScore(Score score);
        Score UpdateScore(Score dbScore, Score score);
        void DeleteScore(Score score);
    }
}
