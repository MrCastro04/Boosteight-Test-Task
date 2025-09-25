using System;
using System.Collections;
using Modules.Content.Coin;
using Modules.Content.Kill_Zone;
using UnityEngine;

namespace Modules.Content.Platforms
{
    [RequireComponent(typeof(KillZone))]
    public class ShapeshifterPlatform : MonoBehaviour
    {
        [SerializeField] private Material _killZoneMaterial;
        [SerializeField] private Material _originalMaterial;
        [SerializeField] private KillZone _killZone;
        [SerializeField] private float _timeToChangeOnKillZone = 5f;

        private MeshRenderer _meshRenderer;
        
        private void Awake()
        {
            _killZone.CanKillAtStart = false;
            // Получаем компонент MeshRenderer
            _meshRenderer = GetComponent<MeshRenderer>();
            
            // Если на этом объекте нет MeshRenderer, ищем в дочерних объектах
            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponentInChildren<MeshRenderer>();
            }
        }
        
        private IEnumerator Start()
        {
            while (true)
            {
                // Устанавливаем обычный материал
                _meshRenderer.material = _originalMaterial;
                _killZone.CanKillAtStart = false;
                
                yield return new WaitForSeconds(_timeToChangeOnKillZone);

                // Устанавливаем материал зоны убийства
                _meshRenderer.material = _killZoneMaterial;
                _killZone.CanKillAtStart = true;
                
                yield return new WaitForSeconds(_timeToChangeOnKillZone);
            }
        }
    }
}