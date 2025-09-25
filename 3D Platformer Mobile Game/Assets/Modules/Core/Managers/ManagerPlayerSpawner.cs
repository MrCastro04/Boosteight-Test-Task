using Modules.Content.Player;
using Modules.Core.Managers;
using UnityEngine;

public class ManagerPlayerSpawner : MonoBehaviour
{
    [SerializeField] private ManagerCheckPoint _managerCheckPoint;
    [SerializeField] private PlayerController _playerController;
    
    private void OnEnable()
    {
        PlayerEvents.OnLose += SpawnAtLastCheckPoint;
    }

    private void OnDisable()
    {
        PlayerEvents.OnLose -= SpawnAtLastCheckPoint;
    }

    private void SpawnAtLastCheckPoint()
    {
        var lastCheckPoint = _managerCheckPoint.LastCheckPoint;

        _playerController.transform.position = lastCheckPoint.transform.position;
    }
}