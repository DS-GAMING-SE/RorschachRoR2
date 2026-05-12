using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HG;

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
