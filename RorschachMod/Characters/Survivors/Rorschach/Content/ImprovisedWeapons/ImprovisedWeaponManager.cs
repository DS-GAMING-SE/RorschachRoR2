using HarmonyLib;
using HG;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[assembly: HG.Reflection.SearchableAttribute.OptIn]
namespace RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons
{
    public static class ImprovisedWeaponManager
    {
        public static BodyIndex rorschachBodyIndex;
        public static PickupIndex[] improvisedWeaponPickupIndices;
        public static List<ItemDef> improvisedWeaponItems = new List<ItemDef>();

        public static SkillDef primaryFlameCan;
        public static SkillDef specialFlameCan;

        public static SteppedSkillDef primaryPipe;
        public static SkillDef secondaryPipe;
        public static SkillDef specialPipe;

        public static SteppedSkillDef primaryCleaver;
        public static SkillDef secondaryCleaver;
        public static SkillDef specialCleaver;

        public static void DropItem(Vector3 position, Vector3 velocityDirectionOffset)
        {
            Xoroshiro128Plus rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
            PickupDropletController.CreatePickupDroplet(new UniquePickup
            {
                pickupIndex = improvisedWeaponPickupIndices[rng.RangeInt(0,improvisedWeaponPickupIndices.Length)],
                decayValue = 1
            }, position + Vector3.up * 1.5f, 
            Vector3.up * 20f + velocityDirectionOffset * 2f, false, false);
        }

        public static void Initialize()
        {
            GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeath;
            CharacterBody.onBodyInventoryChangedGlobal += ImprovisedWeaponSkillOverrides;
            On.RoR2.GenericPickupController.BodyHasPickupPermission += RestrictImprovisedItemPickup;
            IL.RoR2.GlobalEventManager.ProcessHitEnemy += SpecialFinalDot;
        }
        public static void OnCharacterDeath(DamageReport damageReport)
        {
            if (NetworkServer.active && damageReport.attackerBody)
            {
                if (damageReport.damageInfo.damageType.HasModdedDamageType(RorschachDamageTypes.specialOnKillBuff))
                {
                    damageReport.attackerBody.AddTimedBuff(RorschachBuffs.specialOnKillBuff, 3f + damageReport.damageInfo.damageType.ReadJudgementStacks());
                }
            }
        }
        public static void SpecialFinalDot(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            // Bleed
            if (c.TryGotoNext(x => x.MatchLdarg(1),
            x => x.MatchLdflda(typeof(DamageInfo), nameof(DamageInfo.procChainMask)),
            x => x.MatchLdcI4((int)ProcType.BleedOnHit)) && c.TryGotoNext(MoveType.After, x => x.MatchLdarg(1)))
            {
                c.Emit(OpCodes.Ldarg_2);
                // Right before normal bleed chance is handled. If cleaver final hit, apply multiple stacks of bleed and add bleed proc mask so it doesn't do normal bleed
                c.EmitDelegate<Action<DamageInfo, GameObject>>((damageInfo, victim) =>
                {
                    if (damageInfo.damageType.HasModdedDamageType(RorschachDamageTypes.cleaverFinalBleed))
                    {
                        for (int i = 0; i < RorschachStaticValues.specialCleaverFinalBleedStacks - 1; i++)
                        {
                            DotController.InflictDot(victim, damageInfo.attacker, damageInfo.inflictedHurtbox, DotController.DotIndex.Bleed, damageInfo.procCoefficient * 3f);
                        }
                    }
                });
                c.Emit(OpCodes.Ldarg_1);
                // Bleed chance for if bleed chance should be rolled at all
                if (c.TryGotoNext(MoveType.After, x => x.MatchCallOrCallvirt(AccessTools.PropertyGetter(typeof(CharacterBody), nameof(CharacterBody.bleedChance)))))
                {
                    c.Emit(OpCodes.Ldarg_1);
                    c.EmitDelegate<Func<float, DamageInfo, float>>((bleedChance, damageInfo) =>
                    {
                        return bleedChance + (damageInfo.damageType.HasModdedDamageType(RorschachDamageTypes.cleaverBleedChance) ? RorschachStaticValues.primaryCleaverBleedChance : 0);
                    });
                }
                // Actual bleed chance
                if (c.TryGotoNext(MoveType.After, x => x.MatchCallOrCallvirt(AccessTools.PropertyGetter(typeof(CharacterBody), nameof(CharacterBody.bleedChance)))))
                {
                    c.Emit(OpCodes.Ldarg_1);
                    c.EmitDelegate<Func<float, DamageInfo, float>>((bleedChance, damageInfo) =>
                    {
                        return bleedChance + (damageInfo.damageType.HasModdedDamageType(RorschachDamageTypes.cleaverBleedChance) ? RorschachStaticValues.primaryCleaverBleedChance : 0);
                    });
                }
            }
            else
            {
                Log.Error("Bleed Dot IL Hook failed");
            }

            // Burn
            // ???????????????????????????????????????????
            /*ILLabel burnEnd = null;
            if (c.TryGotoNext(x => x.MatchLdfld(typeof(DamageInfo), nameof(DamageInfo.damageType)),
                x => x.MatchLdcI4((int)DamageType.IgniteOnHit)) &&
                c.TryGotoNext(x => x.MatchCallOrCallvirt(AccessTools.Method(typeof(StrengthenBurnUtils), nameof(StrengthenBurnUtils.CheckDotForUpgrade))), 
                x => x.MatchBr(out burnEnd)))
            {
                c.GotoLabel(burnEnd, MoveType.After);
                // Intercept the dotinfo and use it to call burn a few times manually if it's flame can special
                // Don't know how to match the DotController.InflictDot method overload with a ref param so I gotta match with this roundabout shit
                c.Emit(OpCodes.Ldarg_1);
                c.EmitDelegate<Func<InflictDotInfo, DamageInfo, InflictDotInfo>>((dot, damageInfo) =>
                {
                    if (damageInfo.damageType.HasModdedDamageType(RorschachDamageTypes.flameCanFinalBurn))
                    {
                        for (int i = 0; i < RorschachStaticValues.specialFlameCanFinalBurnStacks - 1; i++)
                        {
                            DotController.InflictDot(ref dot);
                        }
                    }
                    return dot;
                });
            }
            else
            {
                Log.Error("Burn Dot IL Hook failed");
            }*/
        }

