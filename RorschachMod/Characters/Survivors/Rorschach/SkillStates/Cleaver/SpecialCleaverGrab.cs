using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RorschachMod.Characters.Survivors.Rorschach.SkillStates
{
    public class SpecialCleaverGrab : SpecialDefaultGrab
    {
        public override Type finalStateType { get { return typeof(SpecialCleaverFinal); } }
        public override Type judgementStateType { get { return typeof(SpecialCleaverJudgement); } }
    }
}
