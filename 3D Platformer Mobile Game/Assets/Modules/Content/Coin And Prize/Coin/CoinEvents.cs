using System;
using Modules.Content.Coin;
using UnityEngine;

namespace Modules.Content.Coin_And_Prize.Coin
{
    public static class CoinEvents
    {
        public static event Action<CoinCollideDetector> OnCollect;
        public static event Action<Vector3> OnDestroyCoin;

        public static void ExecuteEventCoinCollect(CoinCollideDetector coinCollideDetector)
        {
            OnCollect?.Invoke(coinCollideDetector);
        }

        public static void ExecuteEventCoinDestroy(Vector3 destroyPosition)
        {
            OnDestroyCoin?.Invoke(destroyPosition);
        }
    }
}