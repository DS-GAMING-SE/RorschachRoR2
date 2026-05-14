using System;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachStaticValues
    {
        #region Passive

        public const float passiveEliteDropChance = 25f;

        public const float passiveCleaverBleedChance = 30f;

        #endregion

        #region Primary

        public const float primaryDamageCoefficient = 2.6f;

        #endregion

        #region Secondary

        public const float secondaryDashDamageCoefficient = 3.5f;

        public const float secondaryChargeMinDamageCoefficient = 6f;
        public const float secondaryChargeMaxDamageCoefficient = 10f;

        public const float secondaryChargeDuration = 1f;

        public const int judgementBuffCap = 4;

        #endregion

        #region Utility

        public const float utilityDamageCoefficient = 3.2f;

        #endregion

        #region Special

        public const float specialOnKillBuffMultiplier = 0.25f;

        public const float specialFlameCanDamageCoefficient = 20f;
        public const float specialFlameCanJudgementDamageMultiplier = 0.5f / 4f;

        #endregion

        public const float gunDamageCoefficient = 4.2f;

        public const float bombDamageCoefficient = 16f;
    }
}