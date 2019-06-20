using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Entities.Extensions
{
    public static class ScoreExtensions
    {
        public static void Map(this Score dbScore, Score score)
        {
            dbScore.DateCreated = score.DateCreated;
            dbScore.TheScore = score.TheScore;
            dbScore.Time = score.Time;
            dbScore.Missrate = score.Missrate;
            dbScore.MagazineExtension = score.MagazineExtension;
            dbScore.BarrelExtension = score.BarrelExtension;
            dbScore.GoodReloads = score.GoodReloads;
            dbScore.BadReloads = score.BadReloads;
        }
    }
}
