using EntityStates;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Modules.BaseStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryDefaultChargedAttack : BaseMeleeAttack
    {
        public float charge = 1f;
        protected bool gainedJudgement;
        
        public override void OnEnter()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericSecondary;
            damageCoefficient = Mathf.Lerp(RorschachStaticValues.secondaryChargeMinDamageCoefficient, RorschachStaticValues.secondaryChargeMaxDamageCoefficient, charge);
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 1.4f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.2f;
            attackEndPercentTime = 0.4f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 0.7f;

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

            base.OnEnter();
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
            if (!gainedJudgement && charge == 1f)
            {
                gainedJudgement = true;
                if (characterBody.GetBuffCount(RorschachBuffs.judgementBuff.buffIndex) < RorschachStaticValues.judgementBuffCap)
                {
                    new NetworkJudgement(characterBody.netId).Send(R2API.Networking.NetworkDestination.Clients);
                }
                
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public static void AddJudgementStack(CharacterBody body)
        {
            if (NetworkServer.active)
            {
                body.AddBuff(RorschachBuffs.judgementBuff);
            }
            if (body.modelLocator && body.modelLocator.modelTransform && body.modelLocator.modelTransform.TryGetComponent<CharacterModel>(out var model))
            {
                TemporaryOverlayInstance overlay = TemporaryOverlayManager.AddOverlay(model.gameObject);
                overlay.duration = 0.2f;
                overlay.animateShaderAlpha = true;
                overlay.alphaCurve = AnimationCurve.EaseInOut(0f, 0.6f, 1f, 0f);
                overlay.originalMaterial = Addressables.LoadAssetAsync<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_CritOnUse.matFullCrit_mat).WaitForCompletion();
                overlay.destroyComponentOnEnd = true;
                overlay.inspectorCharacterModel = model;
            }
        }
    }
}