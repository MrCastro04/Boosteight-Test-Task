using System;
using Modules.Content.Player;
using UnityEngine;

namespace Modules.Content.Kill_Zone
{
    [RequireComponent(typeof(BoxCollider))]
    public class TrapZone : MonoBehaviour
    {
        [SerializeField] private int _activateCount = 1;

        private int _currentActivatesCount = 0;

        private void OnEnable()
        {
            PlayerEvents.OnLose += () => _currentActivatesCount = 0;
        }

        private void OnDisable()
        {
            PlayerEvents.OnLose += () => _currentActivatesCount = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_currentActivatesCount >= _activateCount) return;
            
            if (other.CompareTag("Player"))
            {
                _currentActivatesCount ++;
                
                TrapEvents.ExecuteEventActivateTrap();
            }
        }
    }
}