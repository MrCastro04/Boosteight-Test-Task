using System;
using System.Collections;
using Modules.Content.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Modules.Content.Kill_Zone
{
    [RequireComponent(typeof(Collider))]
    public class KillZone : MonoBehaviour
    {
        public bool CanKillAtStart = false;
        [SerializeField] private bool _killWithTrigger = true;
        [SerializeField] private bool _destroySelfWithTimer = false;
        [SerializeField] private float _destroySelfDuration;

        private Collider _collider;


        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            if (_killWithTrigger)
            {
                _collider.isTrigger = true;
            }
            else
            {
                _collider.isTrigger = false;
            }

            if (_destroySelfWithTimer & _destroySelfDuration > 0)
            {
                StartCoroutine(DestroySelfWithDelay(_destroySelfDuration));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"{_killWithTrigger}");
            if (_killWithTrigger | CanKillAtStart == false) return;

            if (other.gameObject.CompareTag("Player"))
            {
                PlayerEvents.ExecuteEventPlayerLose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_killWithTrigger == false | CanKillAtStart == false) return;

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