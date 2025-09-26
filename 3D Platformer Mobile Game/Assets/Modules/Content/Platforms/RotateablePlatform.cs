using System;
using DG.Tweening;
using UnityEngine;

namespace Modules.Content.Platforms
{
    public class RotateablePlatform : MonoBehaviour
    {
        [SerializeField] private float _rotationDuration;
        [SerializeField] private Vector3 _rotateVector;
        
        private void Start()
        {
            transform
                .DORotate(_rotateVector, _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}