using System;
using Modules.Content.Player;
using UnityEngine;

namespace Modules.Content.Kill_Zone
{
    [RequireComponent(typeof(BoxCollider))]
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerEvents.ExecuteEventPlayerLose();
            }
        }
    }
}