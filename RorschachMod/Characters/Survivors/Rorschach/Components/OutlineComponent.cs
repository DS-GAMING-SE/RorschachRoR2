using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace RorschachMod.Characters.Survivors.Rorschach.Components
{
    public class OutlineComponent : MonoBehaviour
    {
        public static Material material;
        public CharacterModel characterModel;
        public TemporaryOverlayInstance overlay;
        public static int alphaBoostPropertyId = Shader.PropertyToID("_AlphaBoost");
        public float alpha;
        public float targetAlpha;

        private const float FLASH_ALPHABOOST = 2f;
        private const float ALPHA_PER_SECOND = 1.8f;
        private const float SPECIALONKILLBUFF_ALPHABOOST = 1f;

        public void StartFlash()
        {
            if (overlay != null)
            {
                overlay.materialInstance.SetFloat(alphaBoostPropertyId, FLASH_ALPHABOOST);
                alpha = FLASH_ALPHABOOST;
            }
        }

        private void Update()
        {
            if (overlay != null)
            {
                targetAlpha = characterModel.body && characterModel.body.HasBuff(RorschachBuffs.specialOnKillBuff) ? SPECIALONKILLBUFF_ALPHABOOST : 0f;

                alpha = Mathf.MoveTowards(alpha, targetAlpha, Time.deltaTime * ALPHA_PER_SECOND);

                overlay.materialInstance.SetFloat(alphaBoostPropertyId, alpha);
            }
        }

        private void Awake()
        {
            characterModel = GetComponent<CharacterModel>();
        }

        private void OnEnable()
        {
            overlay = TemporaryOverlayManager.AddOverlay(gameObject);
            overlay.duration = float.PositiveInfinity;
            overlay.originalMaterial = material;
            overlay.AddToCharacterModel(characterModel);
            if (characterModel.TryGetComponent<ModelSkinController>(out var skin))
            {
                skin.onSkinApplied += OnSkinChanged;
            }
        }

        private void OnSkinChanged(int index)
        {
            if (overlay != null) overlay.materialInstance.SetColor("_TintColor", RorschachSkinEffects.GetSkinColor(characterModel, index));
        }

        private void OnDisable()
        {
            overlay?.CleanupEffect();
            overlay = null;
        }
    }
}
