using HG;
using R2API;
using RoR2;
using RoR2.ContentManagement;
using RorschachMod.Characters.Survivors.Rorschach;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static HedgehogUtils.Helpers;
using static RorschachMod.Characters.Survivors.Rorschach.RorschachSurvivor;

namespace RorschachMod.Modules
{
    internal static class Skins
    {
        public static SkinDef[] InitializeSkins(CharacterModel prefabCharacterModel, CharacterModel.RendererInfo[] defaultRendererinfos)
        {
            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.defaultSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial(); };

            SkinDefParams defaultSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            defaultSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            defaultSkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.defaultSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.defaultSkinSwordMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.defaultSkinGunMesh, renderer = defaultRendererinfos[2].renderer }};
            R2API.SkinDefParamsInfo defaultSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "DEFAULT_SKIN_NAME",
                NameToken = "DEFAULT_SKIN",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.defaultSkinIcon).WaitForCompletion(),
                UnlockableDef = null,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = defaultSkinDefParams
            };
            SkinDef defaultSkin = R2API.Skins.CreateNewSkinDef(defaultSkinParamsInfo);
            //add new skindef to our list of skindefs. this is what we'll be passing to the SkinController
            skins.Add(defaultSkin);
            #endregion

            //uncomment this when you have a mastery skin
            #region MasterySkin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.classicSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial(); };

            SkinDefParams masterySkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            masterySkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            masterySkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.classicSkinMaterial;
            masterySkinDefParams.rendererInfos[1].defaultMaterialAddress = RorschachAssets.classicSkinMaterial;
            masterySkinDefParams.rendererInfos[2].defaultMaterialAddress = RorschachAssets.classicSkinMaterial;
            masterySkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.classicSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.classicSkinSwordMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.defaultSkinGunMesh, renderer = defaultRendererinfos[2].renderer }};
            R2API.SkinDefParamsInfo masterySkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "CLASSIC_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "CLASSIC_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.classicSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = masterySkinDefParams
            };
            SkinDef masterySkin = R2API.Skins.CreateNewSkinDef(masterySkinParamsInfo);
            //add new skindef to our list of skindefs. this is what we'll be passing to the SkinController
            skins.Add(masterySkin);
            #endregion

            return skins.ToArray();
        }
    }
}