using RorschachMod.Characters.Survivors.Rorschach.SkillStates.FlameCan;
using RorschachMod.Characters.Survivors.Rorschach.SkillStates;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(PrimaryDefault));

            Modules.Content.AddEntityState(typeof(Shoot));
            Modules.Content.AddEntityState(typeof(SecondaryDefaultChargedAttack));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(SpecialFlameCan));
        }
    }
}
