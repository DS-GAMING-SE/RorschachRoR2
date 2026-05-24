using EntityStates;
using EntityStates.Loader;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class UtilityHooking : FireHook
    {
        public override void OnEnter()
        {
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                TrajectoryAimAssist.ApplyTrajectoryAimAssist(ref aimRay, RorschachAssets.grappleProjectilePrefab, base.gameObject, 1f);
                FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                {
                    position = aimRay.origin,
                    rotation = Quaternion.LookRotation(aimRay.direction),
                    crit = base.characterBody.RollCrit(),
                    damage = characterBody.damage * RorschachStaticValues.utilityGrappleDamageCoefficient,
                    force = 0f,
                    damageColorIndex = DamageColorIndex.Default,
                    damageTypeOverride = new DamageTypeCombo?(DamageTypeCombo.GenericUtility) | DamageType.Stun1s,
                    procChainMask = default(ProcChainMask),
                    projectilePrefab = RorschachAssets.grappleProjectilePrefab,
                    owner = base.gameObject
                };
                ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            }
        }
        public override void FixedUpdate()
        {
            if (this.hookStickOnImpact)
            {
                if (this.hookStickOnImpact.stuck && !this.isStuck)
                {
                    //this.PlayAnimation("Grapple", FireHook.FireHookLoopStateHash);
                }
                this.isStuck = this.hookStickOnImpact.stuck;
            }
            if (base.isAuthority && !this.hookInstance && this.hadHookInstance)
            {
                this.outer.SetNextStateToMain();
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Pain;
        }
    }
}
