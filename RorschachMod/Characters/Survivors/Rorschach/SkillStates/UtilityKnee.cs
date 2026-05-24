using RoR2;
using RorschachMod.Modules.BaseStates;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class UtilityKnee : BaseMeleeAttack
    {
        protected override void Prepare()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericUtility | DamageType.Stun1s;
            damageCoefficient = RorschachStaticValues.utilityKneeDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 0.92f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.1f;
            attackEndPercentTime = 0.4f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 0.6f;

            hitStopDuration = 0.05f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            swingSoundString = "HenrySwordSwing";
            hitSoundString = "";
            muzzleString = swingIndex % 2 == 0 ? "SwingLeft" : "SwingRight";
            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = RorschachAssets.swordSwingEffect;
            hitEffectPrefab = RorschachAssets.swordHitImpactEffect;

            impactSound = RorschachAssets.swordHitSoundEvent.index;
        }

        public override void Update()
        {
            base.Update();
            if (base.isAuthority)
            {
                float fadeTime = duration * (earlyExitPercentTime - attackStartPercentTime);
                Vector3 displacement = inputBank.aimDirection * characterBody.moveSpeed * 1.5f * Time.deltaTime * Mathf.Clamp01(age * (-1 / fadeTime) + ((duration * earlyExitPercentTime) / fadeTime));
                if (characterMotor.isGrounded) displacement.y = 0;
                characterMotor.AddDisplacement(displacement);
                characterMotor.velocity.y = Mathf.Max(-1f, characterMotor.velocity.y);
            }
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("Gesture, Override", "Slash1", playbackRateParam, duration, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
