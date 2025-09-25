using UnityEngine;

namespace Modules.Content.Coin
{
    public class CoinCollideDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoinEvents.ExecuteEventCollect(this);
            }
        }

        private void OnDisable()
        {
           CoinEvents.ExecuteEventDestroyCoin(transform.position);
        }
    }
}