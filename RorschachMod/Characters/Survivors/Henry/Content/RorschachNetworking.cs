using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
using UnityEngine.AddressableAssets;
using RorschachMod.Characters.Survivors.Rorschach.SkillStates;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public class NetworkJudgement : INetMessage
    {
        NetworkInstanceId netId;

        public NetworkJudgement()
        {

        }

        public NetworkJudgement(NetworkInstanceId netId)
        {
            this.netId = netId;
        }

        public void OnReceived()
        {
            GameObject gameObject = Util.FindNetworkObject(netId);
            if (!gameObject || !gameObject.TryGetComponent<CharacterBody>(out var body)) { return; }
            SecondaryDefaultChargedAttack.AddJudgementStack(body);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
        }

        public void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
        }
    }
}
