using System;
using System.Collections;
using DG.Tweening;
using Modules.Content.Coin;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private float _upDownDuration;
    [SerializeField] private float _destroyDuration = 0.4f;
    [SerializeField] private CoinCollideDetector _coinCollideDetector;

    private void OnEnable()
    {
        CoinEvents.OnCollect += PlayDestroyAnimationThenDestory;
    }

    private void OnDisable()
    {
        CoinEvents.OnCollect -= PlayDestroyAnimationThenDestory;
    }

    private void PlayDestroyAnimationThenDestory(CoinCollideDetector coinCollideDetector)
    {
        if(coinCollideDetector != _coinCollideDetector) return;
        
        StartCoroutine(DestroyAnimation(_destroyDuration));
    }

    private void Start()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), _rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);

        transform.DOMoveY(transform.position.y + 1, _upDownDuration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private IEnumerator DestroyAnimation(float duration)
    {
        Tween tween = transform.DOScale(Vector3.zero, duration);

        yield return tween.WaitForCompletion();

        transform.DOKill(true);
        
        gameObject.SetActive(false);
    }
}