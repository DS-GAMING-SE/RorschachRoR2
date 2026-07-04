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

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachAssets
    {
        // common stuff
        public static GameObject inkDecal;
        public static Material ink;
        public static Material inkDot;
        public static Material inkTrail;
        public static Material inkStreak;
        public static Material inkVerticalGradient;

        public static Material whiteInkSplat;
        public static Material cleaverCut;

        public static Material fire;
        public static Material fireOut;

        // particle effects
        public static GameObject swordSwingEffect;
        public static GameObject meleeHitEffect;
        public static GameObject meleeHitPipeEffect;
        public static GameObject meleeHitCleaverEffect;
        public static GameObject meleeHitDirectionalEffect;

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
        public static AssetReferenceT<Avatar> animatorAvatar = new AssetReferenceT<Avatar>("3f55d8352b2212743b809db2974cdd5e"); // commented out of where its used
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
        public static AssetReferenceSprite specialFlameCanSkillIcon = new AssetReferenceSprite("6f0e7dc80d946bd489940eccc8c92f5b");
        public static AssetReferenceSprite specialPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite specialCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");
        #endregion
        #region Skins
        #region Default Skin
        public static AssetReferenceT<Material> defaultSkinMaterial = new AssetReferenceT<Material>("83e8838e42f0e0c44bfa65f58572c81e");
        public static AssetReferenceT<Material> defaultSkinArmMaterial = new AssetReferenceT<Material>("3c6c7959e383556429c3faa40e7783ff");
        public static AssetReferenceT<Mesh> defaultSkinMesh = new AssetReferenceT<Mesh>("33949338006f1a74192571a62910f38c");
        public static AssetReferenceT<Mesh> defaultSkinGlassMesh = new AssetReferenceT<Mesh>("4dcc2256131a72247a6f063f380aa674");
        public static AssetReferenceT<Mesh> defaultSkinArmMesh = new AssetReferenceT<Mesh>("e621f8f419d8ad04d97aa0da34f43bcc");
        public static AssetReferenceSprite defaultSkinIcon = new AssetReferenceSprite("e0bd4029a2d0049499dc8c6d68c3716b");
        #endregion
        #region Classic Skin
        public static AssetReferenceT<Material> classicSkinMaterial = new AssetReferenceT<Material>("cd3ae74bb0848124b9c4e4813f731cc6");
        public static AssetReferenceT<Mesh> classicSkinMesh = new AssetReferenceT<Mesh>("3bdce6993e2b16e46a0fd25fea45cfa5");
        public static AssetReferenceSprite classicSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Future Skin
        public static AssetReferenceT<Material> futureSkinMaterial = new AssetReferenceT<Material>("b0f8af76c50b18d44a0102da35b23dc3");
        public static AssetReferenceT<Mesh> futureSkinMesh = new AssetReferenceT<Mesh>("7ec157145d7a694428a80d11e7f9dde2");
        public static AssetReferenceSprite futureSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Warframe Skin
        public static AssetReferenceT<Material> warframeSkinMaterial = new AssetReferenceT<Material>("fa40aa64deb77d04da385a554f3de463");
        public static AssetReferenceT<Material> warframeSkinHatMaterial = new AssetReferenceT<Material>("7bd906e6af579f140917fd51aa9d7a6a");
        public static AssetReferenceT<Mesh> warframeSkinMesh = new AssetReferenceT<Mesh>("0b11538b5dababe40a1b6984b770f81f");
        public static AssetReferenceT<Mesh> warframeSkinHatMesh = new AssetReferenceT<Mesh>("7c1947acc115f8d43ac7153c0a8f4f82");
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
        public static AssetReferenceT<GameObject> flameCanSpecialProjectileGhost = new AssetReferenceT<GameObject>("fc0da765d454d7e4f86895ef76de0172");
        public static AssetReferenceT<GameObject> projectileExplodeEffect = new AssetReferenceT<GameObject>("05b273758480af74a919e826c7b80a86");

        public static AssetReferenceT<GameObject> swingEffect = new AssetReferenceT<GameObject>("3534552e7829f9842ba3156065afc540");

        public static AssetReferenceT<GameObject> meleeHit = new AssetReferenceT<GameObject>("9dc987e0064626645be61b151c52fb04");
        public static AssetReferenceT<GameObject> meleeHitPipe = new AssetReferenceT<GameObject>("8c1b45118d0a20143b72f7cee5285454");
        public static AssetReferenceT<GameObject> meleeHitCleaver = new AssetReferenceT<GameObject>("68ddfb3f59849ce4196d765297867f0a");

        public static AssetReferenceT<GameObject> meleeHitDirectional = new AssetReferenceT<GameObject>("974a977cf126bc14084058dfe44b53c6");
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
            GameObject solusScorcherVFXToDissect = AssetAsyncReferenceManager<GameObject>.LoadAsset(new AssetReferenceT<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.TankerAccelerantPuddleBodyProjectileGhost_prefab)).WaitForCompletion();
            inkDecal = solusScorcherVFXToDissect.transform.GetChild(0).gameObject;
            ink = new Material(solusScorcherVFXToDissect.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial).Inkify();
            ink.name = "matRorschachInk";
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

            whiteInkSplat = new Material(AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark2Generic_mat)).WaitForCompletion());
            whiteInkSplat.SetFloat("_InvFade", 0.4f);
            whiteInkSplat.SetFloat("_DepthOffset", -2f);
            whiteInkSplat.SetInt("_ZTest", 8);
            whiteInkSplat.name = "matRorschachWhiteInkSplat";
            cleaverCut = new Material(whiteInkSplat);
            cleaverCut.name = "matRorschachCleaverCut";
            whiteInkSplat.SetTexture("_MainTex", Addressables.LoadAssetAsync<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.texOmniShockwave3Mask_png).WaitForCompletion());

            fire = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matMageFlamethrower_mat)).WaitForCompletion();
            fireOut = AssetAsyncReferenceManager<Material>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.matOmniHitspark1_mat)).WaitForCompletion();
            #endregion

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
        }

        private static void CreateBombExplosionEffect(GameObject prefab)
        {
            bombExplosionEffect = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_VFX.OmniExplosionVFX_prefab).WaitForCompletion(), "RorschachFlameCanExplosionEffect");//Asset.CreateEffect(prefab, 5f, false, "HenryBombExplosion");
            bombExplosionEffect.GetComponent<DestroyOnTimer>().duration = 3.5f;
            AddInkDecal(bombExplosionEffect, 0.9f, 3.5f, Vector3.zero);

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

        private static void AddInkDecal(GameObject prefab, float size, float duration, Vector3 positionOffset)
        {
            var decal = GameObject.Instantiate(inkDecal, prefab.transform);
            decal.transform.localScale = new Vector3(size, size / 2, size);
            decal.transform.SetPositionAndRotation(positionOffset, Quaternion.identity);
            decal.GetComponent<AnimateShaderAlpha>().timeMax = duration;
        }

        private static Material Inkify(this Material material)
        {
            material.SetTexture("_RemapTex", AssetAsyncReferenceManager<Texture>.LoadAsset(new AssetReferenceT<Texture>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Common_ColorRamps.texRampMaulingRockHit_png)).WaitForCompletion());
            material.SetColor("_TintColor", new Color(0.06f, 0.06f, 0.06f));
            return material;
        }
    }
}
