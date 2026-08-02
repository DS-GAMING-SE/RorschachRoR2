using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RorschachMod.Characters.Survivors.Rorschach.Components
{
    public class RorschachSpecialPostProcessController : MonoBehaviour
    {
        // Not networked for spectators due to object being passed around states in authority. Low priority though
        public PostProcessVolume ppVolume;
        public LocalCameraEffect localCameraEffect;
        private EffectManagerHelper emh;
        private float _weight;
        public float weight { get { return _weight; } 
            set 
            { 
                _weight = value;
                ppVolume.weight = value;
                PostProcessVolume.DispatchVolumeSettingsChangedEvent();
            } 
        }
        public float targetWeight;
        public float weightSpeed;
        // This effect was going to look so much cooler but apparently lum vs sat curves are bugged in this unity version and cannot be removed, even after..
        // .. deleting the post processing volume
        public static RorschachSpecialPostProcessController GetPooledPostProcessVolume(CharacterBody target)
        {
            RorschachSpecialPostProcessController postProcess = EffectManager.GetAndActivatePooledEffect(RorschachAssets.specialPostProcessVolumeEffect, Vector3.zero, Quaternion.identity).GetComponent<RorschachSpecialPostProcessController>();
            postProcess.localCameraEffect.targetCharacter = target.gameObject;
            return postProcess;
        }
        private void Start()
        {
            emh = GetComponent<EffectManagerHelper>();
        }

        private void OnEnable()
        {
            targetWeight = 0f;
            weight = 0f;
        }

        public void LerpToWeight(float target, float duration)
        {
            targetWeight = target;
            weightSpeed = Mathf.Abs(weight - targetWeight) / duration;
        }

        public void BeginFade()
        {
            targetWeight = 0f;
            weightSpeed = 0.1f;
        }

        public void CancelFade()
        {
            targetWeight = weight;
        }

        private void Update()
        {
            weight = Mathf.MoveTowards(weight, targetWeight, weightSpeed * Time.deltaTime);
            if (targetWeight == 0 && weight == 0)
            {
                if (emh)
                {
                    emh.ReturnToPool();
                }
                else
                {
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
    }
}
