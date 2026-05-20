using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates.FlameCan
{
    public class PrimaryFlameCan : BaseState
    {
        public float entryDuration;
        public static float baseEntryDuration = 0.4f;
        public bool hasBegunFlamethrower;
        public float flamethrowerStopwatch;
        
        public override void OnEnter()
        {
            base.OnEnter();
            this.entryDuration = baseEntryDuration / this.attackSpeedStat;
            skillLocator.primary.onSkillChanged += OnSkillChanged;
        }

        public override void OnExit()
        {
            skillLocator.primary.onSkillChanged -= OnSkillChanged;
            base.OnExit();
        }

        private void Fire()
        {
            Ray aimRay = base.GetAimRay();
            if (base.isAuthority)
            {
                BulletAttack bulletAttack = new BulletAttack();
                bulletAttack.owner = base.gameObject;
                bulletAttack.weapon = base.gameObject;
                bulletAttack.origin = aimRay.origin;
                bulletAttack.aimVector = aimRay.direction;
                bulletAttack.minSpread = 0f;
                bulletAttack.damage = RorschachStaticValues.primaryFlameCanDamageCoefficient * characterBody.damage;
                bulletAttack.force = 0f;
                bulletAttack.muzzleName = "Muzzle";
                bulletAttack.hitEffectPrefab = EntityStates.Mage.Weapon.Flamethrower.impactEffectPrefab;
                bulletAttack.isCrit = Util.CheckRoll(characterBody.crit, characterBody.master);
                bulletAttack.radius = EntityStates.Mage.Weapon.Flamethrower.radius;
                bulletAttack.falloffModel = BulletAttack.FalloffModel.Buckshot;
                bulletAttack.stopperMask = LayerIndex.world.mask;
                bulletAttack.procCoefficient = RorschachStaticValues.primaryFlameCanProcCoefficient;
                bulletAttack.maxDistance = RorschachStaticValues.primaryFlameCanRange;
                bulletAttack.smartCollision = true;
                bulletAttack.damageType = (Util.CheckRoll(EntityStates.Mage.Weapon.Flamethrower.ignitePercentChance, base.characterBody.master) ? DamageType.IgniteOnHit : DamageType.Generic);
                bulletAttack.allowTrajectoryAimAssist = false;
                bulletAttack.damageType.damageSource = DamageSource.Primary;
                bulletAttack.Fire();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge >= this.entryDuration && !this.hasBegunFlamethrower)
            {
                this.hasBegunFlamethrower = true;
                this.Fire();
            }
            if (this.hasBegunFlamethrower)
            {
                this.flamethrowerStopwatch += Time.fixedDeltaTime;
                float num = 1f / RorschachStaticValues.primaryFlameCanAttacksPerSecond / characterBody.attackSpeed;
                if (this.flamethrowerStopwatch > num)
                {
                    this.flamethrowerStopwatch -= num;
                    this.Fire();
                }
            }
            if (fixedAge >= this.entryDuration && !inputBank.skill1.down && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        private void OnSkillChanged(GenericSkill genericSkill)
        {
            this.outer.SetNextStateToMain();
        }
    }
}
