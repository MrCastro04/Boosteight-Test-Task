using System;
using System.Collections;
using Modules.Content.Player;
using UnityEngine;

namespace Modules.Content.Kill_Zone
{
    [RequireComponent(typeof(Collider))]
    public class KillZone : MonoBehaviour
    {
        [SerializeField] private bool _killWithTrigger = true;
        [SerializeField] private bool _destroySelfWithTimer = false;
        [SerializeField] private float _destroySelfDuration;

        private void OnEnable()
        {
            if (_destroySelfWithTimer & _destroySelfDuration > 0)
            {
                StartCoroutine(DestroySelfWithDelay(_destroySelfDuration));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(_killWithTrigger) return;
            
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerEvents.ExecuteEventPlayerLose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_killWithTrigger == false) return;
            
            if (other.CompareTag("Player"))
            {
                PlayerEvents.ExecuteEventPlayerLose();
            }
        }

        private IEnumerator DestroySelfWithDelay(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            StopAllCoroutines();
            
            Destroy(gameObject);
        }
    }
}