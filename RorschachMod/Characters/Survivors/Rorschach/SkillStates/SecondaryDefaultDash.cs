using RoR2;
using RorschachMod.Modules.BaseStates;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryDefaultDash : BaseMeleeAttack
    {
        public virtual Type chargeStateType { get { return typeof(SecondaryDefaultCharge); } }
        protected float movementFadePercentTime = 0.8f;

        protected override void Prepare()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericSecondary;
            damageCoefficient = RorschachStaticValues.secondaryDashDamageCoefficient;
            procCoefficient = 1f;
            bonusForce = Vector3.zero;
            baseDuration = 0.6f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.4f;
            attackEndPercentTime = 0.6f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 0.85f;

            hitStopDuration = 0.016f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            swingSoundString = "HenrySwordSwing";
            hitSoundString = "";
            muzzleString = "SwingRight";
            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = RorschachAssets.swordSwingEffect;
            hitEffectPrefab = RorschachAssets.meleeHitEffect;

            impactSound = RorschachAssets.swordHitSoundEvent.index;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            StartAimMode(0.5f + duration, true);
            characterBody.bodyFlags |= CharacterBody.BodyFlags.Unmovable;
            if (NetworkServer.active)
            {
                characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "SecondaryDefaultDash", playbackRateParam, duration * 1.2f, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority && fixedAge > duration * earlyExitPercentTime && base.inputBank.skill2.down)
            {
                this.outer.SetNextState(EntityStateCatalog.InstantiateState(chargeStateType));
            }
        }

        public override void Update()
        {
            base.Update();
            if (base.isAuthority)
            {
                UpdateDisplacement(inputBank, characterMotor, age, baseDuration, duration, attackStartPercentTime, movementFadePercentTime, characterBody.moveSpeed * (!hit ? 1.6f : 1f));
            }
        }

        public static void UpdateDisplacement(InputBankTest inputBank, CharacterMotor characterMotor, float age, float baseDuration, float duration, float fadeStartPercentTime, float fadeEndPercentTime, float speedMult)
        {
            float fadeTime = duration * (fadeEndPercentTime - fadeStartPercentTime);
            Vector3 displacement = inputBank.aimDirection * speedMult * (baseDuration / duration) * Time.deltaTime * Mathf.Clamp01(age * (-1 / fadeTime) + ((duration * fadeEndPercentTime) / fadeTime));
            Vector3 input = inputBank.moveVector.magnitude > 0.2f ? inputBank.moveVector.normalized : Vector3.zero;
            if (!characterMotor.isFlying)
            {
                input.y = 0;
                input = input.normalized;
            }
            displacement *= (Mathf.Max(0f, (Vector3.Dot(inputBank.aimDirection, input)) * 1.5f) + 1);
            if (characterMotor.isGrounded) displacement.y = 0;
            characterMotor.AddDisplacement(displacement);
            characterMotor.velocity.y = Mathf.Max(-1f, characterMotor.velocity.y);
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            characterBody.bodyFlags &= ~CharacterBody.BodyFlags.Unmovable;
            base.OnExit();
        }
    }
}