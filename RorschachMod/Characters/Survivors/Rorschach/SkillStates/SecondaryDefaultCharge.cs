using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryDefaultCharge : BaseState
    {
        public virtual Type attackStateType { get { return typeof(SecondaryDefaultChargedAttack); } }
        public float charge;
        public float minDurationPercent = 0.2f;
        public float baseMaxDuration = RorschachStaticValues.secondaryChargeDuration;
        public float maxDuration;

        private EffectManagerHelper chargeEffect;

        public virtual void Prepare()
        {

        }
        public override void OnEnter()
        {
            Prepare();
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            characterBody.bodyFlags |= CharacterBody.BodyFlags.Unmovable;
            maxDuration = baseMaxDuration / attackSpeedStat;
            characterBody.SetAimTimer(maxDuration);
            PlayAttackAnimation();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority)
            {
                characterMotor.velocity.y = Mathf.Max(-1f, characterMotor.velocity.y);
            }
            if (fixedAge >= maxDuration * minDurationPercent)
            {
                charge = Mathf.Clamp01(charge + (Time.fixedDeltaTime / (maxDuration * (1 - minDurationPercent))));
                if (base.isAuthority)
                {
                    if (!base.inputBank.skill2.down || fixedAge > maxDuration)
                    {
                        SetNextState();
                        return;
                    }
                }
            }
        }
        protected virtual void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "SecondaryDefaultCharge", "Slash.playbackRate", maxDuration, 0.1f * maxDuration);
            chargeEffect = EffectManager.GetAndActivatePooledEffect(RorschachAssets.secondaryChargeEffect, FindModelChild("HandL"), true);
        }

        protected void SetNextState()
        {
            SecondaryDefaultChargedAttack chargedAttack = (SecondaryDefaultChargedAttack)EntityStateCatalog.InstantiateState(attackStateType);
            chargedAttack.charge = charge;
            this.outer.SetNextState(chargedAttack);
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            characterBody.bodyFlags &= ~CharacterBody.BodyFlags.Unmovable;
            chargeEffect.ReturnToPool();
            PlayAnimation("FullBody, Override", "BufferEmpty");
            base.OnExit();
        }
    }
}
