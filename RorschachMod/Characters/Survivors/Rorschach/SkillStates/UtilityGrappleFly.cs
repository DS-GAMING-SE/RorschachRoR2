using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using EntityStates;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class UtilityGrappleFly : ProjectileGrappleController.BaseState
    {
        protected float duration;
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.grappleController.maxTravelDistance / this.grappleController.GetComponent<ProjectileSimple>().velocity;
        }
        public override void FixedUpdateBehavior()
        {
            base.FixedUpdateBehavior();
            if (base.isAuthority)
            {
                if (this.grappleController.projectileStickOnImpactController.stuck)
                {
                    DeductUtilityStock();
                    this.outer.SetNextState(new UtilityGrapplePull());
                    return;
                }
                if (this.duration <= base.fixedAge)
                {
                    this.outer.SetNextState(new ProjectileGrappleController.ReturnState());
                    return;
                }
            }
        }
        private void DeductUtilityStock()
        {
            if (base.ownerValid && base.owner.hasEffectiveAuthority)
            {
                SkillLocator component = base.owner.gameObject.GetComponent<SkillLocator>();
                if (component)
                {
                    GenericSkill utility = component.utility;
                    if (utility)
                    {
                        utility.DeductStock(1);
                    }
                }
            }
        }
    }
}
