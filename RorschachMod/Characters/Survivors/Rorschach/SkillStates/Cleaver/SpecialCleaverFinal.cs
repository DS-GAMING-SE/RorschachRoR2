using EntityStates;
using R2API.Networking.Interfaces;
using RoR2;
using RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons;
using RorschachMod.Modules.BaseStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialCleaverFinal : SpecialDefaultFinal
    {
        protected override void Prepare()
        {
            base.Prepare();
            damageCoefficient = RorschachStaticValues.specialCleaverFinalDamageCoefficient;
            damageType |= DamageType.BleedOnHit;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active && characterBody.inventory)
            {
                characterBody.inventory.RemoveItemTemp(ImprovisedWeaponItemDefs.cleaver.itemIndex);
            }
        }
    }
}