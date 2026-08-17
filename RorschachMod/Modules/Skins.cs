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
using static RorschachMod.Characters.Survivors.Rorschach.RorschachSkinEffects;
using static RorschachMod.Characters.Survivors.Rorschach.RorschachAssets;

namespace RorschachMod.Modules
{
    internal static class Skins
    {
        public static SkinDef[] InitializeSkins(CharacterModel prefabCharacterModel, CharacterModel.RendererInfo[] defaultRendererinfos)
        {
            List<SkinDef> skins = new List<SkinDef>();

            #region Generic
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.propsMaterial).Completed += x =>
            {
                x.Result.SetHopooMaterial().SetSpecular(0.25f, 1.5f).SpecularIgnoreAlpha();
                AssetAsyncReferenceManager<Texture>.LoadAsset(RorschachAssets.propsFresnelMask).Completed += y =>
                {
                    x.Result.MetalFresnel(y.Result);
                };
            };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.pipeMaterial).Completed += x =>
            {
                x.Result.SetHopooMaterial().SetSpecular(0.5f, 3f);
                AssetAsyncReferenceManager<Texture>.LoadAsset(RorschachAssets.pipeFresnelMask).Completed += y =>
                {
                    x.Result.MetalFresnel(y.Result, 2f, 0.5f);
                };
            };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.flameCanProjectileMaterial).Completed += x =>
            {
                x.Result.SetHopooMaterial().SetSpecular(0.4f, 1.5f).SpecularIgnoreAlpha();
                AssetAsyncReferenceManager<Texture>.LoadAsset(RorschachAssets.propsFresnelMask).Completed += y =>
                {
                    x.Result.GoldFresnel(y.Result, 2.1f, 20f);
                };
            };

            #endregion

            #region DefaultSkin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.defaultSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.17f, 5f); };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.defaultAltSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.17f, 5f); };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.commonSkinArmMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.4f, 3f).SpecularIgnoreAlpha(); };

            SkinDefParams defaultSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            defaultSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            defaultSkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.defaultSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinFaceMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinGlassMesh, renderer = defaultRendererinfos[2].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinArmMesh, renderer = defaultRendererinfos[3].renderer }};
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
            skins.Add(defaultSkin);

            SkinDefParams defaultAltSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            defaultAltSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultSkinDefParams.rendererInfos);
            defaultAltSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.defaultAltSkinMaterial;
            R2API.SkinDefParamsInfo defaultAltSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "DEFAULT_ALT_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "DEFAULT_ALT_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.defaultSkinIcon).WaitForCompletion(),
                //UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = defaultAltSkinDefParams,
                BaseSkins = new SkinDef[] { defaultSkin }
            };
            SkinDef defaultAltSkin = R2API.Skins.CreateNewSkinDef(defaultAltSkinParamsInfo);
            skins.Add(defaultAltSkin);
            #endregion

            #region MasterySkin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.classicSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.25f, 4f); };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.classicAltSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.25f, 4f); };

            SkinDefParams masterySkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            masterySkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            masterySkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.classicSkinMaterial;
            masterySkinDefParams.rendererInfos[2].defaultMaterial = null;
            masterySkinDefParams.rendererInfos[2].defaultMaterialAddress = null;
            masterySkinDefParams.rendererInfos[3].defaultMaterialAddress = null;
            masterySkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.classicSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinFaceMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[2].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[3].renderer }};
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
            skins.Add(masterySkin);

            SkinDefParams classicAltSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            classicAltSkinDefParams.rendererInfos = ArrayUtils.Clone(masterySkinDefParams.rendererInfos);
            classicAltSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.classicAltSkinMaterial;
            R2API.SkinDefParamsInfo classicAltSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "CLASSIC_ALT_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "CLASSIC_ALT_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.classicSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = classicAltSkinDefParams,
                BaseSkins = new SkinDef[] { masterySkin }
            };
            SkinDef classicAltSkin = R2API.Skins.CreateNewSkinDef(classicAltSkinParamsInfo);
            skins.Add(classicAltSkin);
            #endregion
            #region Space Skin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.spaceSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.2f, 5f).SpecularIgnoreAlpha(); };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.spaceAltSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.2f, 5f).SpecularIgnoreAlpha(); };

            SkinDefParams spaceSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            spaceSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            spaceSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.spaceSkinMaterial;
            spaceSkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.spaceSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinFaceMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinGlassMesh, renderer = defaultRendererinfos[2].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinArmMesh, renderer = defaultRendererinfos[3].renderer }};
            R2API.SkinDefParamsInfo spaceSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "SPACE_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "SPACE_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.spaceSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = spaceSkinDefParams
            };
            SkinDef spaceSkin = R2API.Skins.CreateNewSkinDef(spaceSkinParamsInfo);
            skins.Add(spaceSkin);

            SkinDefParams spaceAltSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            spaceAltSkinDefParams.rendererInfos = ArrayUtils.Clone(spaceSkinDefParams.rendererInfos);
            spaceAltSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.spaceAltSkinMaterial;
            R2API.SkinDefParamsInfo spaceAltSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "SPACE_ALT_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "SPACE_ALT_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.spaceSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = spaceAltSkinDefParams,
                BaseSkins = new SkinDef[] { spaceSkin }
            };
            SkinDef spaceAltSkin = R2API.Skins.CreateNewSkinDef(spaceAltSkinParamsInfo);
            skins.Add(spaceAltSkin);
            #endregion

            #region FutureSkin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.futureSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.25f, 4f).SetEmission(0.2f);
                AssetAsyncReferenceManager<Texture>.LoadAsset(RorschachAssets.futureSkinFresnelMask).Completed += y =>
                {
                    x.Result.FresnelEmission(AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_ColorRamps.texRampHuntressSoft_png)).WaitForCompletion(), y.Result, 1.5f, 5f);
                };
            };

            SkinDefParams futureSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            futureSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            futureSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.futureSkinMaterial;
            futureSkinDefParams.rendererInfos[1].defaultMaterial = CreateGlowingFaceMaterial(
                AssetAsyncReferenceManager<Texture>.LoadAsset(futureSkinFaceMask).WaitForCompletion(),
                AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_TiledTextures.texCloudPixel_png)).WaitForCompletion(),
                AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Merc.texRampMercPrisonerMetalFlow_png)).WaitForCompletion());
            futureSkinDefParams.rendererInfos[2].defaultMaterial = null;
            futureSkinDefParams.rendererInfos[2].defaultMaterialAddress = null;
            futureSkinDefParams.rendererInfos[3].defaultMaterialAddress = null;
            futureSkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.futureSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.commonSkinFaceMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[2].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[3].renderer }};
            R2API.SkinDefParamsInfo futureSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "FUTURE_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "FUTURE_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.futureSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = futureSkinDefParams
            };
            SkinDef futureSkin = R2API.Skins.CreateNewSkinDef(futureSkinParamsInfo);
            skins.Add(futureSkin);
            futureSkin.AddSkinColor(new Color(0f, 0.5f, 0.95f));
            #endregion

            #region Question
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.questionSkinMaterial).Completed += x =>
            {
                x.Result.SetHopooMaterial().SetSpecular(0.15f, 4f).SpecularIgnoreAlpha();
            };

            SkinDefParams questionSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            questionSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            questionSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.questionSkinMaterial;
            questionSkinDefParams.rendererInfos[1].defaultMaterial = null;
            questionSkinDefParams.rendererInfos[2].defaultMaterial = null;
            questionSkinDefParams.rendererInfos[2].defaultMaterialAddress = null;
            questionSkinDefParams.rendererInfos[3].defaultMaterialAddress = null;
            questionSkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.questionSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[2].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[3].renderer }};
            R2API.SkinDefParamsInfo questionSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "QUESTION_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "QUESTION_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.questionSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.masterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = questionSkinDefParams
            };
            SkinDef questionSkin = R2API.Skins.CreateNewSkinDef(questionSkinParamsInfo);
            skins.Add(questionSkin);
            questionSkin.AddSkinColor(new Color(1f, 0.9f, 0f));
            #endregion

            #region WarframeSkin
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.warframeSkinMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.3f, 2.5f).SetEmission(0.5f);
                AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC1_Common_ColorRamps.texRampAcidLarva_png)).Completed += y =>
                {
                    AssetAsyncReferenceManager<Texture>.LoadAsset(RorschachAssets.warframeSkinFresnelMask).Completed += z =>
                    {
                        x.Result.FresnelEmission(y.Result, z.Result, 1.5f, 0.7f);
                    };
                };
            };
            AssetAsyncReferenceManager<Material>.LoadAsset(RorschachAssets.warframeSkinHatMaterial).Completed += x =>
            { x.Result.SetHopooMaterial().SetSpecular(0.2f, 2.5f).SpecularIgnoreAlpha().SetEmission(0.5f); };

            SkinDefParams warframeSkinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            warframeSkinDefParams.rendererInfos = ArrayUtils.Clone(defaultRendererinfos);
            warframeSkinDefParams.rendererInfos[0].defaultMaterialAddress = RorschachAssets.warframeSkinMaterial;
            warframeSkinDefParams.rendererInfos[1].defaultMaterial = CreateGlowingFaceMaterial(
                AssetAsyncReferenceManager<Texture>.LoadAsset(commonSkinFaceMask).WaitForCompletion(),
                AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common.texCloudCaustic2_png)).WaitForCompletion(),
            AssetAsyncReferenceManager<Texture>.LoadAsset(warframeSkinFaceRamp).WaitForCompletion());
            warframeSkinDefParams.rendererInfos[1].defaultMaterial.SetTextureScale("_Cloud1Tex", new Vector2(1, 0.3f));
            warframeSkinDefParams.rendererInfos[2].defaultMaterial = null;
            warframeSkinDefParams.rendererInfos[2].defaultMaterialAddress = RorschachAssets.warframeSkinHatMaterial;
            warframeSkinDefParams.rendererInfos[3].defaultMaterialAddress = null;
            warframeSkinDefParams.meshReplacements = new SkinDefParams.MeshReplacement[]
            { new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.warframeSkinMesh, renderer = defaultRendererinfos[0].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.warframeSkinFaceMesh, renderer = defaultRendererinfos[1].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = RorschachAssets.warframeSkinHatMesh, renderer = defaultRendererinfos[2].renderer },
            new SkinDefParams.MeshReplacement { meshAddress = null, renderer = defaultRendererinfos[3].renderer }};
            R2API.SkinDefParamsInfo warframeSkinParamsInfo = new R2API.SkinDefParamsInfo
            {
                Name = RORSCHACH_PREFIX + "WARFRAME_SKIN_NAME",
                NameToken = RORSCHACH_PREFIX + "WARFRAME_SKIN_NAME",
                Icon = Addressables.LoadAssetAsync<Sprite>(RorschachAssets.warframeSkinIcon).WaitForCompletion(),
                UnlockableDef = RorschachUnlockables.grandMasterySkinUnlockableDef,
                RootObject = prefabCharacterModel.gameObject,
                SkinDefParams = warframeSkinDefParams
            };
            SkinDef warframeSkin = R2API.Skins.CreateNewSkinDef(warframeSkinParamsInfo);
            skins.Add(warframeSkin);
            warframeSkin.AddSkinColor(new Color(0.2f, 0.87f, 0.55f));
            #endregion

            return skins.ToArray();
        }
    }
}