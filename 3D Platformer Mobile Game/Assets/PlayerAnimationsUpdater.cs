using System;
using UnityEngine;

public class PlayerAnimationsUpdater : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    private string _speed = "Speed";
    
    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        float speed = _animator.GetFloat(_speed);

        float smooth = Time.deltaTime * _playerController.PlayerAcceleration;

        if (_playerController.IsMoving)
        {
            speed += smooth;
        }

        else
        {
            speed -= smooth;
        }

        speed = Mathf.Clamp01(speed);

        _animator.SetFloat(_speed, speed);
    }
}