using UnityEngine;

namespace Modules.Content.Coin
{
    public class PrizeColliderDetector : MonoBehaviour, IColliderDetector
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
            }   
        }
    }
}