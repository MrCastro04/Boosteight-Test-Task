using System.Collections.Generic;
using System.Linq;
using Modules.Content.Coin;
using Modules.Core.CheckPoint;
using UnityEngine;

namespace Modules.Core.Managers
{
    public class ManagerCheckPoint : MonoBehaviour
    {
        [SerializeField] private ChechPoint _checkPoint;

        private Queue<ChechPoint> _chechPoints = new();

        private void OnEnable()
        {
            CoinEvents.OnDestroyCoin += CreateCheckPoint;
        }

        private void OnDisable()
        {
            CoinEvents.OnDestroyCoin -= CreateCheckPoint;
        }

        private void CreateCheckPoint(Vector3 checkPointPosition, CoinCollideDetector coinCollideDetector)
        {
            DestroyLastCheckPoint();

            var newCheckPoint = Instantiate(_checkPoint, checkPointPosition, Quaternion.identity);

            _chechPoints.Enqueue(newCheckPoint);
        }

        private void DestroyLastCheckPoint()
        {
            if(_chechPoints.Any() == false) return;

            Debug.Log($"До {_chechPoints.Count} ");
            
            var lastChechPoint = _chechPoints.Dequeue();

            Debug.Log($"После {_chechPoints.Count} ");
            Destroy(lastChechPoint.gameObject);
        }
    }
}