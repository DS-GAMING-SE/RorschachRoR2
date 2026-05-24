using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryCleaverCharge : SecondaryDefaultCharge
    {
        public override Type attackStateType => typeof(SecondaryCleaverChargedAttack);
    }
}