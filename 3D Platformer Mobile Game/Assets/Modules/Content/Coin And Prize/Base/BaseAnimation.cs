using System;
using DG.Tweening;
using UnityEngine;

namespace Modules.Content.Coin
{
    public abstract class BaseAnimation : MonoBehaviour
    {
        [SerializeField] protected float _scaleDuration;
        [SerializeField] protected float _rotationDuration = 1f;
        [SerializeField] protected float _upDownDuration;

        protected virtual void Start()
        {
            transform.DORotate(new Vector3(0f, 360f, 0f), _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);

            transform.DOMoveY(transform.position.y + 1, _upDownDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}