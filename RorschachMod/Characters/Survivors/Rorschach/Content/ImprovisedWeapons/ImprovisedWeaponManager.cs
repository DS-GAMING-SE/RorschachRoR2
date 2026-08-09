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
                    damageReport.attackerBody.AddTimedBuff(RorschachBuffs.specialOnKillBuff, RorschachStaticValues.specialOnKillBuffDuration + (RorschachStaticValues.specialOnKillBuffDurationPerJudgement * damageReport.damageInfo.damageType.ReadJudgementStacks()));
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
            ILLabel burnForStart = null;
            ILLabel burnEnd = null;
            VariableDefinition forIterator = new VariableDefinition(il.Import(typeof(int)));
            il.Body.Variables.Add(forIterator);
            VariableDefinition forMax = new VariableDefinition(il.Import(typeof(int)));
            il.Body.Variables.Add(forMax);
            if (c.TryGotoNext(x => x.MatchLdfld(typeof(DamageInfo), nameof(DamageInfo.damageType)),
                x => x.MatchLdcI4((int)DamageType.IgniteOnHit)) && 
                c.TryGotoNext(x => x.MatchBrtrue(out burnForStart)) &&
                c.TryGotoNext(x => x.MatchBrfalse(out burnEnd)))
            {
                // Wrapping the whole igniteOnHit burn stuff in a for loop

                c.Goto(burnForStart.Target, MoveType.AfterLabel);
                int forMaxIndex = il.Body.Variables.Count - 2;
                c.Emit(OpCodes.Ldarg_1);
                c.Emit<DamageInfo>(OpCodes.Ldfld, nameof(DamageInfo.damageType));
                c.EmitDelegate<Func<DamageTypeCombo, bool>>((damageTypeCombo) =>
                {
                    return damageTypeCombo.HasModdedDamageType(RorschachDamageTypes.flameCanFinalBurn);
                });
                // insert brtrue to special flame can stacks
                c.Emit(OpCodes.Ldc_I4_1); // 1 max
                // insert br to store local
                c.Emit(OpCodes.Ldc_I4, RorschachStaticValues.specialFlameCanFinalBurnStacks); // burn stacks
                Instruction ldFlameCanStacks = c.Prev;
                c.Emit(OpCodes.Stloc, forMaxIndex);
                Instruction storeForMax = c.Prev;
                c.Index -= 2;
                c.Emit(OpCodes.Br, storeForMax);
                c.Index -= 2;
                c.Emit(OpCodes.Brtrue, ldFlameCanStacks);


                c.Goto(storeForMax, MoveType.After);
                int forIteratorIndex = il.Body.Variables.Count - 1;
                c.Emit(OpCodes.Ldc_I4_0); // int i = 0
                c.Emit(OpCodes.Stloc, forIteratorIndex);
                Instruction burnStart = c.Next;
                /*c.Emit(OpCodes.Ldloc, forIteratorIndex);
                c.Emit(OpCodes.Ldloc, forMaxIndex);
                c.EmitDelegate<Action<int, int>> ((iterator, max) =>
                {
                    Log.Warning("for (int i = "+iterator + "; i < " + max+"; i++)");
                });*/

                c.Goto(burnEnd.Target);
                c.Emit(OpCodes.Ldloc, forIteratorIndex); // i
                c.Emit(OpCodes.Ldc_I4_1); // 1
                c.Emit(OpCodes.Add);// i + 1
                c.Emit(OpCodes.Stloc, forIteratorIndex); // i = i + 1
                c.Emit(OpCodes.Ldloc, forIteratorIndex); // i
                c.Emit(OpCodes.Ldloc, forMaxIndex); // max
                c.Emit(OpCodes.Blt, burnStart); // if (i < max) go to start

                //Log.Warning(il);
            }
            else
            {
                Log.Error("Burn Dot IL Hook failed");
            }
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
