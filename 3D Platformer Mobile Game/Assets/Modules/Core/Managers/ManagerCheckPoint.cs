using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Content.Coin;
using Modules.Content.Player;
using Modules.Core.CheckPoint;
using UnityEngine;

namespace Modules.Core.Managers
{
    public class ManagerCheckPoint : MonoBehaviour
    {
        [SerializeField] private ChechPoint _checkPoint;
        [SerializeField] private Transform _startCheckPointTransform;

        private Queue<ChechPoint> _chechPoints = new();
        public ChechPoint LastCheckPoint => _chechPoints.Peek();
        
        private void OnEnable()
        {
            CoinEvents.OnDestroyCoin += UpdateCheckPoints;
        }

        private void OnDisable()
        {
            CoinEvents.OnDestroyCoin -= UpdateCheckPoints;
        }

        private void Start()
        {
            CreateCheckPoint(_startCheckPointTransform.position);
        }

        private void CreateCheckPoint(Vector3 checkPointPosition)
        {
            var newCheckPoint = Instantiate(_checkPoint, checkPointPosition, Quaternion.identity);

            _chechPoints.Enqueue(newCheckPoint);
        }

        private void DestroyLastCheckPoint()
        {
            if (_chechPoints.Any() == false) return;

            Debug.Log($"До {_chechPoints.Count} ");

            var lastChechPoint = _chechPoints.Dequeue();

            Debug.Log($"После {_chechPoints.Count} ");
            Destroy(lastChechPoint.gameObject);
        }

        private void UpdateCheckPoints(Vector3 checkPointPosition)
        {
            DestroyLastCheckPoint();

            CreateCheckPoint(checkPointPosition);
        }
    }
}