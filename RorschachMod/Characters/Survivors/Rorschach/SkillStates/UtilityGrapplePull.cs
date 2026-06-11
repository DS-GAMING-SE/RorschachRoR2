using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;
using EntityStates;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class UtilityGrapplePull : ProjectileGrappleController.BaseState
    {
        public static float accelerationTime = 0.5f;
        public static float startSpeed = 35f;
        public static float speed = 75f;

        protected float currentDistance;
        public override void OnEnter()
        {
            base.OnEnter();
            this.currentDistance = Vector3.Distance(this.aimOrigin, this.position);
        }
        public override void FixedUpdateBehavior()
        {
            base.FixedUpdateBehavior();
            this.currentDistance = Vector3.Distance(this.aimOrigin, this.position);
            if (base.isAuthority)
            {
                bool keyHeld = false;
                if (base.owner.stateMachine)
                {
                    BaseSkillState baseSkillState = base.owner.stateMachine.state as BaseSkillState;
                    keyHeld = (baseSkillState != null && baseSkillState.IsKeyDownAuthority());
                }
                if (currentDistance < grappleController.nearBreakDistance)
                {
                    if (grappleController.projectileStickOnImpactController.stuckBody && 
                        grappleController.projectileStickOnImpactController.stuckBody.teamComponent.teamIndex != base.projectileController.teamFilter.teamIndex &&
                        grappleController.projectileStickOnImpactController.stuckBody.healthComponent && grappleController.projectileStickOnImpactController.stuckBody.healthComponent.alive)
                    {
                        owner.stateMachine.SetNextState(new UtilityKnee());
                        SetNextState();
                        return;
                    }
                    else
                    {
                        SetNextState();
                        if (owner.characterMotor && !owner.characterMotor.isGrounded)
                        {
                            owner.characterMotor.velocity.y = Mathf.Max(owner.characterMotor.velocity.y, 10f);
                        }
                        return;
                    }
                }
                if (!grappleController.projectileStickOnImpactController.stuck || !grappleController.OwnerIsInFiringState() || !keyHeld)
                {
                    SetNextState();
                    return;
                }
            }
            if (base.owner.hasEffectiveAuthority && base.owner.characterMotor)
            {
                base.owner.characterMotor.AddDisplacement((this.position - this.aimOrigin).normalized * Time.fixedDeltaTime * GetSpeed());
                base.owner.characterMotor.velocity *= Mathf.Max(1 - (8 * Time.fixedDeltaTime), 0f);
                base.owner.characterMotor.Motor.ForceUnground();
            }
        }
        private float GetSpeed()
        {
            return Mathf.Lerp(startSpeed, speed, fixedAge / accelerationTime);
        }
        public void SetNextState()
        {
            this.outer.SetNextState(new ProjectileGrappleController.ReturnState());
            if (owner.characterMotor)
            {
                owner.characterMotor.velocity = (this.position - this.aimOrigin).normalized * GetSpeed() * 0.35f;
            }
        }
    }
}
