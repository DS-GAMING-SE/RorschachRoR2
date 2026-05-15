using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class PrimaryCleaver : PrimaryDefault
    {
        protected override void Prepare()
        {
            base.Prepare();
            if (Util.CheckRoll0To1(RorschachStaticValues.primaryCleaverBleedChance, characterBody.master))
            {
                damageType |= DamageType.BleedOnHit;
            }
            damageCoefficient = RorschachStaticValues.primaryCleaverDamageCoefficient;
            baseDuration = 0.84f;
        }
    }
}