using UnityEngine;

namespace Modules.Content.Coin
{
    public interface IColliderDetector
    {
        void OnTriggerEnter(Collider other);
    }
}