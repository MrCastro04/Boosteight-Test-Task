using System;
using UnityEngine;

namespace Modules.Content.Coin
{
    public static class CoinEvents
    {
        public static event Action<CoinCollideDetector> OnCollect;
        public static event Action<Vector3> OnDestroyCoin;

        public static void ExecuteEventCollect(CoinCollideDetector coinCollideDetector)
        {
            OnCollect?.Invoke(coinCollideDetector);
        }

        public static void ExecuteEventDestroyCoin(Vector3 destroyPosition)
        {
            OnDestroyCoin?.Invoke(destroyPosition);
        }
    }
}