using EntityStates;
using R2API;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Modules.BaseStates;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialDefaultJudgement : BaseMeleeAttack
    {
        public int judgementStacks;
        public virtual Type finalStateType { get { return typeof(SpecialDefaultFinal); } }
        public virtual Type judgementStateType { get { return typeof(SpecialDefaultJudgement); } }
        public float repeatedAttackDurationMultiplier = 1f;
        public const float durationMultiplierPerRepeat = 0.9f;
        protected override void Prepare()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericSpecial;
            damageType.AddModdedDamageType(RorschachDamageTypes.specialOnKillBuff);
            damageType.AddJudgementStacks(judgementStacks);
            damageCoefficient = RorschachStaticValues.specialJudgementDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 0f;
            bonusForce = Vector3.zero;
            baseDuration = 0.5f * repeatedAttackDurationMultiplier;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.3f;
            attackEndPercentTime = 0.55f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 1f;

            hitStopDuration = 0.03f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            swingSoundString = "HenrySwordSwing";
            hitSoundString = "";
            muzzleString = "SwingLeft";
            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = RorschachAssets.swordSwingEffect;
            hitEffectPrefab = RorschachAssets.swordHitImpactEffect;

            impactSound = RorschachAssets.swordHitSoundEvent.index;

            handleEndState = false;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
                characterBody.RemoveBuff(RorschachBuffs.judgementBuff);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (stopwatch >= duration && isAuthority)
            {
                if (characterBody.HasBuff(RorschachBuffs.judgementBuff))
                {
                    SpecialDefaultJudgement judgementState = (SpecialDefaultJudgement)EntityStateCatalog.InstantiateState(judgementStateType);
                    judgementState.judgementStacks = judgementStacks;
                    judgementState.repeatedAttackDurationMultiplier = repeatedAttackDurationMultiplier * durationMultiplierPerRepeat;
                    this.outer.SetNextState(judgementState);
                }
                else
                {
                    SpecialDefaultFinal finalState = (SpecialDefaultFinal)EntityStateCatalog.InstantiateState(finalStateType);
                    finalState.judgementStacks = judgementStacks;
                    this.outer.SetNextState(EntityStateCatalog.InstantiateState(finalStateType));
                }
            }
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "SpecialDefaultJudgement", playbackRateParam, duration, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            base.OnExit();
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(repeatedAttackDurationMultiplier);
            writer.WritePackedIndex32(judgementStacks);
        }
        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            repeatedAttackDurationMultiplier = reader.ReadSingle();
            judgementStacks = reader.ReadPackedIndex32();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}