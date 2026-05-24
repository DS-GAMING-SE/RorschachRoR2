using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryCleaverDash : SecondaryDefaultDash
    {
        public override Type chargeStateType => typeof(SecondaryCleaverCharge);
        protected override void Prepare()
        {
            base.Prepare();
            damageType |= DamageType.BleedOnHit;
            damageCoefficient = RorschachStaticValues.secondaryDashDamageCoefficient;
        }
    }
}