using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using R2API;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class PrimaryCleaver : PrimaryDefault
    {
        protected override void Prepare()
        {
            base.Prepare();
            damageType.AddModdedDamageType(RorschachDamageTypes.cleaverBleedChance);
            damageCoefficient = RorschachStaticValues.primaryCleaverDamageCoefficient;
            baseDuration = 0.84f;
            hitEffectPrefab = RorschachAssets.meleeHitCleaverEffect;
        }
    }
}