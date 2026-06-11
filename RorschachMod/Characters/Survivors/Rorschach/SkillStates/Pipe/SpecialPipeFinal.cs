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
    public class SpecialPipeFinal : SpecialDefaultFinal
    {
        protected override void Prepare()
        {
            base.Prepare();
            damageType |= DamageType.Stun1s;
            damageCoefficient = RorschachStaticValues.specialPipeFinalDamageCoefficient;
            hitStopDuration = 0.11f;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active && characterBody.inventory)
            {
                characterBody.inventory.RemoveItemTemp(ImprovisedWeaponItemDefs.pipe.itemIndex);
            }
        }
    }
}