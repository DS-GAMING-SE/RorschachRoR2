using RoR2;
using RorschachMod.Characters.Survivors.Rorschach.ImprovisedWeapons;
using UnityEngine;
using static RorschachMod.Characters.Survivors.Rorschach.RorschachStaticValues;

namespace RorschachMod.Characters.Survivors.Rorschach.Components
{
    public class ImprovisedWeaponComponent : MonoBehaviour, IOnKilledOtherServerReceiver
    {
        public float progress = startProgress;
        public const float maxProgress = 100f;
        public const float startProgress = 40f;

        public CharacterBody characterBody;

        private void Awake()
        {
            characterBody = GetComponent<CharacterBody>();
        }

        public void OnKilledOtherServer(DamageReport damageReport)
        {
            if (damageReport.victimBody)
            {
                float addProgress = passiveProgressOnKill;
                addProgress *= damageReport.victimBody.isElite ? passiveProgressEliteMultiplier : 1;
                addProgress *= damageReport.victimBody.isChampion ? passiveProgressChampionMultiplier : 1;
                addProgress *= damageReport.victimBody.isBoss ? passiveProgressBossMultiplier : 1;

                progress += addProgress;
                
                if (progress >= maxProgress)
                {
                    progress -= maxProgress;
                    ImprovisedWeaponManager.DropItem(damageReport.victimBody.transform.position, damageReport.victimBody.inputBank ? damageReport.victimBody.inputBank.aimDirection : damageReport.victimBody.transform.forward);
                }
            }
        }
    }
}