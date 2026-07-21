using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialDefaultGrab : BaseSkillState
    {
        public virtual Type finalStateType { get { return typeof(SpecialDefaultFinal); } }
        public virtual Type judgementStateType { get { return typeof(SpecialDefaultJudgement); } }
        public HurtBox target;
        public float baseMaxDuration = 0.4f;
        public float maxDuration;
        private Transform grabTransform;
        private const float pullStrength = 9f;
        private const float pullRadius = 5.5f;
        private const float pullWeakenRadius = 0.7f;

        public override void OnEnter()
        {
            base.OnEnter();
            maxDuration = baseMaxDuration / attackSpeedStat;
            grabTransform = FindModelChild("SpecialGrabTransform");
            GetModelAnimator().SetBool("judgement", characterBody.HasBuff(RorschachBuffs.judgementBuff));
            if (target && target.healthComponent)
            {
                if (NetworkServer.active && target.healthComponent.TryGetComponent<SetStateOnHurt>(out var stun))
                {
                    stun.SetStun(baseMaxDuration * 1.5f);
                }
            }
            PlayAttackAnimation();
        }
        protected virtual void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "SpecialDefaultGrab", "Slash.playbackRate", maxDuration * 1.4f, 0.1f * maxDuration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (grabTransform)
            {
                Grab(grabTransform.position, target);
            }
            if (fixedAge > maxDuration)
            {
                if (characterBody.HasBuff(RorschachBuffs.judgementBuff))
                {
                    SpecialDefaultJudgement judgementState = (SpecialDefaultJudgement)EntityStateCatalog.InstantiateState(judgementStateType);
                    judgementState.judgementStacks = characterBody.GetBuffCount(RorschachBuffs.judgementBuff);
                    judgementState.target = target;
                    this.outer.SetNextState(judgementState);
                    return;
                }
                SpecialDefaultFinal finalState = (SpecialDefaultFinal)EntityStateCatalog.InstantiateState(finalStateType);
                finalState.judgementStacks = 0;
                this.outer.SetNextState(finalState);
            }
        }
        public static void Grab(Vector3 grabPosition, HurtBox target)
        {
            if (target && target.healthComponent && target.healthComponent.body && target.healthComponent.body.hullClassification != HullClassification.BeetleQueen)
            {
                Vector3 vector = grabPosition - target.healthComponent.body.corePosition;
                if (vector.magnitude > pullRadius) return;
                vector *= Time.fixedDeltaTime * pullStrength * (vector.magnitude < pullWeakenRadius ? (vector.magnitude / pullWeakenRadius) : (1 - (vector.magnitude / (pullRadius))));
                vector *= target.healthComponent.body.hullClassification == HullClassification.Golem ? 0.5f : 1;
                if (target.healthComponent.body.characterMotor)
                {
                    target.healthComponent.body.characterMotor.rootMotion += vector;
                }
                else if (target.healthComponent.body.rigidbody)
                {
                    target.healthComponent.body.rigidbody.velocity += vector;
                }
            }
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(HurtBoxReference.FromHurtBox(target));
        }
        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            target = reader.ReadHurtBoxReference().ResolveHurtBox();
        }
    }
}
