using RorschachMod.Modules;
using RorschachMod.Characters.Survivors.Rorschach.Achievements;
using LookingGlass.LookingGlassLanguage;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static RorschachMod.Characters.Survivors.Rorschach.RorschachStaticValues;

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
            string flameCanDesc = "A long-range weapon that sprays fire at enemies.";
            string pipeDesc = "A heavy weapon with high damage and stuns.";
            string cleaverDesc = "A fast-attacking weapon with a chance to bleed on hit.";
            Language.Add(prefix + "PASSIVE_NAME", $"Improvised Weaponry");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", $"Kills will occasionally drop one of three {Tokens.UtilityText("Improvised Weapons")} which temporarily upgrade some of your skills. {Tokens.UtilityText("Unaffected by luck")}.");
            Language.Add(prefix + "PASSIVE_IMPROVISED_WEAPON_KEYWORD", "<style=cKeywordName>Improvised Weapon</style><style=cSub>A unique temporary item that replaces some of Rorschach's skills.</style>");
            Language.Add(prefix + "PASSIVE_FLAME_CAN_KEYWORD", $"{Tokens.KeywordText("Flame Can", flameCanDesc)}");
            Language.Add(prefix + "PASSIVE_PIPE_KEYWORD", $"{Tokens.KeywordText("Pipe", pipeDesc)}");
            Language.Add(prefix + "PASSIVE_CLEAVER_KEYWORD", $"{Tokens.KeywordText("Cleaver", cleaverDesc)}");
            #region Items
            Language.Add(prefix + "IMPROVISED_WEAPON_FLAME_CAN", $"Flame Can");
            Language.Add(prefix + "IMPROVISED_WEAPON_FLAME_CAN_PICKUP", flameCanDesc);
            Language.Add(prefix + "IMPROVISED_WEAPON_FLAME_CAN_DESC", flameCanDesc);

            Language.Add(prefix + "IMPROVISED_WEAPON_PIPE", $"{HedgehogUtils.Helpers.wipIcon} Pipe");
            Language.Add(prefix + "IMPROVISED_WEAPON_PIPE_PICKUP", pipeDesc);
            Language.Add(prefix + "IMPROVISED_WEAPON_PIPE_DESC", pipeDesc);

            Language.Add(prefix + "IMPROVISED_WEAPON_CLEAVER", $"{HedgehogUtils.Helpers.wipIcon} Cleaver");
            Language.Add(prefix + "IMPROVISED_WEAPON_CLEAVER_PICKUP", cleaverDesc);
            Language.Add(prefix + "IMPROVISED_WEAPON_CLEAVER_DESC", cleaverDesc);
            #endregion
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_DEFAULT_NAME", $"Black and White");
            Language.Add(prefix + "PRIMARY_DEFAULT_DESCRIPTION", $"Swing forward for <style=cIsDamage>{100f * primaryDefaultDamageCoefficient}% damage</style>.");

            Language.Add(prefix + "PRIMARY_FLAME_CAN_NAME", $"Makeshift Flamethrower");
            Language.Add(prefix + "PRIMARY_FLAME_CAN_DESCRIPTION", $"{Tokens.DamageText("Ignite")}. Burn all enemies in front of you for {Tokens.DamageValueText(primaryFlameCanDamagePerSecond)+Tokens.DamageText(" per second")}.");

            Language.Add(prefix + "PRIMARY_PIPE_NAME", $"Bludgeon");
            Language.Add(prefix + "PRIMARY_PIPE_DESCRIPTION", $"Swing forward for <style=cIsDamage>{100f * primaryPipeDamageCoefficient}% damage</style>.");

            Language.Add(prefix + "PRIMARY_CLEAVER_NAME", $"Cleave");
            Language.Add(prefix + "PRIMARY_CLEAVER_DESCRIPTION", $"Swing forward for <style=cIsDamage>{100f * primaryCleaverDamageCoefficient}% damage</style> and a {Tokens.DamageText($"{primaryCleaverBleedChance}%")} chance to {Tokens.DamageText("bleed")}.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_DEFAULT_NAME", $"Judgement Fists");
            Language.Add(prefix + "SECONDARY_DEFAULT_DESCRIPTION", $"Dash forward, dealing {Tokens.DamageValueText(secondaryDashDamageCoefficient)}. Hold the skill to charge up a strong punch, dealing {Tokens.DamageValueText(secondaryChargeMinDamageCoefficient, secondaryChargeMaxDamageCoefficient)}. Landing a fully charged punch grants {Tokens.RedText("Judgement")}.");
            Language.Add(prefix + "JUDGEMENT_KEYWORD", "<style=cKeywordName>Judgement</style><style=cSub>A buff that is consumed to strengthen your special skill. Stacks up to 4 times.</style>");

            Language.Add(prefix + "SECONDARY_PIPE_NAME", $"Back Alley Beating");
            Language.Add(prefix + "SECONDARY_PIPE_DESCRIPTION", $"Dash forward, dealing {Tokens.DamageValueText(secondaryDashDamageCoefficient)}. Hold the skill to charge up a strong swing, dealing {Tokens.DamageValueText(secondaryPipeChargeMinDamageCoefficient, secondaryPipeChargeMaxDamageCoefficient)}. Landing a fully charged swing {Tokens.UtilityText("launches enemies")} and grants {Tokens.RedText("Judgement")}.");

            Language.Add(prefix + "SECONDARY_CLEAVER_NAME", $"Cleaver Hemorrhage");
            Language.Add(prefix + "SECONDARY_CLEAVER_DESCRIPTION", $"Dash forward, dealing {Tokens.DamageValueText(secondaryDashDamageCoefficient)}. Hold the skill to charge up a strong slash, dealing {Tokens.DamageValueText(secondaryCleaverChargeMinDamageCoefficient, secondaryCleaverChargeMaxDamageCoefficient)}. Landing a fully charged slash {Tokens.DamageText("bleeds enemies")} and grants {Tokens.RedText("Judgement")}.");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_DEFAULT_NAME", $"Grappling Hook");
            Language.Add(prefix + "UTILITY_DEFAULT_DESCRIPTION", $"{Tokens.DamageText("Stunning")}. Fire your grappling hook, dealing {Tokens.DamageValueText(utilityGrappleDamageCoefficient)} damage and {Tokens.UtilityText("pulling")} you to the target. Upon reaching an enemy, strike them with your knee dealing {Tokens.DamageValueText(RorschachStaticValues.utilityKneeDamageCoefficient)}.");
            #endregion

            #region Special
            string specialOnKillBuffName = "BUFFNAME";
            Language.Add(prefix + "SPECIAL_DEFAULT_NAME", $"{HedgehogUtils.Helpers.wipIcon} Uncompromising");
            Language.Add(prefix + "SPECIAL_DEFAULT_DESCRIPTION", $"Grab the target and perform a double axe handle, dealing {Tokens.DamageValueText(specialFinalDamageCoefficient)}. Kills grant {Tokens.UtilityText(specialOnKillBuffName)}. {Tokens.RedText("Judgement")} gives an extra hit of {Tokens.DamageValueText(specialJudgementDamageCoefficient)} per stack. Using this move {Tokens.RedText("consumes")} any {Tokens.UtilityText("Improvised Weapons")}.");
            Language.Add(prefix + "SPECIAL_ON_KILL_BUFF_KEYWORD", $"<style=cKeywordName>BUFFNAME</style><style=cSub>Increases attack speed and movement speed by {specialOnKillBuffMultiplier*100f}% for 3s. Judgement increases this duration.</style>");

            Language.Add(prefix + "SPECIAL_FLAME_CAN_NAME", $"Homemade Explosive");
            Language.Add(prefix + "SPECIAL_FLAME_CAN_DESCRIPTION", $"{Tokens.DamageText("Ignite")}. Prime your flame can to explode before throwing it forward, dealing {Tokens.DamageValueText(specialFlameCanDamageCoefficient)}. Kills grant {Tokens.UtilityText(specialOnKillBuffName)}. {Tokens.RedText("Judgement")} gives {Tokens.DamageText($"{specialFlameCanJudgementDamageMultiplier*100f}% increased damage")} and {Tokens.UtilityText($"{specialFlameCanJudgementExplosionRadiusMultiplier * 100f}% increased explosion radius")} per stack. Using this move {Tokens.RedText("consumes")} your {Tokens.DamageText("flame can")}.");

            Language.Add(prefix + "SPECIAL_PIPE_NAME", $"{HedgehogUtils.Helpers.wipIcon} Final Verdict");
            Language.Add(prefix + "SPECIAL_PIPE_DESCRIPTION", $"Grab the target and stab the pipe into them, dealing {Tokens.DamageValueText(specialPipeFinalDamageCoefficient)}. Kills grant {Tokens.UtilityText(specialOnKillBuffName)}. {Tokens.RedText("Judgement")} gives an extra hit of {Tokens.DamageValueText(specialPipeJudgementDamageCoefficient)} per stack. Using this move {Tokens.RedText("consumes")} your {Tokens.UtilityText("pipe")}.");

            Language.Add(prefix + "SPECIAL_CLEAVER_NAME", $"{HedgehogUtils.Helpers.wipIcon} Butchering End");
            Language.Add(prefix + "SPECIAL_CLEAVER_DESCRIPTION", $"Grab the target and embed the cleaver into them, dealing {Tokens.DamageValueText(specialCleaverFinalDamageCoefficient)} and inflicting {Tokens.DamageText($"{specialCleaverFinalBleedStacks} bleeds")}. Kills grant {Tokens.UtilityText(specialOnKillBuffName)}. {Tokens.RedText("Judgement")} gives an extra hit of {Tokens.DamageValueText(specialCleaverJudgementDamageCoefficient)} and {Tokens.DamageText("bleed")} per stack. Using this move {Tokens.RedText("consumes")} your {Tokens.UtilityText("cleaver")}.");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(RorschachMasteryAchievement.identifier), "Rorschach: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(RorschachMasteryAchievement.identifier), "As Rorschach, beat the game or obliterate on Monsoon.");

            Language.Add(Tokens.GetAchievementNameToken(RorschachGrandMasteryAchievement.identifier), "Rorschach: Grand Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(RorschachGrandMasteryAchievement.identifier), "As Rorschach, beat the game or obliterate on Typhoon or Eclipse.");
            #endregion
        }
    }
}
