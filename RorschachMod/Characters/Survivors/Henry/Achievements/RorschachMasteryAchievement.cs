using RoR2;
using RorschachMod.Modules.Achievements;

namespace RorschachMod.Characters.Survivors.Rorschach.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, 10, null)]
    public class RorschachMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = RorschachSurvivor.RORSCHACH_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = RorschachSurvivor.RORSCHACH_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => RorschachSurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}