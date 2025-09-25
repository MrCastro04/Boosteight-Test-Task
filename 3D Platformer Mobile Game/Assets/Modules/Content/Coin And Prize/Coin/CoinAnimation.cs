using DG.Tweening;
using Modules.Content.Coin;
using Modules.Content.Coin_And_Prize.Coin;
using UnityEngine;

[RequireComponent(typeof(CoinCollideDetector))]
public class CoinAnimation : BaseAnimation
{
    [SerializeField] private CoinCollideDetector _coinCollideDetector;
    
    private void OnEnable()
    {
        CoinEvents.OnCollect += PlayDestroyAnimationThenDisable;
    }

    private void OnDisable()
    {
        CoinEvents.OnCollect -= PlayDestroyAnimationThenDisable;
    }

    private async void PlayDestroyAnimationThenDisable(CoinCollideDetector coinCollideDetector)
    {
        if(coinCollideDetector != _coinCollideDetector) return;
        
       await transform.DOScale(Vector3.zero, _scaleDuration).AsyncWaitForCompletion();
       
       transform.DOKill(true);
        
       gameObject.SetActive(false);
    }
}