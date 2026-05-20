using RorschachMod.Characters.Survivors.Rorschach.Achievements;
using RoR2;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachUnlockables
    {
        public static UnlockableDef masterySkinUnlockableDef = null;
        public static UnlockableDef grandMasterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                RorschachMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(RorschachMasteryAchievement.identifier), null);
            RorschachAssets.classicSkinIcon.LoadAssetAsync<Sprite>().Completed += x => { masterySkinUnlockableDef.achievementIcon = x.Result; };
            grandMasterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                RorschachGrandMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(RorschachGrandMasteryAchievement.identifier), null);
            RorschachAssets.defaultSkinIcon.LoadAssetAsync<Sprite>().Completed += x => { grandMasterySkinUnlockableDef.achievementIcon = x.Result; };
        }
    }
}
