using RoR2;
using RorschachMod.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons
{
    public static class ImprovisedWeaponItemDefs
    {
        public const string prefix = RorschachSurvivor.RORSCHACH_PREFIX + "IMPROVISED_WEAPON_";

        public static void Initialize()
        {

        }

        public static ItemDef AddNewItem(string itemName, string token, ItemTierDef itemTier, Sprite icon, AssetReferenceT<GameObject> pickupModel)
        {
            ItemDef itemDef = ScriptableObject.CreateInstance<ItemDef>();
            itemDef.name = itemName;
            itemDef.tier = ItemTier.AssignedAtRuntime;
            itemDef.pickupModelReference = pickupModel;
            itemDef.pickupIconSprite = icon;
            itemDef.canRemove = true;
            itemDef._itemTierDef = itemTier;

            itemDef.nameToken = prefix + token;
            itemDef.pickupToken = prefix + token + "_PICKUP";
            itemDef.descriptionToken = prefix + token + "_DESC";
            itemDef.loreToken = prefix + token + "_LORE";
            itemDef.tags = new[]
            {
                ItemTag.CanBeTemporary,
                ItemTag.CannotCopy,
                ItemTag.CannotSteal,
                ItemTag.WorldUnique,
                ItemTag.AIBlacklist,
                ItemTag.RebirthBlacklist
            };

            ImprovisedWeaponManager.improvisedWeaponItems.Add(itemDef);
            ContentPacks.itemDefs.Add(itemDef);

            return itemDef;
        }
    }
}
