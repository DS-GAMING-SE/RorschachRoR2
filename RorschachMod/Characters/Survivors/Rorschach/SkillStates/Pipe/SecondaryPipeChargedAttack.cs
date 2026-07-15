using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;
using R2API;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryPipeChargedAttack : SecondaryDefaultChargedAttack
    {
        protected override void Prepare()
        {
            base.Prepare();
            baseDuration = 0.9f;
            if (charge == 1)
            {
                damageType.AddModdedDamageType(HedgehogUtils.Launch.DamageTypes.launch);
                pushForce = 0f;
            }
            damageCoefficient = Mathf.Lerp(RorschachStaticValues.secondaryPipeChargeMinDamageCoefficient, RorschachStaticValues.secondaryPipeChargeMaxDamageCoefficient, charge);
            hitEffectPrefab = RorschachAssets.meleeHitPipeEffect;
            attackRecoil *= 2f;
            hitStopDuration *= 1.7f;
        }

        protected override void FireAttack()
        {
            if (base.isAuthority && charge == 1)
            {
                attack.forceVector = base.inputBank.aimDirection * 250f;
            }
            base.FireAttack();
        }
    }
}