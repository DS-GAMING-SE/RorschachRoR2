using BepInEx.Configuration;
using LookingGlass;
using LookingGlass.LookingGlassLanguage;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RoR2;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachBuffs
    {
        public static BuffDef boostBuff;

        public static void Init()
        {
            boostBuff = Modules.Content.CreateAndAddBuff("bdRorschachBoost",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/CloakSpeed").iconSprite,
                Color.white,
                false,
                false);

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(LookingGlass.PluginInfo.PLUGIN_GUID))
            {
                RoR2Application.onLoad += LookingGlassSetup;
            }
        }

        private static void LookingGlassSetup()
        {
            if (Language.languagesByName.TryGetValue("en", out RoR2.Language en))
            {
                RegisterLookingGlassBuff(en, boostBuff, "Rorschach Boost", $"Gain <style=cIsUtility>+{RorschachStaticValues.boostArmor} armor</style>. Gain <style=cIsUtility>+{RorschachStaticValues.boostListedSpeedCoefficient * 100}% movement speed</style>.");
            }
        }

        private static void RegisterLookingGlassBuff(Language lang, BuffDef buff, string name, string description)
        {
            LookingGlassLanguageAPI.SetupToken(lang, $"NAME_{buff.name}", name);
            LookingGlassLanguageAPI.SetupToken(lang, $"DESCRIPTION_{buff.name}", description);
        }
    }
}
