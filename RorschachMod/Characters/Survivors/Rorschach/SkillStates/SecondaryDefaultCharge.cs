using EntityStates;
using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryDefaultCharge : BaseState
    {
        public virtual Type attackStateType { get { return typeof(SecondaryDefaultChargedAttack); } }
        public float charge;
        public float minDurationPercent = 0.2f;
        public float baseMaxDuration = RorschachStaticValues.secondaryChargeDuration;
        public float maxDuration;

        public virtual void Prepare()
        {

        }
        public override void OnEnter()
        {
            Prepare();
            base.OnEnter();
            characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
            maxDuration = baseMaxDuration / attackSpeedStat;
            characterBody.SetAimTimer(maxDuration);
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

        protected void SetNextState()
        {
            SecondaryDefaultChargedAttack chargedAttack = (SecondaryDefaultChargedAttack)EntityStateCatalog.InstantiateState(attackStateType);
            chargedAttack.charge = charge;
            this.outer.SetNextState(chargedAttack);
        }

        public override void OnExit()
        {
            characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            base.OnExit();
        }
    }
}
