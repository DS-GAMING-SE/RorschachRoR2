using EntityStates;
using R2API;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Modules.BaseStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialDefaultFinal : BaseMeleeAttack
    {
        public int judgementStacks;
        protected override void Prepare()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericSpecial;
            damageType.AddModdedDamageType(RorschachDamageTypes.specialOnKillBuff);
            damageType.AddJudgementStacks(judgementStacks);
            damageCoefficient = RorschachStaticValues.specialFinalDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 0f;
            bonusForce = Vector3.zero;
            baseDuration = 0.9f + (judgementStacks > 0 ? 0.35f : 0f);

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.3f;
            attackEndPercentTime = 0.55f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 1f;

            hitStopDuration = 0.08f;
            attackRecoil = 0.8f;
            hitHopVelocity = 6f;

            swingSoundString = "HenrySwordSwing";
            hitSoundString = "";
            muzzleString = "SwingLeft";
            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = RorschachAssets.swordSwingEffect;
            hitEffectPrefab = RorschachAssets.meleeHitEffect;

            impactSound = RorschachAssets.swordHitSoundEvent.index;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
        }

        protected override void PlayAttackAnimation()
        {
            if (judgementStacks > 0)
            {
                PlayCrossfade("FullBody, Override", "SpecialDefaultJudgementEnd", playbackRateParam, 3 * duration, 0.1f * duration);
            }
            else
            {
                PlayCrossfade("FullBody, Override", "SpecialDefaultFinal", playbackRateParam, 3 * duration, 0.1f * duration);
            }
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
            writer.WritePackedIndex32(judgementStacks);
        }
        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            judgementStacks = reader.ReadPackedIndex32();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}