using DG.Tweening;
using Modules.Content.Coin;
using UnityEngine;

namespace Modules.Content.Coin_And_Prize.Prize
{
    public class PrizeAnimation : BaseAnimation
    {
        private void OnEnable()
        {
            PrizeEvents.OnCollect += PlayDestroyAnimationThenDisable;
        }

        private void OnDisable()
        {
            PrizeEvents.OnCollect -= PlayDestroyAnimationThenDisable;
        }
        
        protected override void Start()
         {
             transform.DOScale(Vector3.zero, _scaleDuration)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Yoyo);
             
             base.Start();
         }

        private async void PlayDestroyAnimationThenDisable()
        {
            await transform.DOScale(Vector3.zero, _scaleDuration).AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }
    }
}