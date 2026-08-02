using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace RorschachMod.Characters.Survivors.Rorschach.Components
{
    [RequireComponent(typeof(EffectManagerHelper))]
    internal class SpawnInkDecal : MonoBehaviour
    {
        private EffectManagerHelper emh;
        public bool copyRotation;
        public float scale = 1.2f;
        private void Awake()
        {
            emh = GetComponent<EffectManagerHelper>();
            if (emh) emh.OnEffectActivated += SpawnDecal;
        }

        private void SpawnDecal()
        {
            EffectData effectData = new EffectData
            {
                origin = emh.effectComponent.effectData.origin,
                scale = emh.effectComponent.effectData.scale * scale
            };
            if (copyRotation)
            {
                effectData.rotation = emh.effectComponent.effectData.rotation * Quaternion.FromToRotation(Vector3.forward, Vector3.up);
            }
            EffectManager.SpawnEffect(RorschachAssets.inkDecal, effectData, false);
        }
    }
}
