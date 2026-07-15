using RorschachMod.Modules.BaseStates;
using RoR2;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class PrimaryPipe : PrimaryDefault
    {
        protected override void Prepare()
        {
            base.Prepare();

            damageCoefficient = RorschachStaticValues.primaryPipeDamageCoefficient;
            baseDuration = 1.2f;
            hitEffectPrefab = RorschachAssets.meleeHitPipeEffect;
            attackRecoil *= 2f;
            hitStopDuration *= 1.7f;
        }
    }
}