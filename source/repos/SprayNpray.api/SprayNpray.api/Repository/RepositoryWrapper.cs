using Microsoft.Extensions.Options;
using SprayNpray.api.Contracts;
using SprayNpray.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private IPlayerRepository _player;
        private IScoreRepository _score;
        
        public IPlayerRepository Player
        {
            
            get
            {
                if(_player == null)
                {
                    _player = new PlayerRepository(_repoContext);
                }
                return _player;
            }
        }

        public IScoreRepository Score
        {
            get
            {
                if(_score == null)
                {
                    _score = new ScoreRepository(_repoContext);
                }
                return _score;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
