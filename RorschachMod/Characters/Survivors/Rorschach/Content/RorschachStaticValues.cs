using System;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachStaticValues
    {
        #region Passive

        public const float passiveMaxProgress = 100f;
        public const float passiveStartProgress = 40f;
        public const float passiveProgressOnKill = 3f;
        public const float passiveProgressEliteMultiplier = 2f;
        public const float passiveProgressChampionMultiplier = 5f;
        public const float passiveProgressBossMultiplier = 3f;

        #endregion

        #region Primary

        public const float primaryDefaultDamageCoefficient = 2.6f;

        public const float primaryPipeDamageCoefficient = primaryDefaultDamageCoefficient * 2f;

        public const float primaryCleaverDamageCoefficient = 2.4f;
        public const float primaryCleaverBleedChance = 30f;

        public const float primaryFlameCanDamagePerSecond = 6f;
        public const float primaryFlameCanAttacksPerSecond = 3.5f;
        public const float primaryFlameCanDamageCoefficient = primaryFlameCanDamagePerSecond / primaryFlameCanAttacksPerSecond;
        public const float primaryFlameCanProcCoefficient = 0.5f;
        public const float primaryFlameCanRange = 30f;

        #endregion

        #region Secondary

        public const float secondaryDashDamageCoefficient = 3.2f;

        public const float secondaryChargeMinDamageCoefficient = 6f;
        public const float secondaryChargeMaxDamageCoefficient = 10f;

        public const float secondaryChargeDuration = 0.65f;

        public const int judgementBuffCap = 4;

        public const float secondaryPipeChargeMinDamageCoefficient = secondaryChargeMinDamageCoefficient * 2f;
        public const float secondaryPipeChargeMaxDamageCoefficient = secondaryChargeMaxDamageCoefficient * 2f;

        public const float secondaryCleaverChargeMinDamageCoefficient = secondaryChargeMinDamageCoefficient * 0.9f;
        public const float secondaryCleaverChargeMaxDamageCoefficient = secondaryChargeMaxDamageCoefficient * 0.9f;

        #endregion

        #region Utility

        public const float utilityGrappleDamageCoefficient = 3.2f;
        public const float utilityKneeDamageCoefficient = 6f;

        #endregion

        #region Special

        public const float specialJudgementDamageCoefficient = 3.6f;
        public const float specialFinalDamageCoefficient = 16f;

        public const float specialCleaverJudgementDamageCoefficient = specialJudgementDamageCoefficient * 0.9f;
        public const float specialCleaverFinalDamageCoefficient = specialFinalDamageCoefficient * 0.9f;
        public const int specialCleaverFinalBleedStacks = 5;

        public const float specialPipeJudgementDamageCoefficient = specialJudgementDamageCoefficient * 2.5f;
        public const float specialPipeFinalDamageCoefficient = specialFinalDamageCoefficient * 3f;

        public const float specialOnKillBuffMultiplier = 0.25f;

        public const float specialFlameCanDamageCoefficient = 25f;
        public const float specialFlameCanMaxJudgementDamageMultiplier = 0.8f;
        public const float specialFlameCanJudgementDamageMultiplier = specialFlameCanMaxJudgementDamageMultiplier / judgementBuffCap;
        public const float specialFlameCanMinExplosionRadius = 10f;
        public const float specialFlameCanMaxExplosionRadius = 20f;
        public const float specialFlameCanJudgementExplosionRadiusMultiplier = ((specialFlameCanMaxExplosionRadius / specialFlameCanMinExplosionRadius) - 1) / judgementBuffCap;
        public const int specialFlameCanFinalBurnStacks = 5;

        #endregion
    }
}