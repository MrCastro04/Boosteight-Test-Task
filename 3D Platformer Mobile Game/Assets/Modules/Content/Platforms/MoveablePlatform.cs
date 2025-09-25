using DG.Tweening;
using UnityEngine;

namespace Modules.Content.Platforms
{
    public class MoveablePlatform : MonoBehaviour
    {
        [SerializeField] private bool _moveOnlyX = false;
        [SerializeField] private bool _moveOnlyY = false;
        [SerializeField] private bool _moveOnlyZ = false;
        [SerializeField] private float _offset = 0.5f;
        [SerializeField] private float _moveToPositionDurarion = 7f;

        private void Start()
        {
            if (_moveOnlyX)
            {
                MoveByCoordinateX();
                return;
            }

            if (_moveOnlyY)
            {
                MoveByCoordinateY();
                return;
            } 
            
            if (_moveOnlyZ)
            {
                MoveByCoordinateZ();
                return;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.parent = transform;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.parent = null;
            }
        }

        private void MoveByCoordinateX()
        {
            transform
                .DOMoveX(transform.position.x + _offset, _moveToPositionDurarion)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void MoveByCoordinateY()
        {
            transform
                .DOMoveY(transform.position.y + _offset, _moveToPositionDurarion)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void MoveByCoordinateZ()
        {
            transform
                .DOMoveZ(transform.position.z + _offset, _moveToPositionDurarion)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}