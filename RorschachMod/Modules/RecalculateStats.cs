using R2API;
using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RorschachMod;
using RorschachMod.Characters.Survivors.Rorschach;

namespace RorschachMod.Modules
{
    internal class RecalculateStats
    {
        public static void Initialize()
        {
            RecalculateStatsAPI.GetStatCoefficients += RorschachRecalculateStats;
        }
        private static void RorschachRecalculateStats(CharacterBody self, RecalculateStatsAPI.StatHookEventArgs stats)
        {
            if (self)
            {
                if (self.HasBuff(RorschachBuffs.boostBuff))
                {
                    HedgehogUtils.Boost.BoostLogic.BoostStats(self, stats, RorschachStaticValues.boostListedSpeedCoefficient);
                    stats.armorAdd += RorschachStaticValues.boostArmor;
                }
            }
        }
    }
}
