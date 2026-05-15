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
using UnityEngine.AddressableAssets;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachBuffs
    {
        public static BuffDef judgementBuff;
        public static BuffDef specialOnKillBuff;

        public static void Init()
        {
            judgementBuff = Modules.Content.CreateAndAddBuff("bdRorschachJudgement",
                Addressables.LoadAssetAsync<Sprite>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_WarCryOnMultiKill.texWarcryBuffIcon_tif).WaitForCompletion(),
                Color.red,
                true,
                false);
            specialOnKillBuff = Modules.Content.CreateAndAddBuff("bdRorschachSpecialOnKill",
                Addressables.LoadAssetAsync<Sprite>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_Base_Bandit2.texBuffBanditSkullIcon_tif).WaitForCompletion(),
                Color.red,
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
                RegisterLookingGlassBuff(en, judgementBuff, "Rorschach Judgement", $"Powers up your next special skill.");
                RegisterLookingGlassBuff(en, specialOnKillBuff, "Rorschach SpecialOnKill", $"Gain {Modules.Tokens.UtilityText("+"+RorschachStaticValues.specialOnKillBuffMultiplier * 100f+"% movement speed")} and {Modules.Tokens.DamageText("attack speed")}.");
            }
        }

        private static void RegisterLookingGlassBuff(Language lang, BuffDef buff, string name, string description)
        {
            LookingGlassLanguageAPI.SetupToken(lang, $"NAME_{buff.name}", name);
            LookingGlassLanguageAPI.SetupToken(lang, $"DESCRIPTION_{buff.name}", description);
        }
    }
}