        public static void ImprovisedWeaponSkillOverrides(CharacterBody characterBody)
        {
            if (characterBody && characterBody.skillLocator && characterBody.bodyIndex == rorschachBodyIndex)
            {
                SetSkillOverrideForWeapon(characterBody, ImprovisedWeaponItemDefs.flameCan, primaryFlameCan, null, specialFlameCan);
                SetSkillOverrideForWeapon(characterBody, ImprovisedWeaponItemDefs.pipe, primaryPipe, secondaryPipe, specialPipe);
                SetSkillOverrideForWeapon(characterBody, ImprovisedWeaponItemDefs.cleaver, primaryCleaver, secondaryCleaver, specialCleaver);
            }
        }
        private static void SetSkillOverrideForWeapon(CharacterBody characterBody, ItemDef weapon, SkillDef primary, SkillDef secondary, SkillDef special)
        {
            if (characterBody.inventory.GetItemCountEffective(weapon) > 0)
            {
                if (primary) characterBody.skillLocator.primary.SetSkillOverride(characterBody, primary, GenericSkill.SkillOverridePriority.Upgrade);
                if (secondary) characterBody.skillLocator.secondary.SetSkillOverride(characterBody, secondary, GenericSkill.SkillOverridePriority.Upgrade);
                if (special) characterBody.skillLocator.special.SetSkillOverride(characterBody, special, GenericSkill.SkillOverridePriority.Upgrade);
                return;
            }
            else
            {
                if (primary) characterBody.skillLocator.primary.UnsetSkillOverride(characterBody, primary, GenericSkill.SkillOverridePriority.Upgrade);
                if (secondary) characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody, secondary, GenericSkill.SkillOverridePriority.Upgrade);
                if (special) characterBody.skillLocator.special.UnsetSkillOverride(characterBody, special, GenericSkill.SkillOverridePriority.Upgrade);
                return;
            }
        }

        public static bool RestrictImprovisedItemPickup(On.RoR2.GenericPickupController.orig_BodyHasPickupPermission orig, CharacterBody characterBody, UniquePickup pickup)
        {
            if (improvisedWeaponPickupIndices.Contains(pickup.pickupIndex))
            {
                if (!characterBody || characterBody.bodyIndex != rorschachBodyIndex || !characterBody.inventory)
                {
                    return false;
                }
                PickupDef pickupDef = PickupCatalog.GetPickupDef(pickup.pickupIndex);
                if (pickupDef.itemIndex != ItemIndex.None && characterBody.inventory.GetTotalItemCountOfTier(ImprovisedWeaponItemDefs.improvisedWeaponTier.tier) != characterBody.inventory.GetItemCountEffective(pickupDef.itemIndex))
                {
                    return false;
                }
            }
            return orig(characterBody, pickup);
        }

        [SystemInitializer(typeof(BodyCatalog))]
        public static void SaveRorschachSurvivorIndex()
        {
            rorschachBodyIndex = BodyCatalog.FindBodyIndex("RorschachBody");
        }
        [SystemInitializer(typeof(PickupCatalog))]
        public static void SaveImprovisedWeaponArray()
        {
            List<PickupIndex> pickupIndices = new List<PickupIndex>();
            foreach (var item in improvisedWeaponItems)
            {
                pickupIndices.Add(PickupCatalog.FindPickupIndex(item.itemIndex));
            }
            improvisedWeaponPickupIndices = pickupIndices.ToArray();
        }
    }
}
