using EntityStates;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Characters.Survivors.Rorschach.Components;
using RorschachMod.Modules.BaseStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SecondaryDefaultChargedAttack : BaseMeleeAttack
    {
        public float charge;
        protected float movementFadePercentTime = 0.7f;
        protected float sparkleStartPercentTime = 0.15f;
        private bool sparkled;
        
        protected override void Prepare()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericSecondary;
            damageCoefficient = Mathf.Lerp(RorschachStaticValues.secondaryChargeMinDamageCoefficient, RorschachStaticValues.secondaryChargeMaxDamageCoefficient, charge);
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 0.65f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.3f;
            attackEndPercentTime = 0.6f;

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
        public override void Update()
        {
            base.Update();
            if (base.isAuthority)
            {
                SecondaryDefaultDash.UpdateDisplacement(inputBank, characterMotor, age, duration, attackStartPercentTime, movementFadePercentTime, characterBody.moveSpeed * 1.3f);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge >= duration * sparkleStartPercentTime && !sparkled)
            {
                sparkled = true;
                ChildLocator child = GetModelChildLocator();
                int index = child.FindChildIndex("SecondaryChargeHitSparkleTransform");
                EffectData effectData = new EffectData
                {
                    origin = child.FindChild(index).position,
                    scale = charge >= 1 ? 1.5f : 1f,
                    color = charge >= 1 ? Color.red : Color.white
                };
                effectData.SetChildLocatorTransformReference(gameObject, index);
                EffectManager.SpawnEffect(RorschachAssets.genericSparkleEffect, effectData, false);
            }
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "SecondaryDefaultChargedAttack", playbackRateParam, duration * 3f, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            if (!hit)
            {
                EffectManager.SimpleMuzzleFlash(RorschachAssets.meleeHitDirectionalEffect, gameObject, "SecondaryDefaultHitTransform", true);
                if (charge == 1f && characterBody.GetBuffCount(RorschachBuffs.judgementBuff.buffIndex) < RorschachStaticValues.judgementBuffCap)
                {
                    new NetworkJudgement(characterBody.netId).Send(R2API.Networking.NetworkDestination.Clients);
                }
            }
            base.OnHitEnemyAuthority();
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                characterBody.RemoveBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
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
            if (body.modelLocator && body.modelLocator.modelTransform && body.modelLocator.modelTransform.TryGetComponent<OutlineComponent>(out var outline))
            {
                outline.StartFlash();
                /*TemporaryOverlayInstance overlay = TemporaryOverlayManager.AddOverlay(model.gameObject);
                overlay.duration = 0.2f;
                overlay.animateShaderAlpha = true;
                overlay.alphaCurve = AnimationCurve.EaseInOut(0f, 0.6f, 1f, 0f);
                overlay.originalMaterial = Addressables.LoadAssetAsync<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_CritOnUse.matFullCrit_mat).WaitForCompletion();
                overlay.destroyComponentOnEnd = true;
                overlay.inspectorCharacterModel = model;*/
            }
        }
    }
}