using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialDefaultDash : BaseState
    {
        private bool hit;
        public virtual Type grabStateType { get { return typeof(SpecialDefaultGrab); } }
        protected float baseDuration = 0.7f;
        protected float duration;
        protected float durationPercentAfterGrab = 0.25f;
        protected float movementFadeStartPercentTime = 0.4f;
        protected float movementFadeEndPercentTime = 1f;
        protected HurtBox target;
        protected SphereSearch grabSearch;
        private Transform grabTransform;

        protected virtual void Prepare()
        {
            baseDuration = 0.6f;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            Prepare();
            StartAimMode(0.5f + duration, true);
            duration = baseDuration / attackSpeedStat;
            if (NetworkServer.active)
            {
                characterBody.AddBuff(RoR2Content.Buffs.SmallArmorBoost);
            }
            characterBody.bodyFlags |= CharacterBody.BodyFlags.Unmovable;
            if (isAuthority)
            {
                grabSearch = new SphereSearch();
                grabSearch.mask = LayerIndex.entityPrecise.mask;
                grabTransform = FindModelChild("SpecialGrabTransform");
                if (!grabTransform) return;
                grabSearch.origin = grabTransform.position;
                grabSearch.radius = 4f;
            }
            PlayAttackAnimation();
        }

        protected virtual void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "SpecialDefaultDash", "Slash.playbackRate", duration, 0.1f * duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (isAuthority)
            {
                if (grabTransform && !target)
                {
                    grabSearch.origin = grabTransform.position;
                    grabSearch.RefreshCandidates();
                    grabSearch.FilterCandidatesByDistinctHurtBoxEntities();
                    grabSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex));
                    grabSearch.OrderCandidatesByDistance();
                    target = grabSearch.GetHurtBoxes().FirstOrDefault(x => { return x.healthComponent; });
                    if (target)
                    {
                        duration = Mathf.Min(duration, fixedAge + (duration * durationPercentAfterGrab));
                    }
                }

                if (fixedAge > duration)
                {
                    if (target)
                    {
                        SpecialDefaultGrab grab = (SpecialDefaultGrab)EntityStateCatalog.InstantiateState(grabStateType);
                        grab.target = target;
                        this.outer.SetNextState(grab);
                        return;
                    }
                    else
                    {
                        this.outer.SetNextStateToMain();
                    }
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (base.isAuthority)
            {
                SecondaryDefaultDash.UpdateDisplacement(inputBank, characterMotor, age, baseDuration, duration, movementFadeStartPercentTime, movementFadeEndPercentTime, characterBody.moveSpeed * 2f);
            }
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
