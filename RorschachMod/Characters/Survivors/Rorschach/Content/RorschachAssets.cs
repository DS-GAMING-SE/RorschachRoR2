using RorschachMod.Modules;
using RoR2;
using RoR2.Projectile;
using RoR2.ContentManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using R2API;

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
        public static GameObject bombProjectilePrefab;

        #region AssetGUIDs
        public static AssetReferenceT<GameObject> characterModel = new AssetReferenceT<GameObject>("a32fb17ad7dfd7b4f8b893feaf7d7512");
        public static AssetReferenceT<GameObject> displayPrefab = new AssetReferenceT<GameObject>("9552880169ac54248bdf012a5175ef46");

        public static AssetReferenceT<RuntimeAnimatorController> animator = new AssetReferenceT<RuntimeAnimatorController>("f1a82152954bc974895f8ff9f035f84f");
        public static AssetReferenceT<RuntimeAnimatorController> displayAnimator = new AssetReferenceT<RuntimeAnimatorController>("4397b394b0996d44ba60f0f7769a33d1");
        public static AssetReferenceT<Avatar> animatorAvatar = new AssetReferenceT<Avatar>("3f55d8352b2212743b809db2974cdd5e");
        #region Icons
        public static AssetReferenceTexture characterIcon = new AssetReferenceTexture("33c89ac909113894280a0cfd14c99f2e");

        public static AssetReferenceSprite passiveSkillIcon = new AssetReferenceSprite("977438518be9ff545a06824ddf881e15");

        public static AssetReferenceSprite primarySkillIcon = new AssetReferenceSprite("b4d4d8a7648b8c04393ee5d064218886");
        public static AssetReferenceSprite primaryFlameCanSkillIcon = new AssetReferenceSprite("6f0e7dc80d946bd489940eccc8c92f5b");
        public static AssetReferenceSprite primaryPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite primaryCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");

        public static AssetReferenceSprite secondarySkillIcon = new AssetReferenceSprite("b4d4d8a7648b8c04393ee5d064218886");
        public static AssetReferenceSprite secondaryPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite secondaryCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");

        public static AssetReferenceSprite utilitySkillIcon = new AssetReferenceSprite("0f5783e971afb924daa22df12d6c1325");

        public static AssetReferenceSprite specialSkillIcon = new AssetReferenceSprite("b4d4d8a7648b8c04393ee5d064218886");
        public static AssetReferenceSprite specialFlameCanSkillIcon = new AssetReferenceSprite("6f0e7dc80d946bd489940eccc8c92f5b");
        public static AssetReferenceSprite specialPipeSkillIcon = new AssetReferenceSprite("1872c188be0e7fd4397142cd14cc8863");
        public static AssetReferenceSprite specialCleaverSkillIcon = new AssetReferenceSprite("d79f886258b3bb74a90bd0d43ee60854");
        #endregion
        #region Skins
        #region Default Skin
        public static AssetReferenceT<Material> defaultSkinMaterial = new AssetReferenceT<Material>("14d6d121af8aca345b797120c5f6331b");
        public static AssetReferenceT<Mesh> defaultSkinMesh = new AssetReferenceT<Mesh>("fee9d8e08c8c818498f7613f868f2e34");
        public static AssetReferenceT<Mesh> defaultSkinSwordMesh = new AssetReferenceT<Mesh>("06ba2d7b66b5a7c47ae54f402cb9e132");
        public static AssetReferenceT<Mesh> defaultSkinGunMesh = new AssetReferenceT<Mesh>("95bb770c878495b4296296a4b09a6034");
        public static AssetReferenceSprite defaultSkinIcon = new AssetReferenceSprite("e0bd4029a2d0049499dc8c6d68c3716b");
        #endregion
        #region Classic Skin
        public static AssetReferenceT<Material> classicSkinMaterial = new AssetReferenceT<Material>("507751368e16e18409e1c93f8022eb8c");
        public static AssetReferenceT<Mesh> classicSkinMesh = new AssetReferenceT<Mesh>("e49dd36434c00024985424af2da4cedd");
        public static AssetReferenceT<Mesh> classicSkinSwordMesh = new AssetReferenceT<Mesh>("7a3e4cd720806a248b00c548032956cd");
        public static AssetReferenceSprite classicSkinIcon = new AssetReferenceSprite("b6f91ba019353654992c4a536e207a87");
        #endregion
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
            damage.damageType.AddModdedDamageType(RorschachDamageTypes.specialOnKillBuff);

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
