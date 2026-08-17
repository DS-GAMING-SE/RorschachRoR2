using RorschachMod.Modules;
using RoR2;
using RoR2.Projectile;
using RoR2.ContentManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using R2API;
using RorschachMod.Characters.Survivors.Rorschach.SkillStates;
using RorschachMod.Characters.Survivors.Rorschach.Components;
using ThreeEyedGames;
using UnityEngine.Rendering.PostProcessing;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachAssets
    {
        // common stuff
        public static Material face;

        public static GameObject inkDecal;
        public static Material ink;
        public static Material inkDot;
        public static Material inkTrail;
        public static Material inkStreak;
        public static Material inkVerticalGradient;

        public static Material sparkle;
        public static Material judgementFlash;

        public static Material whiteInkSplat;
        public static Material cleaverCut;

        public static Material cleaverSwingMat;
        public static Material cleaverGlint;

        public static Material fire;
        public static Material fireOut;

        // particle effects
        public static GameObject genericSparkleEffect;

        public static GameObject swordSwingEffect;
        public static GameObject pipeSwingEffect;
        public static GameObject cleaverSwingEffect;

        public static GameObject meleeHitEffect;
        public static GameObject meleeHitPipeEffect;
        public static GameObject meleeHitCleaverEffect;

        public static GameObject secondaryChargeEffect;

        public static GameObject meleeHitDirectionalEffect;

        public static GameObject judgementConsumeEffect;
        public static GameObject specialDefaultHitEffect;
        public static GameObject specialPostProcessVolumeEffect;

        public static GameObject bombExplosionEffect;

        // networked hit sounds
        public static NetworkSoundEventDef swordHitSoundEvent;

        //projectiles
        public static GameObject grappleProjectilePrefab;
        public static GameObject bombProjectilePrefab;

        #region AssetGUIDs
        public static AssetReferenceT<GameObject> characterModel = new AssetReferenceT<GameObject>("8fb604c74ac3bc6488217a956b19c5c1");
        public static AssetReferenceT<GameObject> displayPrefab = new AssetReferenceT<GameObject>("0dcff97246be01d4ebb5aab85c259247");

        public static AssetReferenceT<RuntimeAnimatorController> animator = new AssetReferenceT<RuntimeAnimatorController>("3952c852b06ed0b44a71c936e0236a86");
        public static AssetReferenceT<RuntimeAnimatorController> displayAnimator = new AssetReferenceT<RuntimeAnimatorController>("6af916c388c16a24ea7cda778e419f43");
        public static AssetReferenceT<Avatar> animatorAvatar = new AssetReferenceT<Avatar>("2d566176183833f4b957b8bbb155dfda");
        #region Icons
        public static AssetReferenceTexture characterIcon = new AssetReferenceTexture("33c89ac909113894280a0cfd14c99f2e");

        public static AssetReferenceSprite passiveSkillIcon = new AssetReferenceSprite("0e4c80d73da34aa40b303eb595226e63");

        public static AssetReferenceSprite primarySkillIcon = new AssetReferenceSprite("01b09e98ee52574479b17c0349c88e29");
        public static AssetReferenceSprite primaryFlameCanSkillIcon = new AssetReferenceSprite("ff108aab63a965446ab7dec24b1862ab");
        public static AssetReferenceSprite primaryPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite primaryCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");

        public static AssetReferenceSprite secondarySkillIcon = new AssetReferenceSprite("ab8c7686dfc15b148a46a8bc609acf3e");
        public static AssetReferenceSprite secondaryPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite secondaryCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");

        public static AssetReferenceSprite utilitySkillIcon = new AssetReferenceSprite("575453e0e8deed8488330e8cd804dbab");

        public static AssetReferenceSprite specialSkillIcon = new AssetReferenceSprite("0f8fafa204cb2764ea1a9555ae161057");
        public static AssetReferenceSprite specialFlameCanSkillIcon = new AssetReferenceSprite("70e9a6ae682aede469c23ec9b79a181d");
        public static AssetReferenceSprite specialPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite specialCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");
        #endregion
        #region Skins
        #region Common
        public static AssetReferenceT<Mesh> commonSkinFaceMesh = new AssetReferenceT<Mesh>("f61b5db1bf19b4a469b7bb52c94e1c9a");
        public static AssetReferenceT<Mesh> commonSkinGlassMesh = new AssetReferenceT<Mesh>("4dcc2256131a72247a6f063f380aa674");
        public static AssetReferenceT<Mesh> commonSkinArmMesh = new AssetReferenceT<Mesh>("e621f8f419d8ad04d97aa0da34f43bcc");
        public static AssetReferenceT<Material> commonSkinArmMaterial = new AssetReferenceT<Material>("3c6c7959e383556429c3faa40e7783ff");
        public static AssetReferenceT<Texture> commonSkinFaceTexture = new AssetReferenceT<Texture>("bb2e98fe74378b543b307d6c1e36d8c4");
        public static AssetReferenceT<Texture> commonSkinFaceMask = new AssetReferenceT<Texture>("f96eda95e0e337f49bbd75ebf40311a2");
        #endregion
        #region Default Skin
        public static AssetReferenceT<Material> defaultSkinMaterial = new AssetReferenceT<Material>("83e8838e42f0e0c44bfa65f58572c81e");
        public static AssetReferenceT<Material> defaultAltSkinMaterial = new AssetReferenceT<Material>("8ec4f98e2d3d6034fa9ee2be6f47850e");
        public static AssetReferenceT<Mesh> defaultSkinMesh = new AssetReferenceT<Mesh>("33949338006f1a74192571a62910f38c");
        public static AssetReferenceSprite defaultSkinIcon = new AssetReferenceSprite("e0bd4029a2d0049499dc8c6d68c3716b");
        public static AssetReferenceSprite defaultAltSkinIcon = new AssetReferenceSprite("e0bd4029a2d0049499dc8c6d68c3716b");
        #endregion
        #region Classic Skin
        public static AssetReferenceT<Material> classicSkinMaterial = new AssetReferenceT<Material>("cd3ae74bb0848124b9c4e4813f731cc6");
        public static AssetReferenceT<Material> classicAltSkinMaterial = new AssetReferenceT<Material>("e4d73585e9a9cb248a7dca6e41e784d3");
        public static AssetReferenceT<Mesh> classicSkinMesh = new AssetReferenceT<Mesh>("9d18c52fa7dfd594689995a9777c64be");
        public static AssetReferenceSprite classicSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        public static AssetReferenceSprite classicAltSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Future Skin
        public static AssetReferenceT<Material> futureSkinMaterial = new AssetReferenceT<Material>("b0f8af76c50b18d44a0102da35b23dc3");
        public static AssetReferenceT<Texture> futureSkinFresnelMask = new AssetReferenceT<Texture>("4e5d8ec185ae8674caa695c73cf3c4f7");
        public static AssetReferenceT<Texture> futureSkinFaceTexture = new AssetReferenceT<Texture>("e36638321dfd0b048b863f5820d70b3a");
        public static AssetReferenceT<Texture> futureSkinFaceMask = new AssetReferenceT<Texture>("eb6492d3e16ea3242a1c51c9836549c5");
        public static AssetReferenceT<Mesh> futureSkinMesh = new AssetReferenceT<Mesh>("189fba858f987914dba52ef9286cdc29");
        public static AssetReferenceSprite futureSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Space Skin
        public static AssetReferenceT<Material> spaceSkinMaterial = new AssetReferenceT<Material>("47507833c4f20f642ac05c92737fd26c");
        public static AssetReferenceT<Material> spaceAltSkinMaterial = new AssetReferenceT<Material>("03a3ae7c520de79479ee94d0172bc276");
        public static AssetReferenceT<Mesh> spaceSkinMesh = new AssetReferenceT<Mesh>("cb6e5c2798224374bb04a05a5538c3e9");
        public static AssetReferenceSprite spaceSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        public static AssetReferenceSprite spaceAltSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Question Skin
        public static AssetReferenceT<Material> questionSkinMaterial = new AssetReferenceT<Material>("84866e7150d541b43b2ddfb08f37390a");
        public static AssetReferenceT<Mesh> questionSkinMesh = new AssetReferenceT<Mesh>("115bdb70f73c6b94990ac28f1ec0077f");
        public static AssetReferenceSprite questionSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Warframe Skin
        public static AssetReferenceT<Material> warframeSkinMaterial = new AssetReferenceT<Material>("fa40aa64deb77d04da385a554f3de463");
        public static AssetReferenceT<Texture> warframeSkinFresnelMask = new AssetReferenceT<Texture>("e4cc6ec017771ee41bc179610a4e9b74");
        public static AssetReferenceT<Material> warframeSkinHatMaterial = new AssetReferenceT<Material>("7bd906e6af579f140917fd51aa9d7a6a");
        public static AssetReferenceT<Texture> warframeSkinFaceTexture = new AssetReferenceT<Texture>("87714cd0eb18d2f4e876e96fca19466b");
        public static AssetReferenceT<Texture> warframeSkinFaceRamp = new AssetReferenceT<Texture>("868a757a8351d2043820278b97ea4e42");
        public static AssetReferenceT<Mesh> warframeSkinMesh = new AssetReferenceT<Mesh>("4bb63a9ca15c5b74b90578d026db3464");
        public static AssetReferenceT<Mesh> warframeSkinFaceMesh = new AssetReferenceT<Mesh>("84a8092af2c68d144ad4894eea9140c0");
        public static AssetReferenceT<Mesh> warframeSkinHatMesh = new AssetReferenceT<Mesh>("05076c2208f05c749a649b728b7c8f65");
        public static AssetReferenceSprite warframeSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #endregion
        #region Items
        public static AssetReferenceT<Material> propsMaterial = new AssetReferenceT<Material>("3f08ca721a7f897459657a184428b62b");
        public static AssetReferenceT<Texture> propsFresnelMask = new AssetReferenceT<Texture>("4085fc78a80a6ff44a57db70a0300b61");
        public static AssetReferenceT<Material> pipeMaterial = new AssetReferenceT<Material>("eea6538410b08774f933d0552c9c0953");
        public static AssetReferenceT<Texture> pipeFresnelMask = new AssetReferenceT<Texture>("b67430196d506cc48bc319fcfbef10fa");
        public static AssetReferenceT<Material> flameCanProjectileMaterial = new AssetReferenceT<Material>("df0433928261c9a4982e152b2e1b62c7");

        public static AssetReferenceT<Sprite> flameCanItemIcon = new AssetReferenceT<Sprite>("3aeeaa5cf05fa474da51d6d67fe66ec2");
        public static AssetReferenceT<Sprite> pipeItemIcon = new AssetReferenceT<Sprite>("ce52c093b0698164e878b9cdce9eb986");
        public static AssetReferenceT<Sprite> cleaverItemIcon = new AssetReferenceT<Sprite>("885cc573c66c8ff44a27ee0c0899b244");

        public static AssetReferenceT<GameObject> flameCanItemModel = new AssetReferenceT<GameObject>("2142cbcfebb888744983f02b83eb0dba");
        public static AssetReferenceT<GameObject> pipeItemModel = new AssetReferenceT<GameObject>("508bdaea945e3b8498a2d82f4825bc51");
        public static AssetReferenceT<GameObject> cleaverItemModel = new AssetReferenceT<GameObject>("8dce5d1b37999f34faf5a6f53164427b");
        #endregion
        #region VFX
        public static AssetReferenceT<GameObject> improvisedWeaponItemOrb = new AssetReferenceT<GameObject>("d4d8106d098d889438f9eee294cbebae");

        public static AssetReferenceT<GameObject> flameCanSpecialProjectileGhost = new AssetReferenceT<GameObject>("fc0da765d454d7e4f86895ef76de0172");
        public static AssetReferenceT<GameObject> projectileExplodeEffect = new AssetReferenceT<GameObject>("05b273758480af74a919e826c7b80a86");

        public static AssetReferenceT<GameObject> genericSparkle = new AssetReferenceT<GameObject>("8dd6a179250db5d458b896b7572f1266");

        public static AssetReferenceT<GameObject> swingEffect = new AssetReferenceT<GameObject>("3534552e7829f9842ba3156065afc540");
        public static AssetReferenceT<GameObject> pipeSwing = new AssetReferenceT<GameObject>("b1e80758290eaf5478dd973a3734f867");
        public static AssetReferenceT<GameObject> cleaverSwing = new AssetReferenceT<GameObject>("d06dd27480f9f064ab51ee765fc4de20");

        public static AssetReferenceT<GameObject> meleeHit = new AssetReferenceT<GameObject>("9dc987e0064626645be61b151c52fb04");
        public static AssetReferenceT<GameObject> meleeHitPipe = new AssetReferenceT<GameObject>("8c1b45118d0a20143b72f7cee5285454");
        public static AssetReferenceT<GameObject> meleeHitCleaver = new AssetReferenceT<GameObject>("68ddfb3f59849ce4196d765297867f0a");

        public static AssetReferenceT<GameObject> secondaryCharge = new AssetReferenceT<GameObject>("3b854b20ca3a63a4eb9d48eb717a25f8");

        public static AssetReferenceT<GameObject> meleeHitDirectional = new AssetReferenceT<GameObject>("974a977cf126bc14084058dfe44b53c6");

        public static AssetReferenceT<GameObject> judgementConsume = new AssetReferenceT<GameObject>("5a48c603bbbc48640b6dc9c8a7cad9c1");
        public static AssetReferenceT<GameObject> specialDefaultHit = new AssetReferenceT<GameObject>("5f33ae1c57392d74489392a73d2cb6c3");
        public static AssetReferenceT<GameObject> specialPostProcessVolume = new AssetReferenceT<GameObject>("1cde781a8e6fd40488dd4ae7e68514f5");
        public static AssetReferenceT<PostProcessProfile> specialPostProcessProfile = new AssetReferenceT<PostProcessProfile>("ebbf7b50b1bef5a4499c9746a4d69515");
        #endregion
        #endregion

        public static void Init()
        {
            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

            CreateEffects();
        }

        #region effects
        private static void CreateEffects()
        {
            #region Common Materials
            inkDecal = PrefabAPI.CreateEmptyPrefab("RorschachInkDecal");
            var inkDecalMeshRenderer = inkDecal.AddComponent<MeshRenderer>();
            inkDecalMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            inkDecal.AddComponent<MeshFilter>().sharedMesh = AssetAsyncReferenceManager<Mesh>.LoadAsset(new AssetReferenceT<Mesh>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.Decalicious.DecalCube_asset)).WaitForCompletion();
            var decal = inkDecal.AddComponent<Decal>();
            decal.Material = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.matTankerAccelerantGooDecal_mat)).WaitForCompletion());
            decal.Material.SetVector("_CutoffScroll", new Vector4(0f, 0f, 0f, 0f));
            inkDecalMeshRenderer.sharedMaterial = decal.Material;
            decal.RenderMode = Decal.DecalRenderMode.Deferred;
            decal.DrawAlbedo = true;
            decal.DrawNormalAndGloss = false;
            decal.Fade = 1f;
            inkDecal.AddComponent<ReturnToPoolOnUnseen>();
            Asset.CreateEffect(inkDecal, -1f, false, "", out var inkDecalVFX, out var inkDecalEffect);
            inkDecalVFX.vfxPriority = VFXAttributes.VFXPriority.Medium;
            inkDecalEffect.applyScale = true;

            Mesh donut2 = AssetAsyncReferenceManager<Mesh>.LoadAsset(new AssetReferenceT<Mesh>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.mdlVFXDonut2_fbx_donut2Mesh_)).WaitForCompletion();
            Mesh donut5 = AssetAsyncReferenceManager<Mesh>.LoadAsset(new AssetReferenceT<Mesh>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.mdlVFXDonut5_fbx_donut5Mesh_)).WaitForCompletion();

            ink = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.matTankerGreasePuddleStreaks_mat)).WaitForCompletion();
            inkDot = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matBloodClaySingle_mat)).WaitForCompletion()).Inkify();
            inkDot.name = "matRorschachInkDot";

            inkTrail = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.matTankerAccelerantTrail_mat)).WaitForCompletion();
            inkStreak = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC1_VoidMegaCrab.matVoidCrabAntiMatterParticleStreak_mat)).WaitForCompletion());
            inkStreak.name = "matRorschachInkStreak";
            inkStreak.Inkify();
            inkStreak.SetTexture("_MainTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Imp.texImpSwipeMask_png)).WaitForCompletion());
            inkVerticalGradient = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.matTankerAccelerantBall_mat)).WaitForCompletion());
            inkVerticalGradient.name = "matRorschachInkVerticalGradient";
            inkVerticalGradient.SetTexture("_MainTex", Addressables.LoadAssetAsync<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC2_Common.texRampVerticalSmoothFalloff_png).WaitForCompletion());
            inkVerticalGradient.SetFloat("_AlphaBoost", 0.75f);
            inkVerticalGradient.SetFloat("_Cutoff", 0.1f);
            inkVerticalGradient.SetTextureScale("_Cloud1Tex", new Vector2(4f, 2f));
            inkVerticalGradient.SetTextureScale("_Cloud2Tex", new Vector2(2f, 0.5f));

            sparkle = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC2_FalseSonBoss.matLunarGazeFireLaser1_mat)).WaitForCompletion());
            sparkle.EnableKeyword("VERTEXCOLOR");
            sparkle.DisableKeyword("DISABLEREMAP");
            sparkle.SetTexture("_RemapTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_ColorRamps.texRampDefault_png)).WaitForCompletion());
            sparkle.SetInt("_ZTest", 8);
            sparkle.SetFloat("_DepthOffset", -1.5f);
            sparkle.SetFloat("_InvFade", 0.5f);
            sparkle.SetFloat("_Boost", 2.5f);
            sparkle.name = "matRorschachSparkle";

            judgementFlash = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark1Generic_mat)).WaitForCompletion());
            judgementFlash.name = "matRorschachJudgementFlash";
            judgementFlash.SetInt("_ZTest", 7);
            judgementFlash.SetFloat("_InvFade", 2f);
            judgementFlash.SetFloat("_DepthOffset", -2f);
            judgementFlash.SetFloat("_Boost", 5f);
            judgementFlash.SetFloat("_AlphaBoost", 1.5f);
            judgementFlash.SetFloat("_AlphaBias", 0f);
            judgementFlash.SetTexture("_MainTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.texOmniExplosion2Mask_png)).WaitForCompletion());
            judgementFlash.SetTexture("_RemapTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_ColorRamps.texRampDefault_png)).WaitForCompletion());
            judgementFlash.SetTexture("_Cloud1Tex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drone_Tech.texNanoScanLines_png)).WaitForCompletion());
            judgementFlash.SetTexture("_Cloud2Tex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_TiledTextures.texCloudIce_png)).WaitForCompletion());
            judgementFlash.SetVector("_CutoffScroll", new Vector4(0f, -100f, 40f, -200f));
            judgementFlash.SetFloat("_DistortionStrength", 0.5f);
            judgementFlash.EnableKeyword("USE_CLOUDS");
            judgementFlash.EnableKeyword("CLOUDOFFSET");

            whiteInkSplat = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark2Generic_mat)).WaitForCompletion());
            whiteInkSplat.SetFloat("_InvFade", 0.4f);
            whiteInkSplat.SetFloat("_DepthOffset", -2f);
            whiteInkSplat.SetInt("_ZTest", 8);
            whiteInkSplat.name = "matRorschachWhiteInkSplat";
            cleaverCut = new Material(whiteInkSplat);
            cleaverCut.name = "matRorschachCleaverCut";
            whiteInkSplat.SetTexture("_MainTex", Addressables.LoadAssetAsync<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.texOmniShockwave3Mask_png).WaitForCompletion());

            cleaverSwingMat = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Huntress.matHuntressSwingTrail_mat)).WaitForCompletion());
            cleaverSwingMat.name = "matRorschachCleaverSwing";
            cleaverGlint = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matWideGlow_mat)).WaitForCompletion());
            cleaverGlint.name = "matRorschachCleaverGlint";
            cleaverGlint.SetTextureScale("_MainTex", new Vector2(1f, 2f));
            cleaverGlint.SetTextureOffset("_MainTex", new Vector2(0, -0.5f));
            cleaverGlint.SetTexture("_RemapTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_ColorRamps.texRampTritone_png)).WaitForCompletion());
            cleaverGlint.SetFloat("_AlphaBoost", 2f);
            cleaverGlint.SetFloat("_Boost", 20f);
            cleaverGlint.SetFloat("_DepthOffset", -1.5f);
            cleaverGlint.SetInt("_ZTest", 8);
            cleaverGlint.SetFloat("_InvFade", 0.7f);

            fire = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matMageFlamethrower_mat)).WaitForCompletion();
            fireOut = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark1_mat)).WaitForCompletion();
            #endregion

            Material outlineMat = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common.matEnergyShield_mat)).WaitForCompletion());
            outlineMat.SetTexture("_RemapTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC2.texRampTritoneHShrine_png)).WaitForCompletion());
            outlineMat.SetColor("_TintColor", new Color(1f, 0f, 0.05f));
            outlineMat.SetInt("_SrcBlend", 1);
            outlineMat.SetInt("_DstBlend", 10);
            outlineMat.SetFloat("_OffsetAmount", 0.008f);
            outlineMat.SetFloat("_Boost", 1.5f);
            outlineMat.SetFloat("_AlphaBoost", 0f);
            outlineMat.name = "matRorschachOutline";
            OutlineComponent.material = outlineMat;

            GameObject itemOrb = Addressables.LoadAssetAsync<GameObject>(improvisedWeaponItemOrb).WaitForCompletion();
            var itemOrbSfx1 = itemOrb.AddComponent<PlaySoundOnEvent>();
            itemOrbSfx1.triggeringEvent = PlaySoundOnEvent.PlaySoundEvent.Start;
            itemOrbSfx1.soundEvent = "Play_UI_item_spawn_tier2";
            var itemOrbSfx2 = itemOrb.AddComponent<PlaySoundOnEvent>();
            itemOrbSfx2.triggeringEvent = PlaySoundOnEvent.PlaySoundEvent.Destroy;
            itemOrbSfx2.soundEvent = "Play_UI_item_land_tier2";
            itemOrb.transform.GetChild(0).GetComponent<TrailRenderer>().sharedMaterial = inkTrail;
            Material itemOrbCore = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matTracer_mat)).WaitForCompletion());
            itemOrbCore.SetInt("_ZTest", 8);
            itemOrbCore.SetFloat("_DepthOffset", -1f);
            itemOrb.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = itemOrbCore;
            itemOrb.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
            RorschachAssets.projectileExplodeEffect.LoadAssetAsync().Completed += x =>
            { 
                CreateBombExplosionEffect(x.Result);
                CreateBombProjectile();
            };
            Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Loader.LoaderYankHook_prefab).Completed += x =>
            {
                grappleProjectilePrefab = PrefabAPI.InstantiateClone(x.Result, "RorschachGrappleProjectile");
                ProjectileGrappleController grappleController = grappleProjectilePrefab.GetComponent<ProjectileGrappleController>();
                grappleController.yankMassLimit = 0;
                grappleController.ownerHookStateType = new EntityStates.SerializableEntityStateType(typeof(UtilityHooking));
                grappleController.muzzleStringOnBody = "GrapplingHookMuzzle";
                grappleController.nearBreakDistance = 5f;
                EntityStateMachine stateMachine = grappleProjectilePrefab.GetComponent<EntityStateMachine>();
                stateMachine.initialStateType = new EntityStates.SerializableEntityStateType(typeof(UtilityGrappleFly));
                stateMachine.mainStateType = new EntityStates.SerializableEntityStateType(typeof(UtilityGrappleFly));
                var overlap = grappleProjectilePrefab.GetComponent<ProjectileOverlapAttack>();
                overlap.damageCoefficient = 1f;
                overlap.impactEffect = meleeHitEffect;
            };

            RorschachAssets.swingEffect.LoadAssetAsync().Completed += x =>
            { swordSwingEffect = Asset.CreateEffect(x.Result, 1f, true, ""); };
            RorschachAssets.pipeSwing.LoadAssetAsync().Completed += x =>
            {
                pipeSwingEffect = Asset.CreateEffect(x.Result, -1f, true, "");
                var swing0 = pipeSwingEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
                var swing1 = pipeSwingEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>();
                swing0.sharedMaterial = inkStreak;
                swing0.mesh = donut2;
                swing1.sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Brother.matBrotherSwingDistortion_mat)).WaitForCompletion();
                swing1.mesh = donut2;
                pipeSwingEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
                pipeSwingEffect.AddComponent<DestroyOnParticleEnd>().trackedParticleSystem = pipeSwingEffect.transform.GetChild(2).GetComponent<ParticleSystem>();
                var scale = pipeSwingEffect.AddComponent<ScaleParticleSystemDuration>();
                scale.initialDuration = 0.66f;
                scale.particleSystems = new ParticleSystem[] { pipeSwingEffect.transform.GetChild(0).GetComponent<ParticleSystem>(),
                pipeSwingEffect.transform.GetChild(1).GetComponent<ParticleSystem>(),
                pipeSwingEffect.transform.GetChild(2).GetComponent<ParticleSystem>()};
            };
            RorschachAssets.cleaverSwing.LoadAssetAsync().Completed += x =>
            { 
                cleaverSwingEffect = Asset.CreateEffect(x.Result, -1f, true, "");
                var swing0 = cleaverSwingEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
                var swing1 = cleaverSwingEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>();
                var swing2 = cleaverSwingEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>();
                swing0.sharedMaterial = cleaverSwingMat;
                swing0.mesh = donut2;
                swing1.sharedMaterial = inkStreak;
                swing1.mesh = donut2;
                swing2.sharedMaterial = cleaverGlint;
                swing2.mesh = donut2;
                cleaverSwingEffect.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
                cleaverSwingEffect.AddComponent<DestroyOnParticleEnd>().trackedParticleSystem = cleaverSwingEffect.transform.GetChild(3).GetComponent<ParticleSystem>();
                var scale = cleaverSwingEffect.AddComponent<ScaleParticleSystemDuration>();
                scale.initialDuration = 0.66f;
                scale.particleSystems = new ParticleSystem[] { cleaverSwingEffect.transform.GetChild(0).GetComponent<ParticleSystem>(),
                cleaverSwingEffect.transform.GetChild(1).GetComponent<ParticleSystem>(),
                cleaverSwingEffect.transform.GetChild(2).GetComponent<ParticleSystem>(),
                cleaverSwingEffect.transform.GetChild(3).GetComponent<ParticleSystem>()};
            };

            RorschachAssets.meleeHit.LoadAssetAsync().Completed += x =>
            { 
                meleeHitEffect = Asset.CreateEffect(x.Result, 0.35f);
                meleeHitEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = whiteInkSplat;
                meleeHitEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
                meleeHitEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = inkDot;
                meleeHitEffect.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniRing2Generic_mat)).WaitForCompletion();
            };
            RorschachAssets.meleeHitPipe.LoadAssetAsync().Completed += x =>
            {
                meleeHitPipeEffect = Asset.CreateEffect(x.Result, 0.35f);
                meleeHitPipeEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = whiteInkSplat;
                meleeHitPipeEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
                meleeHitPipeEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = inkDot;
                meleeHitPipeEffect.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniRing2Generic_mat)).WaitForCompletion();
                meleeHitPipeEffect.transform.GetChild(4).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark1Generic_mat)).WaitForCompletion();
            };
            RorschachAssets.meleeHitCleaver.LoadAssetAsync().Completed += x =>
            {
                meleeHitCleaverEffect = Asset.CreateEffect(x.Result, 0.35f);
                meleeHitCleaverEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = cleaverCut;
                meleeHitCleaverEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
                meleeHitCleaverEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = inkDot;
                meleeHitCleaverEffect.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniRing2Generic_mat)).WaitForCompletion();
            };

            RorschachAssets.secondaryCharge.LoadAssetAsync().Completed += x =>
            {
                secondaryChargeEffect = Asset.CreateEffect(x.Result, -1f, true, "");
                var inkRing = secondaryChargeEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
                inkRing.sharedMaterial = inkStreak;
                inkRing.mesh = donut2;
            };

            RorschachAssets.genericSparkle.LoadAssetAsync().Completed += x =>
            {
                genericSparkleEffect = Asset.CreateEffect(x.Result, 0.13f, true, "", out var vfx, out var effect);
                effect.applyScale = true;
                var particleColor = genericSparkleEffect.AddComponent<ParticleSystemColorFromEffectData>();
                particleColor.particleSystems = new ParticleSystem[] { genericSparkleEffect.transform.GetChild(0).GetComponent<ParticleSystem>(), genericSparkleEffect.transform.GetChild(1).GetComponent<ParticleSystem>() };
                particleColor.effectComponent = effect;
                //genericSparkleEffect.AddComponent<DestroyOnParticleEnd>().trackedParticleSystem = genericSparkleEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                genericSparkleEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = sparkle;
                genericSparkleEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Golem.matShockwave1_mat)).WaitForCompletion();
            };

            RorschachAssets.meleeHitDirectional.LoadAssetAsync().Completed += x =>
            {
                meleeHitDirectionalEffect = Asset.CreateEffect(x.Result, 0.65f, true, "", out var vfx, out _);
                vfx.vfxIntensity = VFXAttributes.VFXIntensity.Medium;
                var dome = AssetAsyncReferenceManager<Mesh>.LoadAsset(new AssetReferenceT<Mesh>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC2_Common.mdlVFXDome_fbx_mdVFXDome_)).WaitForCompletion();
                var domeOuter = meleeHitDirectionalEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
                var domeInner = meleeHitDirectionalEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>();
                domeOuter.sharedMaterial = new Material(inkVerticalGradient);
                domeOuter.sharedMaterial.SetInt("_Cull", 2);
                domeOuter.sharedMaterial.SetFloat("_AlphaBoost", 0.35f);
                domeOuter.mesh = dome;
                domeInner.sharedMaterial = new Material(inkVerticalGradient);
                domeInner.sharedMaterial.SetInt("_Cull", 1);
                domeInner.mesh = dome;
                meleeHitDirectionalEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = inkDot;
                meleeHitDirectionalEffect.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().sharedMaterial = inkDot;
                meleeHitDirectionalEffect.transform.GetChild(4).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark2Generic_mat)).WaitForCompletion();
            };

            RorschachAssets.judgementConsume.LoadAssetAsync().Completed += x =>
            { 
                judgementConsumeEffect = Asset.CreateEffect(x.Result, 0.2f, true, "", out var vfx, out var effect);
                judgementConsumeEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = judgementFlash;
                judgementConsumeEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matWideGlow_mat)).WaitForCompletion();
                var color = judgementConsumeEffect.AddComponent<ParticleSystemColorFromEffectData>();
                color.particleSystems = new ParticleSystem[] { judgementConsumeEffect.transform.GetChild(0).GetComponent<ParticleSystem>(), judgementConsumeEffect.transform.GetChild(1).GetComponent<ParticleSystem>(), judgementConsumeEffect.transform.GetChild(2).GetComponent<ParticleSystem>() };
                color.effectComponent = effect;
            };
            RorschachAssets.specialDefaultHit.LoadAssetAsync().Completed += x =>
            {
                specialDefaultHitEffect = Asset.CreateEffect(x.Result, 1f, true, "", out var vfx, out var effect);
                specialDefaultHitEffect.AddComponent<SpawnInkDecal>().scale = 8f;
                vfx.vfxIntensity = VFXAttributes.VFXIntensity.Medium;
                effect.applyScale = true;
                var particleColor = specialDefaultHitEffect.AddComponent<ParticleSystemColorFromEffectData>();
                particleColor.particleSystems = new ParticleSystem[] { specialDefaultHitEffect.transform.GetChild(0).GetComponent<ParticleSystem>(), specialDefaultHitEffect.transform.GetChild(5).GetComponent<ParticleSystem>() };
                particleColor.effectComponent = effect;
                specialDefaultHitEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark2Generic_mat)).WaitForCompletion();
                specialDefaultHitEffect.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = cleaverCut;
                specialDefaultHitEffect.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = cleaverCut;
                var inkDonut5 = specialDefaultHitEffect.transform.GetChild(3).GetComponent<ParticleSystemRenderer>();
                inkDonut5.mesh = donut5;
                inkDonut5.sharedMaterial = inkTrail;
                specialDefaultHitEffect.transform.GetChild(4).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
                specialDefaultHitEffect.transform.GetChild(5).GetComponent<ParticleSystemRenderer>().sharedMaterial = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark3Generic_mat)).WaitForCompletion();
                specialDefaultHitEffect.transform.GetChild(6).GetComponent<ParticleSystemRenderer>().sharedMaterial = whiteInkSplat;
                specialDefaultHitEffect.transform.GetChild(7).GetComponent<ParticleSystemRenderer>().sharedMaterial = ink;
            };
            RorschachAssets.specialPostProcessVolume.LoadAssetAsync().Completed += x =>
            {
                specialPostProcessVolumeEffect = Asset.CreateEffect(x.Result, -1f, true, "", out var vfx, out var effect);
                var postProcessController = specialPostProcessVolumeEffect.AddComponent<RorschachSpecialPostProcessController>();
                postProcessController.localCameraEffect = specialPostProcessVolumeEffect.AddComponent<LocalCameraEffect>();
                postProcessController.localCameraEffect.effectRoot = specialPostProcessVolumeEffect.transform.GetChild(0).gameObject;
                postProcessController.ppVolume = specialPostProcessVolumeEffect.transform.GetChild(0).GetComponent<PostProcessVolume>();
            };
            RorschachAssets.specialPostProcessProfile.LoadAssetAsync().Completed += x =>
            {
                RampFog rampFogSettings = ScriptableObject.CreateInstance<RampFog>();
                rampFogSettings.enabled.Override(true);
                rampFogSettings.skyboxStrength.Override(0f);
                rampFogSettings.fogIntensity.Override(3f);
                rampFogSettings.fogColorStart.Override(new Color(0.75f, 0.75f, 0.75f, 0f));
                rampFogSettings.fogColorMid.Override(new Color(1f, 1f, 1f, 0.4f));
                rampFogSettings.fogColorEnd.Override(Color.white);
                rampFogSettings.fogZero.Override(0.02f);
                x.Result.AddSettings(rampFogSettings);
            };
        }

        private static void CreateBombExplosionEffect(GameObject prefab)
        {
            bombExplosionEffect = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.OmniExplosionVFX_prefab).WaitForCompletion(), "RorschachFlameCanExplosionEffect");//Asset.CreateEffect(prefab, 5f, false, "HenryBombExplosion");
            bombExplosionEffect.GetComponent<DestroyOnTimer>().duration = 3.5f;
            bombExplosionEffect.AddComponent<SpawnInkDecal>();//.copyRotation = true;

            ShakeEmitter shakeEmitter = bombExplosionEffect.AddComponent<ShakeEmitter>();
            shakeEmitter.amplitudeTimeDecay = true;
            shakeEmitter.duration = 0.5f;
            shakeEmitter.radius = 10f;
            shakeEmitter.scaleShakeRadiusWithLocalScale = true;

            shakeEmitter.wave = new Wave
            {
                amplitude = 1f,
                frequency = 40f,
                cycleOffset = 0f
            };
            Content.CreateAndAddEffectDef(bombExplosionEffect);
        }
        #endregion effects

        #region projectiles

        private static void CreateBombProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            bombProjectilePrefab = Asset.CloneProjectilePrefab("CommandoGrenadeProjectile", "RorschachBombProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(bombProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion bombImpactExplosion = bombProjectilePrefab.AddComponent<ProjectileImpactExplosion>();
            
            bombImpactExplosion.blastRadius = RorschachStaticValues.specialFlameCanMinExplosionRadius;
            bombImpactExplosion.blastDamageCoefficient = 1f;
            bombImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            bombImpactExplosion.destroyOnEnemy = true;
            bombImpactExplosion.lifetime = 1.5f;
            bombImpactExplosion.impactEffect = bombExplosionEffect;
            bombImpactExplosion.lifetimeExpiredSound = Content.CreateAndAddNetworkSoundEventDef("HenryBombExplosion");
            bombImpactExplosion.timerAfterImpact = true;
            bombImpactExplosion.lifetimeAfterImpact = 0.1f;
            bombImpactExplosion.preserveExplosionOrientation = true;

            var damage = bombProjectilePrefab.GetComponent<ProjectileDamage>();
            damage.damageType = DamageTypeCombo.AnyFire;
            damage.damageType.damageSource = DamageSource.Special;

            bombProjectilePrefab.AddComponent<ScaleImpactExplosionWithJudgement>();

            ProjectileController bombController = bombProjectilePrefab.GetComponent<ProjectileController>();
            RorschachAssets.flameCanSpecialProjectileGhost.LoadAssetAsync().Completed += x =>
            {
                bombController.ghostPrefab = Asset.CreateProjectileGhostPrefab(x.Result);
                x.Result.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sharedMaterial = fireOut;
                var trail = x.Result.transform.GetChild(3).GetComponent<TrailRenderer>();
                trail.sharedMaterial = inkTrail;
                // detach trail?
            };
            
            bombController.startSound = "";

            Content.AddProjectilePrefab(bombProjectilePrefab);
        }
        #endregion projectiles

        private static Material Inkify(this Material material)
        {
            material.SetTexture("_RemapTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_ColorRamps.texRampMaulingRockHit_png)).WaitForCompletion());
            material.SetColor("_TintColor", new Color(0.06f, 0.06f, 0.06f));
            return material;
        }

        internal static Material CreateDefaultFaceMaterial()
        {
            if (face) return face;

            face = new Material(AssetAsyncReferenceManager<Shader>.LoadAsset(new AssetReferenceT<Shader>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Shaders.HGOpaqueCloudRemap_shader)).WaitForCompletion());
            face.SetTexture("_MainTex", AssetAsyncReferenceManager<Texture>.LoadAsset(commonSkinFaceMask).WaitForCompletion());
            face.SetTexture("_Cloud1Tex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_TiledTextures.texCloudOrganic1_png)).WaitForCompletion());
            face.SetTextureScale("_Cloud1Tex", new Vector2(0.6f, 0.6f));
            face.SetTexture("_Cloud2Tex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common.texCloudWaterFoam2_psd)).WaitForCompletion());
            face.SetVector("_CutoffScroll", new Vector4(-3, -2, -1.5f, 1.5f));
            face.SetColor("_EmissionColor", Color.black);
            face.Inkify();
            return face;
        }
        internal static Material CreateGlowingFaceMaterial(Texture mask, Texture cloud1, Texture remap)
        {
            Material face = new Material(AssetAsyncReferenceManager<Shader>.LoadAsset(new AssetReferenceT<Shader>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Shaders.HGCloudRemap_shader)).WaitForCompletion());
            face.SetTexture("_MainTex", mask);
            face.SetTexture("_Cloud1Tex", cloud1);
            face.SetTexture("_Cloud2Tex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common.texCloudWaterFoam2_psd)).WaitForCompletion());
            face.SetTexture("_RemapTex", remap);
            face.SetVector("_CutoffScroll", new Vector4(-3, -2, -1.5f, 1.5f));
            face.EnableKeyword("USE_CLOUDS");
            face.SetFloat("_InvFade", 0f);
            face.SetFloat("_AlphaBias", 0.25f);
            face.SetInt("_Cull", 2);
            return face;
        }
    }
}
