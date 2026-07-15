using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryCleaverChargedAttack : SecondaryDefaultChargedAttack
    {
        protected override void Prepare()
        {
            base.Prepare();
            if (charge == 1) damageType |= DamageType.BleedOnHit;
            damageCoefficient = Mathf.Lerp(RorschachStaticValues.secondaryCleaverChargeMinDamageCoefficient, RorschachStaticValues.secondaryCleaverChargeMaxDamageCoefficient, charge);
            hitEffectPrefab = RorschachAssets.meleeHitCleaverEffect;
        }
    }
}