using System;
using DG.Tweening;
using UnityEngine;

public class GreenArrowAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveZ(transform.position.z + 0.5f , 1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
