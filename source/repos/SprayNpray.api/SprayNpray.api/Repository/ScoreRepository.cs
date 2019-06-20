using SprayNpray.api.Contracts;
using SprayNpray.api.Entities;
using SprayNpray.api.Entities.Extensions;
using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Repository
{
    public class ScoreRepository : RepositoryBase<Score>, IScoreRepository
    {
        public ScoreRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Score> ScoresByPlayer(Guid playerId)
        {
            return FindByCondition(a => a.Player_PlayerId.Equals(playerId)).ToList();
        }
        public IEnumerable<Score> GetAllScores()
        {
            return FindAll()
                    .OrderBy(score => score.Weapon)
                    .ToList();
        }
        public Score GetScoreById(Guid scoreId)
        {
            return FindByCondition(score => score.Id.Equals(scoreId))
                    .DefaultIfEmpty(new Score())
                    .FirstOrDefault();
        }

        public Score CreateScore(Score score)
        {
            score.Id = Guid.NewGuid();
            Create(score);
            return (score);
        }
        public Score UpdateScore(Score dbScore, Score score)
        {
            dbScore.Map(score);
            Update(dbScore);
            return (score);
        }
        public void DeleteScore(Score score)
        {
            Delete(score);
        }
    }
}
