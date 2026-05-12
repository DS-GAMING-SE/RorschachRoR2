using RorschachMod.Characters.Survivors.Rorschach.Achievements;
using RoR2;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachUnlockables
    {
        public static UnlockableDef characterUnlockableDef = null;
        public static UnlockableDef masterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                RorschachMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(RorschachMasteryAchievement.identifier), null);
            RorschachAssets.masterySkinIcon.LoadAssetAsync<Sprite>().Completed += x => { masterySkinUnlockableDef.achievementIcon = x.Result; };
        }
    }
}
