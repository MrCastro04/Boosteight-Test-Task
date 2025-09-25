using System;
using DG.Tweening;
using UnityEngine;

namespace Modules.Content.Platforms
{
    public class RotateablePlatform : MonoBehaviour
    {
        [SerializeField] private bool _rotateOnlyX = false;
        [SerializeField] private bool _rotateOnlyY = false;
        [SerializeField] private bool _rotateOnlyZ = false;
        [SerializeField] private float _rotationDuration;
        [SerializeField] private Vector3 _rotateVector;
        
        private void Start()
        {
            transform.DORotate(_rotateVector, _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}