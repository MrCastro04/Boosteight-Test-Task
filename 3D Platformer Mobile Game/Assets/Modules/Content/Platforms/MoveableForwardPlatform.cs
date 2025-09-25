using System;
using DG.Tweening;
using UnityEngine;

public class MoveableForwardPlatform : MonoBehaviour
{
    [SerializeField] private float _zOffset = 0.5f;
    [SerializeField] private float _moveToPositionDurarion = 7f;
    
    private void Start()
    {
        MoveByZCoordinate();
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

    private void MoveByZCoordinate()
    {
        transform
            .DOMoveZ(transform.position.z + _zOffset, _moveToPositionDurarion)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
