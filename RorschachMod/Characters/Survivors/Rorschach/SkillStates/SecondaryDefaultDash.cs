using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using System;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryDefaultDash : BaseMeleeAttack
    {
        private bool hit;
        public virtual Type chargeStateType { get { return typeof(SecondaryDefaultCharge); } }

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
            attackStartPercentTime = 0.2f;
            attackEndPercentTime = 0.5f;

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
            hitEffectPrefab = RorschachAssets.swordHitImpactEffect;

            impactSound = RorschachAssets.swordHitSoundEvent.index;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("Gesture, Override", "Slash2", playbackRateParam, duration, 0.1f * duration);
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
                float fadeTime = attackEndPercentTime - attackStartPercentTime;
                Vector3 displacement = (inputBank.aimDirection * characterBody.moveSpeed * Time.deltaTime * Mathf.Clamp01(age * ((-1 / duration) / fadeTime) + (attackEndPercentTime / fadeTime))) / (duration * earlyExitPercentTime);
                if (!hit) displacement *= 2f;
                if (characterMotor.isGrounded) displacement.y = 0;
                characterMotor.AddDisplacement(displacement);
                characterMotor.velocity.y = Mathf.Max(-1f, characterMotor.velocity.y);
            }
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
            hit = true;
        }

        public override void OnExit()
        {
            characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            base.OnExit();
        }
    }
}