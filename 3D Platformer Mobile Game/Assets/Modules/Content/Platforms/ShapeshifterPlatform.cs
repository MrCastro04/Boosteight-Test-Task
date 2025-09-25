using System.Collections;
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
            _killZone.CanKill = false;
     
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        
        private IEnumerator Start()
        {
            while (true)
            {
                _meshRenderer.material = _originalMaterial;
                _killZone.CanKill = false;
                
                yield return new WaitForSeconds(_timeToChangeOnKillZone);
                
                _meshRenderer.material = _killZoneMaterial;
                _killZone.CanKill = true;
                
                yield return new WaitForSeconds(_timeToChangeOnKillZone);
            }
        }
    }
}