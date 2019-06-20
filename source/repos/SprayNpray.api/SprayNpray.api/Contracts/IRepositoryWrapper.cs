using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Contracts
{
    public interface IRepositoryWrapper
    {
        IPlayerRepository Player { get; }
        IScoreRepository Score { get; }
        void Save();
    }
}
