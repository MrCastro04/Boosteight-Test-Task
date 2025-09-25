using Modules.Content.Kill_Zone;
using UnityEngine;

namespace Modules.Core.Managers
{
    public class ManagerKillZoneWithTrap : MonoBehaviour
    {
        [SerializeField] private KillZone _killZone;
        [SerializeField] private Transform _killZoneSpawnTransform;
        
        private void OnEnable()
        {
            TrapEvents.OnActivateTrap += SpawnKillZone;
        }

        private void OnDisable()
        {
            TrapEvents.OnActivateTrap -= SpawnKillZone;
        }

        private void SpawnKillZone()
        {
            Instantiate(_killZone, _killZoneSpawnTransform.position, _killZoneSpawnTransform.rotation);
        }
    }
}