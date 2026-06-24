using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialDefaultDash : BaseState
    {
        private bool hit;
        public virtual Type finalStateType { get { return typeof(SpecialDefaultFinal); } }
        public virtual Type judgementStateType { get { return typeof(SpecialDefaultJudgement); } }
        protected float baseDuration = 0.7f;
        protected float duration;
        protected float movementFadeStartPercentTime = 0.25f;
        protected float movementFadeEndPercentTime = 0.75f;
        protected bool target = true;

        protected virtual void Prepare()
        {
            baseDuration = 0.6f;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            Prepare();
            duration = baseDuration / attackSpeedStat;
            if (NetworkServer.active)
            {
                characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
        }

        protected virtual void PlayAttackAnimation()
        {
            PlayCrossfade("Gesture, Override", "Slash2", "Slash.playbackRate", duration, 0.1f * duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority && fixedAge > duration && target)
            {
                if (characterBody.HasBuff(RorschachBuffs.judgementBuff))
                {
                    SpecialDefaultJudgement judgementState = (SpecialDefaultJudgement)EntityStateCatalog.InstantiateState(judgementStateType);
                    judgementState.judgementStacks = characterBody.GetBuffCount(RorschachBuffs.judgementBuff);
                    this.outer.SetNextState(judgementState);
                    return;
                }
                SpecialDefaultFinal finalState = (SpecialDefaultFinal)EntityStateCatalog.InstantiateState(finalStateType);
                finalState.judgementStacks = 0;
                this.outer.SetNextState(EntityStateCatalog.InstantiateState(finalStateType));
                return;
            }
        }

        public override void Update()
        {
            base.Update();
            if (base.isAuthority)
            {
                SecondaryDefaultDash.UpdateDisplacement(inputBank, characterMotor, age, duration, movementFadeStartPercentTime, movementFadeEndPercentTime, characterBody.moveSpeed * 1.5f);
            }
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            base.OnExit();
        }
    }
}
