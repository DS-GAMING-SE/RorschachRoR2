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

        public static ItemTierDef improvisedWeaponTier;

        public static ItemDef flameCan;
        public static ItemDef pipe;
        public static ItemDef cleaver;
        public static void Initialize()
        {
            improvisedWeaponTier = ScriptableObject.CreateInstance<ItemTierDef>();
            improvisedWeaponTier.tier = ItemTier.AssignedAtRuntime;
            improvisedWeaponTier.isDroppable = true;
            improvisedWeaponTier.canRestack = true;
            improvisedWeaponTier.canRebirth = false;
            improvisedWeaponTier.pickupRules = ItemTierDef.PickupRules.Default;
            improvisedWeaponTier.name = prefix + "TIER";
            improvisedWeaponTier.highlightPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_UI.HighlightTier1Item_prefab).WaitForCompletion();
            improvisedWeaponTier.dropletDisplayPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common.Tier1Orb_prefab).WaitForCompletion();
            improvisedWeaponTier.canScrap = false;
            improvisedWeaponTier.colorIndex = ColorCatalog.ColorIndex.Tier1Item;
            improvisedWeaponTier.darkColorIndex = ColorCatalog.ColorIndex.Unaffordable;
            improvisedWeaponTier.bgIconTexture = Addressables.LoadAssetAsync<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common.texTier1BGIcon_png).WaitForCompletion();
            ContentPacks.itemTierDefs.Add(improvisedWeaponTier);

            RorschachAssets.flameCanItemModel.LoadAssetAsync().Completed += x => { AddModelPanelParams(x.Result); };
            flameCan = AddNewItem("RorschachFlameCan", "FLAME_CAN",
                Addressables.LoadAssetAsync<Sprite>(RorschachAssets.flameCanItemIcon).WaitForCompletion(),
                RorschachAssets.flameCanItemModel);

            RorschachAssets.pipeItemModel.LoadAssetAsync().Completed += x => { AddModelPanelParams(x.Result); };
            pipe = AddNewItem("RorschachPipe", "PIPE",
                Addressables.LoadAssetAsync<Sprite>(RorschachAssets.pipeItemIcon).WaitForCompletion(),
                RorschachAssets.pipeItemModel);

            RorschachAssets.cleaverItemModel.LoadAssetAsync().Completed += x => { AddModelPanelParams(x.Result); };
            cleaver = AddNewItem("RorschachCleaver", "CLEAVER",
                Addressables.LoadAssetAsync<Sprite>(RorschachAssets.cleaverItemIcon).WaitForCompletion(),
                RorschachAssets.cleaverItemModel);

            ImprovisedWeaponManager.Initialize();
        }

        internal static GameObject AddModelPanelParams(GameObject prefab)
        {
            ModelPanelParameters panel = prefab.AddComponent<ModelPanelParameters>();
            panel.focusPointTransform = prefab.transform.Find("FocusPoint");

            panel.cameraPositionTransform = prefab.transform.Find("FocusPoint/CameraPosition");

            panel.minDistance = 0.4f;
            panel.maxDistance = 1f;
            return prefab;
        }

        public static ItemDef AddNewItem(string itemName, string token, Sprite icon, AssetReferenceT<GameObject> pickupModel)
        {
            ItemDef itemDef = ScriptableObject.CreateInstance<ItemDef>();
            itemDef.name = itemName;
            itemDef.tier = ItemTier.AssignedAtRuntime;
            itemDef.pickupModelReference = pickupModel;
            itemDef.pickupIconSprite = icon;
            itemDef.canRemove = true;
            itemDef._itemTierDef = improvisedWeaponTier;

            itemDef.nameToken = prefix + token;
            itemDef.pickupToken = prefix + token + "_PICKUP";
            itemDef.descriptionToken = prefix + token + "_DESC";
            itemDef.loreToken = prefix + token + "_LORE";
            itemDef.tags = new[]
            {
                ItemTag.CanBeTemporary,
                ItemTag.CannotCopy,
                ItemTag.CannotSteal,
                ItemTag.WorldUnique
            };

            ImprovisedWeaponManager.improvisedWeaponItems.Add(itemDef);
            ContentPacks.itemDefs.Add(itemDef);

            return itemDef;
        }
    }
}
