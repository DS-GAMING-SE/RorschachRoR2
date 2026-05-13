using RorschachMod.Modules;
using RorschachMod.Characters.Survivors.Rorschach.Achievements;
using LookingGlass.LookingGlassLanguage;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachTokens
    {
        public static void Init()
        {
            AddRorschachTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Rorschach.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddRorschachTokens()
        {
            string prefix = RorschachSurvivor.RORSCHACH_PREFIX;

            string desc = "Rorschach is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine
             + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine
             + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine
             + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, ?????.";
            string outroFailure = "..and so he vanished, ?????.";

            Language.Add(prefix + "NAME", "Rorschach");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "?????");
            Language.Add(prefix + "LORE", "sample lore");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "CLASSIC_SKIN_NAME", "Classic");
            Language.Add(prefix + "FUTURE_SKIN_NAME", "Future");
            Language.Add(prefix + "WARFRAME_SKIN_NAME", "Warframe");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", $"Improvised Weaponry");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", $"Killing large or elite monsters has a chance of dropping one of three {Tokens.UtilityText("Improvised Weapons")}, a {HedgehogUtils.Helpers.wipIcon + " " + Tokens.DamageText("flame can")}, {HedgehogUtils.Helpers.wipIcon + " " + Tokens.DamageText("pipe")}, or {HedgehogUtils.Helpers.wipIcon + " " + Tokens.DamageText("cleaver")}, which temporarily upgrade some of your skills. {Tokens.UtilityText("Unaffected by luck")}.");
            Language.Add(prefix + "PASSIVE_IMPROVISED_WEAPON_KEYWORD", "<style=cKeywordName>Improvised Weapon</style><style=cSub>A unique temporary item that replaces some of Rorschach's skills.</style>");

            #region Items
            Language.Add(prefix + "IMPROVISED_WEAPON_FLAME_CAN", $"{HedgehogUtils.Helpers.wipIcon} Flame Can");
            Language.Add(prefix + "IMPROVISED_WEAPON_FLAME_CAN_PICKUP", $"A long-range weapon that sprays fire at enemies.");
            Language.Add(prefix + "IMPROVISED_WEAPON_FLAME_CAN_DESC", $"A long-range weapon that sprays fire at enemies.");

            Language.Add(prefix + "IMPROVISED_WEAPON_PIPE", $"{HedgehogUtils.Helpers.wipIcon} Pipe");
            Language.Add(prefix + "IMPROVISED_WEAPON_PIPE_PICKUP", $"A heavy weapon with high damage and stuns.");
            Language.Add(prefix + "IMPROVISED_WEAPON_PIPE_DESC", $"A heavy weapon with high damage and stuns.");

            Language.Add(prefix + "IMPROVISED_WEAPON_CLEAVER", $"{HedgehogUtils.Helpers.wipIcon} Cleaver");
            Language.Add(prefix + "IMPROVISED_WEAPON_CLEAVER_PICKUP", $"A fast-attacking weapon that increases bleed chance.");
            Language.Add(prefix + "IMPROVISED_WEAPON_CLEAVER_DESC", $"A fast-attacking weapon that increases bleed chance.");
            #endregion
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_DEFAULT_NAME", $"Black and White");
            Language.Add(prefix + "PRIMARY_DEFAULT_DESCRIPTION", $"Swing forward for <style=cIsDamage>{100f * RorschachStaticValues.primaryDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_DEFAULT_NAME", $"{HedgehogUtils.Helpers.wipIcon} Judgement Fists");
            Language.Add(prefix + "SECONDARY_DEFAULT_DESCRIPTION", $"Dash forward, dealing {Tokens.DamageValueText(0)}. Hold the skill to charge up a strong punch, dealing {Tokens.DamageValueText(RorschachStaticValues.secondaryChargeMinDamageCoefficient, RorschachStaticValues.secondaryChargeMaxDamageCoefficient)}. Landing a fully charged punch grants {Tokens.RedText("Judgement")}.");
            Language.Add(prefix + "JUDGEMENT_KEYWORD", "<style=cKeywordName>Judgement</style><style=cSub>A buff that is consumed to strengthen your special skill. Stacks up to 4 times.</style>");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_DEFAULT_NAME", $"{HedgehogUtils.Helpers.wipIcon} Grappling Hook");
            Language.Add(prefix + "UTILITY_DEFAULT_DESCRIPTION", $"{Tokens.DamageText("Stunning.")} Fire your grappling hook, dealing {Tokens.DamageValueText(RorschachStaticValues.utilityDamageCoefficient)} damage and {Tokens.UtilityText("pulling")} you to the target.");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_DEFAULT_NAME", $"{HedgehogUtils.Helpers.wipIcon} Uncompromising");
            Language.Add(prefix + "SPECIAL_DEFAULT_DESCRIPTION", $"Grab the target and perform a double axe handle, dealing {Tokens.DamageValueText(0)}. Kills grant {Tokens.UtilityText("BUFFNAME")}. {Tokens.RedText("Judgement")} gives an extra hit of {Tokens.DamageValueText(0)} per stack. Using this move {Tokens.RedText("consumes")} any {Tokens.UtilityText("Improvised Weapons")}.");
            Language.Add(prefix + "SPECIAL_ON_KILL_BUFF_KEYWORD", $"<style=cKeywordName>BUFFNAME</style><style=cSub>Increases attack speed and movement speed by {"0%"} for {"3"}s. Judgement increases this duration.</style>");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(RorschachMasteryAchievement.identifier), "Rorschach: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(RorschachMasteryAchievement.identifier), "As Rorschach, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
