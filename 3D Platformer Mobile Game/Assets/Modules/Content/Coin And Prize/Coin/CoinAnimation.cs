using System;
using System.Collections;
using DG.Tweening;
using Modules.Content.Coin;
using UnityEngine;

[RequireComponent(typeof(CoinCollideDetector))]
public class CoinAnimation : BaseAnimation
{
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
    
    private IEnumerator DestroyAnimation(float duration)
    {
        Tween tween = transform.DOScale(Vector3.zero, duration);

        yield return tween.WaitForCompletion();

        transform.DOKill(true);
        
        gameObject.SetActive(false);
    }
}