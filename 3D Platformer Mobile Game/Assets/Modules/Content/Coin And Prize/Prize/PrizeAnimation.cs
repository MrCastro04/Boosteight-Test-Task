using DG.Tweening;
using UnityEngine;

namespace Modules.Content.Coin
{
    public class PrizeAnimation : BaseAnimation
    {
        protected override void Start()
         {
             transform.DOScale(Vector3.zero, _scaleDuration)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Yoyo);
             
             base.Start();
         }
    }
}