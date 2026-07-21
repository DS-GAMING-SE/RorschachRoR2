using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialPipeDash : SpecialDefaultDash
    {
        public override Type grabStateType { get { return typeof(SpecialPipeGrab); } }
    }
}
