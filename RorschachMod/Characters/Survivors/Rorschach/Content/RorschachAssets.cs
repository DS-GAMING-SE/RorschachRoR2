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

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachAssets
    {
        // particle effects
        public static GameObject swordSwingEffect;
        public static GameObject swordHitImpactEffect;

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
        public static AssetReferenceSprite primaryFlameCanSkillIcon = new AssetReferenceSprite("6f0e7dc80d946bd489940eccc8c92f5b");
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
        public static AssetReferenceT<Mesh> defaultSkinMesh = new AssetReferenceT<Mesh>("822bef3ef7e19554ba24e37bed51d699");
        public static AssetReferenceT<Mesh> defaultSkinGlassMesh = new AssetReferenceT<Mesh>("bcfbbd3f6cd900e488cbb1d96a995783");
        public static AssetReferenceT<Mesh> defaultSkinArmMesh = new AssetReferenceT<Mesh>("73f4e1e3829b7364f886976fe5274efe");
        public static AssetReferenceSprite defaultSkinIcon = new AssetReferenceSprite("e0bd4029a2d0049499dc8c6d68c3716b");
        #endregion
        #region Classic Skin
        public static AssetReferenceT<Material> classicSkinMaterial = new AssetReferenceT<Material>("cd3ae74bb0848124b9c4e4813f731cc6");
        public static AssetReferenceT<Mesh> classicSkinMesh = new AssetReferenceT<Mesh>("5f4832150f25b3041a4ee59b3781a08d");
        public static AssetReferenceSprite classicSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Future Skin
        public static AssetReferenceT<Material> futureSkinMaterial = new AssetReferenceT<Material>("b0f8af76c50b18d44a0102da35b23dc3");
        public static AssetReferenceT<Mesh> futureSkinMesh = new AssetReferenceT<Mesh>("ee1e4bcf8d27acc4593282ff2badeaeb");
        public static AssetReferenceSprite futureSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #region Warframe Skin
        public static AssetReferenceT<Material> warframeSkinMaterial = new AssetReferenceT<Material>("fa40aa64deb77d04da385a554f3de463");
        public static AssetReferenceT<Material> warframeSkinHatMaterial = new AssetReferenceT<Material>("7bd906e6af579f140917fd51aa9d7a6a");
        public static AssetReferenceT<Mesh> warframeSkinMesh = new AssetReferenceT<Mesh>("e337a4fbb4dd5804db25a164153e34a1");
        public static AssetReferenceT<Mesh> warframeSkinHatMesh = new AssetReferenceT<Mesh>("3d5eaa1d2364a08418b036895055447e");
        public static AssetReferenceSprite warframeSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
        #endregion
        #region Items
        public static AssetReferenceT<GameObject> flameCanItemModel = new AssetReferenceT<GameObject>("2142cbcfebb888744983f02b83eb0dba");
        public static AssetReferenceT<GameObject> pipeItemModel = new AssetReferenceT<GameObject>("508bdaea945e3b8498a2d82f4825bc51");
        public static AssetReferenceT<GameObject> cleaverItemModel = new AssetReferenceT<GameObject>("8dce5d1b37999f34faf5a6f53164427b");
        #endregion
        #region VFX
        public static AssetReferenceT<GameObject> projectileGhost = new AssetReferenceT<GameObject>("2b1ae6eb92856db41a261cf5336101dc");

        public static AssetReferenceT<GameObject> projectileExplodeEffect = new AssetReferenceT<GameObject>("05b273758480af74a919e826c7b80a86");
        public static AssetReferenceT<GameObject> swingEffect = new AssetReferenceT<GameObject>("3534552e7829f9842ba3156065afc540");
        public static AssetReferenceT<GameObject> hitEffect = new AssetReferenceT<GameObject>("48eebb9268b618943a4b60bb011fb96d");
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
            RorschachAssets.projectileExplodeEffect.LoadAssetAsync().Completed += delegate (AsyncOperationHandle<GameObject> x)
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
                grappleProjectilePrefab.GetComponent<ProjectileOverlapAttack>().damageCoefficient = 1f;
            };

            RorschachAssets.swingEffect.LoadAssetAsync().Completed += delegate (AsyncOperationHandle<GameObject> x)
            { swordSwingEffect = Asset.CreateEffect(x.Result, 1f, true, ""); };
            RorschachAssets.hitEffect.LoadAssetAsync().Completed += delegate (AsyncOperationHandle<GameObject> x)
            { swordHitImpactEffect = Asset.CreateEffect(x.Result, 1f); };
        }

        private static void CreateBombExplosionEffect(GameObject prefab)
        {
            bombExplosionEffect = Asset.CreateEffect(prefab, 5f, false, "HenryBombExplosion");

            if (!bombExplosionEffect)
                return;

            ShakeEmitter shakeEmitter = bombExplosionEffect.AddComponent<ShakeEmitter>();
            shakeEmitter.amplitudeTimeDecay = true;
            shakeEmitter.duration = 0.5f;
            shakeEmitter.radius = 200f;
            shakeEmitter.scaleShakeRadiusWithLocalScale = false;

            shakeEmitter.wave = new Wave
            {
                amplitude = 1f,
                frequency = 40f,
                cycleOffset = 0f
            };

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
            
            bombImpactExplosion.blastRadius = 12f;
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

            ProjectileController bombController = bombProjectilePrefab.GetComponent<ProjectileController>();
            RorschachAssets.projectileGhost.LoadAssetAsync().Completed += delegate (AsyncOperationHandle<GameObject> x)
            {
                bombController.ghostPrefab = Asset.CreateProjectileGhostPrefab(x.Result);
            };
            
            bombController.startSound = "";

            Content.AddProjectilePrefab(bombProjectilePrefab);
        }
        #endregion projectiles
    }
}
