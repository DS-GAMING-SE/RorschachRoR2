using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RorschachMod.Characters.Survivors.Rorschach.Components
{
    public class ReturnToPoolOnUnseen : MonoBehaviour
    {
        private EffectManagerHelper emh;
        private Renderer renderer;
        private void Start()
        {
            emh = GetComponent<EffectManagerHelper>();
            renderer = GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            if (renderer && !renderer.isVisible)
            {
                emh.ReturnToPool();
            }
        }
    }
}
