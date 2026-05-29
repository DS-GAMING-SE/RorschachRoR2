using RorschachMod.Characters.Survivors.Rorschach.SkillStates.FlameCan;
using RorschachMod.Characters.Survivors.Rorschach.SkillStates;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public static class RorschachStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(PrimaryDefault));
            Modules.Content.AddEntityState(typeof(PrimaryPipe));
            Modules.Content.AddEntityState(typeof(PrimaryCleaver));
            Modules.Content.AddEntityState(typeof(PrimaryFlameCan));

            Modules.Content.AddEntityState(typeof(SecondaryDefaultDash));
            Modules.Content.AddEntityState(typeof(SecondaryDefaultCharge));
            Modules.Content.AddEntityState(typeof(SecondaryDefaultChargedAttack));
            Modules.Content.AddEntityState(typeof(SecondaryCleaverDash));
            Modules.Content.AddEntityState(typeof(SecondaryCleaverCharge));
            Modules.Content.AddEntityState(typeof(SecondaryCleaverChargedAttack));
            Modules.Content.AddEntityState(typeof(SecondaryPipeDash));
            Modules.Content.AddEntityState(typeof(SecondaryPipeCharge));
            Modules.Content.AddEntityState(typeof(SecondaryPipeChargedAttack));

            Modules.Content.AddEntityState(typeof(UtilityHooking));
            Modules.Content.AddEntityState(typeof(UtilityKnee));
            Modules.Content.AddEntityState(typeof(UtilityGrappleFly));
            Modules.Content.AddEntityState(typeof(UtilityGrapplePull));

            Modules.Content.AddEntityState(typeof(SpecialFlameCan));
        }
    }
}
