using System;

namespace Modules.Content.Coin_And_Prize.Prize
{
    public static class PrizeEvents
    {
        public static event Action OnCollect;
        public static void ExecuteEventGetPrize() => OnCollect?.Invoke();
    }
}