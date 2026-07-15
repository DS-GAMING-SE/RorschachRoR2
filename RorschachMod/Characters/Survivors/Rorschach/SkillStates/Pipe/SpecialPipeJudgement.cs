using EntityStates;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Modules.BaseStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using System;
using RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialPipeJudgement : SpecialDefaultJudgement
    {
        public override Type finalStateType { get { return typeof(SpecialPipeFinal); } }
        public override Type judgementStateType { get { return typeof(SpecialPipeJudgement); } }
        protected override void Prepare()
        {
            base.Prepare();
            damageCoefficient = RorschachStaticValues.specialPipeJudgementDamageCoefficient;
            attackRecoil *= 2f;
            hitStopDuration *= 1.7f;
        }
    }
}