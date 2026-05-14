using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HG;
using System.Linq;
using R2API;

[assembly: HG.Reflection.SearchableAttribute.OptIn]
namespace RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons
{
    public static class ImprovisedWeaponManager
    {
        public static BodyIndex rorschachBodyIndex;
        public static PickupIndex[] improvisedWeaponPickupIndices;
        public static List<ItemDef> improvisedWeaponItems = new List<ItemDef>();

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
            On.RoR2.GenericPickupController.BodyHasPickupPermission += RestrictImprovisedItemPickup;
        }
        public static void OnCharacterDeath(DamageReport damageReport)
        {
            if (damageReport.attackerBody)
            {
                if (damageReport.damageInfo.damageType.HasModdedDamageType(RorschachDamageTypes.specialOnKillBuff))
                {
                    damageReport.attackerBody.AddTimedBuff(RorschachBuffs.specialOnKillBuff, 3f + damageReport.damageInfo.damageType.ReadJudgementStacks());
                    Chat.AddMessage($"Judgement Stacks: {damageReport.damageInfo.damageType.ReadJudgementStacks()}");
                }
                if (damageReport.attackerBodyIndex == rorschachBodyIndex && damageReport.victimBody)
                {
                    if (damageReport.victimIsChampion)
                    {
                        DropItem(damageReport.victimBody.corePosition, damageReport.victimBody.characterDirection ? damageReport.victimBody.characterDirection.forward : damageReport.victimBody.transform.forward);
                    }
                    if (damageReport.victimIsElite && Util.CheckRoll(RorschachStaticValues.passiveEliteDropChance))
                    {
                        DropItem(damageReport.victimBody.corePosition, damageReport.victimBody.characterDirection ? damageReport.victimBody.characterDirection.forward : damageReport.victimBody.transform.forward);
                    }
                }
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
