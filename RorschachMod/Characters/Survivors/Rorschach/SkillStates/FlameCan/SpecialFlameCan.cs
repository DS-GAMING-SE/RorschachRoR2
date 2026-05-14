using EntityStates;
using RoR2;
using RoR2.Projectile;
using RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static RorschachMod.Characters.Survivors.Rorschach.RorschachDamageTypes;
using R2API;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates.FlameCan
{
    public class SpecialFlameCan : AimThrowableBase
    {
        public override void OnEnter()
        {
            projectilePrefab = RorschachAssets.bombProjectilePrefab;
            arcVisualizerPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.BasicThrowableVisualizer_prefab).WaitForCompletion();
            endpointVisualizerPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Huntress.HuntressArrowRainIndicator_prefab).WaitForCompletion();
            damageCoefficient = RorschachStaticValues.specialFlameCanDamageCoefficient;
            setFuse = false;
            baseMinimumDuration = 0.8f;
            maxDistance = 40f;
            rayRadius = 0.3f;
            base.OnEnter();
        }
        public override void ModifyProjectile(ref FireProjectileInfo fireProjectileInfo)
        {
            fireProjectileInfo.damage *= 1 + (characterBody.GetBuffCount(RorschachBuffs.judgementBuff.buffIndex) * RorschachStaticValues.specialFlameCanJudgementDamageMultiplier);
            DamageTypeCombo damageType = DamageTypeCombo.AnyFire;
            damageType.AddModdedDamageType(RorschachDamageTypes.specialOnKillBuff);
            damageType.AddJudgementStacks(characterBody.GetBuffCount(RorschachBuffs.judgementBuff.buffIndex));
            fireProjectileInfo.damageTypeOverride = damageType;
        }

        public override void OnExit()
        {
            base.OnExit();
            characterBody.inventory.RemoveItemTemp(ImprovisedWeaponItemDefs.flameCan.itemIndex);
            characterBody.SetBuffCount(RorschachBuffs.judgementBuff.buffIndex, 0);
        }
    }
}
