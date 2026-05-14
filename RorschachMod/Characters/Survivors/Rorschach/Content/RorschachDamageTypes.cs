using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachDamageTypes
    {
        public static DamageAPI.ModdedDamageType specialOnKillBuff;
        public static DamageAPI.ModdedDamageType judgementStackBit0;
        public static DamageAPI.ModdedDamageType judgementStackBit1;
        public static DamageAPI.ModdedDamageType judgementStackBit2;
        public static void Initialize()
        {
            specialOnKillBuff = DamageAPI.ReserveDamageType();
            judgementStackBit0 = DamageAPI.ReserveDamageType();
            judgementStackBit1 = DamageAPI.ReserveDamageType();
            judgementStackBit2 = DamageAPI.ReserveDamageType();
        }
        public static void AddJudgementStacks(this ref DamageTypeCombo damageTypeCombo, int judgementStacks)
        {
            JudgementStackBits bits = (JudgementStackBits)judgementStacks;
            if ((bits & JudgementStackBits.Bit0) != JudgementStackBits.None) damageTypeCombo.AddModdedDamageType(judgementStackBit0);
            if ((bits & JudgementStackBits.Bit1) != JudgementStackBits.None) damageTypeCombo.AddModdedDamageType(judgementStackBit1);
            if ((bits & JudgementStackBits.Bit2) != JudgementStackBits.None) damageTypeCombo.AddModdedDamageType(judgementStackBit2);
        }
        public static int ReadJudgementStacks(this ref DamageTypeCombo damageTypeCombo)
        {
            int judgementStacks = 0;
            if (damageTypeCombo.HasModdedDamageType(judgementStackBit0)) judgementStacks += (int)JudgementStackBits.Bit0;
            if (damageTypeCombo.HasModdedDamageType(judgementStackBit1)) judgementStacks += (int)JudgementStackBits.Bit1;
            if (damageTypeCombo.HasModdedDamageType(judgementStackBit2)) judgementStacks += (int)JudgementStackBits.Bit2;
            return judgementStacks;
        }
        [Flags]
        private enum JudgementStackBits
        {
            None = 0,
            Bit0 = 1,
            Bit1 = 2,
            Bit2 = 4
        }
    }
}
