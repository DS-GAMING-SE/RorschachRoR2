using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryPipeDash : SecondaryDefaultDash
    {
        public override Type chargeStateType => typeof(SecondaryPipeCharge);
        protected override void Prepare()
        {
            base.Prepare();
            damageType |= DamageType.Stun1s;
            damageCoefficient = RorschachStaticValues.secondaryDashDamageCoefficient;
        }
    }
}