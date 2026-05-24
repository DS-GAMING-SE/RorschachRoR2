using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryPipeChargedAttack : SecondaryDefaultChargedAttack
    {
        protected override void Prepare()
        {
            base.Prepare();
            baseDuration = 0.9f;
            if (charge == 1) damageType |= DamageType.Stun1s;
            damageCoefficient = Mathf.Lerp(RorschachStaticValues.secondaryPipeChargeMinDamageCoefficient, RorschachStaticValues.secondaryPipeChargeMaxDamageCoefficient, charge);
        }
    }
}