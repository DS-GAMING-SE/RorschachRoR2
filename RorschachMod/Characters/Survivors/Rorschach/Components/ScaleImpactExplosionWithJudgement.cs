using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using RoR2.Projectile;

namespace RorschachMod.Characters.Survivors.Rorschach.Components
{
    [RequireComponent(typeof(ProjectileImpactExplosion))]
    [RequireComponent(typeof(ProjectileDamage))]
    public class ScaleImpactExplosionWithJudgement : MonoBehaviour
    {
        public ProjectileImpactExplosion explosion;
        public ProjectileDamage damage;

        private void Awake()
        {
            explosion = GetComponent<ProjectileImpactExplosion>();
            damage = GetComponent<ProjectileDamage>();
        }
        private void Start()
        {
            explosion.blastRadius = Mathf.Lerp(RorschachStaticValues.specialFlameCanMinExplosionRadius, RorschachStaticValues.specialFlameCanMaxExplosionRadius, ((float)damage.damageType.ReadJudgementStacks()) / RorschachStaticValues.judgementBuffCap);
        }

    }
}
