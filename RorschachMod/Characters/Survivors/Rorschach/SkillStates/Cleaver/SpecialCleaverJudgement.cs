using EntityStates;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Modules.BaseStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialCleaverJudgement : SpecialDefaultJudgement
    {
        public override Type finalStateType { get { return typeof(SpecialCleaverFinal); } }
        public override Type judgementStateType { get { return typeof(SpecialCleaverJudgement); } }
        protected override void Prepare()
        {
            base.Prepare();
            damageCoefficient = RorschachStaticValues.specialCleaverJudgementDamageCoefficient;
            damageType |= DamageType.BleedOnHit;
        }
    }
}