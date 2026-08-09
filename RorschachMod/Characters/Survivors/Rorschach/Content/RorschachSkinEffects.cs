using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachSkinEffects
    {
        public static Color defaultColor = new Color(1, 0f, 0.05f);
        private static Dictionary<SkinDef, Color> skinVFXColorOverrides = new Dictionary<SkinDef, Color>();

        public static void AddSkinColor(this SkinDef skinDef, Color color)
        {
            skinVFXColorOverrides.Add(skinDef, color);
        }
        public static Color GetSkinColor(CharacterBody characterBody)
        {
            if (characterBody && characterBody.modelLocator && characterBody.modelLocator.modelTransform && 
                characterBody.modelLocator.modelTransform.TryGetComponent<ModelSkinController>(out var skinController) &&
                skinVFXColorOverrides.TryGetValue(skinController.skins[skinController.currentSkinIndex], out var color))
            {
                return color;
            }
            return defaultColor;
        }
        public static Color GetSkinColor(CharacterModel characterModel)
        {
            if (characterModel && characterModel.TryGetComponent<ModelSkinController>(out var skinController) &&
                skinVFXColorOverrides.TryGetValue(skinController.skins[skinController.currentSkinIndex], out var color))
            {
                return color;
            }
            return defaultColor;
        }
        public static Color GetSkinColor(CharacterModel characterModel, int index)
        {
            if (characterModel && characterModel.TryGetComponent<ModelSkinController>(out var skinController) &&
                skinVFXColorOverrides.TryGetValue(skinController.skins[index], out var color))
            {
                return color;
            }
            return defaultColor;
        }
    }
}
