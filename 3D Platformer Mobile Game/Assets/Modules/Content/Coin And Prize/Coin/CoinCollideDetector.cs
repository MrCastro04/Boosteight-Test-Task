using UnityEngine;

namespace Modules.Content.Coin
{
    public class CoinCollideDetector : MonoBehaviour , IColliderDetector
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoinEvents.ExecuteEventCoinCollect(this);
            }
        }

        private void OnDisable()
        {
           CoinEvents.ExecuteEventCoinDestroy(transform.position);
        }
    }
}